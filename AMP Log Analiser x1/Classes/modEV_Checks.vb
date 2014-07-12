Module modEV_Checks
    Public Sub EV_Checks()
        If DataArray(0) = "EV" Then
            'An EV value should have only 1 pieces of numeric data!
            If ReadFileResilienceCheck(1) = True Then
                'Debug.Print("")
                'Debug.Print("----------------------------------------------------------------------------------------")
                'Debug.Print("Event Detected:-")

                Select Case DataArray(1)
                    Case "10"    ' ARMED
                        'Debug.Print("ARMED")
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Armed")
                        If CTUN_Logging = True Then Log_Armed_BarAlt = Log_CTUN_BarAlt Else Log_Armed_BarAlt = Log_GPS_Alt - Log_Temp_Ground_GPS_Alt
                        Log_Armed = True
                        PM_Delay_Counter = 0

                    Case "11"    ' DISARMED
                        'Debug.Print("DISARMED")
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": DisArmed")
                        CTUN_ThrottleUp = False
                        If CTUN_Logging = True Then Log_Disarmed_BarAlt = Log_CTUN_BarAlt Else Log_Disarmed_BarAlt = Log_GPS_Alt - Log_Temp_Ground_GPS_Alt
                        Log_Armed = False
                        PM_Delay_Counter = 0

                    Case "15"    ' AUTO_ARMED
                        'Debug.Print("AUTO_ARMED")
                        'Debug.Print(vbNewLine)
                        'Debug.Print("Log_Total_Flight_Time: " & Log_Total_Flight_Time)
                        'Debug.Print("Current_Flight_Time: " & Log_Current_Flight_Time)
                        If Log_In_Flight = True Then
                            'Debug.Print("### Already in Flight - Ignore ###")
                        Else
                            'Debug.Print("Execute the TAKEOFF Code")
                            Call TakeOffEvent()
                            PM_Delay_Counter = 0
                        End If

                    Case "16"    ' TAKEOFF
                        'Debug.Print("TAKEOFF")
                        'Debug.Print(vbNewLine)
                        'Debug.Print("Log_Total_Flight_Time: " & Log_Total_Flight_Time)
                        'Debug.Print("Current_Flight_Time: " & Log_Current_Flight_Time)
                        If Log_In_Flight = True Then
                            'Debug.Print("### Already in Flight  - Ignore ###")
                        Else
                            Call TakeOffEvent()
                            PM_Delay_Counter = 0
                        End If

                    Case "18"    ' LAND_COMPLETE
                        'Debug.Print("LAND_COMPLETE")
                        If Log_In_Flight = False Then
                            'Debug.Print("### Already NOT in Flight - Ignore ###")
                        Else

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
                        End If
                End Select


                ' Only check these events if the chkboxNonCriticalEvents is checked
                If frmMainForm.chkboxNonCriticalEvents.Checked = True Then
                    Select Case DataArray(1)
                        Case "1"    ' MAVLINK_FLOAT
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": MAVLINK_FLOAT")
                        Case "2"    ' MAVLINK_INT32
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": MAVLINK_INT32")
                        Case "3"    ' MAVLINK_INT16
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": MAVLINK_INT16")
                        Case "4"    ' MAVLINK_INT8
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": MAVLINK_INT8")
                        Case "7"    ' AP_STATE
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AP_STATE")
                        Case "9"    ' INIT_SIMPLE_BEARING
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INIT_SIMPLE_BEARING")

                        Case "10" 'this case is captured above, this is just so the Else Statement is not executed.
                        Case "11" 'this case is captured above, this is just so the Else Statement is not executed.
                        Case "15" 'this case is captured above, this is just so the Else Statement is not executed.
                        Case "16" 'this case is captured above, this is just so the Else Statement is not executed.
                        Case "18" 'this case is captured above, this is just so the Else Statement is not executed.

                        Case "19"    ' LOST_GPS
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": LOST_GPS")
                        Case "21"    ' FLIP_START
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIP_START")
                        Case "22"    ' FLIP_END
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIP_END")
                        Case "25"    ' SET_HOME
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": SET_HOME")
                        Case "26"    ' SET_SIMPLE_ON
                            If DU32_Logging <> True Or frmMainForm.chkboxDU32.Checked <> True Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": SET_SIMPLE_ON")
                            End If
                        Case "27"    ' SET_SIMPLE_OFF
                            If DU32_Logging <> True Or frmMainForm.chkboxDU32.Checked <> True Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": SET_SIMPLE_OFF")
                            End If
                        Case "28"    ' NOT_LANDED
                            'If Log_In_Flight = False Then
                            '    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": NOT_LANDED")
                            '    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ----- Testing: Log_In_Flight = " & Log_In_Flight & ", Alt = " & Log_CTUN_BarAlt & ", Throttle = " & Log_CTUN_ThrOut / 10 & "%")
                            'End If
                            PM_Delay_Counter = 0
                        Case "29"    ' SET_SUPERSIMPLE_ON
                            If DU32_Logging <> True Or frmMainForm.chkboxDU32.Checked <> True Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ":  SET_SUPERSIMPLE_ON")
                            End If
                        Case "30"    ' AUTOTUNE_INITIALISED
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE_INITIALISED")
                        Case "31"    ' AUTOTUNE_OFF
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE_OFF")
                        Case "32"    ' AUTOTUNE_RESTART
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE_RESTART")
                        Case "33"    ' AUTOTUNE_SUCCESS
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE_SUCCESS")
                        Case "34"    ' AUTOTUNE_FAILED
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE_FAILED")
                        Case "35"    ' AUTOTUNE_REACHED_LIMIT
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE_REACHED_LIMIT")
                        Case "36"    ' AUTOTUNE_PILOT_TESTING
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE_PILOT_TESTING")
                        Case "37"    ' AUTOTUNE_SAVEDGAINS
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE_SAVEDGAINS")
                        Case "38"    ' SAVE_TRIM
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": SAVE_TRIM")
                        Case "39"    ' SAVEWP_ADD_WP
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": SAVEWP_ADD_WP: " & Log_GPS_Lat & " " & Log_GPS_Lng & " Alt: " & Log_CTUN_BarAlt)
                        Case "40"    ' SAVEWP_CLEAR_MISSION_RTL
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": SAVEWP_CLEAR_MISSION_RTL")
                        Case "41"    ' FENCE_ENABLE
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE_ENABLE")
                        Case "42"    ' FENCE_DISABLE
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE_DISABLE")
                        Case "43"    ' ACRO_TRAINER_DISABLED
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ACRO_TRAINER_DISABLED")
                        Case "44"    ' ACRO_TRAINER_LEVELING
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ACRO_TRAINER_LEVELING")
                        Case "45"    ' ACRO_TRAINER_LIMITED
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ACRO_TRAINER_LIMITED")
                        Case "46"    ' EPM_ON
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": EPM_ON")
                        Case "47"    ' EPM_OFF
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": EPM_OFF")
                        Case "48"    ' EPM_NEUTRAL
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": EPM_NEUTRAL")
                        Case "49"    ' PARACHUTE_DISABLED
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": PARACHUTE_DISABLED")
                        Case "50"    ' PARACHUTE_ENABLED
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": PARACHUTE_ENABLED")
                        Case "51"    ' PARACHUTE_RELEASED
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": PARACHUTE_RELEASED")
                        Case Else
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Update Program to include this Event: " & DataArray(1))
                    End Select
                End If
                'Debug.Print("----------------------------------------------------------------------------------------")
                'Debug.Print("")
            End If
        End If

    End Sub
End Module
