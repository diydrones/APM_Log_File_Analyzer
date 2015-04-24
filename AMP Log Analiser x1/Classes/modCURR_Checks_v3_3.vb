Module modCURR_Checks_v3_3

    Public Sub CURR_Checks_v3_3()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE HAS BEEN VALIDATED AS THE CORRECT TYPE !!!

        If ReadFileResilienceCheck(7) = True Then
            'v3.2     FMT,   9, 23, CURR, IhIhhhf, TimeMS, ThrOut,   ThrInt, Volt, Curr, Vcc,     CurrTot
            'v3.3-rc1 FMT, 166, 21, CURR, IhhhHfh, TimeMS, Throttle, Volt,   Curr, Vcc,  CurrTot, Volt2
            LOG_CURR_ThrOut = Val(DataArray(2))
            'LOG_CURR_ThrInt = Val(DataArray(3))
            LOG_CURR_Volt = Val(DataArray(3))
            LOG_CURR_Curr = Val(DataArray(4))
            LOG_CURR_Vcc = Val(DataArray(5))
            LOG_CURR_CurrTot = Val(DataArray(6))
            Call CURR_MainAnalysis()
        End If

    End Sub
End Module
