Module modPM_Checks_v3_1
    Public Sub PM_Checks_v3_1()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE HAS BEEN VALIDATED AS THE CORRECT TYPE !!!

        If ReadFileResilienceCheck(9) = True Then
            'FMT, 6, 19, PM, BBHHIhBHB, RenCnt, RenBlw, NLon, NLoop, MaxT, Pmt, I2CErr, INSErr, INAVErr
            Log_PM_RenCnt = Val(DataArray(1))
            Log_PM_RenBlw = Val(DataArray(2))
            Log_PM_NLon = Val(DataArray(3))
            Log_PM_NLoop = Val(DataArray(4))
            Log_PM_MaxT = Val(DataArray(5))
            Log_PM_PMT = Val(DataArray(6))
            Log_PM_I2CErr = Val(DataArray(7))
            Log_PM_INSErr = Val(DataArray(8))
            Log_PM_INAVErr = Val(DataArray(9))
            'Debug.Print("Processing:- " & Str(DataLine) & ": " & Data)
            PM_Delay_Counter = PM_Delay_Counter + 1
            Call PM_MainAnalysis()
        End If

    End Sub
End Module
