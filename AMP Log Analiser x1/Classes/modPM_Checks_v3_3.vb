Module modPM_Checks_v3_3
    Public Sub PM_Checks_v3_3()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE HAS BEEN VALIDATED AS THE CORRECT TYPE !!!

        If ReadFileResilienceCheck(7) = True Then
            'Solo v1.2.0 - FMT, 6, 16, PM, HHIhBH, NLon,NLoop,MaxT,PMT,I2CErr,INSErr
            'v3.1        - FMT, 6, 19, PM, BBHHIhBHB, RenCnt, RenBlw, NLon, NLoop, MaxT, Pmt, I2CErr, INSErr, INAVErr
            'v3.2        - FMT, 6, 17, PM, HHIhBHB, NLon, NLoop, MaxT, Pmt, I2CErr, INSErr, INAVErr
            'v3.3.2      - FMT, 6, 24, PM, QHHIhBH, TimeUS,NLon,NLoop,MaxT,PMT,I2CErr,INSErr
            Log_PM_RenCnt = 0
            Log_PM_RenBlw = 0
            Log_PM_NLon = Val(DataArray(2))
            Log_PM_NLoop = Val(DataArray(3))
            Log_PM_MaxT = Val(DataArray(4))
            Log_PM_PMT = Val(DataArray(5))
            Log_PM_I2CErr = Val(DataArray(6))
            Log_PM_INSErr = Val(DataArray(7))
            'Debug.Print("Processing:- " & Str(DataLine) & ": " & Data)
            PM_Delay_Counter = PM_Delay_Counter + 1
            Call PM_MainAnalysis()
        End If

    End Sub
End Module
