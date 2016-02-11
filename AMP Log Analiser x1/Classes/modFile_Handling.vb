Imports System.IO
Imports System.Threading.Thread

Module modFile_Handling

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
        End If
    End Function

    Public Function FileDataSuitable() As Boolean
        Dim strTemp As String = ""
        FileDataSuitable = False
        ' Function returns True if we need to exit the file processing.

        ' v3.2 Issue - Display warning if FMTs have been found at the end
        If WarnAboutExtraFMT = True Then
            WriteTextLog("WARNING: Curruption Of FMT Lines Detected!")
            WriteTextLog("WARNING: the Analyzer tried to handle them.")
            WriteTextLog("WARNING: This is a v3.2 firmware issue that has been reported!")
            WriteTextLog("")
        End If

        'Display the Release Candidate Warning
        If InStr(StrConv(ArduVersion, vbUpperCase), "RC") Then
            WriteTextLog("WARNING: Installed APM firmware is a Release Candidate version, only advanced pilots should be using this!")
            WriteTextLog("WARNING: Beginners are recommended to install the latest stable officially released version.")
            strTemp = "Installed APM firmware is a Release Candidate version!" & vbNewLine
            MsgBox(strTemp, vbOKOnly, "Error")
        End If

        'Display the Release Candidate Warning
        If InStr(StrConv(ArduVersion, vbUpperCase), "DEV") Then
            WriteTextLog("WARNING: Installed APM firmware is a DEVELOPER version, only DEVELOPERS should be using this!")
            WriteTextLog("WARNING: Beginners and Advanced pilots are recommended to install the latest stable officially released version.")
            strTemp = "Installed APM firmware is a DEVELOPER version!" & vbNewLine
            MsgBox(strTemp, vbOKOnly, "Error")
        End If

        'Display and ArduPlane Warning
        If ArduType = "ArduPlane" Then MsgBox("ArduPlane analysis is still under testing!", vbOKOnly, "Message")

        'Check the log file has been made by an arducopter, if not ArduCoperVersion will still be "".
        If ArduVersion = "" Then
            WriteTextLog("Log file not created by a recognised Ardu Vehicle firmware!")
            strTemp = "Log must be created by an Ardu Vehicle firmware!" & vbNewLine
            MsgBox(strTemp, vbOKOnly, "Error")
            FileDataSuitable = True
        End If

        'Special Code for SOLO solo-1.2.0 to Force V3.2 code
        If InStr(StrConv(ArduVersion, vbUpperCase), "SOLO-1.2.0") Then
            WriteTextLog("Log file created by a Solo Copter (" & ArduVersion & ") - Forcing to v3.2 scanning")
            ArduVersion = 3.2 : SoloFirmwareDetected_v3_2 = True ' Drives different IMU Code.
            strTemp = "         Solo Copter Firmware Detected," & vbNewLine
            strTemp = strTemp & "Compatibility may be limited at this time." & vbNewLine
            MsgBox(strTemp, vbOKOnly, "Warning")
        End If


        'Check the program is compatible with this log file version.
        If Ignore_LOG_Version = False Then
            If ArduType = "ArduCopter" Or ArduType = "APM:Copter" Then
                If VersionCompare("3.0", ArduVersion) = False Then 'Inverse VersionCompare to produce the correct result.
                    WriteTextLog("Log file created by an old APM:Copter firmware version!")
                    strTemp = "            Log must be created by APM firmware v3.1 or above." & vbNewLine
                    strTemp = strTemp & "Try updating by selecting HELP - UPDATE NOW from the menus." & vbNewLine
                    MsgBox(strTemp, vbOKOnly, "Error")
                    FileDataSuitable = True
                End If
            End If

            'Display v3.2 warning, not fully complatible yet.
            If ArduType = "ArduCopter" Or ArduType = "APM:Copter" Then
                'If VersionCompare(ArduVersion, "3.1.5") = False Then 'ArduVersion > "V3.1.5" Then
                If VersionCompare(ArduVersion, "3.4") = False Then 'ArduVersion > "V3.3" Then
                    WriteTextLog("Log file created by a new APM:Copter firmware version!")
                    strTemp = "This Log file was created by a new version, it may not be fully supported yet." & vbNewLine
                    strTemp = strTemp & "            Try updating by selecting HELP - UPDATE NOW from the menus." & vbNewLine
                    strTemp = strTemp & "                     Attempt to run anyway?." & vbNewLine
                    If MsgBox(strTemp, vbYesNo, "Error") = vbNo Then
                        FileDataSuitable = True
                    End If
                End If
            End If
            If ArduType = "ArduPlane" Then
                WriteTextLog("Log file created by an old ArduPlane firmware version!")
                strTemp = "            Log must be created by APM firmware v3.0 or above." & vbNewLine
                strTemp = strTemp & "Try updating by selecting HELP - UPDATE NOW from the menus." & vbNewLine
                MsgBox(strTemp, vbOKOnly, "Error")
                FileDataSuitable = True
            End If
        Else
            MsgBox("Ignore_LOG_Version is Active", vbOKOnly, "DEVELOPER WARNING")
        End If


        'Check to ensure the files contains the minimum data types required.

        If (ArduType = "ArduCopter" Or ArduType = "APM:Copter") And (GPS_Logging = False Or EV_Logging = False) Then
            WriteTextLog("Log file does not contain the correct data!")
            strTemp = "          Log must contain GPS and EV data as a minimum" & vbNewLine
            strTemp = strTemp & "                                 for this program to be useful." & vbNewLine
            strTemp = strTemp & "Try updating by selecting HELP - UPDATE NOW from the menus." & vbNewLine
            strTemp = strTemp & "                     Do you want to run with limited results?" & vbNewLine
            If MsgBox(strTemp, vbYesNo, "Error") = vbNo Then FileDataSuitable = True
        End If


        If Read_LOG_Percentage <> 100 Then
            MsgBox("Read_LOG_Percentage is Active @ " & Read_LOG_Percentage & "%", vbOKOnly, "DEVELOPER WARNING")
        End If

    End Function

    Public Sub FindLoggingDataAndParams()

        ' Run the new code to populate the ardu variables like type, version, pix serial number etc.
        Call FindFirmwareDetails()

        ' This code reads all the log to the end to find all the details, it is a first pass solution
        ' to set up all the variables that the main pass needs to know before hand.
        Debug.Print("Finding Parameter and Logging Details....")
        Dim MotorsDetectedForV3_2 As Boolean = False 'for v3.2 we need to look at the actual dataline not just the FMT line.
        Dim ReadFileVersion As Double = 0
        Dim EndOfPARAM_Counter As Integer = 99        ' counts down for each data line after PARAMs have stopped.

        ' For Speed we need to set a local variable to quicky handle
        ' the version of file being read. Using the check on each line
        ' had a significant overhead.
        If ReadFileVersion = 0 And ArduVersion <> "" Then
            If VersionCompare(ArduVersion, "3.1.999") = True Then
                ReadFileVersion = 3.1
            ElseIf VersionCompare(ArduVersion, "3.2.999") = True Then
                ReadFileVersion = 3.2
            ElseIf VersionCompare(ArduVersion, "3.3.999") = True Then
                ReadFileVersion = 3.3
            Else
                ReadFileVersion = 3.4
            End If
        End If

        APM_No_Motors = 0
        'Do some warnings about DEVELOPER IGNORES
        Debug.Print("1st Pass Started, Finding Logging Data...")
        If Ignore_CTUN_Logging = True Then
            Beep() : Threading.Thread.Sleep(500) : Beep() : Threading.Thread.Sleep(500) : Beep() : Threading.Thread.Sleep(500)
            MsgBox("CTUN will be ignored", vbOKOnly, "DEVELOPER SETTINGS")
        End If

        'Read the File line by line
        Dim objReader As New System.IO.StreamReader(strLogPathFileName)
        Do While objReader.Peek() <> -1
            Array.Clear(DataArray, 0, 25)
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


            If DataArray(0) = "FMT" Then
                FoundFMT = True
                If EndOfFMT = True Then

                    'MsgBox("Curruption Of FMT Lines Detected, the Analyzer is trying to handle them (V3.2 issue)!", vbOKOnly & vbExclamation, "Warning!")
                    WarnAboutExtraFMT = True
                    EndOfFMT = False
                End If
            End If
            If DataArray(0) = "FMT" And DataArray(3) = "MOT" Then
                If Val(Mid(DataArray(DataArrayCounter), 4, Len(DataArray(DataArrayCounter)) - 1)) > 1 And Val(Mid(DataArray(DataArrayCounter), 4, Len(DataArray(DataArrayCounter)) - 1)) < 12 Then
                    APM_No_Motors = Mid(DataArray(DataArrayCounter), 4, Len(DataArray(DataArrayCounter)) - 1)
                Else
                    APM_No_Motors = 0
                End If
            End If

            ' New Detect Motors Code Dec 2015 - KXG
            If DataArray(0) = "RCOU" Then
                DetectMotors()
            End If

            ' New Detect CPU Frequency, remember at this stage we already know the arduversion
            If DataArray(0) = "PM" Then
                'Solo v1.2.0 - FMT, 6, 16, PM, HHIhBH, NLon,NLoop,MaxT,PMT,I2CErr,INSErr
                'v3.1        - FMT, 6, 19, PM, BBHHIhBHB, RenCnt, RenBlw, NLon, NLoop, MaxT, Pmt, I2CErr, INSErr, INAVErr
                'v3.2        - FMT, 6, 17, PM, HHIhBHB, NLon, NLoop, MaxT, Pmt, I2CErr, INSErr, INAVErr
                'v3.3.2      - FMT, 6, 24, PM, QHHIhBH, TimeUS,NLon,NLoop,MaxT,PMT,I2CErr,INSErr
                Dim StoreCurrentFrequency As Integer = 0
                Dim NumberOfSuccesiveHits As Integer = 3

                If ReadFileVersion = 3.1 Then
                    If InStr(ArduVersion, "solo") > 0 Then
                        StoreCurrentFrequency = DataArray(2)
                    Else
                        StoreCurrentFrequency = DataArray(4)
                    End If
                ElseIf ReadFileVersion = 3.2 Then
                    StoreCurrentFrequency = DataArray(2)
                Else
                    StoreCurrentFrequency = DataArray(3)
                End If

                If APM_Frequency_Last = StoreCurrentFrequency Then
                    If APM_Frequency_Counter = NumberOfSuccesiveHits Then
                        APM_Frequency_Counter = 0
                        APM_Frequency = APM_Frequency + StoreCurrentFrequency * NumberOfSuccesiveHits
                        Log_PM_Counter += NumberOfSuccesiveHits
                        'Debug.Print("Found " & NumberOfSuccesiveHits & " in a row, recording " & APM_Frequency / Log_PM_Counter / 10 & "Hz")
                    Else
                        APM_Frequency_Counter += 1
                    End If
                Else
                    APM_Frequency_Last = StoreCurrentFrequency
                    APM_Frequency_Counter = 0
                    'Debug.Print("Ignored...")
                End If
            End If

            ' Logging Data Support for all Firmwares
            If DataArray(0) = "PARM" Then FoundPARAM = True
            If DataArray(0) = "PARM" And DataArray(1) = "FRAME" Then APM_Frame_Type = DataArray(2)
            If DataArray(0) = "PARM" And DataArray(1) = "INS_PRODUCT_ID" Then Hardware = DataArray(2)
            If DataArray(0) = "PARM" And DataArray(2) = "FRAME" Then APM_Frame_Type = DataArray(3)
            If DataArray(0) = "PARM" And DataArray(2) = "INS_PRODUCT_ID" Then Hardware = DataArray(3)
            If DataArray(0) = "IMU" Then IMU_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "GPS" Then GPS_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "CTUN" And Ignore_CTUN_Logging = False Then CTUN_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "PM" Then PM_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "CURR" Then CURR_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "NTUN" Then NTUN_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "MSG" Then MSG_Logging = True : EndOfFMT = True
            If DataArray(0) = "ATUN" Then ATUN_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "ATDE" Then ATDE_logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "MOT" Then MOT_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "OF" Then OF_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "MAG" Then MAG_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "CMD" Then CMD_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "ATT" Then ATT_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "INAV" Then INAV_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "MODE" Then MODE_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "STRT" Then STRT_logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "EV" Then EV_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "D16" Then D16_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "DU16" Then DU16_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "D32" Then D32_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "DU32" Then DU32_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "DFLT" Then DFLT_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "PID" Then PID_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "CAM" Then CAM_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "ERR" Then ERR_Logging = True : EndOfFMT = True : EndOfPARAM = True

            'Logging Data Support for v3.2
            If DataArray(0) = "GPS2" Then GPS2_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "IMU2" Then IMU2_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "IMU3" Then IMU3_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "MAG2" Then MAG2_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "MAG3" Then MAG3_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "AHR2" Then AHR2_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "EKF1" Then EKF1_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "EKF2" Then EKF2_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "EKF3" Then EKF3_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "EKF4" Then EKF4_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "TERR" Then TERR_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "UBX1" Then UBX1_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "UBX2" Then UBX2_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "RCIN" Then RCIN_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "RCOU" Then RCOU_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "BARO" Then BARO_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "POWR" Then POWR_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "RAD" Then RAD_Logging = True : EndOfFMT = True : EndOfPARAM = True
            If DataArray(0) = "SIM" Then SIM_Logging = True : EndOfFMT = True : EndOfPARAM = True

            ' Start the counter, if x datalines are received before getting another parameter then
            ' parameters will be considers as finished.
            If EndOfPARAM = True And EndOfPARAM_Counter > 0 Then EndOfPARAM_Counter -= 1

            ' Parameter Support for all Firmwares
            ' A v3.1 or v3.2 Parameter value should have only 3 pieces of data!
            ' v3.2 - FMT, 129, 23, PARM, Nf, Name,Value
            ' v3.3 - FMT, 129, 31, PARM, QNf, TimeUS,Name,Value
            If DataArray(0) = "PARM" Then
                EndOfFMT = True
                EndOfPARAM = False
                If ReadFileVersion >= 3.3 And ArduType = "APM:Copter" Then
                    'Alter Data to meet v3.2 requirements by shifting it down one place.
                    DataArray(1) = DataArray(2) : DataArray(2) = DataArray(3) : DataArray(3) = Nothing
                End If
                If IsNumeric(DataArray(2)) = False Or IsNothing(DataArray(3)) = False Then
                    Debug.Print("================================================================")
                    Debug.Print("== File Corruption Detected on Data Line " & DataLine & ", PARM Check ignored! ==")
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

                    ' EndOfPARAM Handles log corruption where PARAMS from previous logs are added at the end v3.2 issue.
                    ' However, in v3.4 it may be possible for the PARAM's to be interupted by a few DataLines
                    ' although this could be a SITL issue, not sure.
                    ' Either way a counter has been introduced, we must have at least x datalines before we accept
                    ' EndOfPARAM is actually True
                    If EndOfPARAM = False And EndOfPARAM_Counter > 0 Then
                        EndOfPARAM_Counter = 10                 'Reset the counter if Parameters are found
                        Dim Param As String = DataArray(1)
                        Dim Value As String = Val(DataArray(2))

                        'Write the parameter found to the Parameter List Box
                        frmParameters.lstboxParameters.Items.Add(Param & "  =  " & Value)

                        'Set the Parameters Values
                        If Param = "ACRO_TRAINER" Then PARM_ACRO_TRAINER = Val(Value)
                        If Param = "THR_MIN" Then PARM_THR_MIN = Val(Value)
                        If Param = "THR_MAX" Then PARM_THR_MAX = Val(Value)
                        If Param = "MOT_SPIN_ARMED" Then PARM_MOT_SPIN_ARMED = Val(Value)
                        If Param = "BATT_CAPACITY" Then PARM_BATTERY_CAPACITY = Val(Value)
                        If Param = "ARMING_CHECK" Then PARM_ARMING_CHECK = Val(Value)
                        If Param = "CH7_OPT" Then PARM_CH7_OPT = Val(Value)
                        If Param = "CH8_OPT" Then PARM_CH8_OPT = Val(Value)
                        If Param = "RTL_ALT" Then PARM_RTL_ALT = Val(Value)
                        If Param = "RTL_ALT_FINAL" Then PARM_RTL_ALT_FINAL = Val(Value)
                        If Param = "RTL_LOIT_TIME" Then PARM_RTL_LOIT_TIME = Val(Value)
                        If Param = "COMPASS_OFS_X" Then PARM_COMPASS_OFS_X = Val(Value)
                        If Param = "COMPASS_OFS_Y" Then PARM_COMPASS_OFS_Y = Val(Value)
                        If Param = "COMPASS_OFS_Z" Then PARM_COMPASS_OFS_Z = Val(Value)
                        If Param = "INS_PRODUCT_ID" Then PARM_INS_PRODUCT_ID = Val(Value)
                        If Param = "AHRS_EKF_USE" Then PARM_AHRS_EKF_USE = Val(Value)
                        If Param = "TUNE" Then PARM_TUNE = Val(Value)
                        If Param = "TUNE_LOW" Then PARM_TUNE_LOW = Val(Value)
                        If Param = "TUNE_HIGH" Then PARM_TUNE_HIGH = Val(Value)
                        If Param = "FS_GPS_ENABLE" Then PARM_FS_GPS_ENABLE = Val(Value)
                        If Param = "WPNAV_SPEED" Then Log_CMD_WP_Speed = Val(Value)
                        If Param = "FS_BATT_ENABLE" Then PARM_FS_BATT_ENABLE = Val(Value)
                        If Param = "FS_BATT_VOLTAGE" Then PARM_FS_BATT_VOLTAGE = Val(Value)
                        If Param = "FS_BATT_MAH" Then PARM_FS_BATT_MAH = Val(Value)
                        If Param = "FS_GCS_ENABLE" Then PARM_FS_GCS_ENABLE = Val(Value)
                        If Param = "FS_THR_ENABLE" Then PARM_FS_THR_ENABLE = Val(Value)
                        If Param = "FS_THR_VALUE" Then PARM_FS_THR_VALUE = Val(Value)
                        If Param = "FENCE_ENABLE" Then PARM_FENCE_ENABLE = Val(Value)
                        If Param = "FENCE_TYPE" Then PARM_FENCE_TYPE = Val(Value)
                        If Param = "FENCE_ACTION" Then PARM_FENCE_ACTION = Val(Value)
                        If Param = "FENCE_ALT_MAX" Then PARM_FENCE_ALT_MAX = Val(Value)
                        If Param = "FENCE_RADIUS" Then PARM_FENCE_RADIUS = Val(Value)
                        If Param = "FENCE_MARGIN" Then PARM_FENCE_MARGIN = Val(Value)
                        If Param = "BATT_CURR_PIN" Then PARM_BATT_CURR_PIN = Val(Value)
                    End If
                End If
            End If

            TotalDataLines = TotalDataLines + 1
        Loop

        Debug.Print("Searched " & TotalDataLines & " datalines to find the parameter and logging details.")

        ' Calculate the final frequency we will display
        If Log_PM_Counter <> 0 Then APM_Frequency = Int(APM_Frequency / Log_PM_Counter) / 10
        Log_PM_Counter = 0 'reset this as it is re-used in the main analysis

        ' Sanity Check the findings
        If FoundFMT <> True Then
            MsgBox("No FMT Lines Detected, analysis will be unreliable!", vbOKOnly & vbExclamation, "Warning!")
        End If
        If FoundPARAM <> True Then
            MsgBox("This log file was partially overwritten by a newer log!  Analysis is unreliable at the best!", vbOKOnly & vbExclamation, "Warning!")
        End If

        objReader.Close()
        Debug.Print("Success!")
    End Sub


    Public Sub FindFirmwareDetails()
        ' It has become apparent that the Firmware details has moved around within the DataLog files several times
        ' over the different versions. V3.3 became ideal as we had FMT lines followed immediately by the MSG lines
        ' describing the firmware version and type.
        ' Playing with v3.4-Dev ot seems that some of the PARM lines are before the MSG lines, thus not allowing 
        ' me to detect the version before reading the PARM lines.
        ' This new routine does a pre-scan of the file to get this information.

        Debug.Print("Finding Firmware Details....")
        Dim ReadFileVersion As Double = 0
        Dim Found As Boolean = False              ' True when all details have been found.
        Dim Counter As Integer = 500
        Dim StartCounter As Boolean = False       ' True if we have detected the APM variables, allows time to detect Pix 

        'Read the File line by line
        Dim objReader As New System.IO.StreamReader(strLogPathFileName)
        Do While objReader.Peek() <> -1 And Found = False And Counter <> 0
            Array.Clear(DataArray, 0, 25)
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

            ' Header Data Support for Firmware v3.1.? 
            If DataArray(0) = "ArduCopter" Or DataArray(0) = "APM:Copter" Then ArduType = "APM:Copter" : ArduVersion = DataArray(1) : ArduBuild = DataArray(2)
            If DataArray(0) = "ArduPlane" Then ArduType = "ArduPlane" : ArduVersion = DataArray(1) : ArduBuild = DataArray(2)
            If DataArray(0) = "Free" And DataArray(1) = "RAM:" Then APM_Free_RAM = DataArray(2)
            If DataArray(0) = "APM" Then APM_Version = DataArray(1)

            ' Header Data Support Firmware v3.2.? 
            If DataArray(0) = "MSG" And DataArray(1) = "ArduCopter" Or DataArray(1) = "APM:Copter" Then ArduType = "APM:Copter" : ArduVersion = DataArray(2) : ArduBuild = DataArray(3)
            If DataArray(0) = "MSG" And DataArray(1) = "ArduPlane" Then ArduType = "ArduPlane" : ArduVersion = DataArray(2) : ArduBuild = DataArray(3)
            If DataArray(0) = "MSG" And DataArray(1) = "PX4:" Then APM_Version = DataArray(1) & " " & DataArray(2) & " " & DataArray(3) & " " & DataArray(4)
            If DataArray(0) = "MSG" And DataArray(1) = "PX4v2" Then Pixhawk_Serial_Number = DataArray(1) & " " & DataArray(2) & " " & DataArray(3) & " " & DataArray(4)

            ' Header Data Support for Firmware v3.3
            If DataArray(2) = "APM:Copter" Then ArduType = "APM:Copter" : ArduVersion = DataArray(3) : ArduBuild = DataArray(4)

            TotalDataLines = TotalDataLines + 1

            ' Check if we have found all the data required so we can exit the loop quicker
            If ArduType <> "" And ArduVersion <> "" And ArduBuild <> "" Then
                StartCounter = True
                Counter -= 1
            End If
            If ArduType <> "" And ArduVersion <> "" And ArduBuild <> "" And APM_Version <> "" And Pixhawk_Serial_Number <> "" Then
                Found = True
            End If
        Loop

        Debug.Print("Searched " & TotalDataLines & " datalines to find the firmware details")
        ' Sanity Check the version
        If ArduVersion = "" Then
            Dim str As String = ""
            str = str & "The APM Firmware Version could not be detected due to file corruption," & vbLf
            str = str & "in an attempt to read the file we shall assume APM:Copter v3.2.1 as" & vbLf
            str = str & "this firmware version was known to suffer from this issue." & vbLf
            MsgBox(str, vbOKOnly & vbExclamation, "Warning!")
            ArduType = "APM:Copter" : ArduVersion = "V3.2.1" : ArduBuild = "Unknown"
        End If

        objReader.Close()
        Debug.Print("Success!")

    End Sub

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

    Public Function FileNameWithoutPath(ByVal FullPath As String) As String
        Return System.IO.Path.GetFileName(FullPath).ToString
    End Function

    Public Sub SelectFile()
        With frmMainForm
            Debug.Print("Open File")
            '.OpenFD.InitialDirectory = "C:\Program Files (x86)\Mission Planner\logs\"
            .OpenFD.Title = "Open a Text File"

            ' ## Changed KXG 26/04/2015 ##
            '.OpenFD.FileName = "*.log" 'Allow any log file to be selected
            .OpenFD.Filter = "APM Logs|*.log;*.bin"
            .OpenFD.FileName = "*.log;*.bin"


            'Display the Open File Dialog Window.
            If .OpenFD.ShowDialog() = DialogResult.Cancel Then
                Dim strLogFileName As String = ""
                Debug.Print("User Selected Cancel " & strLogFileName)
                FileOpened = False
                Exit Sub
            Else
                'User has selected a file
                strLogPathFileName = .OpenFD.FileName
                strLogFileName = FileNameWithoutPath(strLogPathFileName)
                'txtboxFileName.Text = strLogFileName
                Debug.Print("User Selected File " & strLogPathFileName)
                .richtxtLogAnalysis.Clear()
                FileOpened = True
                Call ButtonsCheckBoxes_Visible(True)
                Call ButtonsCharting_Visible(False)
                frmMainForm.btnAnalyze.Visible = True

                ' {{ Added KXG 26/04/2015
                ' If the file choosen is a .bin file then convert to a log file first and save in the source folder.
                If UCase(Mid(strLogPathFileName, Len(strLogPathFileName) - 3, 4)) = ".BIN" Then
                    ' move the .bin strings to the correct variables
                    Dim strBinPathFileName As String = strLogPathFileName
                    Dim strBinFileName As String = strLogFileName
                    ' change the .bin to .log
                    strLogFileName = Mid(strLogFileName, 1, Len(strLogFileName) - 4) & ".log"
                    strLogPathFileName = Mid(strLogPathFileName, 1, Len(strLogPathFileName) - 4) & ".log"
                    Debug.Print("User selected a .bin file... ")
                    Debug.Print("Running .Bin Conversion... " & strBinPathFileName & " --> " & strLogPathFileName)

                    ' Let the user know what's happening as there will be a short delay
                    With frmMainForm.richtxtLogAnalysis
                        .Visible = True
                        .Clear()
                        .AppendText("Running .Bin Conversion... " & strBinPathFileName & " --> " & strLogPathFileName)
                        .Refresh()
                    End With
                    'frmMainForm.Refresh()
                    'Application.DoEvents()

                    Call ConvertBin(strBinPathFileName, strLogPathFileName)
                    Debug.Print("Conversion Completed.")
                    Sleep(2000) ' Ensure the new file .log is dropped before allowing it to be opened.

                    ' Clean up
                    With frmMainForm.richtxtLogAnalysis
                        .Clear()
                        .Refresh()
                    End With

                End If
                ' }} - End
            End If
            Debug.Print("Open File Completed." & vbNewLine)
            If FileOpened = True Then
                .btnAnalyze.Visible = True
                .picClickButton.Visible = False
            ElseIf FileOpened = False Then
                .btnAnalyze.Visible = False
                .picClickButton.Visible = True
            End If
        End With
    End Sub


    Public Function VersionCompare(LowestVersion As String, HighestVersion As String) As Boolean
        'This function will return true if "LowestVersion" is lower than "HighestVersion".
        'It should replace any IF statements that uses ArduVersion variable!!

        'It can handle upto 4 parts in a version number, i.e. 1.2.3.4, or 1.0.1.9999

        CodeTimerStart = Format(Now, "ffff")

        VersionCompare = False
        Dim strTemp As String = ""
        Dim DetectRC As Boolean = False ' If we find RC then stop adding the numeric values

        Debug.Print("VersionCompare Called: " & LowestVersion & " <= " & HighestVersion & " ? ")

        'stripe out the non-numeric data from the 
        Debug.Print("Striping Out Non-Numeric Characters....")
        strTemp = "" : DetectRC = False
        For N = 1 To Len(LowestVersion)
            If UCase(Mid(LowestVersion, N, 1)) <> "R" And UCase(Mid(LowestVersion, N, 1)) <> "C" And DetectRC = False Then
                If Mid(LowestVersion, N, 1) >= "." And Mid(LowestVersion, N, 1) <= "9" Then
                    strTemp = strTemp & Mid(LowestVersion, N, 1)
                End If
            Else
                DetectRC = True
            End If
        Next
        LowestVersion = strTemp
        strTemp = "" : DetectRC = False
        For N = 1 To Len(HighestVersion)
            If UCase(Mid(HighestVersion, N, 1)) <> "R" And UCase(Mid(HighestVersion, N, 1)) <> "C" And DetectRC = False Then
                If Mid(HighestVersion, N, 1) >= "." And Mid(HighestVersion, N, 1) <= "9" Then
                    strTemp = strTemp & Mid(HighestVersion, N, 1)
                End If
            Else
                DetectRC = True
            End If
        Next
        HighestVersion = strTemp

        ' make into 4 digits, else the .net compare will fail.
        If Len(LowestVersion) = 1 Then LowestVersion = LowestVersion & ".0.0.0"
        If Len(LowestVersion) = 3 Then LowestVersion = LowestVersion & ".0.0"
        If Len(LowestVersion) = 5 Then LowestVersion = LowestVersion & ".0"
        If Len(HighestVersion) = 1 Then HighestVersion = HighestVersion & ".0.0.0"
        If Len(HighestVersion) = 3 Then HighestVersion = HighestVersion & ".0.0"
        If Len(HighestVersion) = 5 Then HighestVersion = HighestVersion & ".0"

        Debug.Print("Stripping Complete: " & LowestVersion & " < " & HighestVersion & " ? ")

        Dim oldVersion As New Version(LowestVersion)
        Dim newVersion As New Version(HighestVersion)
        If Version.op_GreaterThan(newVersion, oldVersion) Then
            VersionCompare = True
        End If

        Debug.Print("Result: " & LowestVersion & " <= " & HighestVersion & " = " & VersionCompare)

        If Format(Now, "ffff") - CodeTimerStart > 1 Then Debug.Print("Version Check = " & Format(Now, "ffff") - CodeTimerStart & "μs")

    End Function

End Module
