Imports System.IO

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

        'Display the Release Candidate Warning
        If InStr(StrConv(ArduVersion, vbUpperCase), "RC") Then
            WriteTextLog("WARNING: Installed APM firmware is a Release Candidate version, only advanced pilots should be using this!")
            WriteTextLog("WARNING: Beginners are recommended to install the latest stable officially released version.")
            strTemp = "Installed APM firmware is a Release Candidate version!" & vbNewLine
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

        'Check the program is compatible with this log file version.
        If Ignore_LOG_Version = False Then
            If ArduType = "ArduCopter" And VersionCompare("3.1", ArduVersion) = False Then 'Inverse VersionCompare to produce the correct result.
                WriteTextLog("Log file created by an old ArduCopter firmware version!")
                strTemp = "            Log must be created by APM firmware v3.1 or above." & vbNewLine
                strTemp = strTemp & "Try updating by selecting HELP - UPDATE NOW from the menus." & vbNewLine
                MsgBox(strTemp, vbOKOnly, "Error")
                FileDataSuitable = True
            End If

            'Display v3.2 warning, not fully complatible yet.
            If ArduType = "ArduCopter" And VersionCompare(ArduVersion, "3.1.5") = False Then 'ArduVersion > "V3.1.5" Then
                WriteTextLog("Log file created by a new ArduCopter firmware version!")
                strTemp = "This Log file was created by a new version, it may not be fully supported yet." & vbNewLine
                strTemp = strTemp & "            Try updating by selecting HELP - UPDATE NOW from the menus." & vbNewLine
                strTemp = strTemp & "                     Attempt to run anyway?." & vbNewLine
                If MsgBox(strTemp, vbYesNo, "Error") = vbNo Then
                    FileDataSuitable = True
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
        If Ignore_LOG_Requirements = False Then
            If ArduType = "ArduCopter" And (GPS_Logging = False Or EV_Logging = False) Then
                WriteTextLog("Log file does not contain the correct data!")
                strTemp = "          Log must contain GPS and EV data as a minimum" & vbNewLine
                strTemp = strTemp & "                                 for this program to be useful." & vbNewLine
                strTemp = strTemp & "Try updating by selecting HELP - UPDATE NOW from the menus." & vbNewLine
                MsgBox(strTemp, vbOKOnly, "Error")
                FileDataSuitable = True
            End If
        Else
            MsgBox("Ignore_LOG_Requirements is Active", vbOKOnly, "DEVELOPER WARNING")
        End If

        If Read_LOG_Percentage <> 100 Then
            MsgBox("Read_LOG_Percentage is Active @ " & Read_LOG_Percentage & "%", vbOKOnly, "DEVELOPER WARNING")
        End If

    End Function

    Public Sub FindLoggingData()
        Dim MotorsDetectedForV3_2 As Boolean = False 'for v3.2 we need to look at the actual datline not just the FMT line.
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

            ' Support for Firmware v3.1.? 
            If DataArray(0) = "ArduCopter" Then ArduType = "ArduCopter" : ArduVersion = DataArray(1) : ArduBuild = DataArray(2)
            If DataArray(0) = "ArduPlane" Then ArduType = "ArduPlane" : ArduVersion = DataArray(1) : ArduBuild = DataArray(2)
            If DataArray(0) = "Free" And DataArray(1) = "RAM:" Then APM_Free_RAM = DataArray(2)
            If DataArray(0) = "APM" Then APM_Version = DataArray(1)
            If DataArray(0) = "FMT" And DataArray(3) = "MOT" Then
                APM_No_Motors = Mid(DataArray(DataArrayCounter), 4, Len(DataArray(DataArrayCounter)) - 1)
            End If
            ' Support Firmware v3.2.? 
            If DataArray(0) = "MSG" And DataArray(1) = "ArduCopter" Then ArduType = "ArduCopter" : ArduVersion = DataArray(2) : ArduBuild = DataArray(3)
            If DataArray(0) = "MSG" And DataArray(1) = "PX4:" Then APM_Version = DataArray(1) & " " & DataArray(2) & " " & DataArray(3) & " " & DataArray(4)
            If DataArray(0) = "RCOU" And MotorsDetectedForV3_2 = False Then
                For N = 2 To 9
                    If DataArray(N) > 0 Then APM_No_Motors = APM_No_Motors + 1
                Next
                MotorsDetectedForV3_2 = True
            End If

            ' Support for all Firmware
            If DataArray(0) = "PARM" And DataArray(1) = "FRAME" Then APM_Frame_Type = DataArray(2)
            If DataArray(0) = "PARM" And DataArray(1) = "INS_PRODUCT_ID" Then Hardware = DataArray(2)
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

            'Support for v3.2
            If DataArray(0) = "GPS2" Then GPS2_Logging = True
            If DataArray(0) = "IMU2" Then IMU2_Logging = True
            If DataArray(0) = "IMU3" Then IMU3_Logging = True
            If DataArray(0) = "MAG2" Then MAG2_Logging = True
            If DataArray(0) = "MAG3" Then MAG3_Logging = True
            If DataArray(0) = "AHR2" Then AHR2_Logging = True
            If DataArray(0) = "EKF1" Then EKF1_Logging = True
            If DataArray(0) = "EKF2" Then EKF2_Logging = True
            If DataArray(0) = "EKF3" Then EKF3_Logging = True
            If DataArray(0) = "EKF4" Then EKF4_Logging = True
            If DataArray(0) = "TERR" Then TERR_Logging = True
            If DataArray(0) = "UBX1" Then UBX1_Logging = True
            If DataArray(0) = "UBX2" Then UBX2_Logging = True
            If DataArray(0) = "RCIN" Then RCIN_Logging = True
            If DataArray(0) = "RCOU" Then RCOU_Logging = True
            If DataArray(0) = "BARO" Then BARO_Logging = True
            If DataArray(0) = "POWR" Then POWR_Logging = True
            If DataArray(0) = "RAD" Then RAD_Logging = True
            If DataArray(0) = "SIM" Then SIM_Logging = True

            TotalDataLines = TotalDataLines + 1
        Loop
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
            .OpenFD.InitialDirectory = "C:\Program Files (x86)\Mission Planner\logs\"
            .OpenFD.Title = "Open a Text File"
            'OpenFD.FileName = "????-??-?? ??-??*.log"
            .OpenFD.FileName = "*.log" 'Allow any log file to be selected

            'Display the Open File Dialog Window.
            If .OpenFD.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Dim strLogFileName As String
                strLogFileName = ""
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

        ' WARNING: This routine takes around 40μs to call, use a local variable rather than multiple calls!

        CodeTimerStart = Format(Now, "ffff")

        VersionCompare = True
        Dim strTemp As String = ""
        Dim Counter As Integer = 0
        Dim V1(4) As Single
        Dim V2(4) As Single


        Debug.Print("VersionCompare Called: " & LowestVersion & " <= " & HighestVersion & " ? ")

        'stripe out the non-numeric data from the 
        Debug.Print("Striping Out Non-Numeric Characters....")
        strTemp = ""
        For N = 1 To Len(LowestVersion)
            If Mid(LowestVersion, N, 1) >= "." And Mid(LowestVersion, N, 1) <= "9" Then
                strTemp = strTemp & Mid(LowestVersion, N, 1)
            End If
        Next
        LowestVersion = strTemp
        strTemp = ""
        For N = 1 To Len(HighestVersion)
            If Mid(HighestVersion, N, 1) >= "." And Mid(HighestVersion, N, 1) <= "9" Then
                strTemp = strTemp & Mid(HighestVersion, N, 1)
            End If
        Next
        HighestVersion = strTemp
        Debug.Print("Stripping Complete: " & LowestVersion & " < " & HighestVersion & " ? ")

        'We cannot compare strings that are formatted as "1.2.3.99" against "1.2.4.0" as the first would be higher.
        'So what we need to do is split the numbers into parts, a little like an IP address.
        Debug.Print("Separating Version Parts...")
        strTemp = LowestVersion : Counter = 0
        While InStr(StrConv(strTemp, vbUpperCase), ".") And Counter <> 4
            V1(Counter) = Left(strTemp, InStr(StrConv(strTemp, vbUpperCase), ".") - 1)
            strTemp = Right(strTemp, Len(strTemp) - InStr(StrConv(strTemp, vbUpperCase), "."))
            Counter = Counter + 1
        End While
        V1(Counter) = strTemp
        strTemp = HighestVersion : Counter = 0
        While InStr(StrConv(strTemp, vbUpperCase), ".") And Counter <> 4
            V2(Counter) = Left(strTemp, InStr(StrConv(strTemp, vbUpperCase), ".") - 1)
            strTemp = Right(strTemp, Len(strTemp) - InStr(StrConv(strTemp, vbUpperCase), "."))
            Counter = Counter + 1
        End While
        V2(Counter) = strTemp
        Debug.Print("Separation Complete.")

        'Debug.Print("Contents of Arrays...")
        'For N = 0 To 3
        '    Debug.Print("Lowest Version Part " & N & " = " & V1(N))
        'Next
        'For N = 0 To 3
        '    Debug.Print("Highest Version Part " & N & " = " & V2(N))
        'Next

        Debug.Print("Comparing Versions...")
        VersionCompare = True
        If V1(0) > V2(0) Then VersionCompare = False
        If V1(1) > V2(1) Then VersionCompare = False
        If V1(2) > V2(2) Then VersionCompare = False
        If V1(3) > V2(3) Then VersionCompare = False
        Debug.Print("Result: " & LowestVersion & " <= " & HighestVersion & " = " & VersionCompare)
        'It should replace any IF statements that uses ArduVersion variable!!

        'Clear up
        Array.Clear(V1, 0, V1.Length)
        Array.Clear(V2, 0, V2.Length)

        If Format(Now, "ffff") - CodeTimerStart > 1 Then Debug.Print("Version Check = " & Format(Now, "ffff") - CodeTimerStart & "μs")

    End Function
End Module
