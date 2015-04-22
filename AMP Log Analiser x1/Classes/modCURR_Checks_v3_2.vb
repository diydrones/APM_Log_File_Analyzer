Module modCURR_Checks_v3_2

    Public Sub CURR_Checks_v3_2()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE HAS BEEN VALIDATED AS THE CORRECT TYPE !!!

        If ReadFileResilienceCheck(7) = True Then
            'FMT, 9, 23, CURR, IhIhhhf, TimeMS,ThrOut,ThrInt,Volt,Curr,Vcc,CurrTot
            LOG_CURR_ThrOut = Val(DataArray(2))
            LOG_CURR_ThrInt = Val(DataArray(3))
            LOG_CURR_Volt = Val(DataArray(4))
            LOG_CURR_Curr = Val(DataArray(5))
            LOG_CURR_Vcc = Val(DataArray(6))
            LOG_CURR_CurrTot = Val(DataArray(7))
            Call CURR_MainAnalysis()
        End If

    End Sub
End Module
