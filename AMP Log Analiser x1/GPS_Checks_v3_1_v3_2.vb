Module modGPS_Checks_v3_1_v3_2

    Public Sub GPS_Checks_v3_1_v3_2()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE HAS BEEN VALIDATED AS THE CORRECT TYPE !!!

        'An IMU value should have only 7 pieces of numeric data!

        'v3.2 - FMT, 130, 45, GPS, BIHBcLLeeEefI, Status,TimeMS,Week,NSats,HDop,Lat,Lng,RelAlt,Alt,Spd,GCrs,VZ,T


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
            Call GPS_MainAnalysis()
        End If

    End Sub

End Module
