Module modCTUN_Checks_v3_3
    Public Sub CTUN_Checks_v3_3()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        If ReadFileResilienceCheck(11) = True Then
            'v3.1 - FMT, 4, 25, CTUN, hcefchhhh, ThrIn,SonAlt,BarAlt,WPAlt,DesSonAlt,AngBst,CRate,ThrOut,DCRate
            'v3.2 - FMT, 4, 33, CTUN, Ihhhffecchh, TimeMS, ThrIn, AngBst, ThrOut, DAlt, Alt, BarAlt, DSAlt, SAlt, DCRt, CRt
            'v3.3 - FMT, 4, 39, CTUN, Qhhfffecchh, TimeUS, ThrIn, AngBst, ThrOut, DAlt, Alt, BarAlt, DSAlt, SAlt, DCRt, CRt
            Log_CTUN_ThrIn = Val(DataArray(2))
            Log_CTUN_SonAlt = Val(DataArray(9))
            Log_CTUN_BarAlt = Val(DataArray(7))
            Log_CTUN_Alt = Val(DataArray(6))
            Log_CTUN_WPAlt = Val(DataArray(5))       ' CHECK - This may not be correct !!!
            Log_CTUN_DesSonAlt = Val(DataArray(9))   ' CHECK - This may not be correct !!!
            Log_CTUN_AngBst = Val(DataArray(3))
            Log_CTUN_CRate = Val(DataArray(11))
            Log_CTUN_ThrOut = Val(DataArray(4))
            Log_CTUN_DCRate = Val(DataArray(10))
            Call CTUN_MainAnalysis()
        End If

    End Sub
End Module
