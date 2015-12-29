Module modAdditionalChecks
    Public Sub Additional_Checks()

        'It is possible for the "Take_OFF" event to be missing from the log (found in 2014-06-01 07-13-53.log where AUTO has no Take_OFF)
        'This code checks to make sure the current in flight status makes sense
        If Log_Ground_BarAlt + 0.5 < Log_CTUN_BarAlt And Log_In_Flight = False And CTUN_ThrOut_40 = True Then
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": LOG Information: Vehicle is flying without TAKE_OFF Event recorded in the APM Log,")
            'WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ----- Testing: Log_In_Flight = " & Log_In_Flight & ", Alt = " & Log_CTUN_BarAlt & ", Throttle = " & Log_CTUN_ThrOut / 10 & "%")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": LOG Information: The Log Analiser program will simulate a TAKE_OFF Event from this point,")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": LOG Information: The effect on the results can not be determined but should be minimal.")
            Call TakeOffEvent()
            PM_Delay_Counter = 0
        End If


        'It is possible for the "Take_OFF" event to be added to the log but in actual fact the vehicle is not in the air. Pix V3.2.1 - Auto Mission Testing.log
        'This code checks to make sure the current in flight status makes sense.
        'We also add a counter to ensure we are not too quick to change the flight status.
        If Log_Ground_BarAlt + 0.5 > Log_CTUN_BarAlt And Log_In_Flight = True And CTUN_ThrOut_40 = False And Log_In_Flight_Change_Status_Counter > 500 Then  ' 1/2 second delay
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": LOG Information: Vehicle is landed without Landed Event recorded in the APM Log,")
            'WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ----- Testing: Log_In_Flight = " & Log_In_Flight & ", Alt = " & Log_CTUN_BarAlt & ", Throttle = " & Log_CTUN_ThrOut / 10 & "%")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": LOG Information: The Log Analiser program will simulate a LANDING Event from this point,")
            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": LOG Information: The effect on the results can not be determined but should be minimal.")
            Call LandedEvent()
            PM_Delay_Counter = 0
        Else
            ' Check if we should be updating the "force landing" delay counter
            If Log_Ground_BarAlt + 0.5 > Log_CTUN_BarAlt And Log_In_Flight = True And CTUN_ThrOut_40 = False Then
                Log_In_Flight_Change_Status_Counter += 1
            Else
                Log_In_Flight_Change_Status_Counter = 0
            End If
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
