<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainForm))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.OpenFD = New System.Windows.Forms.OpenFileDialog()
        Me.lblCurrentVersion = New System.Windows.Forms.Label()
        Me.lblMyCurrentVersion = New System.Windows.Forms.Label()
        Me.richtxtLogAnalysis = New System.Windows.Forms.RichTextBox()
        Me.lblErrorCountNo = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnDonate = New System.Windows.Forms.Button()
        Me.btnMCB = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.btnVibrations = New System.Windows.Forms.Button()
        Me.btnDisplayVibrationChart = New System.Windows.Forms.Button()
        Me.btnAnalyze = New System.Windows.Forms.Button()
        Me.btnLoadLog = New System.Windows.Forms.Button()
        Me.chkboxAutoCommands = New System.Windows.Forms.CheckBox()
        Me.chkboxParameterWarnings = New System.Windows.Forms.CheckBox()
        Me.chkboxVibrations = New System.Windows.Forms.CheckBox()
        Me.chkboxFlightDataTypes = New System.Windows.Forms.CheckBox()
        Me.chkboxErrors = New System.Windows.Forms.CheckBox()
        Me.chkboxPM = New System.Windows.Forms.CheckBox()
        Me.chkboxDU32 = New System.Windows.Forms.CheckBox()
        Me.chkboxNonCriticalEvents = New System.Windows.Forms.CheckBox()
        Me.chkboxSplitModeLandings = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblEsc = New System.Windows.Forms.Label()
        Me.barReadFile = New System.Windows.Forms.ProgressBar()
        Me.lblErrorCount = New System.Windows.Forms.Label()
        Me.txtVibrationAltitude = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtVibrationSpeed = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkboxVibrationInFlight = New System.Windows.Forms.CheckBox()
        Me.lblErrors = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1008, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'OpenFD
        '
        Me.OpenFD.FileName = "*.txt"
        '
        'lblCurrentVersion
        '
        Me.lblCurrentVersion.AutoSize = True
        Me.lblCurrentVersion.Location = New System.Drawing.Point(896, 674)
        Me.lblCurrentVersion.Name = "lblCurrentVersion"
        Me.lblCurrentVersion.Size = New System.Drawing.Size(54, 13)
        Me.lblCurrentVersion.TabIndex = 16
        Me.lblCurrentVersion.Text = "NOT SET"
        '
        'lblMyCurrentVersion
        '
        Me.lblMyCurrentVersion.AutoSize = True
        Me.lblMyCurrentVersion.Location = New System.Drawing.Point(952, 674)
        Me.lblMyCurrentVersion.Name = "lblMyCurrentVersion"
        Me.lblMyCurrentVersion.Size = New System.Drawing.Size(54, 13)
        Me.lblMyCurrentVersion.TabIndex = 17
        Me.lblMyCurrentVersion.Text = "NOT SET"
        '
        'richtxtLogAnalysis
        '
        Me.richtxtLogAnalysis.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(29, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.richtxtLogAnalysis.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.richtxtLogAnalysis.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.richtxtLogAnalysis.ForeColor = System.Drawing.Color.White
        Me.richtxtLogAnalysis.Location = New System.Drawing.Point(212, 87)
        Me.richtxtLogAnalysis.Name = "richtxtLogAnalysis"
        Me.richtxtLogAnalysis.ReadOnly = True
        Me.richtxtLogAnalysis.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.richtxtLogAnalysis.Size = New System.Drawing.Size(794, 605)
        Me.richtxtLogAnalysis.TabIndex = 19
        Me.richtxtLogAnalysis.Text = ""
        '
        'lblErrorCountNo
        '
        Me.lblErrorCountNo.AutoSize = True
        Me.lblErrorCountNo.ForeColor = System.Drawing.Color.Red
        Me.lblErrorCountNo.Location = New System.Drawing.Point(414, 306)
        Me.lblErrorCountNo.Name = "lblErrorCountNo"
        Me.lblErrorCountNo.Size = New System.Drawing.Size(0, 13)
        Me.lblErrorCountNo.TabIndex = 23
        Me.lblErrorCountNo.Visible = False
        '
        'btnDonate
        '
        Me.btnDonate.BackgroundImage = Global.APMLogFileAnaliser.My.Resources.Resources.DB
        Me.btnDonate.FlatAppearance.BorderSize = 0
        Me.btnDonate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDonate.Location = New System.Drawing.Point(732, 10)
        Me.btnDonate.MaximumSize = New System.Drawing.Size(64, 68)
        Me.btnDonate.MinimumSize = New System.Drawing.Size(64, 68)
        Me.btnDonate.Name = "btnDonate"
        Me.btnDonate.Size = New System.Drawing.Size(64, 68)
        Me.btnDonate.TabIndex = 31
        Me.ToolTip1.SetToolTip(Me.btnDonate, "Donate to help keep this project alive")
        Me.btnDonate.UseVisualStyleBackColor = True
        '
        'btnMCB
        '
        Me.btnMCB.BackgroundImage = Global.APMLogFileAnaliser.My.Resources.Resources.CMBB
        Me.btnMCB.FlatAppearance.BorderSize = 0
        Me.btnMCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMCB.Location = New System.Drawing.Point(872, 10)
        Me.btnMCB.MaximumSize = New System.Drawing.Size(64, 68)
        Me.btnMCB.MinimumSize = New System.Drawing.Size(64, 68)
        Me.btnMCB.Name = "btnMCB"
        Me.btnMCB.Size = New System.Drawing.Size(64, 68)
        Me.btnMCB.TabIndex = 30
        Me.ToolTip1.SetToolTip(Me.btnMCB, "More Cowbells!")
        Me.btnMCB.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.BackgroundImage = Global.APMLogFileAnaliser.My.Resources.Resources.UpdateB
        Me.btnUpdate.FlatAppearance.BorderSize = 0
        Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdate.Location = New System.Drawing.Point(802, 10)
        Me.btnUpdate.MaximumSize = New System.Drawing.Size(64, 68)
        Me.btnUpdate.MinimumSize = New System.Drawing.Size(64, 68)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(64, 68)
        Me.btnUpdate.TabIndex = 29
        Me.ToolTip1.SetToolTip(Me.btnUpdate, "Click here to update to latest version")
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnHelp
        '
        Me.btnHelp.BackgroundImage = Global.APMLogFileAnaliser.My.Resources.Resources.HelpB
        Me.btnHelp.FlatAppearance.BorderSize = 0
        Me.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHelp.Location = New System.Drawing.Point(942, 10)
        Me.btnHelp.MaximumSize = New System.Drawing.Size(64, 68)
        Me.btnHelp.MinimumSize = New System.Drawing.Size(64, 68)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(64, 68)
        Me.btnHelp.TabIndex = 28
        Me.ToolTip1.SetToolTip(Me.btnHelp, "Help me Obi-Wan")
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'btnVibrations
        '
        Me.btnVibrations.BackgroundImage = Global.APMLogFileAnaliser.My.Resources.Resources.CTB
        Me.btnVibrations.FlatAppearance.BorderSize = 0
        Me.btnVibrations.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVibrations.Location = New System.Drawing.Point(222, 10)
        Me.btnVibrations.MaximumSize = New System.Drawing.Size(64, 68)
        Me.btnVibrations.MinimumSize = New System.Drawing.Size(64, 68)
        Me.btnVibrations.Name = "btnVibrations"
        Me.btnVibrations.Size = New System.Drawing.Size(64, 68)
        Me.btnVibrations.TabIndex = 27
        Me.ToolTip1.SetToolTip(Me.btnVibrations, "Copy formatted text to clipboard for pasting to RCG Forums")
        Me.btnVibrations.UseVisualStyleBackColor = True
        '
        'btnDisplayVibrationChart
        '
        Me.btnDisplayVibrationChart.BackgroundImage = Global.APMLogFileAnaliser.My.Resources.Resources.VB
        Me.btnDisplayVibrationChart.Enabled = False
        Me.btnDisplayVibrationChart.FlatAppearance.BorderSize = 0
        Me.btnDisplayVibrationChart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDisplayVibrationChart.Location = New System.Drawing.Point(152, 10)
        Me.btnDisplayVibrationChart.MaximumSize = New System.Drawing.Size(64, 68)
        Me.btnDisplayVibrationChart.MinimumSize = New System.Drawing.Size(64, 68)
        Me.btnDisplayVibrationChart.Name = "btnDisplayVibrationChart"
        Me.btnDisplayVibrationChart.Size = New System.Drawing.Size(64, 68)
        Me.btnDisplayVibrationChart.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.btnDisplayVibrationChart, "Click this button to display vibration analysis")
        Me.btnDisplayVibrationChart.UseVisualStyleBackColor = True
        '
        'btnAnalyze
        '
        Me.btnAnalyze.BackgroundImage = Global.APMLogFileAnaliser.My.Resources.Resources.AB
        Me.btnAnalyze.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnAnalyze.FlatAppearance.BorderSize = 0
        Me.btnAnalyze.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAnalyze.Location = New System.Drawing.Point(82, 10)
        Me.btnAnalyze.MaximumSize = New System.Drawing.Size(64, 68)
        Me.btnAnalyze.MinimumSize = New System.Drawing.Size(64, 68)
        Me.btnAnalyze.Name = "btnAnalyze"
        Me.btnAnalyze.Size = New System.Drawing.Size(64, 68)
        Me.btnAnalyze.TabIndex = 26
        Me.ToolTip1.SetToolTip(Me.btnAnalyze, "Click this button to analyze loaded logfile")
        Me.btnAnalyze.UseVisualStyleBackColor = True
        '
        'btnLoadLog
        '
        Me.btnLoadLog.BackgroundImage = Global.APMLogFileAnaliser.My.Resources.Resources.LLB
        Me.btnLoadLog.FlatAppearance.BorderSize = 0
        Me.btnLoadLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoadLog.Location = New System.Drawing.Point(12, 10)
        Me.btnLoadLog.MaximumSize = New System.Drawing.Size(64, 68)
        Me.btnLoadLog.MinimumSize = New System.Drawing.Size(64, 68)
        Me.btnLoadLog.Name = "btnLoadLog"
        Me.btnLoadLog.Size = New System.Drawing.Size(64, 68)
        Me.btnLoadLog.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.btnLoadLog, "Click this button to load a logfile for analysis")
        Me.btnLoadLog.UseVisualStyleBackColor = True
        '
        'chkboxAutoCommands
        '
        Me.chkboxAutoCommands.AutoSize = True
        Me.chkboxAutoCommands.BackgroundImage = CType(resources.GetObject("chkboxAutoCommands.BackgroundImage"), System.Drawing.Image)
        Me.chkboxAutoCommands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chkboxAutoCommands.Checked = True
        Me.chkboxAutoCommands.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkboxAutoCommands.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkboxAutoCommands.Location = New System.Drawing.Point(21, 397)
        Me.chkboxAutoCommands.MaximumSize = New System.Drawing.Size(177, 42)
        Me.chkboxAutoCommands.MinimumSize = New System.Drawing.Size(177, 42)
        Me.chkboxAutoCommands.Name = "chkboxAutoCommands"
        Me.chkboxAutoCommands.Size = New System.Drawing.Size(177, 42)
        Me.chkboxAutoCommands.TabIndex = 13
        Me.chkboxAutoCommands.Text = "Auto Commands"
        Me.ToolTip1.SetToolTip(Me.chkboxAutoCommands, "Select Automatic Flight mode Commands")
        Me.chkboxAutoCommands.UseVisualStyleBackColor = True
        '
        'chkboxParameterWarnings
        '
        Me.chkboxParameterWarnings.AutoSize = True
        Me.chkboxParameterWarnings.BackgroundImage = CType(resources.GetObject("chkboxParameterWarnings.BackgroundImage"), System.Drawing.Image)
        Me.chkboxParameterWarnings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chkboxParameterWarnings.Checked = True
        Me.chkboxParameterWarnings.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkboxParameterWarnings.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkboxParameterWarnings.Location = New System.Drawing.Point(21, 13)
        Me.chkboxParameterWarnings.MaximumSize = New System.Drawing.Size(177, 42)
        Me.chkboxParameterWarnings.MinimumSize = New System.Drawing.Size(177, 42)
        Me.chkboxParameterWarnings.Name = "chkboxParameterWarnings"
        Me.chkboxParameterWarnings.Size = New System.Drawing.Size(177, 42)
        Me.chkboxParameterWarnings.TabIndex = 2
        Me.chkboxParameterWarnings.Text = "Parameter Warnings"
        Me.chkboxParameterWarnings.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.ToolTip1.SetToolTip(Me.chkboxParameterWarnings, "Select Parameter Warnings")
        Me.chkboxParameterWarnings.UseVisualStyleBackColor = True
        '
        'chkboxVibrations
        '
        Me.chkboxVibrations.AutoSize = True
        Me.chkboxVibrations.BackgroundImage = CType(resources.GetObject("chkboxVibrations.BackgroundImage"), System.Drawing.Image)
        Me.chkboxVibrations.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chkboxVibrations.Checked = True
        Me.chkboxVibrations.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkboxVibrations.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkboxVibrations.Location = New System.Drawing.Point(21, 349)
        Me.chkboxVibrations.MaximumSize = New System.Drawing.Size(177, 42)
        Me.chkboxVibrations.MinimumSize = New System.Drawing.Size(177, 42)
        Me.chkboxVibrations.Name = "chkboxVibrations"
        Me.chkboxVibrations.Size = New System.Drawing.Size(177, 42)
        Me.chkboxVibrations.TabIndex = 12
        Me.chkboxVibrations.Text = "Vibrations"
        Me.ToolTip1.SetToolTip(Me.chkboxVibrations, "Select Vibrations")
        Me.chkboxVibrations.UseVisualStyleBackColor = True
        '
        'chkboxFlightDataTypes
        '
        Me.chkboxFlightDataTypes.AutoSize = True
        Me.chkboxFlightDataTypes.BackgroundImage = CType(resources.GetObject("chkboxFlightDataTypes.BackgroundImage"), System.Drawing.Image)
        Me.chkboxFlightDataTypes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chkboxFlightDataTypes.Checked = True
        Me.chkboxFlightDataTypes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkboxFlightDataTypes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkboxFlightDataTypes.Location = New System.Drawing.Point(21, 61)
        Me.chkboxFlightDataTypes.MaximumSize = New System.Drawing.Size(177, 42)
        Me.chkboxFlightDataTypes.MinimumSize = New System.Drawing.Size(177, 42)
        Me.chkboxFlightDataTypes.Name = "chkboxFlightDataTypes"
        Me.chkboxFlightDataTypes.Size = New System.Drawing.Size(177, 42)
        Me.chkboxFlightDataTypes.TabIndex = 6
        Me.chkboxFlightDataTypes.Text = "Flight Data Types"
        Me.ToolTip1.SetToolTip(Me.chkboxFlightDataTypes, "Select Flight Data Types")
        Me.chkboxFlightDataTypes.UseVisualStyleBackColor = True
        '
        'chkboxErrors
        '
        Me.chkboxErrors.AutoSize = True
        Me.chkboxErrors.BackgroundImage = CType(resources.GetObject("chkboxErrors.BackgroundImage"), System.Drawing.Image)
        Me.chkboxErrors.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chkboxErrors.Checked = True
        Me.chkboxErrors.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkboxErrors.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkboxErrors.Location = New System.Drawing.Point(21, 301)
        Me.chkboxErrors.MaximumSize = New System.Drawing.Size(177, 42)
        Me.chkboxErrors.MinimumSize = New System.Drawing.Size(177, 42)
        Me.chkboxErrors.Name = "chkboxErrors"
        Me.chkboxErrors.Size = New System.Drawing.Size(177, 42)
        Me.chkboxErrors.TabIndex = 9
        Me.chkboxErrors.Text = "Errors"
        Me.ToolTip1.SetToolTip(Me.chkboxErrors, "Select Errors")
        Me.chkboxErrors.UseVisualStyleBackColor = True
        '
        'chkboxPM
        '
        Me.chkboxPM.AutoSize = True
        Me.chkboxPM.BackgroundImage = CType(resources.GetObject("chkboxPM.BackgroundImage"), System.Drawing.Image)
        Me.chkboxPM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chkboxPM.Checked = True
        Me.chkboxPM.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkboxPM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkboxPM.Location = New System.Drawing.Point(21, 205)
        Me.chkboxPM.MaximumSize = New System.Drawing.Size(177, 42)
        Me.chkboxPM.MinimumSize = New System.Drawing.Size(177, 42)
        Me.chkboxPM.Name = "chkboxPM"
        Me.chkboxPM.Size = New System.Drawing.Size(177, 42)
        Me.chkboxPM.TabIndex = 11
        Me.chkboxPM.Text = "APM Perf Management"
        Me.ToolTip1.SetToolTip(Me.chkboxPM, "Select APM Performance Management")
        Me.chkboxPM.UseVisualStyleBackColor = True
        '
        'chkboxDU32
        '
        Me.chkboxDU32.AutoSize = True
        Me.chkboxDU32.BackgroundImage = CType(resources.GetObject("chkboxDU32.BackgroundImage"), System.Drawing.Image)
        Me.chkboxDU32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chkboxDU32.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkboxDU32.Location = New System.Drawing.Point(21, 253)
        Me.chkboxDU32.MaximumSize = New System.Drawing.Size(177, 42)
        Me.chkboxDU32.MinimumSize = New System.Drawing.Size(177, 42)
        Me.chkboxDU32.Name = "chkboxDU32"
        Me.chkboxDU32.Size = New System.Drawing.Size(177, 42)
        Me.chkboxDU32.TabIndex = 10
        Me.chkboxDU32.Text = "DU32 Additional Info"
        Me.ToolTip1.SetToolTip(Me.chkboxDU32, "Select DU32 Additional Information")
        Me.chkboxDU32.UseVisualStyleBackColor = True
        '
        'chkboxNonCriticalEvents
        '
        Me.chkboxNonCriticalEvents.AutoSize = True
        Me.chkboxNonCriticalEvents.BackgroundImage = CType(resources.GetObject("chkboxNonCriticalEvents.BackgroundImage"), System.Drawing.Image)
        Me.chkboxNonCriticalEvents.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chkboxNonCriticalEvents.Checked = True
        Me.chkboxNonCriticalEvents.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkboxNonCriticalEvents.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.chkboxNonCriticalEvents.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkboxNonCriticalEvents.Location = New System.Drawing.Point(21, 109)
        Me.chkboxNonCriticalEvents.MaximumSize = New System.Drawing.Size(177, 42)
        Me.chkboxNonCriticalEvents.MinimumSize = New System.Drawing.Size(177, 42)
        Me.chkboxNonCriticalEvents.Name = "chkboxNonCriticalEvents"
        Me.chkboxNonCriticalEvents.Size = New System.Drawing.Size(177, 42)
        Me.chkboxNonCriticalEvents.TabIndex = 7
        Me.chkboxNonCriticalEvents.Text = "Non-Critical Events"
        Me.ToolTip1.SetToolTip(Me.chkboxNonCriticalEvents, "Select Non-Critical Evens")
        Me.chkboxNonCriticalEvents.UseVisualStyleBackColor = True
        '
        'chkboxSplitModeLandings
        '
        Me.chkboxSplitModeLandings.AutoSize = True
        Me.chkboxSplitModeLandings.BackgroundImage = CType(resources.GetObject("chkboxSplitModeLandings.BackgroundImage"), System.Drawing.Image)
        Me.chkboxSplitModeLandings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chkboxSplitModeLandings.Checked = True
        Me.chkboxSplitModeLandings.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkboxSplitModeLandings.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkboxSplitModeLandings.Location = New System.Drawing.Point(21, 157)
        Me.chkboxSplitModeLandings.MaximumSize = New System.Drawing.Size(177, 42)
        Me.chkboxSplitModeLandings.MinimumSize = New System.Drawing.Size(177, 42)
        Me.chkboxSplitModeLandings.Name = "chkboxSplitModeLandings"
        Me.chkboxSplitModeLandings.Size = New System.Drawing.Size(177, 42)
        Me.chkboxSplitModeLandings.TabIndex = 8
        Me.chkboxSplitModeLandings.Text = "Detailed Mode && Landings"
        Me.ToolTip1.SetToolTip(Me.chkboxSplitModeLandings, "Select Detailed Mode and Landings")
        Me.chkboxSplitModeLandings.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.APMLogFileAnaliser.My.Resources.Resources.left_back
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.lblEsc)
        Me.Panel1.Controls.Add(Me.barReadFile)
        Me.Panel1.Controls.Add(Me.chkboxAutoCommands)
        Me.Panel1.Controls.Add(Me.chkboxParameterWarnings)
        Me.Panel1.Controls.Add(Me.chkboxVibrations)
        Me.Panel1.Controls.Add(Me.chkboxFlightDataTypes)
        Me.Panel1.Controls.Add(Me.chkboxErrors)
        Me.Panel1.Controls.Add(Me.lblErrorCount)
        Me.Panel1.Controls.Add(Me.txtVibrationAltitude)
        Me.Panel1.Controls.Add(Me.chkboxPM)
        Me.Panel1.Controls.Add(Me.chkboxDU32)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtVibrationSpeed)
        Me.Panel1.Controls.Add(Me.chkboxNonCriticalEvents)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.chkboxSplitModeLandings)
        Me.Panel1.Controls.Add(Me.chkboxVibrationInFlight)
        Me.Panel1.Controls.Add(Me.lblErrors)
        Me.Panel1.Location = New System.Drawing.Point(-9, 87)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(225, 602)
        Me.Panel1.TabIndex = 24
        '
        'lblEsc
        '
        Me.lblEsc.AutoSize = True
        Me.lblEsc.ForeColor = System.Drawing.Color.YellowGreen
        Me.lblEsc.Location = New System.Drawing.Point(21, 572)
        Me.lblEsc.Name = "lblEsc"
        Me.lblEsc.Size = New System.Drawing.Size(94, 13)
        Me.lblEsc.TabIndex = 23
        Me.lblEsc.Text = "Press Esc to Abort"
        Me.lblEsc.Visible = False
        '
        'barReadFile
        '
        Me.barReadFile.Location = New System.Drawing.Point(21, 545)
        Me.barReadFile.Name = "barReadFile"
        Me.barReadFile.Size = New System.Drawing.Size(177, 24)
        Me.barReadFile.TabIndex = 10
        Me.barReadFile.Visible = False
        '
        'lblErrorCount
        '
        Me.lblErrorCount.AutoSize = True
        Me.lblErrorCount.ForeColor = System.Drawing.Color.Red
        Me.lblErrorCount.Location = New System.Drawing.Point(21, 522)
        Me.lblErrorCount.Name = "lblErrorCount"
        Me.lblErrorCount.Size = New System.Drawing.Size(34, 13)
        Me.lblErrorCount.TabIndex = 22
        Me.lblErrorCount.Text = "Errors"
        Me.lblErrorCount.Visible = False
        '
        'txtVibrationAltitude
        '
        Me.txtVibrationAltitude.Location = New System.Drawing.Point(68, 500)
        Me.txtVibrationAltitude.Name = "txtVibrationAltitude"
        Me.txtVibrationAltitude.Size = New System.Drawing.Size(19, 20)
        Me.txtVibrationAltitude.TabIndex = 4
        Me.txtVibrationAltitude.Text = "2"
        Me.txtVibrationAltitude.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(21, 503)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Altitude >        m"
        Me.Label4.Visible = False
        '
        'txtVibrationSpeed
        '
        Me.txtVibrationSpeed.Location = New System.Drawing.Point(68, 500)
        Me.txtVibrationSpeed.Name = "txtVibrationSpeed"
        Me.txtVibrationSpeed.Size = New System.Drawing.Size(19, 20)
        Me.txtVibrationSpeed.TabIndex = 2
        Me.txtVibrationSpeed.Text = "40"
        Me.txtVibrationSpeed.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(21, 503)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Speed <        m/s"
        Me.Label3.Visible = False
        '
        'chkboxVibrationInFlight
        '
        Me.chkboxVibrationInFlight.AutoSize = True
        Me.chkboxVibrationInFlight.Checked = True
        Me.chkboxVibrationInFlight.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkboxVibrationInFlight.Location = New System.Drawing.Point(24, 502)
        Me.chkboxVibrationInFlight.Name = "chkboxVibrationInFlight"
        Me.chkboxVibrationInFlight.Size = New System.Drawing.Size(63, 17)
        Me.chkboxVibrationInFlight.TabIndex = 0
        Me.chkboxVibrationInFlight.Text = "In Flight"
        Me.chkboxVibrationInFlight.UseVisualStyleBackColor = True
        Me.chkboxVibrationInFlight.Visible = False
        '
        'lblErrors
        '
        Me.lblErrors.AutoSize = True
        Me.lblErrors.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblErrors.ForeColor = System.Drawing.Color.Red
        Me.lblErrors.Location = New System.Drawing.Point(35, 552)
        Me.lblErrors.Name = "lblErrors"
        Me.lblErrors.Size = New System.Drawing.Size(136, 13)
        Me.lblErrors.TabIndex = 20
        Me.lblErrors.Text = "Log File Corruption handled"
        Me.lblErrors.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.APMLogFileAnaliser.My.Resources.Resources.OFD
        Me.PictureBox1.Location = New System.Drawing.Point(82, 10)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(261, 66)
        Me.PictureBox1.TabIndex = 32
        Me.PictureBox1.TabStop = False
        '
        'frmMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(1008, 688)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnDonate)
        Me.Controls.Add(Me.lblMyCurrentVersion)
        Me.Controls.Add(Me.lblCurrentVersion)
        Me.Controls.Add(Me.richtxtLogAnalysis)
        Me.Controls.Add(Me.btnMCB)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.btnVibrations)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.btnDisplayVibrationChart)
        Me.Controls.Add(Me.btnAnalyze)
        Me.Controls.Add(Me.btnLoadLog)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblErrorCountNo)
        Me.ForeColor = System.Drawing.Color.White
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmMainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "APM Flight Log Analysis"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents chkboxErrors As System.Windows.Forms.CheckBox
    Friend WithEvents chkboxSplitModeLandings As System.Windows.Forms.CheckBox
    Friend WithEvents chkboxNonCriticalEvents As System.Windows.Forms.CheckBox
    Friend WithEvents chkboxFlightDataTypes As System.Windows.Forms.CheckBox
    Friend WithEvents chkboxParameterWarnings As System.Windows.Forms.CheckBox
    Friend WithEvents OpenFD As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblCurrentVersion As System.Windows.Forms.Label
    Friend WithEvents lblMyCurrentVersion As System.Windows.Forms.Label
    Friend WithEvents richtxtLogAnalysis As System.Windows.Forms.RichTextBox
    Friend WithEvents barReadFile As System.Windows.Forms.ProgressBar
    Friend WithEvents lblErrors As System.Windows.Forms.Label
    Friend WithEvents txtVibrationSpeed As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkboxVibrationInFlight As System.Windows.Forms.CheckBox
    Friend WithEvents txtVibrationAltitude As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnDisplayVibrationChart As System.Windows.Forms.Button
    Friend WithEvents lblErrorCount As System.Windows.Forms.Label
    Friend WithEvents lblErrorCountNo As System.Windows.Forms.Label
    Friend WithEvents chkboxDU32 As System.Windows.Forms.CheckBox
    Friend WithEvents chkboxPM As System.Windows.Forms.CheckBox
    Friend WithEvents chkboxVibrations As System.Windows.Forms.CheckBox
    Friend WithEvents chkboxAutoCommands As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnLoadLog As System.Windows.Forms.Button
    Friend WithEvents btnAnalyze As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnVibrations As System.Windows.Forms.Button
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnMCB As System.Windows.Forms.Button
    Friend WithEvents btnDonate As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblEsc As System.Windows.Forms.Label
End Class
