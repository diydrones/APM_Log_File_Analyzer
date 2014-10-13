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
        Dim ChartArea17 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea18 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea19 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea20 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend5 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series33 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series34 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series35 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series36 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series37 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series38 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series39 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea21 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea22 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea23 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea24 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend6 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series40 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series41 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series42 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series43 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series44 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series45 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series46 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series47 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series48 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
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
        Me.btnCopyText = New System.Windows.Forms.Button()
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
        Me.panAnalysisButtons = New System.Windows.Forms.Panel()
        Me.lblEsc = New System.Windows.Forms.Label()
        Me.barReadFile = New System.Windows.Forms.ProgressBar()
        Me.lblErrorCount = New System.Windows.Forms.Label()
        Me.txtVibrationAltitude = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtVibrationSpeed = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkboxVibrationInFlight = New System.Windows.Forms.CheckBox()
        Me.lblErrors = New System.Windows.Forms.Label()
        Me.picClickButton = New System.Windows.Forms.PictureBox()
        Me.chartPowerRails = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.lblVcc = New System.Windows.Forms.Label()
        Me.lblVolts = New System.Windows.Forms.Label()
        Me.lblAmps = New System.Windows.Forms.Label()
        Me.lblThrust = New System.Windows.Forms.Label()
        Me.btnGraphs = New System.Windows.Forms.Button()
        Me.btnAnalysis = New System.Windows.Forms.Button()
        Me.chartVibrations = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.lblAbove = New System.Windows.Forms.Label()
        Me.lblSpeed = New System.Windows.Forms.Label()
        Me.lblAltitude = New System.Windows.Forms.Label()
        Me.lblAccZ = New System.Windows.Forms.Label()
        Me.lblAccXY = New System.Windows.Forms.Label()
        Me.lblAccZ_Acceptable = New System.Windows.Forms.Label()
        Me.lblXY_Acceptable = New System.Windows.Forms.Label()
        Me.panVibrations = New System.Windows.Forms.Panel()
        Me.lblOSD = New System.Windows.Forms.Label()
        Me.panPowerRails = New System.Windows.Forms.Panel()
        Me.panAnalysis = New System.Windows.Forms.Panel()
        Me.panGraphButtons = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnVibrationChart = New System.Windows.Forms.Button()
        Me.btnPowerChart = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.panHelpButtons = New System.Windows.Forms.Panel()
        Me.btnParameters = New System.Windows.Forms.Button()
        Me.panAnalysisButtons.SuspendLayout()
        CType(Me.picClickButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartPowerRails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartVibrations, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panVibrations.SuspendLayout()
        Me.panPowerRails.SuspendLayout()
        Me.panAnalysis.SuspendLayout()
        Me.panGraphButtons.SuspendLayout()
        Me.panHelpButtons.SuspendLayout()
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
        Me.lblCurrentVersion.Location = New System.Drawing.Point(77, 589)
        Me.lblCurrentVersion.Name = "lblCurrentVersion"
        Me.lblCurrentVersion.Size = New System.Drawing.Size(54, 13)
        Me.lblCurrentVersion.TabIndex = 16
        Me.lblCurrentVersion.Text = "NOT SET"
        '
        'lblMyCurrentVersion
        '
        Me.lblMyCurrentVersion.AutoSize = True
        Me.lblMyCurrentVersion.Location = New System.Drawing.Point(144, 589)
        Me.lblMyCurrentVersion.Name = "lblMyCurrentVersion"
        Me.lblMyCurrentVersion.Size = New System.Drawing.Size(54, 13)
        Me.lblMyCurrentVersion.TabIndex = 17
        Me.lblMyCurrentVersion.Text = "NOT SET"
        '
        'richtxtLogAnalysis
        '
        Me.richtxtLogAnalysis.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(29, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.richtxtLogAnalysis.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.richtxtLogAnalysis.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.richtxtLogAnalysis.ForeColor = System.Drawing.Color.White
        Me.richtxtLogAnalysis.Location = New System.Drawing.Point(0, 0)
        Me.richtxtLogAnalysis.Name = "richtxtLogAnalysis"
        Me.richtxtLogAnalysis.ReadOnly = True
        Me.richtxtLogAnalysis.Size = New System.Drawing.Size(167, 155)
        Me.richtxtLogAnalysis.TabIndex = 19
        Me.richtxtLogAnalysis.Text = ""
        Me.richtxtLogAnalysis.WordWrap = False
        '
        'lblErrorCountNo
        '
        Me.lblErrorCountNo.AutoSize = True
        Me.lblErrorCountNo.ForeColor = System.Drawing.Color.Red
        Me.lblErrorCountNo.Location = New System.Drawing.Point(65, 522)
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
        Me.btnDonate.Location = New System.Drawing.Point(3, 3)
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
        Me.btnMCB.Location = New System.Drawing.Point(143, 3)
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
        Me.btnUpdate.Location = New System.Drawing.Point(73, 3)
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
        Me.btnHelp.Location = New System.Drawing.Point(213, 4)
        Me.btnHelp.MaximumSize = New System.Drawing.Size(64, 68)
        Me.btnHelp.MinimumSize = New System.Drawing.Size(64, 68)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(64, 68)
        Me.btnHelp.TabIndex = 28
        Me.ToolTip1.SetToolTip(Me.btnHelp, "Help me Obi-Wan")
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'btnCopyText
        '
        Me.btnCopyText.BackgroundImage = Global.APMLogFileAnaliser.My.Resources.Resources.CTB
        Me.btnCopyText.FlatAppearance.BorderSize = 0
        Me.btnCopyText.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCopyText.Location = New System.Drawing.Point(233, 7)
        Me.btnCopyText.MaximumSize = New System.Drawing.Size(64, 68)
        Me.btnCopyText.MinimumSize = New System.Drawing.Size(64, 68)
        Me.btnCopyText.Name = "btnCopyText"
        Me.btnCopyText.Size = New System.Drawing.Size(64, 68)
        Me.btnCopyText.TabIndex = 27
        Me.ToolTip1.SetToolTip(Me.btnCopyText, "Copy formatted text to clipboard for pasting to RCG Forums")
        Me.btnCopyText.UseVisualStyleBackColor = True
        Me.btnCopyText.Visible = False
        '
        'btnAnalyze
        '
        Me.btnAnalyze.BackgroundImage = Global.APMLogFileAnaliser.My.Resources.Resources.AB
        Me.btnAnalyze.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
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
        Me.btnAnalyze.Visible = False
        '
        'btnLoadLog
        '
        Me.btnLoadLog.BackgroundImage = Global.APMLogFileAnaliser.My.Resources.Resources.LLB
        Me.btnLoadLog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
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
        Me.chkboxNonCriticalEvents.Cursor = System.Windows.Forms.Cursors.Default
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
        'panAnalysisButtons
        '
        Me.panAnalysisButtons.BackgroundImage = Global.APMLogFileAnaliser.My.Resources.Resources.left_back
        Me.panAnalysisButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panAnalysisButtons.Controls.Add(Me.lblEsc)
        Me.panAnalysisButtons.Controls.Add(Me.barReadFile)
        Me.panAnalysisButtons.Controls.Add(Me.lblMyCurrentVersion)
        Me.panAnalysisButtons.Controls.Add(Me.chkboxAutoCommands)
        Me.panAnalysisButtons.Controls.Add(Me.lblCurrentVersion)
        Me.panAnalysisButtons.Controls.Add(Me.chkboxParameterWarnings)
        Me.panAnalysisButtons.Controls.Add(Me.chkboxVibrations)
        Me.panAnalysisButtons.Controls.Add(Me.chkboxFlightDataTypes)
        Me.panAnalysisButtons.Controls.Add(Me.chkboxErrors)
        Me.panAnalysisButtons.Controls.Add(Me.lblErrorCount)
        Me.panAnalysisButtons.Controls.Add(Me.txtVibrationAltitude)
        Me.panAnalysisButtons.Controls.Add(Me.chkboxPM)
        Me.panAnalysisButtons.Controls.Add(Me.chkboxDU32)
        Me.panAnalysisButtons.Controls.Add(Me.Label4)
        Me.panAnalysisButtons.Controls.Add(Me.txtVibrationSpeed)
        Me.panAnalysisButtons.Controls.Add(Me.chkboxNonCriticalEvents)
        Me.panAnalysisButtons.Controls.Add(Me.Label3)
        Me.panAnalysisButtons.Controls.Add(Me.chkboxSplitModeLandings)
        Me.panAnalysisButtons.Controls.Add(Me.chkboxVibrationInFlight)
        Me.panAnalysisButtons.Controls.Add(Me.lblErrors)
        Me.panAnalysisButtons.Controls.Add(Me.lblErrorCountNo)
        Me.panAnalysisButtons.Location = New System.Drawing.Point(-9, 87)
        Me.panAnalysisButtons.Name = "panAnalysisButtons"
        Me.panAnalysisButtons.Size = New System.Drawing.Size(225, 602)
        Me.panAnalysisButtons.TabIndex = 24
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
        'picClickButton
        '
        Me.picClickButton.Image = Global.APMLogFileAnaliser.My.Resources.Resources.OFD
        Me.picClickButton.Location = New System.Drawing.Point(447, 10)
        Me.picClickButton.Name = "picClickButton"
        Me.picClickButton.Size = New System.Drawing.Size(261, 66)
        Me.picClickButton.TabIndex = 32
        Me.picClickButton.TabStop = False
        '
        'chartPowerRails
        '
        Me.chartPowerRails.BackColor = System.Drawing.Color.Transparent
        ChartArea17.AxisX.LabelStyle.Enabled = False
        ChartArea17.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Maroon
        ChartArea17.AxisX.MajorTickMark.Interval = 10.0R
        ChartArea17.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea17.AxisX.Minimum = 0.0R
        ChartArea17.AxisY.Interval = 0.2R
        ChartArea17.AxisY.IsLabelAutoFit = False
        ChartArea17.AxisY.IsStartedFromZero = False
        ChartArea17.AxisY.LabelStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        ChartArea17.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea17.AxisY.LineColor = System.Drawing.Color.Maroon
        ChartArea17.AxisY.MajorGrid.Interval = 0.2R
        ChartArea17.AxisY.MajorTickMark.Interval = 1.0R
        ChartArea17.AxisY.Maximum = 5.8R
        ChartArea17.AxisY.Minimum = 4.2R
        ChartArea17.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea17.Name = "Vcc"
        ChartArea17.Position.Auto = False
        ChartArea17.Position.Height = 19.0!
        ChartArea17.Position.Width = 95.0!
        ChartArea17.Position.X = 3.0!
        ChartArea17.Position.Y = 3.0!
        ChartArea18.AxisX.LabelStyle.Enabled = False
        ChartArea18.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea18.AxisX.MajorTickMark.Interval = 10.0R
        ChartArea18.AxisX.Minimum = 0.0R
        ChartArea18.AxisY.Interval = 1.0R
        ChartArea18.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea18.AxisY.MajorGrid.Interval = 1.0R
        ChartArea18.AxisY.MajorTickMark.Interval = 1.0R
        ChartArea18.AxisY.Maximum = 17.5R
        ChartArea18.AxisY.Minimum = 7.5R
        ChartArea18.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea18.Name = "Volts"
        ChartArea18.Position.Auto = False
        ChartArea18.Position.Height = 20.0!
        ChartArea18.Position.Width = 96.0!
        ChartArea18.Position.X = 2.0!
        ChartArea18.Position.Y = 26.0!
        ChartArea19.AxisX.LabelStyle.Enabled = False
        ChartArea19.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea19.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea19.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea19.Name = "Amps"
        ChartArea19.Position.Auto = False
        ChartArea19.Position.Height = 20.0!
        ChartArea19.Position.Width = 95.0!
        ChartArea19.Position.X = 3.0!
        ChartArea19.Position.Y = 47.0!
        ChartArea20.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea20.AxisY.IsLabelAutoFit = False
        ChartArea20.AxisY.LabelStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        ChartArea20.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea20.AxisY.Maximum = 1200.0R
        ChartArea20.AxisY.Minimum = 0.0R
        ChartArea20.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea20.Name = "Thrust"
        ChartArea20.Position.Auto = False
        ChartArea20.Position.Height = 20.0!
        ChartArea20.Position.Width = 96.0!
        ChartArea20.Position.X = 2.0!
        ChartArea20.Position.Y = 67.0!
        Me.chartPowerRails.ChartAreas.Add(ChartArea17)
        Me.chartPowerRails.ChartAreas.Add(ChartArea18)
        Me.chartPowerRails.ChartAreas.Add(ChartArea19)
        Me.chartPowerRails.ChartAreas.Add(ChartArea20)
        Legend5.Enabled = False
        Legend5.IsDockedInsideChartArea = False
        Legend5.Name = "Legend1"
        Legend5.Title = "Key"
        Me.chartPowerRails.Legends.Add(Legend5)
        Me.chartPowerRails.Location = New System.Drawing.Point(0, 0)
        Me.chartPowerRails.Name = "chartPowerRails"
        Series33.ChartArea = "Vcc"
        Series33.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series33.Color = System.Drawing.Color.Red
        Series33.Legend = "Legend1"
        Series33.Name = "Vcc"
        Series34.BorderWidth = 3
        Series34.ChartArea = "Vcc"
        Series34.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series34.Color = System.Drawing.Color.Blue
        Series34.IsVisibleInLegend = False
        Series34.Legend = "Legend1"
        Series34.Name = "VccHighLine"
        Series35.BorderWidth = 3
        Series35.ChartArea = "Vcc"
        Series35.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series35.Color = System.Drawing.Color.Blue
        Series35.IsVisibleInLegend = False
        Series35.Legend = "Legend1"
        Series35.Name = "VccLowLine"
        Series36.ChartArea = "Vcc"
        Series36.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series36.Color = System.Drawing.Color.OliveDrab
        Series36.Legend = "Legend1"
        Series36.Name = "VccOSDLine"
        Series37.ChartArea = "Volts"
        Series37.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series37.Color = System.Drawing.Color.Blue
        Series37.Legend = "Legend1"
        Series37.Name = "Volts"
        Series38.ChartArea = "Amps"
        Series38.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series38.Color = System.Drawing.Color.Fuchsia
        Series38.IsVisibleInLegend = False
        Series38.Legend = "Legend1"
        Series38.Name = "Amps"
        Series39.ChartArea = "Thrust"
        Series39.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series39.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Series39.IsVisibleInLegend = False
        Series39.Legend = "Legend1"
        Series39.Name = "Thrust"
        Me.chartPowerRails.Series.Add(Series33)
        Me.chartPowerRails.Series.Add(Series34)
        Me.chartPowerRails.Series.Add(Series35)
        Me.chartPowerRails.Series.Add(Series36)
        Me.chartPowerRails.Series.Add(Series37)
        Me.chartPowerRails.Series.Add(Series38)
        Me.chartPowerRails.Series.Add(Series39)
        Me.chartPowerRails.Size = New System.Drawing.Size(208, 161)
        Me.chartPowerRails.TabIndex = 33
        Me.chartPowerRails.Text = "Chart1"
        '
        'lblVcc
        '
        Me.lblVcc.AutoSize = True
        Me.lblVcc.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVcc.ForeColor = System.Drawing.Color.Blue
        Me.lblVcc.Location = New System.Drawing.Point(86, 27)
        Me.lblVcc.Name = "lblVcc"
        Me.lblVcc.Size = New System.Drawing.Size(213, 26)
        Me.lblVcc.TabIndex = 34
        Me.lblVcc.Text = "Vcc - APM Voltage"
        '
        'lblVolts
        '
        Me.lblVolts.AutoSize = True
        Me.lblVolts.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVolts.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblVolts.Location = New System.Drawing.Point(86, 188)
        Me.lblVolts.Name = "lblVolts"
        Me.lblVolts.Size = New System.Drawing.Size(265, 26)
        Me.lblVolts.TabIndex = 35
        Me.lblVolts.Text = "Volts - Main Flight Pack"
        '
        'lblAmps
        '
        Me.lblAmps.AutoSize = True
        Me.lblAmps.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmps.ForeColor = System.Drawing.Color.GreenYellow
        Me.lblAmps.Location = New System.Drawing.Point(86, 331)
        Me.lblAmps.Name = "lblAmps"
        Me.lblAmps.Size = New System.Drawing.Size(73, 26)
        Me.lblAmps.TabIndex = 36
        Me.lblAmps.Text = "Amps"
        '
        'lblThrust
        '
        Me.lblThrust.AutoSize = True
        Me.lblThrust.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblThrust.ForeColor = System.Drawing.Color.Red
        Me.lblThrust.Location = New System.Drawing.Point(86, 470)
        Me.lblThrust.Name = "lblThrust"
        Me.lblThrust.Size = New System.Drawing.Size(78, 26)
        Me.lblThrust.TabIndex = 37
        Me.lblThrust.Text = "Thrust"
        '
        'btnGraphs
        '
        Me.btnGraphs.BackgroundImage = CType(resources.GetObject("btnGraphs.BackgroundImage"), System.Drawing.Image)
        Me.btnGraphs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnGraphs.FlatAppearance.BorderSize = 0
        Me.btnGraphs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGraphs.Location = New System.Drawing.Point(152, 6)
        Me.btnGraphs.Name = "btnGraphs"
        Me.btnGraphs.Size = New System.Drawing.Size(64, 69)
        Me.btnGraphs.TabIndex = 39
        Me.btnGraphs.UseVisualStyleBackColor = True
        Me.btnGraphs.Visible = False
        '
        'btnAnalysis
        '
        Me.btnAnalysis.Location = New System.Drawing.Point(152, 30)
        Me.btnAnalysis.Name = "btnAnalysis"
        Me.btnAnalysis.Size = New System.Drawing.Size(75, 23)
        Me.btnAnalysis.TabIndex = 40
        Me.btnAnalysis.Text = "Analysis"
        Me.btnAnalysis.UseVisualStyleBackColor = True
        Me.btnAnalysis.Visible = False
        '
        'chartVibrations
        '
        Me.chartVibrations.BackColor = System.Drawing.Color.Transparent
        ChartArea21.AxisX.LabelStyle.Enabled = False
        ChartArea21.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea21.AxisX.MajorTickMark.Interval = 10.0R
        ChartArea21.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea21.AxisX.Minimum = 0.0R
        ChartArea21.AxisY.IsStartedFromZero = False
        ChartArea21.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea21.AxisY.MajorGrid.Interval = 1.0R
        ChartArea21.AxisY.MajorTickMark.Interval = 1.0R
        ChartArea21.AxisY.Maximum = 5.0R
        ChartArea21.AxisY.Minimum = -5.0R
        ChartArea21.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea21.Name = "AccXY"
        ChartArea21.Position.Auto = False
        ChartArea21.Position.Height = 19.0!
        ChartArea21.Position.Width = 95.0!
        ChartArea21.Position.X = 3.0!
        ChartArea21.Position.Y = 3.0!
        ChartArea22.AxisX.LabelStyle.Enabled = False
        ChartArea22.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea22.AxisX.MajorTickMark.Interval = 10.0R
        ChartArea22.AxisX.Minimum = 0.0R
        ChartArea22.AxisY.Interval = 1.0R
        ChartArea22.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea22.AxisY.MajorGrid.Interval = 1.0R
        ChartArea22.AxisY.MajorTickMark.Interval = 1.0R
        ChartArea22.AxisY.Maximum = -3.0R
        ChartArea22.AxisY.Minimum = -17.0R
        ChartArea22.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea22.Name = "AccZ"
        ChartArea22.Position.Auto = False
        ChartArea22.Position.Height = 20.0!
        ChartArea22.Position.Width = 96.0!
        ChartArea22.Position.X = 2.0!
        ChartArea22.Position.Y = 26.0!
        ChartArea23.AxisX.LabelStyle.Enabled = False
        ChartArea23.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea23.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea23.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea23.Name = "Altitude"
        ChartArea23.Position.Auto = False
        ChartArea23.Position.Height = 20.0!
        ChartArea23.Position.Width = 95.0!
        ChartArea23.Position.X = 3.0!
        ChartArea23.Position.Y = 47.0!
        ChartArea24.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea24.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea24.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea24.Name = "Speed"
        ChartArea24.Position.Auto = False
        ChartArea24.Position.Height = 20.0!
        ChartArea24.Position.Width = 96.0!
        ChartArea24.Position.X = 2.0!
        ChartArea24.Position.Y = 67.0!
        Me.chartVibrations.ChartAreas.Add(ChartArea21)
        Me.chartVibrations.ChartAreas.Add(ChartArea22)
        Me.chartVibrations.ChartAreas.Add(ChartArea23)
        Me.chartVibrations.ChartAreas.Add(ChartArea24)
        Legend6.Enabled = False
        Legend6.IsDockedInsideChartArea = False
        Legend6.Name = "Legend1"
        Legend6.Title = "Key"
        Me.chartVibrations.Legends.Add(Legend6)
        Me.chartVibrations.Location = New System.Drawing.Point(0, 0)
        Me.chartVibrations.Name = "chartVibrations"
        Series40.ChartArea = "AccXY"
        Series40.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series40.Color = System.Drawing.Color.Red
        Series40.Legend = "Legend1"
        Series40.Name = "AccX"
        Series41.ChartArea = "AccXY"
        Series41.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series41.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Series41.Legend = "Legend1"
        Series41.Name = "AccY"
        Series42.BorderWidth = 3
        Series42.ChartArea = "AccXY"
        Series42.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series42.Color = System.Drawing.Color.Blue
        Series42.IsVisibleInLegend = False
        Series42.Legend = "Legend1"
        Series42.Name = "XYHighLine"
        Series43.BorderWidth = 3
        Series43.ChartArea = "AccXY"
        Series43.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series43.Color = System.Drawing.Color.Blue
        Series43.IsVisibleInLegend = False
        Series43.Legend = "Legend1"
        Series43.Name = "XYLowLine"
        Series44.ChartArea = "AccZ"
        Series44.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series44.Color = System.Drawing.Color.Blue
        Series44.Legend = "Legend1"
        Series44.Name = "AccZ"
        Series45.BorderWidth = 3
        Series45.ChartArea = "AccZ"
        Series45.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series45.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Series45.IsVisibleInLegend = False
        Series45.Legend = "Legend1"
        Series45.Name = "ZHighLine"
        Series46.BorderWidth = 3
        Series46.ChartArea = "AccZ"
        Series46.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series46.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Series46.IsVisibleInLegend = False
        Series46.Legend = "Legend1"
        Series46.Name = "ZLowLine"
        Series47.ChartArea = "Altitude"
        Series47.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series47.Color = System.Drawing.Color.Fuchsia
        Series47.IsVisibleInLegend = False
        Series47.Legend = "Legend1"
        Series47.Name = "Altitude"
        Series48.ChartArea = "Speed"
        Series48.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series48.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Series48.IsVisibleInLegend = False
        Series48.Legend = "Legend1"
        Series48.Name = "Speed"
        Me.chartVibrations.Series.Add(Series40)
        Me.chartVibrations.Series.Add(Series41)
        Me.chartVibrations.Series.Add(Series42)
        Me.chartVibrations.Series.Add(Series43)
        Me.chartVibrations.Series.Add(Series44)
        Me.chartVibrations.Series.Add(Series45)
        Me.chartVibrations.Series.Add(Series46)
        Me.chartVibrations.Series.Add(Series47)
        Me.chartVibrations.Series.Add(Series48)
        Me.chartVibrations.Size = New System.Drawing.Size(159, 156)
        Me.chartVibrations.TabIndex = 41
        Me.chartVibrations.Text = "Chart1"
        '
        'lblAbove
        '
        Me.lblAbove.AutoSize = True
        Me.lblAbove.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAbove.ForeColor = System.Drawing.Color.Green
        Me.lblAbove.Location = New System.Drawing.Point(359, 333)
        Me.lblAbove.Name = "lblAbove"
        Me.lblAbove.Size = New System.Drawing.Size(449, 26)
        Me.lblAbove.TabIndex = 48
        Me.lblAbove.Text = "Vibrations only Analysed when above 2m"
        '
        'lblSpeed
        '
        Me.lblSpeed.AutoSize = True
        Me.lblSpeed.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpeed.ForeColor = System.Drawing.Color.Red
        Me.lblSpeed.Location = New System.Drawing.Point(92, 472)
        Me.lblSpeed.Name = "lblSpeed"
        Me.lblSpeed.Size = New System.Drawing.Size(80, 26)
        Me.lblSpeed.TabIndex = 47
        Me.lblSpeed.Text = "Speed"
        '
        'lblAltitude
        '
        Me.lblAltitude.AutoSize = True
        Me.lblAltitude.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAltitude.ForeColor = System.Drawing.Color.Green
        Me.lblAltitude.Location = New System.Drawing.Point(92, 333)
        Me.lblAltitude.Name = "lblAltitude"
        Me.lblAltitude.Size = New System.Drawing.Size(93, 26)
        Me.lblAltitude.TabIndex = 46
        Me.lblAltitude.Text = "Altitude"
        '
        'lblAccZ
        '
        Me.lblAccZ.AutoSize = True
        Me.lblAccZ.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccZ.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAccZ.Location = New System.Drawing.Point(92, 189)
        Me.lblAccZ.Name = "lblAccZ"
        Me.lblAccZ.Size = New System.Drawing.Size(66, 26)
        Me.lblAccZ.TabIndex = 45
        Me.lblAccZ.Text = "AccZ"
        '
        'lblAccXY
        '
        Me.lblAccXY.AutoSize = True
        Me.lblAccXY.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccXY.ForeColor = System.Drawing.Color.Blue
        Me.lblAccXY.Location = New System.Drawing.Point(92, 29)
        Me.lblAccXY.Name = "lblAccXY"
        Me.lblAccXY.Size = New System.Drawing.Size(162, 26)
        Me.lblAccXY.TabIndex = 44
        Me.lblAccXY.Text = "AccX && AccY "
        '
        'lblAccZ_Acceptable
        '
        Me.lblAccZ_Acceptable.AutoSize = True
        Me.lblAccZ_Acceptable.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccZ_Acceptable.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblAccZ_Acceptable.Location = New System.Drawing.Point(491, 189)
        Me.lblAccZ_Acceptable.Name = "lblAccZ_Acceptable"
        Me.lblAccZ_Acceptable.Size = New System.Drawing.Size(317, 26)
        Me.lblAccZ_Acceptable.TabIndex = 43
        Me.lblAccZ_Acceptable.Text = "Z Acceptable Range -15 ~ -5"
        '
        'lblXY_Acceptable
        '
        Me.lblXY_Acceptable.AutoSize = True
        Me.lblXY_Acceptable.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblXY_Acceptable.ForeColor = System.Drawing.Color.Blue
        Me.lblXY_Acceptable.Location = New System.Drawing.Point(479, 29)
        Me.lblXY_Acceptable.Name = "lblXY_Acceptable"
        Me.lblXY_Acceptable.Size = New System.Drawing.Size(329, 26)
        Me.lblXY_Acceptable.TabIndex = 42
        Me.lblXY_Acceptable.Text = "XY Acceptable Range -3 ~ +3"
        '
        'panVibrations
        '
        Me.panVibrations.Controls.Add(Me.lblXY_Acceptable)
        Me.panVibrations.Controls.Add(Me.lblAccXY)
        Me.panVibrations.Controls.Add(Me.lblAccZ)
        Me.panVibrations.Controls.Add(Me.lblOSD)
        Me.panVibrations.Controls.Add(Me.lblSpeed)
        Me.panVibrations.Controls.Add(Me.lblAccZ_Acceptable)
        Me.panVibrations.Controls.Add(Me.lblAbove)
        Me.panVibrations.Controls.Add(Me.lblAltitude)
        Me.panVibrations.Controls.Add(Me.chartVibrations)
        Me.panVibrations.Location = New System.Drawing.Point(480, 100)
        Me.panVibrations.Name = "panVibrations"
        Me.panVibrations.Size = New System.Drawing.Size(159, 156)
        Me.panVibrations.TabIndex = 49
        Me.panVibrations.Visible = False
        '
        'lblOSD
        '
        Me.lblOSD.AutoSize = True
        Me.lblOSD.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOSD.ForeColor = System.Drawing.Color.GreenYellow
        Me.lblOSD.Location = New System.Drawing.Point(680, 77)
        Me.lblOSD.Name = "lblOSD"
        Me.lblOSD.Size = New System.Drawing.Size(119, 17)
        Me.lblOSD.TabIndex = 38
        Me.lblOSD.Text = "OSD Max 5.25v"
        '
        'panPowerRails
        '
        Me.panPowerRails.Controls.Add(Me.lblAmps)
        Me.panPowerRails.Controls.Add(Me.lblVolts)
        Me.panPowerRails.Controls.Add(Me.lblThrust)
        Me.panPowerRails.Controls.Add(Me.lblVcc)
        Me.panPowerRails.Controls.Add(Me.chartPowerRails)
        Me.panPowerRails.Location = New System.Drawing.Point(480, 269)
        Me.panPowerRails.Name = "panPowerRails"
        Me.panPowerRails.Size = New System.Drawing.Size(208, 161)
        Me.panPowerRails.TabIndex = 50
        Me.panPowerRails.Visible = False
        '
        'panAnalysis
        '
        Me.panAnalysis.BackColor = System.Drawing.Color.Black
        Me.panAnalysis.Controls.Add(Me.richtxtLogAnalysis)
        Me.panAnalysis.Location = New System.Drawing.Point(696, 106)
        Me.panAnalysis.Name = "panAnalysis"
        Me.panAnalysis.Size = New System.Drawing.Size(170, 155)
        Me.panAnalysis.TabIndex = 51
        Me.panAnalysis.Visible = False
        '
        'panGraphButtons
        '
        Me.panGraphButtons.BackgroundImage = Global.APMLogFileAnaliser.My.Resources.Resources.left_back
        Me.panGraphButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panGraphButtons.Controls.Add(Me.Label1)
        Me.panGraphButtons.Controls.Add(Me.btnVibrationChart)
        Me.panGraphButtons.Controls.Add(Me.btnPowerChart)
        Me.panGraphButtons.Controls.Add(Me.Label11)
        Me.panGraphButtons.Location = New System.Drawing.Point(233, 87)
        Me.panGraphButtons.Name = "panGraphButtons"
        Me.panGraphButtons.Size = New System.Drawing.Size(225, 602)
        Me.panGraphButtons.TabIndex = 52
        Me.panGraphButtons.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 115)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(170, 24)
        Me.Label1.TabIndex = 49
        Me.Label1.Text = "Work In Progress"
        '
        'btnVibrationChart
        '
        Me.btnVibrationChart.BackgroundImage = Global.APMLogFileAnaliser.My.Resources.Resources.menuback_B_R
        Me.btnVibrationChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnVibrationChart.FlatAppearance.BorderSize = 0
        Me.btnVibrationChart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVibrationChart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVibrationChart.Location = New System.Drawing.Point(21, 61)
        Me.btnVibrationChart.Name = "btnVibrationChart"
        Me.btnVibrationChart.Size = New System.Drawing.Size(177, 42)
        Me.btnVibrationChart.TabIndex = 40
        Me.btnVibrationChart.Text = "Vibrations"
        Me.btnVibrationChart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnVibrationChart.UseVisualStyleBackColor = True
        '
        'btnPowerChart
        '
        Me.btnPowerChart.BackgroundImage = CType(resources.GetObject("btnPowerChart.BackgroundImage"), System.Drawing.Image)
        Me.btnPowerChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPowerChart.FlatAppearance.BorderSize = 0
        Me.btnPowerChart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPowerChart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPowerChart.Location = New System.Drawing.Point(21, 12)
        Me.btnPowerChart.Name = "btnPowerChart"
        Me.btnPowerChart.Size = New System.Drawing.Size(177, 42)
        Me.btnPowerChart.TabIndex = 40
        Me.btnPowerChart.Text = "Power Rails"
        Me.btnPowerChart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPowerChart.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.Red
        Me.Label11.Location = New System.Drawing.Point(65, 522)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(0, 13)
        Me.Label11.TabIndex = 23
        Me.Label11.Visible = False
        '
        'panHelpButtons
        '
        Me.panHelpButtons.Controls.Add(Me.btnDonate)
        Me.panHelpButtons.Controls.Add(Me.btnUpdate)
        Me.panHelpButtons.Controls.Add(Me.btnMCB)
        Me.panHelpButtons.Controls.Add(Me.btnHelp)
        Me.panHelpButtons.Location = New System.Drawing.Point(714, 6)
        Me.panHelpButtons.Name = "panHelpButtons"
        Me.panHelpButtons.Size = New System.Drawing.Size(280, 75)
        Me.panHelpButtons.TabIndex = 53
        '
        'btnParameters
        '
        Me.btnParameters.BackgroundImage = Global.APMLogFileAnaliser.My.Resources.Resources.Parameters1
        Me.btnParameters.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnParameters.FlatAppearance.BorderSize = 0
        Me.btnParameters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnParameters.Location = New System.Drawing.Point(303, 6)
        Me.btnParameters.Name = "btnParameters"
        Me.btnParameters.Size = New System.Drawing.Size(84, 72)
        Me.btnParameters.TabIndex = 54
        Me.btnParameters.UseVisualStyleBackColor = True
        Me.btnParameters.Visible = False
        '
        'frmMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(1006, 695)
        Me.Controls.Add(Me.btnParameters)
        Me.Controls.Add(Me.panHelpButtons)
        Me.Controls.Add(Me.panGraphButtons)
        Me.Controls.Add(Me.panVibrations)
        Me.Controls.Add(Me.panPowerRails)
        Me.Controls.Add(Me.btnAnalysis)
        Me.Controls.Add(Me.btnGraphs)
        Me.Controls.Add(Me.btnCopyText)
        Me.Controls.Add(Me.btnAnalyze)
        Me.Controls.Add(Me.btnLoadLog)
        Me.Controls.Add(Me.panAnalysisButtons)
        Me.Controls.Add(Me.picClickButton)
        Me.Controls.Add(Me.panAnalysis)
        Me.Controls.Add(Me.MenuStrip1)
        Me.ForeColor = System.Drawing.Color.White
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmMainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "APM Flight Log Analysis"
        Me.panAnalysisButtons.ResumeLayout(False)
        Me.panAnalysisButtons.PerformLayout()
        CType(Me.picClickButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartPowerRails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartVibrations, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panVibrations.ResumeLayout(False)
        Me.panVibrations.PerformLayout()
        Me.panPowerRails.ResumeLayout(False)
        Me.panPowerRails.PerformLayout()
        Me.panAnalysis.ResumeLayout(False)
        Me.panGraphButtons.ResumeLayout(False)
        Me.panGraphButtons.PerformLayout()
        Me.panHelpButtons.ResumeLayout(False)
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
    Friend WithEvents lblErrorCount As System.Windows.Forms.Label
    Friend WithEvents lblErrorCountNo As System.Windows.Forms.Label
    Friend WithEvents chkboxDU32 As System.Windows.Forms.CheckBox
    Friend WithEvents chkboxPM As System.Windows.Forms.CheckBox
    Friend WithEvents chkboxVibrations As System.Windows.Forms.CheckBox
    Friend WithEvents chkboxAutoCommands As System.Windows.Forms.CheckBox
    Friend WithEvents panAnalysisButtons As System.Windows.Forms.Panel
    Friend WithEvents btnLoadLog As System.Windows.Forms.Button
    Friend WithEvents btnAnalyze As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnCopyText As System.Windows.Forms.Button
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnMCB As System.Windows.Forms.Button
    Friend WithEvents btnDonate As System.Windows.Forms.Button
    Friend WithEvents picClickButton As System.Windows.Forms.PictureBox
    Friend WithEvents lblEsc As System.Windows.Forms.Label
    Friend WithEvents chartPowerRails As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents lblVcc As System.Windows.Forms.Label
    Friend WithEvents lblVolts As System.Windows.Forms.Label
    Friend WithEvents lblAmps As System.Windows.Forms.Label
    Friend WithEvents lblThrust As System.Windows.Forms.Label
    Friend WithEvents btnGraphs As System.Windows.Forms.Button
    Friend WithEvents btnAnalysis As System.Windows.Forms.Button
    Friend WithEvents chartVibrations As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents lblAbove As System.Windows.Forms.Label
    Friend WithEvents lblSpeed As System.Windows.Forms.Label
    Friend WithEvents lblAltitude As System.Windows.Forms.Label
    Friend WithEvents lblAccZ As System.Windows.Forms.Label
    Friend WithEvents lblAccXY As System.Windows.Forms.Label
    Friend WithEvents lblAccZ_Acceptable As System.Windows.Forms.Label
    Friend WithEvents lblXY_Acceptable As System.Windows.Forms.Label
    Friend WithEvents panVibrations As System.Windows.Forms.Panel
    Friend WithEvents panPowerRails As System.Windows.Forms.Panel
    Friend WithEvents panAnalysis As System.Windows.Forms.Panel
    Friend WithEvents panGraphButtons As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnVibrationChart As System.Windows.Forms.Button
    Friend WithEvents btnPowerChart As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblOSD As System.Windows.Forms.Label
    Friend WithEvents panHelpButtons As System.Windows.Forms.Panel
    Friend WithEvents btnParameters As System.Windows.Forms.Button
End Class
