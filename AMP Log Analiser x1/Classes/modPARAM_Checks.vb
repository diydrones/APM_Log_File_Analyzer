Module modPARAM_Checks
    Public Sub Parameter_Checks()
        If frmMainForm.chkboxParameterWarnings.Checked = True Then
            'A Parameter value should have only 3 pieces of data!
            If DataArray(0) = "PARM" Then
                If IsNumeric(DataArray(2)) = False Or IsNothing(DataArray(3)) = False Then
                    Debug.Print("================================================================")
                    Debug.Print("== File Corruption Detected on Data Line " & DataLine & ", line ignored! ==")
                    Debug.Print("================================================================")
                    ErrorCount = ErrorCount + 1
                    With frmMainForm
                        .lblErrors.Visible = True
                        .lblErrors.Refresh()
                        .lblErrorCountNo.Visible = True
                        .lblErrorCount.Visible = True
                        .lblErrorCountNo.Text = ErrorCount
                        .lblErrorCount.Refresh()
                        .lblErrorCountNo.Refresh()
                    End With
                Else
                    Param = DataArray(1)
                    Value = Val(DataArray(2))
                    'Check that ACRO mode will try to self level.
                    If Param = "ACRO_TRAINER" And Val(Value) <> 2 Then
                        Call WriteParamHeader()
                        WriteTextLog(Param & " = " & Value)
                        WriteTextLog("Warning: UAV will not attempt to self level in ACRO mode!")
                        WriteTextLog("http://ardupilot.com/forum/viewtopic.php?f=25&t=5613")
                        WriteTextLog(vbNewLine)
                    End If

                    'Check that the Throttle Min and Motor Spin Armed are still defaults
                    'and advise that if the Motors do not spin on Arming then increase the
                    'Motor Spin Arm upto a Max of Throttle Min.
                    If Param = "THR_MIN" Or Param = "MOT_SPIN_ARMED" Then
                        If Param = "THR_MIN" Then Thr_Min = Val(Value)
                        If Param = "MOT_SPIN_ARMED" Then Mot_Spin_Armed = Val(Value)
                        If Param = "MOT_SPIN_ARMED" And Val(Value) = 70 Then
                            Call WriteParamHeader()
                            WriteTextLog(Param & " = " & Value)
                            WriteTextLog("Advice: If motors do not spin when armed calibrate ESC and then try increasing")
                            WriteTextLog("the parameter MOT_SPIN_ARMED up from 70 in small increments.")
                            WriteTextLog("http://ardupilot.com/forum/viewtopic.php?f=25&t=5345&sid=3171087bcf7d84e03491e1845eaa9393")
                            WriteTextLog(vbNewLine)
                        End If
                        If Thr_Min <> 0 And Mot_Spin_Armed <> 0 Then
                            If Thr_Min <> 130 Or Mot_Spin_Armed <> 70 Then
                                Call WriteParamHeader()
                                WriteTextLog(Param & " = " & Value)
                                WriteTextLog("Warning: MOT_SPIN_ARMED & THR_MIN parameters have been altered from their defaults.")
                                WriteTextLog("They should be 70 & 130 respectively unless the motors do not spin when armed.")
                                WriteTextLog("http://ardupilot.com/forum/viewtopic.php?f=25&t=5345&sid=3171087bcf7d84e03491e1845eaa9393")
                                WriteTextLog(vbNewLine)
                            End If
                        End If
                        If Thr_Min <> 0 And Mot_Spin_Armed <> 0 Then
                            If Mot_Spin_Armed > Thr_Min Then
                                Call WriteParamHeader()
                                WriteTextLog(Param & " = " & Value)
                                WriteTextLog("Warning: MOT_SPIN_ARMED is greater then THR_MIN parameter, this is dangerous!")
                                WriteTextLog("These parameter should be 70 & 130 respectively unless the motors do not spin when armed.")
                                WriteTextLog("http://ardupilot.com/forum/viewtopic.php?f=25&t=5345&sid=3171087bcf7d84e03491e1845eaa9393")
                                WriteTextLog(vbNewLine)
                            End If
                        End If
                    End If

                    If Param = "BATT_CAPACITY" Then
                        Log_Battery_Capacity = Val(Value)
                        '    If Val(Value) <> BATTERY_CAPACITY Then
                        '        Call WriteParamHeader()
                        '        WriteTextLog(Param & " = " & Value)
                        '        WriteTextLog("Warning: Battery Capacity in the APM is set differently to that in the Models Parameter file.")
                        '        WriteTextLog(vbNewLine)
                        '    End If
                    End If

                    If Param = "ARMING_CHECK" And Value <> "1" Then
                        Call WriteParamHeader()
                        WriteTextLog(Param & " = " & Value)
                        WriteTextLog("Warning: Pre-Arming Check is not Active (0=Off & 1=On)")
                        WriteTextLog(vbNewLine)
                    End If
                    If Param = "RTL_ALT" And Value < "1500" Then
                        Call WriteParamHeader()
                        WriteTextLog(Param & " = " & Value)
                        WriteTextLog("Warning: Return to Launch Altitude is less than 15 meters (Value in CM)")
                        WriteTextLog(vbNewLine)
                    End If
                    If Param = "RTL_ALT_FINAL" And Val(Value) = 0 Then
                        Call WriteParamHeader()
                        WriteTextLog(Param & " = " & Value)
                        WriteTextLog("Warning: Return to Launch is set to Land (Value in CM)")
                        WriteTextLog(vbNewLine)
                    End If
                    If Param = "RTL_ALT_FINAL" And (Val(Value) > 0 And Val(Value) < 500) Then
                        Call WriteParamHeader()
                        WriteTextLog(Param & " = " & Value)
                        WriteTextLog("Warning: Return to Launch Final Hover Altitude is set to less than 5 meters (Value in CM)")
                        WriteTextLog(vbNewLine)
                    End If
                    If Param = "RTL_LOIT_TIME" And Val(Value) < 5000 Then
                        Call WriteParamHeader()
                        WriteTextLog(Param & " = " & Value)
                        WriteTextLog("Warning: Return to Launch Loiter Time is less than 5 seconds (Value in ms)")
                        WriteTextLog(vbNewLine)
                    End If
                    If (Param = "COMPASS_OFS_X" Or Param = "COMPASS_OFS_Y" Or Param = "COMPASS_OFS_Z") And (Val(Value) < -150 Or Val(Value) > 150) Then
                        Call WriteParamHeader()
                        WriteTextLog(Param & " = " & Value)
                        WriteTextLog("Warning: Compass Offset are not set correctly (-150 to +150 recommended).")
                        WriteTextLog(vbNewLine)
                    End If
                End If
            End If
        End If

    End Sub
End Module
