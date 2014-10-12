Module modPM_Checks
    Public Sub PM_Checks()
        If frmMainForm.chkboxPM.Checked = True Then
            If DataArray(0) = "PM" Then
                'An PM value should have only 9 pieces of numeric data!
                If ReadFileResilienceCheck(9) = True Then
                    Log_PM_RenCnt = Val(DataArray(1))
                    Log_PM_RenBlw = Val(DataArray(2))
                    Log_PM_NLon = Val(DataArray(3))
                    Log_PM_NLoop = Val(DataArray(4))
                    Log_PM_MaxT = Val(DataArray(5))
                    Log_PM_PMT = Val(DataArray(6))
                    Log_PM_I2CErr = Val(DataArray(7))
                    Log_PM_INSErr = Val(DataArray(8))
                    Log_PM_INAVErr = Val(DataArray(9))
                    'Debug.Print("Processing:- " & Str(DataLine) & ": " & Data)
                    PM_Delay_Counter = PM_Delay_Counter + 1

                    If Log_PM_RenCnt > 0 Then
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": DCM Error: DCM renormalization count is " & Log_PM_RenCnt)
                        Debug.Print("DCM Error: DCM renormalization count is " & Log_PM_RenCnt)
                    End If

                    If Log_PM_RenBlw > 0 Then
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": DCM Error: DCM renormalization blow-up count is " & Log_PM_RenBlw)
                        Debug.Print("DCM Error: DCM renormalization blow-up count is " & Log_PM_RenBlw)
                    End If

                    If Log_PM_NLon > 5 And PM_Delay_Counter > 3 Then
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: The number of long running main loops is " & Log_PM_NLon & " @ " & Log_PM_MaxT / 1000 & "ms")
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: A one off here could be ignored but if repeated in this log,")
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: then try disabling some logs, for example INAV, MOTORS and IMU.")
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Testing: Current Ground Speed is: " & Int(Log_GPS_Spd * 2.23693629) & " mph, this could be significant!")
                        Debug.Print("APM Speed Error: The number of sustained long running main loops is " & Log_PM_NLon & " @ " & Log_PM_MaxT & "ms")
                    End If

                    If Log_PM_NLoop > 1002 And PM_Delay_Counter > 3 Then
                        If ((Log_PM_NLoop - 1000) / 1000) * 100 < 5 Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: The total number of sustained loops " & Log_PM_NLoop)
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: A one off here could be ignored but if repeated in this log,")
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: then try disabling some logs, for example INAV, MOTORS and IMU.")
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Testing: Current Ground Speed is: " & Int(Log_GPS_Spd * 2.23693629) & " mph, this could be significant!")
                            Debug.Print("APM Speed Error: The total number of sustained loops " & Log_PM_NLoop)
                        Else
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: The total number of sustained loops " & Log_PM_NLoop)
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: This is above 5% of the total loops!")
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: Try disabling some logs, for example INAV, MOTORS and IMU.")
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: Otherwise seek further advice before flying!")
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Testing: Current Ground Speed is: " & Int(Log_GPS_Spd * 2.23693629) & " mph, this could be significant!")
                            Debug.Print("APM Speed Error: The total number of sustained loops is >= 5% @ " & Log_PM_NLoop)
                        End If
                    End If

                    If Log_PM_PMT = 10 And Log_GCS_Attached = False Then
                        Log_GCS_Attached = True
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Information: Ground Command Station is Attached.")
                        Debug.Print("GCS Info: Ground Command Station Initialised.")
                    End If

                    If Log_PM_PMT <> PM_Last_PMT And Log_PM_PMT < 9 And Log_GCS_Attached = True Then
                        If Log_PM_PMT = 0 Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: GCS Heartbeat signal has been lost!")
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: Current Distance from Launch is " & Format(Dist_From_Launch * 1000, "0.00") & "m ~ " & Format(Dist_From_Launch * 3280.8399, "0.00") & "ft")
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: Current Direction of UAV is " & Log_GPS_GCrs & " degrees (0 = north).")
                            Debug.Print("GCS Heartbeat Error: GCS Heartbeat signal has been lost!")
                        Else
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: GCS Heartbeat signal is low @ " & Log_PM_PMT & "0%")
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: Current Distance from Launch is " & Format(Dist_From_Launch * 1000, "0.00") & "m ~ " & Format(Dist_From_Launch * 3280.8399, "0.00") & "ft")
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: Current Direction of UAV is " & Log_GPS_GCrs & " degrees (0 = north).")
                            Debug.Print("GCS Heartbeat Error: GCS Heartbeat in inconsistent @ " & Log_PM_PMT)
                        End If
                        PM_Last_PMT = Log_PM_PMT
                    Else
                        If PM_Last_PMT < 9 And Log_PM_PMT >= 9 Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: GCS Heartbeat signal is low now OK @ 100%")
                            PM_Last_PMT = Log_PM_PMT
                        End If
                    End If

                    If Log_PM_I2CErr <> 0 Then
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": I2C Error: At least one I2C error has been detected.")
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": I2C Error: Current Value of PM Parameter PM_INSErr is " & Log_PM_I2CErr)
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": I2C Error: Please upload your log here: http://www.rcgroups.com/forums/showthread.php?t=2151318&pp=50")
                        Debug.Print("I2C Error: At least one I2C error has been detected.")
                    End If

                    If Log_PM_INSErr <> 0 Then
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: At least one initialisation error has been detected.")
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: Possible 3.3v Regulator issues!")
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: Current Value of PM Parameter PM_INSErr is " & Log_PM_INSErr)
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: Please upload your log here: http://www.rcgroups.com/forums/showthread.php?t=2151318&pp=50")
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: Please refer to this thread (page 11):")
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: http://diydrones.com/forum/topics/ac3-1-rc5-spi-speed-problem?id=705844%3ATopic%3A1457156&page=1#comments")
                        Debug.Print("INS Error: Current Value of PM Parameter Log_PM_INSErr is " & Log_PM_INSErr)
                    End If

                    'KXG 08/06/2014 - INAVerrCODE REMOVED
                    'The following code has been removed as it was found to give many results which can not be explained
                    'due to a lack of information about this value.
                    '
                    'If Log_PM_INAVErr <> 0 And Log_PM_INAVErr <> PM_Last_INAVErr Then
                    '    PM_Last_INAVErr = Log_PM_INAVErr
                    '    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INAV Error: A navigation error has been detected.")
                    '    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INAV Error: Current Value of PM Parameter PM_INAVErr is " & Log_PM_INAVErr)
                    '    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INAV Error: An update will be displayed if the value changes.")
                    '    Debug.Print("INAV Error: Current Value of PM Parameter Log_PM_INAVErr is " & Log_PM_INAVErr)
                    'Else
                    '    If Log_PM_INAVErr = 0 And PM_Last_INAVErr <> 0 Then
                    '        PM_Last_INAVErr = Log_PM_INAVErr
                    '        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INAV Error: Cleared")
                    '    End If
                    'End If
                End If
            End If
        End If

    End Sub
End Module
