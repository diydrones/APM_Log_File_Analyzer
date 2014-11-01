Module modPARAM_Checks
    Public Sub Parameter_Checks()

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

                'Write the parameter found to the Parameter List Box
                frmParameters.lstboxParameters.Items.Add(Param & "  =  " & Value)

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
                    '0:Disabled  1:Enabled  -3:Skip Baro  -5:Skip Compass  -9:Skip GPS  -17:Skip INS  -33:Skip Parameters  -65:Skip RC  127:Skip Voltage
                    Call WriteParamHeader()
                    WriteTextLog(Param & " = " & Value)

                    Select Case Val(Value)
                        Case 0
                            WriteTextLog("Warning: Pre-Arming Check is not Active (0=Off & 1=Fully On)")
                        Case -3
                            WriteTextLog("Warning: Pre-Arming Check is set to Skip Barometer Checks! (1=Fully On)")
                        Case -5
                            WriteTextLog("Warning: Pre-Arming Check is set to Skip Compass Checks! (1=Fully On)")
                        Case -9
                            WriteTextLog("Warning: Pre-Arming Check is set to Skip GPS Checks! (1=Fully On)")
                        Case -17
                            WriteTextLog("Warning: Pre-Arming Check is set to Skip INS Checks! (1=Fully On)")
                        Case -33
                            WriteTextLog("Warning: Pre-Arming Check is set to Skip Parameter Checks! (1=Fully On)")
                        Case -65
                            WriteTextLog("Warning: Pre-Arming Check is set to Skip RC Checks! (1=Fully On)")
                        Case 127
                            WriteTextLog("Warning: Pre-Arming Check is set to Skip Voltage Checks! (1=Fully On)")
                        Case Else
                            WriteTextLog("Error: Pre-Arming Check has an unrecognised value (0=Off & 1=Fully On)")
                            WriteTextLog("Error: You are advised not to fly until this value has been validated !")
                    End Select
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
                If Param = "INS_PRODUCT_ID" And Val(Value) = 0 Then
                    Call WriteParamHeader()
                    WriteTextLog(Param & " = " & Value)
                    WriteTextLog("Warning: Unknown Ardupilot Hardware Detected.")
                    WriteTextLog(vbNewLine)
                End If
                If Param = "INS_PRODUCT_ID" And (Val(Value) = 3 Or Val(Value) = 256 Or Val(Value) = 257) Then
                    Call WriteParamHeader()
                    WriteTextLog(Param & " = " & Value)
                    WriteTextLog("Warning: Hardware Simulation Software Used.")
                    WriteTextLog(vbNewLine)
                End If

                If Param = "CH7_OPT" Or Param = "CH8_OPT" Then
                    Dim TempString As String = ""
                    Select Case Val(Value)
                        Case 0
                            TempString = "Disabled"
                        Case 2
                            TempString = "Flip"
                        Case 3
                            TempString = "Change to Simple Mode"
                        Case 4
                            TempString = "Change to RTL Mode"
                        Case 5
                            TempString = "Save Current Position as Level"
                        Case 7
                            TempString = "Save Mission Waypoint or RTL if in Auto Mode"
                        Case 8
                            TempString = "Multi Mode: Depends on CH6 for Flip (low), RTL (mid), or Save Waypoint (high)"
                        Case 9
                            TempString = "Trigger Camera Servo or Relay"
                        Case 10
                            TempString = "Enable / Disable Sonar"
                        Case 11
                            TempString = "Enable / Disable Fence"
                        Case 12
                            TempString = "Change Yew to Armed Direction"
                        Case 13
                            TempString = "Low = Normal, Mid = Simple or High = Super Simple."
                        Case 14
                            TempString = "ACRO Trainer, Low = Disabled, Mid = Leveled or High = Leveled and Limited."
                        Case 15
                            TempString = "Enable / Disable Crop Sprayer."
                        Case 16
                            TempString = "Change to Auto Flight Mode"
                        Case 17
                            TempString = "Enable / Disable Auto Tune"
                        Case 18
                            TempString = "Change to LAND Flight Mode"
                        Case 19
                            TempString = "EPM Cargo Gripper, Low = Off, Mid = Neutral or High = On"
                        Case 20
                            TempString = "Enable / Disable NavEKF."
                        Case 21
                            TempString = "Enable / Disable Parachute."
                        Case 22
                            TempString = "Parachute Release."
                        Case 23
                            TempString = "Parachute Disable, Enable and Release (3 pos switch version)."
                        Case 24
                            TempString = "Reset Auto Mission to Start of from First Command."
                        Case 25
                            TempString = "Enable / Disable the Roll and Pitch Rate Feed Forward."
                        Case 26
                            TempString = "Enable / Disable the Roll, Pitch and Yaw Accel Limiting."
                        Case 27
                            TempString = "Retract Mount."
                        Case 28
                            TempString = "Relay pin on/off (only supports first relay)."
                        Case Else
                            TempString = "New Function - Update Program to support this."
                    End Select
                    If Param = "CH7_OPT" Then CH7_OPT = TempString Else CH8_OPT = TempString
                End If
            End If
        End If

    End Sub
End Module
