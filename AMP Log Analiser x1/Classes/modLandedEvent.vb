Module modLandedEvent

    Public Sub LandedEvent()
        If CTUN_Logging = True Then Log_Ground_BarAlt = Log_CTUN_BarAlt Else Log_Ground_BarAlt = Log_GPS_Alt - Log_Temp_Ground_GPS_Alt
        'Debug.Print(vbNewLine)
        'Debug.Print("Log_Current_Flight_Time: " & Log_Current_Flight_Time)
        Log_In_Flight = False
        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Landed")
        If frmMainForm.chkboxSplitModeLandings.Checked = True Then Call AddModeTime()
        'Debug.Print("Log_Current_Flight_Time (Reset): 0")
        'Debug.Print("Aircraft not in flight!")
        Log_Total_Flight_Time = Log_Total_Flight_Time + Log_Current_Flight_Time
        Log_Current_Flight_Time = 0
        Log_Current_Mode_Flight_Time = 0
        PM_Delay_Counter = 0
        Log_In_Flight_Change_Status_Counter = 0
    End Sub

End Module
