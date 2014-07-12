Module modAdditionalChecks
    Public Sub Additional_Checks()

        'It has been noted that it is possible for the AMP to believe it is being powered by a USB plug while in flight
        'from ArduCopter firmware v3.1.2 then GPS glitches are disabled while USB connection is active.
        'This issue is currently under investigation.
        If Log_In_Flight = True And Log_DU32_USB = True And USB_Warning1 = False Then
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Error: APM believes USB is connected while in flight.")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Error: This situation is currently under investigation by Robert Lefebvre (ArduPilot Developer)")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Error: If flying when the ArduPilot believes you are connected to a USB power source then")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Error: there ""could"" be a possibility that GPS Glitches are being ignored from v3.1.2 onwards.")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Error: Follow ArduPilot Issue 1170 for updates, https://github.com/diydrones/ardupilot/issues/1170")
            USB_Warning1 = True
        End If


        'It is possible for the "Take_OFF" event to be missing from the log (found in 2014-06-01 07-13-53.log where AUTO has no Take_OFF)
        'This code checks to make sure the current in flight status makes sense
        If Log_Ground_BarAlt + 0.5 < Log_CTUN_BarAlt And Log_In_Flight = False And CTUN_ThrOut_40 = True Then
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": LOG Information: Vehicle is flying without TAKE_OFF Event recorded in the APM Log,")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ----- Testing: Log_In_Flight = " & Log_In_Flight & ", Alt = " & Log_CTUN_BarAlt & ", Throttle = " & Log_CTUN_ThrOut / 10 & "%")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": LOG Information: The Log Analiser program will simulate a TAKE_OFF Event from this point,")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": LOG Information: The effect on the results can not be determined but should be minimal.")
            Call TakeOffEvent()
            PM_Delay_Counter = 0
        End If


        'It is possible for Log_Last_Mode_Changed_DateTime to equal GPS_Base_Date
        'where the first GPS log has not been read before the first MODE change.
        'This is quite common, this line traps the error and updates Log_Last_Mode_Changed_DateTime
        'with the Log_First_DateTime  
        If Log_Last_Mode_Changed_DateTime = GPS_Base_Date And Log_First_DateTime <> GPS_Base_Date Then
            Log_Last_Mode_Changed_DateTime = Log_First_DateTime

            'If we are already in flight and we only just have the GPS data then we must run the Take Off Code
            First_In_Flight = False  'Reset the First in Flight as we need to capture the Launch GPS Data
            Call TakeOffEvent()

            'Debug.Print("Mode Time Changed to: " & Log_Last_Mode_Changed_DateTime)
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Mode Time Changed to " & Log_Last_Mode_Changed_DateTime)
        End If


    End Sub
End Module
