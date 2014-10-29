Module modCTUN_Checks_v3_1
    Public Sub CTUN_Checks_v3_1()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE HAS BEEN VALIDATED AS THE CORRECT TYPE !!!

        If ReadFileResilienceCheck(9) = True Then
            'FMT, 4, 25, CTUN, hcefchhhh, ThrIn,SonAlt,BarAlt,WPAlt,DesSonAlt,AngBst,CRate,ThrOut,DCRate
            Log_CTUN_ThrIn = Val(DataArray(1))
            Log_CTUN_SonAlt = Val(DataArray(2))
            Log_CTUN_BarAlt = Val(DataArray(3))
            Log_CTUN_WPAlt = Val(DataArray(4))
            Log_CTUN_DesSonAlt = Val(DataArray(5))
            Log_CTUN_AngBst = Val(DataArray(6))
            Log_CTUN_CRate = Val(DataArray(7))
            Log_CTUN_ThrOut = Val(DataArray(8))
            Log_CTUN_DCRate = Val(DataArray(9))
        End If

        Call CTUN_MainAnalysis()

    End Sub
End Module
