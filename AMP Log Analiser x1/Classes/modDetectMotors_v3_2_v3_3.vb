Module modDetectMotors_v3_2_v3_3

    Public Sub DetectMotors()
        Call DetectMotors_BasedOnRCOU1_WithRefTo_RC2_RC3()
    End Sub


    Public Sub DetectMotors_BasedOnRCOU1_WithRefTo_RC2_RC3()

        ' In testing my files I found that a motor produces a minimum of 60%
        ' A non-motor produces a maximum of 12% where the value was 1500 PWM 99% of the time,
        ' and 8% where the value was 1100 PWM 99% of the time.
        ' Therefore the MotorDetectionThreshold is currently set at 40%

        Dim MotorDetectionThreshold As Integer = 40     ' the % an output must be within the same limits as 
        '                                               ' channels 1 to 3 to be considered as a motor.
        Dim MotorDetectionOffset As Integer = 0         ' The highest % difference between either Ch1 and Ch2 or Ch1 and Ch3
        Dim MotorIgnoreMin As Integer = 1225            ' If Ch1 is within this range then we check the other channels
        Dim MotorIgnoreMax As Integer = 1775            ' If Ch1 is within this range then we check the other channels

        ' Do we need to check this RCOU data?
        ' If Motor 1 is not in the correct range then ignore 
        ' Note: The correct range is the outer fringes <1225 or > 1775.
        '       this speeds up analysis and creates better results as all motors are in the same range more often.
        Dim SkipThis As Boolean = False
        If (DataArray(2) >= MotorIgnoreMin And DataArray(2) <= MotorIgnoreMax) Then SkipThis = True

        If SkipThis = False Then

            'Dim sWatch As System.Diagnostics.Stopwatch = New System.Diagnostics.Stopwatch() : sWatch.Start()

            ' Increment the RCOU Counter
            Log_RCOU_Count += 1
            Debug.Print("Processing RCOU_Count = " & Log_RCOU_Count)

            ' Populate the current Ch1 PWM Readings
            Dim Ch1 As Integer = DataArray(2) : Dim Ch2 As Integer = DataArray(3) : Dim Ch3 As Integer = DataArray(4)
            Dim Ch4 As Integer = DataArray(5) : Dim Ch5 As Integer = DataArray(6) : Dim Ch6 As Integer = DataArray(7)
            Dim Ch7 As Integer = DataArray(8) : Dim Ch8 As Integer = DataArray(9) : Dim Ch9 As Integer = DataArray(10)
            Dim Ch10 As Integer = DataArray(11) : Dim Ch11 As Integer = DataArray(12) : Dim Ch12 As Integer = DataArray(13)
            Debug.Print(" Ch1,  Ch2,  Ch3 = " & Format(Ch1, "0000") & "," & Format(Ch2, "0000") & "," & Format(Ch3, "0000"))
            Debug.Print(" Ch4,  Ch5,  Ch6 = " & Format(Ch4, "0000") & "," & Format(Ch5, "0000") & "," & Format(Ch6, "0000"))
            Debug.Print(" Ch7,  Ch8,  Ch9 = " & Format(Ch7, "0000") & "," & Format(Ch8, "0000") & "," & Format(Ch9, "0000"))
            Debug.Print("Ch10, Ch11, Ch12 = " & Format(10, "0000") & "," & Format(Ch11, "0000") & "," & Format(Ch12, "0000"))

            ' Calculate the Min between Ch1 and Ch3
            Log_RCOU_MinCh1toCh3 = Ch1

            ' Calculate the Min Max offsets 
            ' - in this version based on the largest difference between ch1 and ch2 or ch1 and ch3.
            If Math.Abs(Ch1 - Ch2) > Math.Abs(Ch1 - Ch3) Then
                MotorDetectionOffset = Math.Abs(((Ch1 - Ch2) / Ch1 * 100)) + 1
            Else
                MotorDetectionOffset = Math.Abs(((Ch1 - Ch3) / Ch1 * 100)) + 1
            End If
            Debug.Print("Motor Detection Offset = " & MotorDetectionOffset & "%")
            Log_RCOU_MinCh1toCh3 = Ch1 - ((Ch1 * MotorDetectionOffset) / 100)
            Log_RCOU_MaxCh1toCh3 = Ch1 + ((Ch1 * MotorDetectionOffset) / 100)
            Debug.Print("Other Channels must be between " & Log_RCOU_MinCh1toCh3 & " and " _
                & Log_RCOU_MaxCh1toCh3 & " to be considered a possible motor")

            ' Check which other channels are within the Min and Max range and increment that Channel counter by one.
            If Ch1 >= Log_RCOU_MinCh1toCh3 And Ch1 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch1 += 1 : Debug.Print("Ch1 in range")  ' Always!
            If Ch2 >= Log_RCOU_MinCh1toCh3 And Ch2 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch2 += 1 : Debug.Print("Ch2 in range")
            If Ch3 >= Log_RCOU_MinCh1toCh3 And Ch3 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch3 += 1 : Debug.Print("Ch3 in range")
            If Ch4 >= Log_RCOU_MinCh1toCh3 And Ch4 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch4 += 1 : Debug.Print("Ch4 in range")
            If Ch5 >= Log_RCOU_MinCh1toCh3 And Ch5 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch5 += 1 : Debug.Print("Ch5 in range")
            If Ch6 >= Log_RCOU_MinCh1toCh3 And Ch6 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch6 += 1 : Debug.Print("Ch6 in range")
            If Ch7 >= Log_RCOU_MinCh1toCh3 And Ch7 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch7 += 1 : Debug.Print("Ch7 in range")
            If Ch8 >= Log_RCOU_MinCh1toCh3 And Ch8 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch8 += 1 : Debug.Print("Ch8 in range")
            If Ch9 >= Log_RCOU_MinCh1toCh3 And Ch9 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch9 += 1 : Debug.Print("Ch9 in range")
            If Ch10 >= Log_RCOU_MinCh1toCh3 And Ch10 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch10 += 1 : Debug.Print("Ch10 in range")
            If Ch11 >= Log_RCOU_MinCh1toCh3 And Ch11 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch11 += 1 : Debug.Print("Ch11 in range")
            If Ch12 >= Log_RCOU_MinCh1toCh3 And Ch12 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch12 += 1 : Debug.Print("Ch12 in range")
            Debug.Print(" Ch1,  Ch2,  Ch3 = " & Format(Log_RCOU_Ch1 / Log_RCOU_Count * 100, "000") & "%," & Format(Log_RCOU_Ch2 / Log_RCOU_Count * 100, "000") & "%," & Format(Log_RCOU_Ch3 / Log_RCOU_Count * 100, "000") & "%")
            Debug.Print(" Ch4,  Ch5,  Ch6 = " & Format(Log_RCOU_Ch4 / Log_RCOU_Count * 100, "000") & "%," & Format(Log_RCOU_Ch5 / Log_RCOU_Count * 100, "000") & "%," & Format(Log_RCOU_Ch6 / Log_RCOU_Count * 100, "000") & "%")
            Debug.Print(" Ch7,  Ch8,  Ch9 = " & Format(Log_RCOU_Ch7 / Log_RCOU_Count * 100, "000") & "%," & Format(Log_RCOU_Ch8 / Log_RCOU_Count * 100, "000") & "%," & Format(Log_RCOU_Ch9 / Log_RCOU_Count * 100, "000") & "%")
            Debug.Print("Ch10, Ch11, Ch12 = " & Format(Log_RCOU_Ch10 / Log_RCOU_Count * 100, "000") & "%," & Format(Log_RCOU_Ch11 / Log_RCOU_Count * 100, "000") & "%," & Format(Log_RCOU_Ch12 / Log_RCOU_Count * 100, "000") & "%")


            ' Re-Calculate the potential number of motors so far, it does this for every reading!
            APM_No_Motors = 1
            If Log_RCOU_Ch2 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If Log_RCOU_Ch3 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If Log_RCOU_Ch4 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If Log_RCOU_Ch5 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If Log_RCOU_Ch6 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If Log_RCOU_Ch7 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If Log_RCOU_Ch8 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If Log_RCOU_Ch9 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If Log_RCOU_Ch10 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If Log_RCOU_Ch11 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If Log_RCOU_Ch12 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            Debug.Print("Based on a Motor Detection Threshold of " & MotorDetectionThreshold & " the number of motors would be " & APM_No_Motors & " Motors")

            ' THIS IS JUST FOR TESTING, MY TWO TEST FILE HAVE NON-MOTOR OUTPUTS ON CHANNELS 8 AND 9
            ' THIS BIT JUST DEBUGS WHAT THEIR RESULTS END UP AT.
            If Log_RCOU_Ch8 / Log_RCOU_Count * 100 > RCOU_MaxNonMotorChannel1 Then
                RCOU_MaxNonMotorChannel1 = Log_RCOU_Ch8 / Log_RCOU_Count * 100
            End If
            If Log_RCOU_Ch9 / Log_RCOU_Count * 100 > RCOU_MaxNonMotorChannel2 Then
                RCOU_MaxNonMotorChannel2 = Log_RCOU_Ch9 / Log_RCOU_Count * 100
            End If
            Debug.Print("Non Motor Channel 8 Max % = " & RCOU_MaxNonMotorChannel1)
            Debug.Print("Non Motor Channel 9 Max % = " & RCOU_MaxNonMotorChannel2)

            ' DEBUG CODE, SHOULD NOT BE DEPLOYED.
            ' USED TO TRAP IF THIS CODE SWAPS THE NUMBER OF MOTORS FOUND DURING THE ANALYSIS.
            ' SET TO THE NUMBER OF MOTORS EXPECTED AND SET A BREAKPOINT, IF ALL IS WELL IT WILL NOT STOP.
            'If APM_No_Motors <> 6 Then
            '    Debug.Print("Motor Detection Code differs between results")
            'End If

            ' Ensure we have enough data to make this assesment, otherwise set APM_No_Motors to 0
            If Log_RCOU_Count < 10 Then
                APM_No_Motors = 0
                Debug.Print(APM_No_Motors & " Motors will be used due to lack of data")
            End If

            Debug.Print(APM_No_Motors & " Motors will be used.")
            Debug.Print(Log_RCOU_Count & " RCOU Data Lines.")
            Debug.Print(Log_RCOU_Count & "")

            'sWatch.Stop() ': Debug.Print(sWatch.Elapsed.ToString)
            'myStopWatchTimer = myStopWatchTimer + sWatch.Elapsed.TotalMilliseconds
            'Debug.Print(myStopWatchTimer & "ms")

        End If

    End Sub

End Module
