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

                If Param = "AHRS_EKF_USE" Then
                    If Val(Value) = 1 Then
                        Call WriteParamHeader()
                        WriteTextLog(Param & " = " & Value)
                        WriteTextLog("Information: Using Extended Kalman Filter (EKF) for Navigation.")
                        WriteTextLog(vbNewLine)
                    Else
                        Call WriteParamHeader()
                        WriteTextLog(Param & " = " & Value)
                        WriteTextLog("Information: Using Direction Cosine Matrix (DCM) for Navigation.")
                        WriteTextLog(vbNewLine)
                    End If
                End If

                If Param = "TUNE" Then
                    Dim TempString As String = ""
                    CH6_Tune = True
                    Select Case Val(Value)
                        Case 0
                            TempString = "Information: CH6 Tuning is Disabled"
                            CH6_Tune = False
                        Case 1
                            TempString = "Information: CH6 Tuning: stabilize roll/pitch angle controller's P term"
                        Case 3
                            TempString = "Information: CH6 Tuning: stabilize yaw heading controller's P term"
                        Case 4
                            TempString = "Information: CH6 Tuning: body frame roll/pitch rate controller's P term"
                        Case 5
                            TempString = "Information: CH6 Tuning: body frame roll/pitch rate controller's I term"
                        Case 6
                            TempString = "Information: CH6 Tuning: body frame yaw rate controller's P term"
                        Case 7
                            TempString = "Information: CH6 Tuning: throttle rate controller's P term (desired rate to acceleration or motor output)"
                        Case 10
                            TempString = "Information: CH6 Tuning: maximum speed to next way point (0 to 10m/s)"
                        Case 12
                            TempString = "Information: CH6 Tuning: loiter distance controller's P term (position error to speed)"
                        Case 13
                            TempString = "Information: CH6 Tuning: TradHeli specific external tail gyro gain"
                        Case 14
                            TempString = "Information: CH6 Tuning: altitude hold controller's P term (alt error to desired rate)"
                        Case 17
                            TempString = "Information: CH6 Tuning: optical flow loiter controller's P term (position error to tilt angle)"
                        Case 18
                            TempString = "Information: CH6 Tuning: optical flow loiter controller's I term (position error to tilt angle)"
                        Case 19
                            TempString = "Information: CH6 Tuning: optical flow loiter controller's D term (position error to tilt angle)"
                        Case 21
                            TempString = "Information: CH6 Tuning: body frame roll/pitch rate controller's D term"
                        Case 22
                            TempString = "Information: CH6 Tuning: loiter rate controller's P term (speed error to tilt angle)"
                        Case 23
                            TempString = "Information: CH6 Tuning: loiter rate controller's D term (speed error to tilt angle)"
                        Case 25
                            TempString = "Information: CH6 Tuning: acro controller's P term.  converts pilot input to a desired roll, pitch or yaw rate"
                        Case 26
                            TempString = "Information: CH6 Tuning: body frame yaw rate controller's D term"
                        Case 28
                            TempString = "Information: CH6 Tuning: loiter rate controller's I term (speed error to tilt angle)"
                        Case 30
                            TempString = "Information: CH6 Tuning: ahrs's compass effect on yaw angle (0 = very low, 1 = very high)"
                        Case 31
                            TempString = "Information: CH6 Tuning: accelerometer effect on roll/pitch angle (0=low)"
                        Case 34
                            TempString = "Information: CH6 Tuning: accel based throttle controller's P term"
                        Case 35
                            TempString = "Information: CH6 Tuning: accel based throttle controller's I term"
                        Case 36
                            TempString = "Information: CH6 Tuning: accel based throttle controller's D term"
                        Case 38
                            TempString = "Information: CH6 Tuning: compass declination in radians"
                        Case 39
                            TempString = "Information: CH6 Tuning: circle turn rate in degrees (hard coded to about 45 degrees in either direction)"
                        Case 40
                            TempString = "Information: CH6 Tuning: acro controller's P term.  converts pilot input to a desired roll, pitch or yaw rate"
                        Case 41
                            TempString = "Information: CH6 Tuning: sonar gain"
                        Case 42
                            TempString = "Information: CH6 Tuning: EKF's baro vs accel (higher rely on accels more, baro impact is reduced).  Range should be 0.2 ~ 4.0?  2.0 is default"
                        Case 43
                            TempString = "Information: CH6 Tuning: EKF's gps vs accel (higher rely on accels more, gps impact is reduced).  Range should be 1.0 ~ 3.0?  1.5 is default"
                        Case 44
                            TempString = "Information: CH6 Tuning: EKF's accel noise (lower means trust accels more, gps & baro less).  Range should be 0.02 ~ 0.5  0.5 is default (but very robust at that level)"
                        Case 45
                            TempString = "Information: CH6 Tuning: roll-pitch input smoothing"
                        Case 46
                            TempString = "Information: CH6 Tuning: body frame pitch rate controller's P term"
                        Case 47
                            TempString = "Information: CH6 Tuning: body frame pitch rate controller's I term"
                        Case 48
                            TempString = "Information: CH6 Tuning: body frame pitch rate controller's D term"
                        Case 49
                            TempString = "Information: CH6 Tuning: body frame roll rate controller's P term"
                        Case 50
                            TempString = "Information: CH6 Tuning: body frame roll rate controller's I term"
                        Case 51
                            TempString = "Information: CH6 Tuning: body frame roll rate controller's D term"
                        Case 52
                            TempString = "Information: CH6 Tuning: body frame pitch rate controller FF term"
                        Case 53
                            TempString = "Information: CH6 Tuning: body frame roll rate controller FF term"
                        Case 54
                            TempString = "Information: CH6 Tuning: body frame yaw rate controller FF term"
                        Case Else
                            TempString = "Information: Update program to new tuning on CH6, option = " & Value
                    End Select

                    Call WriteParamHeader()
                    WriteTextLog(Param & " = " & Value)
                    WriteTextLog(TempString)
                    If CH6_Tune = False Then WriteTextLog(vbNewLine) 'there will not be any Mix / Max data to follow
                End If

                'Currently assumes TUNE, TUNE_LOW and TUNE_HIGH appear in the log in this order!
                If Param = "TUNE_LOW" And CH6_Tune = True Then
                    Call WriteParamHeader()
                    WriteTextLog("Information: CH6 Tuning: Low Value set to: " & Value)
                End If

                If Param = "TUNE_HIGH" And CH6_Tune = True Then
                    Call WriteParamHeader()
                    WriteTextLog("Information: CH6 Tuning: High Value set to: " & Value)
                    WriteTextLog(vbNewLine)
                End If

                If Param = "FS_GPS_ENABLE" And Val(Value) = 0 Then
                    Call WriteParamHeader()
                    WriteTextLog("Warning: GPS Failsafe has been disabled.")
                    WriteTextLog("Warning: Refer to this Issue for more information:")
                    WriteTextLog("Warning: https://github.com/diydrones/ardupilot/issues/1560")
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
