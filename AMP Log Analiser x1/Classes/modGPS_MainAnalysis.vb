

Module modGPS_MainAnalysis
    Public Sub GPS_MainAnalysis()
        If Log_GPS_Glitch = False Then

            'Calculate GPS Date and Time from Log_GPS_Week & Log_GPS_TimeMS
            'Debug.Print("BEFORE:- Log_GPS_DateTime = " & Log_GPS_DateTime)
            Log_GPS_DateTime = GPS_Base_Date
            'Debug.Print("BASE Date:- Log_GPS_DateTime = " & Log_GPS_DateTime)
            Log_GPS_DateTime = Log_GPS_DateTime.AddDays(7 * Log_GPS_Week)
            'Debug.Print("Calculation:- Add Days = " & 7 * Log_GPS_Week)
            'Debug.Print("Result:- Log_GPS_DateTime = " & Log_GPS_DateTime)
            Log_GPS_DateTime = Log_GPS_DateTime.AddMilliseconds(Log_GPS_TimeMS)
            'Debug.Print("Calculation:- Add MS = " & Log_GPS_TimeMS)
            'Debug.Print("After:- Log_GPS_DateTime = " & Log_GPS_DateTime & vbNewLine)

            'Store the First GPS Date & Time captured
            If Log_GPS_DateTime <> GPS_Base_Date And Log_First_DateTime = GPS_Base_Date Then _
                    Log_First_DateTime = Log_GPS_DateTime

            ' Detect the DataLog file Truncation Issue using the GPS DateTime
            If Log_GPS_DateTime < Log_First_DateTime Then
                Debug.Print("Found Truncation Issue, Signal main code to stop reading!")
                WriteTextLog("WARNING: DataLog File Truncation Issue Found")
                WriteTextLog("WARNING: This is an issue within the ArduCopter Firmware, APM Log Analyser has handled the issue.")
                MsgBox("DataLog File Truncation Issue Detected" & vbNewLine, vbOKOnly, "Error")
                TruncationIssue = True
                Exit Sub
            End If

            'Store the Last GPS Date & Time, actually the same as Log_GPS_DateTime but easier name for later :)
            Log_Last_DateTime = Log_GPS_DateTime

            'Record the "Landed" / "in Flight" Data
            If Log_In_Flight = False Then
                'Record the "Landed" Data

                'Record the Min / Max HDop
                If Log_GPS_HDop < Log_GPS_Min_HDop Then Log_GPS_Min_HDop = Log_GPS_HDop
                If Log_GPS_HDop > Log_GPS_Max_HDop Then Log_GPS_Max_HDop = Log_GPS_HDop

                'Record the Number of Sats
                If Log_GPS_NSats < Log_GPS_Min_NSats Then Log_GPS_Min_NSats = Log_GPS_NSats
                If Log_GPS_NSats > Log_GPS_Max_NSats Then Log_GPS_Max_NSats = Log_GPS_NSats
            Else
                'Record the "In Flight" Data

                'Collect the data for this Flight Mode Summary if CTUN is not logging using the best GPS data we can.
                If CTUN_Logging = False Then

                    'Validate the new Log_GPS_Alt, only allow changes of -1 or +1 at a time
                    If Log_GPS_Last_Alt = 0 Then Log_GPS_Last_Alt = Log_Temp_Ground_GPS_Alt
                    If Log_GPS_Alt > Log_GPS_Last_Alt Then Log_GPS_Alt = Log_GPS_Last_Alt + 1
                    If Log_GPS_Alt < Log_GPS_Last_Alt Then Log_GPS_Alt = Log_GPS_Last_Alt - 1

                    'only update these if we have a good GPS lock
                    If Log_GPS_Status = 3 Then
                        Log_GPS_Last_Alt = Log_GPS_Alt
                        Log_GPS_Calculated_Alt = Log_GPS_Alt - Log_Temp_Ground_GPS_Alt


                        If Log_GPS_Calculated_Alt < Log_Mode_Min_BarAlt Then Log_Mode_Min_BarAlt = Log_GPS_Calculated_Alt
                        If Log_GPS_Calculated_Alt > Log_Mode_Max_BarAlt Then Log_Mode_Max_BarAlt = Log_GPS_Calculated_Alt
                        Log_Mode_Sum_BarAlt = Log_Mode_Sum_BarAlt + Log_GPS_Calculated_Alt
                        Log_CTUN_DLs_for_Mode = Log_CTUN_DLs_for_Mode + 1

                        If Log_GPS_Calculated_Alt > Log_Maximum_Altitude Then
                            Log_Maximum_Altitude = Log_GPS_Calculated_Alt
                        End If
                    End If
                End If

                'Capture the Min / Max Speed Recorded
                If Log_GPS_Spd > Log_Flight_Max_Spd Then Log_Flight_Max_Spd = Log_GPS_Spd
                If Log_GPS_Spd < Log_Mode_Min_Spd Then Log_Mode_Min_Spd = Log_GPS_Spd
                If Log_GPS_Spd > Log_Mode_Max_Spd Then Log_Mode_Max_Spd = Log_GPS_Spd
                Log_Mode_Sum_Spd = Log_Mode_Sum_Spd + Log_GPS_Spd

                'Record the Min / Max HDop
                If Log_GPS_HDop < Log_Flight_Min_HDop Then Log_Flight_Min_HDop = Log_GPS_HDop
                If Log_GPS_HDop > Log_Flight_Max_HDop Then Log_Flight_Max_HDop = Log_GPS_HDop
                If Log_GPS_HDop < Log_Mode_Min_HDop Then Log_Mode_Min_HDop = Log_GPS_HDop
                If Log_GPS_HDop > Log_Mode_Max_HDop Then Log_Mode_Max_HDop = Log_GPS_HDop
                Log_Flight_Sum_HDop = Log_Flight_Sum_HDop + Log_GPS_HDop

                'Record the Number of Sats
                If Log_GPS_NSats < Log_Flight_Min_NSats Then Log_Flight_Min_NSats = Log_GPS_NSats
                If Log_GPS_NSats > Log_Flight_Max_NSats Then Log_Flight_Max_NSats = Log_GPS_NSats
                If Log_GPS_NSats < Log_Mode_Min_NSats Then Log_Mode_Min_NSats = Log_GPS_NSats
                If Log_GPS_NSats > Log_Mode_Max_NSats Then Log_Mode_Max_NSats = Log_GPS_NSats
                Log_Flight_Sum_NSats = Log_Flight_Sum_NSats + Log_GPS_NSats

                'Record the Distance from Launch Site in km.
                'It is possible for the UAV to take off before the first GPS reading during Auto Mode.
                'So only record the distances if the First_GPS_Lat has been updated.
                'it will have been by the time we get to the next GPS data so just miss this one.

                If First_GPS_Lat <> 0.0 Or First_GPS_Lng <> 0.0 Then
                    Dist_From_Launch = CalculateDistance(First_GPS_Lat, First_GPS_Lng, Log_GPS_Lat, Log_GPS_Lng)
                    If Dist_From_Launch > Max_Dist_From_Launch Then Max_Dist_From_Launch = Dist_From_Launch
                    If Dist_From_Launch < Mode_Min_Dist_From_Launch Then Mode_Min_Dist_From_Launch = Dist_From_Launch
                    If Dist_From_Launch > Mode_Max_Dist_From_Launch Then Mode_Max_Dist_From_Launch = Dist_From_Launch

                    'Record the Distance between the Last GPS Co-ordinate and the new one in km.
                    Temp_Dist_Travelled = CalculateDistance(Last_GPS_Lat, Last_GPS_Lng, Log_GPS_Lat, Log_GPS_Lng)
                    '## TODO
                    '## We may need to put some validate here, i.e. travelled distance too small = hovering, Large = GPS Glitch

                    Dist_Travelled = Dist_Travelled + Temp_Dist_Travelled

                    'Calculate the direction of travel based on the GPS Co-ordinate changes
                    'If Log_GPS_Spd > 0.1 Then
                    GPS_Calculated_Direction = GetCourseAndDistance(Last_GPS_Lat, Last_GPS_Lng, Log_GPS_Lat, Log_GPS_Lng)
                    'End If

                End If


                Log_Current_Flight_Time = DateDiff(DateInterval.Second, In_Flight_Start_Time, Log_GPS_DateTime)

                '### ERROR HERE ###
                'Needs an Mode_In_Flight_Start_Time
                Log_Current_Mode_Flight_Time = DateDiff(DateInterval.Second, Mode_In_Flight_Start_Time, Log_GPS_DateTime)

                'Debug.Print(vbNewLine)
                'Debug.Print("Log_Last_Mode_Changed_DateTime = " & Log_Last_Mode_Changed_DateTime)
                'Debug.Print("In_Flight_Start_Time = " & In_Flight_Start_Time)
                'Debug.Print("Log_GPS_DateTime = " & Log_GPS_DateTime)
                'Debug.Print("Log_Current_Flight_Time (secs) = " & Log_Current_Flight_Time)
                'Debug.Print("Log_Current_Mode_Flight_Time (secs) = " & Log_Current_Mode_Flight_Time)
                'Debug.Print("Log_Total_Flight_Time (secs) = " & Log_Total_Flight_Time)
                'Debug.Print(vbNewLine)

                ' Record the Mode Summary Data
                Log_GPS_DLs = Log_GPS_DLs + 1                       'Add a GPS record to the Total GPS record counter.
                Log_GPS_DLs_for_Mode = Log_GPS_DLs_for_Mode + 1     'Add a GPS record to the GPS record counter for the mode.
                Log_Mode_Sum_NSats = Log_Mode_Sum_NSats + Log_GPS_NSats
                Log_Mode_Sum_HDop = Log_Mode_Sum_HDop + Log_GPS_HDop
                Mode_Dist_Travelled = Mode_Dist_Travelled + Temp_Dist_Travelled

                'Debug.Print("Altitude: Baro " & Log_CTUN_BarAlt & " GPS Ground " & Log_Temp_Ground_GPS_Alt & " GPS Calc " & Log_GPS_Calculated_Alt)
            End If


            ' Update the GPS Chart
            frmMainForm.chartGPS.Series("Status").Points.AddY(Log_GPS_Status)
            If Log_GPS_HDop >= 5 Then ' Chart Control (Max Entry)
                frmMainForm.chartGPS.Series("HDop").Points.AddY(4)
            Else
                frmMainForm.chartGPS.Series("HDop").Points.AddY(Log_GPS_HDop)
            End If
            frmMainForm.chartGPS.Series("Satellites").Points.AddY(Log_GPS_NSats)
            frmMainForm.chartGPS.Series("Speed").Points.AddY(Log_GPS_Spd)

            ' Add the min / max lines
            frmMainForm.chartGPS.Series("StatusOKLine").Points.AddY(2.9)
            frmMainForm.chartGPS.Series("HDopMinLine").Points.AddY(2)
            frmMainForm.chartGPS.Series("SatellitesMinLine").Points.AddY(8)
            'frmMainForm.chartPowerRails.Series("SpeedAvgLine").Points.AddY(5.25)

            'Update the Travel Chart.
            frmMainForm.chartTravel.Series("Yaw").Points.AddY(Log_ATT_Yaw)
            frmMainForm.chartTravel.Series("GPS Calculated Direction").Points.AddY(GPS_Calculated_Direction)


            'Debug.Print(vbNewLine)
            'Debug.Print(vbNewLine)
            'Debug.Print("Debug GPS Data Variables:-")
            'Debug.Print("Data Line" & Str(DataLine))
            'Debug.Print("First_GPS_Lat = " & First_GPS_Lat)
            'Debug.Print("First_GPS_Lng = " & First_GPS_Lng)
            'Debug.Print("Log_GPS_Lat = " & Log_GPS_Lat)
            'Debug.Print("Log_GPS_Lng = " & Log_GPS_Lng)
            'Debug.Print("Last_GPS_Lat = " & Last_GPS_Lat)
            'Debug.Print("Last_GPS_Lng = " & Last_GPS_Lng)
            'Debug.Print("Temp_Dist_Travelled = " & Temp_Dist_Travelled & " KM  - " & Temp_Dist_Travelled * 0.621371 & " Miles")
            'Debug.Print("Dist_Travelled = " & Dist_Travelled & " KM  - " & Dist_Travelled * 0.621371 & " Miles")
            'Debug.Print("Dist_From_Launch = " & Dist_From_Launch & " KM  - " & Dist_From_Launch * 0.621371 & " Miles")
            'Debug.Print("Log_GPS_DateTime = " & Log_GPS_DateTime)
            'Debug.Print("NEED TO CAPTURE LOG FILENAME = 2014-03-15 15-14-26.log")
            'Debug.Print("Log_First_DateTime = " & Log_First_DateTime)
            'Debug.Print("In_Flight_Start_Time = " & In_Flight_Start_Time)
            'Debug.Print("Log_Current_Flight_Time (secs) = " & Log_Current_Flight_Time)
            'Debug.Print("Log_Current_Mode_Flight_Time (secs) = " & Log_Current_Mode_Flight_Time)
            'Debug.Print("Log_Total_Flight_Time (secs) = " & Log_Total_Flight_Time)
            'Debug.Print("Log_In_Flight = " & Log_In_Flight)
            'Debug.Print("First_In_Flight = " & First_In_Flight)
            'Debug.Print("Log_GPS_NSats = " & Log_GPS_NSats)
            'Debug.Print("Log_GPS_Min_NSats = " & Log_GPS_Min_NSats)
            'Debug.Print("Log_GPS_Max_NSats = " & Log_GPS_Max_NSats)
            'Debug.Print("Log_Flight_Min_NSats = " & Log_Flight_Min_NSats)
            'Debug.Print("Log_Flight_Max_NSats = " & Log_Flight_Max_NSats)
            'Debug.Print("Log_GPS_HDop = " & Log_GPS_HDop)
            'Debug.Print("Log_GPS_Min_HDop = " & Log_GPS_Min_HDop)
            'Debug.Print("Log_GPS_Max_HDop = " & Log_GPS_Max_HDop)
            'Debug.Print("Log_Flight_Min_HDop = " & Log_Flight_Min_HDop)
            'Debug.Print("Log_Flight_Max_HDop = " & Log_Flight_Max_HDop)
            'Debug.Print("Log_GPS_Spd = " & Log_GPS_Spd)
            'Debug.Print("Log_Flight_Max_Spd = " & Log_Flight_Max_Spd)
            'Debug.Print(vbNewLine)
            'Debug.Print(vbNewLine) '### Debug Code Here ###

            'Update the Last GPS Co-Ordinates so we can calculate the distance travelled next time round.
            'We do not care if we are in flight or not, however we only do the calculation when in flight.
            Last_GPS_Lat = Log_GPS_Lat
            Last_GPS_Lng = Log_GPS_Lng
        End If
    End Sub

End Module
