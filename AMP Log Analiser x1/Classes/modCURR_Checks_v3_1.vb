Module modCURR_Checks_v3_1

    Public Sub CURR_Checks_v3_1()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE HAS BEEN VALIDATED AS THE CORRECT TYPE !!!

        If ReadFileResilienceCheck(6) = True Then
            'FMT, 9, 19, CURR, hIhhhf, ThrOut,ThrInt,Volt,Curr,Vcc,CurrTot
            LOG_CURR_ThrOut = Val(DataArray(1))
            LOG_CURR_ThrInt = Val(DataArray(2))
            LOG_CURR_Volt = Val(DataArray(3))
            LOG_CURR_Curr = Val(DataArray(4))
            LOG_CURR_Vcc = Val(DataArray(5))
            LOG_CURR_CurrTot = Val(DataArray(6))
            Call CURR_MainAnalysis()
        End If

    End Sub
End Module
