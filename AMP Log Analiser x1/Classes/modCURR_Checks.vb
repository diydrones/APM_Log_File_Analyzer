Module modCURR_Checks

    Public Sub CURR_Checks()
        If DataArray(0) = "CURR" Then
            If VersionCompare(ArduVersion, "3.1.5") = True Then
                'An CURR value should have only 6 pieces of numeric data!
                'FMT, 9, 19, CURR, hIhhhf, ThrOut,ThrInt,Volt,Curr,Vcc,CurrTot
                If ReadFileResilienceCheck(6) = True Then
                    LOG_CURR_ThrOut = Val(DataArray(1))
                    LOG_CURR_ThrInt = Val(DataArray(2))
                    LOG_CURR_Volt = Val(DataArray(3))
                    LOG_CURR_Curr = Val(DataArray(4))
                    LOG_CURR_Vcc = Val(DataArray(5))
                    LOG_CURR_CurrTot = Val(DataArray(6))
                End If
            Else
                'Assume new version, v3.2
                'FMT, 9, 23, CURR, IhIhhhf, TimeMS,ThrOut,ThrInt,Volt,Curr,Vcc,CurrTot
                If ReadFileResilienceCheck(7) = True Then
                    LOG_CURR_ThrOut = Val(DataArray(2))
                    LOG_CURR_ThrInt = Val(DataArray(3))
                    LOG_CURR_Volt = Val(DataArray(4))
                    LOG_CURR_Curr = Val(DataArray(5))
                    LOG_CURR_Vcc = Val(DataArray(6))
                    LOG_CURR_CurrTot = Val(DataArray(7))
                End If
            End If


            If LOG_CURR_ThrOut > 0 Then CURR_ThrottleUp = True Else CURR_ThrottleUp = False
            Log_Total_Current = LOG_CURR_CurrTot

            If Log_In_Flight = True Then
                If LOG_CURR_Volt < Log_Min_Battery_Volts Then Log_Min_Battery_Volts = LOG_CURR_Volt
                If LOG_CURR_Volt > Log_Max_Battery_Volts Then Log_Max_Battery_Volts = LOG_CURR_Volt
                If LOG_CURR_Curr < Log_Min_Battery_Current And LOG_CURR_Curr <> "0" Then Log_Min_Battery_Current = LOG_CURR_Curr
                If LOG_CURR_Curr > Log_Max_Battery_Current Then Log_Max_Battery_Current = LOG_CURR_Curr
                Log_Sum_Battery_Current = Log_Sum_Battery_Current + LOG_CURR_Curr
                Total_Mode_Current = Log_Total_Current - Log_Mode_Start_Current
                If LOG_CURR_Vcc < Log_Min_VCC Then Log_Min_VCC = LOG_CURR_Vcc
                If LOG_CURR_Vcc > Log_Max_VCC Then Log_Max_VCC = LOG_CURR_Vcc
                'Collect the Mode Summary Data.
                Log_CURR_DLs = Log_CURR_DLs + 1                       'Add a CURR record to the Total CURR record counter.
                Log_CURR_DLs_for_Mode = Log_CURR_DLs_for_Mode + 1     'Add a CURR record to the CURR record counter for the mode.                    
                If LOG_CURR_Volt < Log_Mode_Min_Volt Then Log_Mode_Min_Volt = LOG_CURR_Volt
                If LOG_CURR_Volt > Log_Mode_Max_Volt Then Log_Mode_Max_Volt = LOG_CURR_Volt
                Log_Mode_Sum_Volt = Log_Mode_Sum_Volt + LOG_CURR_Volt
            End If

            ' Update the Chart
            frmMainForm.chartPowerRails.Series("Vcc").Points.AddY(LOG_CURR_Vcc / 1000)
            frmMainForm.chartPowerRails.Series("Volts").Points.AddY(LOG_CURR_Volt / 100)
            frmMainForm.chartPowerRails.Series("Amps").Points.AddY(LOG_CURR_Curr / 100)
            frmMainForm.chartPowerRails.Series("Thrust").Points.AddY(Log_CTUN_ThrOut)

            ' Add the min / max lines
            frmMainForm.chartPowerRails.Series("VccLowLine").Points.AddY(4.5)
            frmMainForm.chartPowerRails.Series("VccHighLine").Points.AddY(5.5)
            frmMainForm.chartPowerRails.Series("VccOSDLine").Points.AddY(5.25)



            'Debug.Print(vbNewLine)
            'Debug.Print(vbNewLine)
            'Debug.Print("Debug CURR Data Variables:-")
            'Debug.Print("Data Line" & Str(DataLine))
            'Debug.Print("CURR_ThrottleUp = " & CURR_ThrottleUp)
            'Debug.Print("Log_Min_Battery_Volts = " & Log_Min_Battery_Volts)
            'Debug.Print("Log_Max_Battery_Volts = " & Log_Max_Battery_Volts)
            'Debug.Print("Log_Max_Battery_Current = " & Log_Max_Battery_Current)
            'Debug.Print("Log_Min_VCC = " & Log_Min_VCC)
            'Debug.Print("Log_Max_VCC = " & Log_Max_VCC)
            'Debug.Print(vbNewLine)
            'Debug.Print(vbNewLine) '### Debug Code Here ###

        End If


    End Sub
End Module
