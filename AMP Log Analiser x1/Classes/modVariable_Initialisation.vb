Module modVariable_Initialisation
    Public Sub Variable_Initialisation()
        'Initialise the Parameter Variables
        Param = ""                                      'Parameter read from the Log.
        Value = 0                                       'Paramter Value read from the Log.
        Param_Issue_Found = False                       'TRUE if one or more parameters issues are found.
        Thr_Min = 0                                     'Used to Determine the settings for Thr_Min & Mot_Spin_Armed
        Mot_Spin_Armed = 0                              'Used to Determine the settings for Thr_Min & Mot_Spin_Armed
        Log_Battery_Capacity = 0                        'Battery Capacity as found in the APM Parameter Settings.

        'Initialise the INAV Variables
        Log_INAV_Home_GLat = 0                           'Hold where the APM considers home to be.
        Log_INAV_Home_GLng = 0                           'Hold where the APM considers home to be.

        'Initialise the CMD Variables
        Log_CMD_CTot = 0                                'CTot: the total number of commands in the mission
        Log_CMD_CNum = 0                                'CNum: this command’s number in the mission (0 is always home, 1 is the first command, etc)
        Log_CMD_CId = 0                                 'CId: the mavlink message id
        Log_CMD_Copt = 0                                'Copt: the option parameter (used for many different purposes)
        Log_CMD_Prm1 = 0                                'Prm1: the command’s parameter (used for many different purposes)
        Log_CMD_Alt = 0                                 'Alt: the command’s altitude in meters
        Log_CMD_Lat = 0                                 'Lat: the command’s latitude position
        Log_CMD_Lng = 0                                 'Lng: the command’s longitude position
        Log_Last_CMD_Lat = 0                      'Holds the previous WP1 Co-ordinates
        Log_Last_CMD_Lng = 0                      'Holds the previous WP1 Co-ordinates
        Log_Last_CMD_Alt = 0                      'Holds the previous WP Alititude
        Log_CMD_Dist1 = 0                    'Distance using current GPS when we hit the WP radius to the next way point
        Log_CMD_Dist2 = 0                     'Distance between the two way points.

        'Initialise the DU32 Variables
        Log_DU32_ID = 0                                     'Holds the ID number, 7 (bit mask of internal state) or 9 (simple mode’s initial heading in centi-degrees)
        Log_DU32_Value = 0                                  'Holds the DU32 value.
        Log_DU32_HomeSet = 99                               'True if DU32 is reporting Home is set, starts 99 then changes to True or False
        Log_DU32_SimpleMode = 99                             '0=Disabled, 1=Simple, 2=Super Simple Modes
        Log_DU32_RC_PreArm = 99                             'True if the PreArm checks have been completed.
        Log_DU32_ALL_PreArm = 99                                'True if all the PreArm checks have been completed.
        Log_DU32_Landing = 99                                'Ture is Landing Detected
        Log_DU32_CH7_Switch = 99                            'ch7 aux switch : 0 is low or false, 1 is center or true, 2 is high
        Log_DU32_CH8_Switch = 99                            'ch8 aux switch : 0 is low or false, 1 is center or true, 2 is high
        Log_DU32_USB = 99                                   'True if USB is Powered.
        Log_DU32_Compass_Mot = 99                           'True if compassmot is being calibrated.
        Log_DU32_Receiver = 99                              'True if a receiver is present.
        Log_DU32_AutoWait = 99                              'True if Auto mode is waiting for Throttle.
        USB_Warning1 = False                                'True if the USB warning has been presented to the user when APM thinks it has a USB connection while in flight

        'Initialise the PM Variables
        Log_PM_RenCnt = 0                                      'DCM renormalization count
        Log_PM_RenBlw = 0                                      'DCM renormalization blow-up count
        Log_PM_NLon = 0                                        'number of long running main loops
        Log_PM_NLoop = 0                                       'the total number of loops since the last PM message was displayed
        Log_PM_MaxT = 0                                        'the maximum time that any loop took since the last PM message.
        Log_PM_PMT = 0                                         'a number that increments each time a heart beat is received from the ground station, should = 10
        Log_GCS_Attached = False                                'True once we have a valid 100% (PMT = 10) signal from the GCS
        Log_PM_I2CErr = 0                                      'the number of I2C errors since the last PM message.
        Log_PM_INSErr = 0                                      'MPU6k spi bus errors
        Log_PM_INAVErr = 0
        PM_Delay_Counter = 0                         'Counts each PM log found, PM errors on reported when it reaches > x (default = 3)
        PM_Last_PMT = 10                              'The previous PMT reading before the current reading, used to stop repeats.
        PM_Last_INAVErr = 0                          'The previous INAVErr reading before the current reading, used to stop repeats.

        'Declare the EV Variables
        Log_Ground_BarAlt = 0                           'Holds the last Barometer Altitude detected during lift off, now driven from EV Data.
        Log_Armed = False                               'TRUE if the APM is in an Armed State
        Log_Armed_BarAlt = 0                            'Holds the last Barometer Altitude when the APM is Armed, , now driven from EV Data.
        Log_Disarmed_BarAlt = 99999                     'Holds the last Barometer Altitude when the APM is disarmed, now driven from EV Data.
        Log_In_Flight = False                           'TRUE if the APM is in flight, now driven from EV Data.
        In_Flight_Start_Time = BaseDate                 'Holds the GPS Date and Time as the Quad takes off, now driven from EV Data.
        Mode_In_Flight_Start_Time = BaseDate            'Holds the GPS Date and Time as the Quad takes off during MOde, now driven from EV Data.
        Log_Temp_Ground_GPS_Alt = 0                     'Holds the GPS alt on Take Off, recorded in Take Off Event.


        'Declare the MODE Variables
        Log_Current_Mode = "Not Determined"              'Holds the Current Mode, i.e. STABILIZE or AUTO
        Log_Last_Mode_Changed_DateTime = BaseDate       'Holds the Date and Time the Current Mode was initialised, may not be in flight time!
        Log_Current_Mode_Time = 0                       'Holds the number of seconds since the Current Mode was initialised, may not be in flight time!
        Log_Current_Mode_Flight_Time = 0                'Holds the number of seconds recorded in the log during flight in this mode.
        STABILIZE_Flight_Time = 0                       'Holds the flight time for Stabilize Mode in Seconds
        ALT_HOLD_Flight_Time = 0                        'Holds the flight time for Alt_Hold Mode in Seconds
        LOITER_Flight_Time = 0                          'Holds the flight time for LOITER Mode in Seconds
        AUTO_Flight_Time = 0                            'Holds the flight time for AUTO Mode in Seconds
        RTL_Flight_Time = 0                             'Holds the flight time for RTL Mode in Seconds
        LAND_Flight_Time = 0                            'Holds the flight time for LAND Mode in Seconds
        OTHER_Flight_Time = 0                           'Holds the flight time for OTHER Mode in Seconds (Cover New Modes etc.)

        'Declare the CURR Variables
        LOG_CURR_ThrOut = 0                             'Holds the last value in the CURR dataline
        LOG_CURR_ThrInt = 0                             'Holds the last value in the CURR dataline
        LOG_CURR_Volt = 0                              'Holds the last value in the CURR dataline
        LOG_CURR_Curr = 0                              'Holds the last value in the CURR dataline
        LOG_CURR_Vcc = 0                               'Holds the last value in the CURR dataline
        LOG_CURR_CurrTot = 0                             'Holds the last value in the CURR dataline
        CURR_ThrottleUp = False                         'TRUE if Throttle > 0 in CURR logs
        Log_Min_VCC = 99999                             'Holds the Minimum VCC found in the Log while in Flight Only
        Log_Max_VCC = 0                                 'Holds the Maximum VCC found in the Log while in Flight Only
        Log_Min_Battery_Volts = 99999                   'Holds the Minimum Main Battery Volts found in the Log while in Flight Only.
        Log_Max_Battery_Volts = 0                       'Holds the Maximum Main Battery Volts found in the Log while in Flight Only.
        Log_Min_Battery_Current = 99999                 'Hold the Minimum Main Battery Current Drawn found in the Log while in Flight Only.
        Log_Max_Battery_Current = 0                     'Hold the Maximum Main Battery Current Drawn found in the Log while in Flight Only.
        Log_Sum_Battery_Current = 0                     'Hold the Maximum Main Battery Current Drawn found in the Log while in Flight Only.
        Log_Mode_Start_Current = 0                      'Holds the Total Current Used from the Log "CURR" recorded in the log at the start of this flight mode
        Total_Mode_Current = 0                          'Holds the Total Current Used from the Log "CURR" reading for this mode while in flight.
        Log_Total_Current = 0                           'Holds the Total Current Used from the Log "CURR" reading.
        Log_CURR_DLs = 0                                'Counts the number of datalines found in the Log for CURR results, for working out averages
        Log_CURR_DLs_for_Mode = 0                       'Counts the number of datalines found in the Log for CURR results during each mode, for working out averages
        Log_Mode_Min_Volt = 99999                       'Holds the Minimum Volt recorded in the log during flight in this mode.
        Log_Mode_Max_Volt = 0                           'Holds the Maximum Volt recorded in the log during flight in this mode.
        Log_Mode_Sum_Volt = 0                           'Holds the Sum of all Volt recorded in the log during flight in this mode.


        'Declare the CTUN Variables
        CTUN_ThrottleUp = False                         'TRUE if Throttle > 0 in CTUN logs
        CTUN_ThrOut_40 = False                          'TRUE if Throttle > 40 in CTUN logs
        Log_Maximum_Altitude = 0                        'Holds the Maximum Altitude detected during the flight from the log.
        Log_CTUN_BarAlt = 0                             'Holds the Current Barometer Altitude recorded in the log.
        Log_CTUN_ThrOut = 0                             'Holds the Current ThrOut recorded in the log from the CTUN data line.
        Log_CTUN_DLs = 0                                'Counts the number of datalines found in the Log for CTUN results, for working out averages
        Log_CTUN_DLs_for_Mode = 0                       'Counts the number of datalines found in the Log for CTUN results during each mode, for working out averages
        Log_Mode_Min_BarAlt = 99999                     'Holds the Minimum BarAlt recorded in the log during flight in this mode.
        Log_Mode_Max_BarAlt = 0                         'Holds the Maximum BarAlt recorded in the log during flight in this mode.
        Log_Mode_Sum_BarAlt = 0                         'Holds the Sum of all BarAlt recorded in the log during flight in this mode.


        'Declare the GPS Variables
        Log_GPS_GCrs = 0                                'Holds the GPS ground course in degrees (0 = north)
        Log_GPS_Status = 0                              'Holds the GPS Current status as recorded in the Log
        GPS_Base_Date = BaseDate                        'Holds the Standard GPS Base Date of 06/01/1980
        Log_GPS_TimeMS = 0                              'Holds the GPS Current Time in ms as recorded in the Log
        Log_GPS_Week = 0                                'Holds the GPS CUrrent week as recorded in the Log
        Log_GPS_DateTime = BaseDate                     'Holds the Current Date and Time as calculated from the Log
        Log_First_DateTime = BaseDate                   'Holds the first recorded Date and Time as calculated from the Log.
        Log_Last_DateTime = BaseDate                    'Holds the last recorded Date and Time as calculated from the Log.
        Log_Total_Flight_Time = 0                       'Holds the total flight time recorded in Seconds, captured by CTUN to detect landing.
        Log_Current_Flight_Time = 0                     'Holds the current flight time recorded in Seconds between take off and landing, updated live in GPS checks.
        Log_GPS_NSats = 0                               'Holds the GPS Current number of Sats
        Log_GPS_Min_NSats = 99                          'Holds the GPS Minimum number of Sats recorded in the log.
        Log_GPS_Max_NSats = 0                           'Holds the GPS Maximum number of Sats recorded in the log.
        Log_Flight_Sum_NSats = 0                        'Holds the Sum of all GPS Sats recorded in the log during flight.
        Log_Flight_Min_NSats = 99                       'Holds the GPS Minimum number of Sats recorded in the log during Flight.
        Log_Flight_Max_NSats = 0                        'Holds the GPS Maximum number of Sats recorded in the log during Flight.
        Log_Mode_Min_NSats = 99                         'Holds the GPS Minimum number of Sats recorded in the log during flight in this mode.
        Log_Mode_Max_NSats = 0                          'Holds the GPS Maximum number of Sats recorded in the log during flight in this mode.
        Log_Mode_Sum_NSats = 0                          'Holds the Sum of all GPS Sats recorded in the log during flight in this mode.
        Log_GPS_HDop = 0                                'Holds the GPS Current HDop 
        Log_GPS_Min_HDop = 99                           'Holds the Minimum HDop recorded in the log.
        Log_GPS_Max_HDop = 0                            'Holds the Maximum HDop recorded in the log.
        Log_Flight_Sum_HDop = 0                         'Holds the Sum of all HDop recorded in the log during flight.
        Log_Flight_Min_HDop = 99                        'Holds the Minimum HDop recorded in the log during flight.
        Log_Flight_Max_HDop = 0                         'Holds the Maximum HDop recorded in the log during flight.
        Log_Mode_Min_HDop = 99                          'Holds the Minimum HDop recorded in the log during flight in this mode.
        Log_Mode_Max_HDop = 0                           'Holds the Maximum HDop recorded in the log during flight in this mode.
        Log_Mode_Sum_HDop = 0                           'Holds the Sum of all HDop recorded in the log during flight in this mode.
        Log_GPS_Lat = 0                                 'Holds the current GPS Latitude
        Log_GPS_Lng = 0                                 'Holds the current GPS Longtitude
        First_GPS_Lat = 0                               'Holds the Latitude when the Qaud first takes off.
        First_GPS_Lng = 0                               'Holds the Longtitude when the Qaud first takes off.
        Last_GPS_Lat = 0                                'Holds the last Latitude Result, used to calculate distance between each GPS Log.
        Last_GPS_Lng = 0                                'Holds the last Longtitude Result, used to calculate distance between each GPS Log.
        Log_GPS_Alt = 0                                 'Holda the current GPS_Alt result.
        Log_GPS_Last_Alt = 0                            'Holds the last GPS_Alt result, used to filter spikes.
        Log_GPS_Calculated_Alt = 0                      'Calculated Altitude by taking the GPS Alt - launch GPS Alt
        GPS_Calculated_Direction = 0                    'Calculated Direction based on GPS Lat & Lng movements.
        First_In_Flight = False                         'TRUE and remains TRUE the first time Log_In_Flight goes TRUE
        Dist_From_Launch = 0                            'Calculated distance from first take off point in km (Kilometers).
        Max_Dist_From_Launch = 0                        'Calculated Maximum distance from first take off point in km (Kilometers).
        Dist_Travelled = 0                              'Calculated distance between each GPS Log in km (Kilometers).
        Mode_Min_Dist_From_Launch = 99999               'Calculated Minimum distance from first take off point in km (Kilometers) for this mode summary.
        Mode_Max_Dist_From_Launch = 0                   'Calculated Maximum distance from first take off point in km (Kilometers) for this mode summary.
        Mode_Start_Dist_Travelled = 0                   'Calculated distance when the mode changes so we can work out the distance travelled in this mode.
        Mode_Dist_Travelled = 0                         'Calculated distance between each GPS Log in km (Kilometers).
        Temp_Dist_Travelled = 0                         'Used to validate that the Distance should be added.
        Log_GPS_Spd = 0                                 'Holds the Current GPS Speed in ms
        Log_Flight_Max_Spd = 0                          'Holds the Maximum GPS Speed in ms recorded in the log during flight
        Log_Mode_Min_Spd = 99999                        'Holds the Minimum Speed recorded in the log during flight in this mode.
        Log_Mode_Max_Spd = 0                            'Holds the Maximum Speed recorded in the log during flight in this mode.
        Log_Mode_Sum_Spd = 0                            'Holds the Sum of all Speed recorded in the log during flight in this mode.
        Log_GPS_DLs = 0                                 'Counts the number of datalines found in the Log for GPS results while in flight, for working out averages
        Log_GPS_DLs_for_Mode = 0                        'Counts the number of datalines found in the Log for GPS results during each mode while in flight, for working out averages
        Log_GPS_Glitch = False                          'True if there is currently a GPS Glitch in progress

        'Declare the Return Variables that will be set by reading the Log File
        ArduVersion = ""                                'Holds the ArduCopter Version found in the Log File
        ArduBuild = ""                                  'Holds the ArduCopter Build Version found in the Log File
        ArduType = ""                                   'Holds the Ardu type determined from the log file, "ArduCopter".
        APM_Free_RAM = 0                                'Holds the APM Free RAM reported in the Log File
        APM_Version = ""                                 'Hold the APM Version Number as reported in the log file
        APM_Frame_Type = 0                              'Holds the APM Frame Type, determined from the Parmeter FRAME 
        APM_Frame_Name = ""                            'The Text Name of the Frame Type
        APM_No_Motors = 0                               'Holds the number of Motors, determined from the FMT for MOT.
        Hardware = ""                                   'Holds the type of hardware used.

        'Decalre the IMU Variables
        Log_IMU_TimeMS = 0                              'Holds the current IMU Time in ms that the last reading was taken.
        Log_IMU_GyrX = 0                                'Holds the current IMU Gyro X axis when the last reading was taken.
        Log_IMU_GyrY = 0                                'Holds the current IMU Gyro Y axis when the last reading was taken.
        Log_IMU_GyrZ = 0                                'Holds the current IMU Gyro Z axis when the last reading was taken.
        Log_IMU_AccX = 0                                'Holds the current IMU Accelorometer X axis when the last reading was taken.
        Log_IMU_AccY = 0                                'Holds the current IMU Accelorometer Y axis when the last reading was taken.
        Log_IMU_AccZ = 0                                'Holds the current IMU Accelorometer Z axis when the last reading was taken.
        Log_IMU_DLs = 0                                 'Counts the number of datalines found in the Log for IMU results, for working out averages
        Log_IMU_DLs_for_Slow_FLight = 0                 'Counts the number of datalines found in the Log for IMU results while in slow flight, for working out averages
        Log_IMU_Min_AccX = 99                           'Holds the minimum IMU Accelorometer X axis detected during a slow flight / hover.
        Log_IMU_Max_AccX = -99                          'Holds the minimum IMU Accelorometer X axis detected during a slow flight / hover.
        Log_IMU_Sum_AccX = 0                            'Holds the sum of all the IMU Accelorometer X axis detected during a slow flight / hover.
        Log_IMU_Min_AccY = 99                           'Holds the minimum IMU Accelorometer X axis detected during a slow flight / hover.
        Log_IMU_Max_AccY = -99                          'Holds the minimum IMU Accelorometer X axis detected during a slow flight / hover.
        Log_IMU_Sum_AccY = 0                            'Holds the sum of all the IMU Accelorometer Y axis detected during a slow flight / hover.
        Log_IMU_Min_AccZ = 99                           'Holds the minimum IMU Accelorometer X axis detected during a slow flight / hover.
        Log_IMU_Max_AccZ = -99                          'Holds the minimum IMU Accelorometer X axis detected during a slow flight / hover.
        Log_IMU_Sum_AccZ = 0                            'Holds the sum of all the IMU Accelorometer Y axis detected during a slow flight / hover.
        log_IMU_Min_Spd = 99                            'Holds the minimum speed detected during a slow flight / hover.
        log_IMU_Max_Spd = 0                             'Holds the maximum speed detected during a slow flight / hover.
        log_IMU_Sum_Spd = 0                             'Holds the of all speeds detected during a slow flight / hover.
        log_IMU_Min_Alt = 99                            'Holds the minimum speed detected during a slow flight / hover.
        log_IMU_Max_Alt = 0                             'Holds the maximum speed detected during a slow flight / hover.
        log_IMU_Sum_Alt = 0                             'Holds the of all speeds detected during a slow flight / hover.
        IMU_Vibration_Check = False                     'TRUE when 500 successive records have been successfully read for vibration checking.
        IMU_Vibration_Start_DL = 0                      'Holds the Dataline Number of where we started logging for vibrations
        IMU_Vibration_End_DL = 0                        'Holds the Dataline Number of where we Ended the logging for vibrations


        'Declare the ATT Variables
        Log_ATT_RollIn = 0                              'Holds current the ATT RollIn Data
        Log_ATT_Roll = 0                                'Holds current the ATT Roll Data
        Log_ATT_PitchIn = 0                             'Holds current the ATT PitchIn Data
        Log_ATT_Pitch = 0                               'Holds current the ATT Pitch Data
        Log_ATT_YawIn = 0                               'Holds current the ATT YawIn Data
        Log_ATT_Yaw = 0                                 'Holds current the ATT Yaw Data
        Log_ATT_NavYaw = 0                              'Holds current the ATT NavYaw Data

        'Declare the Logging Variables
        IMU_Logging = False                             'TRUE if Valid data found in Log File
        GPS_Logging = False                             'TRUE if Valid data found in Log File
        CTUN_Logging = False                            'TRUE if Valid data found in Log File
        PM_Logging = False                              'TRUE if Valid data found in Log File
        CURR_Logging = False                            'TRUE if Valid data found in Log File
        NTUN_Logging = False                            'TRUE if Valid data found in Log File
        MSG_Logging = False                             'TRUE if Valid data found in Log File
        ATUN_Logging = False                            'TRUE if Valid data found in Log File
        ATDE_logging = False                            'TRUE if Valid data found in Log File
        MOT_Logging = False                             'TRUE if Valid data found in Log File
        OF_Logging = False                              'TRUE if Valid data found in Log File
        MAG_Logging = False                             'TRUE if Valid data found in Log File
        CMD_Logging = False                             'TRUE if Valid data found in Log File
        ATT_Logging = False                             'TRUE if Valid data found in Log File
        INAV_Logging = False                            'TRUE if Valid data found in Log File
        MODE_Logging = False                            'TRUE if Valid data found in Log File
        STRT_logging = False                            'TRUE if Valid data found in Log File
        EV_Logging = False                              'TRUE if Valid data found in Log File
        D16_Logging = False                             'TRUE if Valid data found in Log File
        DU16_Logging = False                            'TRUE if Valid data found in Log File
        D32_Logging = False                             'TRUE if Valid data found in Log File
        DU32_Logging = False                            'TRUE if Valid data found in Log File
        DFLT_Logging = False                            'TRUE if Valid data found in Log File
        PID_Logging = False                             'TRUE if Valid data found in Log File
        CAM_Logging = False                             'TRUE if Valid data found in Log File
        ERR_Logging = False                             'TRUE if Valid data found in Log File
    End Sub
End Module
