Module modIMU_Checks
    Public Sub IMU_Checks()
        If frmMainForm.chkboxVibrations.Checked = True Then
            If DataArray(0) = "IMU" Then
                'An IMU value should have only 7 pieces of numeric data!
                If ReadFileResilienceCheck(7) = True Then
                    'Debug.Print("IMU Data Detected...")
                    Log_IMU_TimeMS = Val(DataArray(1))
                    Log_IMU_GyrX = Val(DataArray(2))
                    Log_IMU_GyrY = Val(DataArray(3))
                    Log_IMU_GyrZ = Val(DataArray(4))
                    Log_IMU_AccX = Val(DataArray(5))
                    Log_IMU_AccY = Val(DataArray(6))
                    Log_IMU_AccZ = Val(DataArray(7))

                    'Debug.Print("DataArray(7) = " & DataArray(7))

                    'Are we in flight and either hovering or flying very slow above 3 meters?  (Log_GPS_Spd in m/s)
                    Dim TempAlt As Single = 0
                    'set the Vibration variables based on the form Vibration Variables.
                    Dim intVibrationSpeed As Integer
                    Dim intVibrationAltitude As Integer
                    If IsNumeric(frmMainForm.txtVibrationSpeed.Text) = True Then
                        intVibrationSpeed = Val(frmMainForm.txtVibrationSpeed.Text)
                    Else
                        intVibrationSpeed = 1
                    End If
                    If IsNumeric(frmMainForm.txtVibrationAltitude.Text) = True Then
                        intVibrationAltitude = Val(frmMainForm.txtVibrationAltitude.Text)
                    Else
                        intVibrationAltitude = 3
                    End If

                    If CTUN_Logging = True Then TempAlt = Log_CTUN_BarAlt Else TempAlt = Log_GPS_Calculated_Alt
                    If Log_GPS_Spd < intVibrationSpeed And Log_In_Flight = frmMainForm.chkboxVibrationInFlight.Checked And TempAlt > intVibrationAltitude And IMU_Vibration_Check = False Then
                        If IMU_Vibration_Start_DL = 0 Then IMU_Vibration_Start_DL = DataLine

                        'Capture the Min and Max values from the IMU data if we are currently in slow flight, filter by increasing by half only each time.
                        If Log_IMU_AccX < Log_IMU_Min_AccX Then Log_IMU_Min_AccX = Log_IMU_Min_AccX - ((Log_IMU_Min_AccX - Log_IMU_AccX) / 2)
                        If Log_IMU_AccX > Log_IMU_Max_AccX Then Log_IMU_Max_AccX = Log_IMU_Max_AccX + ((Log_IMU_AccX - Log_IMU_Max_AccX) / 2)
                        If Log_IMU_AccY < Log_IMU_Min_AccY Then Log_IMU_Min_AccY = Log_IMU_Min_AccY - ((Log_IMU_Min_AccY - Log_IMU_AccY) / 2)
                        If Log_IMU_AccY > Log_IMU_Max_AccY Then Log_IMU_Max_AccY = Log_IMU_Max_AccY + ((Log_IMU_AccY - Log_IMU_Max_AccY) / 2)
                        If Log_IMU_AccZ < Log_IMU_Min_AccZ Then Log_IMU_Min_AccZ = Log_IMU_Min_AccZ - ((Log_IMU_Min_AccZ - Log_IMU_AccZ) / 2)
                        If Log_IMU_AccZ > Log_IMU_Max_AccZ Then Log_IMU_Max_AccZ = Log_IMU_Max_AccZ + ((Log_IMU_AccZ - Log_IMU_Max_AccZ) / 2)
                        If Log_GPS_Spd < log_IMU_Min_Spd Then log_IMU_Min_Spd = log_IMU_Min_Spd - ((log_IMU_Min_Spd - Log_GPS_Spd) / 2)
                        If Log_GPS_Spd > log_IMU_Max_Spd Then log_IMU_Max_Spd = log_IMU_Max_Spd + ((Log_GPS_Spd - log_IMU_Max_Spd) / 2)
                        If TempAlt < log_IMU_Min_Alt Then log_IMU_Min_Alt = TempAlt
                        If TempAlt > log_IMU_Max_Alt Then log_IMU_Max_Alt = TempAlt

                        'Debug.Print("GPS Spd: " & Log_GPS_Spd & " Max AccX: " & Log_IMU_Max_AccX & " Max AccY: " & Log_IMU_Max_AccY & " Max AccZ: " & Log_IMU_Max_AccZ)
                        'Debug.Print("CTUN Alt: " & Log_CTUN_BarAlt & " Min AccX: " & Log_IMU_Min_AccX & " Min AccY: " & Log_IMU_Min_AccY & " Min AccZ: " & Log_IMU_Min_AccZ)

                        'Collect the data for the slow flight averages.
                        Log_IMU_Sum_AccX = Log_IMU_Sum_AccX + Log_IMU_AccX
                        Log_IMU_Sum_AccY = Log_IMU_Sum_AccY + Log_IMU_AccY
                        Log_IMU_Sum_AccZ = Log_IMU_Sum_AccZ + Log_IMU_AccZ
                        log_IMU_Sum_Spd = log_IMU_Sum_Spd + Log_GPS_Spd
                        log_IMU_Sum_Alt = log_IMU_Sum_Alt + TempAlt

                        'Collect the data in the Array so that we can calculate the Mean and Standard Deviation during the flight summary
                        '(only happes if we collect all 5000 records)
                        If Log_IMU_DLs_for_Slow_FLight < 5000 Then
                            IMU_Vibration_AccX(Log_IMU_DLs_for_Slow_FLight) = Log_IMU_AccX
                            IMU_Vibration_AccY(Log_IMU_DLs_for_Slow_FLight) = Log_IMU_AccY
                            IMU_Vibration_AccZ(Log_IMU_DLs_for_Slow_FLight) = Log_IMU_AccZ
                            IMU_Vibration_Alt(Log_IMU_DLs_for_Slow_FLight) = TempAlt 'Log_CTUN_BarAlt
                            IMU_Vibration_Spd(Log_IMU_DLs_for_Slow_FLight) = Log_GPS_Spd
                            Log_IMU_DLs_for_Slow_FLight = Log_IMU_DLs_for_Slow_FLight + 1
                            IMU_Vibration_End_DL = DataLine
                        End If


                    Else
                        ''We need a mimimum of 5000 successive records otherwise we reset and look for a better sample.
                        If Log_IMU_DLs_for_Slow_FLight < 5000 Then
                            'Record everything as Zero as we are landed or below the specified altitude.
                            IMU_Vibration_AccX(Log_IMU_DLs_for_Slow_FLight) = 0
                            IMU_Vibration_AccY(Log_IMU_DLs_for_Slow_FLight) = 0
                            IMU_Vibration_AccZ(Log_IMU_DLs_for_Slow_FLight) = -10
                            IMU_Vibration_Alt(Log_IMU_DLs_for_Slow_FLight) = TempAlt 'Log_CTUN_BarAlt
                            IMU_Vibration_Spd(Log_IMU_DLs_for_Slow_FLight) = Log_GPS_Spd
                            Log_IMU_DLs_for_Slow_FLight = Log_IMU_DLs_for_Slow_FLight + 1
                            IMU_Vibration_End_DL = DataLine

                            '    Log_IMU_DLs_for_Slow_FLight = 0
                            '    Log_IMU_Sum_AccX = 0
                            '    Log_IMU_Sum_AccY = 0
                            '    Log_IMU_Sum_AccZ = 0
                            '    Log_IMU_Min_AccX = 99
                            '    Log_IMU_Max_AccX = -99
                            '    Log_IMU_Min_AccY = 99
                            '    Log_IMU_Max_AccY = -99
                            '    Log_IMU_Min_AccZ = 99
                            '    Log_IMU_Max_AccZ = -99
                            '    log_IMU_Min_Spd = 99
                            '    log_IMU_Max_Spd = 0
                            '    log_IMU_Sum_Spd = 0
                            '    log_IMU_Min_Alt = 99
                            '    log_IMU_Max_Alt = 0
                            '    log_IMU_Sum_Alt = 0
                            '    IMU_Vibration_Start_DL = 0
                            '    IMU_Vibration_End_DL = 0
                        Else
                            'The last successive readings of IMU data gave us what ween needed to check for vibration, dont collect anymore.
                            IMU_Vibration_Check = True
                        End If
                    End If
                End If
            End If
        End If

    End Sub
End Module
