Module modTakeOffEvent
    Public Sub TakeOffEvent()
        'Debug.Print("Execute the TAKEOFF Code")
        Log_In_Flight = True
        If CTUN_Logging = True Then Log_Armed_BarAlt = Log_CTUN_BarAlt Else Log_Armed_BarAlt = Log_GPS_Alt - Log_Temp_Ground_GPS_Alt
        'Capture the GPS time as the Quad takes off.
        In_Flight_Start_Time = Log_GPS_DateTime
        If frmMainForm.chkboxSplitModeLandings.Checked = True Then
            Mode_In_Flight_Start_Time = Log_GPS_DateTime
            Log_Mode_Start_Current = Log_Total_Current
        End If
        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Take Off")
        'Capture the First GPS Lat & Lng if this is the first take off.
        If First_In_Flight = False Then
            First_GPS_Lat = Log_GPS_Lat
            First_GPS_Lng = Log_GPS_Lng
            First_In_Flight = True
        End If
        Log_Armed = True
        Log_Temp_Ground_GPS_Alt = Log_GPS_Alt
    End Sub

End Module
