Imports System.IO
Imports System.Text.RegularExpressions
Module modUpdate

    Function AutoUpdate(ByVal CurrentVersion As String) As Integer
        '### Note: This has all changed to make the Updater Program push the file changes
        '###    instead of the Log_Analyzer pulling them from the FTP.

        'The function will check an FTP website to see if there are any updates for a program.
        'If found it will download the updated program to the local machine. Then it will download a
        'program called UpdaterProgram.exe (used to update the folders on the local machine). 
        'Once we have these program is will create a file on the local machine called 
        'Update.txt, this file contains all the information like version numbers and installation
        'paths that will be required by the UpdaterProgram. Once these are in place the
        'APM_Log_Analiser program will pass control to the UpdaterProgram and then close down.
        'The UpdaterProgram will install the new APM Log Analiser.exe and then start the
        'new version.

        'The Users CurrentVersion must be passed in the format "v0.0"
        'The function will return:
        '   False = Failed to execute the code successfully
        '   True = Code executed successfully but program did not require updating.
        '   99 = Code executed successfully and a new update was downloaded as required.

        'The website must contain three files:
        '   APM Log File Analiser.exe = The latest program .exe file.
        '   UpdaterProgram.exe = The lastest updater program .exe file.
        '   Versions.txt = Must contain a list of programs and versions available.

        'Versions.txt formatting (create in notepad):
        '   [ProgramName]=[V?.?]=[DownloadLink]
        '   [ProgramName]=[V?.?]=[DownloadLink]
        '   [ProgramName]=[V?.?]=[DownloadLink]
        '   etc.

        'Versions.txt Example:
        'APM Log File Analiser.exe=v1.0=ftp://bionicbone.sshcs.com/APM Log File Analiser.exe
        'Avitar.exe=v1.0=ftp://bionicbone.sshcs.com/UpdateAvitar.exe
        'ThisProgram.exe=v1.0=ftp://bionicbone.sshcs.com/Updates with this name for example.exe


        'Delcare the AutoUpdate Variables
        Try
            Debug.Print("Checking for updates...")

            Debug.Print("ProgramInstallationFolder: " & ProgramInstallationFolder)
            'Debug.Print("TempProgramInstallationFolder " & TempProgramInstallationFolder)

            If CurrentVersion = "" Then CurrentVersion = "v9.9.9.9" 'if the function is called incorrectly, force an update!
            AutoUpdate = False                                          'Set as function failed
            Dim ProgramName As String = "APM Log File Analiser.exe"     'This program we want to update.
            Dim UpdaterProgramName As String = "UpdaterProgram.exe"     'This program we will use to update the main program.
            Dim UpdateToLocaleFolder As String = "C:\Temp\"
            Dim SiteName As String = "guq;##cjpojdcpof~ttidt~dpn#"      'ftp://bionicbone.sshcs.com/
            Dim UserName As String = "cjpojdcpof"                       'bionicbone
            Dim Password As String = "s:8yu$S3b[gF"                     'r97xt#R2aZfE
            Dim VersionFileName As String = "Versions.txt"
            Dim GetVer As String = ""
            Dim GetVerLink As String = ""
            Dim GetUpd As Integer = 0

            'Debug.Print(DecryptData(SiteName))
            'Debug.Print(DecryptData(UserName))
            'Debug.Print(DecryptData(Password))

            'Debug.Print("Update Server: " & SiteName)
            Debug.Print("Program Name: " & ProgramName)
            Debug.Print("Current User Version: " & MyCurrentVersionNumber)

            Debug.Print("Opening FTP update Connection...")
            Dim WebRequest As System.Net.FtpWebRequest = System.Net.FtpWebRequest.Create(DecryptData(SiteName) & VersionFileName)
            Debug.Print("Requesting file: " & VersionFileName)
            WebRequest.Credentials = New Net.NetworkCredential(DecryptData(UserName), DecryptData(Password))
            Debug.Print("Waiting for a response...")
            Dim WebResponse As System.Net.FtpWebResponse = WebRequest.GetResponse
            Dim stream1 As System.IO.StreamReader = New System.IO.StreamReader(WebResponse.GetResponseStream())
            Dim ReadSource As String = stream1.ReadToEnd
            'Debug.Print(VersionFileName & " contents: " & ReadSource)
            Dim Regex As New System.Text.RegularExpressions.Regex(ProgramName & "=v(\d+).(\d+).(\d+).(\d+)=(.*?).exe")
            Debug.Print("Using Regex to find Mastches: " & ProgramName)
            Dim matches As MatchCollection = Regex.Matches(ReadSource)
            Debug.Print("Found: " & matches.Count & ", program is known to server!")

            For Each match As Match In matches
                Dim RegSplit() As String = Split(ReadSource.ToString, "=")
                Debug.Print("Getting Current Server Version for Match: " & match.Index + 1 & "...")
                GetVer = RegSplit(1)
                GetVerLink = RegSplit(2)
                Debug.Print("Current Server Version: " & GetVer)
                'Debug.Print("Current Server Link: " & GetVerLink)
            Next

            Debug.Print("Checking versions...")
            If GetVer > MyCurrentVersionNumber Then
                Debug.Print("Update is available!")

                Debug.Print("Updates are currently forced due to developments in progress!")

                'Close current connections in case FTP will only allow one connection!
                Debug.Print("Closing current update FTP connection...")
                stream1.Close()
                WebResponse.Close()

                'Prepare to Get the updated program file via FTP.
                Dim buffer(1023) As Byte ' Allocate a read buffer of 1kB size
                Dim output As IO.Stream ' A file to save response
                Dim bytesIn As Integer = 0 ' Number of bytes read to buffer
                Dim totalBytesIn As Integer = 0 ' Total number of bytes received (= filesize)

                ''Prepare new FTP connection to Get the required file.
                'Debug.Print("Preparing a new FTP Download connection...")
                'Debug.Print("Requesting file: " & ProgramName & "...")
                'Dim ftpRequest As System.Net.FtpWebRequest = System.Net.FtpWebRequest.Create(DecryptData(SiteName) & ProgramName)
                'ftpRequest.Credentials = New Net.NetworkCredential(DecryptData(UserName), DecryptData(Password))
                'ftpRequest.Method = Net.WebRequestMethods.Ftp.DownloadFile
                'Debug.Print("Waiting for a response...")
                'Dim ftpResponse As System.Net.FtpWebResponse = ftpRequest.GetResponse
                'Debug.Print("Found FTP File.")

                ''Need to check local folder exists, if not create it.
                'Debug.Print("Preparing to copy file to local machine...")
                'Debug.Print("Checking for the local folder exists: " & UpdateToLocaleFolder)
                'If Directory.Exists(UpdateToLocaleFolder) Then
                '    Debug.Print("Found: " & UpdateToLocaleFolder & ", no further action required.")
                'Else
                '    Debug.Print("Not Found: " & UpdateToLocaleFolder)
                '    Debug.Print("Creating: " & UpdateToLocaleFolder)
                '    Directory.CreateDirectory(UpdateToLocaleFolder)
                '    Debug.Print("Success!")
                'End If

                ''Need to check that the file we are about to create does not already exist.
                'Debug.Print("Checking previous update does not still exist: " & UpdateToLocaleFolder & ProgramName)
                'If File.Exists(UpdateToLocaleFolder & ProgramName) Then
                '    Debug.Print("Found: " & UpdateToLocaleFolder & ProgramName)
                '    Debug.Print("Attempting to delete old update file: " & UpdateToLocaleFolder & ProgramName)
                '    File.Delete(UpdateToLocaleFolder & ProgramName)
                '    Debug.Print("Success!")
                'Else
                '    Debug.Print("Not Found: " & UpdateToLocaleFolder & ProgramName & ", no further action required.")
                'End If

                '' Write the content to the output file
                'Debug.Print("Creating Local Update File: " & UpdateToLocaleFolder & ProgramName)
                'output = System.IO.File.Create(UpdateToLocaleFolder & ProgramName)
                'Debug.Print("Success, at least by name, file is still empty!")

                'Debug.Print("Opening the stream...")
                'Dim stream2 As System.IO.Stream = ftpRequest.GetResponse.GetResponseStream

                'Debug.Print("Writing file contents...")
                'bytesIn = 1 ' Set initial value to 1 to get into loop. We get out of the loop when bytesIn is zero
                'Do Until bytesIn < 1
                '    bytesIn = stream2.Read(buffer, 0, 1024) ' Read max 1024 bytes to buffer and get the actual number of bytes received
                '    If bytesIn > 0 Then
                '        ' Dump the buffer to a file
                '        output.Write(buffer, 0, bytesIn)
                '        ' Calc total filesize
                '        totalBytesIn += bytesIn
                '        ' Show user the filesize
                '        'Label1.Text = totalBytesIn.ToString + " Bytes Downloaded"
                '        Application.DoEvents()
                '    End If
                'Loop
                'Debug.Print("Success!")
                '' Close streams
                'Debug.Print("Closing FTP download connection...")
                'output.Close()
                'stream2.Close()




                'Prepare to Get the updater program file via FTP.
                bytesIn = 0 ' Number of bytes read to buffer
                totalBytesIn = 0 ' Total number of bytes received (= filesize)

                'Prepare new FTP connection to Get the required file.
                Debug.Print("Preparing a new FTP Download connection...")
                Debug.Print("Requesting file: " & ProgramName & "...")
                Dim ftpRequest2 As System.Net.FtpWebRequest = System.Net.FtpWebRequest.Create(DecryptData(SiteName) & UpdaterProgramName)
                ftpRequest2.Credentials = New Net.NetworkCredential(DecryptData(UserName), DecryptData(Password))
                ftpRequest2.Method = Net.WebRequestMethods.Ftp.DownloadFile
                Debug.Print("Waiting for a response...")
                Dim ftpResponse2 As System.Net.FtpWebResponse = ftpRequest2.GetResponse
                Debug.Print("Found FTP File.")

                'Need to check local folder exists, if not create it.
                Debug.Print("Preparing to copy file to local machine...")
                Debug.Print("Checking for the local folder exists: " & UpdateToLocaleFolder)
                If Directory.Exists(UpdateToLocaleFolder) Then
                    Debug.Print("Found: " & UpdateToLocaleFolder & ", no further action required.")
                Else
                    Debug.Print("Not Found: " & UpdateToLocaleFolder)
                    Debug.Print("Creating: " & UpdateToLocaleFolder)
                    Directory.CreateDirectory(UpdateToLocaleFolder)
                    Debug.Print("Success!")
                End If


                'Need to check that the file we are about to create does not already exist.
                'We know the folder already exists from the step above
                Debug.Print("Checking previous update does not still exist: " & UpdateToLocaleFolder & UpdaterProgramName)
                If File.Exists(UpdateToLocaleFolder & UpdaterProgramName) Then
                    Debug.Print("Found: " & UpdateToLocaleFolder & UpdaterProgramName)
                    Debug.Print("Attempting to delete old update file: " & UpdateToLocaleFolder & UpdaterProgramName)
                    File.Delete(UpdateToLocaleFolder & UpdaterProgramName)
                    Debug.Print("Success!")
                Else
                    Debug.Print("Not Found: " & UpdateToLocaleFolder & UpdaterProgramName & ", no further action required.")
                End If

                ' Write the content to the output file
                Debug.Print("Creating Local Update File: " & UpdateToLocaleFolder & UpdaterProgramName)
                output = System.IO.File.Create(UpdateToLocaleFolder & UpdaterProgramName)
                Debug.Print("Success, at least by name, file is still empty!")

                Debug.Print("Opening the stream...")
                Dim stream3 As System.IO.Stream = ftpRequest2.GetResponse.GetResponseStream

                Debug.Print("Writing file contents...")
                bytesIn = 1 ' Set initial value to 1 to get into loop. We get out of the loop when bytesIn is zero
                Do Until bytesIn < 1
                    bytesIn = stream3.Read(buffer, 0, 1024) ' Read max 1024 bytes to buffer and get the actual number of bytes received
                    If bytesIn > 0 Then
                        ' Dump the buffer to a file
                        output.Write(buffer, 0, bytesIn)
                        ' Calc total filesize
                        totalBytesIn += bytesIn
                        ' Show user the filesize
                        'Label1.Text = totalBytesIn.ToString + " Bytes Downloaded"
                        Application.DoEvents()
                    End If
                Loop
                Debug.Print("Success!")
                ' Close streams
                Debug.Print("Closing FTP download connection...")
                output.Close()
                stream3.Close()


                '' ### Updated by KMG to ensure the gLabel.dll is moved to the Local PC.
                ''Prepare to Get the gLabel.dll file via FTP.
                'bytesIn = 0 ' Number of bytes read to buffer
                'totalBytesIn = 0 ' Total number of bytes received (= filesize)

                ''Prepare new FTP connection to Get the required file.
                'Debug.Print("Preparing a new FTP Download connection...")
                'Debug.Print("Requesting file: " & "gLabel.dll" & "...")
                'Dim ftpRequest3 As System.Net.FtpWebRequest = System.Net.FtpWebRequest.Create(DecryptData(SiteName) & "gLabel.dll")
                'ftpRequest3.Credentials = New Net.NetworkCredential(DecryptData(UserName), DecryptData(Password))
                'ftpRequest3.Method = Net.WebRequestMethods.Ftp.DownloadFile
                'Debug.Print("Waiting for a response...")
                'Dim ftpResponse3 As System.Net.FtpWebResponse = ftpRequest3.GetResponse
                'Debug.Print("Found FTP File.")

                ''Need to check that the file we are about to create does not already exist.
                ''We know the folder already exists from the step above
                'Debug.Print("Checking previous update does not still exist: " & UpdateToLocaleFolder & "gLabel.dll")
                'If File.Exists(UpdateToLocaleFolder & "gLabel.dll") Then
                '    Debug.Print("Found: " & UpdateToLocaleFolder & "gLabel.dll")
                '    Debug.Print("Attempting to delete old update file: " & UpdateToLocaleFolder & "gLabel.dll")
                '    File.Delete(UpdateToLocaleFolder & "gLabel.dll")
                '    Debug.Print("Success!")
                'Else
                '    Debug.Print("Not Found: " & UpdateToLocaleFolder & "gLabel.dll" & ", no further action required.")
                'End If

                '' Write the content to the output file
                'Debug.Print("Creating Local Update File: " & UpdateToLocaleFolder & "gLabel.dll")
                'output = System.IO.File.Create(UpdateToLocaleFolder & "gLabel.dll")
                'Debug.Print("Success, at least by name, file is still empty!")

                'Debug.Print("Opening the stream...")
                'Dim stream4 As System.IO.Stream = ftpRequest3.GetResponse.GetResponseStream

                'Debug.Print("Writing file contents...")
                'bytesIn = 1 ' Set initial value to 1 to get into loop. We get out of the loop when bytesIn is zero
                'Do Until bytesIn < 1
                '    bytesIn = stream4.Read(buffer, 0, 1024) ' Read max 1024 bytes to buffer and get the actual number of bytes received
                '    If bytesIn > 0 Then
                '        ' Dump the buffer to a file
                '        output.Write(buffer, 0, bytesIn)
                '        ' Calc total filesize
                '        totalBytesIn += bytesIn
                '        ' Show user the filesize
                '        'Label1.Text = totalBytesIn.ToString + " Bytes Downloaded"
                '        Application.DoEvents()
                '    End If
                'Loop
                'Debug.Print("Success!")
                '' Close streams
                'Debug.Print("Closing FTP download connection...")
                'output.Close()
                'stream4.Close()







                'Create a file that passes all the information required to update the main program
                'to the updater program.
                Debug.Print("Creating an installation file for the updater (Update.txt)...")

                Dim ScriptFileName As String = "Update.txt"
                Debug.Print("Checking previous update does not still exist: " & UpdateToLocaleFolder & ScriptFileName)
                If File.Exists(UpdateToLocaleFolder & ScriptFileName) Then
                    Debug.Print("Found: " & UpdateToLocaleFolder & ScriptFileName)
                    Debug.Print("Attempting to delete old update file: " & UpdateToLocaleFolder & ScriptFileName)
                    File.Delete(UpdateToLocaleFolder & ScriptFileName)
                    Debug.Print("Success!")
                Else
                    Debug.Print("Not Found: " & UpdateToLocaleFolder & ScriptFileName & ", no further action required.")
                End If

                Dim objWriter As StreamWriter = File.CreateText(UpdateToLocaleFolder & ScriptFileName)


                objWriter.WriteLine("APM Log Analysis Updater file")
                objWriter.WriteLine("")
                objWriter.WriteLine("Program Name =" & ProgramName)
                objWriter.WriteLine("Current Version =" & MyCurrentVersionNumber)
                objWriter.WriteLine("Update Version =" & GetVer)
                objWriter.WriteLine("")
                objWriter.WriteLine("Installation Folder =" & ProgramInstallationFolder & "\")
                objWriter.WriteLine("Updater Folder =" & UpdateToLocaleFolder)
                objWriter.Flush()
                objWriter.Close()

                AutoUpdate = 99

                'Call the updater program to do its stuff
                Debug.Print("Start the Updater Program: " & UpdateToLocaleFolder & UpdaterProgramName)
                System.Diagnostics.Process.Start(UpdateToLocaleFolder & UpdaterProgramName)

                Debug.Print("Close the APM Log Anayliser Program so it can update...")
                Debug.Print(vbNewLine)
                frmMainForm.Close()

            Else
                'Close current connections!
                Debug.Print("Program does not require an update.")
                Debug.Print("Closing FTP update connection...")
                stream1.Close()
                WebResponse.Close()
                Debug.Print("Success!")
                AutoUpdate = True
            End If
            Debug.Print("The check for updates has completed successfully")
            Debug.Print(vbNewLine)

        Catch
            AutoUpdate = False
            Debug.Print("An Error Occured in the Update Function")
            Debug.Print("Exit Function")
            Debug.Print(vbNewLine)
            Exit Function
        End Try

    End Function

End Module
