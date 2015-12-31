Module modRCOU_MainAnalysis

    Public Sub RCOUT_MainAnalysis()

        '#############################################
        ' Can this code cover all versions of APM:Coter firmware ?
        '###############################################

        Dim Ch1 As Integer = DataArray(2)
        Dim Ch2 As Integer = DataArray(3)
        Dim Ch3 As Integer = DataArray(4)
        Dim Ch4 As Integer = DataArray(5)
        Dim Ch5 As Integer = DataArray(6)
        Dim Ch6 As Integer = DataArray(7)
        Dim Ch7 As Integer = DataArray(8)
        Dim Ch8 As Integer = DataArray(9)
        Dim Ch9 As Integer = DataArray(10)
        Dim Ch10 As Integer = DataArray(11)
        Dim Ch11 As Integer = DataArray(12)
        Dim Ch12 As Integer = DataArray(13)

        Dim TempAlt As Single = 0
        If CTUN_Logging = True Then TempAlt = Log_CTUN_BarAlt Else TempAlt = Log_GPS_Calculated_Alt

        'All we need to do is populate the RCOU Graph 
        frmMainForm.chartRCOU.Series("RC1").Points.AddY(Ch1)
        frmMainForm.chartRCOU.Series("RC2").Points.AddY(Ch2)
        frmMainForm.chartRCOU.Series("RC3").Points.AddY(Ch3)
        frmMainForm.chartRCOU.Series("RC4").Points.AddY(Ch4)
        frmMainForm.chartRCOU.Series("RC5").Points.AddY(Ch5)
        frmMainForm.chartRCOU.Series("RC6").Points.AddY(Ch6)
        frmMainForm.chartRCOU.Series("RC7").Points.AddY(Ch7)
        frmMainForm.chartRCOU.Series("RC8").Points.AddY(Ch8)
        frmMainForm.chartRCOU.Series("RC9").Points.AddY(Ch9)
        frmMainForm.chartRCOU.Series("RC10").Points.AddY(Ch10)
        frmMainForm.chartRCOU.Series("RC11").Points.AddY(Ch11)
        frmMainForm.chartRCOU.Series("RC12").Points.AddY(Ch12)

        frmMainForm.chartRCOU.Series("ClimbRate").Points.AddY(Log_CTUN_CRate)
        frmMainForm.chartRCOU.Series("DesiredClimbRate").Points.AddY(Log_CTUN_DCRate)

        frmMainForm.chartRCOU.Series("Speed").Points.AddY(Log_GPS_Spd)
        frmMainForm.chartRCOU.Series("Altitude").Points.AddY(TempAlt)
        frmMainForm.chartRCOU.Series("AltitudeSmoothed").Points.AddY(Log_CTUN_Alt)

    End Sub

End Module
