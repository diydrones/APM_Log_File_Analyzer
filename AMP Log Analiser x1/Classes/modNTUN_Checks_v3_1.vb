Module modNTUN_Checks_v3_1
    Public Sub NTUN_Checks_v3_1()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE HAS BEEN VALIDATED AS THE CORRECT TYPE !!!

        If ReadFileResilienceCheck(12) = True Then
            'FMT, 5, 49, NTUN, Ecffffffffee, WPDst,WPBrg,PErX,PErY,DVelX,DVelY,VelX,VelY,DAcX,DAcY,DRol,DPit
            'Debug.Print("NTUN Data Detected...")
            Log_NTUN_WPDst = Val(DataArray(1))
            Log_NTUN_WPBrg = Val(DataArray(2))
            Log_NTUN_PErX = Val(DataArray(3))
            Log_NTUN_PErY = Val(DataArray(4))
            Log_NTUN_DVelX = Val(DataArray(5))
            Log_NTUN_DVelY = Val(DataArray(6))
            Log_NTUN_VelX = Val(DataArray(7))
            Log_NTUN_VelY = Val(DataArray(8))
            Log_NTUN_DAcX = Val(DataArray(9))
            Log_NTUN_DAcY = Val(DataArray(10))
            Log_NTUN_DRol = Val(DataArray(11))
            Log_NTUN_DPit = Val(DataArray(12))
        End If

        Call NTUN_MainAnalysis()

    End Sub
End Module
