Imports System
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Deployment.Application
Imports System.Globalization
Imports System.Threading
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Drawing.Printing

Public Class frmMainForm

#Region "     Form Position Code Start & Constants "
    'Form Position Code Start
    Public Const WM_NCLBUTTONDOWN As Integer = &HA1
    Public Const HTBORDER As Integer = 18
    Public Const HTBOTTOM As Integer = 15
    Public Const HTBOTTOMLEFT As Integer = 16
    Public Const HTBOTTOMRIGHT As Integer = 17
    Public Const HTCAPTION As Integer = 2
    Public Const HTLEFT As Integer = 10
    Public Const HTRIGHT As Integer = 11
    Public Const HTTOP As Integer = 12
    Public Const HTTOPLEFT As Integer = 13
    Public Const HTTOPRIGHT As Integer = 14
    Public Const EM_GETLINECOUNT = &HBA
    Public Const EM_LINESCROLL = &HB6

    Private Sub SavePosition(ByVal frm As Form, ByVal app_name As String)
        SaveSetting(app_name, "Geometry", "WindowState", frm.WindowState)
        If frm.WindowState = FormWindowState.Normal Then
            SaveSetting(app_name, "Geometry", "Left", frm.Left)
            SaveSetting(app_name, "Geometry", "Top", frm.Top)
            SaveSetting(app_name, "Geometry", "Width", frm.Width)
            SaveSetting(app_name, "Geometry", "Height", frm.Height)
        Else
            SaveSetting(app_name, "Geometry", "Left", frm.RestoreBounds.Left)
            SaveSetting(app_name, "Geometry", "Top", frm.RestoreBounds.Top)
            SaveSetting(app_name, "Geometry", "Width", frm.RestoreBounds.Width)
            SaveSetting(app_name, "Geometry", "Height", frm.RestoreBounds.Height)
        End If
    End Sub
    Private Sub RestorePosition(ByVal frm As Form, ByVal app_name As String)
        frm.SetBounds( _
            GetSetting(app_name, "Geometry", "Left", Me.RestoreBounds.Left), _
            GetSetting(app_name, "Geometry", "Top", Me.RestoreBounds.Top), _
            GetSetting(app_name, "Geometry", "Width", Me.RestoreBounds.Width), _
            GetSetting(app_name, "Geometry", "Height", Me.RestoreBounds.Height) _
        )
        Me.WindowState = GetSetting(app_name, "Geometry", "WindowState", Me.WindowState)
    End Sub
    'Form Position Code End


