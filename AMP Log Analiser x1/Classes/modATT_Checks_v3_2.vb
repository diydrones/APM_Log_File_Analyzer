Module modATT_Checks_v3_2
    Public Sub ATT_Checks_v3_2()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE HAS BEEN VALIDATED AS THE CORRECT TYPE !!!

        If ReadFileResilienceCheck(9) = True Then
            'v3.1 - FMT, 1, 17, ATT, cccccCC, RollIn,Roll,PitchIn,Pitch,YawIn,Yaw,NavYaw
            'v3.2RC12 - FMT, 1, 19, ATT, IccccCC, TimeMS,DesRoll,Roll,DesPitch,Pitch,DesYaw,Yaw
            'V3.2RC13 - FMT, 1, 23, ATT, IccccCCCC, TimeMS,DesRoll,Roll,DesPitch,Pitch,DesYaw,Yaw,ErrRP,ErrYaw
            Log_ATT_RollIn = Val(DataArray(2))
            Log_ATT_Roll = Val(DataArray(3))
            Log_ATT_PitchIn = Val(DataArray(4))
            Log_ATT_Pitch = Val(DataArray(5))
            Log_ATT_YawIn = Val(DataArray(6))
            Log_ATT_Yaw = Val(DataArray(7))
            Log_ATT_NavYaw = Val(DataArray(9))                          'Value is not available in v3.2RC<13 ATT Dataline
        End If

        Call ATT_MainAnalysis()

    End Sub
End Module
