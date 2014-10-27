Module modATT_Checks
    Public Sub ATT_Checks()
        If DataArray(0) = "ATT" Then
            'An ATT value should have only 7 pieces of numeric data!
            If ReadFileResilienceCheck(7) = True Then
                'Debug.Print("ATT Data Detected...")
                Log_ATT_RollIn = Val(DataArray(1))
                Log_ATT_Roll = Val(DataArray(2))
                Log_ATT_PitchIn = Val(DataArray(3))
                Log_ATT_Pitch = Val(DataArray(4))
                Log_ATT_YawIn = Val(DataArray(5))
                Log_ATT_Yaw = Val(DataArray(6))
                Log_ATT_NavYaw = Val(DataArray(7))

                'Track to ensure Roll is following RollIn and Pitch is following PitchIn.
                If Log_In_Flight = True Then
                    If Log_ATT_RollIn <> 0 And Log_ATT_RollIn - Log_ATT_Roll > TempMaxRollDiff Then TempMaxRollDiff = Log_ATT_RollIn - Log_ATT_Roll
                    If Log_ATT_PitchIn <> 0 And Log_ATT_PitchIn - Log_ATT_Pitch > TempMaxPitchDiff Then TempMaxPitchDiff = Log_ATT_PitchIn - Log_ATT_Pitch
                    'Debug.Print("Log_In_Flight = " & Log_In_Flight)
                    'Debug.Print("Diff RollIn and Roll = " & Format(Log_ATT_RollIn - Log_ATT_Roll, "000000") & " Max = " & TempMaxRollDiff _
                    '& " : PicthIn and Pitch = " & Format(Log_ATT_PitchIn - Log_ATT_Pitch, "000000") _
                    '& " Max = " & TempMaxPitchDiff)
                End If

                ' Update the Attitude Chart
                frmMainForm.chartAttitude.Series("RollIn").Points.AddY(Log_ATT_RollIn)
                frmMainForm.chartAttitude.Series("Roll").Points.AddY(Log_ATT_Roll)
                frmMainForm.chartAttitude.Series("NavRoll").Points.AddY(Log_NTUN_DRol)
                frmMainForm.chartAttitude.Series("PitchIn").Points.AddY(Log_ATT_PitchIn)
                frmMainForm.chartAttitude.Series("Pitch").Points.AddY(Log_ATT_Pitch)
                frmMainForm.chartAttitude.Series("NavPitch").Points.AddY(Log_NTUN_DPit)
                frmMainForm.chartAttitude.Series("Speed").Points.AddY(Log_GPS_Spd)
                frmMainForm.chartAttitude.Series("YawIn").Points.AddY(Log_ATT_YawIn)
                frmMainForm.chartAttitude.Series("Yaw").Points.AddY(Log_ATT_Yaw)
                frmMainForm.chartAttitude.Series("NavYaw").Points.AddY(Log_ATT_NavYaw)
                frmMainForm.chartAttitude.Series("Travel").Points.AddY(GPS_Calculated_Direction)
                frmMainForm.chartAttitude.Series("ClimbIn").Points.AddY(Log_CTUN_ThrIn)
                frmMainForm.chartAttitude.Series("NavClimb").Points.AddY(Log_CTUN_CRate)
                frmMainForm.chartAttitude.Series("Altitude").Points.AddY(Log_CTUN_BarAlt * 10)
            End If
        End If

    End Sub
End Module
