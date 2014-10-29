Imports System.Globalization
Module modDisplay_Functions

    Sub WriteTextLog(ByVal LineText As String)
        'set the timer.

        'review the contents of the text that needs to be written.
        Dim txtColor As Color
        txtColor = Color.White
        If InStr(LineText, "WARNING:") Then txtColor = Color.OrangeRed
        If InStr(LineText, "ERROR:") Then txtColor = Color.OrangeRed
        If InStr(LineText, "Error:") Then txtColor = Color.OrangeRed
        If InStr(LineText, "Warning:") Then txtColor = Color.Orange
        If InStr(LineText, "Mode") Then txtColor = Color.Aqua
        If InStr(LineText, "Flight Time") Then txtColor = Color.Aqua
        If InStr(LineText, "Testing") Then txtColor = Color.LightPink
        If InStr(LineText, "Information") Then txtColor = Color.LightGreen
        If InStr(LineText, "***") Then txtColor = Color.LightGreen

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

    Function FormatTextLogValuesBattery(ByVal Range As String, ByVal Volt As String, ByVal Vcc As String, ByVal Curr As String, ByVal CurrTot As String, ByVal Efficiency As String) As String
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

        'CurrTot is reported as mA 
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

        FormatTextLogValuesBattery = LineTemp
    End Function

    Function FormatTextLogValuesVibration(Range As String, AccX As Single, AccY As Single, AccZ As Single, Spd As Single, Alt As Single) As String
        Dim ValueTemp As String = ""
        Dim LineTemp As String = ""
        Dim Value As Single = 0

        ValueTemp = Range
        ValueTemp = ValueTemp.PadRight(5, " ")
        LineTemp = LineTemp & ValueTemp

        ValueTemp = AccX.ToString("00.000", CultureInfo.CurrentCulture.NumberFormat)  'Format(Val(AccX), "00.000")
        ValueTemp = ValueTemp.PadLeft(10, " ")
        LineTemp = LineTemp & ValueTemp

        ValueTemp = AccY.ToString("00.000", CultureInfo.CurrentCulture.NumberFormat)   'Format(Val(AccY), "00.000")
        ValueTemp = ValueTemp.PadLeft(10, " ")
        LineTemp = LineTemp & ValueTemp

        ValueTemp = AccZ.ToString("00.000", CultureInfo.CurrentCulture.NumberFormat)    'Format(Val(AccZ), "00.000")
        ValueTemp = ValueTemp.PadLeft(10, " ")
        LineTemp = LineTemp & ValueTemp

        ValueTemp = Spd.ToString("00", CultureInfo.CurrentCulture.NumberFormat)    'Format(Val(AccZ), "00.000")
        ValueTemp = ValueTemp.PadLeft(7, " ")
        LineTemp = LineTemp & ValueTemp

        ValueTemp = Alt.ToString("00.00", CultureInfo.CurrentCulture.NumberFormat)    'Format(Val(AccZ), "00.000")
        ValueTemp = ValueTemp.PadLeft(12, " ")
        LineTemp = LineTemp & ValueTemp

        FormatTextLogValuesVibration = LineTemp
    End Function

    Sub WriteLogFileHeader()
        If ArduVersion <> "" And LogAnalysisHeader = False Then
            LogAnalysisHeader = True
            WriteTextLog("APM Log File Analiser " & MyCurrentVersionNumber)
            WriteTextLog("")
            WriteTextLog("Log FileName: " & strLogPathFileName)
            WriteTextLog("Ardu Version: " & ArduVersion & " Build: " & ArduBuild)
            WriteTextLog("   Ardu Type: " & ArduType)
            WriteTextLog("APM Free RAM: " & APM_Free_RAM)
            WriteTextLog(" APM Version: " & APM_Version)
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
            WriteTextLog("  Frame Type: " & APM_Frame_Name)
            WriteTextLog("  No. Motors: " & APM_No_Motors)
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
            If IMU_Logging Then strTempEnabledText = strTempEnabledText & "IMU, " Else strTempDisabledText = strTempDisabledText & "IMU, "
            If GPS_Logging Then strTempEnabledText = strTempEnabledText & "GPS, " Else strTempDisabledText = strTempDisabledText & "GPS, "
            If CTUN_Logging Then strTempEnabledText = strTempEnabledText & "CTUN, " Else strTempDisabledText = strTempDisabledText & "CTUN, "
            If PM_Logging Then strTempEnabledText = strTempEnabledText & "PM, " Else strTempDisabledText = strTempDisabledText & "PM, "
            If CURR_Logging Then strTempEnabledText = strTempEnabledText & "CURR, " Else strTempDisabledText = strTempDisabledText & "CURR, "
            If NTUN_Logging Then strTempEnabledText = strTempEnabledText & "NTUN, " Else strTempDisabledText = strTempDisabledText & "NTUN, "
            If MSG_Logging Then strTempEnabledText = strTempEnabledText & "MSG, " Else strTempDisabledText = strTempDisabledText & "MSG, "
            If ATUN_Logging Then strTempEnabledText = strTempEnabledText & "ATUN, " Else strTempDisabledText = strTempDisabledText & "ATUN, "
            If ATDE_logging Then strTempEnabledText = strTempEnabledText & "ATDE, " Else strTempDisabledText = strTempDisabledText & "ATDE, "
            If MOT_Logging Then strTempEnabledText = strTempEnabledText & "MOT, " Else strTempDisabledText = strTempDisabledText & "MOT, "
            If OF_Logging Then strTempEnabledText = strTempEnabledText & "OF, " Else strTempDisabledText = strTempDisabledText & "OF, "
            If MAG_Logging Then strTempEnabledText = strTempEnabledText & "MAG, " Else strTempDisabledText = strTempDisabledText & "MAG, "
            If CMD_Logging Then strTempEnabledText = strTempEnabledText & "CMD, " Else strTempDisabledText = strTempDisabledText & "CMD, "
            If ATT_Logging Then strTempEnabledText = strTempEnabledText & "ATT, " Else strTempDisabledText = strTempDisabledText & "ATT, "
            If INAV_Logging Then strTempEnabledText = strTempEnabledText & "INAV, " Else strTempDisabledText = strTempDisabledText & "INAV, "
            If MODE_Logging Then strTempEnabledText = strTempEnabledText & "MODE, " Else strTempDisabledText = strTempDisabledText & "MODE, "
            If STRT_logging Then strTempEnabledText = strTempEnabledText & "STRT, " Else strTempDisabledText = strTempDisabledText & "STRT, "
            If EV_Logging Then strTempEnabledText = strTempEnabledText & "EV, " Else strTempDisabledText = strTempDisabledText & "EV, "
            If D16_Logging Then strTempEnabledText = strTempEnabledText & "D16, " Else strTempDisabledText = strTempDisabledText & "D16, "
            If DU16_Logging Then strTempEnabledText = strTempEnabledText & "DU16, " Else strTempDisabledText = strTempDisabledText & "DU16, "
            If D32_Logging Then strTempEnabledText = strTempEnabledText & "D32, " Else strTempDisabledText = strTempDisabledText & "D32, "
            If DU32_Logging Then strTempEnabledText = strTempEnabledText & "DU32, " Else strTempDisabledText = strTempDisabledText & "DU32, "
            If DFLT_Logging Then strTempEnabledText = strTempEnabledText & "DFLT, " Else strTempDisabledText = strTempDisabledText & "DFLT, "
            If PID_Logging Then strTempEnabledText = strTempEnabledText & "PID, " Else strTempDisabledText = strTempDisabledText & "PID, "
            If CAM_Logging Then strTempEnabledText = strTempEnabledText & "CAM, " Else strTempDisabledText = strTempDisabledText & "CAM, "
            If ERR_Logging Then strTempEnabledText = strTempEnabledText & "ERR, " Else strTempDisabledText = strTempDisabledText & "ERR, "
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
        Dim TempFlightTime As Integer = 0   'FlightTime in Seconds from the current mode.
        Dim Efficiency As String = ""        'Calculated Efficiency
        'Only update the display if the Flight Time is worth reporting
        'this alos removes some bugs when the mode changes very quickly
        'and not enough data is retrieved to overwrite the program variables
        'from their base settings, i.e. a min setting as 99999
        If Log_Current_Mode_Flight_Time > 0 Then
            Select Case Log_Current_Mode
                Case "STABILIZE"
                    STABILIZE_Flight_Time = STABILIZE_Flight_Time + Log_Current_Mode_Flight_Time    'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = STABILIZE_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & STABILIZE_Flight_Time & " Seconds")
                Case "ALT_HOLD"
                    ALT_HOLD_Flight_Time = ALT_HOLD_Flight_Time + Log_Current_Mode_Flight_Time      'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = ALT_HOLD_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & ALT_HOLD_Flight_Time & " Seconds")
                Case "LOITER"
                    LOITER_Flight_Time = LOITER_Flight_Time + Log_Current_Mode_Flight_Time          'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = LOITER_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & LOITER_Flight_Time & " Seconds")
                Case "AUTO"
                    AUTO_Flight_Time = AUTO_Flight_Time + Log_Current_Mode_Flight_Time              'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = AUTO_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & AUTO_Flight_Time & " Seconds")
                Case "RTL"
                    RTL_Flight_Time = RTL_Flight_Time + Log_Current_Mode_Flight_Time                'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = RTL_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & RTL_Flight_Time & " Seconds")
                Case "LAND"
                    LAND_Flight_Time = LAND_Flight_Time + Log_Current_Mode_Flight_Time              'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = LAND_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & LAND_Flight_Time & " Seconds")
                Case "FBW_A"
                    FBW_A_Flight_Time = FBW_A_Flight_Time + Log_Current_Mode_Flight_Time              'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = FBW_A_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & FBW_A_Flight_Time & " Seconds")
                Case "AUTOTUNE"
                    AUTOTUNE_Flight_Time = AUTOTUNE_Flight_Time + Log_Current_Mode_Flight_Time              'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = AUTOTUNE_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & AUTOTUNE_Flight_Time & " Seconds")
                Case "Manual"
                    MANUAL_Flight_Time = MANUAL_Flight_Time + Log_Current_Mode_Flight_Time              'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = MANUAL_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & MANUAL_Flight_Time & " Seconds")
                Case Else
                    Debug.Print(vbNewLine)
                    Debug.Print("#############################################")
                    Debug.Print("CONDITION ERROR")
                    Debug.Print("New MODE Detected: " & Log_Current_Mode)
                    Debug.Print("Update ""MODE"" Code to support this new mode")
                    Debug.Print("#############################################")
                    Debug.Print(vbNewLine)
                    OTHER_Flight_Time = OTHER_Flight_Time + Log_Current_Mode_Flight_Time            'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
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
            WriteTextLog(Log_Current_Mode & " Flight Time (Session)= " & Log_Current_Mode_Flight_Time & " seconds, " & Int(Log_Current_Mode_Flight_Time / 60) & ":" & Format((((Log_Current_Mode_Flight_Time / 60) - Int(Log_Current_Mode_Flight_Time / 60)) * 60), "00"))
            WriteTextLog(Log_Current_Mode & " Flight Time   (Total)= " & TempFlightTime & " seconds, " & Int(TempFlightTime / 60) & ":" & Format((((TempFlightTime / 60) - Int(TempFlightTime / 60)) * 60), "00"))
            If CTUN_Logging = False Then
                WriteTextLog("* Altitude above launch estimated from GPS Data, enable CTUN for accurate borometer data!")
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
        Log_Last_CMD_Lat = 0                      'Holds the previous WP1 Co-ordinates
        Log_Last_CMD_Lng = 0                      'Holds the previous WP1 Co-ordinates
        Log_Last_CMD_Alt = 0                      'Holds the previous WP Alititude
        Log_CMD_Dist1 = 0                    'Distance using current GPS when we hit the WP radius to the next way point
        Log_CMD_Dist2 = 0                     'Distance between the two way points.

    End Sub

    Sub AddFinalFlightSummary()
        'Calculate the Efficiency if we have enough data points (i.e. based on flight time)
        Dim Efficiency As String = ""        'Calculated Efficiency
        If Log_Total_Flight_Time > MIN_EFF_TIME Then
            Efficiency = Str(Val(Log_Total_Current / Log_Total_Flight_Time))
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
            WriteTextLog("       Battery(V)        Vcc(V)        Current(A)       Used Cap(mAh)    Eff(mA/mim)")
            WriteTextLog(FormatTextLogValuesBattery("Max", Log_Max_Battery_Volts, Log_Max_VCC, Log_Max_Battery_Current, Log_Total_Current, "N/A"))
            WriteTextLog(FormatTextLogValuesBattery("Avg", 0, 0, Log_Sum_Battery_Current / Log_CURR_DLs, "N/A", Efficiency))
            WriteTextLog(FormatTextLogValuesBattery("Min", Log_Min_Battery_Volts, Log_Min_VCC, Log_Min_Battery_Current, "N/A", "N/A"))
            WriteTextLog("Overall Flight Time = " & Log_Total_Flight_Time & " seconds, " & Int(Log_Total_Flight_Time / 60) & ":" & Format((((Log_Total_Flight_Time / 60) - Int(Log_Total_Flight_Time / 60)) * 60), "00"))
            'Check that VCC is stable and in the scope on the .ini setting MAX_VCC_FLUC
            If (Log_Max_VCC / 1000) - (Log_Min_VCC / 1000) > MAX_VCC_FLUC Then
                WriteTextLog("")
                WriteTextLog("WARNING: VCC is unstable with fluctuations of " & (Log_Max_VCC / 1000) - (Log_Min_VCC / 1000) & "v reported.")
                WriteTextLog("         VCC needs to be within " & MAX_VCC_FLUC & "v according to this UAV profile")
            End If
            WriteTextLog("")

            'Add the code to check on Capacity Consumption against the set Battery Capacity in the Parameters.
            If Log_Total_Current > (Log_Battery_Capacity * 80) / 100 Then
                WriteTextLog("")
                WriteTextLog("WARNING: Battery Capacity in Parameters is set to: " & Log_Battery_Capacity)
                WriteTextLog("WARNING: However, Capacity used in this flight is: " & Log_Total_Current)
                WriteTextLog("WARNING: This means you have used " & (Log_Total_Current / Log_Battery_Capacity) * 100 & "% of the total Capacity")
                WriteTextLog("WARNING: First, Check the Battery Capacity Parameter setting is correct.")
                WriteTextLog("WARNING: Second, check the Power Calibration: https://www.youtube.com/watch?v=tEA0Or-1n18")
                WriteTextLog("WARNING: Thrid, reduce flight times to protect the main battery!")
            End If
            If Log_Total_Current = 0 Then
                WriteTextLog("")
                WriteTextLog("WARNING: If an APM Current Sensing Power Module is fitted then it is failing to measure current consumption correctly!")
                WriteTextLog("WARNING: You could try calibrating the module: https://www.youtube.com/watch?v=tEA0Or-1n18")
            End If

        Else
            WriteTextLog("*** Enable CURR logging to view battery and flight efficiency data.")
            WriteTextLog("")
        End If

        'Only display this if IMU logging has been performed.
        If IMU_Logging = True Then
            If IMU_Vibration_Check = True Then
                'Calcualte the Mean and Standard Deviation on the recorded vibration logs.

                WriteTextLog("Full Vibration Summary (exceptions filtered):")
                WriteTextLog("          AccX      AccY      AccZ      Spd       Alt")
                WriteTextLog(FormatTextLogValuesVibration("Max", Log_IMU_Max_AccX, Log_IMU_Max_AccY, Log_IMU_Max_AccZ, log_IMU_Max_Spd, log_IMU_Max_Alt))
                WriteTextLog(FormatTextLogValuesVibration("Min", Log_IMU_Min_AccX, Log_IMU_Min_AccY, Log_IMU_Min_AccZ, log_IMU_Min_Spd, log_IMU_Min_Alt))
                ' CODE REMOVED KXG 08/06/2014
                ' This code that works out the averages is obviously incorrect
                ' WriteTextLog(FormatTextLogValuesVibration("Avg", Log_IMU_Sum_AccX / Log_IMU_DLs_for_Slow_FLight, Log_IMU_Sum_AccY / Log_IMU_DLs_for_Slow_FLight, Log_IMU_Sum_AccZ / Log_IMU_DLs_for_Slow_FLight, log_IMU_Sum_Spd / Log_IMU_DLs_for_Slow_FLight, log_IMU_Sum_Alt / Log_IMU_DLs_for_Slow_FLight))

                ' WriteTextLog(FormatTextLogValuesVibration("StDev", StandardDeviation(IMU_Vibration_AccX), StandardDeviation(IMU_Vibration_AccY), StandardDeviation(IMU_Vibration_AccZ), 0, 0))
                WriteTextLog("Datalines: " & IMU_Vibration_Start_DL & " ~ " & IMU_Vibration_End_DL)
                'Check that vibrations recorded are within the limits set.
                If Log_IMU_Min_AccX < -3 Or Log_IMU_Max_AccX > 3 _
                    Or Log_IMU_Min_AccY < -3 Or Log_IMU_Max_AccY > 3 _
                    Or Log_IMU_Min_AccZ < -15 Or Log_IMU_Max_AccZ > -5 Then
                    If Log_IMU_Min_AccX < -5 Or Log_IMU_Max_AccX > 5 _
                    Or Log_IMU_Min_AccY < -5 Or Log_IMU_Max_AccY > 5 _
                    Or Log_IMU_Min_AccZ < -20 Or Log_IMU_Max_AccZ > 0 Then
                        WriteTextLog("")
                        WriteTextLog("WARNING: HIGH Levels of Vibration Detected, recommended not to fly!")
                        WriteTextLog("WARNING: See the Vibration Section on this web link:")
                        WriteTextLog("         http://copter.ardupilot.com/wiki/common-diagnosing-problems-using-logs/")
                    Else
                        WriteTextLog("")
                        WriteTextLog("WARNING: Level of vibrations are above recommended values.")
                        WriteTextLog("WARNING: See the Vibration Section on this web link:")
                        WriteTextLog("         http://copter.ardupilot.com/wiki/common-diagnosing-problems-using-logs/")
                    End If
                Else
                    WriteTextLog("")
                    WriteTextLog("Excellent: Levels of vibration detected are within recommended limits.")
                End If
            Else
                WriteTextLog("*** Limited IMU data for vibration analysis")
                WriteTextLog("    For accurate vibration analysis ensure UAV is flying slowly above 3 meters for at least 10 seconds.")
                WriteTextLog("")
            End If
            WriteTextLog("")
        Else
            WriteTextLog("*** Enable IMU logging to view vibration results.")
            WriteTextLog("")
        End If
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

        'frmMainForm.btnPowerChart.Visible = Not OnOff
        'frmMainForm.btnVibrationChart.Visible = Not OnOff
        'frmMainForm.lblWorkInProgress.Visible = Not OnOff
    End Sub

    Public Sub ButtonsCharting_Visible(OnOff As Boolean)
        frmMainForm.panAnalysisButtons.Visible = Not OnOff

        'Graph Button Handling.
        frmMainForm.panGraphButtons.Visible = OnOff
        'Enable each button if data is available.
        If OnOff = True Then 'only valid if we are displaying graph buttons.
            If IMU_Logging = True Then frmMainForm.btnVibrationChart.Enabled = True Else frmMainForm.btnVibrationChart.Enabled = False
            If CURR_Logging = True Then frmMainForm.btnPowerChart.Enabled = True Else frmMainForm.btnPowerChart.Enabled = False
            If GPS_Logging = True Then frmMainForm.btnGPSChart.enabled = True Else frmMainForm.btnGPSChart.Enabled = False
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
