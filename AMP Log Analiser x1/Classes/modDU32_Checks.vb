Module modDU32_Checks
    Public Sub DU32_Checks()
        '    DU32 7 Structure

        'struct {
        '    uint8_t home_is_set         : 1; // 0
        '    uint8_t simple_mode         : 2; // 1,2 // This is the state of simple mode : 0 = disabled ; 1 = SIMPLE ; 2 = SUPERSIMPLE
        '    uint8_t pre_arm_rc_check    : 1; // 3   // true if rc input pre-arm checks have been completed successfully
        '    uint8_t pre_arm_check       : 1; // 4   // true if all pre-arm checks (rc, accel calibration, gps lock) have been performed
        '    uint8_t auto_armed          : 1; // 5   // stops auto missions from beginning until throttle is raised
        '    uint8_t logging_started     : 1; // 6   // true if dataflash logging has started
        '    uint8_t land_complete       : 1; // 7   // true if we have detected a landing
        '    uint8_t new_radio_frame     : 1; // 8       // Set true if we have new PWM data to act on from the Radio
        '    uint8_t CH7_flag            : 2; // 9,10   // ch7 aux switch : 0 is low or false, 1 is center or true, 2 is high
        '    uint8_t CH8_flag            : 2; // 11,12   // ch8 aux switch : 0 is low or false, 1 is center or true, 2 is high
        '    uint8_t usb_connected       : 1; // 13      // true if APM is powered from USB connection
        '    uint8_t rc_receiver_present : 1; // 14  // true if we have an rc receiver present (i.e. if we've ever received an update
        '    uint8_t compass_mot         : 1; // 15  // true if we are currently performing compassmot calibration
        '};

        If DataArray(0) = "DU32" Then
            If ReadFileResilienceCheck(2) = True Then
                Log_DU32_ID = Val(DataArray(1))
                Log_DU32_Value = Val(DataArray(2))

                'Debug.Print("Processing:- " & Str(DataLine) & ": " & Data)

                'Process a Bit Mask
                If Log_DU32_ID = 7 Then
                    'Show the bits
                    'Dim BitMask As Int32 = 0
                    'Dim BitText As String = ""
                    'For n = 0 To 15
                    '    BitMask = ((Log_DU32_Value And (2 ^ n)) > 0)

                    '    Debug.Print("Bit " & n & " = " & BitMask)
                    'Next

                    If Log_DU32_HomeSet <> ((Log_DU32_Value And (2 ^ 0)) > 0) Then
                        Log_DU32_HomeSet = ((Log_DU32_Value And (2 ^ 0)) > 0)
                        If Log_DU32_HomeSet = True Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Information: Home is Set")
                        Else
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Information: Home is NOT Set")
                        End If
                    End If

                    If Log_DU32_SimpleMode <> (((Log_DU32_Value And (2 ^ 1)) > 0) + ((Log_DU32_Value And (2 ^ 2)) > 0)) Then
                        Log_DU32_SimpleMode = (((Log_DU32_Value And (2 ^ 1)) > 0) + ((Log_DU32_Value And (2 ^ 2)) > 0))
                        If Log_DU32_SimpleMode = 0 Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Simple Mode is Disabled")
                        ElseIf Log_DU32_SimpleMode = -1 Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Simple Mode is On")
                        Else
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Super Simple Mode is On")
                        End If
                    End If

                    If Log_DU32_RC_PreArm <> ((Log_DU32_Value And (2 ^ 3)) > 0) Then
                        Log_DU32_RC_PreArm = ((Log_DU32_Value And (2 ^ 3)) > 0)
                        If Log_DU32_RC_PreArm = True Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Information: RC Pre-Arm checks have been completed.")
                        Else
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Information: RC Pre-Arm checks have NOT been completed.")
                        End If
                    End If

                    If Log_DU32_ALL_PreArm <> ((Log_DU32_Value And (2 ^ 4)) > 0) Then
                        Log_DU32_ALL_PreArm = ((Log_DU32_Value And (2 ^ 4)) > 0)
                        If Log_DU32_ALL_PreArm = True Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Information: All Pre-Arm checks have been completed.")
                        Else
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Information: All Pre-Arm checks have NOT been completed.")
                        End If
                    End If

                    If Log_DU32_USB <> ((Log_DU32_Value And (2 ^ 13)) > 0) Then
                        Log_DU32_USB = ((Log_DU32_Value And (2 ^ 13)) > 0)
                        If Log_DU32_USB = True Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Information: USB is Connected.")
                        Else
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Information: USB is NOT Connected.")
                            USB_Warning1 = False
                        End If
                    End If


                    If Log_DU32_CH7_Switch <> (((Log_DU32_Value And (2 ^ 9)) > 0) + ((Log_DU32_Value And (2 ^ 10)) > 0)) Then
                        Log_DU32_CH7_Switch = (((Log_DU32_Value And (2 ^ 9)) > 0) + ((Log_DU32_Value And (2 ^ 10)) > 0))
                        If Log_DU32_CH7_Switch = 0 Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Rx Information: Channel 7 Switch is Low.")
                        ElseIf Log_DU32_CH7_Switch = -1 Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Rx Information: Channel 7 Switch is Centred.")
                        Else
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Rx Information: Channel 7 Switch is High.")
                        End If
                    End If

                    If Log_DU32_CH8_Switch <> (((Log_DU32_Value And (2 ^ 11)) > 0) + ((Log_DU32_Value And (2 ^ 12)) > 0)) Then
                        Log_DU32_CH8_Switch = (((Log_DU32_Value And (2 ^ 11)) > 0) + ((Log_DU32_Value And (2 ^ 12)) > 0))
                        If Log_DU32_CH8_Switch = 0 Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Rx Information: Channel 8 Switch is Low.")
                        ElseIf Log_DU32_CH8_Switch = -1 Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Rx Information: Channel 8 Switch is Centred.")
                        Else
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Rx Information: Channel 8 Switch is High.")
                        End If
                    End If

                    If frmMainForm.chkboxDU32.Checked = True Then

                        If Log_DU32_AutoWait <> ((Log_DU32_Value And (2 ^ 5)) > 0) Then
                            Log_DU32_AutoWait = ((Log_DU32_Value And (2 ^ 5)) > 0)
                            If Log_DU32_AutoWait = True Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Mode is waiting for Throttle.")
                                'Else
                                'WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Mode is NOT waiting for Throttle.")
                            End If
                        End If

                        If Log_DU32_Landing <> ((Log_DU32_Value And (2 ^ 7)) > 0) Then
                            Log_DU32_Landing = ((Log_DU32_Value And (2 ^ 7)) > 0)
                            If Log_DU32_Landing = True Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": DU32 Landing Detected.")
                                'Else
                                '    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": DU32 NO Landing Detected.")
                            End If
                        End If


                        If Log_DU32_Receiver <> ((Log_DU32_Value And (2 ^ 8)) > 0) Then
                            Log_DU32_Receiver = ((Log_DU32_Value And (2 ^ 8)) > 0)
                            If Log_DU32_Receiver = True Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Information: PWM is present.")
                            Else
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Information: PWM is NOT present.")
                            End If
                        End If

                        If Log_DU32_Compass_Mot <> ((Log_DU32_Value And (2 ^ 15)) > 0) Then
                            Log_DU32_Compass_Mot = ((Log_DU32_Value And (2 ^ 15)) > 0)
                            If Log_DU32_Compass_Mot = True Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Information: CompassMot is being Calibrated.")
                                'Else
                                '    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Information: CompassMot is NOT being Calibrated.")
                            End If
                        End If

                    End If
                End If
            End If
        End If

    End Sub
End Module
