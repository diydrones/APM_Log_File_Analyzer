Module modATUN_MainAnalysis

    Public Sub ATUN_MainAnalysis()
        ' This part only checks the already preset Variables for the data line.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE VARIABLES HAVE BEEN SET !!!

        ' ATUN cantains the data created by AutoTune
        ' The Kay data is :
        '   Axis where 0 = roll, 1 = pitch, 2 = yaw
        '   TuneStep where currently we go from 0 to 4  (currently ignored as its not useful to display mid point settings)
        '   RP which will be saved to parameter Rate P
        '   RD which will be saved to parameter Rate D
        '   SP which will be saved to parameter STB P
        '   NOTE: Rate "I" will be calculated:
        '                       for Roll and Pitch as Rate P * AUTOTUNE_PI_RATIO_FINAL
        '                       for Yaw as Rate P * AUTOTUNE_YAW_PI_RATIO_FINAL

        ' At begining report what the starting values are based on first dataline of axis / step.
        ' Then step through the Axis and detect when it changes
        ' On each axis change, display the final recommended settings for that axis.

        Dim TempAxis As String = ""
        Dim TempIMultiplier As Single = 0

        ' Which Axis were we doing last? Set the variables
        Select Case ATUN_PreviousAxis
            Case 0
                TempAxis = "Roll"
                TempIMultiplier = AUTOTUNE_PI_RATIO_FINAL
            Case 1
                TempAxis = "Pitch"
                TempIMultiplier = AUTOTUNE_PI_RATIO_FINAL
            Case 2
                TempAxis = "Yaw"
                TempIMultiplier = AUTOTUNE_YAW_PI_RATIO_FINAL     ' The Mulitplier is different for Yaw
            Case Else
                TempAxis = "New Setting"
        End Select

        ' Detect if this is the first line of an axis, if it is then display the starting values.
        If ATUN_WriteBasedSettings = False Then
            Call WriteText("$$$Parameter Current " & TempAxis & " Settings:", TempAxis, Log_ATUN_SP, Log_ATUN_RP, Log_ATUN_RD, TempIMultiplier)
            ATUN_WriteBasedSettings = True
        End If

        ' Detect if we are changing Axis if so Display the final values for the last axis.
        If Log_ATUN_Axis <> ATUN_PreviousAxis Then
            ATUN_WriteBasedSettings = False
            Call WriteText("$$$AutoTune Recommended " & TempAxis & " Settings:", TempAxis, ATUN_PreviousSP, ATUN_PreviousRP, ATUN_PreviousRD, TempIMultiplier)
        End If

        ' Remember these values for the next ATUN dataline.
        ATUN_PreviousAxis = Log_ATUN_Axis
        ATUN_PreviousTuneStep = Log_ATUN_TuneStep
        ATUN_PreviousRD = Log_ATUN_RD
        ATUN_PreviousRP = Log_ATUN_RP
        ATUN_PreviousSP = Log_ATUN_SP
    End Sub

    Private Sub WriteText(TempStr, TempAxis, TempSP, TempRP, TempRD, TempIMultiplier)
        ' $$$ forces the WriteTextLog to colour green.
        WriteTextLog(TempStr)

        WriteTextLog("$$$   Stabilize " & TempAxis & " = " & TempSP)
        ' Check the settings are in the recommended ranges
        If TempIMultiplier = AUTOTUNE_PI_RATIO_FINAL Then
            ' Roll or Pitch
            If TempSP < 3 Or TempSP > 12 Then
                WriteTextLog("ERROR: The above data is out of recommended range!")
            End If
        Else
            ' Yaw
            If TempSP < 3 Or TempSP > 6 Then
                WriteTextLog("ERROR: The above data is out of recommended range!")
            End If
        End If

        WriteTextLog("$$$   Rate " & TempAxis & " P = " & TempRP)
        ' Check the settings are in the recommended ranges
        If TempIMultiplier = AUTOTUNE_PI_RATIO_FINAL Then
            ' Roll or Pitch
            If TempRP < 0.08 Or TempRP > 0.2 Then
                WriteTextLog("ERROR: The above data is out of recommended range!")
            End If
        Else
            ' Yaw
            If TempRP < 0.15 Or TempRP > 0.25 Then
                WriteTextLog("ERROR: The above data is out of recommended range!")
            End If
        End If

        WriteTextLog("$$$   Rate " & TempAxis & " I = " & TempRP * TempIMultiplier)  ' The Mulitplier is different for Yaw
        ' Check the settings are in the recommended ranges
        If TempIMultiplier = AUTOTUNE_PI_RATIO_FINAL Then
            ' Roll or Pitch
            If TempRP * TempIMultiplier < 0.01 Or TempRP * TempIMultiplier > 0.5 Then
                WriteTextLog("ERROR: The above data is out of recommended range!")
            End If
        Else
            ' Yaw
            If TempRP * TempIMultiplier < 0.01 Or TempRP * TempIMultiplier > 0.02 Then
                WriteTextLog("ERROR: The above data is out of recommended range!")
            End If
        End If

        If TempIMultiplier = AUTOTUNE_PI_RATIO_FINAL Then    ' Yaw Rate D is always "0.000"
            ' Roll or Pitch
            WriteTextLog("$$$   Rate " & TempAxis & " D = " & TempRD)
            If TempRD < 0.001 Or TempRD > 0.02 Then
                WriteTextLog("ERROR: The above data is out of recommended range!")
            End If
        Else
            ' Yaw
            WriteTextLog("$$$   Rate " & TempAxis & " D = " & PARM_RATE_YAW_D)
            If TempRD < 0 Or TempRD >= 0 Then
                WriteTextLog("$$$   v3.3-rc3: Yaw Rate D is not saved (" & TempRD & ")")
            End If
        End If
    End Sub

End Module
