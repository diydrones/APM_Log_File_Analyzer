Module modATT_MainAnalysis
    Public Sub ATT_MainAnalysis()

        ' This part only checks the already preset Variables for the data line.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE VARIABLES HAVE BEEN SET !!!

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
    End Sub
End Module
