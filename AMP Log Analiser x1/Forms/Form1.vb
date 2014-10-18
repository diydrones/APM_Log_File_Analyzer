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

        frmMainFormHandle = Me.Handle()

        If FileOpened = True Then
            btnAnalyze.Visible = True
            picClickButton.Visible = False
        ElseIf FileOpened = False Then
            btnAnalyze.Visible = False
            picClickButton.Visible = True
        End If

        'loads the form position

        'This has been commeted out as it does not work so well with the Anchor method.
        'RestorePosition(Me, "PMForm")

        ' Sets the culture to English GB
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
        ' Sets the UI culture to English GB
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-GB")

        'Setup the initial Screen Size so the anchor can work correctly.
        Me.Location = New Point(0, 0) : Me.Size = New Point(1022, 733)
        richtxtLogAnalysis.Location = New Point(0, 0) : richtxtLogAnalysis.Size = New Point(781, 602)
        chartVibrations.Location = New Point(3, 3) : chartVibrations.Size = New Point(794, 691)
        chartPowerRails.Location = New Point(3, 3) : chartPowerRails.Size = New Point(794, 691)
        picClickButton.Location = New Point(82, 10) : picClickButton.Size = New Point(261, 66)
        lblAccXY.Location = New Point(92, 29)
        lblXY_Acceptable.Location = New Point(450, 29)
        lblAccZ.Location = New Point(92, 180)
        lblAccZ_Acceptable.Location = New Point(460, 180)
        lblAltitude.Location = New Point(92, 333)
        lblAbove.Location = New Point(328, 333)
        lblVcc.Location = New Point(86, 27)
        lblVolts.Location = New Point(86, 188)
        lblAmps.Location = New Point(86, 331)
        lblThrust.Location = New Point(86, 470)
        lblOSD.Location = New Point(650, 60)




        'Set up the panels, we leave them small in the WYSIWYG editor for easy of use.
        panAnalysis.Location = New Point(222, 87) : panAnalysis.Size = New Point(784, 602) : panAnalysis.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom Or AnchorStyles.Top Or AnchorStyles.Right
        panVibrations.Location = New Point(222, 87) : panVibrations.Size = New Point(784, 602) : panVibrations.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom Or AnchorStyles.Top Or AnchorStyles.Right
        panPowerRails.Location = New Point(222, 87) : panPowerRails.Size = New Point(784, 602) : panPowerRails.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom Or AnchorStyles.Top Or AnchorStyles.Right
        panAnalysisButtons.Location = New Point(-9, 87) : panAnalysisButtons.Size = New Point(225, 602) : panAnalysisButtons.Anchor = AnchorStyles.Left Or AnchorStyles.Top
        panGraphButtons.Location = New Point(-9, 87) : panGraphButtons.Size = New Point(225, 602) : panGraphButtons.Anchor = AnchorStyles.Left Or AnchorStyles.Top
        panHelpButtons.Location = New Point(714, 6) : panHelpButtons.Size = New Point(280, 75) : panHelpButtons.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.panPowerRails.Controls.Add(chartPowerRails)
        Me.panVibrations.Controls.Add(chartVibrations)
        Me.panAnalysis.Controls.Add(richtxtLogAnalysis)
        Me.panVibrations.Controls.Add(lblAccXY) : lblAccXY.BringToFront()
        Me.panVibrations.Controls.Add(lblAccZ) : lblAccZ.BringToFront()
        Me.panVibrations.Controls.Add(lblAltitude) : lblAltitude.BringToFront()
        Me.panVibrations.Controls.Add(lblAbove) : lblAbove.BringToFront()
        Me.panVibrations.Controls.Add(lblXY_Acceptable) : lblXY_Acceptable.BringToFront()
        Me.panVibrations.Controls.Add(lblAccZ_Acceptable) : lblAccZ_Acceptable.BringToFront()
        Me.panPowerRails.Controls.Add(lblVcc) : lblVcc.BringToFront()
        Me.panPowerRails.Controls.Add(lblVolts) : lblVolts.BringToFront()
        Me.panPowerRails.Controls.Add(lblAmps) : lblAmps.BringToFront()
        Me.panPowerRails.Controls.Add(lblThrust) : lblThrust.BringToFront()
        Me.panPowerRails.Controls.Add(lblOSD) : lblOSD.BringToFront()



        'Set up the resizeing anchors
        richtxtLogAnalysis.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        chartVibrations.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        chartPowerRails.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        lblOSD.Anchor = AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
        'lblOSD.Dock = DockStyle.Right Or DockStyle.Top Or DockStyle.Bottom
        lblXY_Acceptable.Anchor = AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
        lblAccZ_Acceptable.Anchor = AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
        lblAbove.Anchor = AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom


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

            'Update the available button on the window.
            btnAnalyze.Visible = False
            btnGraphs.Visible = False
            btnCopyText.Visible = False
            btnParameters.Visible = False

            'Clear down all the old data.
            richtxtLogAnalysis.Clear()
            frmParameters.lstboxParameters.Items.Clear()
            'chartVibrations.Series(0).Points.Clear()
            'chartPowerRails.Series(0).Points.Clear()
            chartPowerRails.Series("Vcc").Points.Clear()
            chartPowerRails.Series("Volts").Points.Clear()
            chartPowerRails.Series("Amps").Points.Clear()
            chartPowerRails.Series("Thrust").Points.Clear()
            chartPowerRails.Series("VccLowLine").Points.Clear()
            chartPowerRails.Series("VccHighLine").Points.Clear()
            chartPowerRails.Series("VccOSDLine").Points.Clear()

            lblEsc.Visible = True
            Call ReadFile(strLogPathFileName)
            lblEsc.Visible = False

            'Update the available button on the window.
            btnGraphs.Visible = True
            btnCopyText.Visible = True
            btnParameters.Visible = True


        Else
            MsgBox("Please Select a Valid APM Log File first!", MsgBoxStyle.Exclamation & vbOKOnly, "APM Log Error")
        End If
    End Sub
    Private Sub btnCopyText_Click(sender As Object, e As EventArgs) Handles btnCopyText.Click
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

    Private Sub btnAnalysis_Click(sender As Object, e As EventArgs) Handles btnAnalysis.Click
        Call ButtonsCheckBoxes_Visible(True)
        Call ButtonsCharting_Visible(False)
        Call Chart_PowerRails_Visible(False)
        Call Chart_Vibrations_Visible(False)
    End Sub

    Private Sub btnGraphs_Click(sender As Object, e As EventArgs) Handles btnGraphs.Click
        Call ButtonsCheckBoxes_Visible(False)
        If Me.chartVibrations.Series("AccZ").Points.Count = 0 Then
            Me.chartVibrations.ChartAreas("AccZ").Visible = False
        End If
        Call ButtonsCharting_Visible(True)
    End Sub

    Private Sub btnPowerChart_Click(sender As Object, e As EventArgs) Handles btnPowerChart.Click
        Call Chart_Vibrations_Visible(False)
        Call Chart_PowerRails_Visible(True)
    End Sub

    Private Sub btnVibrationChart_Click(sender As Object, e As EventArgs) Handles btnVibrationChart.Click
        Call Chart_Vibrations_Visible(True)
        Call Chart_PowerRails_Visible(False)
    End Sub


    Private Sub frmMainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        frmParameters.Close()
    End Sub

    Private Sub btnParameters_Click(sender As Object, e As EventArgs) Handles btnParameters.Click
        Call ShowParametersForm()
    End Sub





End Class

