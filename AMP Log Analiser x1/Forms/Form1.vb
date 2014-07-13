Imports System
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Deployment.Application
Imports System.Globalization
Imports System.Threading
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Drawing.Printing


Public Class frmMainForm

#Region "     Form Position Code Start & Constants "
    'Form Position Code Start
    Public Const WM_NCLBUTTONDOWN As Integer = &HA1
    Public Const HTBORDER As Integer = 18
    Public Const HTBOTTOM As Integer = 15
    Public Const HTBOTTOMLEFT As Integer = 16
    Public Const HTBOTTOMRIGHT As Integer = 17
    Public Const HTCAPTION As Integer = 2
    Public Const HTLEFT As Integer = 10
    Public Const HTRIGHT As Integer = 11
    Public Const HTTOP As Integer = 12
    Public Const HTTOPLEFT As Integer = 13
    Public Const HTTOPRIGHT As Integer = 14
    Public Const EM_GETLINECOUNT = &HBA
    Public Const EM_LINESCROLL = &HB6

    Private Sub SavePosition(ByVal frm As Form, ByVal app_name As String)
        SaveSetting(app_name, "Geometry", "WindowState", frm.WindowState)
        If frm.WindowState = FormWindowState.Normal Then
            SaveSetting(app_name, "Geometry", "Left", frm.Left)
            SaveSetting(app_name, "Geometry", "Top", frm.Top)
            SaveSetting(app_name, "Geometry", "Width", frm.Width)
            SaveSetting(app_name, "Geometry", "Height", frm.Height)
        Else
            SaveSetting(app_name, "Geometry", "Left", frm.RestoreBounds.Left)
            SaveSetting(app_name, "Geometry", "Top", frm.RestoreBounds.Top)
            SaveSetting(app_name, "Geometry", "Width", frm.RestoreBounds.Width)
            SaveSetting(app_name, "Geometry", "Height", frm.RestoreBounds.Height)
        End If
    End Sub
    Private Sub RestorePosition(ByVal frm As Form, ByVal app_name As String)
        frm.SetBounds( _
            GetSetting(app_name, "Geometry", "Left", Me.RestoreBounds.Left), _
            GetSetting(app_name, "Geometry", "Top", Me.RestoreBounds.Top), _
            GetSetting(app_name, "Geometry", "Width", Me.RestoreBounds.Width), _
            GetSetting(app_name, "Geometry", "Height", Me.RestoreBounds.Height) _
        )
        Me.WindowState = GetSetting(app_name, "Geometry", "WindowState", Me.WindowState)
    End Sub
    'Form Position Code End


