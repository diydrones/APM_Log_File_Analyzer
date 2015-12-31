Module modIMU_MainAnalysis

    Public Sub IMU_MainAnalysis()

        Dim TempAlt As Single = 0
        If CTUN_Logging = True Then TempAlt = Log_CTUN_BarAlt Else TempAlt = Log_GPS_Calculated_Alt

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

    End Sub
End Module
