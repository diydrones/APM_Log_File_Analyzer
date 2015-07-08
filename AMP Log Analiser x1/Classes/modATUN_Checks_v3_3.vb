Module modATUN_Checks_v3_3

    Public Sub ATUN_Checks_v3_3()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE HAS BEEN VALIDATED AS THE CORRECT TYPE !!!

        If ReadFileResilienceCheck(9) = True Then
            ' v3.3-rc3 FMT, 25, 33, ATUN, IBBffffff, TimeMS,Axis,TuneStep,RateTarg,RateMin,RateMax,RP,RD,SP
            Log_ATUN_Axis = Val(DataArray(2))
            Log_ATUN_TuneStep = Val(DataArray(3))
            Log_ATUN_RP = Val(DataArray(7))
            Log_ATUN_RD = Val(DataArray(8))
            Log_ATUN_SP = Val(DataArray(9))
            Call ATUN_MainAnalysis()
        End If


    End Sub

End Module