#End Region

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If FileOpened = True Then
            btnAnalyze.Visible = True
            btnVibrations.Visible = True
            PictureBox1.Visible = False
        ElseIf FileOpened = False Then
            btnAnalyze.Visible = False
            btnVibrations.Visible = False
            PictureBox1.Visible = True
        End If


        'loads the form position
        RestorePosition(Me, "PMForm")
        ' Sets the culture to English GB
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
        ' Sets the UI culture to English GB
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-GB")

        'Get the version numbers a run the update if not developing.
        CurrentPublishVersionNumber = GetCurrentDeploymentCurrentVersion()
        lblCurrentVersion.Text = "s:" & CurrentPublishVersionNumber
        lblMyCurrentVersion.Text = "m:" & MyCurrentVersionNumber

        Debug.Print("==========================================================================")
        Debug.Print("========================Starting Program =================================")
        Debug.Print("==========================================================================")
        Debug.Print("ProgramInstallationFolder: " & ProgramInstallationFolder)
        Debug.Print("TempProgramInstallationFolder: " & TempProgramInstallationFolder)
        Debug.Print("ApplicationStartUpPath: " & ApplicationStartUpPath)
        Debug.Print("TempIsDeployed: " & TempIsDeployed)
        Debug.Print("MyCurrentVersionNumber:      " & MyCurrentVersionNumber)
        Debug.Print("CurrentPublishVersionNumber: " & CurrentPublishVersionNumber)
        Debug.Print("==========================================================================")

        Debug.Print(vbNewLine & "Open Form: " & Me.Name)

        If File.Exists("C:\Temp\OpenKKWM.LFA") Then
            Debug.Print("Found an old .LFA file, trying to delete it...")
            File.Delete("C:\Temp\OpenKKWM.LFA")
            Debug.Print("Success!")
        End If

        If (ApplicationDeployment.IsNetworkDeployed) Then
            If MsgBox("Would you like to check for updates now?", vbYesNo) = vbYes Then
                'display the Updating splash screen.
                frmUpdate.Show()
                frmUpdate.Refresh()
                Call AutoUpdate(CurrentPublishVersionNumber)
                frmUpdate.Close()
            End If
        End If

        Call ReadINIfile()

        Debug.Print("Open Form " & Me.Name & " Completed" & vbNewLine)

    End Sub
    Private Sub Form1_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        'saves form position
        SavePosition(Me, "PMForm")
    End Sub


    Private Sub AboutInfo()
        Dim strTempText As String = ""
        strTempText = strTempText & "AMP Log Anaylser is Free to use, but if you" & vbNewLine
        strTempText = strTempText & "find it useful then please send a small donation" & vbNewLine
        strTempText = strTempText & "using the Pay Pal button as every bit helps :)" & vbNewLine
        strTempText = strTempText & vbNewLine
        strTempText = strTempText & "AMP Log Anaylser should not be sold or used for commercial" & vbNewLine
        strTempText = strTempText & "purposes without written permission etc. etc. etc." & vbNewLine
        strTempText = strTempText & vbNewLine
        strTempText = strTempText & "For more support and instructions please visit the" & vbNewLine
        strTempText = strTempText & "RC Groups dedicated thread." & vbNewLine
        strTempText = strTempText & "http://www.rcgroups.com/forums/showthread.php?t=2151318" & vbNewLine
        strTempText = strTempText & vbNewLine
        strTempText = strTempText & "Thank you for your support :)" & vbNewLine
        strTempText = strTempText & "---Version: " & CurrentPublishVersionNumber & vbNewLine
        MsgBox(strTempText, vbInformation & vbOKOnly, "About")
    End Sub

    Private Sub UpdateNow()
        'display the Updating splash screen.
        frmUpdate.Show()
        frmUpdate.Refresh()
        Call AutoUpdate(CurrentPublishVersionNumber)
        frmUpdate.Close()
    End Sub

    Private Sub CopytoClip()

        Dim SelectedText As String = ""
        Dim RCGFormattedText As String = "[CODE][COLOR=""Black""]"
        Dim Character As String = ""
        Dim LineData As String = ""
        Dim DataCounter As Integer = 1
        SelectedText = richtxtLogAnalysis.SelectedText

        If SelectedText = "" Then
            MsgBox("Please select some text first!", MsgBoxStyle.Exclamation & vbOKOnly, "Error")
        End If

        While DataCounter < Len(SelectedText) - 1
            While Character <> Chr(10) And DataCounter < Len(SelectedText) + 1
                Character = Mid(SelectedText, DataCounter, 1)
                Debug.Print(Character)
                LineData = LineData & Character
                DataCounter = DataCounter + 1
            End While

            'work out the special formatting for RCGroups

            Debug.Print("Line Found:" & LineData)

            If InStr(LineData, "WARNING:") Then LineData = "[COLOR=""Red""]" & LineData & "[/COLOR]"
            If InStr(LineData, "ERROR:") Then LineData = "[COLOR=""Red""]" & LineData & "[/COLOR]"
            If InStr(LineData, "Error:") Then LineData = "[COLOR=""Red""]" & LineData & "[/COLOR]"
            If InStr(LineData, "Warning:") Then LineData = "[COLOR=""Orange""]" & LineData & "[/COLOR]"
            If InStr(LineData, "Mode") Then LineData = "[COLOR=""Blue""]" & LineData & "[/COLOR]"
            If InStr(LineData, "Flight Time") Then LineData = "[COLOR=""Blue""]" & LineData & "[/COLOR]"
            If InStr(LineData, "Testing") Then LineData = "[COLOR=""Purple""]" & LineData & "[/COLOR]"
            If InStr(LineData, "Information") Then LineData = "[COLOR=""Green""]" & LineData & "[/COLOR]"
            If InStr(LineData, "***") Then LineData = "[COLOR=""Green""]" & LineData & "[/COLOR]"
            If InStr(LineData, "http://") Then LineData = "[U][URL=""" & LineData & """]" & LineData & "[/URL][/U]"

            RCGFormattedText = RCGFormattedText & LineData
            Character = ""
            LineData = ""
        End While

        'Add the CODE end
        RCGFormattedText = RCGFormattedText & "[/COLOR][/CODE]"
        Clipboard.Clear()
        Clipboard.SetText(RCGFormattedText)

    End Sub

    Private Sub richtxtLogAnalysis_KeyDown(sender As Object, e As KeyEventArgs) Handles richtxtLogAnalysis.KeyDown
        If e.KeyCode = Keys.Escape Then
            ESCPress = True
        End If
    End Sub
    Private Sub richtxtLogAnalysis_LinkClicked_1(sender As Object, e As LinkClickedEventArgs) Handles richtxtLogAnalysis.LinkClicked
        System.Diagnostics.Process.Start(e.LinkText)
    End Sub

    Private Sub btnLoadLog_Click(sender As Object, e As EventArgs) Handles btnLoadLog.Click
        Call SelectFile()
    End Sub
    Private Sub btnAnalyze_Click(sender As Object, e As EventArgs) Handles btnAnalyze.Click
        If strLogPathFileName <> "" Then
            richtxtLogAnalysis.Clear()
            lblEsc.Visible = True
            Call ReadFile(strLogPathFileName)
            lblEsc.Visible = False
        Else
            MsgBox("Please Select a Valid APM Log File first!", MsgBoxStyle.Exclamation & vbOKOnly, "APM Log Error")
        End If
    End Sub
    Private Sub btnVibrations_Click(sender As Object, e As EventArgs) Handles btnVibrations.Click
        Call CopytoClip() 'changed this to its own sub so it can be called via anything except the one button it was assigned to
    End Sub
    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        Call AboutInfo()
    End Sub
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Call UpdateNow()
    End Sub
    Private Sub btnMCB_Click(sender As Object, e As EventArgs) Handles btnMCB.Click

        My.Computer.Audio.Play(My.Resources.MCB, AudioPlayMode.Background)
    End Sub
    Private Sub btnDonate_Click(sender As Object, e As EventArgs) Handles btnDonate.Click
        Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=RZFHAR67833ZQ")

    End Sub

    Private Sub btnTempAnalysis_Click(sender As Object, e As EventArgs) Handles btnTempAnalysis.Click
        Call ButtonsCheckBoxes_Visible(True)
        Call ButtonsCharting_Visible(False)
        Call Chart_PowerRails_Visible(False)
        Call Chart_Vibrations_Visible(False)
    End Sub

    Private Sub btnTempGraphs_Click(sender As Object, e As EventArgs) Handles btnTempGraphs.Click
        Call ButtonsCheckBoxes_Visible(False)
        Call ButtonsCharting_Visible(True)
    End Sub

    Private Sub btnTempPowerChart_Click(sender As Object, e As EventArgs) Handles btnTempPowerChart.Click
        Call Chart_Vibrations_Visible(False)
        Call Chart_PowerRails_Visible(True)
    End Sub

    Private Sub btnTempVibrationChart_Click(sender As Object, e As EventArgs) Handles btnTempVibrationChart.Click
        Call Chart_Vibrations_Visible(True)
        Call Chart_PowerRails_Visible(False)
    End Sub


End Class

