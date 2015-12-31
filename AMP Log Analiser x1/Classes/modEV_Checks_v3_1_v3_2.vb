Module modEV_Checks_v3_1_v3_2
    Public Sub EV_Checks_v3_1_v3_2()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        'An EV value should have only 2 pieces of numeric data!
        If ReadFileResilienceCheck(1) = True Then
            ' v3.1 - FMT, 13, 4, EV, B, Id  - Guessing
            ' v3.2 - FMT, 13, 4, EV, B, Id

            Log_EV_ID = DataArray(1)
            Call EV_MainAnalysis()

        End If

    End Sub
End Module
