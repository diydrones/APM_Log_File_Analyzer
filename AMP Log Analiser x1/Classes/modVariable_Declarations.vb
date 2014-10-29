Imports System.IO
Imports System.Deployment.Application

Module modVariable_Declarations

    Public MyCurrentVersionNumber As String = "v2.0.0.0"          'Update on every released version.      'frmMainForm.BuildVers()                          'Update on every released version.
    Public CurrentPublishVersionNumber As String = ""               'frmMainForm.PublishedVers()                 'Now Detected by ApplicationDeployment.CurrentDeployment.CurrentVersion


    '### DEVELOPER TEMPORARY VARIABLES, TODO ###
    Public TempMaxRollDiff As Single = 0
    Public TempMaxPitchDiff As Single = 0

    '### DEVELOPER VARIABLES ###
    Public Ignore_CTUN_Logging As Boolean = False           'TRUE if you want to pretend that CTUN is not in the logs
    Public Ignore_LOG_Version As Boolean = False            'TRUE is you want to load any log regardless of the Firmware Version that created it.
    Public Ignore_LOG_Requirements As Boolean = False       'TRUE is you want to load any log regardless of the Firmware Version that created it.
    Public Read_LOG_Percentage As Integer = 100             'Set to the percentage of the log you want to read, i.e. ignore the end.


    'Declare the Main Program Variables
    Public osVer As Version = Environment.OSVersion.Version 'Detect Windows Environment.
    Public CodeTimerStart As Double                         'Use CodeTimerStart = Format(Now, "ffff") at the start of a process.
    'Then If Format(Now, "ffff") - CodeTimerStart > 1 Then Debug.Print("Parameter Checks = " & Format(Now, "ffff") - CodeTimerStart & "μs") at the end
    Public CodeTimerStart2 As Double
    Public LogAnalysisHeader As Boolean = False             'True once the header has been written to the screen.
    Public BaseDate As Date = "06 Jan 1980"
    Public DataLine As Single = 0                           'Line Number of File being Processed.
    Public Data As String = ""                              'String of Data being processed from the file.
    Public DataArray(25) As String                          'Holds the Data Line as Split into Values
    Public DataChar As String = ""                          'Hold the Data Line Character during the Spiliting of the DataLine
    Public DataArrayCounter As Integer = 0                  'Holds the current position in the DataArray for the Next Entry.
    Public DataSplit As String = ""                         'Holds the Data Spilt so far until the end of the spilt is found.
    Public strLogFileName As String = ""                    'Holds the Current Log Filename.
    Public strLogPathFileName As String = ""                'Holds the Current Log Path and Filename.
    Public ProgramInstallationFolder As String = _
            Directory.GetCurrentDirectory                   'Holds the current installation directory
    Public TempProgramInstallationFolder As String = _
            Environment.CurrentDirectory                    'Holds the current environment directory
    Public ApplicationStartUpPath As String = _
            Application.StartupPath                         'Holds the current startup directory
    Public TempIsDeployed As Boolean = _
            ApplicationDeployment.IsNetworkDeployed         'TRUE if the application is deployed.
    Public TotalDataLines As Integer = 0                    'Holds the Total Number of Data Lines in the current log.
    Public ErrorCount As Integer = 0                        'Counts the number of errors found in the logs.
    Public ESCPress As Boolean = False                      'True if Escape is pressed
    Public ParametersVisible As Boolean = False             'True if Parameters window is currently displayed.
    Public frmMainFormHandle As Double = 0                  ' The Handle of the main form.
    Public PercentageComplete As Integer                    ' Holds the percentage complete to update the progress bar (speeds up code)

    'Declare the .ini Variables
    Public BATTERY_CAPACITY As Integer = 5000               'Holds the Models Battery Capacity
    Public MIN_VCC As Single = 4.85                         'Holds the Minimum APM VCC allowed before an error is displayed
    Public MAX_VCC As Single = 5.15                         'Holds the Maximum APM VCC allowed before an error is displayed
    Public MAX_VCC_FLUC As Single = 0.15                    'Holds the Maximum Fluctuation on the APM VCC allowed before an error is displayed
    Public BATTERY_VOLTS As Single = 11.1                   'Holds the Models Battery Voltage
    Public BATTERY_TYPE As String = "LiPo"                  'Holds the Models Battery Type, i.e. LiPo
    Public BATTERY_C_RATING As Integer = 10                 'Holds the Models Battery Minimum C Rating, i.e. 10 for 10C
    Public MIN_MODE_EFF_TIME As Integer = 60                'Holds the Minimum number of seconds in a flight mode before efficiency is calculated.
    Public MIN_EFF_TIME As Integer = 180                    'Holds the Minimum number of seconds in flight before efficiency is calculated.
    'If new variables are added then the Default .ini file
    'creation must also be updated.


    'Declare the Return Variables that will be set by reading the Log File
    Public ArduVersion As String = ""                      'Holds the Ardu***** Version found in the Log File
    Public ArduBuild As String = ""                        'Holds the Ardu***** Build Version found in the Log File
    Public ArduType As String = ""                         'Holds the Ardu type determined from the log file, "ArduCopter".
    Public APM_Free_RAM As Integer = 0                     'Holds the APM Free RAM reported in the Log File
    Public APM_Version As String = 0                       'Hold the APM Version Number as reported in the log file
    Public APM_Frame_Type As Single = 0                    'Holds the APM Frame Type, determined from the Parmeter FRAME
    Public APM_Frame_Name As String = ""                   'The Text Name of the Frame Type
    Public APM_No_Motors As Integer = 0                    'Holds the number of Motors, determined from the FMT for MOT.
    Public Hardware As String = ""                          'Holds the type of hardware used.

    'Declare the Parameter Variables
    Public Param As String = ""                             'Parameter read from the Log.
    Public Value As Integer = 0                             'Paramter Value read from the Log.
    Public Param_Issue_Found As Boolean = False             'TRUE if one or more parameters issues are found.
    Public Thr_Min As Integer = 0                           'Used to Determine the settings for Thr_Min & Mot_Spin_Armed
    Public Mot_Spin_Armed As Integer = 0                    'Used to Determine the settings for Thr_Min & Mot_Spin_Armed
    Public Log_Battery_Capacity As Integer                  'Battery Capacity as found in the APM Parameter Settings.

    'Delcare the NTUN Variables
    Public Log_NTUN_WPDst As Single = 0
    Public Log_NTUN_WPBrg As Single = 0
    Public Log_NTUN_PErX As Single = 0
    Public Log_NTUN_PErY As Single = 0
    Public Log_NTUN_DVelX As Single = 0
    Public Log_NTUN_DVelY As Single = 0
    Public Log_NTUN_VelX As Single = 0
    Public Log_NTUN_VelY As Single = 0
    Public Log_NTUN_DAcX As Single = 0
    Public Log_NTUN_DAcY As Single = 0
    Public Log_NTUN_DRol As Single = 0                      'Navigator Roll In
    Public Log_NTUN_DPit As Single = 0                      'Navigator Pitch In

    'Delcare the INAV Variables
    Public Log_INAV_Home_GLat As Double = 0                 'Hold where the APM considers home to be.
    Public Log_INAV_Home_GLng As Double = 0                 'Hold where the APM considers home to be.

    'Declare the CMD Variables
    Public Log_CMD_CTot As Integer = 0                      'CTot: the total number of commands in the mission
    Public Log_CMD_CNum As Integer = 0                      'CNum: this command’s number in the mission (0 is always home, 1 is the first command, etc)
    Public Log_CMD_CId As Integer = 0                       'CId: the mavlink message id
    Public Log_CMD_Copt As Integer = 0                      'Copt: the option parameter (used for many different purposes)
    Public Log_CMD_Prm1 As Integer = 0                      'Prm1: the command’s parameter (used for many different purposes)
    Public Log_CMD_Alt As Single = 0                        'Alt: the command’s altitude in meters
    Public Log_CMD_Lat As Double = 0                        'Lat: the command’s latitude position
    Public Log_CMD_Lng As Double = 0                        'Lng: the command’s longitude position
    Public Log_Last_CMD_Lat As Double = 0                   'Holds the previous WP Co-ordinates
    Public Log_Last_CMD_Lng As Double = 0                   'Holds the previous WP Co-ordinates
    Public Log_Last_CMD_Alt As Single = 0                   'Holds the previous WP Alititude
    Public Log_CMD_Dist1 As Single = 0                      'Distance using current GPS when we hit the WP radius to the next way point
    Public Log_CMD_Dist2 As Single = 0                      'Distance between the two way points.

    'Declare the DU32 Variables
    Public Log_DU32_ID As Integer = 0                       'Holds the ID number, 7 (bit mask of internal state) or 9 (simple mode’s initial heading in centi-degrees)
    Public Log_DU32_Value As Single = 0                     'Holds the DU32 value.
    Public Log_DU32_HomeSet As Integer = 99                 'True if DU32 is reporting Home is set, integer to handle 99 starting figure
    Public Log_DU32_SimpleMode As Integer = 99              '0=Disabled, 1=Simple, 2=Super Simple Modes
    Public Log_DU32_RC_PreArm As Integer = 99               'True if the PreArm checks have been completed.
    Public Log_DU32_ALL_PreArm As Integer = 99              'True if all the PreArm checks have been completed.
    Public Log_DU32_Landing As Integer = 99                 'Ture is Landing Detected
    Public Log_DU32_CH7_Switch As Integer = 99              'ch7 aux switch : 0 is low or false, 1 is center or true, 2 is high
    Public Log_DU32_CH8_Switch As Integer = 99              'ch8 aux switch : 0 is low or false, 1 is center or true, 2 is high
    Public Log_DU32_USB As Integer = 99                     'True if USB is Powered.
    Public Log_DU32_Compass_Mot As Integer = 99             'True if compassmot is being calibrated.
    Public Log_DU32_Receiver As Integer = 99                'True if a receiver is present.
    Public Log_DU32_AutoWait As Integer = 99                'True if Auto mode is waiting for Throttle.
    Public USB_Warning1 As Boolean = False                  'True if the USB warning has been presented to the user when APM thinks it has a USB connection while in flight

    'Declare the Error Variables
    Public strErrorCode As String = ""                      'Holds the Main Error Code.
    Public strECode As String = ""                          'Holds the Sub Error Code.

    'Declare the PM Variables
    Public Log_PM_RenCnt As Integer = 0                     'DCM renormalization count
    Public Log_PM_RenBlw As Integer = 0                     'DCM renormalization blow-up count
    Public Log_PM_NLon As Integer = 0                       'number of long running main loops
    Public Log_PM_NLoop As Integer = 0                      'the total number of loops since the last PM message was displayed
    Public Log_PM_MaxT As Integer = 0                       'the maximum time that any loop took since the last PM message.
    Public Log_PM_PMT As Integer = 0                        'a number that increments each time a heart beat is received from the ground station, should be 10
    Public Log_GCS_Attached As Boolean = False              'True once we have a valid 100% (PMT = 10) signal from the GCS
    Public Log_PM_I2CErr As Integer = 0                     'the number of I2C errors since the last PM message.
    Public Log_PM_INSErr As Integer = 0                     'MPU6k spi bus errors
    Public Log_PM_INAVErr As Integer = 0                    'Counts the number of Navigation Errors during flight
    Public PM_Delay_Counter As Integer = 0                  'Counts each PM log found, PM errors on reported when it reaches > x (default = 3)
    Public PM_Last_PMT As Integer = 10                      'The previous PMT reading before the current reading, used to stop repeats.
    Public PM_Last_INAVErr As Integer = 0                   'The previous INAVErr reading before the current reading, used to stop repeats.

    'Declare the EV Variables
    Public Log_Ground_BarAlt As Integer = 0                 'Holds the last Barometer Altitude detected during lift off, now driven from EV Data.
    Public Log_Armed = False                                'TRUE if the APM is in an Armed State
    Public Log_Armed_BarAlt As Integer = 0                  'Holds the last Barometer Altitude when the APM is Armed, , now driven from EV Data.
    Public Log_Disarmed_BarAlt As Integer = 99999           'Holds the last Barometer Altitude when the APM is disarmed, now driven from EV Data.
    Public Log_In_Flight As Boolean = False                 'TRUE if the APM is in flight, now driven from EV Data.
    Public In_Flight_Start_Time As Date = BaseDate          'Holds the GPS Date and Time as the Quad takes off, now driven from EV Data.
    Public Mode_In_Flight_Start_Time As Date = BaseDate     'Holds the GPS Date and Time as the Quad takes off during MOde, now driven from EV Data.
    Public Log_Temp_Ground_GPS_Alt As Integer = 0           'Holds the GPS alt on Take Off, recorded in Take Off Event.


    'Declare the MODE Variables
    Public Log_Current_Mode As String = "Not Determined"    'Holds the Current Mode, i.e. STABILIZE or AUTO
    Public Log_Last_Mode_Changed_DateTime As Date = BaseDate    'Holds the Date and Time the Current Mode was initialised, may not be in flight time!
    Public Log_Current_Mode_Time As Integer = 0             'Holds the number of seconds since the Current Mode was initialised, may not be in flight time!
    Public Log_Current_Mode_Flight_Time As Integer = 0      'Holds the number of seconds recorded in the log during flight in this mode.
    Public STABILIZE_Flight_Time As Integer = 0             'Holds the flight time for Stabilize Mode in Seconds
    Public ALT_HOLD_Flight_Time As Integer = 0              'Holds the flight time for Alt_Hold Mode in Seconds
    Public LOITER_Flight_Time As Integer = 0                'Holds the flight time for LOITER Mode in Seconds
    Public AUTO_Flight_Time As Integer = 0                  'Holds the flight time for AUTO Mode in Seconds
    Public RTL_Flight_Time As Integer = 0                   'Holds the flight time for RTL Mode in Seconds
    Public LAND_Flight_Time As Integer = 0                  'Holds the flight time for LAND Mode in Seconds
    Public OTHER_Flight_Time As Integer = 0                 'Holds the flight time for OTHER Mode in Seconds (Cover New Modes etc.)
    Public FBW_A_Flight_Time As Integer = 0                 'Holds the flight time for FBW_A Mode in Seconds
    Public AUTOTUNE_Flight_Time As Integer = 0              'Holds the flight time for AUTOTUNE Mode in Seconds
    Public MANUAL_Flight_Time As Integer = 0                'Holds the flight time for MANUAL Mode in Seconds


    'Declare the CURR Variables
    Public LOG_CURR_ThrOut As Single                        'Holds the last value in the CURR dataline
    Public LOG_CURR_ThrInt As Single                        'Holds the last value in the CURR dataline
    Public LOG_CURR_Volt As Single                          'Holds the last value in the CURR dataline
    Public LOG_CURR_Curr As Single                          'Holds the last value in the CURR dataline
    Public LOG_CURR_Vcc As Single                           'Holds the last value in the CURR dataline
    Public LOG_CURR_CurrTot As Single                       'Holds the last value in the CURR dataline
    Public CURR_ThrottleUp As Boolean = False               'TRUE if Throttle > 0 in CURR logs
    Public Log_Min_VCC As Single = 99999                    'Holds the Minimum VCC found in the Log while in Flight Only
    Public Log_Max_VCC As Single = 0                        'Holds the Maximum VCC found in the Log while in Flight Only
    Public Log_Min_Battery_Volts As Single = 99999          'Holds the Minimum Main Battery Volts found in the Log while in Flight Only.
    Public Log_Max_Battery_Volts As Single = 0              'Holds the Maximum Main Battery Volts found in the Log while in Flight Only.
    Public Log_Min_Battery_Current As Single = 99999        'Hold the Minimum Main Battery Current Drawn found in the Log while in Flight Only.
    Public Log_Max_Battery_Current As Single = 0            'Hold the Maximum Main Battery Current Drawn found in the Log while in Flight Only.
    Public Log_Sum_Battery_Current As Single = 0            'Hold the Maximum Main Battery Current Drawn found in the Log while in Flight Only.
    Public Log_Mode_Start_Current As Single = 0             'Holds the Total Current Used from the Log "CURR" recorded in the log at the start of this flight mode
    Public Total_Mode_Current As Integer = 0                'Holds the Total Current Used from the Log "CURR" reading for this mode while in flight.
    Public Log_Total_Current As Integer = 0                 'Holds the Total Current Used from the Log "CURR" reading.
    Public Log_CURR_DLs As Integer = 0                      'Counts the number of datalines found in the Log for CURR results, for working out averages
    Public Log_CURR_DLs_for_Mode As Integer = 0             'Counts the number of datalines found in the Log for CURR results during each mode, for working out averages
    Public Log_Mode_Min_Volt As Integer = 99999             'Holds the Minimum Volt recorded in the log during flight in this mode.
    Public Log_Mode_Max_Volt As Integer = 0                 'Holds the Maximum Volt recorded in the log during flight in this mode.
    Public Log_Mode_Sum_Volt As Integer = 0                 'Holds the Sum of all Volt recorded in the log during flight in this mode.


    'Declare the CTUN Variables
    Public CTUN_ThrottleUp As Boolean = False               'TRUE if Throttle > 0 in CTUN logs
    Public CTUN_ThrOut_40 As Boolean = False                'True if Throttle > 40 in CTUN logs
    Public Log_Maximum_Altitude As Integer = 0              'Holds the Maximum Altitude detected during the flight from the log.
    Public Log_CTUN_BarAlt As Single = 0                    'Holds the Current Barometer Altitude recorded in the log.
    Public Log_CTUN_ThrOut As Integer = 0                   'Holds the Current ThrOut recorded in the log from the CTUN data line.
    Public Log_CTUN_ThrIn As Single = 0
    Public Log_CTUN_SonAlt As Single = 0
    Public Log_CTUN_WPAlt As Single = 0
    Public Log_CTUN_DesSonAlt As Single = 0
    Public Log_CTUN_AngBst As Single = 0
    Public Log_CTUN_CRate As Single = 0
    Public Log_CTUN_DCRate As Single = 0

    Public Log_CTUN_DLs As Integer = 0                      'Counts the number of datalines found in the Log for CTUN results, for working out averages
    Public Log_CTUN_DLs_for_Mode As Integer = 0             'Counts the number of datalines found in the Log for CTUN results during each mode, for working out averages
    Public Log_Mode_Min_BarAlt As Integer = 99999           'Holds the Minimum BarAlt recorded in the log during flight in this mode.
    Public Log_Mode_Max_BarAlt As Integer = 0               'Holds the Maximum BarAlt recorded in the log during flight in this mode.
    Public Log_Mode_Sum_BarAlt As Integer = 0               'Holds the Sum of all BarAlt recorded in the log during flight in this mode.



    'Decalre the IMU Variables
    Public Log_IMU_TimeMS As Integer = 0                    'Holds the current IMU Time in ms that the last reading was taken.
    Public Log_IMU_GyrX As Single = 0                       'Holds the current IMU Gyro X axis when the last reading was taken.
    Public Log_IMU_GyrY As Single = 0                       'Holds the current IMU Gyro Y axis when the last reading was taken.
    Public Log_IMU_GyrZ As Single = 0                       'Holds the current IMU Gyro Z axis when the last reading was taken.
    Public Log_IMU_AccX As Single = 0                       'Holds the current IMU Accelorometer X axis when the last reading was taken.
    Public Log_IMU_AccY As Single = 0                       'Holds the current IMU Accelorometer Y axis when the last reading was taken.
    Public Log_IMU_AccZ As Single = 0                       'Holds the current IMU Accelorometer Z axis when the last reading was taken.
    Public Log_IMU_DLs As Integer = 0                       'Counts the number of datalines found in the Log for IMU results, for working out averages
    Public Log_IMU_DLs_for_Slow_FLight As Integer = 0       'Counts the number of datalines found in the Log for IMU results while in slow flight, for working out averages
    Public Log_IMU_Min_AccX As Single = 99                  'Holds the minimum IMU Accelorometer X axis detected during a slow flight / hover.
    Public Log_IMU_Max_AccX As Single = -99                 'Holds the minimum IMU Accelorometer X axis detected during a slow flight / hover.
    Public Log_IMU_Sum_AccX As Single = 0                   'Holds the sum of all the IMU Accelorometer X axis detected during a slow flight / hover.
    Public Log_IMU_Min_AccY As Single = 99                  'Holds the minimum IMU Accelorometer X axis detected during a slow flight / hover.
    Public Log_IMU_Max_AccY As Single = -99                 'Holds the minimum IMU Accelorometer X axis detected during a slow flight / hover.
    Public Log_IMU_Sum_AccY As Single = 0                   'Holds the sum of all the IMU Accelorometer Y axis detected during a slow flight / hover.
    Public Log_IMU_Min_AccZ As Single = 99                  'Holds the minimum IMU Accelorometer X axis detected during a slow flight / hover.
    Public Log_IMU_Max_AccZ As Single = -99                 'Holds the minimum IMU Accelorometer X axis detected during a slow flight / hover.
    Public Log_IMU_Sum_AccZ As Single = 0                   'Holds the sum of all the IMU Accelorometer Y axis detected during a slow flight / hover.
    Public log_IMU_Min_Spd As Integer = 99                  'Holds the minimum speed detected during a slow flight / hover.
    Public log_IMU_Max_Spd As Integer = 0                   'Holds the maximum speed detected during a slow flight / hover.
    Public log_IMU_Sum_Spd As Integer = 0                   'Holds the of all speeds detected during a slow flight / hover.
    Public log_IMU_Min_Alt As Integer = 99                  'Holds the minimum altitude detected during a slow flight / hover.
    Public log_IMU_Max_Alt As Integer = 0                   'Holds the maximum altitude detected during a slow flight / hover.
    Public log_IMU_Sum_Alt As Integer = 0                   'Holds the of all altitude detected during a slow flight / hover.
    Public IMU_Vibration_Check As Boolean = False           'TRUE when 500 successive records have been successfully read for vibration checking.
    Public IMU_Vibration_Start_DL As Single = 0             'Holds the Dataline Number of where we started logging for vibrations
    Public IMU_Vibration_End_DL As Single = 0               'Holds the Dataline Number of where we Ended the logging for vibrations

    'Declare the ATT Variables
    Public Log_ATT_RollIn As Single = 0                     'Holds current the ATT RollIn Data
    Public Log_ATT_Roll As Single = 0                       'Holds current the ATT Roll Data
    Public Log_ATT_PitchIn As Single = 0                    'Holds current the ATT PitchIn Data
    Public Log_ATT_Pitch As Single = 0                      'Holds current the ATT Pitch Data
    Public Log_ATT_YawIn As Single = 0                      'Holds current the ATT YawIn Data
    Public Log_ATT_Yaw As Single = 0                        'Holds current the ATT Yaw Data
    Public Log_ATT_NavYaw As Single = 0                     'Holds current the ATT NavYaw Data

    'Declare the Logging Variables
    Public IMU_Logging As Boolean = False                   'TRUE if Valid data found in Log File
    Public GPS_Logging As Boolean = False                   'TRUE if Valid data found in Log File
    Public CTUN_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Public PM_Logging As Boolean = False                    'TRUE if Valid data found in Log File
    Public CURR_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Public NTUN_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Public MSG_Logging As Boolean = False                   'TRUE if Valid data found in Log File
    Public ATUN_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Public ATDE_logging As Boolean = False                  'TRUE if Valid data found in Log File
    Public MOT_Logging As Boolean = False                   'TRUE if Valid data found in Log File
    Public OF_Logging As Boolean = False                    'TRUE if Valid data found in Log File
    Public MAG_Logging As Boolean = False                   'TRUE if Valid data found in Log File
    Public CMD_Logging As Boolean = False                   'TRUE if Valid data found in Log File
    Public ATT_Logging As Boolean = False                   'TRUE if Valid data found in Log File
    Public INAV_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Public MODE_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Public STRT_logging As Boolean = False                  'TRUE if Valid data found in Log File
    Public EV_Logging As Boolean = False                    'TRUE if Valid data found in Log File
    Public D16_Logging As Boolean = False                   'TRUE if Valid data found in Log File
    Public DU16_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Public D32_Logging As Boolean = False                   'TRUE if Valid data found in Log File
    Public DU32_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Public DFLT_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Public PID_Logging As Boolean = False                   'TRUE if Valid data found in Log File
    Public CAM_Logging As Boolean = False                   'TRUE if Valid data found in Log File
    Public ERR_Logging As Boolean = False                   'TRUE if Valid data found in Log File

    Public FileOpened As Boolean = False                    'TRUE if file opened. False if no file opened or opened Cancelled



    'Declare the GPS Variables
    Public Log_GPS_Glitch As Boolean = False                'TRUE if there is currently a GPS Glitch in progress.
    Public Log_GPS_GCrs As Integer = 0                      'Holds the GPS ground course in degrees (0 = north)
    Public Log_GPS_Status As Integer = 0                    'Holds the GPS Current status as recorded in the Log
    Public GPS_Base_Date As Date = BaseDate                 'Holds the Standard GPS Base Date of 06/01/1980
    Public Log_GPS_TimeMS As Integer = 0                    'Holds the GPS Current Time in ms as recorded in the Log
    Public Log_GPS_Week As Integer = 0                      'Holds the GPS CUrrent week as recorded in the Log
    Public Log_GPS_DateTime As Date = BaseDate              'Holds the Current Date and Time as calculated from the Log
    Public Log_First_DateTime As Date = BaseDate            'Holds the first recorded Date and Time as calculated from the Log.
    Public Log_Last_DateTime As Date = BaseDate             'Holds the last recorded Date and Time as calculated from the Log.
    Public Log_Total_Flight_Time As Integer = 0             'Holds the total flight time recorded in Seconds, captured by CTUN to detect landing.
    Public Log_Current_Flight_Time As Integer = 0           'Holds the current flight time recorded in Seconds between take off and landing, updated live in GPS checks.
    Public Log_GPS_NSats As Integer = 0                     'Holds the GPS Current number of Sats
    Public Log_GPS_Min_NSats As Integer = 99                'Holds the GPS Minimum number of Sats recorded in the log.
    Public Log_GPS_Max_NSats As Integer = 0                 'Holds the GPS Maximum number of Sats recorded in the log.
    Public Log_Flight_Sum_NSats As Integer = 0              'Holds the Sum of all GPS Sats recorded in the log during flight.
    Public Log_Flight_Min_NSats As Integer = 99             'Holds the GPS Minimum number of Sats recorded in the log during Flight.
    Public Log_Flight_Max_NSats As Integer = 0              'Holds the GPS Maximum number of Sats recorded in the log during Flight.
    Public Log_Mode_Min_NSats As Integer = 99               'Holds the GPS Minimum number of Sats recorded in the log during flight in this mode.
    Public Log_Mode_Max_NSats As Integer = 0                'Holds the GPS Maximum number of Sats recorded in the log during flight in this mode.
    Public Log_Mode_Sum_NSats As Integer = 0                'Holds the Sum of all GPS Sats recorded in the log during flight in this mode.
    Public Log_GPS_HDop As Single = 0                       'Holds the GPS Current HDop 
    Public Log_GPS_Min_HDop As Single = 99                  'Holds the Minimum HDop recorded in the log.
    Public Log_GPS_Max_HDop As Single = 0                   'Holds the Maximum HDop recorded in the log.
    Public Log_Flight_Sum_HDop As Single = 0                'Holds the Sum of all HDop recorded in the log during flight.
    Public Log_Flight_Min_HDop As Single = 99               'Holds the Minimum HDop recorded in the log during flight.
    Public Log_Flight_Max_HDop As Single = 0                'Holds the Maximum HDop recorded in the log during flight.
    Public Log_Mode_Min_HDop As Single = 99                 'Holds the Minimum HDop recorded in the log during flight in this mode.
    Public Log_Mode_Max_HDop As Single = 0                  'Holds the Maximum HDop recorded in the log during flight in this mode.
    Public Log_Mode_Sum_HDop As Single = 0                  'Holds the Sum of all HDop recorded in the log during flight in this mode.
    Public Log_GPS_Lat As Double = 0                        'Holds the current GPS Latitude
    Public Log_GPS_Lng As Double = 0                        'Holds the current GPS Longtitude
    Public First_GPS_Lat As Double = 0                      'Holds the Latitude when the Qaud first takes off.
    Public First_GPS_Lng As Double = 0                      'Holds the Longtitude when the Qaud first takes off.
    Public Last_GPS_Lat As Double = 0                       'Holds the last Latitude Result, used to calculate distance between each GPS Log.
    Public Last_GPS_Lng As Double = 0                       'Holds the last Longtitude Result, used to calculate distance between each GPS Log.
    Public Log_GPS_Alt As Integer = 0                       'Holds the current GPS_Alt result.
    Public Log_GPS_Last_Alt As Integer = 0                  'Holds the last GPS_Alt result, used to filter spikes.
    Public Log_GPS_Calculated_Alt As Integer = 0            'Calculated Altitude by taking the GPS Alt - launch GPS Alt
    Public GPS_Calculated_Direction As Integer = 0          'Calculated Direction based on GPS Lat & Lng movements.
    Public First_In_Flight As Boolean = False               'TRUE and remains TRUE the first time Log_In_Flight goes TRUE
    Public Dist_From_Launch As Single = 0                   'Calculated distance from first take off point in km (Kilometers).
    Public Max_Dist_From_Launch As Single                   'Calculated Maximum distance from first take off point in km (Kilometers).
    Public Dist_Travelled As Single = 0                     'Calculated distance between each GPS Log in km (Kilometers).
    Public Mode_Min_Dist_From_Launch As Single = 99999      'Calculated Minimum distance from first take off point in km (Kilometers) for this mode summary.
    Public Mode_Max_Dist_From_Launch As Single = 0          'Calculated Maximum distance from first take off point in km (Kilometers) for this mode summary.
    Public Mode_Start_Dist_Travelled As Single = 0          'Calculated distance when the mode changes so we can work out the distance travelled in this mode.
    Public Mode_Dist_Travelled As Single = 0                'Calculated distance between each GPS Log in km (Kilometers).
    Public Temp_Dist_Travelled As Single                    'Used to validate that the Distance should be added.
    Public Log_GPS_Spd As Single = 0                        'Holds the Current GPS Speed in ms
    Public Log_Flight_Max_Spd As Integer = 0                'Holds the Maximum GPS Speed in ms recorded in the log during flight
    Public Log_Mode_Min_Spd As Integer = 99999              'Holds the Minimum Speed recorded in the log during flight in this mode.
    Public Log_Mode_Max_Spd As Integer = 0                  'Holds the Maximum Speed recorded in the log during flight in this mode.
    Public Log_Mode_Sum_Spd As Integer = 0                  'Holds the Sum of all Speed recorded in the log during flight in this mode.
    Public Log_GPS_DLs As Integer = 0                       'Counts the number of datalines found in the Log for GPS results while in flight, for working out averages
    Public Log_GPS_DLs_for_Mode As Integer = 0              'Counts the number of datalines found in the Log for GPS results during each mode while in flight, for working out averages

End Module
