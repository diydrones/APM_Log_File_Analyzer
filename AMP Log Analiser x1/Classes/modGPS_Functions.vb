Module modGPS_Functions
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

    Public Function Degree2Radius(ByVal deg As Double) As Double
        Return (deg * Math.PI / 180.0)
    End Function

    Public Function Radius2Degree(ByVal rad As Double) As Double
        Return rad / Math.PI * 180.0
    End Function





    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Dim Distance As Double = 0
    '    Dim Course As Double = 0
    '    Dim Source As wptType
    '    Dim Destination As wptType
    '    Source.lat = 56
    '    Source.lon = 12
    '    Destination.lat = 56
    '    Destination.lon = 11
    '    GetCourseAndDistance(Source, Destination, Course, Distance)
    '    MsgBox(Course)
    'End Sub


    Public Function GetCourseAndDistance(ByVal GPSlat1 As Double, ByVal GPSlon1 As Double, ByVal GPSlat2 As Double, ByVal GPSlon2 As Double) As Integer
        Dim dist As Double = 0
        Dim course As Double = 0

        ' convert latitude and longitude to radians
        Dim lat1 As Double = DegreesToRadians(CDbl(GPSlat1))
        Dim lon1 As Double = DegreesToRadians(CDbl(GPSlon1))
        Dim lat2 As Double = DegreesToRadians(CDbl(GPSlat2))
        Dim lon2 As Double = DegreesToRadians(CDbl(GPSlon2))

        ' compute latitude and longitude differences
        Dim dlat As Double = lat2 - lat1
        Dim dlon As Double = lon2 - lon1

        Dim distanceNorth As Double = dlat
        Dim distanceEast As Double = dlon * Math.Cos(lat1)

        ' compute the distance in radians
        dist = Math.Sqrt(distanceNorth * distanceNorth + distanceEast * distanceEast)

        ' and convert the radians to meters
        dist = RadiansToMeters(dist)

        ' add the elevation difference to the calculation
        'Dim dele As Double = CDbl(pt2.ele - pt1.ele)
        'dist = Math.Sqrt(dist * dist + dele * dele)

        ' compute the course
        course = Math.Atan2(distanceEast, distanceNorth) Mod (2 * Math.PI)
        course = RadiansToDegrees(course)
        If course < 0 Then
            course += 360
        End If
        GetCourseAndDistance = course
    End Function

    Function DegreesToRadians(ByVal degrees As Double) As Double
        Return degrees * Math.PI / 180.0
    End Function

    Function RadiansToDegrees(ByVal radians As Double) As Double
        Return radians * 180.0 / Math.PI
    End Function

    Function RadiansToNauticalMiles(ByVal radians As Double) As Double
        ' There are 60 nautical miles for each degree
        Return radians * 60 * 180 / Math.PI
    End Function

    Function RadiansToMeters(ByVal radians As Double) As Double
        ' there are 1852 meters in a nautical mile
        Return 1852 * RadiansToNauticalMiles(radians)
    End Function

End Module
