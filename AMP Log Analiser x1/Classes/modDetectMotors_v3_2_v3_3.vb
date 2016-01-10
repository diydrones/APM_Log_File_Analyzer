Module modDetectMotors_v3_2_v3_3

    Public Sub DetectMotors()
        Call DetectMotors_BasedOnFluctuations()
    End Sub


    Public Sub DetectMotors_BasedOnFluctuations()

        ' In the end we may need to make a mixture of the Channel Average code below and this code.
        ' The Channel Average code creates higher numbers for the motor channels but has the 
        ' significant draw back that when a non-channel remains within the scan limits it is counted as a motor.
        ' It is for this reason the fluctuation code was created. However, this code may fail if a gimbal was being
        ' driven by the Flight Controller because the fluctuations on those channels may be within the Motor range.
        ' For this reason we must keep the MotorDetectionMinFluctuation value as high as possible and the 
        ' MotorDetectionThreshold as low as possible.
        ' The other trouble with this code is that when a quad is flying forward the front motors do much less than
        ' then side and back motors. So for a Hex motors 1 and 2 usually produce much lower results than all the other motors.
        ' To speed up the code we monitor the % of Channels 1,2 and 3, if they fall below MotorDetectionEnd then we stop. The 
        ' reason for this is that channels will find the first calcualtion as 50%, and will start to drop quickly. Only 
        ' motor channels seem to do this. 
        ' Also to speed up the code we monitor the number of succcessful hits on channels 1,2, and 3, once these reach
        ' MotorDetectionEnd_Count then we also stop. 


        Dim MotorDetectionThreshold As Integer = 2          ' the % an output must be within the same limits as 
        Dim MotorDetectionEnd_Percent As Integer = 5        ' the critical low % for either motor 1,2 or 3 before the analysis stops
        Dim MotorDetectionEnd_Count As Integer = 15         ' When all motors 1,2 or 3 have at least this many detections the analysis stops
        Dim MotorDetectionMinFluctuation As Integer = 18    ' The Lowest difference between PWM level to be a possible motor
        Dim SkipThis As Boolean = False


        ' In the event this is the first RCOU data line then prime the Channel Variables
        If Log_RCOU_Ch1 = 0 Then
            ' Increment the RCOU Counter
            MotorDetec_Counter_RCOU += 1
            Debug.Print("Processing RCOU_Count = " & MotorDetec_Counter_RCOU)
            ' Set to skip
            SkipThis = True
            ' Populate the current PWM Readings into the global variables
            Log_RCOU_Ch1 = DataArray(2) : Log_RCOU_Ch2 = DataArray(3) : Log_RCOU_Ch3 = DataArray(4)
            Log_RCOU_Ch4 = DataArray(5) : Log_RCOU_Ch5 = DataArray(6) : Log_RCOU_Ch6 = DataArray(7)
            Log_RCOU_Ch7 = DataArray(8) : Log_RCOU_Ch8 = DataArray(9) : Log_RCOU_Ch9 = DataArray(10)
            Log_RCOU_Ch10 = DataArray(11) : Log_RCOU_Ch11 = DataArray(12) : Log_RCOU_Ch12 = DataArray(13)
            Debug.Print("FIRST PASS - PRIME THE VARIABLES")
            Debug.Print(" Ch1,  Ch2,  Ch3 = " & Format(Log_RCOU_Ch1, "0000") & "," & Format(Log_RCOU_Ch2, "0000") & "," & Format(Log_RCOU_Ch3, "0000"))
            Debug.Print(" Ch4,  Ch5,  Ch6 = " & Format(Log_RCOU_Ch4, "0000") & "," & Format(Log_RCOU_Ch5, "0000") & "," & Format(Log_RCOU_Ch6, "0000"))
            Debug.Print(" Ch7,  Ch8,  Ch9 = " & Format(Log_RCOU_Ch7, "0000") & "," & Format(Log_RCOU_Ch8, "0000") & "," & Format(Log_RCOU_Ch9, "0000"))
            Debug.Print("Ch10, Ch11, Ch12 = " & Format(Log_RCOU_Ch10, "0000") & "," & Format(Log_RCOU_Ch11, "0000") & "," & Format(Log_RCOU_Ch12, "0000"))
        End If

        ' Do we need to check this RCOU data?
        ' If Motor 1 has not changed then skip the testing
        ' Note: 
        '       this speeds up analysis and create better calculations.
        If (DataArray(2) = Log_RCOU_Ch1) Then SkipThis = True

        If SkipThis = False And MotorDetec_Stop = False Then

            'Dim sWatch As System.Diagnostics.Stopwatch = New System.Diagnostics.Stopwatch() : sWatch.Start()

            ' Increment the RCOU Counter
            MotorDetec_Counter_RCOU += 1
            Debug.Print("Processing RCOU_Count = " & MotorDetec_Counter_RCOU)

            ' Populate the current PWM Readings into the temporary variables
            Dim Ch1_PWM As Integer = DataArray(2) : Dim Ch2_PWM As Integer = DataArray(3) : Dim Ch3_PWM As Integer = DataArray(4)
            Dim Ch4_PWM As Integer = DataArray(5) : Dim Ch5_PWM As Integer = DataArray(6) : Dim Ch6_PWM As Integer = DataArray(7)
            Dim Ch7_PWM As Integer = DataArray(8) : Dim Ch8_PWM As Integer = DataArray(9) : Dim Ch9_PWM As Integer = DataArray(10)
            Dim Ch10_PWM As Integer = DataArray(11) : Dim Ch11_PWM As Integer = DataArray(12) : Dim Ch12_PWM As Integer = DataArray(13)

            ' Calculate the fluctuations for each channel based on the globally saved RCOU PWM's
            Dim Ch1_FLU As Integer = Math.Abs(Ch1_PWM - Log_RCOU_Ch1) : Dim Ch2_FLU As Integer = Math.Abs(Ch2_PWM - Log_RCOU_Ch2)
            Dim Ch3_FLU As Integer = Math.Abs(Ch3_PWM - Log_RCOU_Ch3) : Dim Ch4_FLU As Integer = Math.Abs(Ch4_PWM - Log_RCOU_Ch4)
            Dim Ch5_FLU As Integer = Math.Abs(Ch5_PWM - Log_RCOU_Ch5) : Dim Ch6_FLU As Integer = Math.Abs(Ch6_PWM - Log_RCOU_Ch6)
            Dim Ch7_FLU As Integer = Math.Abs(Ch7_PWM - Log_RCOU_Ch7) : Dim Ch8_FLU As Integer = Math.Abs(Ch8_PWM - Log_RCOU_Ch8)
            Dim Ch9_FLU As Integer = Math.Abs(Ch9_PWM - Log_RCOU_Ch9) : Dim Ch10_FLU As Integer = Math.Abs(Ch10_PWM - Log_RCOU_Ch10)
            Dim Ch11_FLU As Integer = Math.Abs(Ch11_PWM - Log_RCOU_Ch11) : Dim Ch12_FLU As Integer = Math.Abs(Ch12_PWM - Log_RCOU_Ch12)


            ' Check which other channels are within the fluctuation limit and increment that Channel counter by one.
            If Ch1_FLU >= MotorDetectionMinFluctuation Then MotorDetec_Counter_Ch1 += 1 : Debug.Print("Ch1 in range, FLU >= " & MotorDetectionMinFluctuation)
            If Ch2_FLU >= MotorDetectionMinFluctuation Then MotorDetec_Counter_Ch2 += 1 : Debug.Print("Ch2 in range, FLU >= " & MotorDetectionMinFluctuation)
            If Ch3_FLU >= MotorDetectionMinFluctuation Then MotorDetec_Counter_Ch3 += 1 : Debug.Print("Ch3 in range, FLU >= " & MotorDetectionMinFluctuation)
            If Ch4_FLU >= MotorDetectionMinFluctuation Then MotorDetec_Counter_Ch4 += 1 : Debug.Print("Ch4 in range, FLU >= " & MotorDetectionMinFluctuation)
            If Ch5_FLU >= MotorDetectionMinFluctuation Then MotorDetec_Counter_Ch5 += 1 : Debug.Print("Ch5 in range, FLU >= " & MotorDetectionMinFluctuation)
            If Ch6_FLU >= MotorDetectionMinFluctuation Then MotorDetec_Counter_Ch6 += 1 : Debug.Print("Ch6 in range, FLU >= " & MotorDetectionMinFluctuation)
            If Ch7_FLU >= MotorDetectionMinFluctuation Then MotorDetec_Counter_Ch7 += 1 : Debug.Print("Ch7 in range, FLU >= " & MotorDetectionMinFluctuation)
            If Ch8_FLU >= MotorDetectionMinFluctuation Then MotorDetec_Counter_Ch8 += 1 : Debug.Print("Ch8 in range, FLU >= " & MotorDetectionMinFluctuation)
            If Ch9_FLU >= MotorDetectionMinFluctuation Then MotorDetec_Counter_Ch9 += 1 : Debug.Print("Ch9 in range, FLU >= " & MotorDetectionMinFluctuation)
            If Ch10_FLU >= MotorDetectionMinFluctuation Then MotorDetec_Counter_Ch10 += 1 : Debug.Print("Ch10 in range, FLU >= " & MotorDetectionMinFluctuation)
            If Ch11_FLU >= MotorDetectionMinFluctuation Then MotorDetec_Counter_Ch11 += 1 : Debug.Print("Ch11 in range, FLU >= " & MotorDetectionMinFluctuation)
            If Ch12_FLU >= MotorDetectionMinFluctuation Then MotorDetec_Counter_Ch12 += 1 : Debug.Print("Ch12 in range, FLU >= " & MotorDetectionMinFluctuation)


            ' Re-Calculate the potential number of motors so far, it does this for every reading!
            APM_No_Motors = 0
            If MotorDetec_Counter_Ch1 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If MotorDetec_Counter_Ch2 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If MotorDetec_Counter_Ch3 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If MotorDetec_Counter_Ch4 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If MotorDetec_Counter_Ch5 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If MotorDetec_Counter_Ch6 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If MotorDetec_Counter_Ch7 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If MotorDetec_Counter_Ch8 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If MotorDetec_Counter_Ch9 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If MotorDetec_Counter_Ch10 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If MotorDetec_Counter_Ch11 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If MotorDetec_Counter_Ch12 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1


            ' Show the Debug Data
            Debug.Print(" Ch1: OLD, PWM, FLU, CNT, DET% -- " & Format(Log_RCOU_Ch1, "0000") & "," & Format(Ch1_PWM, "0000") & "," & Format(Ch1_FLU, "0000") & "," & Format(MotorDetec_Counter_Ch1, "0000") & "," & Format(MotorDetec_Counter_Ch1 / MotorDetec_Counter_RCOU * 100, "000") & "%")
            Debug.Print(" Ch2: OLD, PWM, FLU, CNT, DET% -- " & Format(Log_RCOU_Ch2, "0000") & "," & Format(Ch2_PWM, "0000") & "," & Format(Ch2_FLU, "0000") & "," & Format(MotorDetec_Counter_Ch2, "0000") & "," & Format(MotorDetec_Counter_Ch2 / MotorDetec_Counter_RCOU * 100, "000") & "%")
            Debug.Print(" Ch3: OLD, PWM, FLU, CNT, DET% -- " & Format(Log_RCOU_Ch3, "0000") & "," & Format(Ch3_PWM, "0000") & "," & Format(Ch3_FLU, "0000") & "," & Format(MotorDetec_Counter_Ch3, "0000") & "," & Format(MotorDetec_Counter_Ch3 / MotorDetec_Counter_RCOU * 100, "000") & "%")
            Debug.Print(" Ch4: OLD, PWM, FLU, CNT, DET% -- " & Format(Log_RCOU_Ch4, "0000") & "," & Format(Ch4_PWM, "0000") & "," & Format(Ch4_FLU, "0000") & "," & Format(MotorDetec_Counter_Ch4, "0000") & "," & Format(MotorDetec_Counter_Ch4 / MotorDetec_Counter_RCOU * 100, "000") & "%")
            Debug.Print(" Ch5: OLD, PWM, FLU, CNT, DET% -- " & Format(Log_RCOU_Ch5, "0000") & "," & Format(Ch5_PWM, "0000") & "," & Format(Ch5_FLU, "0000") & "," & Format(MotorDetec_Counter_Ch5, "0000") & "," & Format(MotorDetec_Counter_Ch5 / MotorDetec_Counter_RCOU * 100, "000") & "%")
            Debug.Print(" Ch6: OLD, PWM, FLU, CNT, DET% -- " & Format(Log_RCOU_Ch6, "0000") & "," & Format(Ch6_PWM, "0000") & "," & Format(Ch6_FLU, "0000") & "," & Format(MotorDetec_Counter_Ch6, "0000") & "," & Format(MotorDetec_Counter_Ch6 / MotorDetec_Counter_RCOU * 100, "000") & "%")
            Debug.Print(" Ch7: OLD, PWM, FLU, CNT, DET% -- " & Format(Log_RCOU_Ch7, "0000") & "," & Format(Ch7_PWM, "0000") & "," & Format(Ch7_FLU, "0000") & "," & Format(MotorDetec_Counter_Ch7, "0000") & "," & Format(MotorDetec_Counter_Ch7 / MotorDetec_Counter_RCOU * 100, "000") & "%")
            Debug.Print(" Ch8: OLD, PWM, FLU, CNT, DET% -- " & Format(Log_RCOU_Ch8, "0000") & "," & Format(Ch8_PWM, "0000") & "," & Format(Ch8_FLU, "0000") & "," & Format(MotorDetec_Counter_Ch8, "0000") & "," & Format(MotorDetec_Counter_Ch8 / MotorDetec_Counter_RCOU * 100, "000") & "%")
            Debug.Print(" Ch9: OLD, PWM, FLU, CNT, DET% -- " & Format(Log_RCOU_Ch9, "0000") & "," & Format(Ch9_PWM, "0000") & "," & Format(Ch9_FLU, "0000") & "," & Format(MotorDetec_Counter_Ch9, "0000") & "," & Format(MotorDetec_Counter_Ch9 / MotorDetec_Counter_RCOU * 100, "000") & "%")
            Debug.Print(" Ch10: OLD, PWM, FLU, CNT, DET% -- " & Format(Log_RCOU_Ch10, "0000") & "," & Format(Ch10_PWM, "0000") & "," & Format(Ch10_FLU, "0000") & "," & Format(MotorDetec_Counter_Ch10, "0000") & "," & Format(MotorDetec_Counter_Ch10 / MotorDetec_Counter_RCOU * 100, "000") & "%")
            Debug.Print(" Ch11: OLD, PWM, FLU, CNT, DET% -- " & Format(Log_RCOU_Ch11, "0000") & "," & Format(Ch11_PWM, "0000") & "," & Format(Ch11_FLU, "0000") & "," & Format(MotorDetec_Counter_Ch11, "0000") & "," & Format(MotorDetec_Counter_Ch11 / MotorDetec_Counter_RCOU * 100, "000") & "%")
            Debug.Print(" Ch12: OLD, PWM, FLU, CNT, DET% -- " & Format(Log_RCOU_Ch12, "0000") & "," & Format(Ch12_PWM, "0000") & "," & Format(Ch12_FLU, "0000") & "," & Format(MotorDetec_Counter_Ch12, "0000") & "," & Format(MotorDetec_Counter_Ch12 / MotorDetec_Counter_RCOU * 100, "000") & "%")
            Debug.Print("Based on a Motor Detection Threshold of " & MotorDetectionThreshold & "% the number of motors would be " & APM_No_Motors & " Motors")


            ' DEBUG CODE, SHOULD NOT BE DEPLOYED.
            ' USED TO TRAP IF THIS CODE SWAPS THE NUMBER OF MOTORS FOUND DURING THE ANALYSIS.
            ' SET TO THE NUMBER OF MOTORS EXPECTED AND SET A BREAKPOINT, IF ALL IS WELL IT WILL NOT STOP.
            If APM_No_Motors <> 6 Then
                Debug.Print("Motor Detection Code differs between results")
            End If

            ' Set the MotorDetectionEnd_Monitor to activate the STOP code
            If MotorDetec_Stop_Monitor = False And MotorDetec_Counter_Ch1 / MotorDetec_Counter_RCOU * 100 >= MotorDetectionEnd_Percent _
                    Then MotorDetec_Stop_Monitor = True
            ' Should we STOP the analysis because we have enough data or the data is getting out of control?
            If (MotorDetec_Counter_Ch1 / MotorDetec_Counter_RCOU * 100 <= MotorDetectionEnd_Percent _
                                        Or MotorDetec_Counter_Ch2 / MotorDetec_Counter_RCOU * 100 <= MotorDetectionEnd_Percent _
                                        Or MotorDetec_Counter_Ch3 / MotorDetec_Counter_RCOU * 100 <= MotorDetectionEnd_Percent _
                                        Or (MotorDetec_Counter_Ch1 >= MotorDetectionEnd_Count _
                                        And MotorDetec_Counter_Ch2 >= MotorDetectionEnd_Count _
                                        And MotorDetec_Counter_Ch3 >= MotorDetectionEnd_Count)) _
                                        And MotorDetec_Stop_Monitor = True _
            Then
                MotorDetec_Stop = True
                Debug.Print("FORCING MOTOR DETECTION ANALYSIS TO STOP !!!")
                ' Store the Values to return them back as the final results.
                ' Populate the global variables with the results.
                Log_RCOU_Ch1 = MotorDetec_Counter_Ch1 / MotorDetec_Counter_RCOU * 100 : Log_RCOU_Ch2 = MotorDetec_Counter_Ch2 / MotorDetec_Counter_RCOU * 100
                Log_RCOU_Ch3 = MotorDetec_Counter_Ch3 / MotorDetec_Counter_RCOU * 100 : Log_RCOU_Ch4 = MotorDetec_Counter_Ch4 / MotorDetec_Counter_RCOU * 100
                Log_RCOU_Ch5 = MotorDetec_Counter_Ch5 / MotorDetec_Counter_RCOU * 100 : Log_RCOU_Ch6 = MotorDetec_Counter_Ch6 / MotorDetec_Counter_RCOU * 100
                Log_RCOU_Ch7 = MotorDetec_Counter_Ch7 / MotorDetec_Counter_RCOU * 100 : Log_RCOU_Ch8 = MotorDetec_Counter_Ch8 / MotorDetec_Counter_RCOU * 100
                Log_RCOU_Ch9 = MotorDetec_Counter_Ch9 / MotorDetec_Counter_RCOU * 100 : Log_RCOU_Ch10 = MotorDetec_Counter_Ch10 / MotorDetec_Counter_RCOU * 100
                Log_RCOU_Ch11 = MotorDetec_Counter_Ch11 / MotorDetec_Counter_RCOU * 100 : Log_RCOU_Ch12 = MotorDetec_Counter_Ch12 / MotorDetec_Counter_RCOU * 100
            Else
                ' Store the New Values for the next run.
                ' Populate the current PWM Readings into the global variables
                Log_RCOU_Ch1 = DataArray(2) : Log_RCOU_Ch2 = DataArray(3) : Log_RCOU_Ch3 = DataArray(4)
                Log_RCOU_Ch4 = DataArray(5) : Log_RCOU_Ch5 = DataArray(6) : Log_RCOU_Ch6 = DataArray(7)
                Log_RCOU_Ch7 = DataArray(8) : Log_RCOU_Ch8 = DataArray(9) : Log_RCOU_Ch9 = DataArray(10)
                Log_RCOU_Ch10 = DataArray(11) : Log_RCOU_Ch11 = DataArray(12) : Log_RCOU_Ch12 = DataArray(13)
            End If

            '' Ensure we have enough data to make this assesment, otherwise set APM_No_Motors to 0
            'If MotorDetec_Counter_RCOU < 10 Then
            '    APM_No_Motors = 0
            '    Debug.Print(APM_No_Motors & " Motors will be used due to lack of data")
            'End If

            Debug.Print(APM_No_Motors & " Motors will be used.")
            Debug.Print(MotorDetec_Counter_RCOU & " RCOU Data Lines.")
            Debug.Print(MotorDetec_Counter_RCOU & "")

            'sWatch.Stop() ': Debug.Print(sWatch.Elapsed.ToString)
            'myStopWatchTimer = myStopWatchTimer + sWatch.Elapsed.TotalMilliseconds
            'Debug.Print(myStopWatchTimer & "ms")

        End If

    End Sub


    'Public Sub DetectMotors_BasedOnRCOU1_WithRefTo_RC2_RC3()

    '    ' ##############################################################
    '    ' ### THIS SUB IS NOW REDUNDENT IN FAVOR OF FLUCTUATION CODE ###
    '    ' ##############################################################

    '    ' In testing my files I found that a motor produces a minimum of 60%
    '    ' A non-motor produces a maximum of 12% where the value was 1500 PWM 99% of the time,
    '    ' and 8% where the value was 1100 PWM 99% of the time.
    '    ' Therefore the MotorDetectionThreshold is currently set at 40%

    '    Dim MotorDetectionThreshold As Integer = 40     ' the % an output must be within the same limits as 
    '    '                                               ' channels 1 to 3 to be considered as a motor.
    '    Dim MotorDetectionOffset As Integer = 0         ' The highest % difference between either Ch1 and Ch2 or Ch1 and Ch3
    '    Dim MotorIgnoreMin As Integer = 1225            ' If Ch1 is within this range then we check the other channels
    '    Dim MotorIgnoreMax As Integer = 1775            ' If Ch1 is within this range then we check the other channels

    '    ' Do we need to check this RCOU data?
    '    ' If Motor 1 is not in the correct range then ignore 
    '    ' Note: The correct range is the outer fringes <1225 or > 1775.
    '    '       this speeds up analysis and creates better results as all motors are in the same range more often.
    '    Dim SkipThis As Boolean = False
    '    If (DataArray(2) >= MotorIgnoreMin And DataArray(2) <= MotorIgnoreMax) Then SkipThis = True

    '    ' ##############################################################
    '    ' ### THIS SUB IS NOW REDUNDENT IN FAVOR OF FLUCTUATION CODE ###
    '    ' ##############################################################

    '    If SkipThis = False Then

    '        'Dim sWatch As System.Diagnostics.Stopwatch = New System.Diagnostics.Stopwatch() : sWatch.Start()

    '        ' Increment the RCOU Counter
    '        MotorDetec_Counter_RCOU += 1
    '        Debug.Print("Processing RCOU_Count = " & MotorDetec_Counter_RCOU)

    '        ' Populate the current Ch1 PWM Readings
    '        Dim Ch1 As Integer = DataArray(2) : Dim Ch2 As Integer = DataArray(3) : Dim Ch3 As Integer = DataArray(4)
    '        Dim Ch4 As Integer = DataArray(5) : Dim Ch5 As Integer = DataArray(6) : Dim Ch6 As Integer = DataArray(7)
    '        Dim Ch7 As Integer = DataArray(8) : Dim Ch8 As Integer = DataArray(9) : Dim Ch9 As Integer = DataArray(10)
    '        Dim Ch10 As Integer = DataArray(11) : Dim Ch11 As Integer = DataArray(12) : Dim Ch12 As Integer = DataArray(13)
    '        Debug.Print(" Ch1,  Ch2,  Ch3 = " & Format(Ch1, "0000") & "," & Format(Ch2, "0000") & "," & Format(Ch3, "0000"))
    '        Debug.Print(" Ch4,  Ch5,  Ch6 = " & Format(Ch4, "0000") & "," & Format(Ch5, "0000") & "," & Format(Ch6, "0000"))
    '        Debug.Print(" Ch7,  Ch8,  Ch9 = " & Format(Ch7, "0000") & "," & Format(Ch8, "0000") & "," & Format(Ch9, "0000"))
    '        Debug.Print("Ch10, Ch11, Ch12 = " & Format(Ch10, "0000") & "," & Format(Ch11, "0000") & "," & Format(Ch12, "0000"))

    '        ' Calculate the Min between Ch1 and Ch3
    '        Log_RCOU_MinCh1toCh3 = Ch1

    '        ' ##############################################################
    '        ' ### THIS SUB IS NOW REDUNDENT IN FAVOR OF FLUCTUATION CODE ###
    '        ' ##############################################################

    '        ' Calculate the Min Max offsets 
    '        ' - in this version based on the largest difference between ch1 and ch2 or ch1 and ch3.
    '        If Math.Abs(Ch1 - Ch2) > Math.Abs(Ch1 - Ch3) Then
    '            MotorDetectionOffset = Math.Abs(((Ch1 - Ch2) / Ch1 * 100)) + 1
    '        Else
    '            MotorDetectionOffset = Math.Abs(((Ch1 - Ch3) / Ch1 * 100)) + 1
    '        End If
    '        Debug.Print("Motor Detection Offset = " & MotorDetectionOffset & "%")
    '        Log_RCOU_MinCh1toCh3 = Ch1 - ((Ch1 * MotorDetectionOffset) / 100)
    '        Log_RCOU_MaxCh1toCh3 = Ch1 + ((Ch1 * MotorDetectionOffset) / 100)
    '        Debug.Print("Other Channels must be between " & Log_RCOU_MinCh1toCh3 & " and " _
    '                    & Log_RCOU_MaxCh1toCh3 & " to be considered a possible motor")

    '        ' Check which other channels are within the Min and Max range and increment that Channel counter by one.
    '        If Ch1 >= Log_RCOU_MinCh1toCh3 And Ch1 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch1 += 1 : Debug.Print("Ch1 in range")  ' Always!
    '        If Ch2 >= Log_RCOU_MinCh1toCh3 And Ch2 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch2 += 1 : Debug.Print("Ch2 in range")
    '        If Ch3 >= Log_RCOU_MinCh1toCh3 And Ch3 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch3 += 1 : Debug.Print("Ch3 in range")
    '        If Ch4 >= Log_RCOU_MinCh1toCh3 And Ch4 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch4 += 1 : Debug.Print("Ch4 in range")
    '        If Ch5 >= Log_RCOU_MinCh1toCh3 And Ch5 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch5 += 1 : Debug.Print("Ch5 in range")
    '        If Ch6 >= Log_RCOU_MinCh1toCh3 And Ch6 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch6 += 1 : Debug.Print("Ch6 in range")
    '        If Ch7 >= Log_RCOU_MinCh1toCh3 And Ch7 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch7 += 1 : Debug.Print("Ch7 in range")
    '        If Ch8 >= Log_RCOU_MinCh1toCh3 And Ch8 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch8 += 1 : Debug.Print("Ch8 in range")
    '        If Ch9 >= Log_RCOU_MinCh1toCh3 And Ch9 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch9 += 1 : Debug.Print("Ch9 in range")
    '        If Ch10 >= Log_RCOU_MinCh1toCh3 And Ch10 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch10 += 1 : Debug.Print("Ch10 in range")
    '        If Ch11 >= Log_RCOU_MinCh1toCh3 And Ch11 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch11 += 1 : Debug.Print("Ch11 in range")
    '        If Ch12 >= Log_RCOU_MinCh1toCh3 And Ch12 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch12 += 1 : Debug.Print("Ch12 in range")
    '        Debug.Print(" Ch1,  Ch2,  Ch3 = " & Format(Log_RCOU_Ch1 / MotorDetec_Counter_RCOU * 100, "000") & "%," & Format(Log_RCOU_Ch2 / MotorDetec_Counter_RCOU * 100, "000") & "%," & Format(Log_RCOU_Ch3 / MotorDetec_Counter_RCOU * 100, "000") & "%")
    '        Debug.Print(" Ch4,  Ch5,  Ch6 = " & Format(Log_RCOU_Ch4 / MotorDetec_Counter_RCOU * 100, "000") & "%," & Format(Log_RCOU_Ch5 / MotorDetec_Counter_RCOU * 100, "000") & "%," & Format(Log_RCOU_Ch6 / MotorDetec_Counter_RCOU * 100, "000") & "%")
    '        Debug.Print(" Ch7,  Ch8,  Ch9 = " & Format(Log_RCOU_Ch7 / MotorDetec_Counter_RCOU * 100, "000") & "%," & Format(Log_RCOU_Ch8 / MotorDetec_Counter_RCOU * 100, "000") & "%," & Format(Log_RCOU_Ch9 / MotorDetec_Counter_RCOU * 100, "000") & "%")
    '        Debug.Print("Ch10, Ch11, Ch12 = " & Format(Log_RCOU_Ch10 / MotorDetec_Counter_RCOU * 100, "000") & "%," & Format(Log_RCOU_Ch11 / MotorDetec_Counter_RCOU * 100, "000") & "%," & Format(Log_RCOU_Ch12 / MotorDetec_Counter_RCOU * 100, "000") & "%")

    '        ' ##############################################################
    '        ' ### THIS SUB IS NOW REDUNDENT IN FAVOR OF FLUCTUATION CODE ###
    '        ' ##############################################################

    '        ' Re-Calculate the potential number of motors so far, it does this for every reading!
    '        APM_No_Motors = 1
    '        If Log_RCOU_Ch2 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
    '        If Log_RCOU_Ch3 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
    '        If Log_RCOU_Ch4 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
    '        If Log_RCOU_Ch5 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
    '        If Log_RCOU_Ch6 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
    '        If Log_RCOU_Ch7 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
    '        If Log_RCOU_Ch8 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
    '        If Log_RCOU_Ch9 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
    '        If Log_RCOU_Ch10 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
    '        If Log_RCOU_Ch11 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
    '        If Log_RCOU_Ch12 / MotorDetec_Counter_RCOU * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
    '        Debug.Print("Based on a Motor Detection Threshold of " & MotorDetectionThreshold & " the number of motors would be " & APM_No_Motors & " Motors")

    '        ' THIS IS JUST FOR TESTING, MY TWO TEST FILE HAVE NON-MOTOR OUTPUTS ON CHANNELS 8 AND 9
    '        ' THIS BIT JUST DEBUGS WHAT THEIR RESULTS END UP AT.
    '        If Log_RCOU_Ch8 / MotorDetec_Counter_RCOU * 100 > RCOU_MaxNonMotorChannel1 Then
    '            RCOU_MaxNonMotorChannel1 = Log_RCOU_Ch8 / MotorDetec_Counter_RCOU * 100
    '        End If
    '        If Log_RCOU_Ch9 / MotorDetec_Counter_RCOU * 100 > RCOU_MaxNonMotorChannel2 Then
    '            RCOU_MaxNonMotorChannel2 = Log_RCOU_Ch9 / MotorDetec_Counter_RCOU * 100
    '        End If
    '        Debug.Print("Non Motor Channel 8 Max % = " & RCOU_MaxNonMotorChannel1)
    '        Debug.Print("Non Motor Channel 9 Max % = " & RCOU_MaxNonMotorChannel2)

    '        ' DEBUG CODE, SHOULD NOT BE DEPLOYED.
    '        ' USED TO TRAP IF THIS CODE SWAPS THE NUMBER OF MOTORS FOUND DURING THE ANALYSIS.
    '        ' SET TO THE NUMBER OF MOTORS EXPECTED AND SET A BREAKPOINT, IF ALL IS WELL IT WILL NOT STOP.
    '        'If APM_No_Motors <> 6 Then
    '        '    Debug.Print("Motor Detection Code differs between results")
    '        'End If

    '        ' Ensure we have enough data to make this assesment, otherwise set APM_No_Motors to 0
    '        If MotorDetec_Counter_RCOU < 10 Then
    '            APM_No_Motors = 0
    '            Debug.Print(APM_No_Motors & " Motors will be used due to lack of data")
    '        End If

    '        ' ##############################################################
    '        ' ### THIS SUB IS NOW REDUNDENT IN FAVOR OF FLUCTUATION CODE ###
    '        ' ##############################################################

    '        Debug.Print(APM_No_Motors & " Motors will be used.")
    '        Debug.Print(MotorDetec_Counter_RCOU & " RCOU Data Lines.")
    '        Debug.Print(MotorDetec_Counter_RCOU & "")

    '        'sWatch.Stop() ': Debug.Print(sWatch.Elapsed.ToString)
    '        'myStopWatchTimer = myStopWatchTimer + sWatch.Elapsed.TotalMilliseconds
    '        'Debug.Print(myStopWatchTimer & "ms")

    '    End If

    'End Sub

End Module
