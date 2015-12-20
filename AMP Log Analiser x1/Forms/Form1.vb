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
        picClickButton.Location = New Point(82, 10) : picClickButton.Size = New Point(261, 66)
        panAnalysisButtons.Location = New Point(-9, 87) : panAnalysisButtons.Size = New Point(225, 602) : panAnalysisButtons.Anchor = AnchorStyles.Left Or AnchorStyles.Top
        panGraphButtons.Location = New Point(-9, 87) : panGraphButtons.Size = New Point(225, 602) : panGraphButtons.Anchor = AnchorStyles.Left Or AnchorStyles.Top
        panHelpButtons.Location = New Point(714, 6) : panHelpButtons.Size = New Point(280, 75) : panHelpButtons.Anchor = AnchorStyles.Top Or AnchorStyles.Right


        'Setup the Analysis Panel.
        panAnalysis.Location = New Point(222, 87) : panAnalysis.Size = New Point(784, 602) : panAnalysis.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom Or AnchorStyles.Top Or AnchorStyles.Right
        Me.panAnalysis.Controls.Add(richtxtLogAnalysis)
        richtxtLogAnalysis.Location = New Point(0, 0) : richtxtLogAnalysis.Size = New Point(781, 602)
        richtxtLogAnalysis.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right


        'Setup the Vibrations Chart Panel.
        panVibrations.Location = New Point(222, 87) : panVibrations.Size = New Point(784, 602) : panVibrations.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom Or AnchorStyles.Top Or AnchorStyles.Right
        Me.panVibrations.Controls.Add(chartVibrations)
        Me.panVibrations.Controls.Add(lblAccXY) : lblAccXY.BringToFront()
        Me.panVibrations.Controls.Add(lblAccZ) : lblAccZ.BringToFront()
        Me.panVibrations.Controls.Add(lblAltitude) : lblAltitude.BringToFront()
        Me.panVibrations.Controls.Add(lblXY_Acceptable) : lblXY_Acceptable.BringToFront()
        Me.panVibrations.Controls.Add(lblAccZ_Acceptable) : lblAccZ_Acceptable.BringToFront()
        chartVibrations.Location = New Point(3, 3) : chartVibrations.Size = New Point(794, 691)
        lblAccXY.Location = New Point(92, 29)
        lblXY_Acceptable.Location = New Point(450, 29)
        lblAccZ.Location = New Point(92, 180)
        lblAccZ_Acceptable.Location = New Point(460, 180)
        lblAltitude.Location = New Point(92, 333)
        chartVibrations.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        lblXY_Acceptable.Anchor = AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
        lblAccZ_Acceptable.Anchor = AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom


        'Setup the Power Rails Chart Panel.
        panPowerRails.Location = New Point(222, 87) : panPowerRails.Size = New Point(784, 602) : panPowerRails.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom Or AnchorStyles.Top Or AnchorStyles.Right
        Me.panPowerRails.Controls.Add(chartPowerRails)
        Me.panPowerRails.Controls.Add(lblVcc) : lblVcc.BringToFront()
        Me.panPowerRails.Controls.Add(lblVolts) : lblVolts.BringToFront()
        Me.panPowerRails.Controls.Add(lblAmps) : lblAmps.BringToFront()
        Me.panPowerRails.Controls.Add(lblThrust) : lblThrust.BringToFront()
        Me.panPowerRails.Controls.Add(lblOSD) : lblOSD.BringToFront()
        chartPowerRails.Location = New Point(3, 3) : chartPowerRails.Size = New Point(794, 691)
        lblVcc.Location = New Point(86, 27)
        lblVolts.Location = New Point(86, 188)
        lblAmps.Location = New Point(86, 331)
        lblThrust.Location = New Point(86, 470)
        lblOSD.Location = New Point(650, 60)
        chartPowerRails.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        lblOSD.Anchor = AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom


        'Setup the GPS Chart Panel.
        panGPS.Location = New Point(222, 87) : panGPS.Size = New Point(784, 602) : panGPS.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom Or AnchorStyles.Top Or AnchorStyles.Right
        Me.panGPS.Controls.Add(chartGPS)
        chartGPS.Location = New Point(3, 3) : chartGPS.Size = New Point(794, 691)
        chartGPS.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        Me.panGPS.Controls.Add(lblGPSChart1Header) : lblGPSChart1Header.BringToFront()
        lblGPSChart1Header.Location = New Point(86, 5)
        lblGPSChart1Header.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        lblGPSChart1Header.Text = "GPS - Status"
        Me.panGPS.Controls.Add(lblGPSChart2Header) : lblGPSChart2Header.BringToFront()
        lblGPSChart2Header.Location = New Point(86, 150)
        lblGPSChart2Header.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        lblGPSChart2Header.Text = "GPS - HDop"
        Me.panGPS.Controls.Add(lblGPSChart3Header) : lblGPSChart3Header.BringToFront()
        lblGPSChart3Header.Location = New Point(86, 295)
        lblGPSChart3Header.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        lblGPSChart3Header.Text = "GPS - Satellites"
        Me.panGPS.Controls.Add(lblGPSChart4Header) : lblGPSChart4Header.BringToFront()
        lblGPSChart4Header.Location = New Point(86, 440)
        lblGPSChart4Header.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        lblGPSChart4Header.Text = "Ground Speed (m/s)"
        'Add any additional Labels here for this chart.
        Me.panGPS.Controls.Add(lblGPSChart1Status0) : lblGPSChart1Status0.BringToFront()
        lblGPSChart1Status0.Location = New Point(650, 127)
        lblGPSChart1Status0.Anchor = AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
        Me.panGPS.Controls.Add(lblGPSChart1Status1) : lblGPSChart1Status1.BringToFront()
        lblGPSChart1Status1.Location = New Point(650, 89)
        lblGPSChart1Status1.Anchor = AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
        Me.panGPS.Controls.Add(lblGPSChart1Status2) : lblGPSChart1Status2.BringToFront()
        lblGPSChart1Status2.Location = New Point(650, 51)
        lblGPSChart1Status2.Anchor = AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
        Me.panGPS.Controls.Add(lblGPSChart1Status3) : lblGPSChart1Status3.BringToFront()
        lblGPSChart1Status3.Location = New Point(650, 13)
        lblGPSChart1Status3.Anchor = AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
        Me.panGPS.Controls.Add(picGPSTickStraight) : picGPSTickStraight.BringToFront()
        picGPSTickStraight.Location = New Point(3, 22)
        picGPSTickStraight.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom
        Me.panGPS.Controls.Add(picGPSTickDown) : picGPSTickDown.BringToFront()
        picGPSTickDown.Location = New Point(3, 175)
        picGPSTickDown.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom
        Me.panGPS.Controls.Add(picGPSTickUp) : picGPSTickUp.BringToFront()
        picGPSTickUp.Location = New Point(3, 367)
        picGPSTickUp.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom


        'Setup the Attitude Chart Panel.
        panAttitude.Location = New Point(222, 87) : panAttitude.Size = New Point(784, 602) : panAttitude.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom Or AnchorStyles.Top Or AnchorStyles.Right
        Me.panAttitude.Controls.Add(chartAttitude)
        chartAttitude.Location = New Point(3, 3) : chartAttitude.Size = New Point(794, 691)
        chartAttitude.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        Me.panAttitude.Controls.Add(lblAttitudeChart1Header) : lblAttitudeChart1Header.BringToFront()
        lblAttitudeChart1Header.Location = New Point(86, 5)
        lblAttitudeChart1Header.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom
        lblAttitudeChart1Header.Text = "Attitude - Roll"
        Me.panAttitude.Controls.Add(lblAttitudeChart2Header) : lblAttitudeChart2Header.BringToFront()
        lblAttitudeChart2Header.Location = New Point(86, 151)
        lblAttitudeChart2Header.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom
        lblAttitudeChart2Header.Text = "Attitude - Pitch / Speed"
        Me.panAttitude.Controls.Add(lblAttitudeChart3Header) : lblAttitudeChart3Header.BringToFront()
        lblAttitudeChart3Header.Location = New Point(86, 297)
        lblAttitudeChart3Header.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom
        lblAttitudeChart3Header.Text = "Attitude - Yaw / Direction of Travel"
        Me.panAttitude.Controls.Add(lblAttitudeChart4Header) : lblAttitudeChart4Header.BringToFront()
        lblAttitudeChart4Header.Location = New Point(86, 443)
        lblAttitudeChart4Header.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom
        lblAttitudeChart4Header.Text = "Attitude - Climb Rate"
        'Add any additional Labels here for this chart.
        Me.panAttitude.Controls.Add(picAttitudeKey1) : picAttitudeKey1.BringToFront()
        picAttitudeKey1.Location = New Point(529, 5)
        picAttitudeKey1.Anchor = AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
        Me.panAttitude.Controls.Add(picAttitudeKey2) : picAttitudeKey2.BringToFront()
        picAttitudeKey2.Location = New Point(529, 151)
        picAttitudeKey2.Anchor = AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
        Me.panAttitude.Controls.Add(picAttitudeKey3) : picAttitudeKey3.BringToFront()
        picAttitudeKey3.Location = New Point(529, 297)
        picAttitudeKey3.Anchor = AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
        Me.panAttitude.Controls.Add(picAttitudeKey4) : picAttitudeKey4.BringToFront()
        picAttitudeKey4.Location = New Point(529, 443)
        picAttitudeKey4.Anchor = AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
        Me.panAttitude.Controls.Add(picRollRight) : picRollRight.BringToFront()
        picRollRight.Location = New Point(3, 25)
        picRollRight.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom
        Me.panAttitude.Controls.Add(picRollLeft) : picRollLeft.BringToFront()
        picRollLeft.Location = New Point(3, 85)
        picRollLeft.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom
        Me.panAttitude.Controls.Add(picPitchBackward) : picPitchBackward.BringToFront()
        picPitchBackward.Location = New Point(3, 170)
        picPitchBackward.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom
        Me.panAttitude.Controls.Add(picPitchForward) : picPitchForward.BringToFront()
        picPitchForward.Location = New Point(3, 234)
        picPitchForward.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom
        Me.panAttitude.Controls.Add(picCompass) : picCompass.BringToFront()
        picCompass.Location = New Point(0, 316)
        picCompass.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom
        Me.panAttitude.Controls.Add(picCW) : picCW.BringToFront()
        picCW.Location = New Point(3, 390)
        picCW.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom
        Me.panAttitude.Controls.Add(picCCW) : picCCW.BringToFront()
        picCCW.Location = New Point(3, 418)
        picCCW.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom
        Me.panAttitude.Controls.Add(picClimb) : picClimb.BringToFront()
        picClimb.Location = New Point(3, 462)
        picClimb.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom
        Me.panAttitude.Controls.Add(picDescend) : picDescend.BringToFront()
        picDescend.Location = New Point(3, 511)
        picDescend.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom


        'Setup the Travel Chart Panel.
        panTravel.Location = New Point(222, 87) : panTravel.Size = New Point(784, 602) : panTravel.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom Or AnchorStyles.Top Or AnchorStyles.Right
        Me.panTravel.Controls.Add(chartTravel)
        chartTravel.Location = New Point(3, 3) : chartTravel.Size = New Point(794, 691)
        chartTravel.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right


        ' ### DO NOT FORGET TO CLEAR NEW GRAPHS WHEN THE ANALYZE BUTTON IS PRESSED!



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
                Call AutoUpdateHttp(CurrentPublishVersionNumber)
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
        frmHelp.Show()
    End Sub

    Private Sub UpdateNow()
        'display the Updating splash screen.
        frmUpdate.Show()
        frmUpdate.Refresh()
        Call AutoUpdateHttp(CurrentPublishVersionNumber)
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
            chartVibrations.Series("AccX").Points.Clear()
            chartVibrations.Series("AccY").Points.Clear()
            chartVibrations.Series("XYHighLine").Points.Clear()
            chartVibrations.Series("XYLowLine").Points.Clear()
            chartVibrations.Series("AccZ").Points.Clear()
            chartVibrations.Series("ZHighLine").Points.Clear()
            chartVibrations.Series("ZLowLine").Points.Clear()
            chartVibrations.Series("Altitude").Points.Clear()
            chartVibrations.Series("Speed").Points.Clear()
            chartPowerRails.Series("Vcc").Points.Clear()
            chartPowerRails.Series("VccLowLine").Points.Clear()
            chartPowerRails.Series("VccHighLine").Points.Clear()
            chartPowerRails.Series("VccOSDLine").Points.Clear()
            chartPowerRails.Series("Volts").Points.Clear()
            chartPowerRails.Series("Amps").Points.Clear()
            chartPowerRails.Series("Thrust").Points.Clear()
            chartGPS.Series("Status").Points.Clear()
            chartGPS.Series("StatusOKLine").Points.Clear()
            chartGPS.Series("HDop").Points.Clear()
            chartGPS.Series("HDopMinLine").Points.Clear()
            chartGPS.Series("Satellites").Points.Clear()
            chartGPS.Series("SatellitesMinLine").Points.Clear()
            chartGPS.Series("Speed").Points.Clear()
            chartGPS.Series("SpeedAvgLine").Points.Clear()
            chartAttitude.Series("Roll").Points.Clear()
            chartAttitude.Series("RollIn").Points.Clear()
            chartAttitude.Series("NavRoll").Points.Clear()
            chartAttitude.Series("Pitch").Points.Clear()
            chartAttitude.Series("PitchIn").Points.Clear()
            chartAttitude.Series("NavPitch").Points.Clear()
            chartAttitude.Series("Speed").Points.Clear()
            chartAttitude.Series("Yaw").Points.Clear()
            chartAttitude.Series("YawIn").Points.Clear()
            chartAttitude.Series("NavYaw").Points.Clear()
            chartAttitude.Series("Travel").Points.Clear()
            chartAttitude.Series("ClimbIn").Points.Clear()
            chartAttitude.Series("NavClimb").Points.Clear()
            chartAttitude.Series("Altitude").Points.Clear()
            chartTravel.Series("Yaw").Points.Clear()
            chartTravel.Series("GPS Calculated Direction").Points.Clear()

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


    Private Sub frmMainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        frmParameters.Close()
    End Sub

    Private Sub btnParameters_Click(sender As Object, e As EventArgs) Handles btnParameters.Click
        Call ShowParametersForm()
    End Sub


    Private Sub btnAnalysis_Click(sender As Object, e As EventArgs) Handles btnAnalysis.Click
        Call ButtonsCheckBoxes_Visible(True)
        Call ButtonsCharting_Visible(False)
        Call Chart_PowerRails_Visible(False)
        Call Chart_Vibrations_Visible(False)
        Call Chart_GPS_Visible(False)
        Call Chart_Attitude_Visible(False)
    End Sub

    Private Sub btnGraphs_Click(sender As Object, e As EventArgs) Handles btnGraphs.Click
        Call ButtonsCheckBoxes_Visible(False)
        'If Me.chartVibrations.Series("AccZ").Points.Count = 0 Then
        '    Me.chartVibrations.ChartAreas("AccZ").Visible = False
        'End If
        Call ButtonsCharting_Visible(True)
    End Sub




    Private Sub btnPowerChart_Click(sender As Object, e As EventArgs) Handles btnPowerChart.Click
        Call Chart_Vibrations_Visible(False)
        Call Chart_PowerRails_Visible(True)
        Call Chart_GPS_Visible(False)
        Call Chart_Attitude_Visible(False)
        Call Chart_Travel_Visible(False)
    End Sub

    Private Sub btnVibrationChart_Click(sender As Object, e As EventArgs) Handles btnVibrationChart.Click
        Call Chart_Vibrations_Visible(True)
        Call Chart_PowerRails_Visible(False)
        Call Chart_GPS_Visible(False)
        Call Chart_Attitude_Visible(False)
        Call Chart_Travel_Visible(False)
    End Sub

    Private Sub btnGPS_Click(sender As Object, e As EventArgs) Handles btnGPSChart.Click
        Call Chart_Vibrations_Visible(False)
        Call Chart_PowerRails_Visible(False)
        Call Chart_GPS_Visible(True)
        Call Chart_Attitude_Visible(False)
        Call Chart_Travel_Visible(False)
    End Sub

    Private Sub btnAttitude_Click(sender As Object, e As EventArgs) Handles btnAttitudeChart.Click
        Call Chart_Vibrations_Visible(False)
        Call Chart_PowerRails_Visible(False)
        Call Chart_GPS_Visible(False)
        Call Chart_Attitude_Visible(True)
        Call Chart_Travel_Visible(False)
    End Sub

    Private Sub btnTravel_Click(sender As Object, e As EventArgs) Handles btnTravel.Click
        Call Chart_Vibrations_Visible(False)
        Call Chart_PowerRails_Visible(False)
        Call Chart_GPS_Visible(False)
        Call Chart_Attitude_Visible(False)
        Call Chart_Travel_Visible(True)
    End Sub
End Class

