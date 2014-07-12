Module modCMD_Checks
    Public Sub CMD_Checks()

        'CMD (commands received from the ground station or executed as part of a mission):
        'CTot: the total number of commands in the mission
        'CNum: this Command 's number in the mission (0 is always home, 1 is the first command, etc)
        'CId: the mavlink message id
        '       https://pixhawk.ethz.ch/mavlink/   see MAV_CMD section

        'Copt: the option parameter (used for many different purposes)
        'Prm1: the Command 's parameter (used for many different purposes)
        'Alt:  the Command 's altitude in meters
        'Lat:  the Command 's latitude position
        'Lng:  the Command 's longitude position

        If frmMainForm.chkboxAutoCommands.Checked = True Then
            If DataArray(0) = "CMD" Then
                If ReadFileResilienceCheck(8) = True Then
                    Log_CMD_CTot = Val(DataArray(1)) - 1
                    Log_CMD_CNum = Val(DataArray(2))
                    Log_CMD_CId = Val(DataArray(3))
                    Log_CMD_Copt = Val(DataArray(4))
                    Log_CMD_Prm1 = Val(DataArray(5))
                    Log_CMD_Alt = Val(DataArray(6))
                    Log_CMD_Lat = Val(DataArray(7))
                    Log_CMD_Lng = Val(DataArray(8))


                    If Log_CMD_CNum <> 0 Then
                        If Log_CMD_CId = 16 Or Log_CMD_CId = 20 Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:    Hit WP" & Log_CMD_CNum - 1 & " Radius: " & Format(Log_GPS_Lat, "00.0000000") & " " & Format(Log_GPS_Lng, "00.0000000") & " at an Altitude of " & Log_CTUN_BarAlt & "m  -  " & Format(CalculateDistance(Log_GPS_Lat, Log_GPS_Lng, Log_Last_CMD_Lat, Log_Last_CMD_Lng) * 1000, "0.000") & "m to center.")
                            If (Log_CTUN_BarAlt < Log_Last_CMD_Alt - 4 Or Log_CTUN_BarAlt > Log_Last_CMD_Alt + 4) And Log_CMD_CNum > 2 Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Warning: UAV is struggling to maintain desired altitude!")
                            End If
                        End If
                        WriteTextLog("")
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: " & Log_CMD_CNum & " of " & Log_CMD_CTot)
                    End If

                    Select Case Log_CMD_CId
                        Case 16
                            If Log_CMD_CNum = 0 Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Set Home as " & Format(Log_CMD_Lat, "00.0000000") & " " & Format(Log_CMD_Lng, "00.0000000") & " Alt:" & Log_CMD_Alt)
                            Else
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Navigate to New Way Point")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Navigate From WP" & Log_CMD_CNum - 1 & ": " & Format(Log_Last_CMD_Lat, "00.0000000") & " " & Format(Log_Last_CMD_Lng, "00.0000000") & " with current Altitude of " & Log_Last_CMD_Alt & "m")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:            to WP" & Log_CMD_CNum & ": " & Format(Log_CMD_Lat, "00.0000000") & " " & Format(Log_CMD_Lng, "00.0000000") & " with current Altitude of " & Log_CMD_Alt & "m")
                                'WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: GPS - WP Distance: " & Format(CalculateDistance(Log_GPS_Lat, Log_GPS_Lng, Log_CMD_Lat, Log_CMD_Lng) * 1000, "0.00") & "m")
                                Log_CMD_Dist1 = Log_CMD_Dist1 + CalculateDistance(Log_GPS_Lat, Log_GPS_Lng, Log_CMD_Lat, Log_CMD_Lng)
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  WP - WP Distance: " & Format(CalculateDistance(Log_Last_CMD_Lat, Log_Last_CMD_Lng, Log_CMD_Lat, Log_CMD_Lng) * 1000, "0.00") & "m")
                                Log_CMD_Dist2 = Log_CMD_Dist2 + CalculateDistance(Log_Last_CMD_Lat, Log_Last_CMD_Lng, Log_CMD_Lat, Log_CMD_Lng)
                                If Log_Last_CMD_Alt < 5 Then
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  Warning: Navigating to a new GPS position from a low altitude can be dangerous!")
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  Warning: Insert a Take Off command to a good altitude at the start of the mission.")
                                    Log_CMD_Dist1 = 0
                                    Log_CMD_Dist2 = 0
                                End If
                            End If
                        Case 20
                            'CMD, 7, 6, 20, 1, 0, 0.00, 0.0000000, 0.0000000
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Return to launch location")
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Navigate From WP" & Log_CMD_CNum - 1 & ": " & Format(Log_Last_CMD_Lat, "00.0000000") & " " & Format(Log_Last_CMD_Lng, "00.0000000") & " with current Altitude of " & Log_Last_CMD_Alt & "m")
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:      Navigate RTL: " & Format(First_GPS_Lat, "00.0000000") & " " & Format(First_GPS_Lng, "00.0000000") & " with a final Altitude of " & Log_CMD_Alt & "m")
                            'WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Distance Away: " & Format(CalculateDistance(Log_GPS_Lat, Log_GPS_Lng, First_GPS_Lat, First_GPS_Lng) * 1000, "0.00") & "m")
                            Log_CMD_Dist1 = Log_CMD_Dist1 + CalculateDistance(Log_GPS_Lat, Log_GPS_Lng, First_GPS_Lat, First_GPS_Lng)
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  WP - WP Distance: " & Format(CalculateDistance(Log_Last_CMD_Lat, Log_Last_CMD_Lng, First_GPS_Lat, First_GPS_Lng) * 1000, "0.00") & "m")
                            Log_CMD_Dist2 = Log_CMD_Dist2 + CalculateDistance(Log_Last_CMD_Lat, Log_Last_CMD_Lng, First_GPS_Lat, First_GPS_Lng)
                            If Log_Last_CMD_Alt < 5 Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  Warning: Navigating to a new GPS position from a low altitude can be dangerous!")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  Warning: Insert a Take Off command to a good altitude at the start of the mission.")
                                Log_CMD_Dist1 = 0
                                Log_CMD_Dist2 = 0
                            End If

                        Case 22
                            'CMD, 4, 1, 22, 1, 0, 30.48, 0.0000000, 0.0000000
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Takeoff from ground / hand")
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Climb to Alt of " & Log_CMD_Alt & "m   with a Climb Rate of " & Log_CMD_Copt & "m/s")

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
                    If Log_CMD_CTot = Log_CMD_CNum Then
                        'WriteTextLog("Testing GPS to WP (Dist1) = " & Log_CMD_Dist1 & "m")
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Mission WP to WP Dist: " & Format(Log_CMD_Dist2, "0.00") & "km")
                        WriteTextLog("")
                    End If

                End If
            End If
        End If


    End Sub
End Module
