
Imports Microsoft.WindowsAPICodePack
Imports Microsoft.WindowsAPICodePack.Taskbar

Module modMainReadFile

    Public Sub ReadFile(ByVal strLogFileName As String)

        With frmMainForm

            CodeTimerStart2 = Format(Now, "ss")

            If osVer.Major >= 6 And osVer.Minor >= 1 Then 'Only allowed in Windows 7 or above
                '.WindowState = FormWindowState.Minimized
            End If

            'Initialise the Variables for Reading the Variables
            Dim objReader As New System.IO.StreamReader(strLogFileName)

            DataLine = 0
            Data = ""
            TotalDataLines = 0
            LogAnalysisHeader = False                        'True once the header has been written to the screen.
            ErrorCount = 0                                  'Counts the number of errors found in the logs.

            'Initialise the Variables
            Call Variable_Initialisation()

            'Read the File line by line (1st Pass)
            'This pass checks what data is available in the log.
            .lblErrors.Visible = False
            .lblErrorCountNo.Visible = False
            .lblErrorCount.Visible = False

            Call FindLoggingData()

            ' Check File Versions and program compatibility.
            ' Also displays warnings about RC versions etc.
            If FileDataSuitable() Then Exit Sub 'Exit if the file data is not compatible.

            'Read the File line by line (2nd Pass)
            .barReadFile.Value = 0
            .barReadFile.Visible = True
            .richtxtLogAnalysis.Focus()
            Do While objReader.Peek() <> -1

                CodeTimerStart = Format(Now, "ffff")

                DataArrayCounter = 0
                DataSplit = ""
                Array.Clear(DataArray, 0, DataArray.Length)

                DataLine = DataLine + 1
                'Update progress bar, note -1 and back again keeps it updaing real time!
                PercentageComplete = (DataLine / TotalDataLines) * 100
                '.barReadFile.Value = PercentageComplete
                '.barReadFile.Refresh()
                'If PercentageComplete Then .barReadFile.Value = PercentageComplete - 1
                '.barReadFile.Refresh()
                .barReadFile.Value = PercentageComplete
                '.barReadFile.Refresh()
                'My.Application.DoEvents()
                'Update the TaskBar Progress Bar
                If osVer.Major >= 6 And osVer.Minor >= 1 Then 'Only allowed in Windows 7 or above
                    TaskbarManager.Instance.SetProgressValue(DataLine, TotalDataLines, frmMainFormHandle)
                End If

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


                If Format(Now, "ffff") - CodeTimerStart > 10 Then Debug.Print("Read Takes = " & Format(Now, "ffff") - CodeTimerStart & "μs")

                '######################################
                '### Main Checking Code Starts Here ###
                '######################################
                Try

                    ' Write Log Analysis Header.
                    Call WriteLogFileHeader()


                    'Debug.Print("DATA: " & DataArray(0).PadRight(5) & " Flying: " & Log_In_Flight & " -- Mode_In_Flight_Start_Time: " & Mode_In_Flight_Start_Time & " -- Log_Current_Mode_Flight_Time (Flying Only): " & Log_Current_Mode_Flight_Time & " -- Log_Current_Flight_Time: " & Log_Current_Flight_Time & " -- Log_Total_Flight_Time: " & Log_Total_Flight_Time & " -- Log_Current_Mode_Time: " & Log_Current_Mode_Time)
                    'Debug.Print("DATA: " & DataArray(0).PadRight(5) & " Flying: " & Log_In_Flight & " -- Log_CTUN_ThrOut: " & Log_CTUN_ThrOut & " -- CTUN_ThrottleUp: " & CTUN_ThrottleUp & " -- Log_Ground_BarAlt: " & Log_Ground_BarAlt & " -- Log_CTUN_BarAlt: " & Log_CTUN_BarAlt & " -- Log_Armed_BarAlt: " & Log_Armed_BarAlt & " -- Log_Disarmed_BarAlt: " & Log_Disarmed_BarAlt)
                    'Debug.Print("DATA: " & DataArray(0).PadRight(5) & " Flying: " & Log_In_Flight & " -- Dist_From_Launch: " & Format(Dist_From_Launch * 1000, "0.00") & " -- Mode_Min_Dist_From_Launch: " & Format(Mode_Min_Dist_From_Launch * 1000, "0.00") & " -- Mode_Max_Dist_From_Launch: " & Format(Mode_Max_Dist_From_Launch * 1000, "0.00"))

                    'Parameter Checks
                    Call Parameter_Checks()

                    'EV Checks
                    Call EV_Checks()

                    'Error Checks
                    Call ERROR_Checks()

                    'Mode Checks
                    Call Mode_Checks()

                    'CURR Checks
                    Call CURR_Checks()

                    'CTUN Checks
                    Call CTUN_Checks()

                    'GPS Checks
                    Call GPS_Checks()

                    'IMU Checks - Vibration
                    Call IMU_Checks()

                    'NTUN Checks, Navigation Data - MUST CALL BEFORE ATT due to Charting Data
                    Call NTUN_Checks()

                    'ATT Checks, Roll and Pitch
                    Call ATT_Checks()

                    'PM Checks, process manager timings
                    Call PM_Checks()

                    'DU32 Checks
                    Call DU32_Checks()

                    'CMD Checks
                    Call CMD_Checks()

                    'Additional Checks not related to any one log data type
                    Call Additional_Checks()

                    'This keeps log of how long we have been in this mode, note that this includes ground time!
                    Log_Current_Mode_Time = DateDiff(DateInterval.Second, Log_Last_Mode_Changed_DateTime, Log_GPS_DateTime)

                    'Check the DEVELOPER OPTION not to read all the file, i.e. brown out detection
                    If Read_LOG_Percentage <> 100 Then
                        If DataLine / TotalDataLines * 100 > Read_LOG_Percentage Then Exit Do
                    End If

                    'Check ESCAPE has not been pressed
                    .richtxtLogAnalysis.Focus()
                    Application.DoEvents()
                    If ESCPress = True Then
                        WriteTextLog("")
                        WriteTextLog(Log_GPS_DateTime & " - " & Format(DataLine, "000000") & ": ERROR: User has aborted the analysis")
                        ESCPress = False
                        .lblEsc.Visible = False
                        .btnGraphs.Visible = True
                        .btnCopyText.Visible = True
                        .btnParameters.Visible = True
                        Exit Sub
                    End If


                    'Catch Any Errors
                Catch
                    Debug.Print("Error Processing DataLine " & DataLine & " try Deleting this line!")
                    WriteTextLog("")
                    WriteTextLog(Log_GPS_DateTime & " - " & DataLine & ": Error Processing this log Line, line ignored")
                    WriteTextLog(Log_GPS_DateTime & " - " & DataLine & ": " & Data)
                    WriteTextLog("")
                    ErrorCount = ErrorCount + 1
                    .lblErrors.Visible = True
                    .lblErrors.Refresh()
                    .lblErrorCountNo.Visible = True
                    .lblErrorCount.Visible = True
                    .lblErrorCountNo.Text = ErrorCount
                    .lblErrorCount.Refresh()
                    .lblErrorCountNo.Refresh()
                End Try
            Loop

            'Clean Up Tasks
            objReader.Close()

            'Check the Log_Current_Flight_Time is Zero, if it is not the the log ended without running the "Landed" code.
            'Im this case add the Log_Current_Flight_Time to the Log_Total_Flight_Time before running the Flight Summary.
            Debug.Print("Add Log_Current_Flight_Time to Log_Total_Flight_Time in case the code did not detect a landing")
            Log_Total_Flight_Time = Log_Total_Flight_Time + Log_Current_Flight_Time

            'Display the final summary
            Call AddFinalFlightSummary()

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

            'Real time update the progress bar so it does not look like we only did 75%
            .barReadFile.Value = 99
            .barReadFile.Refresh()
            My.Application.DoEvents()
            .barReadFile.Value = 100
            .barReadFile.Refresh()
            My.Application.DoEvents()
            .barReadFile.Visible = False

            Debug.Print("Sub ReadFile Completed" & vbNewLine)

            'Restore the form and complete the task bar progress
            If osVer.Major >= 6 And osVer.Minor >= 1 Then 'Only allowed in Windows 7 or above
                .WindowState = FormWindowState.Normal
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress, frmMainFormHandle)
                frmMainForm.richtxtLogAnalysis.HideSelection = False
                frmMainForm.richtxtLogAnalysis.SelectionStart = 0
                frmMainForm.richtxtLogAnalysis.HideSelection = True
            End If


            Debug.Print("Total Time = T" & Format(Now, "ss") - CodeTimerStart2 & " Seconds")
            'Threading.Thread.Sleep(3000)

        End With
    End Sub

End Module
