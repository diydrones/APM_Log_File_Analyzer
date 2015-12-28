Module modDetectMotors_v3_2_v3_3

    Public Sub DetectMotors()
        ' The function was added because with original basic code I was getting 7 motors for my Hexa.
        ' The reason was (i believe) that the "Reacts" were connected to channel 8, because the old
        ' code was detecting changes on this channel it was considering this to be a motor.
        ' This new code looks at motors 1 and 3 (minimum rquired to fly). It works out the min and max
        ' values on these motors at each log dataline. If any other channel is within the same
        ' min max limits then it is considered a potential motor. If any of the channels 4 - 12 fall
        ' withing these min max limits more than "MotorDetectionThreshold" of the flight time it 
        ' is considered to be a motor output.
        ' I had to added a little more complexity, for example, offset and only detecting  when
        ' channel 1 is NOT between 1350 And 1650 made a good differnce with my test file.

        ' Pix V3.2.1 - Motor Detection Hexa with Ch8 Active.log
        ' Testing:
        ' Offset%    Ignore        Ch4%   Ch8%    %Diff
        '   0           X           32     16      49 
        '  10           X           42     20      52
        '  20           X           50     24      53
        '  20       1350-1650       76     14             -- Best Option with my test file
        '  20       1400-1600       54     12
        '  20       1455-1550       51     16
        ' I also tried only counting nut that was not a good idea.
        ' The code only adds 13ms to the 1st pass.


        Dim MotorDetectionThreshold As Integer = 30     ' the % an output must be within the same limits as 
        '                                               ' channels 1 to 3 to be considered as a motor.
        Dim MotorDetectionOffset As Integer = 20        ' the % that will be deducted or add to the min max values
        '                                               ' this increases the detection of a true motor.
        Dim MotorIgnoreMin As Integer = 1350            ' If Ch1 is within this range then we check the other channels
        Dim MotorIgnoreMax As Integer = 1650            ' If Ch1 is within this range then we check the other channels

        ' v3.2 - FMT, 134, 31, RCOU, Ihhhhhhhhhhhh, TimeMS,Ch1,Ch2,Ch3,Ch4,Ch5,Ch6,Ch7,Ch8,Ch9,Ch10,Ch11,Ch12

        ' If Motor 1 is not in the correct range "flying" then ignore.
        Dim SkipThis As Boolean = False

        ' Do we need to check?
        If (DataArray(2) >= MotorIgnoreMin And DataArray(2) <= MotorIgnoreMax) Then SkipThis = True

        If SkipThis = False Then

            'Dim sWatch As System.Diagnostics.Stopwatch = New System.Diagnostics.Stopwatch() : sWatch.Start()

            Dim Ch1 As Integer = DataArray(2)
            Dim Ch2 As Integer = DataArray(3)
            Dim Ch3 As Integer = DataArray(4)
            Dim Ch4 As Integer = DataArray(5)
            Dim Ch5 As Integer = DataArray(6)
            Dim Ch6 As Integer = DataArray(7)
            Dim Ch7 As Integer = DataArray(8)
            Dim Ch8 As Integer = DataArray(9)
            Dim Ch9 As Integer = DataArray(10)
            Dim Ch10 As Integer = DataArray(11)
            Dim Ch11 As Integer = DataArray(12)
            Dim Ch12 As Integer = DataArray(13)

            ' Calculate the Min between Ch1 and Ch3
            Log_RCOU_MinCh1toCh3 = Ch1
            If Ch2 < Log_RCOU_MinCh1toCh3 Then Log_RCOU_MinCh1toCh3 = Ch2
            If Ch3 < Log_RCOU_MinCh1toCh3 Then Log_RCOU_MinCh1toCh3 = Ch3

            ' Calculate the Max between Ch1 and Ch3
            Log_RCOU_MaxCh1toCh3 = Ch1
            If Ch2 > Log_RCOU_MaxCh1toCh3 Then Log_RCOU_MaxCh1toCh3 = Ch2
            If Ch3 > Log_RCOU_MaxCh1toCh3 Then Log_RCOU_MaxCh1toCh3 = Ch3
            'Debug.Print("Ch1 to Ch3 Min -- Max " & Log_RCOU_MinCh1toCh3 & " -- " & Log_RCOU_MaxCh1toCh3)

            ' Apply the Min Max offsets.
            'Debug.Print("Log_RCOU_MinCh1toCh3 = " & Log_RCOU_MinCh1toCh3)
            Log_RCOU_MinCh1toCh3 = Log_RCOU_MinCh1toCh3 - MotorDetectionOffset%
            Log_RCOU_MaxCh1toCh3 = Log_RCOU_MaxCh1toCh3 + MotorDetectionOffset%

            'Debug.Print("Log_RCOU_MinCh1toCh3 = " & Log_RCOU_MinCh1toCh3)

            ' Check which other channels are within the Min and Max range and increment that by one.
            If Ch4 >= Log_RCOU_MinCh1toCh3 And Ch4 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch4 += 1
            If Ch5 >= Log_RCOU_MinCh1toCh3 And Ch5 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch5 += 1
            If Ch6 >= Log_RCOU_MinCh1toCh3 And Ch6 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch6 += 1
            If Ch7 >= Log_RCOU_MinCh1toCh3 And Ch7 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch7 += 1
            If Ch8 >= Log_RCOU_MinCh1toCh3 And Ch8 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch8 += 1
            If Ch9 >= Log_RCOU_MinCh1toCh3 And Ch9 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch9 += 1
            If Ch10 >= Log_RCOU_MinCh1toCh3 And Ch10 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch10 += 1
            If Ch11 >= Log_RCOU_MinCh1toCh3 And Ch11 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch11 += 1
            If Ch12 >= Log_RCOU_MinCh1toCh3 And Ch12 <= Log_RCOU_MaxCh1toCh3 Then Log_RCOU_Ch12 += 1

            ' Increment the RCOU Counter
            Log_RCOU_Count += 1
            'Debug.Print("RCOU_Count = " & Log_RCOU_Count)

            ' Calculate the potential number of motors so far.
            APM_No_Motors = 3
            If Log_RCOU_Ch4 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If Log_RCOU_Ch5 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If Log_RCOU_Ch6 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If Log_RCOU_Ch7 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If Log_RCOU_Ch8 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If Log_RCOU_Ch9 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If Log_RCOU_Ch10 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If Log_RCOU_Ch11 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1
            If Log_RCOU_Ch12 / Log_RCOU_Count * 100 > MotorDetectionThreshold Then APM_No_Motors += 1


            'Debug.Print("Ch4 = " & Log_RCOU_Ch4 / Log_RCOU_Count * 100 & "%")
            'Debug.Print("Ch5 = " & Log_RCOU_Ch5 / Log_RCOU_Count * 100 & "%")
            'Debug.Print("Ch6 = " & Log_RCOU_Ch6 / Log_RCOU_Count * 100 & "%")
            'Debug.Print("Ch7 = " & Log_RCOU_Ch7 / Log_RCOU_Count * 100 & "%")
            'Debug.Print("Ch8 = " & Log_RCOU_Ch8 / Log_RCOU_Count * 100 & "%")
            'Debug.Print("Ch9 = " & Log_RCOU_Ch9 / Log_RCOU_Count * 100 & "%")
            'Debug.Print("Ch10 = " & Log_RCOU_Ch10 / Log_RCOU_Count * 100 & "%")
            'Debug.Print("Ch11 = " & Log_RCOU_Ch11 / Log_RCOU_Count * 100 & "%")
            'Debug.Print("Ch12 = " & Log_RCOU_Ch12 / Log_RCOU_Count * 100 & "%")
            'Debug.Print("Motor Detection thinks " & APM_No_Motors & " Motors")

            'sWatch.Stop() ': Debug.Print(sWatch.Elapsed.ToString)
            'myStopWatchTimer = myStopWatchTimer + sWatch.Elapsed.TotalMilliseconds
            'Debug.Print(myStopWatchTimer & "ms")

        End If


    End Sub

End Module
