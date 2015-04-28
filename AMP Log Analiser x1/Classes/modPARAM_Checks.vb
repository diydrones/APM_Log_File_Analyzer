Module modPARAM_Checks
    Public Sub Parameter_Checks()

        ' FailSafe Header
        ' NOTE: $$$ just truns the Text Colour LightGreen by the formatter
        Dim MyString As String = ""
        WriteTextLog("$$$FAILSAFE SETTINGS:")
        ' Battery FailSafe
        MyString = "$$$  Main Battery = "
        If PARM_FS_BATT_ENABLE = 99 Then
            WriteTextLog(MyString & "Parameter Not Found in Log")
        ElseIf PARM_FS_BATT_ENABLE = 0 Then
            WriteTextLog(MyString & "Not Activated")
        ElseIf PARM_FS_BATT_ENABLE = 1 Then
            WriteTextLog(MyString & "Activated at " & PARM_FS_BATT_VOLTAGE & " volts or " & PARM_FS_BATT_MAH & " mAH, with Land")
        ElseIf PARM_FS_BATT_ENABLE = 2 Then
            WriteTextLog(MyString & "Activated at " & PARM_FS_BATT_VOLTAGE & " volts or " & PARM_FS_BATT_MAH & " mAH, with RTL")
        Else
            WriteTextLog(MyString & "Update program for new FS_BATT_ENABLE value.")
        End If
        ' GCS FailSafe
        MyString = "$$$Ground Control = "
        If PARM_FS_GCS_ENABLE = 99 Then
            WriteTextLog(MyString & "Parameter Not Found in Log")
        ElseIf PARM_FS_GCS_ENABLE = 0 Then
            WriteTextLog(MyString & "Not Activated")
        ElseIf PARM_FS_GCS_ENABLE = 1 Then
            WriteTextLog(MyString & "Activated, with always RTL")
        ElseIf PARM_FS_GCS_ENABLE = 2 Then
            WriteTextLog(MyString & "Activated, with Continue with Mission in Auto Mode")
        Else
            WriteTextLog(MyString & "Update program for new FS_GCS_ENABLE value.")
        End If
        ' Throttle FailSafe
        MyString = "$$$      Receiver = "
        If PARM_FS_THR_ENABLE = 99 Then
            WriteTextLog(MyString & "Parameter Not Found in Log")
        ElseIf PARM_FS_THR_ENABLE = 0 Then
            WriteTextLog(MyString & "Not Activated")
        ElseIf PARM_FS_THR_ENABLE = 1 Then
            WriteTextLog(MyString & "Activated at " & PARM_FS_THR_VALUE & " PWM with always RTL")
        ElseIf PARM_FS_THR_ENABLE = 2 Then
            WriteTextLog(MyString & "Activated at " & PARM_FS_THR_VALUE & " PWM with Continue with Mission in Auto Mode")
        ElseIf PARM_FS_THR_ENABLE = 3 Then
            WriteTextLog(MyString & "Activated at " & PARM_FS_THR_VALUE & " PWM with always LAND")
        Else
            WriteTextLog(MyString & "Update program for new FS_THR_ENABLE value.")
        End If
        ' Fence FailSafe
        MyString = "$$$     GEO Fence = "
        If PARM_FENCE_ENABLE = 99 Then
            WriteTextLog(MyString & "Parameter Not Found in Log")
        ElseIf PARM_FENCE_ENABLE = 0 Then
            WriteTextLog(MyString & "Not Activated")
        ElseIf PARM_FENCE_ENABLE = 1 Then
            MyString = MyString & "Activated "
            If PARM_FENCE_TYPE = 1 Then
                WriteTextLog(MyString & "on Altitude of " & PARM_FENCE_ALT_MAX & " meters")
            ElseIf PARM_FENCE_TYPE = 2 Then
                WriteTextLog(MyString & "on Circle Radius of " & PARM_FENCE_RADIUS & " meters")
            ElseIf PARM_FENCE_TYPE = 3 Then
                WriteTextLog(MyString & "on Altitude of " & PARM_FENCE_ALT_MAX & " & Circle Radius " & PARM_FENCE_RADIUS & " meters")
            Else
                WriteTextLog(MyString & "with a new Type Parameter :" & PARM_FENCE_TYPE)
            End If
            MyString = "$$$    GEO Action = "
            If PARM_FENCE_ACTION = 0 Then
                WriteTextLog(MyString & "Report Only")
            ElseIf PARM_FENCE_ACTION = 1 Then
                WriteTextLog(MyString & "RTL or Land")
            Else
                WriteTextLog(MyString & "Fail Safe Activated with with a new Action Parameter :" & PARM_FENCE_ACTION)
            End If
            MyString = "$$$    GEO Margin = "
            WriteTextLog(MyString & PARM_FENCE_MARGIN & " meters")
        Else
            WriteTextLog(MyString & "Update program for new FENCE_ENABLE value.")
        End If


        'Check that ACRO mode will try to self level.
        If PARM_ACRO_TRAINER <> 2 Then
            Call WriteParamHeader()
            WriteTextLog("ACRO_TRAINER = " & PARM_ACRO_TRAINER)
            WriteTextLog("Warning: UAV will not attempt to self level in ACRO mode!")
            WriteTextLog("http://ardupilot.com/forum/viewtopic.php?f=25&t=5613")
            WriteTextLog(vbNewLine)
        End If

        'Check that the Throttle Min and Motor Spin Armed are still defaults
        'and advise that if the Motors do not spin on Arming then increase the
        'Motor Spin Arm upto a Max of Throttle Min.
        If PARM_THR_MIN <> 99 Or PARM_MOT_SPIN_ARMED <> 99 Then
            If PARM_THR_MIN <> 0 And PARM_MOT_SPIN_ARMED <> 0 Then
                If PARM_MOT_SPIN_ARMED > PARM_THR_MIN Then
                    Call WriteParamHeader()
                    WriteTextLog("THR_MIN = " & PARM_THR_MIN)
                    WriteTextLog("MOT_SPIN_ARMED = " & PARM_MOT_SPIN_ARMED)
                    WriteTextLog("Warning: MOT_SPIN_ARMED is greater then THR_MIN parameter, this is dangerous!")
                    WriteTextLog("http://ardupilot.com/forum/viewtopic.php?f=25&t=5345&sid=3171087bcf7d84e03491e1845eaa9393")
                    WriteTextLog(vbNewLine)
                End If
            End If
        End If

        ' CHeck the THR_MAX setting is set in the correct range.
        ' if too low the Quad will not take off.
        If PARM_THR_MAX <> 99 Then ' Ensure the Parmeter to available and set.
            If PARM_THR_MAX < 800 Then
                Call WriteParamHeader()
                WriteTextLog("THR_MAX = " & PARM_THR_MAX)
                WriteTextLog("Warning: MOT_MAX is set lower than normal.")
                WriteTextLog("This may result in the UAV being underpowered and may not take off, normal setting is 800-1000.")
                WriteTextLog("http://www.rcgroups.com/forums/showpost.php?p=31375573&postcount=4406")
                WriteTextLog(vbNewLine)
            End If
            If PARM_THR_MAX > 1000 Then
                Call WriteParamHeader()
                WriteTextLog("THR_MAX = " & PARM_THR_MAX)
                WriteTextLog("Warning: MOT_MAX is set Higher than normal.")
                WriteTextLog("This may result in the UAV being overpowered and may become unstable, normal setting is 800-1000.")
                WriteTextLog("At the time of writing there are no good web links to more information.")
                WriteTextLog("If you are aware of more information please raise a new ""issue"" here so it can be added:")
                WriteTextLog("https://github.com/diydrones/APM_Log_File_Analyzer")
                WriteTextLog(vbNewLine)
            End If
        End If

        ' Warning if Pre-Arming Check is not fully active.
        If PARM_ARMING_CHECK <> 1 Then
            '0:Disabled  1:Enabled  -3:Skip Baro  -5:Skip Compass  -9:Skip GPS  -17:Skip INS  -33:Skip Parameters  -65:Skip RC  127:Skip Voltage
            Call WriteParamHeader()
            WriteTextLog("ARMING_CHECK = " & PARM_ARMING_CHECK)

            Select Case PARM_ARMING_CHECK
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

        ' Warning about GPS FailSafe being disabled.
        If PARM_FS_GPS_ENABLE = 0 Then
            Call WriteParamHeader()
            WriteTextLog("FS_GPS_ENABLE = " & PARM_FS_GPS_ENABLE)
            WriteTextLog("Warning: GPS Failsafe has been disabled.")
            WriteTextLog("Warning: Refer to this Issue for more information:")
            WriteTextLog("Warning: https://github.com/diydrones/ardupilot/issues/1560")
            WriteTextLog(vbNewLine)
        End If

        ' Note: in v3.3 logs this parameter has been removed.
        ' Warning if Pre-Arm check is disabled or skipping GPS and GPS fail safe is enabled
        If PARM_FS_GPS_ENABLE = 1 And (PARM_ARMING_CHECK = 0 Or PARM_ARMING_CHECK = -9) Then
            WriteTextLog("ARMING_CHECK = " & PARM_ARMING_CHECK)
            WriteTextLog("FS_GPS_ENABLE = " & PARM_FS_GPS_ENABLE)
            WriteTextLog("Warning: GPS Pre-Arm checks are disabled, while GPS Failsafe are Enabled.")
            WriteTextLog("Warning: GPS could be unstable while the GPS failsafe requires it to be stable.")
            WriteTextLog("Warning: Recommend having both Enabled! Refer to Issue 1487 for more information.")
            WriteTextLog("Warning: https://github.com/diydrones/ardupilot/issues/1487")
            WriteTextLog(vbNewLine)
        End If


        ' Warning if final RTL landing value is less than 15 meters
        If PARM_RTL_ALT < 1500 Then
            Call WriteParamHeader()
            WriteTextLog("RTL_ALT = " & PARM_RTL_ALT)
            WriteTextLog("Warning: Return to Launch Altitude is less than 15 meters (Value in CM)")
            WriteTextLog(vbNewLine)
        End If
        If PARM_RTL_ALT = 0 Then
            Call WriteParamHeader()
            WriteTextLog("RTL_ALT = " & PARM_RTL_ALT)
            WriteTextLog("Warning: Return to Launch is set to Land (Value in CM)")
            WriteTextLog(vbNewLine)
        End If
        If PARM_RTL_ALT > 0 And PARM_RTL_ALT < 500 Then
            Call WriteParamHeader()
            WriteTextLog("RTL_ALT = " & PARM_RTL_ALT)
            WriteTextLog("Warning: Return to Launch Final Hover Altitude is set to less than 5 meters (Value in CM)")
            WriteTextLog(vbNewLine)
        End If

        ' Warning if RTL loiter time < 5 seconds
        If PARM_RTL_LOIT_TIME < 5000 Then
            Call WriteParamHeader()
            WriteTextLog("RTL_LOIT_TIME = " & PARM_RTL_LOIT_TIME)
            WriteTextLog("Warning: Return to Launch Loiter Time is less than 5 seconds (Value in ms)")
            WriteTextLog(vbNewLine)
        End If

        'Warning if Compass offsets are not within -150 to 150 range
        If PARM_COMPASS_OFS_X < -150 Or PARM_COMPASS_OFS_Y < -150 Or PARM_COMPASS_OFS_Z < -150 Or PARM_COMPASS_OFS_X > 150 Or PARM_COMPASS_OFS_Y > 150 Or PARM_COMPASS_OFS_Z > 150 Then
            Call WriteParamHeader()
            WriteTextLog("COMPASS_OFS_X = " & PARM_COMPASS_OFS_X)
            WriteTextLog("COMPASS_OFS_Y = " & PARM_COMPASS_OFS_Y)
            WriteTextLog("COMPASS_OFS_Z = " & PARM_COMPASS_OFS_Z)
            WriteTextLog("Warning: One or more compass offset is not set correctly (-150 to +150 recommended).")
            WriteTextLog(vbNewLine)
        End If

        'Warning if Hardware is not recongnised
        If PARM_INS_PRODUCT_ID = 0 Or PARM_INS_PRODUCT_ID = 99 Then
            Call WriteParamHeader()
            WriteTextLog("INS_PRODUCT_ID = " & PARM_INS_PRODUCT_ID)
            WriteTextLog("Warning: Unknown Ardupilot Hardware Detected.")
            WriteTextLog(vbNewLine)
        End If

        'Warning if Simulation Software is being used.
        If PARM_INS_PRODUCT_ID = 3 Or PARM_INS_PRODUCT_ID = 256 Or PARM_INS_PRODUCT_ID = 257 Then
            Call WriteParamHeader()
            WriteTextLog("INS_PRODUCT_ID = " & PARM_INS_PRODUCT_ID)
            WriteTextLog("Warning: Hardware Simulation Software Used.")
            WriteTextLog(vbNewLine)
        End If

        'Infomration about which Navigation Code is being exected.
        If PARM_AHRS_EKF_USE = 1 Then
            Call WriteParamHeader()
            WriteTextLog("AHRS_EKF_USE = " & PARM_AHRS_EKF_USE)
            WriteTextLog("Information: Using Extended Kalman Filter (EKF) for Navigation.")
            WriteTextLog("Information: Direction Cosine Matrix (DCM) will be used in the event of EKF failure.")
            WriteTextLog(vbNewLine)
        ElseIf PARM_AHRS_EKF_USE = 0 Then
            Call WriteParamHeader()
            WriteTextLog("AHRS_EKF_USE = " & PARM_AHRS_EKF_USE)
            WriteTextLog("Information: Using Direction Cosine Matrix (DCM) for Navigation.")
            WriteTextLog(vbNewLine)

        End If

        'Information about CH6 tuning setup.
        If PARM_TUNE <> 99 Then
            Dim TempString As String = ""
            PARM_CH6_TUNE = True
            Select Case PARM_TUNE
                Case 0
                    TempString = "Information: CH6 Tuning is Disabled"
                    PARM_CH6_TUNE = False
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
                    TempString = "Information: Update program to new tuning on CH6, option = " & PARM_TUNE
            End Select

            Call WriteParamHeader()
            WriteTextLog("TUNE = " & PARM_TUNE)
            WriteTextLog(TempString)
            If PARM_CH6_TUNE = False Then WriteTextLog(vbNewLine) 'there will not be any Mix / Max data to follow
            If PARM_TUNE_LOW <> 99 And PARM_CH6_TUNE = True Then
                Call WriteParamHeader()
                WriteTextLog("Information: CH6 Tuning: Low Value set to: " & PARM_TUNE_LOW)
            End If
            If PARM_TUNE_HIGH <> 99 And PARM_CH6_TUNE = True Then
                Call WriteParamHeader()
                WriteTextLog("Information: CH6 Tuning: High Value set to: " & PARM_TUNE_HIGH)
                WriteTextLog(vbNewLine)
            End If
        End If


        'Store Information About what CH7 does.
        If PARM_CH7_OPT <> 99 Then
            Dim TempString As String = ""
            Select Case PARM_CH7_OPT
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
            CH7_OPT_TEXT = TempString
        End If

        'Store Information About what CH8 does.
        If PARM_CH8_OPT <> 99 Then
            Dim TempString As String = ""
            Select Case PARM_CH8_OPT
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
            CH8_OPT_TEXT = TempString
        End If


    End Sub
End Module
