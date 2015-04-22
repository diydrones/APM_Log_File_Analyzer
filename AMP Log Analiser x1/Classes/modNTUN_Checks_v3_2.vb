Module modNTUN_Checks_v3_2
    Public Sub NTUN_Checks_v3_2()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE HAS BEEN VALIDATED AS THE CORRECT TYPE !!!

        If ReadFileResilienceCheck(11) = True Then
            'FMT, 5, 47, NTUN, Iffffffffff, TimeMS,DPosX,DPosY,PosX,PosY,DVelX,DVelY,VelX,VelY,DAccX,DAccY

            'Debug.Print("NTUN Data Detected...")

            '############################
            '### V3.2 totally changed ###
            '############################

            Call NTUN_MainAnalysis()
        End If



    End Sub
End Module
