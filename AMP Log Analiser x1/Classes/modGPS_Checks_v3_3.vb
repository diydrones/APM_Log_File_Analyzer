Module modGPS_Checks_v3_3


    Public Sub GPS_Checks_v3_3()

        'This part only check the correct data structure of the data line
        'and prepares the variables ready for the MainAnalysis which
        'is called directly after by this code.

        ' THIS CODE MUST ONLY BE CALLED AFTER THE DATA LINE HAS BEEN VALIDATED AS THE CORRECT TYPE !!!

        'An IMU value should have only 7 pieces of numeric data!

        'v3.2 - FMT, 130, 45, GPS, BIHBcLLeeEefI, Status,TimeMS,Week,NSats,HDop,Lat,Lng,RelAlt,Alt,Spd,GCrs,VZ,T
        'v3.3 - FMT, 130, 50, GPS, QBIHBcLLeeEefB, TimeUS,Status,GMS,GWk,NSats,HDop,Lat,Lng,RAlt,Alt,Spd,GCrs,VZ,U

        If ReadFileResilienceCheck(14) = True Then
            Log_GPS_Status = Val(DataArray(2))
            Log_GPS_TimeMS = Val(DataArray(3))
            Log_GPS_Week = Val(DataArray(4))
            Log_GPS_NSats = Val(DataArray(5))
            Log_GPS_HDop = Val(DataArray(6))
            Log_GPS_Lat = Val(DataArray(7))
            Log_GPS_Lng = Val(DataArray(8))
            Log_GPS_Alt = Val(DataArray(10))
            Log_GPS_Spd = Val(DataArray(11))
            Log_GPS_GCrs = Val(DataArray(12))
            Call GPS_MainAnalysis()
        End If

    End Sub


End Module
