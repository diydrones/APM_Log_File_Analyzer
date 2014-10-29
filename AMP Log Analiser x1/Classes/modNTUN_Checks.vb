Module modNTUN_Checks
    Public Sub NTUN_Checks()
        If DataArray(0) = "NTUN" Then
            'An NTUN value should have only 12 pieces of numeric data - V3.1!
            'FMT, 5, 49, NTUN, Ecffffffffee, WPDst,WPBrg,PErX,PErY,DVelX,DVelY,VelX,VelY,DAcX,DAcY,DRol,DPit
            If VersionCompare(ArduVersion, "3.1.5") = True Then
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
            Else
                'Assume new version, 11 pieces of numeric data - v3.2!
                'FMT, 5, 47, NTUN, Iffffffffff, TimeMS,DPosX,DPosY,PosX,PosY,DVelX,DVelY,VelX,VelY,DAccX,DAccY
                If ReadFileResilienceCheck(11) = True Then
                    'Debug.Print("NTUN Data Detected...")

                    '############################
                    '### V3.2 totally changed ###
                    '############################

                End If
            End If
        End If
    End Sub
End Module
