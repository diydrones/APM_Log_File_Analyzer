Module modATT_Checks_v3_1
    Public Sub ATT_Checks_v3_1()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE HAS BEEN VALIDATED AS THE CORRECT TYPE !!!

        If ReadFileResilienceCheck(7) = True Then
            'v3.1 - FMT, 1, 17, ATT, cccccCC, RollIn,Roll,PitchIn,Pitch,YawIn,Yaw,NavYaw
            Log_ATT_RollIn = Val(DataArray(1))
            Log_ATT_Roll = Val(DataArray(2))
            Log_ATT_PitchIn = Val(DataArray(3))
            Log_ATT_Pitch = Val(DataArray(4))
            Log_ATT_YawIn = Val(DataArray(5))
            Log_ATT_Yaw = Val(DataArray(6))
            Log_ATT_NavYaw = Val(DataArray(7))
            Call ATT_MainAnalysis()
        End If



    End Sub
End Module
