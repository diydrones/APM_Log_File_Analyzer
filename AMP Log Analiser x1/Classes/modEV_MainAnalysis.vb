Module modEV_MainAnalysis

    Public Sub EV_MainAnalysis()

        ' This part only checks the already preset Variables for the data line.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE VARIABLES HAVE BEEN SET !!!

        'Debug.Print("")
        'Debug.Print("----------------------------------------------------------------------------------------")
        'Debug.Print("Event Detected:-")

        Select Case Log_EV_ID
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
            Case "8"    ' DATA_SYSTEM_TIME_SET
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": DATA_SYSTEM_TIME_SET")
            Case "9"    ' INIT_SIMPLE_BEARING
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INIT_SIMPLE_BEARING")

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
            Case "17"    ' LAND_COMPLETE_MAYBE
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": LAND_COMPLETE_MAYBE")

            Case "18"    ' LAND_COMPLETE
                'Debug.Print("LAND_COMPLETE")
                If Log_In_Flight = False Then
                    'Debug.Print("### Already NOT in Flight - Ignore ###")
                Else
                    Call LandedEvent()
                End If

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
                If Log_In_Flight = False Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": NOT_LANDED")
                    '    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ----- Testing: Log_In_Flight = " & Log_In_Flight & ", Alt = " & Log_CTUN_BarAlt & ", Throttle = " & Log_CTUN_ThrOut / 10 & "%")
                End If
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
            Case "46"    ' EPM_GRAB 
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": EPM_GRAB ")
            Case "47"    ' EPM_RELEASE
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": EPM_RELEASE")
                'Case "48"    ' EPM_NEUTRAL
                '    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": EPM_NEUTRAL")
            Case "49"    ' PARACHUTE_DISABLED
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": PARACHUTE_DISABLED")
            Case "50"    ' PARACHUTE_ENABLED
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": PARACHUTE_ENABLED")
            Case "51"    ' PARACHUTE_RELEASED
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": PARACHUTE_RELEASED")

                    'Added for v3.3
            Case "52"    ' LANDING_GEAR_DEPLOYED 
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": LANDING_GEAR_DEPLOYED")
            Case "53"    ' LANDING_GEAR_RETRACTED
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": LANDING_GEAR_RETRACTED")
            Case "54"    ' MOTORS_EMERGENCY_STOPPED
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": MOTORS_EMERGENCY_STOPPED")
            Case "55"    ' MOTORS_EMERGENCY_STOP_CLEARED
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": MOTORS_EMERGENCY_STOP_CLEARED")
            Case "56"    ' MOTORS_INTERLOCK_DISABLED
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": MOTORS_INTERLOCK_DISABLED")
            Case "57"    ' MOTORS_INTERLOCK_ENABLED
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": MOTORS_INTERLOCK_ENABLED")
            Case "58"    ' ROTOR_RUNUP_COMPLETE
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ROTOR_RUNUP_COMPLETE")
            Case "59"    ' ROTOR_SPEED_BELOW_CRITICAL
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ROTOR_SPEED_BELOW_CRITICAL")
            Case "60"    ' EKF_ALT_RESET
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": EKF_ALT_RESET")
            Case "61"    'DATA_LAND_CANCELLED_BY_PILOT.
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": DATA_LAND_CANCELLED_BY_PILOT ")
            Case Else
                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": *** Update Program to include this Event *** : " & DataArray(2))
        End Select
        'Debug.Print("----------------------------------------------------------------------------------------")
        'Debug.Print("")
        'End If

    End Sub

End Module
