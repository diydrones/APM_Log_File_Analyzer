Module modIMU_Checks

    Public Sub IMU_MainAnalysis()

        If frmMainForm.chkboxVibrations.Checked = True Then
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
            If Log_GPS_Spd < intVibrationSpeed And Log_In_Flight = frmMainForm.chkboxVibrationInFlight.Checked And TempAlt > intVibrationAltitude Then
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


                '### NEW CODE to send data directly to chart ####
                frmMainForm.chartVibrations.Series("AccX").Points.AddY(Log_IMU_AccX)
                frmMainForm.chartVibrations.Series("AccY").Points.AddY(Log_IMU_AccY)
                frmMainForm.chartVibrations.Series("AccZ").Points.AddY(Log_IMU_AccZ)
                frmMainForm.chartVibrations.Series("Altitude").Points.AddY(TempAlt) 'Log_CTUN_BarAlt
                frmMainForm.chartVibrations.Series("Speed").Points.AddY(Log_GPS_Spd)

                '    'Add the Marker Lines
                frmMainForm.chartVibrations.Series("XYHighLine").Points.AddY(3)
                frmMainForm.chartVibrations.Series("XYLowLine").Points.AddY(-3)
                frmMainForm.chartVibrations.Series("ZHighLine").Points.AddY(-5)
                frmMainForm.chartVibrations.Series("ZLowLine").Points.AddY(-15)

                Log_IMU_DLs_for_Slow_FLight = Log_IMU_DLs_for_Slow_FLight + 1
                IMU_Vibration_End_DL = DataLine


                IMU_Vibration_Check = True
            Else

                '### NEW CODE to send data directly to chart ####
                frmMainForm.chartVibrations.Series("AccX").Points.AddY(0)
                frmMainForm.chartVibrations.Series("AccY").Points.AddY(0)
                frmMainForm.chartVibrations.Series("AccZ").Points.AddY(0)
                frmMainForm.chartVibrations.Series("Altitude").Points.AddY(TempAlt) 'Log_CTUN_BarAlt
                frmMainForm.chartVibrations.Series("Speed").Points.AddY(Log_GPS_Spd)

                '    'Add the Marker Lines
                frmMainForm.chartVibrations.Series("XYHighLine").Points.AddY(3)
                frmMainForm.chartVibrations.Series("XYLowLine").Points.AddY(-3)
                frmMainForm.chartVibrations.Series("ZHighLine").Points.AddY(-5)
                frmMainForm.chartVibrations.Series("ZLowLine").Points.AddY(-15)

                Log_IMU_DLs_for_Slow_FLight = Log_IMU_DLs_for_Slow_FLight + 1
                IMU_Vibration_End_DL = DataLine

            End If
        End If

    End Sub
End Module
