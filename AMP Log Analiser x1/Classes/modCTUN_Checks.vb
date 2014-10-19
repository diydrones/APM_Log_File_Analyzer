Module modCTUN_Checks
    Public Sub CTUN_Checks()
        If DataArray(0) = "CTUN" Then
            'An CTUN value should have only 9 pieces of numeric data!
            If ReadFileResilienceCheck(9) = True Then
                Log_CTUN_ThrIn = Val(DataArray(1))
                Log_CTUN_SonAlt = Val(DataArray(2))
                Log_CTUN_BarAlt = Val(DataArray(3))
                Log_CTUN_WPAlt = Val(DataArray(4))
                Log_CTUN_DesSonAlt = Val(DataArray(5))
                Log_CTUN_AngBst = Val(DataArray(6))
                Log_CTUN_CRate = Val(DataArray(7))
                Log_CTUN_ThrOut = Val(DataArray(8))
                Log_CTUN_DCRate = Val(DataArray(9))


                'Capture Throttle up based on CTUN.
                If Log_CTUN_ThrOut > 0 And CTUN_ThrottleUp = False Then
                    CTUN_ThrottleUp = True
                    'Log_Armed_BarAlt = Log_CTUN_BarAlt
                End If

                'Capture Throttle % based on CTUN.
                If Log_CTUN_ThrOut > 400 Then
                    CTUN_ThrOut_40 = True
                Else
                    CTUN_ThrOut_40 = False
                End If

                'Capture Throttle up based on CTUN and Capture BarAlt if just throttled down
                If Log_CTUN_ThrOut = 0 And CTUN_ThrottleUp = True Then
                    'CTUN_ThrottleUp = False
                    'Log_Disarmed_BarAlt = Log_CTUN_BarAlt
                End If
                'Capture Throttle up based on CTUN and Capture BarAlt if we have just landed.
                'Also add the "In Flight Time" to the Total so far.
                If Log_CTUN_ThrOut = 0 And CTUN_ThrottleUp = False And Log_In_Flight = True Then
                    'Log_Ground_BarAlt = Log_CTUN_BarAlt
                    'Debug.Print(vbNewLine)
                    'Debug.Print("CTUN detected Aircraft Landed (Old Code, Now Driven from EV Data):")
                    'Debug.Print("Log_Total_Flight_Time: " & Log_Total_Flight_Time)
                    'Debug.Print("Current_Flight_Time (should be Zero now): " & Log_Current_Flight_Time)
                    'Log_Total_Flight_Time = Log_Total_Flight_Time + Log_Current_Flight_Time
                    'Log_Current_Flight_Time = 0
                    'Log_In_Flight = False
                End If

                'Capture the Maximum Altitude detected.
                If Log_CTUN_BarAlt > Log_Maximum_Altitude Then Log_Maximum_Altitude = Log_CTUN_BarAlt

                'Try to workout if the ArduCopter is in the air :)
                If Log_Ground_BarAlt < Log_CTUN_BarAlt And Log_In_Flight = False Then
                    'Debug.Print(vbNewLine)
                    'Debug.Print("CTUN detected Aircraft Take-Off (Old Code, Now Driven from EV Data):")
                    'Debug.Print("Log_Total_Flight_Time: " & Log_Total_Flight_Time)
                    'Debug.Print("Current_Flight_Time (should be Zero now): " & Log_Current_Flight_Time)
                    'Log_In_Flight = True

                    'Capture the GPS time as the Quad takes off.
                    'In_Flight_Start_Time = Log_GPS_DateTime

                    'Capture the First GPS Lat & Lng if this is the first take off.
                    'If First_In_Flight = False Then
                    '    First_GPS_Lat = Log_GPS_Lat
                    '    First_GPS_Lng = Log_GPS_Lng
                    '    First_In_Flight = True
                    'End If
                End If

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
            End If
        End If
    End Sub
End Module
