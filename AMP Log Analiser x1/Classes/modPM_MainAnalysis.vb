Module modPM_MainAnalysis
    Public Sub PM_MainAnalysis()

        ' This part only checks the already preset Variables for the data line.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE VARIABLES HAVE BEEN SET !!!

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
            'Debug.Print("APM Speed Error: The number of sustained long running main loops is " & Log_PM_NLon & " @ " & Log_PM_MaxT & "ms")
        End If

        If Log_PM_NLoop > 1002 And PM_Delay_Counter > 3 Then
            If ((Log_PM_NLoop - 1000) / 1000) * 100 < 5 Then
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: The total number of sustained loops " & Log_PM_NLoop)
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: A one off here could be ignored but if repeated in this log,")
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: then try disabling some logs, for example INAV, MOTORS and IMU.")
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Testing: Current Ground Speed is: " & Int(Log_GPS_Spd * 2.23693629) & " mph, this could be significant!")
                'Debug.Print("APM Speed Error: The total number of sustained loops " & Log_PM_NLoop)
            Else
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: The total number of sustained loops " & Log_PM_NLoop)
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: This is above 5% of the total loops!")
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: Try disabling some logs, for example INAV, MOTORS and IMU.")
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: Otherwise seek further advice before flying!")
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Testing: Current Ground Speed is: " & Int(Log_GPS_Spd * 2.23693629) & " mph, this could be significant!")
                'Debug.Print("APM Speed Error: The total number of sustained loops is >= 5% @ " & Log_PM_NLoop)
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
            Debug.Print("I2C Error: At least one I2C error has been detected.")
        End If

        If Log_PM_INSErr <> 0 Then
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: At least one unknown initialisation error has been detected.")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: Current Value of PM Parameter PM_INSErr is " & Log_PM_INSErr)
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: This can sometimes be writen at the end of the log if the APM")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: was disconnected from power before being disabled / disconnected.")
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

    End Sub
End Module