#End Region

    'Declare the Version Number

    'Declare the Version Number
    Shared Function BuildVers()
        Dim MyCurrentVersionNumber As String = "v1.0.3.4"          'Update on every released version.
        Return MyCurrentVersionNumber
    End Function
    Shared Function PublishedVers()
        Dim CurrentPublishVersionNumber As String = ""                  'Now Detected by ApplicationDeployment.CurrentDeployment.CurrentVersion
        Return CurrentPublishVersionNumber
    End Function
    Dim MyCurrentVersionNumber As String = frmMainForm.BuildVers()       'Update on every released version.
    Dim CurrentPublishVersionNumber As String = frmMainForm.PublishedVers()                 'Now Detected by ApplicationDeployment.CurrentDeployment.CurrentVersion


    '### DEVELOPER VARIABLES ###
    Dim Ignore_CTUN_Logging As Boolean = False                  'TRUE if you want to pretend that CTUN is not in the logs
    Dim Ignore_LOG_Version As Boolean = False                   'TRUE is you want to load any log regardless of the Firmware Version that created it.
    Dim Ignore_LOG_Requirements As Boolean = False              'TRUE is you want to load any log regardless of the Firmware Version that created it.
    Dim Read_LOG_Percentage As Integer = 100                    'Set to the percentage of the log you want to read, i.e. ignore the end.

    '### DEVELOPER TEMPORARY VARIABLES ###
    Dim TempMaxRollDiff As Single = 0
    Dim TempMaxPitchDiff As Single = 0


    'Declare the program Variables
    Dim LogAnalysisHeader As Boolean = False            'True once the header has been written to the screen.
    Dim BaseDate As Date = "06 Jan 1980"
    Dim DataLine As Single = 0                         'Line Number of File being Processed.
    Dim Data As String = ""                             'String of Data being processed from the file.
    Dim DataArray(25) As String                         'Holds the Data Line as Split into Values
    Dim DataChar As String = ""                         'Hold the Data Line Character during the Spiliting of the DataLine
    Dim DataArrayCounter As Integer = 0                 'Holds the current position in the DataArray for the Next Entry.
    Dim DataSplit As String = ""                        'Holds the Data Spilt so far until the end of the spilt is found.
    Dim strLogFileName As String = ""                   'Holds the Current Log Filename.
    Dim strLogPathFileName As String = ""               'Holds the Current Log Path and Filename.
    Dim ProgramInstallationFolder As String = _
            Directory.GetCurrentDirectory               'Holds the current installation directory
    Dim TempProgramInstallationFolder As String = _
            Environment.CurrentDirectory                'Holds the current environment directory
    Dim ApplicationStartUpPath As String = _
            Application.StartupPath                     'Holds the current startup directory
    Dim TempIsDeployed As Boolean = _
            ApplicationDeployment.IsNetworkDeployed     'TRUE if the application is deployed.
    Dim TotalDataLines As Integer = 0                   'Holds the Total Number of Data Lines in the current log.
    Dim ErrorCount As Integer = 0                       'Counts the number of errors found in the logs.
    Dim ESCPress As Boolean = False                     'True if Escape is pressed


    'Declare the .ini Variables
    Dim BATTERY_CAPACITY As Integer = 5000              'Holds the Models Battery Capacity
    Dim MIN_VCC As Single = 4.85                        'Holds the Minimum APM VCC allowed before an error is displayed
    Dim MAX_VCC As Single = 5.15                        'Holds the Maximum APM VCC allowed before an error is displayed
    Dim MAX_VCC_FLUC As Single = 0.15                   'Holds the Maximum Fluctuation on the APM VCC allowed before an error is displayed
    Dim BATTERY_VOLTS As Single = 11.1                  'Holds the Models Battery Voltage
    Dim BATTERY_TYPE As String = "LiPo"                 'Holds the Models Battery Type, i.e. LiPo
    Dim BATTERY_C_RATING As Integer = 10                'Holds the Models Battery Minimum C Rating, i.e. 10 for 10C
    Dim MIN_MODE_EFF_TIME As Integer = 60               'Holds the Minimum number of seconds in a flight mode before efficiency is calculated.
    Dim MIN_EFF_TIME As Integer = 180                   'Holds the Minimum number of seconds in flight before efficiency is calculated.
    'If new variables are added then the Default .ini file
    'creation must also be updated.


    'Declare the Parameter Variables
    Dim Param As String = ""                            'Parameter read from the Log.
    Dim Value As Integer = 0                            'Paramter Value read from the Log.
    Dim Param_Issue_Found As Boolean = False            'TRUE if one or more parameters issues are found.
    Dim Thr_Min As Integer = 0                          'Used to Determine the settings for Thr_Min & Mot_Spin_Armed
    Dim Mot_Spin_Armed As Integer = 0                   'Used to Determine the settings for Thr_Min & Mot_Spin_Armed
    Dim Log_Battery_Capacity As Integer                 'Battery Capacity as found in the APM Parameter Settings.

    'Delcare the INAV Variables
    Dim Log_INAV_Home_GLat As Double = 0                 'Hold where the APM considers home to be.
    Dim Log_INAV_Home_GLng As Double = 0                 'Hold where the APM considers home to be.

    'Declare the CMD Variables
    Dim Log_CMD_CTot As Integer = 0                     'CTot: the total number of commands in the mission
    Dim Log_CMD_CNum As Integer = 0                     'CNum: this command’s number in the mission (0 is always home, 1 is the first command, etc)
    Dim Log_CMD_CId As Integer = 0                      'CId: the mavlink message id
    Dim Log_CMD_Copt As Integer = 0                     'Copt: the option parameter (used for many different purposes)
    Dim Log_CMD_Prm1 As Integer = 0                     'Prm1: the command’s parameter (used for many different purposes)
    Dim Log_CMD_Alt As Single = 0                       'Alt: the command’s altitude in meters
    Dim Log_CMD_Lat As Double = 0                       'Lat: the command’s latitude position
    Dim Log_CMD_Lng As Double = 0                       'Lng: the command’s longitude position
    Dim Log_Last_CMD_Lat As Double = 0                      'Holds the previous WP Co-ordinates
    Dim Log_Last_CMD_Lng As Double = 0                      'Holds the previous WP Co-ordinates
    Dim Log_Last_CMD_Alt As Single = 0                      'Holds the previous WP Alititude
    Dim Log_CMD_Dist1 As Single = 0                    'Distance using current GPS when we hit the WP radius to the next way point
    Dim Log_CMD_Dist2 As Single = 0                     'Distance between the two way points.

    'Declare the DU32 Variables
    Dim Log_DU32_ID As Integer = 0                      'Holds the ID number, 7 (bit mask of internal state) or 9 (simple mode’s initial heading in centi-degrees)
    Dim Log_DU32_Value As Single = 0                   'Holds the DU32 value.
    Dim Log_DU32_HomeSet As Integer = 99             'True if DU32 is reporting Home is set, integer to handle 99 starting figure
    Dim Log_DU32_SimpleMode As Integer = 99             '0=Disabled, 1=Simple, 2=Super Simple Modes
    Dim Log_DU32_RC_PreArm As Integer = 99              'True if the PreArm checks have been completed.
    Dim Log_DU32_ALL_PreArm As Integer = 99             'True if all the PreArm checks have been completed.
    Dim Log_DU32_Landing As Integer = 99                'Ture is Landing Detected
    Dim Log_DU32_CH7_Switch As Integer = 99             'ch7 aux switch : 0 is low or false, 1 is center or true, 2 is high
    Dim Log_DU32_CH8_Switch As Integer = 99             'ch8 aux switch : 0 is low or false, 1 is center or true, 2 is high
    Dim Log_DU32_USB As Integer = 99                      'True if USB is Powered.
    Dim Log_DU32_Compass_Mot As Integer = 99            'True if compassmot is being calibrated.
    Dim Log_DU32_Receiver As Integer = 99               'True if a receiver is present.
    Dim Log_DU32_AutoWait As Integer = 99               'True if Auto mode is waiting for Throttle.
    Dim USB_Warning1 As Boolean = False                 'True if the USB warning has been presented to the user when APM thinks it has a USB connection while in flight

    'Declare the Error Variables
    Dim strErrorCode As String = ""                     'Holds the Main Error Code.
    Dim strECode As String = ""                         'Holds the Sub Error Code.

    'Declare the PM Variables
    Dim Log_PM_RenCnt As Integer = 0                           'DCM renormalization count
    Dim Log_PM_RenBlw As Integer = 0                           'DCM renormalization blow-up count
    Dim Log_PM_NLon As Integer = 0                             'number of long running main loops
    Dim Log_PM_NLoop As Integer = 0                            'the total number of loops since the last PM message was displayed
    Dim Log_PM_MaxT As Integer = 0                                'the maximum time that any loop took since the last PM message.
    Dim Log_PM_PMT As Integer = 0                              'a number that increments each time a heart beat is received from the ground station, should be 10
    Dim Log_GCS_Attached As Boolean = False                     'True once we have a valid 100% (PMT = 10) signal from the GCS
    Dim Log_PM_I2CErr As Integer = 0                           'the number of I2C errors since the last PM message.
    Dim Log_PM_INSErr As Integer = 0                           'MPU6k spi bus errors
    Dim Log_PM_INAVErr As Integer = 0                           'Counts the number of Navigation Errors during flight
    Dim PM_Delay_Counter As Integer = 0                         'Counts each PM log found, PM errors on reported when it reaches > x (default = 3)
    Dim PM_Last_PMT As Integer = 10                              'The previous PMT reading before the current reading, used to stop repeats.
    Dim PM_Last_INAVErr As Integer = 0                          'The previous INAVErr reading before the current reading, used to stop repeats.

    'Declare the EV Variables
    Dim Log_Ground_BarAlt As Integer = 0                'Holds the last Barometer Altitude detected during lift off, now driven from EV Data.
    Dim Log_Armed = False                               'TRUE if the APM is in an Armed State
    Dim Log_Armed_BarAlt As Integer = 0                 'Holds the last Barometer Altitude when the APM is Armed, , now driven from EV Data.
    Dim Log_Disarmed_BarAlt As Integer = 99999          'Holds the last Barometer Altitude when the APM is disarmed, now driven from EV Data.
    Dim Log_In_Flight As Boolean = False                'TRUE if the APM is in flight, now driven from EV Data.
    Dim In_Flight_Start_Time As Date = BaseDate       'Holds the GPS Date and Time as the Quad takes off, now driven from EV Data.
    Dim Mode_In_Flight_Start_Time As Date = BaseDate  'Holds the GPS Date and Time as the Quad takes off during MOde, now driven from EV Data.
    Dim Log_Temp_Ground_GPS_Alt As Integer = 0          'Holds the GPS alt on Take Off, recorded in Take Off Event.


    'Declare the MODE Variables
    Dim Log_Current_Mode As String = "Not Determined"    'Holds the Current Mode, i.e. STABILIZE or AUTO
    Dim Log_Last_Mode_Changed_DateTime As Date = BaseDate 'Holds the Date and Time the Current Mode was initialised, may not be in flight time!
    Dim Log_Current_Mode_Time As Integer = 0            'Holds the number of seconds since the Current Mode was initialised, may not be in flight time!
    Dim Log_Current_Mode_Flight_Time As Integer = 0     'Holds the number of seconds recorded in the log during flight in this mode.
    Dim STABILIZE_Flight_Time As Integer = 0            'Holds the flight time for Stabilize Mode in Seconds
    Dim ALT_HOLD_Flight_Time As Integer = 0             'Holds the flight time for Alt_Hold Mode in Seconds
    Dim LOITER_Flight_Time As Integer = 0               'Holds the flight time for LOITER Mode in Seconds
    Dim AUTO_Flight_Time As Integer = 0                 'Holds the flight time for AUTO Mode in Seconds
    Dim RTL_Flight_Time As Integer = 0                  'Holds the flight time for RTL Mode in Seconds
    Dim LAND_Flight_Time As Integer = 0                 'Holds the flight time for LAND Mode in Seconds
    Dim OTHER_Flight_Time As Integer = 0                'Holds the flight time for OTHER Mode in Seconds (Cover New Modes etc.)
    Dim FBW_A_Flight_Time As Integer = 0                'Holds the flight time for FBW_A Mode in Seconds
    Dim AUTOTUNE_Flight_Time As Integer = 0             'Holds the flight time for AUTOTUNE Mode in Seconds
    Dim MANUAL_Flight_Time As Integer = 0               'Holds the flight time for MANUAL Mode in Seconds

    'Declare the CURR Variables
    Dim Value1 As Single                                'Temporary Variable to hold the DataLine values
    Dim Value3 As Single                                'Temporary Variable to hold the DataLine values
    Dim Value4 As Single                                'Temporary Variable to hold the DataLine values
    Dim Value5 As Single                                'Temporary Variable to hold the DataLine values
    Dim Value6 As Single                                'Temporary Variable to hold the DataLine values
    Dim CURR_ThrottleUp As Boolean = False              'TRUE if Throttle > 0 in CURR logs
    Dim Log_Min_VCC As Single = 99999                   'Holds the Minimum VCC found in the Log while in Flight Only
    Dim Log_Max_VCC As Single = 0                       'Holds the Maximum VCC found in the Log while in Flight Only
    Dim Log_Min_Battery_Volts As Single = 99999         'Holds the Minimum Main Battery Volts found in the Log while in Flight Only.
    Dim Log_Max_Battery_Volts As Single = 0             'Holds the Maximum Main Battery Volts found in the Log while in Flight Only.
    Dim Log_Min_Battery_Current As Single = 99999       'Hold the Minimum Main Battery Current Drawn found in the Log while in Flight Only.
    Dim Log_Max_Battery_Current As Single = 0           'Hold the Maximum Main Battery Current Drawn found in the Log while in Flight Only.
    Dim Log_Sum_Battery_Current As Single = 0           'Hold the Maximum Main Battery Current Drawn found in the Log while in Flight Only.
    Dim Log_Mode_Start_Current As Single = 0            'Holds the Total Current Used from the Log "CURR" recorded in the log at the start of this flight mode
    Dim Total_Mode_Current As Integer = 0               'Holds the Total Current Used from the Log "CURR" reading for this mode while in flight.
    Dim Log_Total_Current As Integer = 0                'Holds the Total Current Used from the Log "CURR" reading.
    Dim Log_CURR_DLs As Integer = 0                     'Counts the number of datalines found in the Log for CURR results, for working out averages
    Dim Log_CURR_DLs_for_Mode As Integer = 0            'Counts the number of datalines found in the Log for CURR results during each mode, for working out averages
    Dim Log_Mode_Min_Volt As Integer = 99999            'Holds the Minimum Volt recorded in the log during flight in this mode.
    Dim Log_Mode_Max_Volt As Integer = 0                'Holds the Maximum Volt recorded in the log during flight in this mode.
    Dim Log_Mode_Sum_Volt As Integer = 0                'Holds the Sum of all Volt recorded in the log during flight in this mode.


    'Declare the CTUN Variables
    Dim CTUN_ThrottleUp As Boolean = False              'TRUE if Throttle > 0 in CTUN logs
    Dim CTUN_ThrOut_40 As Boolean = False               'True if Throttle > 40 in CTUN logs
    Dim Log_Maximum_Altitude As Integer = 0             'Holds the Maximum Altitude detected during the flight from the log.
    Dim Log_CTUN_BarAlt As Single = 0                  'Holds the Current Barometer Altitude recorded in the log.
    Dim Log_CTUN_ThrOut As Integer = 0                  'Holds the Current ThrOut recorded in the log from the CTUN data line.
    Dim Log_CTUN_DLs As Integer = 0                     'Counts the number of datalines found in the Log for CTUN results, for working out averages
    Dim Log_CTUN_DLs_for_Mode As Integer = 0            'Counts the number of datalines found in the Log for CTUN results during each mode, for working out averages
    Dim Log_Mode_Min_BarAlt As Integer = 99999          'Holds the Minimum BarAlt recorded in the log during flight in this mode.
    Dim Log_Mode_Max_BarAlt As Integer = 0              'Holds the Maximum BarAlt recorded in the log during flight in this mode.
    Dim Log_Mode_Sum_BarAlt As Integer = 0              'Holds the Sum of all BarAlt recorded in the log during flight in this mode.


    'Declare the GPS Variables
    Dim Log_GPS_GCrs As Integer = 0                     'Holds the GPS ground course in degrees (0 = north)
    Dim Log_GPS_Status As Integer = 0                   'Holds the GPS Current status as recorded in the Log
    Dim GPS_Base_Date As Date = BaseDate              'Holds the Standard GPS Base Date of 06/01/1980
    Dim Log_GPS_TimeMS As Integer = 0                   'Holds the GPS Current Time in ms as recorded in the Log
    Dim Log_GPS_Week As Integer = 0                     'Holds the GPS CUrrent week as recorded in the Log
    Dim Log_GPS_DateTime As Date = BaseDate           'Holds the Current Date and Time as calculated from the Log
    Dim Log_First_DateTime As Date = BaseDate         'Holds the first recorded Date and Time as calculated from the Log.
    Dim Log_Last_DateTime As Date = BaseDate          'Holds the last recorded Date and Time as calculated from the Log.
    Dim Log_Total_Flight_Time As Integer = 0            'Holds the total flight time recorded in Seconds, captured by CTUN to detect landing.
    Dim Log_Current_Flight_Time As Integer = 0          'Holds the current flight time recorded in Seconds between take off and landing, updated live in GPS checks.
    Dim Log_GPS_NSats As Integer = 0                    'Holds the GPS Current number of Sats
    Dim Log_GPS_Min_NSats As Integer = 99               'Holds the GPS Minimum number of Sats recorded in the log.
    Dim Log_GPS_Max_NSats As Integer = 0                'Holds the GPS Maximum number of Sats recorded in the log.
    Dim Log_Flight_Sum_NSats As Integer = 0             'Holds the Sum of all GPS Sats recorded in the log during flight.
    Dim Log_Flight_Min_NSats As Integer = 99            'Holds the GPS Minimum number of Sats recorded in the log during Flight.
    Dim Log_Flight_Max_NSats As Integer = 0             'Holds the GPS Maximum number of Sats recorded in the log during Flight.
    Dim Log_Mode_Min_NSats As Integer = 99              'Holds the GPS Minimum number of Sats recorded in the log during flight in this mode.
    Dim Log_Mode_Max_NSats As Integer = 0               'Holds the GPS Maximum number of Sats recorded in the log during flight in this mode.
    Dim Log_Mode_Sum_NSats As Integer = 0               'Holds the Sum of all GPS Sats recorded in the log during flight in this mode.
    Dim Log_GPS_HDop As Single = 0                      'Holds the GPS Current HDop 
    Dim Log_GPS_Min_HDop As Single = 99                 'Holds the Minimum HDop recorded in the log.
    Dim Log_GPS_Max_HDop As Single = 0                  'Holds the Maximum HDop recorded in the log.
    Dim Log_Flight_Sum_HDop As Single = 0               'Holds the Sum of all HDop recorded in the log during flight.
    Dim Log_Flight_Min_HDop As Single = 99              'Holds the Minimum HDop recorded in the log during flight.
    Dim Log_Flight_Max_HDop As Single = 0               'Holds the Maximum HDop recorded in the log during flight.
    Dim Log_Mode_Min_HDop As Single = 99                'Holds the Minimum HDop recorded in the log during flight in this mode.
    Dim Log_Mode_Max_HDop As Single = 0                 'Holds the Maximum HDop recorded in the log during flight in this mode.
    Dim Log_Mode_Sum_HDop As Single = 0                 'Holds the Sum of all HDop recorded in the log during flight in this mode.
    Dim Log_GPS_Lat As Double = 0                       'Holds the current GPS Latitude
    Dim Log_GPS_Lng As Double = 0                       'Holds the current GPS Longtitude
    Dim First_GPS_Lat As Double = 0                     'Holds the Latitude when the Qaud first takes off.
    Dim First_GPS_Lng As Double = 0                     'Holds the Longtitude when the Qaud first takes off.
    Dim Last_GPS_Lat As Double = 0                      'Holds the last Latitude Result, used to calculate distance between each GPS Log.
    Dim Last_GPS_Lng As Double = 0                      'Holds the last Longtitude Result, used to calculate distance between each GPS Log.
    Dim Log_GPS_Alt As Integer = 0                      'Holds the current GPS_Alt result.
    Dim Log_GPS_Last_Alt As Integer = 0                 'Holds the last GPS_Alt result, used to filter spikes.
    Dim Log_GPS_Calculated_Alt As Integer = 0           'Calculated Altitude by taking the GPS Alt - launch GPS Alt
    Dim First_In_Flight As Boolean = False              'TRUE and remains TRUE the first time Log_In_Flight goes TRUE
    Dim Dist_From_Launch As Single = 0                  'Calculated distance from first take off point in km (Kilometers).
    Dim Max_Dist_From_Launch As Single                  'Calculated Maximum distance from first take off point in km (Kilometers).
    Dim Dist_Travelled As Single = 0                    'Calculated distance between each GPS Log in km (Kilometers).
    Dim Mode_Min_Dist_From_Launch As Single = 99999     'Calculated Minimum distance from first take off point in km (Kilometers) for this mode summary.
    Dim Mode_Max_Dist_From_Launch As Single = 0         'Calculated Maximum distance from first take off point in km (Kilometers) for this mode summary.
    Dim Mode_Start_Dist_Travelled As Single = 0         'Calculated distance when the mode changes so we can work out the distance travelled in this mode.
    Dim Mode_Dist_Travelled As Single = 0               'Calculated distance between each GPS Log in km (Kilometers).
    Dim Temp_Dist_Travelled As Single                   'Used to validate that the Distance should be added.
    Dim Log_GPS_Spd As Single = 0                      'Holds the Current GPS Speed in ms
    Dim Log_Flight_Max_Spd As Integer = 0               'Holds the Maximum GPS Speed in ms recorded in the log during flight
    Dim Log_Mode_Min_Spd As Integer = 99999             'Holds the Minimum Speed recorded in the log during flight in this mode.
    Dim Log_Mode_Max_Spd As Integer = 0                 'Holds the Maximum Speed recorded in the log during flight in this mode.
    Dim Log_Mode_Sum_Spd As Integer = 0                 'Holds the Sum of all Speed recorded in the log during flight in this mode.
    Dim Log_GPS_DLs As Integer = 0                      'Counts the number of datalines found in the Log for GPS results while in flight, for working out averages
    Dim Log_GPS_DLs_for_Mode As Integer = 0             'Counts the number of datalines found in the Log for GPS results during each mode while in flight, for working out averages
    Dim Log_GPS_Glitch As Boolean = False               'TRUE if there is currently a GPS Glitch in progress.

    'Declare the Return Variables that will be set by reading the Log File
    Dim ArduVersion As String = ""                      'Holds the Ardu***** Version found in the Log File
    Dim ArduBuild As String = ""                        'Holds the Ardu***** Build Version found in the Log File
    Dim ArduType As String = ""                         'Holds the Ardu type determined from the log file, "ArduCopter".
    Dim APM_Free_RAM As Integer = 0                     'Holds the APM Free RAM reported in the Log File
    Dim APM_Version As Single = 0                       'Hold the APM Version Number as reported in the log file
    Dim APM_Frame_Type As Single = 0                    'Holds the APM Frame Type, determined from the Parmeter FRAME
    Dim APM_Frame_Name As String = ""                   'The Text Name of the Frame Type
    Dim APM_No_Motors As Integer = 0                    'Holds the number of Motors, determined from the FMT for MOT.

    'Decalre the IMU Variables
    Dim Log_IMU_TimeMS As Integer = 0                   'Holds the current IMU Time in ms that the last reading was taken.
    Dim Log_IMU_GyrX As Single = 0                      'Holds the current IMU Gyro X axis when the last reading was taken.
    Dim Log_IMU_GyrY As Single = 0                      'Holds the current IMU Gyro Y axis when the last reading was taken.
    Dim Log_IMU_GyrZ As Single = 0                      'Holds the current IMU Gyro Z axis when the last reading was taken.
    Dim Log_IMU_AccX As Single = 0                      'Holds the current IMU Accelorometer X axis when the last reading was taken.
    Dim Log_IMU_AccY As Single = 0                      'Holds the current IMU Accelorometer Y axis when the last reading was taken.
    Dim Log_IMU_AccZ As Single = 0                      'Holds the current IMU Accelorometer Z axis when the last reading was taken.
    Dim Log_IMU_DLs As Integer = 0                      'Counts the number of datalines found in the Log for IMU results, for working out averages
    Dim Log_IMU_DLs_for_Slow_FLight As Integer = 0      'Counts the number of datalines found in the Log for IMU results while in slow flight, for working out averages
    Dim Log_IMU_Min_AccX As Single = 99                 'Holds the minimum IMU Accelorometer X axis detected during a slow flight / hover.
    Dim Log_IMU_Max_AccX As Single = -99                'Holds the minimum IMU Accelorometer X axis detected during a slow flight / hover.
    Dim Log_IMU_Sum_AccX As Single = 0                  'Holds the sum of all the IMU Accelorometer X axis detected during a slow flight / hover.
    Dim Log_IMU_Min_AccY As Single = 99                 'Holds the minimum IMU Accelorometer X axis detected during a slow flight / hover.
    Dim Log_IMU_Max_AccY As Single = -99                'Holds the minimum IMU Accelorometer X axis detected during a slow flight / hover.
    Dim Log_IMU_Sum_AccY As Single = 0                  'Holds the sum of all the IMU Accelorometer Y axis detected during a slow flight / hover.
    Dim Log_IMU_Min_AccZ As Single = 99                 'Holds the minimum IMU Accelorometer X axis detected during a slow flight / hover.
    Dim Log_IMU_Max_AccZ As Single = -99                'Holds the minimum IMU Accelorometer X axis detected during a slow flight / hover.
    Dim Log_IMU_Sum_AccZ As Single = 0                  'Holds the sum of all the IMU Accelorometer Y axis detected during a slow flight / hover.
    Dim log_IMU_Min_Spd As Integer = 99                 'Holds the minimum speed detected during a slow flight / hover.
    Dim log_IMU_Max_Spd As Integer = 0                  'Holds the maximum speed detected during a slow flight / hover.
    Dim log_IMU_Sum_Spd As Integer = 0                  'Holds the of all speeds detected during a slow flight / hover.
    Dim log_IMU_Min_Alt As Integer = 99                 'Holds the minimum altitude detected during a slow flight / hover.
    Dim log_IMU_Max_Alt As Integer = 0                  'Holds the maximum altitude detected during a slow flight / hover.
    Dim log_IMU_Sum_Alt As Integer = 0                  'Holds the of all altitude detected during a slow flight / hover.
    Dim IMU_Vibration_Check As Boolean = False          'TRUE when 500 successive records have been successfully read for vibration checking.
    Dim IMU_Vibration_AccX(5000) As Single               'Array holds the values that will be analysied for the Mean and Standard Deviation results of AccX
    Dim IMU_Vibration_AccY(5000) As Single               'Array holds the values that will be analysied for the Mean and Standard Deviation results of AccY
    Dim IMU_Vibration_AccZ(5000) As Single               'Array holds the values that will be analysied for the Mean and Standard Deviation results of AccZ
    Dim IMU_Vibration_Alt(5000) As Single               'Array holds the Altitude values that will be plotted on the Vibration Charts
    Dim IMU_Vibration_Spd(5000) As Single               'Array holds the Speed values that will be plotted on the Vibration Charts
    Dim IMU_Vibration_Start_DL As Single = 0            'Holds the Dataline Number of where we started logging for vibrations
    Dim IMU_Vibration_End_DL As Single = 0              'Holds the Dataline Number of where we Ended the logging for vibrations

    'Declare the ATT Variables
    Dim Log_ATT_RollIn As Single = 0                    'Holds current the ATT RollIn Data
    Dim Log_ATT_Roll As Single = 0                     'Holds current the ATT Roll Data
    Dim Log_ATT_PitchIn As Single = 0                   'Holds current the ATT PitchIn Data
    Dim Log_ATT_Pitch As Single = 0                     'Holds current the ATT Pitch Data
    Dim Log_ATT_YawIn As Single = 0                     'Holds current the ATT YawIn Data
    Dim Log_ATT_Yaw As Single = 0                       'Holds current the ATT Yaw Data
    Dim Log_ATT_NavYaw As Single = 0                    'Holds current the ATT NavYaw Data

    'Declare the Logging Variables
    Dim IMU_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Dim GPS_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Dim CTUN_Logging As Boolean = False                 'TRUE if Valid data found in Log File
    Dim PM_Logging As Boolean = False                   'TRUE if Valid data found in Log File
    Dim CURR_Logging As Boolean = False                 'TRUE if Valid data found in Log File
    Dim NTUN_Logging As Boolean = False                 'TRUE if Valid data found in Log File
    Dim MSG_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Dim ATUN_Logging As Boolean = False                 'TRUE if Valid data found in Log File
    Dim ATDE_logging As Boolean = False                 'TRUE if Valid data found in Log File
    Dim MOT_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Dim OF_Logging As Boolean = False                   'TRUE if Valid data found in Log File
    Dim MAG_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Dim CMD_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Dim ATT_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Dim INAV_Logging As Boolean = False                 'TRUE if Valid data found in Log File
    Dim MODE_Logging As Boolean = False                 'TRUE if Valid data found in Log File
    Dim STRT_logging As Boolean = False                 'TRUE if Valid data found in Log File
    Dim EV_Logging As Boolean = False                   'TRUE if Valid data found in Log File
    Dim D16_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Dim DU16_Logging As Boolean = False                 'TRUE if Valid data found in Log File
    Dim D32_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Dim DU32_Logging As Boolean = False                 'TRUE if Valid data found in Log File
    Dim DFLT_Logging As Boolean = False                 'TRUE if Valid data found in Log File
    Dim PID_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Dim CAM_Logging As Boolean = False                  'TRUE if Valid data found in Log File
    Dim ERR_Logging As Boolean = False                  'TRUE if Valid data found in Log File

    Dim FileOpened As Boolean = False                   'TRUE if file opened. False if no file opened or opened Cancelled


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If FileOpened = True Then
            btnAnalyze.Visible = True
            btnDisplayVibrationChart.Visible = True
            btnVibrations.Visible = True
            PictureBox1.Visible = False
        ElseIf FileOpened = False Then
            btnAnalyze.Visible = False
            btnDisplayVibrationChart.Visible = False
            btnVibrations.Visible = False
            PictureBox1.Visible = True
        End If


        'loads the form position
        RestorePosition(Me, "PMForm")
        ' Sets the culture to English GB
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
        ' Sets the UI culture to English GB
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-GB")

        'Get the version numbers a run the update if not developing.
        CurrentPublishVersionNumber = GetCurrentDeploymentCurrentVersion()
        lblCurrentVersion.Text = "s:" & CurrentPublishVersionNumber
        lblMyCurrentVersion.Text = "m:" & MyCurrentVersionNumber

        Debug.Print("==========================================================================")
        Debug.Print("========================Starting Program =================================")
        Debug.Print("==========================================================================")
        Debug.Print("ProgramInstallationFolder: " & ProgramInstallationFolder)
        Debug.Print("TempProgramInstallationFolder: " & TempProgramInstallationFolder)
        Debug.Print("ApplicationStartUpPath: " & ApplicationStartUpPath)
        Debug.Print("TempIsDeployed: " & TempIsDeployed)
        Debug.Print("MyCurrentVersionNumber:      " & MyCurrentVersionNumber)
        Debug.Print("CurrentPublishVersionNumber: " & CurrentPublishVersionNumber)
        Debug.Print("==========================================================================")

        Debug.Print(vbNewLine & "Open Form: " & Me.Name)

        If File.Exists("C:\Temp\OpenKKWM.LFA") Then
            Debug.Print("Found an old .LFA file, trying to delete it...")
            File.Delete("C:\Temp\OpenKKWM.LFA")
            Debug.Print("Success!")
        End If

        If (ApplicationDeployment.IsNetworkDeployed) Then
            If MsgBox("Would you like to check for updates now?", vbYesNo) = vbYes Then
                'display the Updating splash screen.
                frmUpdate.Show()
                frmUpdate.Refresh()
                Call AutoUpdate(CurrentPublishVersionNumber)
                frmUpdate.Close()
            End If
        End If

        Call ReadINIfile()

        Debug.Print("Open Form " & Me.Name & " Completed" & vbNewLine)

    End Sub
    Private Sub Form1_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        'saves form position
        SavePosition(Me, "PMForm")
    End Sub

    Public Sub ReadFile(ByVal strLogFileName As String)

        'Initialise the Variables for Reading the Variables
        Dim objReader As New System.IO.StreamReader(strLogFileName)

        DataLine = 0
        Data = ""
        TotalDataLines = 0
        LogAnalysisHeader = False                        'True once the header has been written to the screen.
        ErrorCount = 0                                  'Counts the number of errors found in the logs.

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
        APM_Version = 0                                 'Hold the APM Version Number as reported in the log file
        APM_Frame_Type = 0                              'Holds the APM Frame Type, determined from the Parmeter FRAME 
        APM_Frame_Name = ""                            'The Text Name of the Frame Type
        APM_No_Motors = 0                               'Holds the number of Motors, determined from the FMT for MOT.

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
        btnDisplayVibrationChart.Enabled = False        'Disable the tempory vibration button

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

        'Read the File line by line (1st Pass)
        'This pass checks what data is available in the log.
        lblErrors.Visible = False
        lblErrorCountNo.Visible = False
        lblErrorCount.Visible = False
        Call FindLoggingData()

        'Display the Release Candidate Warning
        If InStr(StrConv(ArduVersion, vbUpperCase), "RC") Then
            WriteTextLog("WARNING: Installed APM firmware is a Release Candidate version, only advanced pilots should be using this!")
            WriteTextLog("WARNING: Beginners are recommended to install the latest stable officially released version.")
            Dim strTemp As String = "Installed APM firmware is a Release Candidate version!"
            strTemp = strTemp & "!" & vbNewLine
            MsgBox(strTemp, vbOKOnly, "Error")
        End If




        'Display and ArduPlane Warning
        If ArduType = "ArduPlane" Then MsgBox("ArduPlane analysis is still under testing!", vbOKOnly, "Message")

        'Check the log file has been made by an arducopter, if not ArduCoperVersion will still be "".
        If ArduVersion = "" Then
            WriteTextLog("Log file not created by a recognised Ardu Vehicle firmware!")
            Dim strTemp As String = ""
            strTemp = strTemp & "Log must be created by an Ardu Vehicle firmware!" & vbNewLine
            MsgBox(strTemp, vbOKOnly, "Error")
            Exit Sub
        End If

        'Check the program is compatible with this log file version.
        If Ignore_LOG_Version = False Then
            If ArduType = "ArduCopter" And ArduVersion < "V3.1" Then
                WriteTextLog("Log file created by an old ArduCopter firmware version!")
                Dim strTemp As String = ""
                strTemp = strTemp & "            Log must be created by APM firmware v3.1 or above." & vbNewLine
                strTemp = strTemp & "Try updating by selecting HELP - UPDATE NOW from the menus." & vbNewLine
                MsgBox(strTemp, vbOKOnly, "Error")
                Exit Sub
            End If

            'Display v3.2 warning, not fully complatible yet.
            If ArduType = "ArduCopter" And ArduVersion > "V3.1.5" Then
                WriteTextLog("Log file created by a new ArduCopter firmware version!")
                Dim strTemp As String = ""
                strTemp = strTemp & "This Log file was created by a new version, it may not be fully supported yet." & vbNewLine
                strTemp = strTemp & "            Try updating by selecting HELP - UPDATE NOW from the menus." & vbNewLine
                strTemp = strTemp & "                     Attempt to run anyway?." & vbNewLine
                If MsgBox(strTemp, vbYesNo, "Error") = vbNo Then
                    Exit Sub
                End If

            End If

            If ArduType = "ArduPlane" And ArduVersion < "V3.0" Then
                WriteTextLog("Log file created by an old ArduPlane firmware version!")
                Dim strTemp As String = ""
                strTemp = strTemp & "            Log must be created by APM firmware v3.0 or above." & vbNewLine
                strTemp = strTemp & "Try updating by selecting HELP - UPDATE NOW from the menus." & vbNewLine
                MsgBox(strTemp, vbOKOnly, "Error")
                Exit Sub
            End If
        Else
            MsgBox("Ignore_LOG_Version is Active", vbOKOnly, "DEVELOPER WARNING")
        End If


        'Check to ensure the files contains the minimum data types required.
        If Ignore_LOG_Requirements = False Then
            If ArduType = "ArduCopter" And (GPS_Logging = False Or EV_Logging = False) Then
                WriteTextLog("Log file does not contain the correct data!")
                Dim strTemp As String = ""
                strTemp = strTemp & "          Log must contain GPS and EV data as a minimum" & vbNewLine
                strTemp = strTemp & "                                 for this program to be useful." & vbNewLine
                strTemp = strTemp & "Try updating by selecting HELP - UPDATE NOW from the menus." & vbNewLine
                MsgBox(strTemp, vbOKOnly, "Error")
                Exit Sub
            End If
        Else
            MsgBox("Ignore_LOG_Requirements is Active", vbOKOnly, "DEVELOPER WARNING")
        End If

        If Read_LOG_Percentage <> 100 Then
            MsgBox("Read_LOG_Percentage is Active @ " & Read_LOG_Percentage & "%", vbOKOnly, "DEVELOPER WARNING")
        End If

        'Read the File line by line (2nd Pass)
        barReadFile.Value = 0
        barReadFile.Visible = True
        richtxtLogAnalysis.Focus()
        Do While objReader.Peek() <> -1
            DataArrayCounter = 0
            DataSplit = ""
            Array.Clear(DataArray, 0, DataArray.Length)

            DataLine = DataLine + 1
            'Update progress bar, note -1 and back again keeps it updaing real time!
            barReadFile.Value = (DataLine / TotalDataLines) * 100
            barReadFile.Refresh()
            If (DataLine / TotalDataLines) * 100 > 1 Then barReadFile.Value = ((DataLine / TotalDataLines) * 100) - 1
            barReadFile.Refresh()
            barReadFile.Value = (DataLine / TotalDataLines) * 100
            barReadFile.Refresh()

            Data = objReader.ReadLine()
            'Debug.Print("Processing:- " & Str(DataLine) & ": " & Data)
            ' Split the data into the DataArray()
            For n = 1 To Len(Data)
                DataChar = Mid(Data, n, 1)
                If (DataChar = "," Or DataChar = " ") Then
                    If DataSplit <> "" Then
                        DataArray(DataArrayCounter) = DataSplit
                        'Debug.Print("--- Paramter " & DataArrayCounter & " = " & DataArray(DataArrayCounter))
                        DataSplit = ""
                        DataArrayCounter = DataArrayCounter + 1
                        'sometimes some bad data is found in the logs.
                        'ensure we do not make more than 25 enteries into the array.
                        'the code in each section will pick up the issue and ensure we do not process corrupt data.
                        If DataArrayCounter > 25 Then DataArrayCounter = 25
                    End If
                Else
                    DataSplit = DataSplit + DataChar
                End If
            Next n
            'Capture the very Last entry (i.e. there is no , after the last entry.
            'DataArrayCounter is now = to the last entry in the data spilt.
            DataArray(DataArrayCounter) = DataSplit
            'Debug.Print("--- Paramter " & DataArrayCounter & " = " & DataArray(DataArrayCounter))

            '######################################
            '### Main Checking Code Starts Here ###
            '######################################
            Try

                ' Write Log Analysis Header.
                If ArduVersion <> "" And LogAnalysisHeader = False Then
                    LogAnalysisHeader = True
                    WriteTextLog("APM Log File Analiser " & MyCurrentVersionNumber)
                    WriteTextLog("")
                    WriteTextLog("Log FileName: " & strLogPathFileName)
                    WriteTextLog("Ardu Version: " & ArduVersion & " Build: " & ArduBuild)
                    WriteTextLog("   Ardu Type: " & ArduType)
                    WriteTextLog("APM Free RAM: " & APM_Free_RAM)
                    WriteTextLog(" APM Version: " & APM_Version)
                    Select Case APM_Frame_Type
                        Case 0
                            APM_Frame_Name = "+"
                        Case 1
                            APM_Frame_Name = "X"
                        Case 2
                            APM_Frame_Name = "V"
                        Case 3
                            APM_Frame_Name = "H"
                        Case 4
                            APM_Frame_Name = "V-Tail"
                        Case 10
                            APM_Frame_Name = "Y6B"
                        Case Else
                            APM_Frame_Name = "Error: Please update code to determine frame type " & APM_Frame_Type
                    End Select
                    WriteTextLog("  Frame Type: " & APM_Frame_Name)
                    WriteTextLog("  No. Motors: " & APM_No_Motors)
                    WriteTextLog("")
                    'Display what Data Types were found in the log file.
                    WriteTextLog_LoggingData()
                End If


                'Debug.Print("DATA: " & DataArray(0).PadRight(5) & " Flying: " & Log_In_Flight & " -- Mode_In_Flight_Start_Time: " & Mode_In_Flight_Start_Time & " -- Log_Current_Mode_Flight_Time (Flying Only): " & Log_Current_Mode_Flight_Time & " -- Log_Current_Flight_Time: " & Log_Current_Flight_Time & " -- Log_Total_Flight_Time: " & Log_Total_Flight_Time & " -- Log_Current_Mode_Time: " & Log_Current_Mode_Time)
                'Debug.Print("DATA: " & DataArray(0).PadRight(5) & " Flying: " & Log_In_Flight & " -- Log_CTUN_ThrOut: " & Log_CTUN_ThrOut & " -- CTUN_ThrottleUp: " & CTUN_ThrottleUp & " -- Log_Ground_BarAlt: " & Log_Ground_BarAlt & " -- Log_CTUN_BarAlt: " & Log_CTUN_BarAlt & " -- Log_Armed_BarAlt: " & Log_Armed_BarAlt & " -- Log_Disarmed_BarAlt: " & Log_Disarmed_BarAlt)
                'Debug.Print("DATA: " & DataArray(0).PadRight(5) & " Flying: " & Log_In_Flight & " -- Dist_From_Launch: " & Format(Dist_From_Launch * 1000, "0.00") & " -- Mode_Min_Dist_From_Launch: " & Format(Mode_Min_Dist_From_Launch * 1000, "0.00") & " -- Mode_Max_Dist_From_Launch: " & Format(Mode_Max_Dist_From_Launch * 1000, "0.00"))


                'Parameter Checking
                If chkboxParameterWarnings.Checked = True Then
                    'A Parameter value should have only 3 pieces of data!
                    If DataArray(0) = "PARM" Then
                        If IsNumeric(DataArray(2)) = False Or IsNothing(DataArray(3)) = False Then
                            Debug.Print("================================================================")
                            Debug.Print("== File Corruption Detected on Data Line " & DataLine & ", line ignored! ==")
                            Debug.Print("================================================================")
                            lblErrors.Visible = True
                            lblErrors.Refresh()
                            lblErrorCountNo.Visible = True
                            lblErrorCount.Visible = True
                            ErrorCount = ErrorCount + 1
                            lblErrorCountNo.Text = ErrorCount
                            lblErrorCount.Refresh()
                            lblErrorCountNo.Refresh()
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

                'EV Checks
                If DataArray(0) = "EV" Then
                    'An EV value should have only 1 pieces of numeric data!
                    If ReadFileResilienceCheck(1) = True Then
                        'Debug.Print("")
                        'Debug.Print("----------------------------------------------------------------------------------------")
                        'Debug.Print("Event Detected:-")

                        Select Case DataArray(1)
                            Case "10"    ' ARMED
                                'Debug.Print("ARMED")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Armed")
                                If CTUN_Logging = True Then Log_Armed_BarAlt = Log_CTUN_BarAlt Else Log_Armed_BarAlt = Log_GPS_Alt - Log_Temp_Ground_GPS_Alt
                                Log_Armed = True
                                PM_Delay_Counter = 0

                            Case "11"    ' DISARMED
                                'Debug.Print("DISARMED")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": DisArmed")
                                CTUN_ThrottleUp = False
                                If CTUN_Logging = True Then Log_Disarmed_BarAlt = Log_CTUN_BarAlt Else Log_Disarmed_BarAlt = Log_GPS_Alt - Log_Temp_Ground_GPS_Alt
                                Log_Armed = False
                                PM_Delay_Counter = 0

                            Case "15"    ' AUTO_ARMED
                                'Debug.Print("AUTO_ARMED")
                                'Debug.Print(vbNewLine)
                                'Debug.Print("Log_Total_Flight_Time: " & Log_Total_Flight_Time)
                                'Debug.Print("Current_Flight_Time: " & Log_Current_Flight_Time)
                                If Log_In_Flight = True Then
                                    'Debug.Print("### Already in Flight - Ignore ###")
                                Else
                                    'Debug.Print("Execute the TAKEOFF Code")
                                    Call TakeOffCode()
                                    PM_Delay_Counter = 0
                                End If

                            Case "16"    ' TAKEOFF
                                'Debug.Print("TAKEOFF")
                                'Debug.Print(vbNewLine)
                                'Debug.Print("Log_Total_Flight_Time: " & Log_Total_Flight_Time)
                                'Debug.Print("Current_Flight_Time: " & Log_Current_Flight_Time)
                                If Log_In_Flight = True Then
                                    'Debug.Print("### Already in Flight  - Ignore ###")
                                Else
                                    Call TakeOffCode()
                                    PM_Delay_Counter = 0
                                End If

                            Case "18"    ' LAND_COMPLETE
                                'Debug.Print("LAND_COMPLETE")
                                If Log_In_Flight = False Then
                                    'Debug.Print("### Already NOT in Flight - Ignore ###")
                                Else

                                    If CTUN_Logging = True Then Log_Ground_BarAlt = Log_CTUN_BarAlt Else Log_Ground_BarAlt = Log_GPS_Alt - Log_Temp_Ground_GPS_Alt
                                    'Debug.Print(vbNewLine)
                                    'Debug.Print("Log_Current_Flight_Time: " & Log_Current_Flight_Time)
                                    Log_In_Flight = False
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Landed")
                                    If chkboxSplitModeLandings.Checked = True Then Call AddModeTime()
                                    'Debug.Print("Log_Current_Flight_Time (Reset): 0")
                                    'Debug.Print("Aircraft not in flight!")
                                    Log_Total_Flight_Time = Log_Total_Flight_Time + Log_Current_Flight_Time
                                    Log_Current_Flight_Time = 0
                                    Log_Current_Mode_Flight_Time = 0
                                    PM_Delay_Counter = 0
                                End If
                        End Select


                        ' Only check these events if the chkboxNonCriticalEvents is checked
                        If chkboxNonCriticalEvents.Checked = True Then
                            Select Case DataArray(1)
                                Case "1"    ' MAVLINK_FLOAT
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": MAVLINK_FLOAT")
                                Case "2"    ' MAVLINK_INT32
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": MAVLINK_INT32")
                                Case "3"    ' MAVLINK_INT16
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": MAVLINK_INT16")
                                Case "4"    ' MAVLINK_INT8
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": MAVLINK_INT8")
                                Case "7"    ' AP_STATE
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AP_STATE")
                                Case "9"    ' INIT_SIMPLE_BEARING
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INIT_SIMPLE_BEARING")

                                Case "10" 'this case is captured above, this is just so the Else Statement is not executed.
                                Case "11" 'this case is captured above, this is just so the Else Statement is not executed.
                                Case "15" 'this case is captured above, this is just so the Else Statement is not executed.
                                Case "16" 'this case is captured above, this is just so the Else Statement is not executed.
                                Case "18" 'this case is captured above, this is just so the Else Statement is not executed.

                                Case "19"    ' LOST_GPS
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": LOST_GPS")
                                Case "21"    ' FLIP_START
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIP_START")
                                Case "22"    ' FLIP_END
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIP_END")
                                Case "25"    ' SET_HOME
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": SET_HOME")
                                Case "26"    ' SET_SIMPLE_ON
                                    If DU32_Logging <> True Or chkboxDU32.Checked <> True Then
                                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": SET_SIMPLE_ON")
                                    End If
                                Case "27"    ' SET_SIMPLE_OFF
                                    If DU32_Logging <> True Or chkboxDU32.Checked <> True Then
                                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": SET_SIMPLE_OFF")
                                    End If
                                Case "28"    ' NOT_LANDED
                                    'If Log_In_Flight = False Then
                                    '    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": NOT_LANDED")
                                    '    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ----- Testing: Log_In_Flight = " & Log_In_Flight & ", Alt = " & Log_CTUN_BarAlt & ", Throttle = " & Log_CTUN_ThrOut / 10 & "%")
                                    'End If
                                    PM_Delay_Counter = 0
                                Case "29"    ' SET_SUPERSIMPLE_ON
                                    If DU32_Logging <> True Or chkboxDU32.Checked <> True Then
                                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ":  SET_SUPERSIMPLE_ON")
                                    End If
                                Case "30"    ' AUTOTUNE_INITIALISED
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE_INITIALISED")
                                Case "31"    ' AUTOTUNE_OFF
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE_OFF")
                                Case "32"    ' AUTOTUNE_RESTART
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE_RESTART")
                                Case "33"    ' AUTOTUNE_SUCCESS
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE_SUCCESS")
                                Case "34"    ' AUTOTUNE_FAILED
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE_FAILED")
                                Case "35"    ' AUTOTUNE_REACHED_LIMIT
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE_REACHED_LIMIT")
                                Case "36"    ' AUTOTUNE_PILOT_TESTING
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE_PILOT_TESTING")
                                Case "37"    ' AUTOTUNE_SAVEDGAINS
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": AUTOTUNE_SAVEDGAINS")
                                Case "38"    ' SAVE_TRIM
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": SAVE_TRIM")
                                Case "39"    ' SAVEWP_ADD_WP
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": SAVEWP_ADD_WP: " & Log_GPS_Lat & " " & Log_GPS_Lng & " Alt: " & Log_CTUN_BarAlt)
                                Case "40"    ' SAVEWP_CLEAR_MISSION_RTL
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": SAVEWP_CLEAR_MISSION_RTL")
                                Case "41"    ' FENCE_ENABLE
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE_ENABLE")
                                Case "42"    ' FENCE_DISABLE
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE_DISABLE")
                                Case "43"    ' ACRO_TRAINER_DISABLED
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ACRO_TRAINER_DISABLED")
                                Case "44"    ' ACRO_TRAINER_LEVELING
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ACRO_TRAINER_LEVELING")
                                Case "45"    ' ACRO_TRAINER_LIMITED
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ACRO_TRAINER_LIMITED")
                                Case "46"    ' EPM_ON
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": EPM_ON")
                                Case "47"    ' EPM_OFF
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": EPM_OFF")
                                Case "48"    ' EPM_NEUTRAL
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": EPM_NEUTRAL")
                                Case "49"    ' PARACHUTE_DISABLED
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": PARACHUTE_DISABLED")
                                Case "50"    ' PARACHUTE_ENABLED
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": PARACHUTE_ENABLED")
                                Case "51"    ' PARACHUTE_RELEASED
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": PARACHUTE_RELEASED")
                                Case Else
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Update Program to include this Event: " & DataArray(1))
                            End Select
                        End If
                        'Debug.Print("----------------------------------------------------------------------------------------")
                        'Debug.Print("")
                    End If
                End If

                'Error Checks
                If chkboxErrors.Checked = True Then
                    'An Error value should have only 2 pieces or 3 Pieces of data!
                    If DataArray(0) = "ERR" Then
                        If IsNumeric(DataArray(1)) = False Or IsNothing(DataArray(3)) = False And (IsNumeric(DataArray(2) = False Or IsNothing(DataArray(2)) = False)) Then
                            Debug.Print("================================================================")
                            Debug.Print("== File Corruption Detected on Data Line " & DataLine & ", line ignored! ==")
                            Debug.Print("================================================================")
                            lblErrors.Visible = True
                            lblErrors.Refresh()
                            lblErrorCountNo.Visible = True
                            lblErrorCount.Visible = True
                            ErrorCount = ErrorCount + 1
                            lblErrorCountNo.Text = ErrorCount
                            lblErrorCount.Refresh()
                            lblErrorCountNo.Refresh()
                        Else

                            'Debug.Print("")
                            'Debug.Print("----------------------------------------------------------------------------------------")
                            'Debug.Print("Error Detected:-")


                            strErrorCode = DataArray(1)
                            strECode = DataArray(2)
                            If strErrorCode = "1" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": MAIN ERROR: Critical Error, contact firmware developers and DO NOT FLY!")
                            End If
                            If strErrorCode = "2" And Val(strECode) > 1 Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": RADIO ERROR: New Error Code (ECode Unknown to APM Log Analyser")
                            End If
                            If strErrorCode = "2" And strECode = "1" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": RADIO ERROR: Late Frame Detected, i.e. Longer than 2 Seconds.")
                                Debug.Print("Radio Error: Start")
                            End If
                            If strErrorCode = "2" And strECode = "0" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": RADIO ERROR: Previous Radio Error Resolved.")
                                Debug.Print("Radio Error: End")
                            End If
                            If strErrorCode = "3" And Val(strECode) > 2 Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": COMPASS ERROR: New Error Code (strECode Unknown to Macro).")
                            End If
                            If strErrorCode = "3" And strECode = "2" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": COMPASS ERROR: Failure while trying to read a single value from the compass (probably a hardware issue).")
                            End If
                            If strErrorCode = "3" And strECode = "1" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": COMPASS ERROR: The compass failed to initialise (likely a hardware issue).")
                            End If
                            If strErrorCode = "3" And strECode = "0" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": COMPASS ERROR: Previous Compass Error Resolved.")
                            End If
                            If strErrorCode = "4" And Val(strECode) > 1 Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": OPTICAL FLOW ERROR: New Error Code (strECode Unknown to Macro).")
                            End If
                            If strErrorCode = "4" And strECode = "1" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": OPTICAL FLOW ERROR: Failed to initialise (likely a hardware issue).")
                            End If
                            If strErrorCode = "4" And strECode = "0" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": OPTICAL FLOW ERROR: Undocumented but assume error resolved.")
                            End If
                            If strErrorCode = "5" And Val(strECode) > 1 Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: New Error Code (strECode Unknown to Macro).")
                            End If
                            If strErrorCode = "5" And strECode = "1" Then
                                Debug.Print("Throttle Error: Start")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: Throttle dropped below FS_THR_VALUE meaning likely loss of contact between RX/TX.")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: Current Distance from Launch is " & Format(Dist_From_Launch * 1000, "0.00") & "m ~ " & Format(Dist_From_Launch * 3280.8399, "0.00") & "ft")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: Current Direction of UAV is " & Log_GPS_GCrs & " degrees (0 = north).")
                            End If
                            If strErrorCode = "5" And strECode = "0" Then
                                Debug.Print("Throttle Error: End")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: Throttle error resolve meaning likely RX/TX contact restored.")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: Current Distance from Launch is " & Format(Dist_From_Launch * 1000, "0.00") & "m ~ " & Format(Dist_From_Launch * 3280.8399, "0.00") & "ft")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": THROTTLE ERROR: Current Direction of UAV is " & Log_GPS_GCrs & " degrees (0 = north).")
                            End If
                            If strErrorCode = "6" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ERROR: Battery Fail Safe Occurred.")
                            End If
                            If strErrorCode = "7" And Val(strECode) > 1 Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: New Error Code (strECode Unknown to Macro).")
                                Log_GPS_Glitch = True
                            End If
                            If strErrorCode = "7" And strECode = "1" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: GPS lock lost for at least 5 seconds.")
                                Log_GPS_Glitch = True
                            End If
                            If strErrorCode = "7" And strECode = "0" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: GPS lock restored.")
                                Log_GPS_Glitch = False
                            End If
                            If strErrorCode = "8" And Val(strECode) > 1 Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS ERROR: New Error Code (strECode Unknown to Macro).")
                            End If
                            If strErrorCode = "8" And strECode = "1" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS ERROR: Updates from ground station joystick lost for at least 5 seconds.")
                            End If
                            If strErrorCode = "8" And strECode = "0" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS ERROR: Updates from ground station restored.")
                            End If
                            If strErrorCode = "9" And Val(strECode) > 3 Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE ERROR: New Error Code (strECode Unknown to Macro).")
                            End If
                            If strErrorCode = "9" And strECode = "3" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE ERROR: Both altitude and circular fences breached.")
                            End If
                            If strErrorCode = "9" And strECode = "2" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE ERROR: Circular fence breached.")
                            End If
                            If strErrorCode = "9" And strECode = "1" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE ERROR: Altitude fence breached.")
                            End If
                            If strErrorCode = "9" And strECode = "0" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FENCE ERROR: Vehicle is back within the fences.")
                            End If
                            If strErrorCode = "10" And Val(strECode) > 10 Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: New Error Code (strECode Unknown to Macro).")
                            End If
                            If strErrorCode = "10" And strECode = "0" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Stabilize mode.")
                            End If
                            If strErrorCode = "10" And strECode = "1" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Acro mode.")
                            End If
                            If strErrorCode = "10" And strECode = "2" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter AltHold mode.")
                            End If
                            If strErrorCode = "10" And strECode = "3" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Auto mode.")
                            End If
                            If strErrorCode = "10" And strECode = "4" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Guided mode.")
                            End If
                            If strErrorCode = "10" And strECode = "5" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Loiter mode.")
                            End If
                            If strErrorCode = "10" And strECode = "6" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter RTL mode.")
                            End If
                            If strErrorCode = "10" And strECode = "7" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Circle mode.")
                            End If
                            If strErrorCode = "10" And strECode = "8" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Position mode.")
                            End If
                            If strErrorCode = "10" And strECode = "9" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter Land mode.")
                            End If
                            If strErrorCode = "10" And strECode = "10" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": FLIGHT MODE ERROR: the vehicle was unable to enter OF_Loiter mode.")
                            End If
                            If strErrorCode = "11" And strECode = "2" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: GPS Glitch.")
                                Log_GPS_Glitch = True
                            End If
                            If strErrorCode = "11" And strECode = "1" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: Undocumented GPS Error.")
                                Log_GPS_Glitch = True
                            End If
                            If strErrorCode = "11" And strECode = "0" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GPS ERROR: GPS Glitch Cleared.")
                                Log_GPS_Glitch = False
                            End If
                            If strErrorCode = "12" And strECode = "1" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": CRASH ERROR: Crash Detected.")
                            End If
                            If strErrorCode = "12" And strECode = "0" Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": CRASH ERROR: Undocumented Crash Error.")
                            End If
                        End If
                    End If
                End If

                'MODE Checks
                If DataArray(0) = "MODE" Then
                    'The Mode has just changed, we need to save all the details about the previous mode before we
                    'can make the changes to the program variables.
                    'Debug.Print("")
                    'Debug.Print("----------------------------------------------------------------------------------------")
                    'Debug.Print("MODE Change Detected, Call AddModeTime() first!")
                    'Debug.Print("Current Mode is: " & Log_Current_Mode)
                    'Debug.Print("Log_Current_Mode_Time (inc Ground Time) = " & Log_Current_Mode_Time)
                    'Debug.Print("Log_Current_Mode_Flight_Time (Flying Only Reset): " & Log_Current_Mode_Flight_Time)
                    'Only display this if chkboxSplitModeLandings is not checked

                    If chkboxSplitModeLandings.Checked = True Then Call AddModeTime()

                    'ArduCopter and ArduPlane have the Mode "Name" in different cells, assume cell 1 if we dont know the type.
                    If ArduType = "ArduCopter" Then
                        Log_Current_Mode = DataArray(1)
                    ElseIf ArduType = "ArduPlane" Then
                        Log_Current_Mode = DataArray(2)
                    Else
                        Log_Current_Mode = DataArray(1)
                    End If

                    'Debug.Print("@ Logline " & DataLine & " - Mode Changed to: " & Log_Current_Mode & " @ " & Log_GPS_DateTime)

                    WriteTextLog("")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Mode Changed to " & Log_Current_Mode)
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Mode Initialised at " & Log_GPS_Lat & " " & Log_GPS_Lng & " Alt: " & Log_CTUN_BarAlt & " Spd:" & Log_GPS_Spd)

                    'Store the Total_Current used so far when the Mode Changes, thus Log_Total_Current - Log_Mode_Start_Current
                    'will = the Total_Mode_Current
                    Log_Mode_Start_Current = Log_Total_Current

                    If Log_In_Flight = True Then Mode_In_Flight_Start_Time = Log_GPS_DateTime

                    'Store the current time as the mode changes and store the new mode.
                    Log_Last_Mode_Changed_DateTime = Log_GPS_DateTime
                    'Debug.Print("Log_Last_Mode_Changed_DateTime: " & Log_Last_Mode_Changed_DateTime)
                    'Initialise the Mode Time and Flight Time Variables
                    Log_Current_Mode_Time = 0
                    Log_Current_Mode_Flight_Time = 0
                    PM_Delay_Counter = 0
                    'Debug.Print("Log_Current_Mode_Time (inc Ground Time) = " & Log_Current_Mode_Time)
                    'Debug.Print("Log_Current_Mode_Flight_Time (Flying Only Reset): " & Log_Current_Mode_Flight_Time)
                    'Debug.Print("----------------------------------------------------------------------------------------")
                    'Debug.Print("")
                End If

                'CURR Checks
                If DataArray(0) = "CURR" Then
                    'An CURR value should have only 6 pieces of numeric data!
                    If ReadFileResilienceCheck(6) = True Then
                        Value1 = Val(DataArray(1))
                        Value3 = Val(DataArray(3))
                        Value4 = Val(DataArray(4))
                        Value5 = Val(DataArray(5))
                        Value6 = Val(DataArray(6))

                        If Value1 > 0 Then CURR_ThrottleUp = True Else CURR_ThrottleUp = False
                        Log_Total_Current = Value6

                        If Log_In_Flight = True Then
                            If Value3 < Log_Min_Battery_Volts Then Log_Min_Battery_Volts = Value3
                            If Value3 > Log_Max_Battery_Volts Then Log_Max_Battery_Volts = Value3
                            If Value4 < Log_Min_Battery_Current And Value4 <> "0" Then Log_Min_Battery_Current = Value4
                            If Value4 > Log_Max_Battery_Current Then Log_Max_Battery_Current = Value4
                            Log_Sum_Battery_Current = Log_Sum_Battery_Current + Value4
                            Total_Mode_Current = Log_Total_Current - Log_Mode_Start_Current
                            If Value5 < Log_Min_VCC Then Log_Min_VCC = Value5
                            If Value5 > Log_Max_VCC Then Log_Max_VCC = Value5
                            'Collect the Mode Summary Data.
                            Log_CURR_DLs = Log_CURR_DLs + 1                       'Add a CURR record to the Total CURR record counter.
                            Log_CURR_DLs_for_Mode = Log_CURR_DLs_for_Mode + 1     'Add a CURR record to the CURR record counter for the mode.                    
                            If Value3 < Log_Mode_Min_Volt Then Log_Mode_Min_Volt = Value3
                            If Value3 > Log_Mode_Max_Volt Then Log_Mode_Max_Volt = Value3
                            Log_Mode_Sum_Volt = Log_Mode_Sum_Volt + Value3
                        End If


                        'Debug.Print(vbNewLine)
                        'Debug.Print(vbNewLine)
                        'Debug.Print("Debug CURR Data Variables:-")
                        'Debug.Print("Data Line" & Str(DataLine))
                        'Debug.Print("CURR_ThrottleUp = " & CURR_ThrottleUp)
                        'Debug.Print("Log_Min_Battery_Volts = " & Log_Min_Battery_Volts)
                        'Debug.Print("Log_Max_Battery_Volts = " & Log_Max_Battery_Volts)
                        'Debug.Print("Log_Max_Battery_Current = " & Log_Max_Battery_Current)
                        'Debug.Print("Log_Min_VCC = " & Log_Min_VCC)
                        'Debug.Print("Log_Max_VCC = " & Log_Max_VCC)
                        'Debug.Print(vbNewLine)
                        'Debug.Print(vbNewLine) '### Debug Code Here ###

                    End If
                End If

                'CTUN Checks
                If DataArray(0) = "CTUN" Then
                    'An CTUN value should have only 9 pieces of numeric data!
                    If ReadFileResilienceCheck(9) = True Then
                        Log_CTUN_BarAlt = Val(DataArray(3))
                        Log_CTUN_ThrOut = Val(DataArray(8))
                        'Capture Throttle up based on CTUN.
                        If Log_CTUN_ThrOut > 0 And CTUN_ThrottleUp = False Then
                            CTUN_ThrottleUp = True
                            'Log_Armed_BarAlt = Log_CTUN_BarAlt

                        End If

                        'Capture Throttle % based on CTUN.
                        If Log_CTUN_ThrOut > 400 Then
                            CTUN_ThrOut_40 = True
                        Else
                            CTUN_ThrOut_40 = False
                        End If

                        'Capture Throttle up based on CTUN and Capture BarAlt if just throttled down
                        If Log_CTUN_ThrOut = 0 And CTUN_ThrottleUp = True Then
                            'CTUN_ThrottleUp = False
                            'Log_Disarmed_BarAlt = Log_CTUN_BarAlt
                        End If
                        'Capture Throttle up based on CTUN and Capture BarAlt if we have just landed.
                        'Also add the "In Flight Time" to the Total so far.
                        If Log_CTUN_ThrOut = 0 And CTUN_ThrottleUp = False And Log_In_Flight = True Then
                            'Log_Ground_BarAlt = Log_CTUN_BarAlt
                            'Debug.Print(vbNewLine)
                            'Debug.Print("CTUN detected Aircraft Landed (Old Code, Now Driven from EV Data):")
                            'Debug.Print("Log_Total_Flight_Time: " & Log_Total_Flight_Time)
                            'Debug.Print("Current_Flight_Time (should be Zero now): " & Log_Current_Flight_Time)
                            'Log_Total_Flight_Time = Log_Total_Flight_Time + Log_Current_Flight_Time
                            'Log_Current_Flight_Time = 0
                            'Log_In_Flight = False
                        End If

                        'Capture the Maximum Altitude detected.
                        If Log_CTUN_BarAlt > Log_Maximum_Altitude Then Log_Maximum_Altitude = Log_CTUN_BarAlt

                        'Try to workout if the ArduCopter is in the air :)
                        If Log_Ground_BarAlt < Log_CTUN_BarAlt And Log_In_Flight = False Then
                            'Debug.Print(vbNewLine)
                            'Debug.Print("CTUN detected Aircraft Take-Off (Old Code, Now Driven from EV Data):")
                            'Debug.Print("Log_Total_Flight_Time: " & Log_Total_Flight_Time)
                            'Debug.Print("Current_Flight_Time (should be Zero now): " & Log_Current_Flight_Time)
                            'Log_In_Flight = True

                            'Capture the GPS time as the Quad takes off.
                            'In_Flight_Start_Time = Log_GPS_DateTime

                            'Capture the First GPS Lat & Lng if this is the first take off.
                            'If First_In_Flight = False Then
                            '    First_GPS_Lat = Log_GPS_Lat
                            '    First_GPS_Lng = Log_GPS_Lng
                            '    First_In_Flight = True
                            'End If
                        End If

                        'Collect the data for this Flight Mode Summary.
                        If Log_In_Flight = True Then
                            Log_CTUN_DLs = Log_CTUN_DLs + 1                       'Add a CTUN record to the Total GPS record counter.
                            Log_CTUN_DLs_for_Mode = Log_CTUN_DLs_for_Mode + 1     'Add a CTUN record to the GPS record counter for the mode.
                            If Log_CTUN_BarAlt < Log_Mode_Min_BarAlt Then Log_Mode_Min_BarAlt = Log_CTUN_BarAlt
                            If Log_CTUN_BarAlt > Log_Mode_Max_BarAlt Then Log_Mode_Max_BarAlt = Log_CTUN_BarAlt
                            Log_Mode_Sum_BarAlt = Log_Mode_Sum_BarAlt + Log_CTUN_BarAlt
                        End If

                        'Debug.Print(vbNewLine)
                        'Debug.Print(vbNewLine)
                        'Debug.Print("Debug CTUN Data Variables:-")
                        'Debug.Print("Data Line" & Str(DataLine))
                        'Debug.Print("CTUN_ThrottleUp = " & CTUN_ThrottleUp)
                        'Debug.Print("Log_Ground_BarAlt = " & Log_Ground_BarAlt)
                        'Debug.Print("Log_Armed_BarAlt = " & Log_Armed_BarAlt)
                        'Debug.Print("Log_CTUN_BarAlt = " & Log_CTUN_BarAlt)
                        'Debug.Print("Log_Maximum_Altitude = " & Log_Maximum_Altitude)
                        'Debug.Print("Log_Disarmed_BarAlt = " & Log_Disarmed_BarAlt)
                        'Debug.Print("Log_In_Flight = " & Log_In_Flight)
                        'Debug.Print("First_In_Flight = " & First_In_Flight)
                        'Debug.Print("First_GPS_Lat = " & First_GPS_Lat)
                        'Debug.Print("First_GPS_Lng = " & First_GPS_Lng)
                        'Debug.Print(vbNewLine)
                        'Debug.Print(vbNewLine) '### Debug Code Here ###

                    End If
                End If

                'GPS Checks
                If DataArray(0) = "GPS" And Log_GPS_Glitch = False Then
                    'An GPS value should have only 13 pieces of data!
                    If ReadFileResilienceCheck(13) = True Then
                        Log_GPS_Status = Val(DataArray(1))
                        Log_GPS_TimeMS = Val(DataArray(2))
                        Log_GPS_Week = Val(DataArray(3))
                        Log_GPS_NSats = Val(DataArray(4))
                        Log_GPS_HDop = Val(DataArray(5))
                        Log_GPS_Lat = Val(DataArray(6))
                        Log_GPS_Lng = Val(DataArray(7))
                        Log_GPS_Alt = Val(DataArray(9))
                        Log_GPS_Spd = Val(DataArray(10))
                        Log_GPS_GCrs = Val(DataArray(11))

                        'Calculate GPS Date and Time from Log_GPS_Week & Log_GPS_TimeMS
                        'Debug.Print("BEFORE:- Log_GPS_DateTime = " & Log_GPS_DateTime)
                        Log_GPS_DateTime = GPS_Base_Date
                        'Debug.Print("BASE Date:- Log_GPS_DateTime = " & Log_GPS_DateTime)
                        Log_GPS_DateTime = Log_GPS_DateTime.AddDays(7 * Log_GPS_Week)
                        'Debug.Print("Calculation:- Add Days = " & 7 * Log_GPS_Week)
                        'Debug.Print("Result:- Log_GPS_DateTime = " & Log_GPS_DateTime)
                        Log_GPS_DateTime = Log_GPS_DateTime.AddMilliseconds(Log_GPS_TimeMS)
                        'Debug.Print("Calculation:- Add MS = " & Log_GPS_TimeMS)
                        'Debug.Print("After:- Log_GPS_DateTime = " & Log_GPS_DateTime & vbNewLine)

                        'Store the First GPS Date & Time captured
                        If Log_GPS_DateTime <> GPS_Base_Date And Log_First_DateTime = GPS_Base_Date Then _
                            Log_First_DateTime = Log_GPS_DateTime

                        'Store the Last GPS Date & Time, actually the same as Log_GPS_DateTime but easier name for later :)
                        Log_Last_DateTime = Log_GPS_DateTime



                        'Record the "Landed" / "in Flight" Data
                        If Log_In_Flight = False Then
                            'Record the "Landed" Data

                            'Record the Min / Max HDop
                            If Log_GPS_HDop < Log_GPS_Min_HDop Then Log_GPS_Min_HDop = Log_GPS_HDop
                            If Log_GPS_HDop > Log_GPS_Max_HDop Then Log_GPS_Max_HDop = Log_GPS_HDop

                            'Record the Number of Sats
                            If Log_GPS_NSats < Log_GPS_Min_NSats Then Log_GPS_Min_NSats = Log_GPS_NSats
                            If Log_GPS_NSats > Log_GPS_Max_NSats Then Log_GPS_Max_NSats = Log_GPS_NSats
                        Else
                            'Record the "In Flight" Data

                            'Collect the data for this Flight Mode Summary if CTUN is not logging using the best GPS data we can.
                            If CTUN_Logging = False Then

                                'Validate the new Log_GPS_Alt, only allow changes of -1 or +1 at a time
                                If Log_GPS_Last_Alt = 0 Then Log_GPS_Last_Alt = Log_Temp_Ground_GPS_Alt
                                If Log_GPS_Alt > Log_GPS_Last_Alt Then Log_GPS_Alt = Log_GPS_Last_Alt + 1
                                If Log_GPS_Alt < Log_GPS_Last_Alt Then Log_GPS_Alt = Log_GPS_Last_Alt - 1

                                'only update these if we have a good GPS lock
                                If Log_GPS_Status = 3 Then
                                    Log_GPS_Last_Alt = Log_GPS_Alt
                                    Log_GPS_Calculated_Alt = Log_GPS_Alt - Log_Temp_Ground_GPS_Alt


                                    If Log_GPS_Calculated_Alt < Log_Mode_Min_BarAlt Then Log_Mode_Min_BarAlt = Log_GPS_Calculated_Alt
                                    If Log_GPS_Calculated_Alt > Log_Mode_Max_BarAlt Then Log_Mode_Max_BarAlt = Log_GPS_Calculated_Alt
                                    Log_Mode_Sum_BarAlt = Log_Mode_Sum_BarAlt + Log_GPS_Calculated_Alt
                                    Log_CTUN_DLs_for_Mode = Log_CTUN_DLs_for_Mode + 1

                                    If Log_GPS_Calculated_Alt > Log_Maximum_Altitude Then
                                        Log_Maximum_Altitude = Log_GPS_Calculated_Alt
                                    End If
                                End If
                            End If

                            'Capture the Min / Max Speed Recorded
                            If Log_GPS_Spd > Log_Flight_Max_Spd Then Log_Flight_Max_Spd = Log_GPS_Spd
                            If Log_GPS_Spd < Log_Mode_Min_Spd Then Log_Mode_Min_Spd = Log_GPS_Spd
                            If Log_GPS_Spd > Log_Mode_Max_Spd Then Log_Mode_Max_Spd = Log_GPS_Spd
                            Log_Mode_Sum_Spd = Log_Mode_Sum_Spd + Log_GPS_Spd

                            'Record the Min / Max HDop
                            If Log_GPS_HDop < Log_Flight_Min_HDop Then Log_Flight_Min_HDop = Log_GPS_HDop
                            If Log_GPS_HDop > Log_Flight_Max_HDop Then Log_Flight_Max_HDop = Log_GPS_HDop
                            If Log_GPS_HDop < Log_Mode_Min_HDop Then Log_Mode_Min_HDop = Log_GPS_HDop
                            If Log_GPS_HDop > Log_Mode_Max_HDop Then Log_Mode_Max_HDop = Log_GPS_HDop
                            Log_Flight_Sum_HDop = Log_Flight_Sum_HDop + Log_GPS_HDop

                            'Record the Number of Sats
                            If Log_GPS_NSats < Log_Flight_Min_NSats Then Log_Flight_Min_NSats = Log_GPS_NSats
                            If Log_GPS_NSats > Log_Flight_Max_NSats Then Log_Flight_Max_NSats = Log_GPS_NSats
                            If Log_GPS_NSats < Log_Mode_Min_NSats Then Log_Mode_Min_NSats = Log_GPS_NSats
                            If Log_GPS_NSats > Log_Mode_Max_NSats Then Log_Mode_Max_NSats = Log_GPS_NSats
                            Log_Flight_Sum_NSats = Log_Flight_Sum_NSats + Log_GPS_NSats

                            'Record the Distance from Launch Site in km.
                            'It is possible for the UAV to take off before the first GPS reading during Auto Mode.
                            'So only record the distances if the First_GPS_Lat has been updated.
                            'it will have been by the time we get to the next GPS data so just miss this one.
                            If First_GPS_Lat <> 0.0 Or First_GPS_Lng <> 0.0 Then
                                Dist_From_Launch = CalculateDistance(First_GPS_Lat, First_GPS_Lng, Log_GPS_Lat, Log_GPS_Lng)
                                If Dist_From_Launch > Max_Dist_From_Launch Then Max_Dist_From_Launch = Dist_From_Launch
                                If Dist_From_Launch < Mode_Min_Dist_From_Launch Then Mode_Min_Dist_From_Launch = Dist_From_Launch
                                If Dist_From_Launch > Mode_Max_Dist_From_Launch Then Mode_Max_Dist_From_Launch = Dist_From_Launch

                                'Record the Distance between the Last GPS Co-ordinate and the new one in km.
                                Temp_Dist_Travelled = CalculateDistance(Last_GPS_Lat, Last_GPS_Lng, Log_GPS_Lat, Log_GPS_Lng)
                                '## TODO
                                '## We may need to put some validate here, i.e. travelled distance too small = hovering, Large = GPS Glitch

                                Dist_Travelled = Dist_Travelled + Temp_Dist_Travelled
                            End If


                            Log_Current_Flight_Time = DateDiff(DateInterval.Second, In_Flight_Start_Time, Log_GPS_DateTime)

                            '### ERROR HERE ###
                            'Needs an Mode_In_Flight_Start_Time
                            Log_Current_Mode_Flight_Time = DateDiff(DateInterval.Second, Mode_In_Flight_Start_Time, Log_GPS_DateTime)

                            'Debug.Print(vbNewLine)
                            'Debug.Print("Log_Last_Mode_Changed_DateTime = " & Log_Last_Mode_Changed_DateTime)
                            'Debug.Print("In_Flight_Start_Time = " & In_Flight_Start_Time)
                            'Debug.Print("Log_GPS_DateTime = " & Log_GPS_DateTime)
                            'Debug.Print("Log_Current_Flight_Time (secs) = " & Log_Current_Flight_Time)
                            'Debug.Print("Log_Current_Mode_Flight_Time (secs) = " & Log_Current_Mode_Flight_Time)
                            'Debug.Print("Log_Total_Flight_Time (secs) = " & Log_Total_Flight_Time)
                            'Debug.Print(vbNewLine)

                            ' Record the Mode Summary Data
                            Log_GPS_DLs = Log_GPS_DLs + 1                       'Add a GPS record to the Total GPS record counter.
                            Log_GPS_DLs_for_Mode = Log_GPS_DLs_for_Mode + 1     'Add a GPS record to the GPS record counter for the mode.
                            Log_Mode_Sum_NSats = Log_Mode_Sum_NSats + Log_GPS_NSats
                            Log_Mode_Sum_HDop = Log_Mode_Sum_HDop + Log_GPS_HDop
                            Mode_Dist_Travelled = Mode_Dist_Travelled + Temp_Dist_Travelled

                            'Debug.Print("Altitude: Baro " & Log_CTUN_BarAlt & " GPS Ground " & Log_Temp_Ground_GPS_Alt & " GPS Calc " & Log_GPS_Calculated_Alt)
                        End If


                        'Debug.Print(vbNewLine)
                        'Debug.Print(vbNewLine)
                        'Debug.Print("Debug GPS Data Variables:-")
                        'Debug.Print("Data Line" & Str(DataLine))
                        'Debug.Print("First_GPS_Lat = " & First_GPS_Lat)
                        'Debug.Print("First_GPS_Lng = " & First_GPS_Lng)
                        'Debug.Print("Log_GPS_Lat = " & Log_GPS_Lat)
                        'Debug.Print("Log_GPS_Lng = " & Log_GPS_Lng)
                        'Debug.Print("Last_GPS_Lat = " & Last_GPS_Lat)
                        'Debug.Print("Last_GPS_Lng = " & Last_GPS_Lng)
                        'Debug.Print("Temp_Dist_Travelled = " & Temp_Dist_Travelled & " KM  - " & Temp_Dist_Travelled * 0.621371 & " Miles")
                        'Debug.Print("Dist_Travelled = " & Dist_Travelled & " KM  - " & Dist_Travelled * 0.621371 & " Miles")
                        'Debug.Print("Dist_From_Launch = " & Dist_From_Launch & " KM  - " & Dist_From_Launch * 0.621371 & " Miles")
                        'Debug.Print("Log_GPS_DateTime = " & Log_GPS_DateTime)
                        'Debug.Print("NEED TO CAPTURE LOG FILENAME = 2014-03-15 15-14-26.log")
                        'Debug.Print("Log_First_DateTime = " & Log_First_DateTime)
                        'Debug.Print("In_Flight_Start_Time = " & In_Flight_Start_Time)
                        'Debug.Print("Log_Current_Flight_Time (secs) = " & Log_Current_Flight_Time)
                        'Debug.Print("Log_Current_Mode_Flight_Time (secs) = " & Log_Current_Mode_Flight_Time)
                        'Debug.Print("Log_Total_Flight_Time (secs) = " & Log_Total_Flight_Time)
                        'Debug.Print("Log_In_Flight = " & Log_In_Flight)
                        'Debug.Print("First_In_Flight = " & First_In_Flight)
                        'Debug.Print("Log_GPS_NSats = " & Log_GPS_NSats)
                        'Debug.Print("Log_GPS_Min_NSats = " & Log_GPS_Min_NSats)
                        'Debug.Print("Log_GPS_Max_NSats = " & Log_GPS_Max_NSats)
                        'Debug.Print("Log_Flight_Min_NSats = " & Log_Flight_Min_NSats)
                        'Debug.Print("Log_Flight_Max_NSats = " & Log_Flight_Max_NSats)
                        'Debug.Print("Log_GPS_HDop = " & Log_GPS_HDop)
                        'Debug.Print("Log_GPS_Min_HDop = " & Log_GPS_Min_HDop)
                        'Debug.Print("Log_GPS_Max_HDop = " & Log_GPS_Max_HDop)
                        'Debug.Print("Log_Flight_Min_HDop = " & Log_Flight_Min_HDop)
                        'Debug.Print("Log_Flight_Max_HDop = " & Log_Flight_Max_HDop)
                        'Debug.Print("Log_GPS_Spd = " & Log_GPS_Spd)
                        'Debug.Print("Log_Flight_Max_Spd = " & Log_Flight_Max_Spd)
                        'Debug.Print(vbNewLine)
                        'Debug.Print(vbNewLine) '### Debug Code Here ###

                        'Update the Last GPS Co-Ordinates so we can calculate the distance travelled next time round.
                        'We do not care if we are in flight or not, however we only do the calculation when in flight.
                        Last_GPS_Lat = Log_GPS_Lat
                        Last_GPS_Lng = Log_GPS_Lng
                    End If
                End If

                'IMU Checks - Vibration
                If chkboxVibrations.Checked = True Then
                    If DataArray(0) = "IMU" Then
                        'An IMU value should have only 7 pieces of numeric data!
                        If ReadFileResilienceCheck(7) = True Then
                            'Debug.Print("IMU Data Detected...")
                            Log_IMU_TimeMS = Val(DataArray(1))
                            Log_IMU_GyrX = Val(DataArray(2))
                            Log_IMU_GyrY = Val(DataArray(3))
                            Log_IMU_GyrZ = Val(DataArray(4))
                            Log_IMU_AccX = Val(DataArray(5))
                            Log_IMU_AccY = Val(DataArray(6))
                            Log_IMU_AccZ = Val(DataArray(7))

                            'Debug.Print("DataArray(7) = " & DataArray(7))

                            'Are we in flight and either hovering or flying very slow above 3 meters?  (Log_GPS_Spd in m/s)
                            Dim TempAlt As Single = 0
                            'set the Vibration variables based on the form Vibration Variables.
                            Dim intVibrationSpeed As Integer
                            Dim intVibrationAltitude As Integer
                            If IsNumeric(txtVibrationSpeed.Text) = True Then
                                intVibrationSpeed = Val(txtVibrationSpeed.Text)
                            Else
                                intVibrationSpeed = 1
                            End If
                            If IsNumeric(txtVibrationAltitude.Text) = True Then
                                intVibrationAltitude = Val(txtVibrationAltitude.Text)
                            Else
                                intVibrationAltitude = 3
                            End If

                            If CTUN_Logging = True Then TempAlt = Log_CTUN_BarAlt Else TempAlt = Log_GPS_Calculated_Alt
                            If Log_GPS_Spd < intVibrationSpeed And Log_In_Flight = chkboxVibrationInFlight.Checked And TempAlt > intVibrationAltitude And IMU_Vibration_Check = False Then
                                If IMU_Vibration_Start_DL = 0 Then IMU_Vibration_Start_DL = DataLine

                                'Capture the Min and Max values from the IMU data if we are currently in slow flight, filter by increasing by half only each time.
                                If Log_IMU_AccX < Log_IMU_Min_AccX Then Log_IMU_Min_AccX = Log_IMU_Min_AccX - ((Log_IMU_Min_AccX - Log_IMU_AccX) / 2)
                                If Log_IMU_AccX > Log_IMU_Max_AccX Then Log_IMU_Max_AccX = Log_IMU_Max_AccX + ((Log_IMU_AccX - Log_IMU_Max_AccX) / 2)
                                If Log_IMU_AccY < Log_IMU_Min_AccY Then Log_IMU_Min_AccY = Log_IMU_Min_AccY - ((Log_IMU_Min_AccY - Log_IMU_AccY) / 2)
                                If Log_IMU_AccY > Log_IMU_Max_AccY Then Log_IMU_Max_AccY = Log_IMU_Max_AccY + ((Log_IMU_AccY - Log_IMU_Max_AccY) / 2)
                                If Log_IMU_AccZ < Log_IMU_Min_AccZ Then Log_IMU_Min_AccZ = Log_IMU_Min_AccZ - ((Log_IMU_Min_AccZ - Log_IMU_AccZ) / 2)
                                If Log_IMU_AccZ > Log_IMU_Max_AccZ Then Log_IMU_Max_AccZ = Log_IMU_Max_AccZ + ((Log_IMU_AccZ - Log_IMU_Max_AccZ) / 2)
                                If Log_GPS_Spd < log_IMU_Min_Spd Then log_IMU_Min_Spd = log_IMU_Min_Spd - ((log_IMU_Min_Spd - Log_GPS_Spd) / 2)
                                If Log_GPS_Spd > log_IMU_Max_Spd Then log_IMU_Max_Spd = log_IMU_Max_Spd + ((Log_GPS_Spd - log_IMU_Max_Spd) / 2)
                                If TempAlt < log_IMU_Min_Alt Then log_IMU_Min_Alt = TempAlt
                                If TempAlt > log_IMU_Max_Alt Then log_IMU_Max_Alt = TempAlt

                                'Debug.Print("GPS Spd: " & Log_GPS_Spd & " Max AccX: " & Log_IMU_Max_AccX & " Max AccY: " & Log_IMU_Max_AccY & " Max AccZ: " & Log_IMU_Max_AccZ)
                                'Debug.Print("CTUN Alt: " & Log_CTUN_BarAlt & " Min AccX: " & Log_IMU_Min_AccX & " Min AccY: " & Log_IMU_Min_AccY & " Min AccZ: " & Log_IMU_Min_AccZ)

                                'Collect the data for the slow flight averages.
                                Log_IMU_Sum_AccX = Log_IMU_Sum_AccX + Log_IMU_AccX
                                Log_IMU_Sum_AccY = Log_IMU_Sum_AccY + Log_IMU_AccY
                                Log_IMU_Sum_AccZ = Log_IMU_Sum_AccZ + Log_IMU_AccZ
                                log_IMU_Sum_Spd = log_IMU_Sum_Spd + Log_GPS_Spd
                                log_IMU_Sum_Alt = log_IMU_Sum_Alt + TempAlt

                                'Collect the data in the Array so that we can calculate the Mean and Standard Deviation during the flight summary
                                '(only happes if we collect all 5000 records)
                                If Log_IMU_DLs_for_Slow_FLight < 5000 Then
                                    IMU_Vibration_AccX(Log_IMU_DLs_for_Slow_FLight) = Log_IMU_AccX
                                    IMU_Vibration_AccY(Log_IMU_DLs_for_Slow_FLight) = Log_IMU_AccY
                                    IMU_Vibration_AccZ(Log_IMU_DLs_for_Slow_FLight) = Log_IMU_AccZ
                                    IMU_Vibration_Alt(Log_IMU_DLs_for_Slow_FLight) = TempAlt 'Log_CTUN_BarAlt
                                    IMU_Vibration_Spd(Log_IMU_DLs_for_Slow_FLight) = Log_GPS_Spd
                                    Log_IMU_DLs_for_Slow_FLight = Log_IMU_DLs_for_Slow_FLight + 1
                                    IMU_Vibration_End_DL = DataLine
                                End If


                            Else
                                ''We need a mimimum of 5000 successive records otherwise we reset and look for a better sample.
                                If Log_IMU_DLs_for_Slow_FLight < 5000 Then
                                    'Record everything as Zero as we are landed or below the specified altitude.
                                    IMU_Vibration_AccX(Log_IMU_DLs_for_Slow_FLight) = 0
                                    IMU_Vibration_AccY(Log_IMU_DLs_for_Slow_FLight) = 0
                                    IMU_Vibration_AccZ(Log_IMU_DLs_for_Slow_FLight) = -10
                                    IMU_Vibration_Alt(Log_IMU_DLs_for_Slow_FLight) = TempAlt 'Log_CTUN_BarAlt
                                    IMU_Vibration_Spd(Log_IMU_DLs_for_Slow_FLight) = Log_GPS_Spd
                                    Log_IMU_DLs_for_Slow_FLight = Log_IMU_DLs_for_Slow_FLight + 1
                                    IMU_Vibration_End_DL = DataLine

                                    '    Log_IMU_DLs_for_Slow_FLight = 0
                                    '    Log_IMU_Sum_AccX = 0
                                    '    Log_IMU_Sum_AccY = 0
                                    '    Log_IMU_Sum_AccZ = 0
                                    '    Log_IMU_Min_AccX = 99
                                    '    Log_IMU_Max_AccX = -99
                                    '    Log_IMU_Min_AccY = 99
                                    '    Log_IMU_Max_AccY = -99
                                    '    Log_IMU_Min_AccZ = 99
                                    '    Log_IMU_Max_AccZ = -99
                                    '    log_IMU_Min_Spd = 99
                                    '    log_IMU_Max_Spd = 0
                                    '    log_IMU_Sum_Spd = 0
                                    '    log_IMU_Min_Alt = 99
                                    '    log_IMU_Max_Alt = 0
                                    '    log_IMU_Sum_Alt = 0
                                    '    IMU_Vibration_Start_DL = 0
                                    '    IMU_Vibration_End_DL = 0
                                Else
                                    'The last successive readings of IMU data gave us what ween needed to check for vibration, dont collect anymore.
                                    IMU_Vibration_Check = True
                                End If
                            End If
                        End If
                    End If
                End If

                'ATT Checks, Roll and Pitch
                If DataArray(0) = "ATT" Then
                    'An ATT value should have only 7 pieces of numeric data!
                    If ReadFileResilienceCheck(7) = True Then
                        'Debug.Print("ATT Data Detected...")
                        Log_ATT_RollIn = Val(DataArray(1))
                        Log_ATT_Roll = Val(DataArray(2))
                        Log_ATT_PitchIn = Val(DataArray(3))
                        Log_ATT_Pitch = Val(DataArray(4))
                        Log_ATT_YawIn = Val(DataArray(5))
                        Log_ATT_Yaw = Val(DataArray(6))
                        Log_ATT_NavYaw = Val(DataArray(7))

                        'Track to ensure Roll is following RollIn and Pitch is following PitchIn.
                        If Log_In_Flight = True Then
                            If Log_ATT_RollIn <> 0 And Log_ATT_RollIn - Log_ATT_Roll > TempMaxRollDiff Then TempMaxRollDiff = Log_ATT_RollIn - Log_ATT_Roll
                            If Log_ATT_PitchIn <> 0 And Log_ATT_PitchIn - Log_ATT_Pitch > TempMaxPitchDiff Then TempMaxPitchDiff = Log_ATT_PitchIn - Log_ATT_Pitch
                            'Debug.Print("Log_In_Flight = " & Log_In_Flight)
                            'Debug.Print("Diff RollIn and Roll = " & Format(Log_ATT_RollIn - Log_ATT_Roll, "000000") & " Max = " & TempMaxRollDiff _
                            '& " : PicthIn and Pitch = " & Format(Log_ATT_PitchIn - Log_ATT_Pitch, "000000") _
                            '& " Max = " & TempMaxPitchDiff)
                        End If
                    End If
                End If

                'PM Checks, process manager timings
                If chkboxPM.Checked = True Then
                    If DataArray(0) = "PM" Then
                        'An PM value should have only 9 pieces of numeric data!
                        If ReadFileResilienceCheck(9) = True Then
                            Log_PM_RenCnt = Val(DataArray(1))
                            Log_PM_RenBlw = Val(DataArray(2))
                            Log_PM_NLon = Val(DataArray(3))
                            Log_PM_NLoop = Val(DataArray(4))
                            Log_PM_MaxT = Val(DataArray(5))
                            Log_PM_PMT = Val(DataArray(6))
                            Log_PM_I2CErr = Val(DataArray(7))
                            Log_PM_INSErr = Val(DataArray(8))
                            Log_PM_INAVErr = Val(DataArray(9))
                            'Debug.Print("Processing:- " & Str(DataLine) & ": " & Data)
                            PM_Delay_Counter = PM_Delay_Counter + 1

                            If Log_PM_RenCnt > 0 Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": DCM Error: DCM renormalization count is " & Log_PM_RenCnt)
                                Debug.Print("DCM Error: DCM renormalization count is " & Log_PM_RenCnt)
                            End If

                            If Log_PM_RenBlw > 0 Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": DCM Error: DCM renormalization blow-up count is " & Log_PM_RenBlw)
                                Debug.Print("DCM Error: DCM renormalization blow-up count is " & Log_PM_RenBlw)
                            End If

                            If Log_PM_NLon > 1 And PM_Delay_Counter > 3 Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: The number of long running main loops is " & Log_PM_NLon & " @ " & Log_PM_MaxT / 1000 & "ms")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: A one off here could be ignored but if repeated in this log,")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: then try disabling some logs, for example INAV, MOTORS and IMU.")
                                Debug.Print("APM Speed Error: The number of sustained long running main loops is " & Log_PM_NLon & " @ " & Log_PM_MaxT & "ms")
                            End If

                            If Log_PM_NLoop > 1001 And PM_Delay_Counter > 3 Then
                                If ((Log_PM_NLoop - 1000) / 1000) * 100 < 5 Then
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: The total number of sustained loops " & Log_PM_NLoop)
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: A one off here could be ignored but if repeated in this log,")
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: then try disabling some logs, for example INAV, MOTORS and IMU.")
                                    Debug.Print("APM Speed Error: The total number of sustained loops " & Log_PM_NLoop)
                                Else
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: The total number of sustained loops " & Log_PM_NLoop)
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: This is above 5% of the total loops!")
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: Try disabling some logs, for example INAV, MOTORS and IMU.")
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Speed Error: Otherwise seek further advice before flying!")
                                    Debug.Print("APM Speed Error: The total number of sustained loops is >= 5% @ " & Log_PM_NLoop)
                                End If
                            End If

                            If Log_PM_PMT = 10 And Log_GCS_Attached = False Then
                                Log_GCS_Attached = True
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Information: Ground Command Station is Attached.")
                                Debug.Print("GCS Info: Ground Command Station Initialised.")
                            End If

                            If Log_PM_PMT <> PM_Last_PMT And Log_PM_PMT < 9 And Log_GCS_Attached = True Then
                                If Log_PM_PMT = 0 Then
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: GCS Heartbeat signal has been lost!")
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: Current Distance from Launch is " & Format(Dist_From_Launch * 1000, "0.00") & "m ~ " & Format(Dist_From_Launch * 3280.8399, "0.00") & "ft")
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: Current Direction of UAV is " & Log_GPS_GCrs & " degrees (0 = north).")
                                    Debug.Print("GCS Heartbeat Error: GCS Heartbeat signal has been lost!")
                                Else
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: GCS Heartbeat signal is low @ " & Log_PM_PMT & "0%")
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: Current Distance from Launch is " & Format(Dist_From_Launch * 1000, "0.00") & "m ~ " & Format(Dist_From_Launch * 3280.8399, "0.00") & "ft")
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: Current Direction of UAV is " & Log_GPS_GCrs & " degrees (0 = north).")
                                    Debug.Print("GCS Heartbeat Error: GCS Heartbeat in inconsistent @ " & Log_PM_PMT)
                                End If
                                PM_Last_PMT = Log_PM_PMT
                            Else
                                If PM_Last_PMT < 9 And Log_PM_PMT >= 9 Then
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": GCS Heartbeat Error: GCS Heartbeat signal is low now OK @ 100%")
                                    PM_Last_PMT = Log_PM_PMT
                                End If
                            End If

                            If Log_PM_I2CErr <> 0 Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": I2C Error: At least one I2C error has been detected.")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": I2C Error: Current Value of PM Parameter PM_INSErr is " & Log_PM_I2CErr)
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": I2C Error: Please upload your log here: http://www.rcgroups.com/forums/showthread.php?t=2151318&pp=50")
                                Debug.Print("I2C Error: At least one I2C error has been detected.")
                            End If

                            If Log_PM_INSErr <> 0 Then
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: At least one initialisation error has been detected.")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: Possible 3.3v Regulator issues!")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: Current Value of PM Parameter PM_INSErr is " & Log_PM_INSErr)
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: Please upload your log here: http://www.rcgroups.com/forums/showthread.php?t=2151318&pp=50")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: Please refer to this thread (page 11):")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INS Error: http://diydrones.com/forum/topics/ac3-1-rc5-spi-speed-problem?id=705844%3ATopic%3A1457156&page=1#comments")
                                Debug.Print("INS Error: Current Value of PM Parameter Log_PM_INSErr is " & Log_PM_INSErr)
                            End If

                            'KXG 08/06/2014 - INAVerrCODE REMOVED
                            'The following code has been removed as it was found to give many results which can not be explained
                            'due to a lack of information about this value.
                            '
                            'If Log_PM_INAVErr <> 0 And Log_PM_INAVErr <> PM_Last_INAVErr Then
                            '    PM_Last_INAVErr = Log_PM_INAVErr
                            '    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INAV Error: A navigation error has been detected.")
                            '    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INAV Error: Current Value of PM Parameter PM_INAVErr is " & Log_PM_INAVErr)
                            '    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INAV Error: An update will be displayed if the value changes.")
                            '    Debug.Print("INAV Error: Current Value of PM Parameter Log_PM_INAVErr is " & Log_PM_INAVErr)
                            'Else
                            '    If Log_PM_INAVErr = 0 And PM_Last_INAVErr <> 0 Then
                            '        PM_Last_INAVErr = Log_PM_INAVErr
                            '        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": INAV Error: Cleared")
                            '    End If
                            'End If
                        End If
                    End If
                End If

                'DU32 Checks

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

                            If chkboxDU32.Checked = True Then

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

                'CMD Checks
                If chkboxAutoCommands.Checked = True Then
                    If DataArray(0) = "CMD" Then
                        If ReadFileResilienceCheck(8) = True Then
                            Log_CMD_CTot = Val(DataArray(1)) - 1
                            Log_CMD_CNum = Val(DataArray(2))
                            Log_CMD_CId = Val(DataArray(3))
                            Log_CMD_Copt = Val(DataArray(4))
                            Log_CMD_Prm1 = Val(DataArray(5))
                            Log_CMD_Alt = Val(DataArray(6))
                            Log_CMD_Lat = Val(DataArray(7))
                            Log_CMD_Lng = Val(DataArray(8))


                            If Log_CMD_CNum <> 0 Then
                                If Log_CMD_CId = 16 Or Log_CMD_CId = 20 Then
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:    Hit WP" & Log_CMD_CNum - 1 & " Radius: " & Format(Log_GPS_Lat, "00.0000000") & " " & Format(Log_GPS_Lng, "00.0000000") & " at an Altitude of " & Log_CTUN_BarAlt & "m  -  " & Format(CalculateDistance(Log_GPS_Lat, Log_GPS_Lng, Log_Last_CMD_Lat, Log_Last_CMD_Lng) * 1000, "0.000") & "m to center.")
                                    If (Log_CTUN_BarAlt < Log_Last_CMD_Alt - 4 Or Log_CTUN_BarAlt > Log_Last_CMD_Alt + 4) And Log_CMD_CNum > 2 Then
                                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Warning: UAV is struggling to maintain desired altitude!")
                                    End If
                                End If
                                WriteTextLog("")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: " & Log_CMD_CNum & " of " & Log_CMD_CTot)
                            End If

                            Select Case Log_CMD_CId
                                Case 16
                                    If Log_CMD_CNum = 0 Then
                                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Set Home as " & Format(Log_CMD_Lat, "00.0000000") & " " & Format(Log_CMD_Lng, "00.0000000") & " Alt:" & Log_CMD_Alt)
                                    Else
                                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Navigate to New Way Point")
                                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Navigate From WP" & Log_CMD_CNum - 1 & ": " & Format(Log_Last_CMD_Lat, "00.0000000") & " " & Format(Log_Last_CMD_Lng, "00.0000000") & " with current Altitude of " & Log_Last_CMD_Alt & "m")
                                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:            to WP" & Log_CMD_CNum & ": " & Format(Log_CMD_Lat, "00.0000000") & " " & Format(Log_CMD_Lng, "00.0000000") & " with current Altitude of " & Log_CMD_Alt & "m")
                                        'WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: GPS - WP Distance: " & Format(CalculateDistance(Log_GPS_Lat, Log_GPS_Lng, Log_CMD_Lat, Log_CMD_Lng) * 1000, "0.00") & "m")
                                        Log_CMD_Dist1 = Log_CMD_Dist1 + CalculateDistance(Log_GPS_Lat, Log_GPS_Lng, Log_CMD_Lat, Log_CMD_Lng)
                                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  WP - WP Distance: " & Format(CalculateDistance(Log_Last_CMD_Lat, Log_Last_CMD_Lng, Log_CMD_Lat, Log_CMD_Lng) * 1000, "0.00") & "m")
                                        Log_CMD_Dist2 = Log_CMD_Dist2 + CalculateDistance(Log_Last_CMD_Lat, Log_Last_CMD_Lng, Log_CMD_Lat, Log_CMD_Lng)
                                        If Log_Last_CMD_Alt < 5 Then
                                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  Warning: Navigating to a new GPS position from a low altitude can be dangerous!")
                                            WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  Warning: Insert a Take Off command to a good altitude at the start of the mission.")
                                            Log_CMD_Dist1 = 0
                                            Log_CMD_Dist2 = 0
                                        End If
                                    End If
                                Case 20
                                    'CMD, 7, 6, 20, 1, 0, 0.00, 0.0000000, 0.0000000
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Return to launch location")
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Navigate From WP" & Log_CMD_CNum - 1 & ": " & Format(Log_Last_CMD_Lat, "00.0000000") & " " & Format(Log_Last_CMD_Lng, "00.0000000") & " with current Altitude of " & Log_Last_CMD_Alt & "m")
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:      Navigate RTL: " & Format(First_GPS_Lat, "00.0000000") & " " & Format(First_GPS_Lng, "00.0000000") & " with a final Altitude of " & Log_CMD_Alt & "m")
                                    'WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Distance Away: " & Format(CalculateDistance(Log_GPS_Lat, Log_GPS_Lng, First_GPS_Lat, First_GPS_Lng) * 1000, "0.00") & "m")
                                    Log_CMD_Dist1 = Log_CMD_Dist1 + CalculateDistance(Log_GPS_Lat, Log_GPS_Lng, First_GPS_Lat, First_GPS_Lng)
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  WP - WP Distance: " & Format(CalculateDistance(Log_Last_CMD_Lat, Log_Last_CMD_Lng, First_GPS_Lat, First_GPS_Lng) * 1000, "0.00") & "m")
                                    Log_CMD_Dist2 = Log_CMD_Dist2 + CalculateDistance(Log_Last_CMD_Lat, Log_Last_CMD_Lng, First_GPS_Lat, First_GPS_Lng)
                                    If Log_Last_CMD_Alt < 5 Then
                                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  Warning: Navigating to a new GPS position from a low altitude can be dangerous!")
                                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command:  Warning: Insert a Take Off command to a good altitude at the start of the mission.")
                                        Log_CMD_Dist1 = 0
                                        Log_CMD_Dist2 = 0
                                    End If

                                Case 22
                                    'CMD, 4, 1, 22, 1, 0, 30.48, 0.0000000, 0.0000000
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Takeoff from ground / hand")
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Climb to Alt of " & Log_CMD_Alt & "m   with a Climb Rate of " & Log_CMD_Copt & "m/s")

                                Case Else
                                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: " & Log_CMD_CId & " - NEW COMMAND, UPDATE CODE TO HANDLE")
                            End Select



                            'Remember Next Way Point GPS Co-ords so we can calculate distance correctly
                            If Log_CMD_Lat <> 0 And Log_CMD_Lng <> 0 Then
                                'if we have a correct WP co-ord then use that.
                                Log_Last_CMD_Lat = Log_CMD_Lat
                                Log_Last_CMD_Lng = Log_CMD_Lng
                            Else
                                'sometimes at the start we do not have a WP co-ord in the buffer so use the current GPS position
                                'in an attempt to calculate the distance.
                                Log_Last_CMD_Lat = Log_GPS_Lat
                                Log_Last_CMD_Lng = Log_GPS_Lng
                            End If

                            If Log_CMD_Alt <> 0 Then
                                Log_Last_CMD_Alt = Log_CMD_Alt
                            End If

                            Debug.Print("Testing GPS to WP (Dist1) = " & Log_CMD_Dist1 * 1000 & "m")
                            Debug.Print(" Testing WP to WP (Dist2) = " & Log_CMD_Dist2 * 1000 & "m")


                            ' If this is the final Way Point Command in the mission the display the total planned distance.
                            If Log_CMD_CTot = Log_CMD_CNum Then
                                'WriteTextLog("Testing GPS to WP (Dist1) = " & Log_CMD_Dist1 & "m")
                                WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Auto Command: Mission WP to WP Dist: " & Format(Log_CMD_Dist2, "0.00") & "km")
                                WriteTextLog("")
                            End If

                        End If
                    End If
                End If

                'CMD (commands received from the ground station or executed as part of a mission):
                'CTot: the total number of commands in the mission
                'CNum: this Command 's number in the mission (0 is always home, 1 is the first command, etc)
                'CId: the mavlink message id
                '       https://pixhawk.ethz.ch/mavlink/   see MAV_CMD section

                'Copt: the option parameter (used for many different purposes)
                'Prm1: the Command 's parameter (used for many different purposes)
                'Alt:  the Command 's altitude in meters
                'Lat:  the Command 's latitude position
                'Lng:  the Command 's longitude position

                'INAV Checks
                'If DataArray(0) = "INAV" Then
                '    If ReadFileResilienceCheck(10) = True Then
                '        Debug.Print("Processing:- " & Str(DataLine) & ": " & Data)
                '        Log_INAV_Home_GLat = DataArray(7)
                '        Log_INAV_Home_GLng = DataArray(8)
                '        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Testing INAV From Home Data = " & Log_INAV_Home_GLat & " " & Log_INAV_Home_GLng)
                '    End If
                'End If


                'Additional Checks not related to any one log data type

                'It has been noted that it is possible for the AMP to believe it is being powered by a USB plug while in flight
                'from ArduCopter firmware v3.1.2 then GPS glitches are disabled while USB connection is active.
                'This issue is currently under investigation.
                If Log_In_Flight = True And Log_DU32_USB = True And USB_Warning1 = False Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Error: APM believes USB is connected while in flight.")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Error: This situation is currently under investigation by Robert Lefebvre (ArduPilot Developer)")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Error: If flying when the ArduPilot believes you are connected to a USB power source then")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Error: there ""could"" be a possibility that GPS Glitches are being ignored from v3.1.2 onwards.")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": APM Error: Follow ArduPilot Issue 1170 for updates, https://github.com/diydrones/ardupilot/issues/1170")
                    USB_Warning1 = True
                End If


                'It is possible for the "Take_OFF" event to be missing from the log (found in 2014-06-01 07-13-53.log where AUTO has no Take_OFF)
                'This code checks to make sure the current in flight status makes sense
                If Log_Ground_BarAlt + 0.5 < Log_CTUN_BarAlt And Log_In_Flight = False And CTUN_ThrOut_40 = True Then
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": LOG Information: Vehicle is flying without TAKE_OFF Event recorded in the APM Log,")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ----- Testing: Log_In_Flight = " & Log_In_Flight & ", Alt = " & Log_CTUN_BarAlt & ", Throttle = " & Log_CTUN_ThrOut / 10 & "%")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": LOG Information: The Log Analiser program will simulate a TAKE_OFF Event from this point,")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": LOG Information: The effect on the results can not be determined but should be minimal.")
                    Call TakeOffCode()
                    PM_Delay_Counter = 0
                End If


                'It is possible for Log_Last_Mode_Changed_DateTime to equal GPS_Base_Date
                'where the first GPS log has not been read before the first MODE change.
                'This is quite common, this line traps the error and updates Log_Last_Mode_Changed_DateTime
                'with the Log_First_DateTime  
                If Log_Last_Mode_Changed_DateTime = GPS_Base_Date And Log_First_DateTime <> GPS_Base_Date Then
                    Log_Last_Mode_Changed_DateTime = Log_First_DateTime

                    'If we are already in flight and we only just have the GPS data then we must run the Take Off Code
                    First_In_Flight = False  'Reset the First in Flight as we need to capture the Launch GPS Data
                    Call TakeOffCode()

                    'Debug.Print("Mode Time Changed to: " & Log_Last_Mode_Changed_DateTime)
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Mode Time Changed to " & Log_Last_Mode_Changed_DateTime)
                End If

                'This keep log of how long we have been in this mode, note that this includes ground time!
                Log_Current_Mode_Time = DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)


                'Check the DEVELOPER OPTION not to read all the file, i.e. brown out detection
                If Read_LOG_Percentage <> 100 Then
                    If DataLine / TotalDataLines * 100 > Read_LOG_Percentage Then Exit Do
                End If

                'Check ESCAPE has not been pressed
                richtxtLogAnalysis.Focus()
                Application.DoEvents()
                If ESCPress = True Then
                    WriteTextLog("")
                    WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ERROR: User has aborted the analysis")
                    ESCPress = False
                    lblEsc.Visible = False
                    Exit Sub
                End If

                'Catch Any Errors
            Catch
                Debug.Print("Error Processing DataLine " & DataLine & " try Deleting this line!")
                WriteTextLog("")
                WriteTextLog(Log_GPS_DateTime & " - " & DataLine & ": Error Processing this log Line, line ignored")
                WriteTextLog(Log_GPS_DateTime & " - " & DataLine & ": " & Data)
                WriteTextLog("")
                lblErrors.Visible = True
                lblErrors.Refresh()
                lblErrorCountNo.Visible = True
                lblErrorCount.Visible = True
                ErrorCount = ErrorCount + 1
                lblErrorCountNo.Text = ErrorCount
                lblErrorCount.Refresh()
                lblErrorCountNo.Refresh()
            End Try
        Loop

        'Clean Up Tasks
        objReader.Close()

        '### Now Captured by the Landed Event ### - Not sure about Crashes though :)
        'At the end there is some time flown in the current mode that is not accounted for, this code addd that time.
        'Call AddModeTime()  'Captures the time flown in the final mode.

        'Check the Log_Current_Flight_Time is Zero, if it is not the the log ended without running the "Landed" code.
        'Im this case add the Log_Current_Flight_Time to the Log_Total_Flight_Time before running the Flight Summary.
        Debug.Print("Add Log_Current_Flight_Time to Log_Total_Flight_Time in case the code did not detect a landing")
        Log_Total_Flight_Time = Log_Total_Flight_Time + Log_Current_Flight_Time

        Call AddFinalFlightSummary()        'Display the final summary

        'Brown Out Detection.
        If Log_CTUN_BarAlt > 1 And Log_In_Flight = True Then
            If Log_CTUN_BarAlt > 3 Then
                WriteTextLog("WARNING: Possible Brown Out Occurred because log ends in a highier that expected altitude of " & Log_CTUN_BarAlt & "m")
                WriteTextLog("  TOPIC: http://diydrones.com/group/arducopterusergroup/forum/topics/quadcopter-crash-please-help")
            Else
                WriteTextLog("Warning: Log ends in a highier that expected altitude of " & Log_CTUN_BarAlt & "m (consider the possibility of a Brown Out).")
                WriteTextLog("  TOPIC: http://diydrones.com/group/arducopterusergroup/forum/topics/quadcopter-crash-please-help")
            End If
        End If

        WriteTextLog("")
        WriteTextLog("")

        barReadFile.Visible = False



        '### Debug Code Here for the logging variables ###
        'Debug.Print(vbNewLine)
        'Debug.Print(vbNewLine)
        'Debug.Print("Log File Contains for the following Data Sets:-")
        'Debug.Print("IMU_Logging = " & IMU_Logging)
        'Debug.Print("GPS_Logging = " & GPS_Logging)
        'Debug.Print("CTUN_Logging = " & CTUN_Logging)
        'Debug.Print("PM_Logging = " & PM_Logging)
        'Debug.Print("CURR_Logging = " & CURR_Logging)
        'Debug.Print("NTUN_Logging = " & NTUN_Logging)
        'Debug.Print("MSG_Logging = " & MSG_Logging)
        'Debug.Print("ATUN_Logging = " & ATUN_Logging)
        'Debug.Print("ATDE_logging = " & ATDE_logging)
        'Debug.Print("MOT_Logging = " & MOT_Logging)
        'Debug.Print("OF_Logging = " & OF_Logging)
        'Debug.Print("MAG_Logging = " & MAG_Logging)
        'Debug.Print("CMD_Logging = " & CMD_Logging)
        'Debug.Print("ATT_Logging = " & ATT_Logging)
        'Debug.Print("INAV_Logging = " & INAV_Logging)
        'Debug.Print("MODE_Logging = " & MODE_Logging)
        'Debug.Print("STRT_logging = " & STRT_logging)
        'Debug.Print("EV_Logging = " & EV_Logging)
        'Debug.Print("D16_Logging = " & D16_Logging)
        'Debug.Print("DU16_Logging = " & DU16_Logging)
        'Debug.Print("D32_Logging = " & D32_Logging)
        'Debug.Print("DU32_Logging = " & DU32_Logging)
        'Debug.Print("DFLT_Logging = " & DFLT_Logging)
        'Debug.Print("CAM_Logging = " & CAM_Logging)
        'Debug.Print("ERR_Logging = " & ERR_Logging)
        'Debug.Print(vbNewLine)
        'Debug.Print(vbNewLine) 

        Debug.Print("Sub ReadFile Completed" & vbNewLine)
    End Sub

    Public Function ReadFileResilienceCheck(intNumberOfNumericValues As Integer) As Boolean
        ReadFileResilienceCheck = True
        For n = 1 To intNumberOfNumericValues
            If IsNumeric(DataArray(n)) = False Then
                ReadFileResilienceCheck = False
                n = intNumberOfNumericValues
            End If
        Next
        If IsNothing(DataArray(intNumberOfNumericValues + 1)) = False Then
            ReadFileResilienceCheck = False
        End If
        If ReadFileResilienceCheck = False Then
            Debug.Print("================================================================")
            Debug.Print("== File Corruption Detected on Data Line " & DataLine & ", line ignored! ==")
            Debug.Print("================================================================")
            lblErrors.Visible = True
            lblErrors.Refresh()
            lblErrorCountNo.Visible = True
            lblErrorCount.Visible = True
            ErrorCount = ErrorCount + 1
            lblErrorCountNo.Text = ErrorCount
            lblErrorCount.Refresh()
            lblErrorCountNo.Refresh()
        End If
    End Function
    Public Sub ReadINIfile()
        'Read Config File
        Debug.Print(vbNewLine & "Sub Read INI File Called")

        Dim DataLine As Single              'Line Number of File being Processed.
        Dim Data As String                  'String of Data being processed from the file.

        Data = Environment.CurrentDirectory
        Debug.Print("Current Directory: " & Data)

        Debug.Print("Checking if the INI File Exists (APM_Log.ini)...")
        If File.Exists(Data & "\" & "APM_Log.ini") Then
            Dim objReader As New System.IO.StreamReader("APM_Log.ini")

            'Initialise the Variables
            DataLine = 0
            Data = ""

            Debug.Print("Reading Config .ini Settings")
            Do While objReader.Peek() <> -1

                Data = objReader.ReadLine()
                Debug.Print(Str(DataLine) & ": " & Data)
                If InStr(Data, "BATTERY_CAPACITY") > 0 Then
                    Debug.Print("Setting: BATTERY_CAPACITY ...")
                    BATTERY_CAPACITY = Val(Mid(Data, InStr(Data, "=") + 1, Len(Data)))
                    Debug.Print("Success!")
                End If
                If InStr(Data, "MIN_VCC") > 0 Then
                    Debug.Print("Setting: MIN_VCC ...")
                    MIN_VCC = Val(Mid(Data, InStr(Data, "=") + 1, Len(Data)))
                    Debug.Print("Success!")
                End If
                If InStr(Data, "MAX_VCC") > 0 Then
                    Debug.Print("Setting: MAX_VCC ...")
                    MAX_VCC = Val(Mid(Data, InStr(Data, "=") + 1, Len(Data)))
                    Debug.Print("Success!")
                End If
                If InStr(Data, "MAX_VCC_FLUC") > 0 Then
                    Debug.Print("Setting: MAX_VCC_FLUC ...")
                    MAX_VCC_FLUC = Val(Mid(Data, InStr(Data, "=") + 1, Len(Data)))
                    Debug.Print("Success!")
                End If
                If InStr(Data, "BATTERY_VOLTS") > 0 Then
                    Debug.Print("Setting: BATTERY_VOLTS ...")
                    BATTERY_VOLTS = Val(Mid(Data, InStr(Data, "=") + 1, Len(Data)))
                    Debug.Print("Success!")
                End If
                If InStr(Data, "BATTERY_TYPE") > 0 Then
                    Debug.Print("Setting: BATTERY_TYPE ...")
                    BATTERY_TYPE = Mid(Data, InStr(Data, "=") + 1, Len(Data))
                    Debug.Print("Success!")
                    BATTERY_TYPE = BATTERY_TYPE.Trim
                End If
                If InStr(Data, "BATTERY_C_RATING") > 0 Then
                    Debug.Print("Setting: BATTERY_C_RATING ...")
                    BATTERY_C_RATING = Val(Mid(Data, InStr(Data, "=") + 1, Len(Data)))
                    Debug.Print("Success!")
                End If
                If InStr(Data, "MIN_MODE_EFF_TIME") > 0 Then
                    Debug.Print("Setting: MIN_MODE_EFF_TIME ...")
                    MIN_MODE_EFF_TIME = Val(Mid(Data, InStr(Data, "=") + 1, Len(Data)))
                    Debug.Print("Success!")
                End If
                If InStr(Data, "MIN_EFF_TIME") > 0 Then
                    Debug.Print("Setting: MIN_EFF_TIME ...")
                    MIN_EFF_TIME = Val(Mid(Data, InStr(Data, "=") + 1, Len(Data)))
                    Debug.Print("Success!")
                End If
                'DataLine = DataLine + 1
            Loop

            Debug.Print("Using the following Parameters from the .ini file")
            Debug.Print("MIN_VCC = " & MIN_VCC)
            Debug.Print("MAX_VCC = " & MAX_VCC)
            Debug.Print("MAX_VCC_FLUC =" & MAX_VCC_FLUC)
            Debug.Print("BATTERY_VOLTS = " & BATTERY_VOLTS)
            Debug.Print("BATTERY_TYPE =" & BATTERY_TYPE)
            Debug.Print("BATTERY_CAPACITY =" & BATTERY_CAPACITY)
            Debug.Print("BATTERY_C_RATING =" & BATTERY_C_RATING)

            Debug.Print("Finished Reading Config .ini File")
            objReader.Close()
        Else
            Debug.Print("APM_Log.ini not found, creating one with the default settings.")
            Dim objWriter As StreamWriter = File.CreateText(Data & "\" & "APM_Log.ini")

            objWriter.WriteLine("APM Log Analysis Settings")
            objWriter.WriteLine(vbNewLine)
            objWriter.WriteLine("[Default]")
            objWriter.WriteLine("BATTERY_CAPACITY As Integer = 5000")
            objWriter.WriteLine("MIN_VCC As Single = 4.85")
            objWriter.WriteLine("MAX_VCC As Single = 5.15")
            objWriter.WriteLine("Dim MAX_VCC_FLUC As Single = 0.3")
            objWriter.WriteLine("BATTERY_VOLTS As Single = 11.1")
            objWriter.WriteLine("BATTERY_TYPE As String = LiPo")
            objWriter.WriteLine("BATTERY_C_RATING As Integer = 10")
            objWriter.WriteLine("MIN_MODE_EFF_TIME As Integer = 60")
            objWriter.WriteLine("MIN_EFF_TIME As Integer = 180")
            objWriter.Flush()
            objWriter.Close()
        End If
        Debug.Print("Sub Read INI File Completed" & vbNewLine)

    End Sub

    Public Function DecryptData(ByVal strText As String) As String
        Dim strReturn As String = ""
        Dim strTempChar As String = ""
        'Debug.Print("Decrypting: " & strText)
        For n = 1 To Len(strText)
            strTempChar = Mid(strText, n, 1)
            If strTempChar <> "#" And strTempChar <> "~" Then
                'Debug.Print(strTempChar)
                'Debug.Print(Asc(strTempChar))
                'Debug.Print(Asc(strTempChar) - 1)
                'Debug.Print(Chr(Asc(strTempChar) - 1))
                strReturn = strReturn & Chr(Asc(strTempChar) - 1)
            ElseIf strTempChar = "#" Then
                strReturn = strReturn & "/"
            Else
                strReturn = strReturn & "."
            End If
        Next
        'Debug.Print("Returning: " & strReturn)
        DecryptData = strReturn
    End Function
    Public Function EncryptData(ByVal strText As String) As String
        Dim strReturn As String = ""
        Dim strTempChar As String = ""
        'Debug.Print("Encrypting: " & strText)
        For n = 1 To Len(strText)
            strTempChar = Mid(strText, n, 1)
            If strTempChar <> "/" And strTempChar <> "." Then
                'Debug.Print(strTempChar)
                'Debug.Print(Asc(strTempChar))
                'Debug.Print(Asc(strTempChar) + 1)
                'Debug.Print(Chr(Asc(strTempChar) + 1))
                strReturn = strReturn & Chr(Asc(strTempChar) + 1)
            ElseIf strTempChar = "/" Then
                strReturn = strReturn & "#"
            Else
                strReturn = strReturn & "~"
            End If
        Next
        'Debug.Print("Returning: " & strReturn)
        EncryptData = strReturn
    End Function

    Public Function CalculateDistance(ByVal lat1 As Double, ByVal lon1 As Double, ByVal lat2 As Double, ByVal lon2 As Double) As Double
        ' Return the Distance between two GPS Co-ordinates in KM * 0.621371 to get Miles
        Dim t As Double = lon1 - lon2
        Dim distance As Double = Math.Sin(Degree2Radius(lat1)) * Math.Sin(Degree2Radius(lat2)) + Math.Cos(Degree2Radius(lat1)) * Math.Cos(Degree2Radius(lat2)) * Math.Cos(Degree2Radius(t))
        distance = Math.Acos(distance)
        distance = Radius2Degree(distance)
        distance = distance * 60 * 1.1515
        distance = distance / 0.621371
        'BugFix: If the two points are extremely close this calculation can return a NaN result, in this case we use 0/Zero
        If Double.IsNaN(distance) = True Then distance = 0
        Return distance

    End Function
    Private Function Degree2Radius(ByVal deg As Double) As Double
        Return (deg * Math.PI / 180.0)
    End Function
    Private Function Radius2Degree(ByVal rad As Double) As Double
        Return rad / Math.PI * 180.0
    End Function

    Private Sub TakeOffCode()
        'Debug.Print("Execute the TAKEOFF Code")
        Log_In_Flight = True
        If CTUN_Logging = True Then Log_Armed_BarAlt = Log_CTUN_BarAlt Else Log_Armed_BarAlt = Log_GPS_Alt - Log_Temp_Ground_GPS_Alt
        'Capture the GPS time as the Quad takes off.
        In_Flight_Start_Time = Log_GPS_DateTime
        If chkboxSplitModeLandings.Checked = True Then
            Mode_In_Flight_Start_Time = Log_GPS_DateTime
            Log_Mode_Start_Current = Log_Total_Current
        End If
        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": Take Off")
        'Capture the First GPS Lat & Lng if this is the first take off.
        If First_In_Flight = False Then
            First_GPS_Lat = Log_GPS_Lat
            First_GPS_Lng = Log_GPS_Lng
            First_In_Flight = True
        End If
        Log_Armed = True
        Log_Temp_Ground_GPS_Alt = Log_GPS_Alt
    End Sub

    Public Sub AddModeTime()
        Dim TempFlightTime As Integer = 0   'FlightTime in Seconds from the current mode.
        Dim Efficiency As String = ""        'Calculated Efficiency
        'Only update the display if the Flight Time is worth reporting
        'this alos removes some bugs when the mode changes very quickly
        'and not enough data is retrieved to overwrite the program variables
        'from their base settings, i.e. a min setting as 99999
        If Log_Current_Mode_Flight_Time > 0 Then
            Select Case Log_Current_Mode
                Case "STABILIZE"
                    STABILIZE_Flight_Time = STABILIZE_Flight_Time + Log_Current_Mode_Flight_Time    'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = STABILIZE_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & STABILIZE_Flight_Time & " Seconds")
                Case "ALT_HOLD"
                    ALT_HOLD_Flight_Time = ALT_HOLD_Flight_Time + Log_Current_Mode_Flight_Time      'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = ALT_HOLD_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & ALT_HOLD_Flight_Time & " Seconds")
                Case "LOITER"
                    LOITER_Flight_Time = LOITER_Flight_Time + Log_Current_Mode_Flight_Time          'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = LOITER_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & LOITER_Flight_Time & " Seconds")
                Case "AUTO"
                    AUTO_Flight_Time = AUTO_Flight_Time + Log_Current_Mode_Flight_Time              'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = AUTO_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & AUTO_Flight_Time & " Seconds")
                Case "RTL"
                    RTL_Flight_Time = RTL_Flight_Time + Log_Current_Mode_Flight_Time                'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = RTL_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & RTL_Flight_Time & " Seconds")
                Case "LAND"
                    LAND_Flight_Time = LAND_Flight_Time + Log_Current_Mode_Flight_Time              'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = LAND_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & LAND_Flight_Time & " Seconds")
                Case "FBW_A"
                    FBW_A_Flight_Time = FBW_A_Flight_Time + Log_Current_Mode_Flight_Time              'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = FBW_A_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & FBW_A_Flight_Time & " Seconds")
                Case "AUTOTUNE"
                    AUTOTUNE_Flight_Time = AUTOTUNE_Flight_Time + Log_Current_Mode_Flight_Time              'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = AUTOTUNE_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & AUTOTUNE_Flight_Time & " Seconds")
                Case "Manual"
                    MANUAL_Flight_Time = MANUAL_Flight_Time + Log_Current_Mode_Flight_Time              'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = MANUAL_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & MANUAL_Flight_Time & " Seconds")
                Case Else
                    Debug.Print(vbNewLine)
                    Debug.Print("#############################################")
                    Debug.Print("CONDITION ERROR")
                    Debug.Print("New MODE Detected: " & Log_Current_Mode)
                    Debug.Print("Update ""MODE"" Code to support this new mode")
                    Debug.Print("#############################################")
                    Debug.Print(vbNewLine)
                    OTHER_Flight_Time = OTHER_Flight_Time + Log_Current_Mode_Flight_Time            'DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)
                    TempFlightTime = OTHER_Flight_Time
                    Debug.Print("Mode Changed from: " & Log_Current_Mode & " Flight Time: " & OTHER_Flight_Time & " Seconds")
            End Select

            'Calculate the Efficiency if we have enough data points (i.e. based on flight time)
            If Log_Current_Mode_Flight_Time > MIN_MODE_EFF_TIME Then
                Efficiency = Str(Val(Total_Mode_Current / Log_Current_Mode_Flight_Time))
            Else
                Efficiency = "~~~"
            End If

            'Update the richtxtLogAnalysis text box with all the details from the previous mode.
            WriteTextLog("       Alt(m ~ ft)   Spd(m/s ~ mph)   Dist(km ~ mi)     Launch(m ~ ft)    GPS-Sats   GPS-Hdop    Eff(mA/min)")
            WriteTextLog(FormatTextLogValuesFlying("Max", Log_Mode_Max_BarAlt, Log_Mode_Max_Spd, Mode_Dist_Travelled, Mode_Max_Dist_From_Launch, Log_Mode_Max_NSats, Log_Mode_Max_HDop, "N/A"))
            WriteTextLog(FormatTextLogValuesFlying("Avg", Int(Log_Mode_Sum_BarAlt / Log_CTUN_DLs_for_Mode), Int(Log_Mode_Sum_Spd / Log_GPS_DLs_for_Mode), "N/A", "N/A", Int(Log_Mode_Sum_NSats / Log_GPS_DLs_for_Mode), Format(Log_Mode_Sum_HDop / Log_GPS_DLs_for_Mode, "0.00"), Efficiency))
            WriteTextLog(FormatTextLogValuesFlying("Min", Log_Mode_Min_BarAlt, Log_Mode_Min_Spd, "N/A", Mode_Min_Dist_From_Launch, Log_Mode_Min_NSats, Log_Mode_Min_HDop, "N/A"))
            WriteTextLog(Log_Current_Mode & " Flight Time (Session)= " & Log_Current_Mode_Flight_Time & " seconds, " & Int(Log_Current_Mode_Flight_Time / 60) & ":" & Format((((Log_Current_Mode_Flight_Time / 60) - Int(Log_Current_Mode_Flight_Time / 60)) * 60), "00"))
            WriteTextLog(Log_Current_Mode & " Flight Time   (Total)= " & TempFlightTime & " seconds, " & Int(TempFlightTime / 60) & ":" & Format((((TempFlightTime / 60) - Int(TempFlightTime / 60)) * 60), "00"))
            If CTUN_Logging = False Then
                WriteTextLog("* Altitude above launch estimated from GPS Data, enable CTUN for accurate borometer data!")
            End If
            If Log_Mode_Min_NSats < 9 Then
                WriteTextLog("WARNING: Less than 9 Satellites was detected during this mode.")
            End If
            If Log_Mode_Min_HDop > 2 Then
                WriteTextLog("WARNING: A HDop of greater than 2 was detected during this mode.")
            End If

            WriteTextLog("")

        Else
            If Log_Current_Mode <> "Not Determined" Then
                WriteTextLog("Limited Data Available - Mode Ignored")
                Debug.Print("Limited Data Available - Previous Mode Ignored")
            End If
        End If

        Log_Mode_Min_Volt = 99999
        Log_Mode_Max_Volt = 0
        Log_Mode_Sum_Volt = 0

        'Reset the Mode Calculatation Variables
        Log_GPS_DLs_for_Mode = 0
        Log_Mode_Min_NSats = 99
        Log_Mode_Max_NSats = 0
        Log_Mode_Sum_NSats = 0
        Log_Mode_Min_HDop = 99
        Log_Mode_Max_HDop = 0
        Log_Mode_Sum_HDop = 0
        Log_Mode_Min_Spd = 999
        Log_Mode_Max_Spd = 0
        Log_Mode_Sum_Spd = 0
        Mode_Min_Dist_From_Launch = 99999
        Mode_Max_Dist_From_Launch = 0
        Mode_Start_Dist_Travelled = 0
        Mode_Dist_Travelled = 0
        Log_CTUN_DLs_for_Mode = 0
        Log_Mode_Min_BarAlt = 99999
        Log_Mode_Max_BarAlt = 0
        Log_Mode_Sum_BarAlt = 0
        Log_CURR_DLs_for_Mode = 0
        Log_Mode_Min_Volt = 99999
        Log_Mode_Max_Volt = 0
        Log_Mode_Sum_Volt = 0
        Log_Last_CMD_Lat = 0                      'Holds the previous WP1 Co-ordinates
        Log_Last_CMD_Lng = 0                      'Holds the previous WP1 Co-ordinates
        Log_Last_CMD_Alt = 0                      'Holds the previous WP Alititude
        Log_CMD_Dist1 = 0                    'Distance using current GPS when we hit the WP radius to the next way point
        Log_CMD_Dist2 = 0                     'Distance between the two way points.

    End Sub
    Public Sub AddFinalFlightSummary()
        'Calculate the Efficiency if we have enough data points (i.e. based on flight time)
        Dim Efficiency As String = ""        'Calculated Efficiency
        If Log_Total_Flight_Time > MIN_EFF_TIME Then
            Efficiency = Str(Val(Log_Total_Current / Log_Total_Flight_Time))
        Else
            Efficiency = "~~~"
        End If

        WriteTextLog("")
        WriteTextLog("Overall Flight Summary:")
        WriteTextLog("       Alt(m ~ ft)   Spd(m/s ~ mph)   Dist(km ~ mi)     Launch(m ~ ft)    GPS-Sats   GPS-Hdop")
        WriteTextLog(FormatTextLogValuesFlying("Max", Log_Maximum_Altitude, Log_Flight_Max_Spd, Dist_Travelled, Max_Dist_From_Launch, Log_Flight_Max_NSats, Log_Flight_Max_HDop, ""))
        WriteTextLog(FormatTextLogValuesFlying("Avg", 0, 0, "N/A", "N/A", Int(Log_Flight_Sum_NSats / Log_GPS_DLs), Format(Log_Flight_Sum_HDop / Log_GPS_DLs, "0.00"), ""))
        WriteTextLog(FormatTextLogValuesFlying("Min", 0, 0, "N/A", "N/A", Log_GPS_Min_NSats, Log_GPS_Min_HDop, ""))
        If CTUN_Logging = False Then
            WriteTextLog("* Altitude above launch estimated from GPS Data, enable CTUN for accurate borometer data!")
        End If
        If Log_GPS_Min_NSats < 9 Then
            WriteTextLog("WARNING: Less than 9 Satellites was detected during this mode.")
        End If
        If Log_GPS_Min_HDop > 2 Then
            WriteTextLog("WARNING: A HDop of greater than 2 was detected during this mode.")
        End If
        WriteTextLog("")

        'Only display this if CURR logging has been performed.
        If CURR_Logging = True Then
            WriteTextLog("Power Summary:")
            WriteTextLog("       Battery(V)        Vcc(V)        Current(A)       Used Cap(mAh)    Eff(mA/mim)")
            WriteTextLog(FormatTextLogValuesBattery("Max", Log_Max_Battery_Volts, Log_Max_VCC, Log_Max_Battery_Current, Log_Total_Current, "N/A"))
            WriteTextLog(FormatTextLogValuesBattery("Avg", 0, 0, Log_Sum_Battery_Current / Log_CURR_DLs, "N/A", Efficiency))
            WriteTextLog(FormatTextLogValuesBattery("Min", Log_Min_Battery_Volts, Log_Min_VCC, Log_Min_Battery_Current, "N/A", "N/A"))
            WriteTextLog("Overall Flight Time = " & Log_Total_Flight_Time & " seconds, " & Int(Log_Total_Flight_Time / 60) & ":" & Format((((Log_Total_Flight_Time / 60) - Int(Log_Total_Flight_Time / 60)) * 60), "00"))
            'Check that VCC is stable and in the scope on the .ini setting MAX_VCC_FLUC
            If (Log_Max_VCC / 1000) - (Log_Min_VCC / 1000) > MAX_VCC_FLUC Then
                WriteTextLog("")
                WriteTextLog("WARNING: VCC is unstable with fluctuations of " & (Log_Max_VCC / 1000) - (Log_Min_VCC / 1000) & "v reported.")
                WriteTextLog("         VCC needs to be within " & MAX_VCC_FLUC & "v according to this UAV profile")
            End If
            WriteTextLog("")

            'Add the code to check on Capacity Consumption against the set Battery Capacity in the Parameters.
            If Log_Total_Current > (Log_Battery_Capacity * 80) / 100 Then
                WriteTextLog("")
                WriteTextLog("WARNING: Battery Capacity in Parameters is set to: " & Log_Battery_Capacity)
                WriteTextLog("WARNING: However, Capacity used in this flight is: " & Log_Total_Current)
                WriteTextLog("WARNING: This means you have used " & (Log_Total_Current / Log_Battery_Capacity) * 100 & "% of the total Capacity")
                WriteTextLog("WARNING: First, Check the Battery Capacity Parameter setting is correct.")
                WriteTextLog("WARNING: Second, check the Power Calibration: https://www.youtube.com/watch?v=tEA0Or-1n18")
                WriteTextLog("WARNING: Thrid, reduce flight times to protect the main battery!")
            End If
            If Log_Total_Current = 0 Then
                WriteTextLog("")
                WriteTextLog("WARNING: If an APM Current Sensing Power Module is fitted then it is failing to measure current consumption correctly!")
                WriteTextLog("WARNING: You could try calibrating the module: https://www.youtube.com/watch?v=tEA0Or-1n18")
            End If

        Else
            WriteTextLog("*** Enable CURR logging to view battery and flight efficiency data.")
            WriteTextLog("")
        End If

        'Only display this if IMU logging has been performed.
        If IMU_Logging = True Then
            If chkboxVibrations.Checked = True Then btnDisplayVibrationChart.Enabled = True
            If IMU_Vibration_Check = True Then
                'Calcualte the Mean and Standard Deviation on the recorded vibration logs.

                WriteTextLog("Full Vibration Summary (exceptions filtered):")
                WriteTextLog("          AccX      AccY      AccZ      Spd       Alt")
                WriteTextLog(FormatTextLogValuesVibration("Max", Log_IMU_Max_AccX, Log_IMU_Max_AccY, Log_IMU_Max_AccZ, log_IMU_Max_Spd, log_IMU_Max_Alt))
                WriteTextLog(FormatTextLogValuesVibration("Min", Log_IMU_Min_AccX, Log_IMU_Min_AccY, Log_IMU_Min_AccZ, log_IMU_Min_Spd, log_IMU_Min_Alt))
                ' CODE REMOVED KXG 08/06/2014
                ' This code that works out the averages is obviously incorrect
                ' WriteTextLog(FormatTextLogValuesVibration("Avg", Log_IMU_Sum_AccX / Log_IMU_DLs_for_Slow_FLight, Log_IMU_Sum_AccY / Log_IMU_DLs_for_Slow_FLight, Log_IMU_Sum_AccZ / Log_IMU_DLs_for_Slow_FLight, log_IMU_Sum_Spd / Log_IMU_DLs_for_Slow_FLight, log_IMU_Sum_Alt / Log_IMU_DLs_for_Slow_FLight))

                ' WriteTextLog(FormatTextLogValuesVibration("StDev", StandardDeviation(IMU_Vibration_AccX), StandardDeviation(IMU_Vibration_AccY), StandardDeviation(IMU_Vibration_AccZ), 0, 0))
                WriteTextLog("Datalines: " & IMU_Vibration_Start_DL & " ~ " & IMU_Vibration_End_DL)
                'Check that vibrations recorded are within the limits set.
                If Log_IMU_Min_AccX < -3 Or Log_IMU_Max_AccX > 3 _
                    Or Log_IMU_Min_AccY < -3 Or Log_IMU_Max_AccY > 3 _
                    Or Log_IMU_Min_AccZ < -15 Or Log_IMU_Max_AccZ > -5 Then
                    If Log_IMU_Min_AccX < -5 Or Log_IMU_Max_AccX > 5 _
                    Or Log_IMU_Min_AccY < -5 Or Log_IMU_Max_AccY > 5 _
                    Or Log_IMU_Min_AccZ < -20 Or Log_IMU_Max_AccZ > 0 Then
                        WriteTextLog("")
                        WriteTextLog("WARNING: HIGH Levels of Vibration Detected, recommended not to fly!")
                        WriteTextLog("WARNING: See the Vibration Section on this web link:")
                        WriteTextLog("         http://copter.ardupilot.com/wiki/common-diagnosing-problems-using-logs/")

                        'Display the Vibration Chart
                        Call ShowVibrationChart("WARNING: Level of vibrations are above recommended values.")
                    Else
                        WriteTextLog("")
                        WriteTextLog("WARNING: Level of vibrations are above recommended values.")
                        WriteTextLog("WARNING: See the Vibration Section on this web link:")
                        WriteTextLog("         http://copter.ardupilot.com/wiki/common-diagnosing-problems-using-logs/")

                        'Display the Vibration Chart
                        Call ShowVibrationChart("WARNING: Level of vibrations are above recommended values.")
                    End If
                Else
                    WriteTextLog("")
                    WriteTextLog("Excellent: Levels of vibration detected are within recommended limits.")
                End If
            Else
                WriteTextLog("*** Limited IMU data for vibration analysis")
                WriteTextLog("    For accurate vibration analysis ensure UAV is flying slowly above 3 meters for at least 10 seconds.")
                WriteTextLog("")
            End If
            WriteTextLog("")
        Else
            WriteTextLog("*** Enable IMU logging to view vibration results.")
            WriteTextLog("")
        End If
        WriteTextLog("")
    End Sub

    Public Sub WriteTextLog(ByVal LineText As String)
        'review the contents of the text that needs to be written.
        Dim txtColor As Color
        txtColor = Color.White
        If InStr(LineText, "WARNING:") Then txtColor = Color.Red
        If InStr(LineText, "ERROR:") Then txtColor = Color.Red
        If InStr(LineText, "Error:") Then txtColor = Color.Red
        If InStr(LineText, "Warning:") Then txtColor = Color.Orange
        If InStr(LineText, "Mode") Then txtColor = Color.Aqua
        If InStr(LineText, "Flight Time") Then txtColor = Color.Aqua
        If InStr(LineText, "Testing") Then txtColor = Color.LightPink
        If InStr(LineText, "Information") Then txtColor = Color.LightGreen
        If InStr(LineText, "***") Then txtColor = Color.LightGreen

        richtxtLogAnalysis.SelectionStart = richtxtLogAnalysis.Text.Length

        richtxtLogAnalysis.SelectionColor = txtColor

        richtxtLogAnalysis.AppendText(LineText & vbNewLine)

        richtxtLogAnalysis.Refresh()
        richtxtLogAnalysis.SelectionColor = Color.Black
    End Sub
    Private Sub WriteTextLog_LoggingData()
        Dim strTempEnabledText As String = ""
        Dim strTempDisabledText As String = ""
        If chkboxFlightDataTypes.Checked = True Then
            If IMU_Logging Then strTempEnabledText = strTempEnabledText & "IMU, " Else strTempDisabledText = strTempDisabledText & "IMU, "
            If GPS_Logging Then strTempEnabledText = strTempEnabledText & "GPS, " Else strTempDisabledText = strTempDisabledText & "GPS, "
            If CTUN_Logging Then strTempEnabledText = strTempEnabledText & "CTUN, " Else strTempDisabledText = strTempDisabledText & "CTUN, "
            If PM_Logging Then strTempEnabledText = strTempEnabledText & "PM, " Else strTempDisabledText = strTempDisabledText & "PM, "
            If CURR_Logging Then strTempEnabledText = strTempEnabledText & "CURR, " Else strTempDisabledText = strTempDisabledText & "CURR, "
            If NTUN_Logging Then strTempEnabledText = strTempEnabledText & "NTUN, " Else strTempDisabledText = strTempDisabledText & "NTUN, "
            If MSG_Logging Then strTempEnabledText = strTempEnabledText & "MSG, " Else strTempDisabledText = strTempDisabledText & "MSG, "
            If ATUN_Logging Then strTempEnabledText = strTempEnabledText & "ATUN, " Else strTempDisabledText = strTempDisabledText & "ATUN, "
            If ATDE_logging Then strTempEnabledText = strTempEnabledText & "ATDE, " Else strTempDisabledText = strTempDisabledText & "ATDE, "
            If MOT_Logging Then strTempEnabledText = strTempEnabledText & "MOT, " Else strTempDisabledText = strTempDisabledText & "MOT, "
            If OF_Logging Then strTempEnabledText = strTempEnabledText & "OF, " Else strTempDisabledText = strTempDisabledText & "OF, "
            If MAG_Logging Then strTempEnabledText = strTempEnabledText & "MAG, " Else strTempDisabledText = strTempDisabledText & "MAG, "
            If CMD_Logging Then strTempEnabledText = strTempEnabledText & "CMD, " Else strTempDisabledText = strTempDisabledText & "CMD, "
            If ATT_Logging Then strTempEnabledText = strTempEnabledText & "ATT, " Else strTempDisabledText = strTempDisabledText & "ATT, "
            If INAV_Logging Then strTempEnabledText = strTempEnabledText & "INAV, " Else strTempDisabledText = strTempDisabledText & "INAV, "
            If MODE_Logging Then strTempEnabledText = strTempEnabledText & "MODE, " Else strTempDisabledText = strTempDisabledText & "MODE, "
            If STRT_logging Then strTempEnabledText = strTempEnabledText & "STRT, " Else strTempDisabledText = strTempDisabledText & "STRT, "
            If EV_Logging Then strTempEnabledText = strTempEnabledText & "EV, " Else strTempDisabledText = strTempDisabledText & "EV, "
            If D16_Logging Then strTempEnabledText = strTempEnabledText & "D16, " Else strTempDisabledText = strTempDisabledText & "D16, "
            If DU16_Logging Then strTempEnabledText = strTempEnabledText & "DU16, " Else strTempDisabledText = strTempDisabledText & "DU16, "
            If D32_Logging Then strTempEnabledText = strTempEnabledText & "D32, " Else strTempDisabledText = strTempDisabledText & "D32, "
            If DU32_Logging Then strTempEnabledText = strTempEnabledText & "DU32, " Else strTempDisabledText = strTempDisabledText & "DU32, "
            If DFLT_Logging Then strTempEnabledText = strTempEnabledText & "DFLT, " Else strTempDisabledText = strTempDisabledText & "DFLT, "
            If PID_Logging Then strTempEnabledText = strTempEnabledText & "PID, " Else strTempDisabledText = strTempDisabledText & "PID, "
            If CAM_Logging Then strTempEnabledText = strTempEnabledText & "CAM, " Else strTempDisabledText = strTempDisabledText & "CAM, "
            If ERR_Logging Then strTempEnabledText = strTempEnabledText & "ERR, " Else strTempDisabledText = strTempDisabledText & "ERR, "
            If Len(strTempEnabledText) > 0 Then
                strTempEnabledText = Mid(strTempEnabledText, 1, Len(strTempEnabledText) - 2)
            End If
            If Len(strTempDisabledText) > 0 Then
                strTempDisabledText = Mid(strTempDisabledText, 1, Len(strTempDisabledText) - 2)
            End If
            WriteTextLog("Data Found in APM Log File:-")
            WriteTextLog(strTempEnabledText)
            WriteTextLog("")
            WriteTextLog("Data NOT Found in APM Log File:-")
            WriteTextLog(strTempDisabledText)
            WriteTextLog("")
        End If
    End Sub

    Function FormatTextLogValuesFlying(ByVal Range As String, ByVal Alt As String, ByVal Spd As String, ByVal Dist As String, ByVal DistHome As String, ByVal GPS_Sats As String, ByVal GPS_Hdop As String, ByVal Efficiency As String) As String
        FormatTextLogValuesFlying = ""
        Dim MidPointCharTemp As Integer
        Dim MidPointValueTemp As String
        Dim ValueTemp As String = ""
        Dim LineTemp As String = ""

        ValueTemp = Range
        ValueTemp = ValueTemp.PadRight(5, " ")
        LineTemp = LineTemp & ValueTemp

        'Alt is reported in m (meters)
        'Add the ft (feet) conversion to the Alt and format so ~ is always in the middle of the string.
        ValueTemp = Alt & " ~ " & Int(Alt * 3.280833)
        MidPointCharTemp = InStr(ValueTemp, "~")
        MidPointValueTemp = Mid(ValueTemp, 1, MidPointCharTemp)
        MidPointValueTemp = MidPointValueTemp.PadLeft(7, " ")
        ValueTemp = Mid(ValueTemp, MidPointCharTemp + 1, Len(ValueTemp))
        ValueTemp = ValueTemp.PadRight(7, " ")
        ValueTemp = MidPointValueTemp & ValueTemp
        LineTemp = LineTemp & ValueTemp

        'Speed is reported in m/s (meters/second)
        'Add the mph conversion to the Spd and format so ~ is always in the middle of the string.
        ValueTemp = Spd & " ~ " & Int(Spd * 2.23693629)
        MidPointCharTemp = InStr(ValueTemp, "~")
        MidPointValueTemp = Mid(ValueTemp, 1, MidPointCharTemp)
        MidPointValueTemp = MidPointValueTemp.PadLeft(9, " ")
        ValueTemp = Mid(ValueTemp, MidPointCharTemp + 1, Len(ValueTemp))
        ValueTemp = ValueTemp.PadRight(7, " ")
        ValueTemp = MidPointValueTemp & ValueTemp
        LineTemp = LineTemp & ValueTemp

        'Distance travelled is Calculated in km (kilometers), display as meters
        If Dist <> "N/A" Then
            ValueTemp = Format(Val(Dist), "0.00") & " ~ " & Format(Dist * 0.621371192, "0.00")
        Else
            ValueTemp = "N/A ~ N/A"
        End If
        MidPointCharTemp = InStr(ValueTemp, "~")
        MidPointValueTemp = Mid(ValueTemp, 1, MidPointCharTemp)
        MidPointValueTemp = MidPointValueTemp.PadLeft(10, " ")
        ValueTemp = Mid(ValueTemp, MidPointCharTemp + 1, Len(ValueTemp))
        ValueTemp = ValueTemp.PadRight(7, " ")
        ValueTemp = MidPointValueTemp & ValueTemp
        LineTemp = LineTemp & ValueTemp

        'Distance from Home is Calculated in km (kilometers), display as meters
        'Add the feet conversion to the Distance from Home and format so ~ is always in the middle of the string.
        If DistHome <> "N/A" Then
            ValueTemp = Format(DistHome * 1000, "0.00") & " ~ " & Format(DistHome * 3280.8399, "0.00")
        Else
            ValueTemp = "N/A ~ N/A"
        End If
        MidPointCharTemp = InStr(ValueTemp, "~")
        MidPointValueTemp = Mid(ValueTemp, 1, MidPointCharTemp)
        MidPointValueTemp = MidPointValueTemp.PadLeft(11, " ")
        ValueTemp = Mid(ValueTemp, MidPointCharTemp + 1, Len(ValueTemp))
        ValueTemp = ValueTemp.PadRight(7, " ")
        ValueTemp = MidPointValueTemp & ValueTemp
        LineTemp = LineTemp & ValueTemp


        ValueTemp = GPS_Sats
        ValueTemp = ValueTemp.PadLeft(8, " ")
        LineTemp = LineTemp & ValueTemp

        ValueTemp = GPS_Hdop
        ValueTemp = ValueTemp.PadLeft(12, " ")
        LineTemp = LineTemp & ValueTemp

        'Efficiency is reported as mA/s (milliamps per second), convert to mA/m (milliamps per minute)
        If Efficiency <> "N/A" And Efficiency <> "" And Efficiency <> "~~~" Then
            ValueTemp = Format(Efficiency * 60, "0.00")
        Else
            ValueTemp = Efficiency
        End If
        ValueTemp = ValueTemp.PadLeft(14, " ")
        LineTemp = LineTemp & ValueTemp

        FormatTextLogValuesFlying = LineTemp
    End Function
    Function FormatTextLogValuesBattery(ByVal Range As String, ByVal Volt As String, ByVal Vcc As String, ByVal Curr As String, ByVal CurrTot As String, ByVal Efficiency As String) As String
        FormatTextLogValuesBattery = ""
        Dim ValueTemp As String = ""
        Dim LineTemp As String = ""

        ValueTemp = Range
        ValueTemp = ValueTemp.PadRight(5, " ")
        LineTemp = LineTemp & ValueTemp

        'Volts are reported as Voltage * 100 so we need to convert here to Volts
        ValueTemp = Format(Volt / 100, "0.00")
        ValueTemp = ValueTemp.PadLeft(10, " ")
        LineTemp = LineTemp & ValueTemp

        'Vcc is reported as mVoltage (i.e. V * 1000) so we need to convert here to Volts
        ValueTemp = Format(Vcc / 1000, "0.00")
        ValueTemp = ValueTemp.PadLeft(15, " ")
        LineTemp = LineTemp & ValueTemp

        'Curr is reported as mA * 1000 
        ValueTemp = Format(Curr / 100, "0.00")
        ValueTemp = ValueTemp.PadLeft(17, " ")
        LineTemp = LineTemp & ValueTemp

        'CurrTot is reported as mA 
        If CurrTot <> "N/A" Then
            ValueTemp = CurrTot
        Else
            ValueTemp = "N/A"
        End If
        ValueTemp = ValueTemp.PadLeft(17, " ")
        LineTemp = LineTemp & ValueTemp

        'Efficiency is reported as mA/s (milliamps per second), convert to mA/m (milliamps per minute)
        If Efficiency <> "N/A" And Efficiency <> "~~~" Then
            ValueTemp = Format(Efficiency * 60, "0.00")
        Else
            ValueTemp = Efficiency
        End If
        ValueTemp = ValueTemp.PadLeft(16, " ")
        LineTemp = LineTemp & ValueTemp

        FormatTextLogValuesBattery = LineTemp
    End Function
    Function FormatTextLogValuesVibration(Range As String, AccX As Single, AccY As Single, AccZ As Single, Spd As Single, Alt As Single) As String
        Dim ValueTemp As String = ""
        Dim LineTemp As String = ""
        Dim Value As Single = 0

        ValueTemp = Range
        ValueTemp = ValueTemp.PadRight(5, " ")
        LineTemp = LineTemp & ValueTemp

        ValueTemp = AccX.ToString("00.000", CultureInfo.CurrentCulture.NumberFormat)  'Format(Val(AccX), "00.000")
        ValueTemp = ValueTemp.PadLeft(10, " ")
        LineTemp = LineTemp & ValueTemp

        ValueTemp = AccY.ToString("00.000", CultureInfo.CurrentCulture.NumberFormat)   'Format(Val(AccY), "00.000")
        ValueTemp = ValueTemp.PadLeft(10, " ")
        LineTemp = LineTemp & ValueTemp

        ValueTemp = AccZ.ToString("00.000", CultureInfo.CurrentCulture.NumberFormat)    'Format(Val(AccZ), "00.000")
        ValueTemp = ValueTemp.PadLeft(10, " ")
        LineTemp = LineTemp & ValueTemp

        ValueTemp = Spd.ToString("00", CultureInfo.CurrentCulture.NumberFormat)    'Format(Val(AccZ), "00.000")
        ValueTemp = ValueTemp.PadLeft(7, " ")
        LineTemp = LineTemp & ValueTemp

        ValueTemp = Alt.ToString("00.00", CultureInfo.CurrentCulture.NumberFormat)    'Format(Val(AccZ), "00.000")
        ValueTemp = ValueTemp.PadLeft(12, " ")
        LineTemp = LineTemp & ValueTemp

        FormatTextLogValuesVibration = LineTemp
    End Function

    Public Sub WriteParamHeader()
        If Param_Issue_Found = False Then
            WriteTextLog(vbNewLine)
            WriteTextLog("PARAMETER ISSUES:-")
            Param_Issue_Found = True
        End If
    End Sub

    Public Sub FindLoggingData()
        'Do some warnings about DEVELOPER IGNORES
        Debug.Print("1st Pass Started, Finding Logging Data...")
        If Ignore_CTUN_Logging = True Then
            Beep() : Threading.Thread.Sleep(500) : Beep() : Threading.Thread.Sleep(500) : Beep() : Threading.Thread.Sleep(500)
            MsgBox("CTUN will be ignored", vbOKOnly, "DEVELOPER SETTINGS")
        End If

        'Read the File line by line
        Dim objReader As New System.IO.StreamReader(strLogPathFileName)
        Do While objReader.Peek() <> -1
            DataArrayCounter = 0
            DataSplit = ""

            Data = objReader.ReadLine()
            'Debug.Print("1st Pass Processing:-" & Str(DataLine) & ": " & Data)
            ' Split the data into the DataArray()
            For n = 1 To Len(Data)
                DataChar = Mid(Data, n, 1)
                If (DataChar = "," Or DataChar = " ") Then
                    If DataSplit <> "" Then
                        DataArray(DataArrayCounter) = DataSplit
                        'Debug.Print("--- Paramter " & DataArrayCounter & " = " & DataArray(DataArrayCounter))
                        DataSplit = ""
                        DataArrayCounter = DataArrayCounter + 1
                        'sometimes some bad data is found in the logs.
                        'ensure we do not make more than 25 enteries into the array.
                        If DataArrayCounter > 25 Then DataArrayCounter = 25
                    End If
                Else
                    DataSplit = DataSplit + DataChar
                End If
            Next n
            'Capture the very Last entry (i.e. there is no , after the last entry.
            'DataArrayCounter is now = to the last entry in the data spilt.
            DataArray(DataArrayCounter) = DataSplit
            'Debug.Print("--- Paramter " & DataArrayCounter & " = " & DataArray(DataArrayCounter))

            If DataArray(0) = "ArduCopter" Then ArduType = "ArduCopter" : ArduVersion = DataArray(1) : ArduBuild = DataArray(2)
            If DataArray(1) = "ArduCopter" Then ArduType = "ArduCopter" : ArduVersion = DataArray(2) : ArduBuild = DataArray(3)
            If DataArray(0) = "ArduPlane" Then ArduType = "ArduPlane" : ArduVersion = DataArray(1) : ArduBuild = DataArray(2)
            If DataArray(0) = "Free" And DataArray(1) = "RAM:" Then APM_Free_RAM = DataArray(2)
            If DataArray(0) = "APM" Then APM_Version = DataArray(1)
            If DataArray(0) = "PARM" And DataArray(1) = "FRAME" Then APM_Frame_Type = DataArray(2)
            If DataArray(0) = "FMT" And DataArray(3) = "MOT" Then
                APM_No_Motors = Mid(DataArray(DataArrayCounter), 4, Len(DataArray(DataArrayCounter)) - 1)
            End If
            If DataArray(0) = "IMU" Then IMU_Logging = True
            If DataArray(0) = "GPS" Then GPS_Logging = True
            If DataArray(0) = "CTUN" And Ignore_CTUN_Logging = False Then CTUN_Logging = True
            If DataArray(0) = "PM" Then PM_Logging = True
            If DataArray(0) = "CURR" Then CURR_Logging = True
            If DataArray(0) = "NTUN" Then NTUN_Logging = True
            If DataArray(0) = "MSG" Then MSG_Logging = True
            If DataArray(0) = "ATUN" Then ATUN_Logging = True
            If DataArray(0) = "ATDE" Then ATDE_logging = True
            If DataArray(0) = "MOT" Then MOT_Logging = True
            If DataArray(0) = "OF" Then OF_Logging = True
            If DataArray(0) = "MAG" Then MAG_Logging = True
            If DataArray(0) = "CMD" Then CMD_Logging = True
            If DataArray(0) = "ATT" Then ATT_Logging = True
            If DataArray(0) = "INAV" Then INAV_Logging = True
            If DataArray(0) = "MODE" Then MODE_Logging = True
            If DataArray(0) = "STRT" Then STRT_logging = True
            If DataArray(0) = "EV" Then EV_Logging = True
            If DataArray(0) = "D16" Then D16_Logging = True
            If DataArray(0) = "DU16" Then DU16_Logging = True
            If DataArray(0) = "D32" Then D32_Logging = True
            If DataArray(0) = "DU32" Then DU32_Logging = True
            If DataArray(0) = "DFLT" Then DFLT_Logging = True
            If DataArray(0) = "PID" Then PID_Logging = True
            If DataArray(0) = "CAM" Then CAM_Logging = True
            If DataArray(0) = "ERR" Then ERR_Logging = True

            TotalDataLines = TotalDataLines + 1
        Loop
        objReader.Close()
        Debug.Print("Success!")
    End Sub

    Public Function FileNameWithoutPath(ByVal FullPath As String) As String
        Return System.IO.Path.GetFileName(FullPath).ToString
    End Function
    Public Sub SelectFile()
        Debug.Print("Open File")
        OpenFD.InitialDirectory = "C:\Program Files (x86)\Mission Planner\logs\"
        OpenFD.Title = "Open a Text File"
        'OpenFD.FileName = "????-??-?? ??-??*.log"
        OpenFD.FileName = "*.log" 'Allow any log file to be selected

        'Display the Open File Dialog Window.
        If OpenFD.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
            Dim strLogFileName As String
            strLogFileName = ""
            Debug.Print("User Selected Cancel " & strLogFileName)
            FileOpened = False
            Exit Sub
        Else
            'User has selected a file
            strLogPathFileName = OpenFD.FileName
            strLogFileName = FileNameWithoutPath(strLogPathFileName)
            'txtboxFileName.Text = strLogFileName
            Debug.Print("User Selected File " & strLogPathFileName)
            richtxtLogAnalysis.Clear()
            FileOpened = True
        End If
        Debug.Print("Open File Completed." & vbNewLine)
        If FileOpened = True Then
            btnAnalyze.Visible = True
            btnDisplayVibrationChart.Visible = True
            btnVibrations.Visible = True
            PictureBox1.Visible = False
        ElseIf FileOpened = False Then
            btnAnalyze.Visible = False
            btnDisplayVibrationChart.Visible = False
            btnVibrations.Visible = False
            PictureBox1.Visible = True
        End If
    End Sub

    Private Function AutoUpdate(ByVal CurrentVersion As String) As Integer
        '### Note: This has all changed to make the Updater Program push the file changes
        '###    instead of the Log_Analyzer pulling them from the FTP.

        'The function will check an FTP website to see if there are any updates for a program.
        'If found it will download the updated program to the local machine. Then it will download a
        'program called UpdaterProgram.exe (used to update the folders on the local machine). 
        'Once we have these program is will create a file on the local machine called 
        'Update.txt, this file contains all the information like version numbers and installation
        'paths that will be required by the UpdaterProgram. Once these are in place the
        'APM_Log_Analiser program will pass control to the UpdaterProgram and then close down.
        'The UpdaterProgram will install the new APM Log Analiser.exe and then start the
        'new version.

        'The Users CurrentVersion must be passed in the format "v0.0"
        'The function will return:
        '   False = Failed to execute the code successfully
        '   True = Code executed successfully but program did not require updating.
        '   99 = Code executed successfully and a new update was downloaded as required.

        'The website must contain three files:
        '   APM Log File Analiser.exe = The latest program .exe file.
        '   UpdaterProgram.exe = The lastest updater program .exe file.
        '   Versions.txt = Must contain a list of programs and versions available.

        'Versions.txt formatting (create in notepad):
        '   [ProgramName]=[V?.?]=[DownloadLink]
        '   [ProgramName]=[V?.?]=[DownloadLink]
        '   [ProgramName]=[V?.?]=[DownloadLink]
        '   etc.

        'Versions.txt Example:
        'APM Log File Analiser.exe=v1.0=ftp://bionicbone.sshcs.com/APM Log File Analiser.exe
        'Avitar.exe=v1.0=ftp://bionicbone.sshcs.com/UpdateAvitar.exe
        'ThisProgram.exe=v1.0=ftp://bionicbone.sshcs.com/Updates with this name for example.exe


        'Delcare the AutoUpdate Variables
        Try
            Debug.Print("Checking for updates...")

            Debug.Print("ProgramInstallationFolder: " & ProgramInstallationFolder)
            'Debug.Print("TempProgramInstallationFolder " & TempProgramInstallationFolder)

            If CurrentVersion = "" Then CurrentVersion = "v9.9.9.9" 'if the function is called incorrectly, force an update!
            AutoUpdate = False                                          'Set as function failed
            Dim ProgramName As String = "APM Log File Analiser.exe"     'This program we want to update.
            Dim UpdaterProgramName As String = "UpdaterProgram.exe"     'This program we will use to update the main program.
            Dim UpdateToLocaleFolder As String = "C:\Temp\"
            Dim SiteName As String = "guq;##cjpojdcpof~ttidt~dpn#"      'ftp://bionicbone.sshcs.com/
            Dim UserName As String = "cjpojdcpof"                       'bionicbone
            Dim Password As String = "s:8yu$S3b[gF"                     'r97xt#R2aZfE
            Dim VersionFileName As String = "Versions.txt"
            Dim GetVer As String = ""
            Dim GetVerLink As String = ""
            Dim GetUpd As Integer = 0

            'Debug.Print(DecryptData(SiteName))
            'Debug.Print(DecryptData(UserName))
            'Debug.Print(DecryptData(Password))

            'Debug.Print("Update Server: " & SiteName)
            Debug.Print("Program Name: " & ProgramName)
            Debug.Print("Current User Version: " & MyCurrentVersionNumber)

            Debug.Print("Opening FTP update Connection...")
            Dim WebRequest As System.Net.FtpWebRequest = System.Net.FtpWebRequest.Create(DecryptData(SiteName) & VersionFileName)
            Debug.Print("Requesting file: " & VersionFileName)
            WebRequest.Credentials = New Net.NetworkCredential(DecryptData(UserName), DecryptData(Password))
            Debug.Print("Waiting for a response...")
            Dim WebResponse As System.Net.FtpWebResponse = WebRequest.GetResponse
            Dim stream1 As System.IO.StreamReader = New System.IO.StreamReader(WebResponse.GetResponseStream())
            Dim ReadSource As String = stream1.ReadToEnd
            'Debug.Print(VersionFileName & " contents: " & ReadSource)
            Dim Regex As New System.Text.RegularExpressions.Regex(ProgramName & "=v(\d+).(\d+).(\d+).(\d+)=(.*?).exe")
            Debug.Print("Using Regex to find Mastches: " & ProgramName)
            Dim matches As MatchCollection = Regex.Matches(ReadSource)
            Debug.Print("Found: " & matches.Count & ", program is known to server!")

            For Each match As Match In matches
                Dim RegSplit() As String = Split(ReadSource.ToString, "=")
                Debug.Print("Getting Current Server Version for Match: " & match.Index + 1 & "...")
                GetVer = RegSplit(1)
                GetVerLink = RegSplit(2)
                Debug.Print("Current Server Version: " & GetVer)
                'Debug.Print("Current Server Link: " & GetVerLink)
            Next

            Debug.Print("Checking versions...")
            If GetVer > MyCurrentVersionNumber Then
                Debug.Print("Update is available!")

                Debug.Print("Updates are currently forced due to developments in progress!")

                'Close current connections in case FTP will only allow one connection!
                Debug.Print("Closing current update FTP connection...")
                stream1.Close()
                WebResponse.Close()

                'Prepare to Get the updated program file via FTP.
                Dim buffer(1023) As Byte ' Allocate a read buffer of 1kB size
                Dim output As IO.Stream ' A file to save response
                Dim bytesIn As Integer = 0 ' Number of bytes read to buffer
                Dim totalBytesIn As Integer = 0 ' Total number of bytes received (= filesize)

                ''Prepare new FTP connection to Get the required file.
                'Debug.Print("Preparing a new FTP Download connection...")
                'Debug.Print("Requesting file: " & ProgramName & "...")
                'Dim ftpRequest As System.Net.FtpWebRequest = System.Net.FtpWebRequest.Create(DecryptData(SiteName) & ProgramName)
                'ftpRequest.Credentials = New Net.NetworkCredential(DecryptData(UserName), DecryptData(Password))
                'ftpRequest.Method = Net.WebRequestMethods.Ftp.DownloadFile
                'Debug.Print("Waiting for a response...")
                'Dim ftpResponse As System.Net.FtpWebResponse = ftpRequest.GetResponse
                'Debug.Print("Found FTP File.")

                ''Need to check local folder exists, if not create it.
                'Debug.Print("Preparing to copy file to local machine...")
                'Debug.Print("Checking for the local folder exists: " & UpdateToLocaleFolder)
                'If Directory.Exists(UpdateToLocaleFolder) Then
                '    Debug.Print("Found: " & UpdateToLocaleFolder & ", no further action required.")
                'Else
                '    Debug.Print("Not Found: " & UpdateToLocaleFolder)
                '    Debug.Print("Creating: " & UpdateToLocaleFolder)
                '    Directory.CreateDirectory(UpdateToLocaleFolder)
                '    Debug.Print("Success!")
                'End If

                ''Need to check that the file we are about to create does not already exist.
                'Debug.Print("Checking previous update does not still exist: " & UpdateToLocaleFolder & ProgramName)
                'If File.Exists(UpdateToLocaleFolder & ProgramName) Then
                '    Debug.Print("Found: " & UpdateToLocaleFolder & ProgramName)
                '    Debug.Print("Attempting to delete old update file: " & UpdateToLocaleFolder & ProgramName)
                '    File.Delete(UpdateToLocaleFolder & ProgramName)
                '    Debug.Print("Success!")
                'Else
                '    Debug.Print("Not Found: " & UpdateToLocaleFolder & ProgramName & ", no further action required.")
                'End If

                '' Write the content to the output file
                'Debug.Print("Creating Local Update File: " & UpdateToLocaleFolder & ProgramName)
                'output = System.IO.File.Create(UpdateToLocaleFolder & ProgramName)
                'Debug.Print("Success, at least by name, file is still empty!")

                'Debug.Print("Opening the stream...")
                'Dim stream2 As System.IO.Stream = ftpRequest.GetResponse.GetResponseStream

                'Debug.Print("Writing file contents...")
                'bytesIn = 1 ' Set initial value to 1 to get into loop. We get out of the loop when bytesIn is zero
                'Do Until bytesIn < 1
                '    bytesIn = stream2.Read(buffer, 0, 1024) ' Read max 1024 bytes to buffer and get the actual number of bytes received
                '    If bytesIn > 0 Then
                '        ' Dump the buffer to a file
                '        output.Write(buffer, 0, bytesIn)
                '        ' Calc total filesize
                '        totalBytesIn += bytesIn
                '        ' Show user the filesize
                '        'Label1.Text = totalBytesIn.ToString + " Bytes Downloaded"
                '        Application.DoEvents()
                '    End If
                'Loop
                'Debug.Print("Success!")
                '' Close streams
                'Debug.Print("Closing FTP download connection...")
                'output.Close()
                'stream2.Close()




                'Prepare to Get the updater program file via FTP.
                bytesIn = 0 ' Number of bytes read to buffer
                totalBytesIn = 0 ' Total number of bytes received (= filesize)

                'Prepare new FTP connection to Get the required file.
                Debug.Print("Preparing a new FTP Download connection...")
                Debug.Print("Requesting file: " & ProgramName & "...")
                Dim ftpRequest2 As System.Net.FtpWebRequest = System.Net.FtpWebRequest.Create(DecryptData(SiteName) & UpdaterProgramName)
                ftpRequest2.Credentials = New Net.NetworkCredential(DecryptData(UserName), DecryptData(Password))
                ftpRequest2.Method = Net.WebRequestMethods.Ftp.DownloadFile
                Debug.Print("Waiting for a response...")
                Dim ftpResponse2 As System.Net.FtpWebResponse = ftpRequest2.GetResponse
                Debug.Print("Found FTP File.")

                'Need to check local folder exists, if not create it.
                Debug.Print("Preparing to copy file to local machine...")
                Debug.Print("Checking for the local folder exists: " & UpdateToLocaleFolder)
                If Directory.Exists(UpdateToLocaleFolder) Then
                    Debug.Print("Found: " & UpdateToLocaleFolder & ", no further action required.")
                Else
                    Debug.Print("Not Found: " & UpdateToLocaleFolder)
                    Debug.Print("Creating: " & UpdateToLocaleFolder)
                    Directory.CreateDirectory(UpdateToLocaleFolder)
                    Debug.Print("Success!")
                End If


                'Need to check that the file we are about to create does not already exist.
                'We know the folder already exists from the step above
                Debug.Print("Checking previous update does not still exist: " & UpdateToLocaleFolder & UpdaterProgramName)
                If File.Exists(UpdateToLocaleFolder & UpdaterProgramName) Then
                    Debug.Print("Found: " & UpdateToLocaleFolder & UpdaterProgramName)
                    Debug.Print("Attempting to delete old update file: " & UpdateToLocaleFolder & UpdaterProgramName)
                    File.Delete(UpdateToLocaleFolder & UpdaterProgramName)
                    Debug.Print("Success!")
                Else
                    Debug.Print("Not Found: " & UpdateToLocaleFolder & UpdaterProgramName & ", no further action required.")
                End If

                ' Write the content to the output file
                Debug.Print("Creating Local Update File: " & UpdateToLocaleFolder & UpdaterProgramName)
                output = System.IO.File.Create(UpdateToLocaleFolder & UpdaterProgramName)
                Debug.Print("Success, at least by name, file is still empty!")

                Debug.Print("Opening the stream...")
                Dim stream3 As System.IO.Stream = ftpRequest2.GetResponse.GetResponseStream

                Debug.Print("Writing file contents...")
                bytesIn = 1 ' Set initial value to 1 to get into loop. We get out of the loop when bytesIn is zero
                Do Until bytesIn < 1
                    bytesIn = stream3.Read(buffer, 0, 1024) ' Read max 1024 bytes to buffer and get the actual number of bytes received
                    If bytesIn > 0 Then
                        ' Dump the buffer to a file
                        output.Write(buffer, 0, bytesIn)
                        ' Calc total filesize
                        totalBytesIn += bytesIn
                        ' Show user the filesize
                        'Label1.Text = totalBytesIn.ToString + " Bytes Downloaded"
                        Application.DoEvents()
                    End If
                Loop
                Debug.Print("Success!")
                ' Close streams
                Debug.Print("Closing FTP download connection...")
                output.Close()
                stream3.Close()


                '' ### Updated by KMG to ensure the gLabel.dll is moved to the Local PC.
                ''Prepare to Get the gLabel.dll file via FTP.
                'bytesIn = 0 ' Number of bytes read to buffer
                'totalBytesIn = 0 ' Total number of bytes received (= filesize)

                ''Prepare new FTP connection to Get the required file.
                'Debug.Print("Preparing a new FTP Download connection...")
                'Debug.Print("Requesting file: " & "gLabel.dll" & "...")
                'Dim ftpRequest3 As System.Net.FtpWebRequest = System.Net.FtpWebRequest.Create(DecryptData(SiteName) & "gLabel.dll")
                'ftpRequest3.Credentials = New Net.NetworkCredential(DecryptData(UserName), DecryptData(Password))
                'ftpRequest3.Method = Net.WebRequestMethods.Ftp.DownloadFile
                'Debug.Print("Waiting for a response...")
                'Dim ftpResponse3 As System.Net.FtpWebResponse = ftpRequest3.GetResponse
                'Debug.Print("Found FTP File.")

                ''Need to check that the file we are about to create does not already exist.
                ''We know the folder already exists from the step above
                'Debug.Print("Checking previous update does not still exist: " & UpdateToLocaleFolder & "gLabel.dll")
                'If File.Exists(UpdateToLocaleFolder & "gLabel.dll") Then
                '    Debug.Print("Found: " & UpdateToLocaleFolder & "gLabel.dll")
                '    Debug.Print("Attempting to delete old update file: " & UpdateToLocaleFolder & "gLabel.dll")
                '    File.Delete(UpdateToLocaleFolder & "gLabel.dll")
                '    Debug.Print("Success!")
                'Else
                '    Debug.Print("Not Found: " & UpdateToLocaleFolder & "gLabel.dll" & ", no further action required.")
                'End If

                '' Write the content to the output file
                'Debug.Print("Creating Local Update File: " & UpdateToLocaleFolder & "gLabel.dll")
                'output = System.IO.File.Create(UpdateToLocaleFolder & "gLabel.dll")
                'Debug.Print("Success, at least by name, file is still empty!")

                'Debug.Print("Opening the stream...")
                'Dim stream4 As System.IO.Stream = ftpRequest3.GetResponse.GetResponseStream

                'Debug.Print("Writing file contents...")
                'bytesIn = 1 ' Set initial value to 1 to get into loop. We get out of the loop when bytesIn is zero
                'Do Until bytesIn < 1
                '    bytesIn = stream4.Read(buffer, 0, 1024) ' Read max 1024 bytes to buffer and get the actual number of bytes received
                '    If bytesIn > 0 Then
                '        ' Dump the buffer to a file
                '        output.Write(buffer, 0, bytesIn)
                '        ' Calc total filesize
                '        totalBytesIn += bytesIn
                '        ' Show user the filesize
                '        'Label1.Text = totalBytesIn.ToString + " Bytes Downloaded"
                '        Application.DoEvents()
                '    End If
                'Loop
                'Debug.Print("Success!")
                '' Close streams
                'Debug.Print("Closing FTP download connection...")
                'output.Close()
                'stream4.Close()







                'Create a file that passes all the information required to update the main program
                'to the updater program.
                Debug.Print("Creating an installation file for the updater (Update.txt)...")

                Dim ScriptFileName As String = "Update.txt"
                Debug.Print("Checking previous update does not still exist: " & UpdateToLocaleFolder & ScriptFileName)
                If File.Exists(UpdateToLocaleFolder & ScriptFileName) Then
                    Debug.Print("Found: " & UpdateToLocaleFolder & ScriptFileName)
                    Debug.Print("Attempting to delete old update file: " & UpdateToLocaleFolder & ScriptFileName)
                    File.Delete(UpdateToLocaleFolder & ScriptFileName)
                    Debug.Print("Success!")
                Else
                    Debug.Print("Not Found: " & UpdateToLocaleFolder & ScriptFileName & ", no further action required.")
                End If

                Dim objWriter As StreamWriter = File.CreateText(UpdateToLocaleFolder & ScriptFileName)


                objWriter.WriteLine("APM Log Analysis Updater file")
                objWriter.WriteLine("")
                objWriter.WriteLine("Program Name =" & ProgramName)
                objWriter.WriteLine("Current Version =" & MyCurrentVersionNumber)
                objWriter.WriteLine("Update Version =" & GetVer)
                objWriter.WriteLine("")
                objWriter.WriteLine("Installation Folder =" & ProgramInstallationFolder & "\")
                objWriter.WriteLine("Updater Folder =" & UpdateToLocaleFolder)
                objWriter.Flush()
                objWriter.Close()

                AutoUpdate = 99

                'Call the updater program to do its stuff
                Debug.Print("Start the Updater Program: " & UpdateToLocaleFolder & UpdaterProgramName)
                System.Diagnostics.Process.Start(UpdateToLocaleFolder & UpdaterProgramName)

                Debug.Print("Close the APM Log Anayliser Program so it can update...")
                Debug.Print(vbNewLine)
                Close()

            Else
                'Close current connections!
                Debug.Print("Program does not require an update.")
                Debug.Print("Closing FTP update connection...")
                stream1.Close()
                WebResponse.Close()
                Debug.Print("Success!")
                AutoUpdate = True
            End If
            Debug.Print("The check for updates has completed successfully")
            Debug.Print(vbNewLine)

        Catch
            AutoUpdate = False
            Debug.Print("An Error Occured in the Update Function")
            Debug.Print("Exit Function")
            Debug.Print(vbNewLine)
            Exit Function
        End Try

    End Function
    Private Sub AboutInfo()
        Dim strTempText As String = ""
        strTempText = strTempText & "AMP Log Anaylser is Free to use, but if you" & vbNewLine
        strTempText = strTempText & "find it useful then please send a small donation" & vbNewLine
        strTempText = strTempText & "using the Pay Pal button as every bit helps :)" & vbNewLine
        strTempText = strTempText & vbNewLine
        strTempText = strTempText & "AMP Log Anaylser should not be sold or used for commercial" & vbNewLine
        strTempText = strTempText & "purposes without written permission etc. etc. etc." & vbNewLine
        strTempText = strTempText & vbNewLine
        strTempText = strTempText & "For more support and instructions please visit the" & vbNewLine
        strTempText = strTempText & "RC Groups dedicated thread." & vbNewLine
        strTempText = strTempText & "http://www.rcgroups.com/forums/showthread.php?t=2151318" & vbNewLine
        strTempText = strTempText & vbNewLine
        strTempText = strTempText & "Thank you for your support :)" & vbNewLine
        strTempText = strTempText & "---Version: " & CurrentPublishVersionNumber & vbNewLine
        MsgBox(strTempText, vbInformation & vbOKOnly, "About")
    End Sub

    Private Sub UpdateNow()
        'display the Updating splash screen.
        frmUpdate.Show()
        frmUpdate.Refresh()
        Call AutoUpdate(CurrentPublishVersionNumber)
        frmUpdate.Close()
    End Sub

    Private Function GetCurrentDeploymentCurrentVersion() As String
        Dim CurrentVersion As String = ""
        CurrentVersion = System.Reflection.Assembly.GetExecutingAssembly.ToString

        'if running the deployed application, you can get the version
        '  from the ApplicationDeployment information. If you try
        '  to access this when you are running in Visual Studio, it will not work.
        If (ApplicationDeployment.IsNetworkDeployed) Then
            CurrentVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
        Else
            If CurrentVersion <> "" Then
                CurrentVersion = System.Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString

            End If
        End If
        GetCurrentDeploymentCurrentVersion = CurrentVersion
    End Function
    Public Function StandardDeviation(NumericArray As Object) As Double

        Dim dblSum As Double = 0
        Dim dblSumSqdDevs As Double = 0
        Dim dblMean As Double = 0
        Dim lngCount As Long = 0
        Dim dblAnswer As Double = 0
        Dim vElement As Object = DBNull.Value
        Dim lngStartPoint As Long = 0
        Dim lngEndPoint As Long = 0
        Dim lngCtr As Long = 0

        'if NumericArray is not an array, this statement will
        'raise an error in the errorhandler
        lngCount = UBound(NumericArray)
        lngCount = 0

        'the check below will allow
        'for 0 or 1 based arrays.
        vElement = NumericArray(0)

        lngStartPoint = IIf(Err.Number = 0, 0, 1)
        lngEndPoint = UBound(NumericArray)

        'get sum and sample size
        For lngCtr = lngStartPoint To lngEndPoint
            vElement = NumericArray(lngCtr)
            If IsNumeric(vElement) Then
                lngCount = lngCount + 1
                dblSum = dblSum + CDbl(vElement)
            End If
        Next

        'get mean
        If lngCount > 1 Then
            dblMean = dblSum / lngCount

            'get sum of squared deviations
            For lngCtr = lngStartPoint To lngEndPoint
                vElement = NumericArray(lngCtr)

                If IsNumeric(vElement) Then
                    dblSumSqdDevs = dblSumSqdDevs + ((vElement - dblMean) ^ 2)
                End If
            Next

            'divide result by sample size - 1 and get square root. 
            'this function calculates standard deviation of a sample.  
            'If your  set of values represents the population, use sample
            'size not sample size - 1
            If lngCount > 1 Then
                lngCount = lngCount - 1 'eliminate for population values
                dblAnswer = Math.Sqrt(dblSumSqdDevs / lngCount)
            End If
        End If
        StandardDeviation = dblAnswer
    End Function

    Private Sub richtxtLogAnalysis_LinkClicked_1(sender As Object, e As LinkClickedEventArgs) Handles richtxtLogAnalysis.LinkClicked
        System.Diagnostics.Process.Start(e.LinkText)
    End Sub

    Private Sub ShowVibrationChart(txtWarning As String)
        'Update the Chart Screen
        frmVibrationChart.Show()
        frmVibrationChart.lblWarning.Text = txtWarning
        frmVibrationChart.richtextLogVibration.AppendText("Vibration Summary over " & Log_IMU_DLs_for_Slow_FLight & " successive IMU readings (exceptions filtered):" & vbNewLine)
        frmVibrationChart.richtextLogVibration.AppendText("          AccX      AccY      AccZ      Spd       Alt" & vbNewLine)
        frmVibrationChart.richtextLogVibration.AppendText(FormatTextLogValuesVibration("Max", Log_IMU_Max_AccX, Log_IMU_Max_AccY, Log_IMU_Max_AccZ, log_IMU_Max_Spd, log_IMU_Max_Alt) & vbNewLine)
        frmVibrationChart.richtextLogVibration.AppendText(FormatTextLogValuesVibration("Avg", Log_IMU_Sum_AccX / Log_IMU_DLs_for_Slow_FLight, Log_IMU_Sum_AccY / Log_IMU_DLs_for_Slow_FLight, Log_IMU_Sum_AccZ / Log_IMU_DLs_for_Slow_FLight, log_IMU_Sum_Spd / Log_IMU_DLs_for_Slow_FLight, log_IMU_Sum_Alt / Log_IMU_DLs_for_Slow_FLight) & vbNewLine)
        frmVibrationChart.richtextLogVibration.AppendText(FormatTextLogValuesVibration("Min", Log_IMU_Min_AccX, Log_IMU_Min_AccY, Log_IMU_Min_AccZ, log_IMU_Min_Spd, log_IMU_Min_Alt) & vbNewLine)
        frmVibrationChart.richtextLogVibration.AppendText(FormatTextLogValuesVibration("StDev", StandardDeviation(IMU_Vibration_AccX), StandardDeviation(IMU_Vibration_AccY), StandardDeviation(IMU_Vibration_AccZ), 0, 0) & vbNewLine)
        frmVibrationChart.richtextLogVibration.AppendText("Datalines: " & IMU_Vibration_Start_DL & " ~ " & IMU_Vibration_End_DL & vbNewLine)


        'Write the data to the chart
        For n = 0 To 4999
            frmVibrationChart.Chart1.Series("AccX").Points.AddY(IMU_Vibration_AccX(n))
            frmVibrationChart.Chart1.Series("AccY").Points.AddY(IMU_Vibration_AccY(n))
            frmVibrationChart.Chart1.Series("AccZ").Points.AddY(IMU_Vibration_AccZ(n))
            frmVibrationChart.Chart1.Series("Altitude").Points.AddY(IMU_Vibration_Alt(n))
            frmVibrationChart.Chart1.Series("Speed").Points.AddY(IMU_Vibration_Spd(n))
            'Add the Marker Lines
            frmVibrationChart.Chart1.Series("XYHighLine").Points.AddY(3)
            frmVibrationChart.Chart1.Series("XYLowLine").Points.AddY(-3)
            frmVibrationChart.Chart1.Series("ZHighLine").Points.AddY(-5)
            frmVibrationChart.Chart1.Series("ZLowLine").Points.AddY(-15)
        Next
    End Sub
    Private Sub btnDisplayVibrationChart_Click(sender As Object, e As EventArgs) Handles btnDisplayVibrationChart.Click
        Call ShowVibrationChart("User Forced Chart being Displayed..")
    End Sub

    Private Sub CopyForRCGroupsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Call CopytoClip() 'changed this to its own sub so it can be called via anything except the one button it was assigned to

    End Sub
    Private Sub CopytoClip()

        Dim SelectedText As String = ""
        Dim RCGFormattedText As String = "[CODE][COLOR=""Black""]"
        Dim Character As String = ""
        Dim LineData As String = ""
        Dim DataCounter As Integer = 1
        SelectedText = richtxtLogAnalysis.SelectedText

        If SelectedText = "" Then
            MsgBox("Please select some text first!", MsgBoxStyle.Exclamation & vbOKOnly, "Error")
        End If

        While DataCounter < Len(SelectedText) - 1
            While Character <> Chr(10) And DataCounter < Len(SelectedText) + 1
                Character = Mid(SelectedText, DataCounter, 1)
                Debug.Print(Character)
                LineData = LineData & Character
                DataCounter = DataCounter + 1
            End While

            'work out the special formatting for RCGroups

            Debug.Print("Line Found:" & LineData)

            If InStr(LineData, "WARNING:") Then LineData = "[COLOR=""Red""]" & LineData & "[/COLOR]"
            If InStr(LineData, "ERROR:") Then LineData = "[COLOR=""Red""]" & LineData & "[/COLOR]"
            If InStr(LineData, "Error:") Then LineData = "[COLOR=""Red""]" & LineData & "[/COLOR]"
            If InStr(LineData, "Warning:") Then LineData = "[COLOR=""Orange""]" & LineData & "[/COLOR]"
            If InStr(LineData, "Mode") Then LineData = "[COLOR=""Blue""]" & LineData & "[/COLOR]"
            If InStr(LineData, "Flight Time") Then LineData = "[COLOR=""Blue""]" & LineData & "[/COLOR]"
            If InStr(LineData, "Testing") Then LineData = "[COLOR=""Purple""]" & LineData & "[/COLOR]"
            If InStr(LineData, "Information") Then LineData = "[COLOR=""Green""]" & LineData & "[/COLOR]"
            If InStr(LineData, "***") Then LineData = "[COLOR=""Green""]" & LineData & "[/COLOR]"
            If InStr(LineData, "http://") Then LineData = "[U][URL=""" & LineData & """]" & LineData & "[/URL][/U]"

            RCGFormattedText = RCGFormattedText & LineData
            Character = ""
            LineData = ""
        End While

        'Add the CODE end
        RCGFormattedText = RCGFormattedText & "[/COLOR][/CODE]"
        Clipboard.Clear()
        Clipboard.SetText(RCGFormattedText)

    End Sub

    Private Sub richtxtLogAnalysis_KeyDown(sender As Object, e As KeyEventArgs) Handles richtxtLogAnalysis.KeyDown
        If e.KeyCode = Keys.Escape Then
            ESCPress = True
        End If
    End Sub

    Private Sub btnLoadLog_Click(sender As Object, e As EventArgs) Handles btnLoadLog.Click
        Call SelectFile()
    End Sub
    Private Sub btnAnalyze_Click(sender As Object, e As EventArgs) Handles btnAnalyze.Click
        If strLogPathFileName <> "" Then
            richtxtLogAnalysis.Clear()
            lblEsc.Visible = True
            Call ReadFile(strLogPathFileName)
            lblEsc.Visible = False
        Else
            MsgBox("Please Select a Valid APM Log File first!", MsgBoxStyle.Exclamation & vbOKOnly, "APM Log Error")
        End If
    End Sub
    Private Sub btnVibrations_Click(sender As Object, e As EventArgs) Handles btnVibrations.Click
        Call CopytoClip() 'changed this to its own sub so it can be called via anything except the one button it was assigned to
    End Sub
    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        Call AboutInfo()
    End Sub
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Call UpdateNow()
    End Sub
    Private Sub btnMCB_Click(sender As Object, e As EventArgs) Handles btnMCB.Click

        My.Computer.Audio.Play(My.Resources.MCB, AudioPlayMode.Background)
    End Sub
    Private Sub btnDonate_Click(sender As Object, e As EventArgs) Handles btnDonate.Click
        Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=RZFHAR67833ZQ")

    End Sub
End Class


'v0.1 BETA 21/04/2014 - First Issue on Net.
'   BugFix: TheQuestor reported issues with US / UK date conversions, issued an update under the same version number 
'           but TheQuestor reports still the same. This maybe the "Expired" flaging in the LFA file - Released
'
'v0.2 BETA 22/04/2014 - Released
'           Issued a new issue that can write a new LFA file when a new version number of program is detected.
'           Also altered the Dates calculations again in an attempt to fix TheQuestors issue.
'
'v0.21 BETA 22/04/2014
'           Changed the Filename Template from ????-??-?? ??-??*.log to *.log, this should allow any log file
'           to be opened - Requested by JNJO
'           
'v0.3 BETA 23/04/20114 - Released
'           Altered the Dates calculations again in an attempt to fix TheQuestors issue, now using a Val(yyyyMMdd) method.
'           Removed the Date Formatting bug, dd/mm/yyyy is now dd/MM/yyyy.
'
'v1.0.0.0   02/05/2014 - Released on Forum and the installer
'           Added the new code to do automatic updates, this part checks for updates and brings the file back to local machine.
'           Removes all the old expiry code and LFA file generation code.
'           Added code to clean up any LFA files that previous versions may have created.
'
'v1.0.0.1   02/05/2014 - Released on Server
'           Addded the Successfully updated to the latest version!" message just to prove the updater works.
'
'v1.0.0.2   03/05/2014 - Released on Server
'           Added the vibration analysis code.
'               - Data is only analysed when we have 500 successive readings (10 seconds) while the UAV is above
'                 3 meters to eliminate ground wash and <1 meter per second ground speed (may need to be extended for planes) 
'               - X and Y need to be within the -3 to +3 with Z within -15 to -5 for the summary to report
'                 "Excellent: Levels of vibration detected are within recommended limits."
'               - Levels outside these limit will issue a warning 
'                 "WARNING: Level of vibrations are above recommended values."
'               - Where X and Y are over -5 to +5 with Z within -20 to 0 a different warning is displayed.
'                 "WARNING: HIGH Levels of Vibration Detected, recommended not to fly!"
'v1.0.0.3   03/05/2014 - Released on Server
'           BUG FIX: Coding erorr on the Y and Z vibration results
'           Added the Standard Deviation from the Average result to the Vibration Results.
'
'v1.0.0.4   03/05/2014 - Released on Server
'           Added the filtering on the Vibration Checks
'           Added the DataLine reference that were used for the Vibration Check.
'
'v1.0.0.5   03/05/2014 - Released on Server
'           BUG FIX: fix a bug that caused an update each time the user selected to search for updates.
'           Commented out some code that allows users to see the FTP server name.
'
'v1.0.0.6   03/05/2014 - Released on Server
'           BUG FIX: fix a bug that caused the flight times to be incorrect where a user loading or reads a second log.
'
'v1.0.0.7   04/05/2014 - Released on Server
'           Added some checks to ensure the log file being read is compatible with the program.
'               - Check that GPS and CTUN are being log, where not display message and exit log reading.
'               - Check the log is >= version 3.1, where not display message and exit log reading.
'               - Check the log was created by an ArduCopter, where not display message and exit log reading.
'           Changed text box type to a rich text box with Hyperlinks.
'
'v1.0.0.8   04/05/2014 - Released on Server 
'           Added a progress bar for the file read status.
'           Started adding some code for Planes.
'               - Dection of the "ArduPlane" log type.
'               - Mode Change, and summary section.
'           Added code to calculate altitude from GPS data if BarAlt is not available.
'
'v1.0.0.9   05/05/2014 - Released on Server
'           Added warnings for HDop < 2 and Satellites < 9 for both mode and overall summaries.
'           Added Brown Out Detection code.
'           Started looking at the ATT and NTUN Pitch and Roll data, debug info is in but it is complicated.
'           Added some colours for the formatting of diplayed data.
'
'v1.0.0.10  08/05/2014 - Released on Server
'           Changed the Eff description to (mA/mi) from (mA/mi)
'           Changed the reporting of Vibration in Overall Summary to look at IMU_Vibration_Check = true instead on DLs>=500 to allow changing.
'           Due to a report that French Canadian did not work I added some Culture Detection Code.
'           BUG: Update code does not work as expected.
'
'v1.0.1.0   08/05/2014 - Released on Server
'           Indentical to above.
'
'v1.0.1.1   08/05/2014 - Released on Server
'           Added the code to force the Thread to us en-GB Culture and UICulture to keep the programming simple.
'
'v1.0.1.2   10/05/2014 - Released on Server
'           Adding some resilience to both the 1st & 2nd passes of the log file to ensure values are within expectations.
'           Added the Log File Corruption Handled label along with basic file corruption code 
'           Added the abiltiy to automatically display a Vibration Chart where vibrations are higher than allowed.
'           Bug FIx: that caused GPS Distances and Times to be incorrect when AUTO mode takes off very quickly.
'           Tested the AutoTune mode reporting.
'
'v1.0.1.3   13/05/2014 - Released on Server
'           Bug Fix: Removed filtering from the Vibration Alt Code that sometimes failed to get min altitude down in time.
'           Bug Fix: Eff error after landing and taking off again.
'           WARNING has been added when > 80% of the capacity has been used.
'           Bug Fix: when trying to handle an APM detected GPS Glitch
'           Changed code to ignore GPS data while there are currently GPS issues!
'           Added the ability to change the vibration detection parameter on the main screen, also a Force Chart button has been added.
'           WARNING has been added to inform the user the Current Sensor may be faulty.
'   
'
'v1.0.1.4   16/05/2014 - Released on Server
'           WARNING has been added if the Log File Version is a RC (Release Candidate) rather than an official release.
'           Basic support added for V3.2 (RC) files, there may be some issues though with data layouts.
'
'v1.0.1.5   18/05/2014 - Released on Server
'           Added the PM Checking Code.
'           Fixed Battery Capacity Bug.
'
'v1.0.1.6   08/06/2014 - Released on Server
'           Updated the spelling of Determine, or at least I think I have as I can not find any references to the miss spelling.
'           INAVerr code has been removed as it does not seem useful in real world testing.
'           Added a check to ensure the current Log_In_Flight status makes sense, 
'               if the vehicle altitude is higher than the ground and throttle is out then the take_off code is executed.
'               this solves the issues with 2014-06-01 07-13-53.log where the take off event was missing. 
'           Added a log file error counter near the progress bar.
'           Suppress the "Not_Landed" event from the display where it follows the "Take_off" event.
'           Add some testing data to the remaining "Not_Landed" event to see if this is useful data or should be removed.
'           Added code the suppress GCS issues until we have had at least one 100% signal, also informs user when GCS is detected for the first time.
'           Added charts for Altitude and Speed to the vibration charts and also increased the data collection by 900%
'           Removed the ability to alter the Vibration Detection Parameters, these are now set at speed<40m/s and alt>0m in flight.
'           Vibration Averages have been removed while I get time to work on them.
'           Added compatibility for V3.1.5 logs
'           Updated some screen formatting issues 
'
'v1.0.1.7   14/06/2014 - Released on Server
'           Changed the way the "LOG Analyser" Take_Off code detects a take off. it now needs to see the throttle at 40% not just up, and Alt @ +0.5m above ground.
'           Added the RCG Code Copy function to the edit menu.
'
'v1.0.1.8   14/06/2014 - Released on Server
'           Added the DU32 code.
'           Killed the NOT_LANDED display
'           Added the Esc Key to abort the Analysis
'           Added the auto scroll to the analysis window.
'
'v1.0.1.9   28/06/2014 - Released on Server
'           Added some basic CMD checks for the auto flights.
'           Removed the Check of Battery Capacity against the Model Capacity - put back when we add models
'           Added GPS co-ords when Mode Changes
'           Added GPS co-ords when Home is Set (note this is based on last GPS co-ords, actual set home in APM could be different)
'               If NTUN is available then we try to validate the Home Posistion against the Current Position.
'           Frame Type Added, determined from PARM FRAME
'           Number of Motors determined from FMT MOT line.
'
'v1.0.2.0   28/06/2014 - Released on Server
'           Battery Capacity Bug Fix, because I had removed the check against the model.
'
'v1.0.2.1
'           Added detection when an Auto Mission is struggling to maintain the desired altitude.
'           Added a safety warning when a mission does not start with a take off.
'           Altered the AUTO mission formatting on screen to make it more readable and understandable.
'           Deactivated the INAV code until I work some more on it.
'           Changed TABS to 2 spaces to help code flow.
'v1.0.2.2
'Craig's Additions
'           Added Form position memory. Will remember where form was last closed no matter which window and what location.
'           Redisgned the interface to better match Mission Planner
'           Created ToolTips for each option
'           Finalized initial GUI layout. Still need to figure out a few things as they no longer display after being moved.
'v1.0.2.3
'Craig's Additions
'           Changed copy to clip so it can be called via anything. Since I am hiding the menu strip I need to be able to  
'           call it's info via buttons
'           Moved Help/About into its own sub for the same reason and added a button to call it
'           Moved Update Now into its own sub for the same reason and added a button to call it
'           All items present in the now hidden toolstrip are available via large buttons at top of form

'v1.0.2.4
'Craig's Additions
'           Added a little joke :) MCB
'v1.0.2.5
'Craig's Additions
'           Added a donate button 
'           Redid the cowbell wav to add actual cowbells :)
'           changed the black text output to white to deal with the new background color.
'           Added more ToolTips for all buttons and checkboxes
'
'v1.0.2.6
'Craig's Additions
'           Made the wav 1/3 the size
'           verified Kev's change to the donate button works.
'           made sure the github stuff works :)
'v1.0.2.7
'Craig's Additions
'           ????
'v1.0.2.8
'Craig's Additions
'           Added an icon. It was REALLY needed!
'
'v1.0.2.9
'           KMG - After the black screen / white text the RC Groups copy needed updating to inverse white text.
'           KMG - Fixed bug where copy would miss the final two characters when using the copy for RC Groups.
'           KMG - Changed the normal screen text colours to suit the black screen.
'           KMG - Changed my Indent control to Standard 4 Spaces and corrected the code :) .
'v1.0.3.0
'           CAW - Added code to hide function buttons of no log is loaded
'v1.0.3.1
'           CAW - Added new image to better let people understand what to click to open a log file
'           ...
'v1.0.3.2   KMG - Removed orphaned code.
'           GitHub Branch Test.
'           KMG - Removed more orphaned code and controls.
'           KMG - Submitted Pull - Code Cleansing
'           KMG - Added the "Press Esc to Abort" label
'           KMG - Created the Pull Request.



' ##TODO##
'Because there is no EV logs in my Plane sample then the Take Off is not recorded, because of this there are no mode times.
'Finish the Pitch and Roll stuff for crash detection.
'Look at Mission Planner Code to see how they download logs from the APM.
'Need to check FMT for any "New" entries that I do not know about and report back when found.
'Need more v3.2 files from reliable source before continuing.
'Write detected errors to FTP, upload log if user agrees.

'Can I use The Questors Failing GPS logs to give users better information that the GPS unit is faulty?
'	TQ just changed the unit out.

'Resize the text window when the Window changes size, or stop the window changing size?

'Calculate Spd Eff (mi/A) and create Spd % over flight within Ranges, then calculate the eff within each range.


'Add support for Planes - I am still investigating whether this is possible
'   - need to find a take off and land solution if EV in not available for planes

'On TX/RX issue we have direction of UAV but need to reference where the Pilot is.
'   - I have a quad that can fly away from me to 1/2 mile, however turn to face me at that distance a RTL kicks in
'   - Knowing these angles could help placement of RX aerials to reduce such issues

'Detect if Battery Voltage / Capacity calibration has been performed, need to know default parameter for BATT_AMP_PERVOLT & BATT_AMP_OFFSET








