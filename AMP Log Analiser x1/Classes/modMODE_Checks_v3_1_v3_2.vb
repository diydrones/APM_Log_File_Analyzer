Module modMODE_Checks_v3_1_v3_2

    Sub MODE_Checks_v3_1_v3_2()
        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE HAS BEEN VALIDATED AS THE CORRECT TYPE !!!

        ' ReadFileResilienceCheck(3) Can not be used here because the value is not numeric

        ' We must update the screen before we change the mode
        If frmMainForm.chkboxSplitModeLandings.Checked = True Then Call AddModeTime()

        Log_Current_Mode = DataArray(1)

        Call MODE_MainAnalysis()

    End Sub


End Module
