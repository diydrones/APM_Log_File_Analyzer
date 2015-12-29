Module modCTUN_MainAnalysis
    Public Sub CTUN_MainAnalysis()

        ' This part only checks the already preset Variables for the data line.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE VARIABLES HAVE BEEN SET !!!

        'Capture Throttle up based on CTUN.
        If Log_CTUN_ThrOut > 0 And CTUN_ThrottleUp = False Then
            CTUN_ThrottleUp = True
        End If

        'Capture Throttle % based on CTUN.
        If Log_CTUN_ThrOut > 400 Then
            CTUN_ThrOut_40 = True
        Else
            CTUN_ThrOut_40 = False
        End If

        'Capture the Maximum Altitude detected.
        If Log_CTUN_BarAlt > Log_Maximum_Altitude Then Log_Maximum_Altitude = Log_CTUN_BarAlt

        'Collect the data for this Flight Mode Summary.
        If Log_In_Flight = True Then
            Log_CTUN_DLs = Log_CTUN_DLs + 1                       'Add a CTUN record to the Total GPS record counter.
            Log_CTUN_DLs_for_Mode = Log_CTUN_DLs_for_Mode + 1     'Add a CTUN record to the GPS record counter for the mode.
            If Log_CTUN_BarAlt < Log_Mode_Min_BarAlt Then Log_Mode_Min_BarAlt = Log_CTUN_BarAlt
            If Log_CTUN_BarAlt > Log_Mode_Max_BarAlt Then Log_Mode_Max_BarAlt = Log_CTUN_BarAlt
            Log_Mode_Sum_BarAlt = Log_Mode_Sum_BarAlt + Log_CTUN_BarAlt
        End If

        'Debug.Print(vbNewLine)
        'Debug.Print(vbNewLine)
        'Debug.Print("Debug CTUN Data Variables:-")
        'Debug.Print("Data Line" & Str(DataLine))
        'Debug.Print("CTUN_ThrottleUp = " & CTUN_ThrottleUp)
        'Debug.Print("Log_Ground_BarAlt = " & Log_Ground_BarAlt)
        'Debug.Print("Log_Armed_BarAlt = " & Log_Armed_BarAlt)
        'Debug.Print("Log_CTUN_BarAlt = " & Log_CTUN_BarAlt)
        'Debug.Print("Log_Maximum_Altitude = " & Log_Maximum_Altitude)
        'Debug.Print("Log_Disarmed_BarAlt = " & Log_Disarmed_BarAlt)
        'Debug.Print("Log_In_Flight = " & Log_In_Flight)
        'Debug.Print("First_In_Flight = " & First_In_Flight)
        'Debug.Print("First_GPS_Lat = " & First_GPS_Lat)
        'Debug.Print("First_GPS_Lng = " & First_GPS_Lng)
        'Debug.Print(vbNewLine)
        'Debug.Print(vbNewLine) '### Debug Code Here ###

    End Sub
End Module
