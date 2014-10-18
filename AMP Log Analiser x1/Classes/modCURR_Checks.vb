Module modCURR_Checks

    Public Sub CURR_Checks()
        If DataArray(0) = "CURR" Then
            'An CURR value should have only 6 pieces of numeric data!
            If ReadFileResilienceCheck(6) = True Then
                Value1 = Val(DataArray(1))
                Value3 = Val(DataArray(3))
                Value4 = Val(DataArray(4))
                Value5 = Val(DataArray(5))
                Value6 = Val(DataArray(6))

                If Value1 > 0 Then CURR_ThrottleUp = True Else CURR_ThrottleUp = False
                Log_Total_Current = Value6

                If Log_In_Flight = True Then
                    If Value3 < Log_Min_Battery_Volts Then Log_Min_Battery_Volts = Value3
                    If Value3 > Log_Max_Battery_Volts Then Log_Max_Battery_Volts = Value3
                    If Value4 < Log_Min_Battery_Current And Value4 <> "0" Then Log_Min_Battery_Current = Value4
                    If Value4 > Log_Max_Battery_Current Then Log_Max_Battery_Current = Value4
                    Log_Sum_Battery_Current = Log_Sum_Battery_Current + Value4
                    Total_Mode_Current = Log_Total_Current - Log_Mode_Start_Current
                    If Value5 < Log_Min_VCC Then Log_Min_VCC = Value5
                    If Value5 > Log_Max_VCC Then Log_Max_VCC = Value5
                    'Collect the Mode Summary Data.
                    Log_CURR_DLs = Log_CURR_DLs + 1                       'Add a CURR record to the Total CURR record counter.
                    Log_CURR_DLs_for_Mode = Log_CURR_DLs_for_Mode + 1     'Add a CURR record to the CURR record counter for the mode.                    
                    If Value3 < Log_Mode_Min_Volt Then Log_Mode_Min_Volt = Value3
                    If Value3 > Log_Mode_Max_Volt Then Log_Mode_Max_Volt = Value3
                    Log_Mode_Sum_Volt = Log_Mode_Sum_Volt + Value3
                End If

                ' Update the Chart
                frmMainForm.chartPowerRails.Series("Vcc").Points.AddY(Value5 / 1000)
                frmMainForm.chartPowerRails.Series("Volts").Points.AddY(Value3 / 100)
                frmMainForm.chartPowerRails.Series("Amps").Points.AddY(Value4 / 100)
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
        End If

    End Sub
End Module
