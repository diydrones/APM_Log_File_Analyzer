Module modERROR_Checks
    Public Sub ERROR_Checks()
        'An Error value should have only 2 pieces or 3 Pieces of data!
        If DataArray(0) = "ERR" Then
            If IsNumeric(DataArray(1)) = False Or IsNothing(DataArray(3)) = False And (IsNumeric(DataArray(2) = False Or IsNothing(DataArray(2)) = False)) Then
                Debug.Print("================================================================")
                Debug.Print("== File Corruption Detected on Data Line " & DataLine & ", ERROR Checks ignored! ==")
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


                strErrorCode = DataArray(1)
                strECode = DataArray(2)
                If strErrorCode = "1" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": MAIN ERROR: Critical Error, contact firmware developers and DO NOT FLY!")
                End If
                If strErrorCode = "2" And Val(strECode) > 2 Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": RADIO ERROR: New Error Code (ECode Unknown to APM Log Analyser")
                End If
                If strErrorCode = "2" And strECode = "1" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": RADIO ERROR: The radio failed to initialise (likely a hardware / signal issue).")
                End If
                If strErrorCode = "2" And strECode = "2" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": RADIO ERROR: Late Frame Detected, i.e. Longer than 2 Seconds.")
                    Debug.Print("Radio Error: Start")
                End If
                If strErrorCode = "2" And strECode = "0" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": RADIO ERROR: Previous Radio Error Resolved.")
                    Debug.Print("Radio Error: End")
                End If
                If strErrorCode = "3" And Val(strECode) > 2 Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": COMPASS ERROR: New Error Code (strECode Unknown to Macro).")
                End If
                If strErrorCode = "3" And strECode = "2" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": COMPASS ERROR: Failure while trying to read a single value from the compass (probably a hardware issue).")
                End If
                If strErrorCode = "3" And strECode = "1" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": COMPASS ERROR: The compass failed to initialise (likely a hardware issue).")
                End If
                If strErrorCode = "3" And strECode = "0" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": COMPASS ERROR: Previous Compass Error Resolved.")
                End If
                If strErrorCode = "4" And Val(strECode) > 1 Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": OPTICAL FLOW ERROR: New Error Code (strECode Unknown to Macro).")
                End If
                If strErrorCode = "4" And strECode = "1" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": OPTICAL FLOW ERROR: Failed to initialise (likely a hardware issue).")
                End If
                If strErrorCode = "4" And strECode = "0" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": OPTICAL FLOW ERROR: Undocumented but assume error resolved.")
                End If
                If strErrorCode = "5" And Val(strECode) > 1 Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: New Error Code (strECode Unknown to Macro).")
                End If
                If strErrorCode = "5" And strECode = "1" Then
                    Debug.Print("Throttle Error: Start")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: Throttle dropped below FS_THR_VALUE meaning likely loss of contact between RX/TX.")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: Current Distance from Launch is " & Format(Dist_From_Launch * 1000, "0.00") & "m ~ " & Format(Dist_From_Launch * 3280.8399, "0.00") & "ft")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: Current Direction of UAV is " & Log_GPS_GCrs & " degrees (0 = north).")
                End If
                If strErrorCode = "5" And strECode = "0" Then
                    Debug.Print("Throttle Error: End")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: Throttle error resolve meaning likely RX/TX contact restored.")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: Current Distance from Launch is " & Format(Dist_From_Launch * 1000, "0.00") & "m ~ " & Format(Dist_From_Launch * 3280.8399, "0.00") & "ft")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: Current Direction of UAV is " & Log_GPS_GCrs & " degrees (0 = north).")
                End If
                If strErrorCode = "6" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ERROR: Battery Fail Safe Occurred.")
                End If
                If strErrorCode = "7" And Val(strECode) > 1 Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: New Error Code (strECode Unknown to Macro).")
                    Log_GPS_Glitch = True
                End If
                If strErrorCode = "7" And strECode = "1" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: GPS lock lost for at least 5 seconds.")
                    Log_GPS_Glitch = True
                End If
                If strErrorCode = "7" And strECode = "0" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: GPS lock restored.")
                    Log_GPS_Glitch = False
                End If
                If strErrorCode = "8" And Val(strECode) > 1 Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS ERROR: New Error Code (strECode Unknown to Macro).")
                End If
                If strErrorCode = "8" And strECode = "1" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS ERROR: Updates from ground station joystick lost for at least 5 seconds.")
                End If
                If strErrorCode = "8" And strECode = "0" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS ERROR: Updates from ground station restored.")
                End If
                If strErrorCode = "9" And Val(strECode) > 3 Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE ERROR: New Error Code (strECode Unknown to Macro).")
                End If
                If strErrorCode = "9" And strECode = "3" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE ERROR: Both altitude and circular fences breached.")
                End If
                If strErrorCode = "9" And strECode = "2" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE ERROR: Circular fence breached.")
                End If
                If strErrorCode = "9" And strECode = "1" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE ERROR: Altitude fence breached.")
                End If
                If strErrorCode = "9" And strECode = "0" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE ERROR: Vehicle is back within the fences.")
                End If
                If strErrorCode = "10" And Val(strECode) > 10 Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: New Error Code (strECode Unknown to Macro).")
                End If
                If strErrorCode = "10" And strECode = "0" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Stabilize mode.")
                End If
                If strErrorCode = "10" And strECode = "1" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Acro mode.")
                End If
                If strErrorCode = "10" And strECode = "2" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter AltHold mode.")
                End If
                If strErrorCode = "10" And strECode = "3" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Auto mode.")
                End If
                If strErrorCode = "10" And strECode = "4" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Guided mode.")
                End If
                If strErrorCode = "10" And strECode = "5" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Loiter mode.")
                End If
                If strErrorCode = "10" And strECode = "6" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter RTL mode.")
                End If
                If strErrorCode = "10" And strECode = "7" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Circle mode.")
                End If
                If strErrorCode = "10" And strECode = "8" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Position mode.")
                End If
                If strErrorCode = "10" And strECode = "9" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Land mode.")
                End If
                If strErrorCode = "10" And strECode = "10" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter OF_Loiter mode.")
                End If
                If strErrorCode = "11" And strECode = "2" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: GPS Glitch.")
                    Log_GPS_Glitch = True
                End If
                If strErrorCode = "11" And strECode = "1" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: Undocumented GPS Error.")
                    Log_GPS_Glitch = True
                End If
                If strErrorCode = "11" And strECode = "0" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: GPS Glitch Cleared.")
                    Log_GPS_Glitch = False
                End If
                If strErrorCode = "12" And strECode = "1" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": CRASH ERROR: Crash Detected.")
                End If
                If strErrorCode = "12" And strECode = "0" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": CRASH ERROR: Undocumented Crash Error.")
                End If
                If strErrorCode = "13" And strECode = "0" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIP ERROR: Undocumented Flip Error.")
                End If
                If strErrorCode = "13" And strECode = "1" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIP ERROR: Undocumented Flip Error.")
                End If
                If strErrorCode = "13" And strECode = "2" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIP ERROR: Flip abandoned (because of 2 second timeout).")
                End If

                If strErrorCode = "14" And strECode = "0" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE ERROR: Undocumented AUTOTUNE Error.")
                End If
                If strErrorCode = "14" And strECode = "1" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE ERROR: Undocumented AUTOTUNE Error.")
                End If
                If strErrorCode = "14" And strECode = "2" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE ERROR: Bad Gains (failed to determine proper gains).")
                End If

                If strErrorCode = "15" And strECode = "0" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": PARACHUTE ERROR: Undocumented PARACHUTE Error.")
                End If
                If strErrorCode = "15" And strECode = "1" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": PARACHUTE ERROR: Undocumented PARACHUTE Error.")
                End If
                If strErrorCode = "15" And strECode = "2" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": PARACHUTE ERROR: @ " & Log_CTUN_BarAlt & "m you were too low to deploy parachute.")
                End If

                If strErrorCode = "16" And strECode = "0" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ":  EKF/ Inertial Nav Check ERROR: Bad Variance cleared.")
                End If
                If strErrorCode = "16" And strECode = "1" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ":  EKF/ Inertial Nav Check ERROR: Undocumented EKF/ Inertial Nav Check Error.")
                End If
                If strErrorCode = "16" And strECode = "2" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ":  EKF/ Inertial Nav Check ERROR: Bad Variance.")
                End If

                If strErrorCode = "17" And strECode = "0" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": EKF/InertialNav Failsafe ERROR: Undocumented EKF/InertialNav Failsafe Error.")
                End If
                If strErrorCode = "17" And strECode = "1" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": EKF/InertialNav Failsafe ERROR: Undocumented EKF/InertialNav Failsafe Error.")
                End If
                If strErrorCode = "17" And strECode = "2" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": EKF/InertialNav Failsafe ERROR: EKF Failsafe triggered.")
                End If

                If strErrorCode = "18" And strECode = "0" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ":  BARO GLITCH ERROR: Baro glitch cleared.")
                End If
                If strErrorCode = "18" And strECode = "1" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ":  BARO GLITCH ERROR: Undocumented BARO GLITCH Error.")
                End If
                If strErrorCode = "18" And strECode = "2" Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ":  BARO GLITCH ERROR: Baro glitch.")
                End If

            End If
        End If
    End Sub
End Module
