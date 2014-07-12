Imports System.Deployment.Application
Module modOther_Functions

    Function DecryptData(ByVal strText As String) As String
        Dim strReturn As String = ""
        Dim strTempChar As String = ""
        'Debug.Print("Decrypting: " & strText)
        For n = 1 To Len(strText)
            strTempChar = Mid(strText, n, 1)
            If strTempChar <> "#" And strTempChar <> "~" Then
                'Debug.Print(strTempChar)
                'Debug.Print(Asc(strTempChar))
                'Debug.Print(Asc(strTempChar) - 1)
                'Debug.Print(Chr(Asc(strTempChar) - 1))
                strReturn = strReturn & Chr(Asc(strTempChar) - 1)
            ElseIf strTempChar = "#" Then
                strReturn = strReturn & "/"
            Else
                strReturn = strReturn & "."
            End If
        Next
        'Debug.Print("Returning: " & strReturn)
        DecryptData = strReturn
    End Function

    Function EncryptData(ByVal strText As String) As String
        Dim strReturn As String = ""
        Dim strTempChar As String = ""
        'Debug.Print("Encrypting: " & strText)
        For n = 1 To Len(strText)
            strTempChar = Mid(strText, n, 1)
            If strTempChar <> "/" And strTempChar <> "." Then
                'Debug.Print(strTempChar)
                'Debug.Print(Asc(strTempChar))
                'Debug.Print(Asc(strTempChar) + 1)
                'Debug.Print(Chr(Asc(strTempChar) + 1))
                strReturn = strReturn & Chr(Asc(strTempChar) + 1)
            ElseIf strTempChar = "/" Then
                strReturn = strReturn & "#"
            Else
                strReturn = strReturn & "~"
            End If
        Next
        'Debug.Print("Returning: " & strReturn)
        EncryptData = strReturn
    End Function

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

End Module
