Module modPM_MainAnalysis
    Public Sub PM_MainAnalysis()

        ' This part only checks the already preset Variables for the data line.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE VARIABLES HAVE BEEN SET !!!

        Dim PercentOfLongLoops As Single = Log_PM_NLon / Log_PM_NLoop * 100
        Dim PercentOfMaxT As Single = (((Log_PM_MaxT * APM_Frequency) - Log_PM_NLoop) / Log_PM_NLoop) / 100

        Const AllowedOffset As Integer = 20     ' The maximum increase allowed to be added to the average, otherwise value will be ignored.
        Const HighValueWarning As Integer = 2   ' The number of times high values can occure together before a warning is issued.
        Const IgnoreFirstValues As Integer = 0  ' The number of values that will be ignored from the start of the log, they can be very high
        Const PrimeLast As Integer = 10         ' Number to be used as the reference point, ensures the first values are not set too high.

        Debug.Print("PM_NLon = " & Log_PM_NLon &
            ", PM_NLoop = " & Log_PM_NLoop &
            ", PM_MaxT = " & Log_PM_MaxT &
            ", % Long = " & Format(PercentOfLongLoops, "0.00") &
            ", Longest = " & Format(PercentOfMaxT, "0.00") & "ms")

        ' Prime the "last" values, ensures we do not set the initial value higher than PrimeLast + AllowedOffSett
        If Log_PM_Perf_LastMaxT = 0 Then
            Log_PM_Perf_LastMaxT = PrimeLast
            Log_PM_Perf_LastNLon = PrimeLast
        End If

        ' Calculate the average Nlon as a % and MaxT as millisecond while suppressing one off stupidly high values
        If Log_PM_Stability_Counter > IgnoreFirstValues Then 'Suppress the first values
            If PercentOfLongLoops < Log_PM_Perf_LastNLon + AllowedOffset Then
                Log_PM_Perf_AvgNLon = Log_PM_Perf_AvgNLon + PercentOfLongLoops
                Log_PM_HighValue_Counter = 0
            Else
                Log_PM_HighValue_Counter += 1
            End If
            If PercentOfMaxT < Log_PM_Perf_LastMaxT + AllowedOffset Then
                Log_PM_Perf_AvgMaxT = Log_PM_Perf_AvgMaxT + PercentOfMaxT
                Log_PM_HighValue_Counter = 0
            Else
                Log_PM_HighValue_Counter += 1
            End If
            Log_PM_Counter += 1
        Else
            Log_PM_Stability_Counter += 1
        End If
        ' Issue a warning if we start detecting too many high MaxT values in a row.
        If Log_PM_HighValue_Counter > HighValueWarning Then
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FC Performance Warning: Mulitple high MaxT Values are being detected, review performance logs.")
        End If

        ' Remember the last values so we can detect if the next value is significantly too high and should be ignored.
        Log_PM_Perf_LastNLon = PercentOfLongLoops
        Log_PM_Perf_LastMaxT = PercentOfMaxT

        ' Issue a warning if the number of loops is not in track with the hardware frequency.
        If Int(Log_PM_NLoop / 10) <> Int(APM_Frequency) Then
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FC Performance Warning: The number of main loops is out of sync with the clock frequency @ " & Log_PM_NLoop & " loops.")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FC Performance Warning: A one off here could be ignored but if repeated in this log,")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FC Performance Warning: then try disabling some logs, for example INAV, MOTORS and IMU.")
        End If

        Dim SeverityLevel As String = "Not Set - Program Error!!!"
        If PercentOfLongLoops >= 3 And frmMainForm.chkboxPM.Checked = True Then SeverityLevel = "Information"
        If PercentOfLongLoops > 10 Then SeverityLevel = "Warning"
        If PercentOfLongLoops > 15 Then SeverityLevel = "Error"
        If SeverityLevel = "Warning" Or SeverityLevel = "Error" Or SeverityLevel = "Information" Then
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FC Performance " & SeverityLevel & ": The number of long running loops is " & PercentOfLongLoops & "%")
        End If
        If SeverityLevel = "Error" Then
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FC Performance " & SeverityLevel & ": This level is classed by the developers as serious and should be investigated further.")
        ElseIf SeverityLevel = "Warning" Or SeverityLevel = "Information" Then
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FC Performance " & SeverityLevel & ": This level is not classed as serious and one off reports can be ignored.")
        End If



        If PercentOfMaxT > 20 And PM_Delay_Counter > 3 Then
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FC Performance Warning: The longest loop is running for an extended period @ " & PercentOfMaxT & " ms.")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FC Performance Warning: A one off here could be ignored but if repeated in this log,")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FC Performance Warning: then try disabling some logs, for example INAV, MOTORS and IMU.")
        End If

        ' Old code replaced by the above, delete once the new code is validated
        'If Log_PM_NLon > 70 And PM_Delay_Counter > 3 Then
        '    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: The number of long running main loops is " & Log_PM_NLon & " @ " & Log_PM_MaxT / 1000 & "ms")
        '    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: A one off here could be ignored but if repeated in this log,")
        '    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: then try disabling some logs, for example INAV, MOTORS and IMU.")
        'End If

        'If Log_PM_NLoop > 1002 And PM_Delay_Counter > 3 Then
        '    If ((Log_PM_NLoop - 1000) / 1000) * 100 < 5 Then
        '        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: The total number of sustained loops " & Log_PM_NLoop)
        '        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: A one off here could be ignored but if repeated in this log,")
        '        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: then try disabling some logs, for example INAV, MOTORS and IMU.")
        '    Else
        '        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: The total number of sustained loops " & Log_PM_NLoop)
        '        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: This is above 5% of the total loops!")
        '        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: Try disabling some logs, for example INAV, MOTORS and IMU.")
        '        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: Otherwise seek further advice before flying!")
        '    End If
        'End If


        ' GCS monitoring
        If Log_PM_PMT = 10 And Log_GCS_Attached = False Then
            Log_GCS_Attached = True
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Information: Ground Command Station is Attached.")
            Debug.Print("GCS Info: Ground Command Station Initialised.")
        End If

        ' GCS monitoring when GCS failsafe is Activated
        If Log_PM_PMT <> PM_Last_PMT And Log_PM_PMT < 9 And Log_GCS_Attached = True And PARM_FS_GCS_ENABLE > 0 Then
            If Log_PM_PMT = 0 Then
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: GCS Heartbeat signal has been lost!")
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: Current Distance from Launch is " & Format(Dist_From_Launch * 1000, "0.00") & "m ~ " & Format(Dist_From_Launch * 3280.8399, "0.00") & "ft")
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: Current Direction of UAV is " & Log_GPS_GCrs & " degrees (0 = north).")
                Debug.Print("GCS Heartbeat Error: GCS Heartbeat signal has been lost!")
            ElseIf Log_PM_PMT >= 1 And Log_PM_PMT <= 4 Then
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: GCS Heartbeat signal is low @ " & Log_PM_PMT & "0%")
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: Current Distance from Launch is " & Format(Dist_From_Launch * 1000, "0.00") & "m ~ " & Format(Dist_From_Launch * 3280.8399, "0.00") & "ft")
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: Current Direction of UAV is " & Log_GPS_GCrs & " degrees (0 = north).")
                Debug.Print("GCS Heartbeat Error: GCS Heartbeat in inconsistent @ " & Log_PM_PMT)
            Else
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Warning: GCS Heartbeat signal is low @ " & Log_PM_PMT & "0%")
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Warning: Current Distance from Launch is " & Format(Dist_From_Launch * 1000, "0.00") & "m ~ " & Format(Dist_From_Launch * 3280.8399, "0.00") & "ft")
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Warning: Current Direction of UAV is " & Log_GPS_GCrs & " degrees (0 = north).")
                Debug.Print("GCS Heartbeat Error: GCS Heartbeat in inconsistent @ " & Log_PM_PMT)
            End If
            PM_Last_PMT = Log_PM_PMT
        Else
            If PM_Last_PMT < 9 And Log_PM_PMT >= 9 Then
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: GCS Heartbeat signal is now OK @ 100%")
                PM_Last_PMT = Log_PM_PMT
            End If
        End If

        ' GCS monitoring when GCS failsafe is NOT Activated
        If Log_PM_PMT <> PM_Last_PMT And Log_PM_PMT < 9 And Log_GCS_Attached = True And PARM_FS_GCS_ENABLE > 0 Then
            If Log_PM_PMT = 0 Then
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: GCS Heartbeat signal has been lost!")
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: Current Distance from Launch is " & Format(Dist_From_Launch * 1000, "0.00") & "m ~ " & Format(Dist_From_Launch * 3280.8399, "0.00") & "ft")
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: Current Direction of UAV is " & Log_GPS_GCrs & " degrees (0 = north).")
                Debug.Print("GCS Heartbeat Error: GCS Heartbeat signal has been lost!")
            End If
            PM_Last_PMT = Log_PM_PMT
        Else
            If PM_Last_PMT < 9 And Log_PM_PMT >= 9 Then
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: GCS Heartbeat signal is now OK @ 100%")
                PM_Last_PMT = Log_PM_PMT
            End If
        End If


        If Log_PM_I2CErr <> 0 Then
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": I2C Error: At least one I2C error has been detected.")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": I2C Error: Current Value of PM Parameter PM_INSErr is " & Log_PM_I2CErr)
            Debug.Print("I2C Error: At least one I2C error has been detected.")
        End If

        If Log_PM_INSErr <> 0 Then
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: At least one unknown initialisation error has been detected.")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: Current Value of PM Parameter PM_INSErr is " & Log_PM_INSErr)
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: This can sometimes be writen at the end of the log if the APM")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: was disconnected from power before being disabled / disconnected.")
            Debug.Print("INS Error: Current Value of PM Parameter Log_PM_INSErr is " & Log_PM_INSErr)
        End If


        ' Code to support older versions

        ' RenCnt and RenBlw are only avaliable for v3.1, higher firmwares set the variables to 0
        If Log_PM_RenCnt > 0 Then
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": DCM Error: DCM renormalization count is " & Log_PM_RenCnt)
            Debug.Print("DCM Error: DCM renormalization count is " & Log_PM_RenCnt)
        End If
        If Log_PM_RenBlw > 0 Then
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": DCM Error: DCM renormalization blow-up count is " & Log_PM_RenBlw)
            Debug.Print("DCM Error: DCM renormalization blow-up count is " & Log_PM_RenBlw)
        End If

    End Sub
End Module
