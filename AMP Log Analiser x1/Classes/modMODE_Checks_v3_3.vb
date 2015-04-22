Module modMODE_Checks_v3_3

    Sub MODE_Checks_v3_3()
        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE HAS BEEN VALIDATED AS THE CORRECT TYPE !!!

        ' ReadFileResilienceCheck(3) Can not be used here because the value is not numeric

        Log_Current_Mode = DataArray(2)

        Call MODE_MainAnalysis()

    End Sub


End Module
