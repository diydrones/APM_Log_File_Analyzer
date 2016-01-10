Module modCMD_Checks_MainAnalysis
    Public Sub CMD_Checks_MainAnalysis()

        ' This part only checks the already preset Variables for the data line.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE VARIABLES HAVE BEEN SET !!!

        'CMD (commands received from the ground station or executed as part of a mission):
        'CTot: the total number of commands in the mission
        'CNum: this Command 's number in the mission (0 is always home, 1 is the first command, etc)
        'CId: the mavlink message id
        '       https://github.com/diydrones/ardupilot/blob/master/libraries/GCS_MAVLink/message_definitions/common.xml
        '           Find the part <enum name="MAV_CMD">
        'Copt: the option parameter (used for many different purposes)
        'Prm1: the Command 's parameter (used for many different purposes)
        'Alt:  the Command 's altitude in meters
        'Lat:  the Command 's latitude position
        'Lng:  the Command 's longitude position

        ' in v3.2 it is possible to have strange CMD Datalines, this has been added to ignore them.
        If Log_CMD_CTot <> 0 Then

            ' Only run this if we are flying a mission, it details where we are etc.
            If AUTO_NewMission = False And (Log_CMD_CId = 16 Or Log_CMD_CId = 20) Then
                If Log_CMD_CNum < 10 Then   ' just format spacing.
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Hit WP" & Log_CMD_CNum - 1 & " Radius            : " & Format(Log_GPS_Lat, "00.0000000") & " " & Format(Log_GPS_Lng, "00.0000000") & " at an Altitude of " & Log_CTUN_BarAlt & "m  -  " & Format(CalculateDistance(Log_GPS_Lat, Log_GPS_Lng, Log_Last_CMD_Lat, Log_Last_CMD_Lng) * 1000, "0.000") & "m to center.")
                Else
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Hit WP" & Log_CMD_CNum - 1 & " Radius           : " & Format(Log_GPS_Lat, "00.0000000") & " " & Format(Log_GPS_Lng, "00.0000000") & " at an Altitude of " & Log_CTUN_BarAlt & "m  -  " & Format(CalculateDistance(Log_GPS_Lat, Log_GPS_Lng, Log_Last_CMD_Lat, Log_Last_CMD_Lng) * 1000, "0.000") & "m to center.")
                End If
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: WP - WP Distance was      : " & Log_CMD_Dist1 & " m")
                If Log_CMD_WP_PreviousTime <> GPS_Base_Date Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: WP - WP Time was          : " & DateDiff(DateInterval.Second, Log_CMD_WP_PreviousTime, Log_GPS_DateTime) & " seconds")
                End If
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: WP - WP Speed was         : " & Log_CMD_Dist1 / (DateDiff(DateInterval.Second, Log_CMD_WP_PreviousTime, Log_GPS_DateTime)) & " m/s")
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: WP - WP Used Capacity was : " & (LOG_CURR_CurrTot - Log_CMD_WPtoWP_Eff) & " mAh")
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: WP - WP Efficiency was    : " & (LOG_CURR_CurrTot - Log_CMD_WPtoWP_Eff) / Log_CMD_Dist1 & " mAh/m (mAh per Meter)")
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: WP - WP Efficiency Result : " & 1000 / ((LOG_CURR_CurrTot - Log_CMD_WPtoWP_Eff) / Log_CMD_Dist1) & " m/A (Meters per Amp)")

                If (Log_CTUN_BarAlt < Log_Last_CMD_Alt - 4 Or Log_CTUN_BarAlt > Log_Last_CMD_Alt + 4) And Log_CMD_CNum > 2 Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Warning: UAV is struggling to maintain desired altitude!")
                End If
            End If

            If AUTO_NewMission = True And Log_CMD_CNum = 1 Then
                WriteTextLog("List of Saved AUTO Commands, these will be executed if the UAV is switched to Auto:-")
            End If

            ' Detail the Command Number
            WriteTextLog("")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: " & Log_CMD_CNum & " of " & Log_CMD_CTot)


            Select Case Log_CMD_CId
                Case 16   ' Navigate to New WayPoint
                    'v3.2  ---  CMD, 118727, 16, 3, 16, 0, 0, 0, 0, 53.45058, -1.211618, 44.8
                    If Log_CMD_CNum = 1 Then
                        If AUTO_NewMission = False Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Set Home as " & Format(Log_CMD_Lat, "00.0000000") & " " & Format(Log_CMD_Lng, "00.0000000") & " Alt:" & Log_CMD_Alt)
                        Else
                            'First_GPS_Lat = Log_CMD_Lat : First_GPS_Lng = Log_CMD_Lng
                            'Log_Last_CMD_Lat = Log_CMD_Lat : Log_Last_CMD_Lng = Log_CMD_Lng
                        End If
                    End If
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Navigate to New Way Point")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Navigate From WP" & Log_CMD_CNum - 1 & ": " & Format(Log_Last_CMD_Lat, "00.0000000") & " " & Format(Log_Last_CMD_Lng, "00.0000000") & " with current Altitude of " & Log_Last_CMD_Alt & "m")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:            to WP" & Log_CMD_CNum & ": " & Format(Log_CMD_Lat, "00.0000000") & " " & Format(Log_CMD_Lng, "00.0000000") & " with current Altitude of " & Log_CMD_Alt & "m")
                    'WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: GPS - WP Distance: " & Format(CalculateDistance(Log_GPS_Lat, Log_GPS_Lng, Log_CMD_Lat, Log_CMD_Lng) * 1000, "0.00") & "m")
                    'Log_CMD_Dist1 = Log_CMD_Dist1 + CalculateDistance(Log_GPS_Lat, Log_GPS_Lng, Log_CMD_Lat, Log_CMD_Lng)
                    Log_CMD_Dist1 = CalculateDistance(Log_Last_CMD_Lat, Log_Last_CMD_Lng, Log_CMD_Lat, Log_CMD_Lng) * 1000
                    If AUTO_NewMission = True And Log_CMD_CNum = 1 Then
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  WP - WP Distance: This distance will depend on the take off GPS Coordinates")
                    Else
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  WP - WP Distance: " & Format(CalculateDistance(Log_Last_CMD_Lat, Log_Last_CMD_Lng, Log_CMD_Lat, Log_CMD_Lng) * 1000, "0.00") & "m")
                    End If

                    Log_CMD_Dist2 = Log_CMD_Dist2 + CalculateDistance(Log_Last_CMD_Lat, Log_Last_CMD_Lng, Log_CMD_Lat, Log_CMD_Lng)

                    Log_CMD_WPtoWP_Eff = LOG_CURR_CurrTot
                    Log_CMD_WP_PreviousTime = Log_GPS_DateTime

                    If Log_Last_CMD_Alt < 5 And Log_CMD_CNum <> 1 Then
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  Warning: Navigating to a new GPS position from a low altitude can be dangerous!")
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  Warning: Insert a Take Off command to a good altitude at the start of the mission.")
                        Log_CMD_Dist1 = 0
                        Log_CMD_Dist2 = 0
                    End If


                Case 20    ' Return to Launch
                    'v3.1  ---  CMD, 7, 6, 20, 1, 0, 0.00, 0.0000000, 0.0000000
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Return to launch location")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Navigate From WP" & Log_CMD_CNum - 1 & ": " & Format(Log_Last_CMD_Lat, "00.0000000") & " " & Format(Log_Last_CMD_Lng, "00.0000000") & " with current Altitude of " & Log_CTUN_BarAlt & "m")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:      Navigate RTL: " & Format(First_GPS_Lat, "00.0000000") & " " & Format(First_GPS_Lng, "00.0000000") & " with a final Altitude of " & Log_CMD_Alt & "m")
                    'WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Distance Away: " & Format(CalculateDistance(Log_GPS_Lat, Log_GPS_Lng, First_GPS_Lat, First_GPS_Lng) * 1000, "0.00") & "m")
                    'Log_CMD_Dist1 = Log_CMD_Dist1 + CalculateDistance(Log_GPS_Lat, Log_GPS_Lng, First_GPS_Lat, First_GPS_Lng)
                    Log_CMD_Dist1 = CalculateDistance(Log_Last_CMD_Lat, Log_Last_CMD_Lng, Log_CMD_Lat, Log_CMD_Lng) * 1000
                    If AUTO_NewMission = True Then
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  WP - WP Distance: This distance will depend on the take off GPS Coordinates")
                    Else
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  WP - WP Distance: " & Format(CalculateDistance(Log_Last_CMD_Lat, Log_Last_CMD_Lng, First_GPS_Lat, First_GPS_Lng) * 1000, "0.00") & "m")
                    End If
                    Log_CMD_Dist2 = Log_CMD_Dist2 + CalculateDistance(Log_Last_CMD_Lat, Log_Last_CMD_Lng, First_GPS_Lat, First_GPS_Lng)
                    If Log_Last_CMD_Alt < 5 Then
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  Warning: Navigating to a new GPS position from a low altitude can be dangerous!")
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  Warning: Insert a Take Off command to a good altitude at the start of the mission.")
                        Log_CMD_Dist1 = 0
                        Log_CMD_Dist2 = 0
                    End If

                Case 22     'Take Off
                    'v3.1  ---  CMD, 4, 1, 22, 1, 0, 30.48, 0.0000000, 0.0000000
                    'v3.2  ---  CMD, 102710, 16, 2, 22, 0, 0, 0, 0, 0, 0, 30.48
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Takeoff from ground / hand")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Climb to Alt of " & Log_CMD_Alt & " m")


                Case 82     ' Navigate to New Spline WayPoint
                    'v3.2  ---  CMD, 145154, 10, 4, 82, 0, 0, 0, 0, 53.45088, -1.211146, 30.48
                    If AUTO_NewMission = False And Log_CMD_CNum = 1 Then
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Set Home as " & Format(Log_CMD_Lat, "00.0000000") & " " & Format(Log_CMD_Lng, "00.0000000") & " Alt:" & Log_CMD_Alt)
                    End If
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Navigate to New Spline Way Point")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Navigate From WP" & Log_CMD_CNum - 1 & ": " & Format(Log_Last_CMD_Lat, "00.0000000") & " " & Format(Log_Last_CMD_Lng, "00.0000000") & " with current Altitude of " & Log_Last_CMD_Alt & "m")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:            to WP" & Log_CMD_CNum & ": " & Format(Log_CMD_Lat, "00.0000000") & " " & Format(Log_CMD_Lng, "00.0000000") & " with current Altitude of " & Log_CMD_Alt & "m")
                    'WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: GPS - WP Distance: " & Format(CalculateDistance(Log_GPS_Lat, Log_GPS_Lng, Log_CMD_Lat, Log_CMD_Lng) * 1000, "0.00") & "m")
                    'Log_CMD_Dist1 = Log_CMD_Dist1 + CalculateDistance(Log_GPS_Lat, Log_GPS_Lng, Log_CMD_Lat, Log_CMD_Lng)
                    Log_CMD_Dist1 = CalculateDistance(Log_Last_CMD_Lat, Log_Last_CMD_Lng, Log_CMD_Lat, Log_CMD_Lng) * 1000
                    'WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  WP - WP Distance: " & Format(CalculateDistance(Log_Last_CMD_Lat, Log_Last_CMD_Lng, Log_CMD_Lat, Log_CMD_Lng) * 1000, "0.00") & "m")
                    Log_CMD_Dist2 = Log_CMD_Dist2 + CalculateDistance(Log_Last_CMD_Lat, Log_Last_CMD_Lng, Log_CMD_Lat, Log_CMD_Lng)

                    Log_CMD_WPtoWP_Eff = LOG_CURR_CurrTot
                    Log_CMD_WP_PreviousTime = Log_GPS_DateTime

                    If AUTO_NewMission = False And Log_Last_CMD_Alt < 5 Then
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  Warning: Navigating to a new GPS position from a low altitude can be dangerous!")
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  Warning: Insert a Take Off command to a good altitude at the start of the mission.")
                        Log_CMD_Dist1 = 0
                        Log_CMD_Dist2 = 0
                    End If


                Case 113    'CONDITION_CHANGE_ALT
                    'v3.2  ---  CMD, 381523, 16, 14, 113, 15.24, 0, 0, 0, 0, 0, 15.24
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:     Change Altitude to " & Log_CMD_Prm1 & " m @ " & Log_CMD_Prm2 & " m/s")
                    If Log_CMD_Prm2 = 0 Then
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Warning: Command will be ignored as m/s = 0")
                    End If
                    WriteTextLog("")

                Case 178    'DO_CHANGE_SPEED
                    'v3.2  ---  CMD, 102710, 16, 1, 178, 0, 4, 0, 0, 0, 0, 0
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Change WP Speed to        : " & Log_CMD_Prm2 & " m/s")
                    WriteTextLog("")
                    Log_CMD_WP_PreviousSpeed = Log_CMD_WP_Speed
                    Log_CMD_WP_Speed = Log_CMD_Prm2

                Case 201    'DO_SET_ROI
                    'v3.2  ---  CMD, 145154, 10, 3, 201, 0, 0, 0, 0, 53.45024, -1.212013, 30.48
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Set ROI Point : " & Format(Log_CMD_Lat, "00.0000000") & " " & Format(Log_CMD_Lng, "00.0000000") & " Alt:" & Log_CMD_Alt)
                    WriteTextLog("")

                Case Else
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: " & Log_CMD_CId & " - NEW COMMAND, UPDATE CODE TO HANDLE")
            End Select


            'Remember Next Way Point GPS Co-ords so we can calculate distance correctly
            If Log_CMD_Lat <> 0 And Log_CMD_Lng <> 0 Then
                'if we have a correct WP co-ord then use that.
                Log_Last_CMD_Lat = Log_CMD_Lat
                Log_Last_CMD_Lng = Log_CMD_Lng
            Else
                'sometimes at the start we do not have a WP co-ord in the buffer so use the current GPS position
                'in an attempt to calculate the distance.
                Log_Last_CMD_Lat = Log_GPS_Lat
                Log_Last_CMD_Lng = Log_GPS_Lng
            End If

            If Log_CMD_Alt <> 0 Then
                Log_Last_CMD_Alt = Log_CMD_Alt
            End If

            Debug.Print("Testing GPS to WP (Dist1) = " & Log_CMD_Dist1 * 1000 & "m")
            Debug.Print(" Testing WP to WP (Dist2) = " & Log_CMD_Dist2 * 1000 & "m")


            ' If this is the final Way Point Command in the mission the display the total planned distance.
            If AUTO_NewMission = False And Log_CMD_CTot = Log_CMD_CNum Then
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Mission WP to WP Dist: " & Format(Log_CMD_Dist2, "0.00") & "km")
                WriteTextLog("")
            End If
            If AUTO_NewMission = True And Log_CMD_CTot = Log_CMD_CNum Then
                WriteTextLog("End of Saved AUTO Commands List")
                WriteTextLog("")
            End If
        End If
    End Sub
End Module
