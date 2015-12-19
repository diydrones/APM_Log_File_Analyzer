Module modDU32_Checks_v3_3
    Public Sub DU32_Checks_v3_3()
        '    DU32 7 Structure is bependent on the AP Firmware Version Number being used.

        'Firmware 2.9.x (Switch Off and Uncheck the DU32 button)
        '    static struct AP_System{
        '    uint8_t GPS_light               : 1; // 1   // Solid indicates we have full 3D lock and can navigate, flash = read
        '    uint8_t motor_light             : 1; // 2   // Solid indicates Armed state
        '    uint8_t new_radio_frame         : 1; // 3   // Set true if we have new PWM data to act on from the Radio
        '    uint8_t nav_ok                  : 1; // 4   // deprecated
        '    uint8_t CH7_flag                : 1; // 5   // manages state of the ch7 toggle switch
        '    uint8_t usb_connected           : 1; // 6   // true if APM is powered from USB connection
        '    uint8_t run_50hz_loop           : 1; // 7   // toggles the 100hz loop for 50hz
        '    uint8_t alt_sensor_flag         : 1; // 8   // used to track when to read sensors vs estimate alt
        '    uint8_t yaw_stopped             : 1; // 9   // Used to manage the Yaw hold capabilities

        '} ap_system;


        'Firmware 3.0.x (Switch Off and Uncheck the DU32 button)
        '    static struct AP_System{
        '    uint8_t GPS_light               : 1; // 0   // Solid indicates we have full 3D lock and can navigate, flash = read
        '    uint8_t arming_light            : 1; // 1   // Solid indicates armed state, flashing is disarmed, double flashing is disarmed and failing pre-arm checks
        '    uint8_t new_radio_frame         : 1; // 2   // Set true if we have new PWM data to act on from the Radio
        '    uint8_t CH7_flag                : 1; // 3   // true if ch7 aux switch is high
        '    uint8_t CH8_flag                : 1; // 4   // true if ch8 aux switch is high
        '    uint8_t usb_connected           : 1; // 5   // true if APM is powered from USB connection
        '    uint8_t yaw_stopped             : 1; // 6   // Used to manage the Yaw hold capabilities

        '} ap_system;


        'Firmware 3.1.5 (Analyse based on this v3.1.x)
        '    static union {
        'struct {
        '    uint8_t home_is_set         : 1; // 0
        '    uint8_t simple_mode         : 2; // 1,2 // This is the state of simple mode : 0 = disabled ; 1 = SIMPLE ; 2 = SUPERSIMPLE

        '    uint8_t pre_arm_rc_check    : 1; // 3   // true if rc input pre-arm checks have been completed successfully
        '    uint8_t pre_arm_check       : 1; // 4   // true if all pre-arm checks (rc, accel calibration, gps lock) have been performed
        '    uint8_t auto_armed          : 1; // 5   // stops auto missions from beginning until throttle is raised
        '    uint8_t logging_started     : 1; // 6   // true if dataflash logging has started

        '    uint8_t do_flip             : 1; // 7   // Used to enable flip code
        '    uint8_t takeoff_complete    : 1; // 8
        '    uint8_t land_complete       : 1; // 9   // true if we have detected a landing

        '    uint8_t new_radio_frame     : 1; // 10      // Set true if we have new PWM data to act on from the Radio
        '    uint8_t CH7_flag            : 2; // 11,12   // ch7 aux switch : 0 is low or false, 1 is center or true, 2 is high
        '    uint8_t CH8_flag            : 2; // 13,14   // ch8 aux switch : 0 is low or false, 1 is center or true, 2 is high
        '    uint8_t usb_connected       : 1; // 15      // true if APM is powered from USB connection
        '    uint8_t yaw_stopped         : 1; // 16      // Used to manage the Yaw hold capabilities

        '    uint8_t disable_stab_rate_limit : 1; // 17  // disables limits rate request from the stability controller

        '    uint8_t rc_receiver_present : 1; // 18  // true if we have an rc receiver present (i.e. if we've ever received an update
        '};
        'uint32_t value;
        '} ap;


        'Firmware 3.2 (Analyse based on this v3.2.x) - to be confirmed
        '        static union {
        '    struct {
        '        uint8_t home_is_set         : 1; // 0
        '        uint8_t simple_mode         : 2; // 1,2 // This is the state of simple mode : 0 = disabled ; 1 = SIMPLE ; 2 = SUPERSIMPLE
        '        uint8_t pre_arm_rc_check    : 1; // 3   // true if rc input pre-arm checks have been completed successfully
        '        uint8_t pre_arm_check       : 1; // 4   // true if all pre-arm checks (rc, accel calibration, gps lock) have been performed
        '        uint8_t auto_armed          : 1; // 5   // stops auto missions from beginning until throttle is raised
        '        uint8_t logging_started     : 1; // 6   // true if dataflash logging has started
        '        uint8_t land_complete       : 1; // 7   // true if we have detected a landing
        '        uint8_t new_radio_frame     : 1; // 8       // Set true if we have new PWM data to act on from the Radio
        '        uint8_t CH7_flag            : 2; // 9,10   // ch7 aux switch : 0 is low or false, 1 is center or true, 2 is high
        '        uint8_t CH8_flag            : 2; // 11,12   // ch8 aux switch : 0 is low or false, 1 is center or true, 2 is high
        '        uint8_t usb_connected       : 1; // 13      // true if APM is powered from USB connection
        '        uint8_t rc_receiver_present : 1; // 14  // true if we have an rc receiver present (i.e. if we've ever received an update
        '        uint8_t compass_mot         : 1; // 15  // true if we are currently performing compassmot calibration
        '        uint8_t motor_test          : 1; // 16  // true if we are currently performing the motors test
        '        uint8_t initialised         : 1; // 17  // true once the init_ardupilot function has completed.  Extended status to GCS is not sent until this completes
        '        uint8_t land_complete_maybe : 1; // 18  // true if we may have landed (less strict version of land_complete)

        'Firmware 3.3 
        '        // Documentation of GLobals
        'union {
        '    struct {
        '        uint8_t unused1             :  1; // 0
        '        uint8_t simple_mode         :  2; // 1,2     // This Is the state of simple mode : 0 = disabled ; 1 = SIMPLE ; 2 = SUPERSIMPLE
        '        uint8_t pre_arm_rc_check    :  1; // 3       // true if rc input pre-arm checks have been completed successfully
        '        uint8_t pre_arm_check       :  1; // 4       // true if all pre-arm checks (rc, accel calibration, gps lock) have been performed
        '        uint8_t auto_armed          :  1; // 5       // stops auto missions from beginning until throttle Is raised
        '        uint8_t logging_started     :  1; // 6       // true if dataflash logging has started
        '        uint8_t land_complete       :  1; // 7       // true if we have detected a landing
        '        uint8_t new_radio_frame     :  1; // 8       // Set true if we have New PWM data to act on from the Radio
        '        uint8_t usb_connected       :  1; // 9       // true if APM Is powered from USB connection
        '        uint8_t rc_receiver_present :  1; // 10      // true if we have an rc receiver present (i.e. if we've ever received an update
        '        uint8_t compass_mot         :  1; // 11      // true if we are currently performing compassmot calibration
        '        uint8_t motor_test          :  1; // 12      // true if we are currently performing the motors test
        '        uint8_t initialised         :  1; // 13      // true once the init_ardupilot function has completed.  Extended status to GCS Is Not sent until this completes
        '        uint8_t land_complete_maybe :  1; // 14      // true if we may have landed (less strict version of land_complete)
        '        uint8_t throttle_zero       :  1; // 15      // true if the throttle stick Is at zero, debounced, determines if pilot intends shut-down when Not using motor interlock
        '        uint8_t system_time_set     :  1; // 16      // true if the system time has been set from the GPS
        '        uint8_t gps_base_pos_set    :  1; // 17      // true when the gps base position has been set (used for RTK gps only)
        '        Enum HomeState home_state   : 2; // 18,19   // home status (unset, set, locked)
        '        uint8_t using_interlock :  1; // 20      // aux switch motor interlock function Is in use
        '        uint8_t motor_emergency_stop :  1; // 21      // motor estop switch, shuts off motors when enabled
        '        uint8_t land_repo_active :  1; // 22      // true if the pilot Is overriding the landing position

        'v3.2 - FMT, 22, 8, D32, Bi, Id,Value
        'v3.3 - FMT, 22, 16, D32, QBi, TimeUS,Id,Value

        If DataArray(0) = "DU32" Then
            If ReadFileResilienceCheck(3) = True Then
                Log_DU32_ID = Val(DataArray(2))
                Log_DU32_Value = Val(DataArray(3))

                'Debug.Print("Processing:- " & Str(DataLine) & ": " & Data)

                'Process a Bit Mask
                If Log_DU32_ID = 7 Then

                    If Log_DU32_SimpleMode <> (((Log_DU32_Value And (2 ^ 1)) > 0) + ((Log_DU32_Value And (2 ^ 2)))) And Log_DU32_SimpleMode <> -99 Then
                        Log_DU32_SimpleMode = (((Log_DU32_Value And (2 ^ 1)) > 0) + ((Log_DU32_Value And (2 ^ 2))))
                        If Log_DU32_SimpleMode = 0 Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Simple Mode is Disabled")
                        Else
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Simple Mode is On")
                        End If
                    Else
                        If Log_DU32_SimpleMode <> -99 Then
                            Log_DU32_SimpleMode = -99
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


                    If Log_DU32_AutoWait <> ((Log_DU32_Value And (2 ^ 5)) > 0) Then
                        Log_DU32_AutoWait = ((Log_DU32_Value And (2 ^ 5)) > 0)
                        If Log_DU32_AutoWait = True Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Mode is waiting for Throttle.")
                        End If
                    End If


                    If Log_DU32_USB <> ((Log_DU32_Value And (2 ^ 9)) > 0) Then
                        Log_DU32_USB = ((Log_DU32_Value And (2 ^ 9)) > 0)
                        If Log_DU32_USB = True Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Information: USB is Connected.")
                        Else
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Information: USB is NOT Connected.")
                            USB_Warning1 = False
                        End If
                    End If


                    If Log_DU32_SystemTimeGPS <> ((Log_DU32_Value And (2 ^ 16)) > 0) Then
                        Log_DU32_SystemTimeGPS = ((Log_DU32_Value And (2 ^ 16)) > 0)
                        If Log_DU32_SystemTimeGPS = True Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Information: System Time has been set from the GPS.")
                        End If
                    End If

                    'Log_DU32_Value = 131072  ' Bit 18 only = 0
                    'Log_DU32_Value = 262144  ' Bit 19 only, = -1 
                    'Log_DU32_Value = 393216  ' Both bits = 99  

                    If Log_DU32_HomeSet <> (((Log_DU32_Value And (2 ^ 18)) > 0) + ((Log_DU32_Value And (2 ^ 19)))) And Log_DU32_HomeSet <> -99 Then
                        Log_DU32_HomeSet = (((Log_DU32_Value And (2 ^ 18)) > 0) + ((Log_DU32_Value And (2 ^ 19))))
                        If Log_DU32_HomeSet = 0 Then
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Information: Home is NOT Set")
                        Else
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Information: Home is Set")
                        End If
                    Else
                        If Log_DU32_HomeSet <> -99 Then
                            Log_DU32_HomeSet = -99
                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Information: Home is Locked")
                        End If
                    End If


                        ' **************************************************
                        ' *** Only if the DU32 box checked               ***
                        ' **************************************************
                        If frmMainForm.chkboxDU32.Checked = True Then

                        If Log_DU32_Landing <> ((Log_DU32_Value And (2 ^ 7)) > 0) Then
                            Log_DU32_Landing = ((Log_DU32_Value And (2 ^ 7)) > 0)
                            If Log_DU32_Landing = True Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": DU32 Landing Detected.")
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


                    End If
                End If
            End If
        End If

    End Sub
End Module
