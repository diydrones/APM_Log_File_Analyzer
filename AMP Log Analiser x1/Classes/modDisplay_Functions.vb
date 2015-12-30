Imports System.Globalization
Module modDisplay_Functions

    Sub WriteTextLog(ByVal LineText As String)
        'review the contents of the text that needs to be written.
        Dim txtColor As Color
        txtColor = Color.White
        If InStr(LineText, "WARNING:") Then txtColor = Color.OrangeRed
        If InStr(LineText, "ERROR:") Then txtColor = Color.OrangeRed
        If InStr(LineText, "Error:") Then txtColor = Color.OrangeRed
        If InStr(LineText, "Warning:") Then txtColor = Color.Orange
        If InStr(LineText, "Mode") Then txtColor = Color.Aqua
        If InStr(LineText, "Flight Time") Or InStr(LineText, "--") Then txtColor = Color.Aqua
        If InStr(LineText, "Testing") Then txtColor = Color.LightPink
        If InStr(LineText, "Information") Then txtColor = Color.LightGreen
        If InStr(LineText, "***") Then txtColor = Color.LightPink
        If Mid(LineText, 1, 3) = "$$$" Then
            ' Special Marker at start of line to force Text LightGreen, the marker is removed
            LineText = Mid(LineText, 4, Len(LineText) - 3)
            txtColor = Color.LightGreen
        End If

        With frmMainForm.richtxtLogAnalysis
            .SelectionStart = .Text.Length
            .SelectionColor = txtColor
            .AppendText(LineText & vbNewLine)
            .Refresh()
            My.Application.DoEvents()
            .SelectionColor = Color.Black
        End With
    End Sub

    Function FormatTextLogValuesFlying(ByVal Range As String, ByVal Alt As String, ByVal Spd As String, ByVal Dist As String, ByVal DistHome As String, ByVal GPS_Sats As String, ByVal GPS_Hdop As String, ByVal Efficiency As String) As String
        FormatTextLogValuesFlying = ""
        Dim MidPointCharTemp As Integer
        Dim MidPointValueTemp As String
        Dim ValueTemp As String = ""
        Dim LineTemp As String = ""

        ValueTemp = Range
        ValueTemp = ValueTemp.PadRight(5, " ")
        LineTemp = LineTemp & ValueTemp

        'Alt is reported in m (meters)
        'Add the ft (feet) conversion to the Alt and format so ~ is always in the middle of the string.
        ValueTemp = Alt & " ~ " & Int(Alt * 3.280833)
        MidPointCharTemp = InStr(ValueTemp, "~")
        MidPointValueTemp = Mid(ValueTemp, 1, MidPointCharTemp)
        MidPointValueTemp = MidPointValueTemp.PadLeft(7, " ")
        ValueTemp = Mid(ValueTemp, MidPointCharTemp + 1, Len(ValueTemp))
        ValueTemp = ValueTemp.PadRight(7, " ")
        ValueTemp = MidPointValueTemp & ValueTemp
        LineTemp = LineTemp & ValueTemp

        'Speed is reported in m/s (meters/second)
        'Add the mph conversion to the Spd and format so ~ is always in the middle of the string.
        ValueTemp = Spd & " ~ " & Int(Spd * 2.23693629)
        MidPointCharTemp = InStr(ValueTemp, "~")
        MidPointValueTemp = Mid(ValueTemp, 1, MidPointCharTemp)
        MidPointValueTemp = MidPointValueTemp.PadLeft(9, " ")
        ValueTemp = Mid(ValueTemp, MidPointCharTemp + 1, Len(ValueTemp))
        ValueTemp = ValueTemp.PadRight(7, " ")
        ValueTemp = MidPointValueTemp & ValueTemp
        LineTemp = LineTemp & ValueTemp

        'Distance travelled is Calculated in km (kilometers), display as meters
        If Dist <> "N/A" Then
            ValueTemp = Format(Val(Dist), "0.00") & " ~ " & Format(Dist * 0.621371192, "0.00")
        Else
            ValueTemp = "N/A ~ N/A"
        End If
        MidPointCharTemp = InStr(ValueTemp, "~")
        MidPointValueTemp = Mid(ValueTemp, 1, MidPointCharTemp)
        MidPointValueTemp = MidPointValueTemp.PadLeft(10, " ")
        ValueTemp = Mid(ValueTemp, MidPointCharTemp + 1, Len(ValueTemp))
        ValueTemp = ValueTemp.PadRight(7, " ")
        ValueTemp = MidPointValueTemp & ValueTemp
        LineTemp = LineTemp & ValueTemp

        'Distance from Home is Calculated in km (kilometers), display as meters
        'Add the feet conversion to the Distance from Home and format so ~ is always in the middle of the string.
        If DistHome <> "N/A" Then
            ValueTemp = Format(DistHome * 1000, "0.00") & " ~ " & Format(DistHome * 3280.8399, "0.00")
        Else
            ValueTemp = "N/A ~ N/A"
        End If
        MidPointCharTemp = InStr(ValueTemp, "~")
        MidPointValueTemp = Mid(ValueTemp, 1, MidPointCharTemp)
        MidPointValueTemp = MidPointValueTemp.PadLeft(11, " ")
        ValueTemp = Mid(ValueTemp, MidPointCharTemp + 1, Len(ValueTemp))
        ValueTemp = ValueTemp.PadRight(7, " ")
        ValueTemp = MidPointValueTemp & ValueTemp
        LineTemp = LineTemp & ValueTemp

        ValueTemp = GPS_Sats
        ValueTemp = ValueTemp.PadLeft(8, " ")
        LineTemp = LineTemp & ValueTemp

        ValueTemp = GPS_Hdop
        ValueTemp = ValueTemp.PadLeft(12, " ")
        LineTemp = LineTemp & ValueTemp

        'Efficiency is reported as mA/s (milliamps per second), convert to mA/m (milliamps per minute)
        If Efficiency <> "N/A" And Efficiency <> "" And Efficiency <> "~~~" Then
            ValueTemp = Format(Efficiency * 60, "0.00")
        Else
            ValueTemp = Efficiency
        End If
        ValueTemp = ValueTemp.PadLeft(14, " ")
        LineTemp = LineTemp & ValueTemp

        FormatTextLogValuesFlying = LineTemp
    End Function

    Function FormatTextLogValuesBattery(ByVal Range As String, ByVal Volt As String, ByVal Vcc As String, ByVal Curr As String, ByVal BatCap As String, ByVal CurrTot As String, ByVal Efficiency As String, ByVal MaxFlightTime As String) As String
        FormatTextLogValuesBattery = ""
        Dim ValueTemp As String = ""
        Dim LineTemp As String = ""

        ValueTemp = Range
        ValueTemp = ValueTemp.PadRight(5, " ")
        LineTemp = LineTemp & ValueTemp

        'Volts are reported as Voltage * 100 so we need to convert here to Volts
        ValueTemp = Format(Volt / 100, "0.00")
        ValueTemp = ValueTemp.PadLeft(10, " ")
        LineTemp = LineTemp & ValueTemp

        'Vcc is reported as mVoltage (i.e. V * 1000) so we need to convert here to Volts
        ValueTemp = Format(Vcc / 1000, "0.00")
        ValueTemp = ValueTemp.PadLeft(15, " ")
        LineTemp = LineTemp & ValueTemp

        'Curr is reported as mA * 1000 
        ValueTemp = Format(Curr / 100, "0.00")
        ValueTemp = ValueTemp.PadLeft(17, " ")
        LineTemp = LineTemp & ValueTemp

        'Capacity is reported as mAh
        If BatCap <> "N/A" Then
            ValueTemp = BatCap
        Else
            ValueTemp = "N/A"
        End If
        ValueTemp = ValueTemp.PadLeft(17, " ")
        LineTemp = LineTemp & ValueTemp

        'CurrTot is reported as mAh
        If CurrTot <> "N/A" Then
            ValueTemp = CurrTot
        Else
            ValueTemp = "N/A"
        End If
        ValueTemp = ValueTemp.PadLeft(17, " ")
        LineTemp = LineTemp & ValueTemp

        'Efficiency is reported as mA/s (milliamps per second), convert to mA/m (milliamps per minute)
        If Efficiency <> "N/A" And Efficiency <> "~~~" Then
            ValueTemp = Format(Efficiency * 60, "0.00")
        Else
            ValueTemp = Efficiency
        End If
        ValueTemp = ValueTemp.PadLeft(16, " ")
        LineTemp = LineTemp & ValueTemp

        'Flight Time is reported as mins
        If MaxFlightTime <> "N/A" Then
            ValueTemp = MaxFlightTime
        Else
            ValueTemp = "N/A"
        End If
        ValueTemp = ValueTemp.PadLeft(17, " ")
        LineTemp = LineTemp & ValueTemp

        FormatTextLogValuesBattery = LineTemp
    End Function

    Sub WriteLogFileHeader()
        If ArduVersion <> "" And LogAnalysisHeader = False Then
            LogAnalysisHeader = True
            WriteTextLog("APM Log File Analiser " & MyCurrentVersionNumber)
            WriteTextLog("")
            WriteTextLog("Log FileName: " & strLogPathFileName)
            WriteTextLog("Ardu Version: " & ArduVersion & " Build: " & ArduBuild)
            WriteTextLog("   Ardu Type: " & ArduType)
            If Hardware <> "" Then ' This just checks that the Hardware Parameter line was found before converting to int()
                Select Case Int(Hardware)
                    Case 0
                        Hardware = "Unknown - Redundant Parameter for PixHawk"
                    Case 1
                        Hardware = "APM1-1280"
                    Case 2
                        Hardware = "APM1 -2560"
                    Case 3
                        Hardware = "SITL"
                    Case 4
                        Hardware = "PX4v1"
                    Case 5
                        Hardware = "PX4v2"
                    Case 88
                        Hardware = "APM2"
                    Case 256
                        Hardware = "Flymaple"
                    Case 257
                        Hardware = "Linux"
                End Select
            Else
                Hardware = "Parameter Value Missing"
            End If
            If Hardware <> "" And APM_Version = "" Then WriteTextLog("    Hardware: " & Hardware)
            If APM_Free_RAM <> 0 Then WriteTextLog(" HW Free RAM: " & APM_Free_RAM)
            If APM_Version <> "" Then WriteTextLog("  HW Version: " & APM_Version)
            If Pixhawk_Serial_Number <> "" Then WriteTextLog("  Serial No.: " & Pixhawk_Serial_Number)
            Select Case APM_Frame_Type
                Case 0
                    APM_Frame_Name = "+"
                Case 1
                    APM_Frame_Name = "X"
                Case 2
                    APM_Frame_Name = "V"
                Case 3
                    APM_Frame_Name = "H"
                Case 4
                    APM_Frame_Name = "V-Tail"
                Case 10
                    APM_Frame_Name = "Y6B"
                Case Else
                    APM_Frame_Name = "Error: Please update code to determine frame type " & APM_Frame_Type
            End Select
            If APM_No_Motors = 6 Then APM_Frame_Name += " (Hexa)"
            If APM_Frame_Name <> "" Then WriteTextLog("  Frame Type: " & APM_Frame_Name)
            If APM_No_Motors <> 0 Then WriteTextLog("  No. Motors: " & APM_No_Motors)
            WriteTextLog("")
            'Display what Data Types were found in the log file.
            WriteTextLog_LoggingData()
        End If
    End Sub

    Sub WriteParamHeader()
        If Param_Issue_Found = False Then
            WriteTextLog(vbNewLine)
            WriteTextLog("PARAMETER ISSUES:-")
            Param_Issue_Found = True
        End If
    End Sub

    Sub WriteTextLog_LoggingData()
        Dim strTempEnabledText As String = ""
        Dim strTempDisabledText As String = ""
        If frmMainForm.chkboxFlightDataTypes.Checked = True Then
            If IMU_Logging Then strTempEnabledText += "IMU, " Else strTempDisabledText += "IMU, "
            If GPS_Logging Then strTempEnabledText += "GPS, " Else strTempDisabledText += "GPS, "
            If CTUN_Logging Then strTempEnabledText += "CTUN, " Else strTempDisabledText += "CTUN, "
            If PM_Logging Then strTempEnabledText += "PM, " Else strTempDisabledText += "PM, "
            If CURR_Logging Then strTempEnabledText += "CURR, " Else strTempDisabledText += "CURR, "
            If NTUN_Logging Then strTempEnabledText += "NTUN, " Else strTempDisabledText += "NTUN, "
            If MSG_Logging Then strTempEnabledText += "MSG, " Else strTempDisabledText += "MSG, "
            If ATUN_Logging Then strTempEnabledText += "ATUN, " Else strTempDisabledText += "ATUN, "
            If ATDE_logging Then strTempEnabledText += "ATDE, " Else strTempDisabledText += "ATDE, "
            If MOT_Logging Then strTempEnabledText += "MOT, " Else strTempDisabledText += "MOT, "
            If OF_Logging Then strTempEnabledText += "OF, " Else strTempDisabledText += "OF, "
            If MAG_Logging Then strTempEnabledText += "MAG, " Else strTempDisabledText += "MAG, "
            If CMD_Logging Then strTempEnabledText += "CMD, " Else strTempDisabledText += "CMD, "
            If ATT_Logging Then strTempEnabledText += "ATT, " Else strTempDisabledText += "ATT, "
            If INAV_Logging Then strTempEnabledText += "INAV, " Else strTempDisabledText += "INAV, "
            If MODE_Logging Then strTempEnabledText += "MODE, " Else strTempDisabledText += "MODE, "
            If STRT_logging Then strTempEnabledText += "STRT, " Else strTempDisabledText += "STRT, "
            If EV_Logging Then strTempEnabledText += "EV, " Else strTempDisabledText += "EV, "
            If D16_Logging Then strTempEnabledText += "D16, " Else strTempDisabledText += "D16, "
            If DU16_Logging Then strTempEnabledText += "DU16, " Else strTempDisabledText += "DU16, "
            If D32_Logging Then strTempEnabledText += "D32, " Else strTempDisabledText += "D32, "
            If DU32_Logging Then strTempEnabledText += "DU32, " Else strTempDisabledText += "DU32, "
            If DFLT_Logging Then strTempEnabledText += "DFLT, " Else strTempDisabledText += "DFLT, "
            If PID_Logging Then strTempEnabledText += "PID, " Else strTempDisabledText += "PID, "
            If CAM_Logging Then strTempEnabledText += "CAM, " Else strTempDisabledText += "CAM, "
            If ERR_Logging Then strTempEnabledText += "ERR, " Else strTempDisabledText += "ERR, "

            'Support for v3.2
            If GPS2_Logging Then strTempEnabledText += "GPS2, " Else strTempDisabledText += "GPS2, "
            If IMU2_Logging Then strTempEnabledText += "IMU2, " Else strTempDisabledText += "IMU2, "
            If IMU3_Logging Then strTempEnabledText += "IMU3, " Else strTempDisabledText += "IMU3, "
            If MAG2_Logging Then strTempEnabledText += "MAG2, " Else strTempDisabledText += "MAG2, "
            If MAG3_Logging Then strTempEnabledText += "MAG3, " Else strTempDisabledText += "MAG3, "
            If AHR2_Logging Then strTempEnabledText += "AHR2, " Else strTempDisabledText += "AHR2, "
            If EKF1_Logging Then strTempEnabledText += "EKF1, " Else strTempDisabledText += "EKF1, "
            If EKF2_Logging Then strTempEnabledText += "EKF2, " Else strTempDisabledText += "EKF2, "
            If EKF3_Logging Then strTempEnabledText += "EKF3, " Else strTempDisabledText += "EKF3, "
            If EKF4_Logging Then strTempEnabledText += "EKF4, " Else strTempDisabledText += "EKF4, "
            If TERR_Logging Then strTempEnabledText += "TERR, " Else strTempDisabledText += "TERR, "
            If UBX1_Logging Then strTempEnabledText += "UBX1, " Else strTempDisabledText += "UBX1, "
            If UBX2_Logging Then strTempEnabledText += "UBX2, " Else strTempDisabledText += "UBX2, "
            If RCIN_Logging Then strTempEnabledText += "RCIN, " Else strTempDisabledText += "RCIN, "
            If RCOU_Logging Then strTempEnabledText += "RCOU, " Else strTempDisabledText += "RCOU, "
            If BARO_Logging Then strTempEnabledText += "BARO, " Else strTempDisabledText += "BARO, "
            If POWR_Logging Then strTempEnabledText += "POWR, " Else strTempDisabledText += "POWR, "
            If RAD_Logging Then strTempEnabledText += "RAD, " Else strTempDisabledText += "RAD, "
            If SIM_Logging Then strTempEnabledText += "SIM, " Else strTempDisabledText += "SIM, "

            If Len(strTempEnabledText) > 0 Then
                strTempEnabledText = Mid(strTempEnabledText, 1, Len(strTempEnabledText) - 2)
            End If
            If Len(strTempDisabledText) > 0 Then
                strTempDisabledText = Mid(strTempDisabledText, 1, Len(strTempDisabledText) - 2)
            End If
            WriteTextLog("Data Found in APM Log File:-")
            WriteTextLog(strTempEnabledText)
            WriteTextLog("")
            WriteTextLog("Data NOT Found in APM Log File:-")
            WriteTextLog(strTempDisabledText)
            WriteTextLog("")
        End If
    End Sub

    Sub AddModeTime()
        Dim TempFlightTime As Integer = 0       'FlightTime in Seconds from the current mode.
        TempFlightTime = 0
        Dim Efficiency As String = ""           'Calculated Efficiency
        Efficiency = ""
        'Only update the display if the Flight Time is worth reporting
        'this alos removes some bugs when the mode changes very quickly
        'and not enough data is retrieved to overwrite the program variables
        'from their base settings, i.e. a min setting as 99999
        If Log_Current_Mode_Flight_Time > 0 Then
            'Select Case UCase(Log_Current_Mode)
            Select Case UCase(Log_Current_Mode)
                Case "STABILIZE"
                    STABILIZE_Flight_Time = STABILIZE_Flight_Time + Log_Current_Mode_Flight_Time
                    TempFlightTime = STABILIZE_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & STABILIZE_Flight_Time & " Seconds")
                Case "ALT_HOLD"
                    ALT_HOLD_Flight_Time = ALT_HOLD_Flight_Time + Log_Current_Mode_Flight_Time
                    TempFlightTime = ALT_HOLD_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & ALT_HOLD_Flight_Time & " Seconds")
                Case "ALTHOLD"
                    ALT_HOLD_Flight_Time = ALT_HOLD_Flight_Time + Log_Current_Mode_Flight_Time
                    TempFlightTime = ALT_HOLD_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & ALT_HOLD_Flight_Time & " Seconds")
                Case "LOITER"
                    LOITER_Flight_Time = LOITER_Flight_Time + Log_Current_Mode_Flight_Time
                    TempFlightTime = LOITER_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & LOITER_Flight_Time & " Seconds")
                Case "AUTO"
                    AUTO_Flight_Time = AUTO_Flight_Time + Log_Current_Mode_Flight_Time
                    TempFlightTime = AUTO_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & AUTO_Flight_Time & " Seconds")
                Case "RTL"
                    RTL_Flight_Time = RTL_Flight_Time + Log_Current_Mode_Flight_Time
                    TempFlightTime = RTL_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & RTL_Flight_Time & " Seconds")
                Case "LAND"
                    LAND_Flight_Time = LAND_Flight_Time + Log_Current_Mode_Flight_Time
                    TempFlightTime = LAND_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & LAND_Flight_Time & " Seconds")
                Case "FBW_A"
                    FBW_A_Flight_Time = FBW_A_Flight_Time + Log_Current_Mode_Flight_Time
                    TempFlightTime = FBW_A_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & FBW_A_Flight_Time & " Seconds")
                Case "AUTOTUNE"
                    AUTOTUNE_Flight_Time = AUTOTUNE_Flight_Time + Log_Current_Mode_Flight_Time
                    TempFlightTime = AUTOTUNE_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & AUTOTUNE_Flight_Time & " Seconds")
                Case "MANUAL"
                    MANUAL_Flight_Time = MANUAL_Flight_Time + Log_Current_Mode_Flight_Time
                    TempFlightTime = MANUAL_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & MANUAL_Flight_Time & " Seconds")
                Case Else
                    Debug.Print(vbNewLine)
                    Debug.Print("#############################################")
                    Debug.Print("CONDITION ERROR")
                    Debug.Print("New MODE Detected: " & Log_Current_Mode)
                    Debug.Print("Update ""MODE"" Code to support this new mode")
                    Debug.Print("DataLine: " & DataLine)
                    Debug.Print("#############################################")
                    Debug.Print(vbNewLine)
                    OTHER_Flight_Time = OTHER_Flight_Time + Log_Current_Mode_Flight_Time
                    TempFlightTime = OTHER_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & OTHER_Flight_Time & " Seconds")
            End Select

            'Calculate the Efficiency if we have enough data points (i.e. based on flight time)
            If Log_Current_Mode_Flight_Time > MIN_MODE_EFF_TIME Then
                Efficiency = Str(Val(Total_Mode_Current / Log_Current_Mode_Flight_Time))
            Else
                Efficiency = "~~~"
            End If

            'Update the richtxtLogAnalysis text box with all the details from the previous mode.
            WriteTextLog("       Alt(m ~ ft)   Spd(m/s ~ mph)   Dist(km ~ mi)     Launch(m ~ ft)    GPS-Sats   GPS-Hdop    Eff(mA/min)")
            WriteTextLog(FormatTextLogValuesFlying("Max", Log_Mode_Max_BarAlt, Log_Mode_Max_Spd, Mode_Dist_Travelled, Mode_Max_Dist_From_Launch, Log_Mode_Max_NSats, Log_Mode_Max_HDop, "N/A"))
            WriteTextLog(FormatTextLogValuesFlying("Avg", Int(Log_Mode_Sum_BarAlt / Log_CTUN_DLs_for_Mode), Int(Log_Mode_Sum_Spd / Log_GPS_DLs_for_Mode), "N/A", "N/A", Int(Log_Mode_Sum_NSats / Log_GPS_DLs_for_Mode), Format(Log_Mode_Sum_HDop / Log_GPS_DLs_for_Mode, "0.00"), Efficiency))
            WriteTextLog(FormatTextLogValuesFlying("Min", Log_Mode_Min_BarAlt, Log_Mode_Min_Spd, "N/A", Mode_Min_Dist_From_Launch, Log_Mode_Min_NSats, Log_Mode_Min_HDop, "N/A"))
            WriteTextLog(Log_Current_Mode & " Flight Time (Session)= " & Log_Current_Mode_Flight_Time & " seconds, " & ConvertSeconds(Log_Current_Mode_Flight_Time))
            WriteTextLog(Log_Current_Mode & " Flight Time   (Total)= " & TempFlightTime & " seconds, " & ConvertSeconds(TempFlightTime))
            'WriteTextLog("Testing:" & Total_Mode_Current & "mA" & "  " & Log_Current_Mode_Flight_Time & " secs")

            If CTUN_Logging = False Then
                WriteTextLog(" * Altitude above launch estimated from GPS Data, enable CTUN for accurate borometer data!")
            End If
            If Log_Mode_Min_NSats < 9 Then
                WriteTextLog("WARNING: Less than 9 Satellites was detected during this mode.")
            End If
            If Log_Mode_Min_HDop > 2 Then
                WriteTextLog("WARNING: A HDop of greater than 2 was detected during this mode.")
            End If

            WriteTextLog("")

        Else
            If Log_Current_Mode <> "Not Determined" Then
                WriteTextLog("Limited Data Available - Mode Ignored")
                Debug.Print("Limited Data Available - Previous Mode Ignored")
            End If
        End If

        Log_Mode_Min_Volt = 99999
        Log_Mode_Max_Volt = 0
        Log_Mode_Sum_Volt = 0

        'Reset the Mode Calculatation Variables
        Log_GPS_DLs_for_Mode = 0
        Log_Mode_Min_NSats = 99
        Log_Mode_Max_NSats = 0
        Log_Mode_Sum_NSats = 0
        Log_Mode_Min_HDop = 99
        Log_Mode_Max_HDop = 0
        Log_Mode_Sum_HDop = 0
        Log_Mode_Min_Spd = 999
        Log_Mode_Max_Spd = 0
        Log_Mode_Sum_Spd = 0
        Mode_Min_Dist_From_Launch = 99999
        Mode_Max_Dist_From_Launch = 0
        Mode_Start_Dist_Travelled = 0
        Mode_Dist_Travelled = 0
        Log_CTUN_DLs_for_Mode = 0
        Log_Mode_Min_BarAlt = 99999
        Log_Mode_Max_BarAlt = 0
        Log_Mode_Sum_BarAlt = 0
        Log_CURR_DLs_for_Mode = 0
        Log_Mode_Min_Volt = 99999
        Log_Mode_Max_Volt = 0
        Log_Mode_Sum_Volt = 0
        Log_Last_CMD_Lat = 0                        'Holds the previous WP1 Co-ordinates
        Log_Last_CMD_Lng = 0                        'Holds the previous WP1 Co-ordinates
        Log_Last_CMD_Alt = 0                        'Holds the previous WP Alititude
        Log_CMD_Dist1 = 0                           'Distance using current GPS when we hit the WP radius to the next way point
        Log_CMD_Dist2 = 0                           'Distance between the two way points.

    End Sub

    Sub AddFinalFlightSummary()
        'Calculate the Efficiency if we have enough data points (i.e. based on flight time)
        Dim Efficiency As String = ""           'Calculated Efficiency
        Dim MaxFlyTime As Integer = 0           'Calculated Maximum Flight Time
        If Log_Total_Flight_Time > MIN_EFF_TIME Then
            Efficiency = Str(Val(Log_Total_Current / Log_Total_Flight_Time))
            MaxFlyTime = (PARM_BATTERY_CAPACITY * 80 / 100) / Efficiency
        Else
            Efficiency = "~~~"
        End If

        WriteTextLog("")
        WriteTextLog("Overall Flight Summary:")
        WriteTextLog("       Alt(m ~ ft)   Spd(m/s ~ mph)   Dist(km ~ mi)     Launch(m ~ ft)    GPS-Sats   GPS-Hdop")
        WriteTextLog(FormatTextLogValuesFlying("Max", Log_Maximum_Altitude, Log_Flight_Max_Spd, Dist_Travelled, Max_Dist_From_Launch, Log_Flight_Max_NSats, Log_Flight_Max_HDop, ""))
        WriteTextLog(FormatTextLogValuesFlying("Avg", 0, 0, "N/A", "N/A", Int(Log_Flight_Sum_NSats / Log_GPS_DLs), Format(Log_Flight_Sum_HDop / Log_GPS_DLs, "0.00"), ""))
        WriteTextLog(FormatTextLogValuesFlying("Min", 0, 0, "N/A", "N/A", Log_GPS_Min_NSats, Log_GPS_Min_HDop, ""))
        If CTUN_Logging = False Then
            WriteTextLog("* Altitude above launch estimated from GPS Data, enable CTUN for accurate borometer data!")
        End If
        If Log_GPS_Min_NSats < 9 Then
            WriteTextLog("WARNING: Less than 9 Satellites was detected during this mode.")
        End If
        If Log_GPS_Min_HDop > 2 Then
            WriteTextLog("WARNING: A HDop of greater than 2 was detected during this mode.")
        End If
        WriteTextLog("")

        'Only display this if CURR logging has been performed.
        If CURR_Logging = True Then
            WriteTextLog("Power Summary:")
            ' New Power Summary Dec 2015 KXG
            WriteTextLog("       Battery(V)        Vcc(V)        Current(A)        Cap(mAh)        Used Cap(mAh)    Eff(mA/mim)    Max Fly 80%(mins)")
            WriteTextLog(FormatTextLogValuesBattery("Max", Log_Max_Battery_Volts, Log_Max_VCC, Log_Max_Battery_Current, PARM_BATTERY_CAPACITY, Log_Total_Current, "N/A", "N/A"))
            If Efficiency <> "~~~" Then
                WriteTextLog(FormatTextLogValuesBattery("Avg", 0, 0, Log_Sum_Battery_Current / Log_CURR_DLs, "N/A", "N/A", Efficiency, ConvertSeconds(MaxFlyTime)))
            End If
            WriteTextLog(FormatTextLogValuesBattery("Min", Log_Min_Battery_Volts, Log_Min_VCC, Log_Min_Battery_Current, PARM_BATTERY_CAPACITY - Log_Total_Current, "N/A", "N/A", "N/A"))
            WriteTextLog("  Overall Flight Time = " & Log_Total_Flight_Time & " seconds, " & ConvertSeconds(Log_Total_Flight_Time))
            WriteTextLog("Max Flight Time @ 80% = " & MaxFlyTime & " seconds, " & ConvertSeconds(MaxFlyTime))
            WriteTextLog("                        Max Flight Time calculation can be influenced by")
            WriteTextLog("                         -- Battery Capacity Parameter setting")
            WriteTextLog("                         -- Calibration of the Battery Current Monitoring Circuit")
            WriteTextLog("                         -- The type of flight being analysed")

            'Check that VCC is stable and in the scope on the .ini setting MAX_VCC_FLUC
            If (Log_Max_VCC / 1000) - (Log_Min_VCC / 1000) > MAX_VCC_FLUC Then
                WriteTextLog("")
                WriteTextLog("WARNING: VCC is unstable with fluctuations of " & (Log_Max_VCC / 1000) - (Log_Min_VCC / 1000) & "v reported.")
                WriteTextLog("         VCC needs to be within " & MAX_VCC_FLUC & "v according to this UAV profile")
            End If
            WriteTextLog("")

            'Add the code to check on Capacity Consumption against the set Battery Capacity in the Parameters.
            If Log_Total_Current > (PARM_BATTERY_CAPACITY * 80) / 100 Then
                WriteTextLog("WARNING: Battery Capacity in Parameters is set to: " & PARM_BATTERY_CAPACITY)
                WriteTextLog("WARNING: However, Capacity used in this flight is: " & Log_Total_Current)
                WriteTextLog("WARNING: This means you have used " & (Log_Total_Current / PARM_BATTERY_CAPACITY) * 100 & "% of the total Capacity")
                WriteTextLog("WARNING: First, Check the Battery Capacity Parameter setting is correct.")
                WriteTextLog("WARNING: Second, check the Power Calibration: https://www.youtube.com/watch?v=tEA0Or-1n18")
                WriteTextLog("WARNING: Thrid, reduce flight times to protect the main battery!")
                WriteTextLog("")
            End If
            If Log_Total_Current = 0 Then
                If PARM_BATT_CURR_PIN = -1 Then
                    WriteTextLog("APM Information: Current Sensing is disabled.")
                    WriteTextLog("")
                Else
                    WriteTextLog("WARNING: Current Sensing is Enabled but failing to measure current consumption correctly!")
                    WriteTextLog("WARNING: Issue Analysis:")
                    If PARM_BATT_CURR_PIN <> 12 And Hardware = "APM2" Then
                        WriteTextLog("WARNING:  - Parameter BATT_CURR_PIN is normally set to 12 on an APM, this is Pin " & PARM_BATT_CURR_PIN)
                    ElseIf PARM_BATT_CURR_PIN = 12 And Hardware = "APM2" Then
                        WriteTextLog("WARNING:  - Parameter BATT_CURR_PIN is set correctly for an APM as pin " & PARM_BATT_CURR_PIN)
                    ElseIf PARM_BATT_CURR_PIN <> 3 And Hardware = "PX4v2" Then
                        WriteTextLog("WARNING:  - Parameter BATT_CURR_PIN is normally set to 3 on a Pix v2, this is Pin " & PARM_BATT_CURR_PIN)
                    ElseIf PARM_BATT_CURR_PIN = 3 And Hardware = "PX4v2" Then
                        WriteTextLog("WARNING:  - Parameter BATT_CURR_PIN is set correctly for a Pixhawk as pin " & PARM_BATT_CURR_PIN)
                    Else
                        WriteTextLog("WARNING:  - Situation can not be automatically determined " & PARM_BATT_CURR_PIN)
                    End If
                    WriteTextLog("WARNING:  - Calibrate your Power Module: https://www.youtube.com/watch?v=tEA0Or-1n18")
                    WriteTextLog("WARNING:  - Switch off Current Sensing by setting Parameter BATT_CURR_PIN to -1")
                    WriteTextLog("")
                End If
            End If

        Else
            WriteTextLog("*** Enable CURR logging to view battery and flight efficiency data.")
            WriteTextLog("")
        End If

        WriteTextLog("*** Vibration Analysis has been removed from v2.2 onwards, you should now")
        WriteTextLog("*** review the ""Vibration Graph"" instead to understand your levels of vibration.")
        WriteTextLog("")

        'Change the y min max values of the Voltage chart to meet the needs of the users flight pack.
        frmMainForm.chartPowerRails.ChartAreas("Volts").AxisY.Minimum = Int((Log_Min_Battery_Volts / 100)) - 0.5
        frmMainForm.chartPowerRails.ChartAreas("Volts").AxisY.Maximum = Int((Log_Max_Battery_Volts / 100)) + 1.5

    End Sub


    Public Function StandardDeviation(NumericArray As Object) As Double

        Dim dblSum As Double = 0
        Dim dblSumSqdDevs As Double = 0
        Dim dblMean As Double = 0
        Dim lngCount As Long = 0
        Dim dblAnswer As Double = 0
        Dim vElement As Object = DBNull.Value
        Dim lngStartPoint As Long = 0
        Dim lngEndPoint As Long = 0
        Dim lngCtr As Long = 0

        'if NumericArray is not an array, this statement will
        'raise an error in the errorhandler
        lngCount = UBound(NumericArray)
        lngCount = 0

        'the check below will allow
        'for 0 or 1 based arrays.
        vElement = NumericArray(0)

        lngStartPoint = IIf(Err.Number = 0, 0, 1)
        lngEndPoint = UBound(NumericArray)

        'get sum and sample size
        For lngCtr = lngStartPoint To lngEndPoint
            vElement = NumericArray(lngCtr)
            If IsNumeric(vElement) Then
                lngCount = lngCount + 1
                dblSum = dblSum + CDbl(vElement)
            End If
        Next

        'get mean
        If lngCount > 1 Then
            dblMean = dblSum / lngCount

            'get sum of squared deviations
            For lngCtr = lngStartPoint To lngEndPoint
                vElement = NumericArray(lngCtr)

                If IsNumeric(vElement) Then
                    dblSumSqdDevs = dblSumSqdDevs + ((vElement - dblMean) ^ 2)
                End If
            Next

            'divide result by sample size - 1 and get square root. 
            'this function calculates standard deviation of a sample.  
            'If your  set of values represents the population, use sample
            'size not sample size - 1
            If lngCount > 1 Then
                lngCount = lngCount - 1 'eliminate for population values
                dblAnswer = Math.Sqrt(dblSumSqdDevs / lngCount)
            End If
        End If
        StandardDeviation = dblAnswer
    End Function

    Public Sub Chart_PowerRails_Visible(OnOff As Boolean)
        frmMainForm.panPowerRails.Visible = OnOff
        frmMainForm.Refresh()
    End Sub

    Public Sub Chart_Vibrations_Visible(OnOff As Boolean)
        frmMainForm.panVibrations.Visible = OnOff
        frmMainForm.Refresh()
    End Sub

    Public Sub Chart_GPS_Visible(OnOff As Boolean)
        frmMainForm.panGPS.Visible = OnOff
        frmMainForm.Refresh()
    End Sub

    Public Sub Chart_Attitude_Visible(OnOff As Boolean)
        frmMainForm.panAttitude.Visible = OnOff
        frmMainForm.Refresh()
    End Sub

    Public Sub Chart_Travel_Visible(OnOff As Boolean)
        frmMainForm.panTravel.Visible = OnOff
        frmMainForm.Refresh()
    End Sub

    Public Sub ButtonsCheckBoxes_Visible(OnOff As Boolean)
        frmMainForm.btnLoadLog.Visible = OnOff
        frmMainForm.panAnalysis.Visible = OnOff
        frmMainForm.panAnalysisButtons.Visible = Not OnOff
        frmMainForm.panGraphButtons.Visible = OnOff
        frmMainForm.btnParameters.Visible = OnOff
    End Sub

    Public Sub ButtonsCharting_Visible(OnOff As Boolean)
        frmMainForm.panAnalysisButtons.Visible = Not OnOff

        'Graph Button Handling.
        frmMainForm.panGraphButtons.Visible = OnOff
        'Enable each button if data is available.
        If OnOff = True Then 'only valid if we are displaying graph buttons.
            If IMU_Logging = True Then frmMainForm.btnVibrationChart.Enabled = True Else frmMainForm.btnVibrationChart.Enabled = False
            If CURR_Logging = True Then frmMainForm.btnPowerChart.Enabled = True Else frmMainForm.btnPowerChart.Enabled = False
            If GPS_Logging = True Then frmMainForm.btnGPSChart.Enabled = True Else frmMainForm.btnGPSChart.Enabled = False
            If ATT_Logging = True Then frmMainForm.btnAttitudeChart.Enabled = True Else frmMainForm.btnAttitudeChart.Enabled = False
        End If

        frmMainForm.btnAnalysis.Visible = OnOff
        frmMainForm.btnCopyText.Visible = Not OnOff
        frmMainForm.btnGraphs.Visible = Not OnOff
        frmMainForm.btnAnalyze.Visible = Not OnOff
    End Sub

    Public Sub ShowParametersForm()
        frmParameters.Show()
        ParametersVisible = Not ParametersVisible
        frmParameters.Visible = ParametersVisible
    End Sub
End Module
