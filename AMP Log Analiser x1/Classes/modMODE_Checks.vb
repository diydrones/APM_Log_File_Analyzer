Module modMODE_Checks

    Public Sub Mode_Checks()
        If DataArray(0) = "MODE" Then
            'The Mode has just changed, we need to save all the details about the previous mode before we
            'can make the changes to the program variables.
            'Debug.Print("")
            'Debug.Print("----------------------------------------------------------------------------------------")
            'Debug.Print("MODE Change Detected, Call AddModeTime() first!")
            'Debug.Print("Current Mode is: " & Log_Current_Mode)
            'Debug.Print("Log_Current_Mode_Time (inc Ground Time) = " & Log_Current_Mode_Time)
            'Debug.Print("Log_Current_Mode_Flight_Time (Flying Only Reset): " & Log_Current_Mode_Flight_Time)
            'Only display this if chkboxSplitModeLandings is not checked

            If frmMainForm.chkboxSplitModeLandings.Checked = True Then Call AddModeTime()

            'ArduCopter and ArduPlane have the Mode "Name" in different cells, assume cell 1 if we dont know the type.
            If ArduType = "APM:Copter" Then
                Log_Current_Mode = DataArray(1)
            ElseIf ArduType = "ArduPlane" Then
                Log_Current_Mode = DataArray(2)
            Else
                Log_Current_Mode = DataArray(1)
            End If

            'Debug.Print("@ Logline " & DataLine & " - Mode Changed to: " & Log_Current_Mode & " @ " & Log_GPS_DateTime)

            WriteTextLog("")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Mode Changed to " & Log_Current_Mode)
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Mode Initialised at " & Log_GPS_Lat & " " & Log_GPS_Lng & " Alt: " & Log_CTUN_BarAlt & " Spd:" & Log_GPS_Spd)

            'Store the Total_Current used so far when the Mode Changes, thus Log_Total_Current - Log_Mode_Start_Current
            'will = the Total_Mode_Current
            Log_Mode_Start_Current = Log_Total_Current

            If Log_In_Flight = True Then Mode_In_Flight_Start_Time = Log_GPS_DateTime

            'Store the current time as the mode changes and store the new mode.
            Log_Last_Mode_Changed_DateTime = Log_GPS_DateTime
            'Debug.Print("Log_Last_Mode_Changed_DateTime: " & Log_Last_Mode_Changed_DateTime)
            'Initialise the Mode Time and Flight Time Variables
            Log_Current_Mode_Time = 0
            Log_Current_Mode_Flight_Time = 0
            PM_Delay_Counter = 0
            'Debug.Print("Log_Current_Mode_Time (inc Ground Time) = " & Log_Current_Mode_Time)
            'Debug.Print("Log_Current_Mode_Flight_Time (Flying Only Reset): " & Log_Current_Mode_Flight_Time)
            'Debug.Print("----------------------------------------------------------------------------------------")
            'Debug.Print("")
        End If
    End Sub

End Module
