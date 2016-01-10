Module modCMD_Checks_v3_1
    Public Sub CMD_Checks_v3_1()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE HAS BEEN VALIDATED AS THE CORRECT TYPE !!!

        If ReadFileResilienceCheck(8) = True Then
            'FMT, 8, 20, CMD, BBBBBeLL, CTot,CNum,CId,COpt,Prm1,Alt,Lat,Lng
            Log_CMD_CTot = Val(DataArray(1))  '- 1
            Log_CMD_CNum = Val(DataArray(2)) + 1
            Log_CMD_CId = Val(DataArray(3))
            Log_CMD_Copt = Val(DataArray(4))
            Log_CMD_Prm1 = Val(DataArray(5))
            Log_CMD_Alt = Val(DataArray(6))
            Log_CMD_Lat = Val(DataArray(7))
            Log_CMD_Lng = Val(DataArray(8))
            Call CMD_Checks_MainAnalysis()
        End If

    End Sub
End Module
