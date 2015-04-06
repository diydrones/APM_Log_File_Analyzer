Imports System.Deployment.Application
Imports System.Net

Module modOther_Functions

    Function GetCurrentDeploymentCurrentVersion() As String
        Dim CurrentVersion As String = ""
        CurrentVersion = System.Reflection.Assembly.GetExecutingAssembly.ToString

        'if running the deployed application, you can get the version
        '  from the ApplicationDeployment information. If you try
        '  to access this when you are running in Visual Studio, it will not work.
        If (ApplicationDeployment.IsNetworkDeployed) Then
            CurrentVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
        Else
            If CurrentVersion <> "" Then
                CurrentVersion = System.Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString

            End If
        End If
        GetCurrentDeploymentCurrentVersion = CurrentVersion
    End Function

    Public Function CheckAddress(ByVal URL As String) As Boolean
        Try
            Dim request As WebRequest = WebRequest.Create(URL)
            Dim response As WebResponse = request.GetResponse()
            If response.ContentLength > 300 Then
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

End Module
