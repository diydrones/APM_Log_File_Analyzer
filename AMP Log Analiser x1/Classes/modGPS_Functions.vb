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

End Module
