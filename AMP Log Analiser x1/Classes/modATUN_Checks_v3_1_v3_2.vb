Module modATUN_Checks_v3_1_v3_2

    Public Sub ATUN_Checks_v3_1_v3_2()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE HAS BEEN VALIDATED AS THE CORRECT TYPE !!!

        If ReadFileResilienceCheck(7) = True Then
            ' v3.2.1   FMT, 25, 25, ATUN, BBfffff, Axis,TuneStep,RateMin,RateMax,RPGain,RDGain,SPGain
            Log_ATUN_Axis = Val(DataArray(1))
            Log_ATUN_TuneStep = Val(DataArray(2))
            Log_ATUN_RP = Val(DataArray(5))
            Log_ATUN_RD = Val(DataArray(6))
            Log_ATUN_SP = Val(DataArray(7))
            Call ATUN_MainAnalysis()
        End If


    End Sub

End Module
