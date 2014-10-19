Module modNTUN_Checks
    Public Sub NTUN_Checks()
        If DataArray(0) = "NTUN" Then
            'An NTUN value should have only 12 pieces of numeric data!
            If ReadFileResilienceCheck(12) = True Then
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
        End If

    End Sub
End Module
