Module modERROR_Checks_v3_3
    Public Sub ERROR_Checks_v3_3()
        'An Error value should have only 3 Pieces of data!
        If DataArray(0) = "ERR" Then
            If IsNumeric(DataArray(1)) = False Or IsNothing(DataArray(3)) = False And (IsNumeric(DataArray(2) = False Or IsNothing(DataArray(2)) = False)) Then
                Debug.Print("================================================================")
                Debug.Print("== File Corruption Detected on Data Line " & DataLine & ", line ignored! ==")
                Debug.Print("================================================================")
                ErrorCount = ErrorCount + 1
                With frmMainForm
                    .lblErrors.Visible = True
                    .lblErrors.Refresh()
                    .lblErrorCountNo.Visible = True
                    .lblErrorCount.Visible = True
                    .lblErrorCountNo.Text = ErrorCount
                    .lblErrorCount.Refresh()
                    .lblErrorCountNo.Refresh()
                End With
            Else

                'Debug.Print("")
                'Debug.Print("----------------------------------------------------------------------------------------")
                'Debug.Print("Error Detected:-")


                strErrorCode = DataArray(2)
                strECode = DataArray(3)
                Select Case strErrorCode
                    Case "1"
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": MAIN ERROR: Critical Error, contact firmware developers and DO NOT FLY!")
                    Case "2"
                        Select Case strECode
                            Case "0"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": RADIO ERROR: Previous Radio Error Resolved.")
                                Debug.Print("Radio Error: End")
                            Case "1"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": RADIO ERROR: The radio failed to initialise (likely a hardware / signal issue).")
                            Case "2"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": RADIO ERROR: Late Frame Detected, i.e. Longer than 2 Seconds.")
                                Debug.Print("Radio Error: Start")
                            Case Else
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": RADIO ERROR: New Error Code (ECode Unknown to APM Log Analyser")
                        End Select
                    Case "3"
                        Select Case strECode
                            Case "0"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": COMPASS ERROR: Previous Compass Error Resolved.")
                            Case "1"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": COMPASS ERROR: The compass failed to initialise (likely a hardware issue).")
                            Case "2"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": COMPASS ERROR: Failure while trying to read a single value from the compass (probably a hardware issue).")
                            Case Else
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": COMPASS ERROR: New Error Code (strECode Unknown to Macro).")
                        End Select
                    Case "4"
                        Select Case strECode
                            Case "0"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": OPTICAL FLOW ERROR: Undocumented but assume error resolved.")
                            Case "1"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": OPTICAL FLOW ERROR: Failed to initialise (likely a hardware issue).")
                            Case Else
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": OPTICAL FLOW ERROR: New Error Code (strECode Unknown to Macro).")
                        End Select
                    Case "5"
                        Select Case strECode
                            Case "0"
                                Debug.Print("Throttle Error: End")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: Throttle error resolve meaning likely RX/TX contact restored.")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: Current Distance from Launch is " & Format(Dist_From_Launch * 1000, "0.00") & "m ~ " & Format(Dist_From_Launch * 3280.8399, "0.00") & "ft")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: Current Direction of UAV is " & Log_GPS_GCrs & " degrees (0 = north).")
                            Case "1"
                                Debug.Print("Throttle Error: Start")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: Throttle dropped below FS_THR_VALUE meaning likely loss of contact between RX/TX.")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: Current Distance from Launch is " & Format(Dist_From_Launch * 1000, "0.00") & "m ~ " & Format(Dist_From_Launch * 3280.8399, "0.00") & "ft")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: Current Direction of UAV is " & Log_GPS_GCrs & " degrees (0 = north).")
                            Case Else
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: New Error Code (strECode Unknown to Macro).")
                        End Select
                    Case "6"
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ERROR: Battery Fail Safe Occurred.")
                    Case "7"
                        Select Case strECode
                            Case "0"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: GPS lock restored.")
                                Log_GPS_Glitch = False
                            Case "1"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: GPS lock lost for at least 5 seconds.")
                                Log_GPS_Glitch = True
                            Case Else
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: New Error Code (strECode Unknown).")
                                Log_GPS_Glitch = True
                        End Select
                    Case "8"
                        Select Case strECode
                            Case "0"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS ERROR: Updates from ground station restored.")
                            Case "1"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS ERROR: Updates from ground station joystick lost for at least 5 seconds.")
                            Case Else
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS ERROR: New Error Code (strECode Unknown).")
                        End Select
                    Case "9"
                        Select Case strECode
                            Case "0"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE ERROR: Vehicle is back within the fences.")
                            Case "1"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE ERROR: Altitude fence breached.")
                            Case "2"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE ERROR: Circular fence breached.")
                            Case "3"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE ERROR: Both altitude and circular fences breached.")
                            Case Else
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE ERROR: New Error Code (strECode Unknown to Macro).")
                        End Select
                    Case "10"
                        Select Case strECode
                            Case "0"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Stabilize mode.")
                            Case "1"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Acro mode.")
                            Case "2"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter AltHold mode.")
                            Case "3"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Auto mode.")
                            Case "4"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Guided mode.")
                            Case "5"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Loiter mode.")
                            Case "6"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter RTL mode.")
                            Case "7"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Circle mode.")
                            Case "8"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Position mode.")
                            Case "9"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Land mode.")
                            Case "10"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter OF_Loiter mode.")
                            Case Else
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: New Error Code (strECode Unknown).")
                        End Select
                    Case "11"
                        Select Case strECode
                            Case "0"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: GPS Glitch Cleared.")
                                Log_GPS_Glitch = False
                            Case "1"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: Undocumented GPS Error.")
                                Log_GPS_Glitch = True
                            Case "2"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: GPS Glitch.")
                                Log_GPS_Glitch = True
                            Case Else
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: New Error Code (strECode Unknown).")
                        End Select
                    Case "12"
                        Select Case strECode
                            Case "0"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": CRASH ERROR: Undocumented Crash Error.")
                            Case "1"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": CRASH ERROR: Crash Detected.")
                            Case Else
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": CRASH ERROR: New Error Code (strECode Unknown).")
                        End Select
                    Case "13"
                        Select Case strECode
                            Case "0"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIP ERROR: Undocumented Flip Error.")
                            Case "1"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIP ERROR: Undocumented Flip Error.")
                            Case "2"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIP ERROR: Flip abandoned (because of 2 second timeout).")
                            Case Else
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIP ERROR: New Error Code (strECode Unknown).")
                        End Select
                    Case "14"
                        Select Case strECode
                            Case "0"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE ERROR: Undocumented AUTOTUNE Error.")
                            Case "1"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE ERROR: Undocumented AUTOTUNE Error.")
                            Case "2"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE ERROR: Bad Gains (failed to determine proper gains).")
                            Case Else
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE ERROR: New Error Code (strECode Unknown).")
                        End Select
                    Case "15"
                        Select Case strECode
                            Case "0"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": PARACHUTE ERROR: Undocumented PARACHUTE Error.")
                            Case "1"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": PARACHUTE ERROR: Undocumented PARACHUTE Error.")
                            Case "2"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": PARACHUTE ERROR: @ " & Log_CTUN_BarAlt & "m you were too low to deploy parachute.")
                            Case Else
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": PARACHUTE ERROR: New Error Code (strECode Unknown).")
                        End Select
                    Case "16"
                        Select Case strECode
                            Case "0"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ":  EKF/ Inertial Nav Check ERROR: Bad Variance cleared.")
                            Case "1"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ":  EKF/ Inertial Nav Check ERROR: Undocumented EKF/ Inertial Nav Check Error.")
                            Case "2"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ":  EKF/ Inertial Nav Check ERROR: Bad Variance.")
                            Case Else
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ":  EKF/ Inertial Nav Check ERROR: New Error Code (strECode Unknown).")
                        End Select
                    Case "17"
                        Select Case strECode
                            Case "0"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": EKF/InertialNav Failsafe ERROR: Undocumented EKF/InertialNav Failsafe Error.")
                            Case "1"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": EKF/InertialNav Failsafe ERROR: Undocumented EKF/InertialNav Failsafe Error.")
                            Case "2"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": EKF/InertialNav Failsafe ERROR: EKF Failsafe triggered.")
                            Case Else
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": EKF/InertialNav Failsafe ERROR: New Error Code (strECode Unknown).")
                        End Select
                    Case "18"
                        Select Case strECode
                            Case "0"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ":  BARO GLITCH ERROR: Baro glitch cleared.")
                            Case "1"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ":  BARO GLITCH ERROR: Undocumented BARO GLITCH Error.")
                            Case "2"
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ":  BARO GLITCH ERROR: Baro glitch.")
                            Case Else
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ":  BARO GLITCH ERROR: New Error Code (strECode Unknown).")
                        End Select
                    Case "19"
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ":          CPU ERROR: Critical Error.")
                    Case "20"
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ":      FAILSAFE_ADSB: Live Aircraft Error.")
                    Case Else
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ERROR: New Error Code found, code = " & strErrorCode)
                End Select
            End If
        End If
    End Sub
End Module
