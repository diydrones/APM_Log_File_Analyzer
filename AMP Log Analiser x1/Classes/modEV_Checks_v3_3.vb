﻿Module modEV_Checks_v3_3
    Public Sub EV_Checks_v3_3()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        'An EV value should have only 2 pieces of numeric data!
        If ReadFileResilienceCheck(2) = True Then
            ' v3.1 - FMT, 13, 4, EV, B, Id  - Guessing
            ' v3.2 - FMT, 13, 4, EV, B, Id
            ' v3.3 - FMT, 13, 12, EV, QB, TimeUS,Id

            Log_EV_ID = DataArray(2)
            Call EV_MainAnalysis()

        End If

    End Sub
End Module
