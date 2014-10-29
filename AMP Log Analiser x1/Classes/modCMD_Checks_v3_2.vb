Module modCMD_Checks_v3_2
    Public Sub CMD_Checks_v3_2()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE HAS BEEN VALIDATED AS THE CORRECT TYPE !!!

        If ReadFileResilienceCheck(11) = True Then
            'FMT, 145, 41, CMD, IHHHfffffff, TimeMS,CTot,CNum,CId,Prm1,Prm2,Prm3,Prm4,Lat,Lng,Alt
            Log_CMD_CTot = Val(DataArray(2)) - 1
            Log_CMD_CNum = Val(DataArray(3))
            Log_CMD_CId = Val(DataArray(4))
            Log_CMD_Copt = 0                    'This value may not be available.
            Log_CMD_Prm1 = Val(DataArray(5))
            Log_CMD_Alt = Val(DataArray(11))
            Log_CMD_Lat = Val(DataArray(9))
            Log_CMD_Lng = Val(DataArray(10))
        End If

        Call CMD_Checks_MainAnalysis()

    End Sub
End Module
