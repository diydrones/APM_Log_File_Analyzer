Module modMODE_MainAnalysis

    Public Sub MODE_MainAnalysis()

        ' This part only checks the already preset Variables for the data line.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE VARIABLES HAVE BEEN SET !!!

        'Debug.Print("@ Logline " & DataLine & " - Mode Changed to: " & Log_Current_Mode & " @ " & Log_GPS_DateTime)

        ' When AutoTune activate it gives a log Mode Change with a " " for the Mode.
        ' I'm not sure if it does this with any other special modes.
        If Log_Current_Mode = " " Then Log_Current_Mode = "SPECIAL SUB MODE"

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

    End Sub

End Module
