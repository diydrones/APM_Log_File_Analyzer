Imports System.Deployment.Application
Imports System.Net

Module modOther_Functions

    Public Function GetCurrentDeploymentCurrentVersion() As String
        Dim CurrentVersion As String = Reflection.Assembly.GetExecutingAssembly.ToString

        '  If running the deployed application, you can get the version
        '  from the ApplicationDeployment information. If you try
        '  to access this when you are running in Visual Studio, it will not work.
        If (ApplicationDeployment.IsNetworkDeployed) Then
            CurrentVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
        Else
            If CurrentVersion <> "" Then
                CurrentVersion = Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString
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


    Public Function IsDeveloping() As Boolean
        ' Because under Windows XP the installer does not work then we have to create the folder
        ' and just run the .exe, this means that .exe is not "deployed" just as when I am testing.
        ' The result is that anything I do not want to run during development, like checking for updates
        ' does not execute for Windows XP users.
        ' The code has been written so I can remove all the "deployed" checks, instead I will check the
        ' execution path. If this contains "GitHub" then I'll assume we are developing and activate a
        ' TESTING LABEL"
        Dim directory As String = UCase(My.Application.Info.DirectoryPath)
        If InStr(directory, "GITHUB") > 0 Then
            IsDeveloping = True
        Else
            IsDeveloping = False
        End If
        Debug.Print("IsDeveloping = " & IsDeveloping)
    End Function

End Module
