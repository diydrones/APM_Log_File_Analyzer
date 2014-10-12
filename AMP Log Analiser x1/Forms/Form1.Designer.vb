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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series5 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series6 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series7 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea5 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea6 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea7 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea8 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series8 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series9 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series10 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series11 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series12 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series13 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series14 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series15 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series16 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
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
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
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
        Me.panAnalysisButtons.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.richtxtLogAnalysis.Size = New System.Drawing.Size(778, 596)
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
        Me.btnAnalyze.Visible = False
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
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.APMLogFileAnaliser.My.Resources.Resources.OFD
        Me.PictureBox1.Location = New System.Drawing.Point(82, 10)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(261, 66)
        Me.PictureBox1.TabIndex = 32
        Me.PictureBox1.TabStop = False
        '
        'chartPowerRails
        '
        Me.chartPowerRails.BackColor = System.Drawing.Color.Transparent
        ChartArea1.AxisX.LabelStyle.Enabled = False
        ChartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Maroon
        ChartArea1.AxisX.MajorTickMark.Interval = 10.0R
        ChartArea1.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.AxisX.Minimum = 0.0R
        ChartArea1.AxisY.Interval = 0.2R
        ChartArea1.AxisY.IsLabelAutoFit = False
        ChartArea1.AxisY.IsStartedFromZero = False
        ChartArea1.AxisY.LabelStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        ChartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea1.AxisY.LineColor = System.Drawing.Color.Maroon
        ChartArea1.AxisY.MajorGrid.Interval = 0.2R
        ChartArea1.AxisY.MajorTickMark.Interval = 1.0R
        ChartArea1.AxisY.Maximum = 5.8R
        ChartArea1.AxisY.Minimum = 4.2R
        ChartArea1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea1.Name = "Vcc"
        ChartArea1.Position.Auto = False
        ChartArea1.Position.Height = 19.0!
        ChartArea1.Position.Width = 95.0!
        ChartArea1.Position.X = 3.0!
        ChartArea1.Position.Y = 3.0!
        ChartArea2.AxisX.LabelStyle.Enabled = False
        ChartArea2.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea2.AxisX.MajorTickMark.Interval = 10.0R
        ChartArea2.AxisX.Minimum = 0.0R
        ChartArea2.AxisY.Interval = 1.0R
        ChartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea2.AxisY.MajorGrid.Interval = 1.0R
        ChartArea2.AxisY.MajorTickMark.Interval = 1.0R
        ChartArea2.AxisY.Maximum = 17.5R
        ChartArea2.AxisY.Minimum = 7.5R
        ChartArea2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea2.Name = "Volts"
        ChartArea2.Position.Auto = False
        ChartArea2.Position.Height = 20.0!
        ChartArea2.Position.Width = 96.0!
        ChartArea2.Position.X = 2.0!
        ChartArea2.Position.Y = 26.0!
        ChartArea3.AxisX.LabelStyle.Enabled = False
        ChartArea3.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea3.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea3.Name = "Amps"
        ChartArea3.Position.Auto = False
        ChartArea3.Position.Height = 20.0!
        ChartArea3.Position.Width = 95.0!
        ChartArea3.Position.X = 3.0!
        ChartArea3.Position.Y = 47.0!
        ChartArea4.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea4.AxisY.IsLabelAutoFit = False
        ChartArea4.AxisY.LabelStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        ChartArea4.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea4.AxisY.Maximum = 1200.0R
        ChartArea4.AxisY.Minimum = 0.0R
        ChartArea4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea4.Name = "Thrust"
        ChartArea4.Position.Auto = False
        ChartArea4.Position.Height = 20.0!
        ChartArea4.Position.Width = 96.0!
        ChartArea4.Position.X = 2.0!
        ChartArea4.Position.Y = 67.0!
        Me.chartPowerRails.ChartAreas.Add(ChartArea1)
        Me.chartPowerRails.ChartAreas.Add(ChartArea2)
        Me.chartPowerRails.ChartAreas.Add(ChartArea3)
        Me.chartPowerRails.ChartAreas.Add(ChartArea4)
        Legend1.Enabled = False
        Legend1.IsDockedInsideChartArea = False
        Legend1.Name = "Legend1"
        Legend1.Title = "Key"
        Me.chartPowerRails.Legends.Add(Legend1)
        Me.chartPowerRails.Location = New System.Drawing.Point(3, 3)
        Me.chartPowerRails.Name = "chartPowerRails"
        Series1.ChartArea = "Vcc"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series1.Color = System.Drawing.Color.Red
        Series1.Legend = "Legend1"
        Series1.Name = "Vcc"
        Series2.BorderWidth = 3
        Series2.ChartArea = "Vcc"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series2.Color = System.Drawing.Color.Blue
        Series2.IsVisibleInLegend = False
        Series2.Legend = "Legend1"
        Series2.Name = "VccHighLine"
        Series3.BorderWidth = 3
        Series3.ChartArea = "Vcc"
        Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series3.Color = System.Drawing.Color.Blue
        Series3.IsVisibleInLegend = False
        Series3.Legend = "Legend1"
        Series3.Name = "VccLowLine"
        Series4.ChartArea = "Vcc"
        Series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series4.Color = System.Drawing.Color.OliveDrab
        Series4.Legend = "Legend1"
        Series4.Name = "VccOSDLine"
        Series5.ChartArea = "Volts"
        Series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series5.Color = System.Drawing.Color.Blue
        Series5.Legend = "Legend1"
        Series5.Name = "Volts"
        Series6.ChartArea = "Amps"
        Series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series6.Color = System.Drawing.Color.Fuchsia
        Series6.IsVisibleInLegend = False
        Series6.Legend = "Legend1"
        Series6.Name = "Amps"
        Series7.ChartArea = "Thrust"
        Series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series7.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Series7.IsVisibleInLegend = False
        Series7.Legend = "Legend1"
        Series7.Name = "Thrust"
        Me.chartPowerRails.Series.Add(Series1)
        Me.chartPowerRails.Series.Add(Series2)
        Me.chartPowerRails.Series.Add(Series3)
        Me.chartPowerRails.Series.Add(Series4)
        Me.chartPowerRails.Series.Add(Series5)
        Me.chartPowerRails.Series.Add(Series6)
        Me.chartPowerRails.Series.Add(Series7)
        Me.chartPowerRails.Size = New System.Drawing.Size(794, 691)
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
        Me.btnGraphs.FlatAppearance.BorderSize = 0
        Me.btnGraphs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGraphs.Location = New System.Drawing.Point(152, 6)
        Me.btnGraphs.Name = "btnGraphs"
        Me.btnGraphs.Size = New System.Drawing.Size(75, 70)
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
        ChartArea5.AxisX.LabelStyle.Enabled = False
        ChartArea5.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea5.AxisX.MajorTickMark.Interval = 10.0R
        ChartArea5.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea5.AxisX.Minimum = 0.0R
        ChartArea5.AxisY.IsStartedFromZero = False
        ChartArea5.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea5.AxisY.MajorGrid.Interval = 1.0R
        ChartArea5.AxisY.MajorTickMark.Interval = 1.0R
        ChartArea5.AxisY.Maximum = 5.0R
        ChartArea5.AxisY.Minimum = -5.0R
        ChartArea5.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea5.Name = "AccXY"
        ChartArea5.Position.Auto = False
        ChartArea5.Position.Height = 19.0!
        ChartArea5.Position.Width = 95.0!
        ChartArea5.Position.X = 3.0!
        ChartArea5.Position.Y = 3.0!
        ChartArea6.AxisX.LabelStyle.Enabled = False
        ChartArea6.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea6.AxisX.MajorTickMark.Interval = 10.0R
        ChartArea6.AxisX.Minimum = 0.0R
        ChartArea6.AxisY.Interval = 1.0R
        ChartArea6.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea6.AxisY.MajorGrid.Interval = 1.0R
        ChartArea6.AxisY.MajorTickMark.Interval = 1.0R
        ChartArea6.AxisY.Maximum = -3.0R
        ChartArea6.AxisY.Minimum = -17.0R
        ChartArea6.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea6.Name = "AccZ"
        ChartArea6.Position.Auto = False
        ChartArea6.Position.Height = 20.0!
        ChartArea6.Position.Width = 96.0!
        ChartArea6.Position.X = 2.0!
        ChartArea6.Position.Y = 26.0!
        ChartArea7.AxisX.LabelStyle.Enabled = False
        ChartArea7.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea7.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea7.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea7.Name = "Altitude"
        ChartArea7.Position.Auto = False
        ChartArea7.Position.Height = 20.0!
        ChartArea7.Position.Width = 95.0!
        ChartArea7.Position.X = 3.0!
        ChartArea7.Position.Y = 47.0!
        ChartArea8.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea8.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White
        ChartArea8.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea8.Name = "Speed"
        ChartArea8.Position.Auto = False
        ChartArea8.Position.Height = 20.0!
        ChartArea8.Position.Width = 96.0!
        ChartArea8.Position.X = 2.0!
        ChartArea8.Position.Y = 67.0!
        Me.chartVibrations.ChartAreas.Add(ChartArea5)
        Me.chartVibrations.ChartAreas.Add(ChartArea6)
        Me.chartVibrations.ChartAreas.Add(ChartArea7)
        Me.chartVibrations.ChartAreas.Add(ChartArea8)
        Legend2.Enabled = False
        Legend2.IsDockedInsideChartArea = False
        Legend2.Name = "Legend1"
        Legend2.Title = "Key"
        Me.chartVibrations.Legends.Add(Legend2)
        Me.chartVibrations.Location = New System.Drawing.Point(4, 3)
        Me.chartVibrations.Name = "chartVibrations"
        Series8.ChartArea = "AccXY"
        Series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series8.Color = System.Drawing.Color.Red
        Series8.Legend = "Legend1"
        Series8.Name = "AccX"
        Series9.ChartArea = "AccXY"
        Series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series9.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Series9.Legend = "Legend1"
        Series9.Name = "AccY"
        Series10.BorderWidth = 3
        Series10.ChartArea = "AccXY"
        Series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series10.Color = System.Drawing.Color.Blue
        Series10.IsVisibleInLegend = False
        Series10.Legend = "Legend1"
        Series10.Name = "XYHighLine"
        Series11.BorderWidth = 3
        Series11.ChartArea = "AccXY"
        Series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series11.Color = System.Drawing.Color.Blue
        Series11.IsVisibleInLegend = False
        Series11.Legend = "Legend1"
        Series11.Name = "XYLowLine"
        Series12.ChartArea = "AccZ"
        Series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series12.Color = System.Drawing.Color.Blue
        Series12.Legend = "Legend1"
        Series12.Name = "AccZ"
        Series13.BorderWidth = 3
        Series13.ChartArea = "AccZ"
        Series13.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series13.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Series13.IsVisibleInLegend = False
        Series13.Legend = "Legend1"
        Series13.Name = "ZHighLine"
        Series14.BorderWidth = 3
        Series14.ChartArea = "AccZ"
        Series14.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series14.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Series14.IsVisibleInLegend = False
        Series14.Legend = "Legend1"
        Series14.Name = "ZLowLine"
        Series15.ChartArea = "Altitude"
        Series15.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series15.Color = System.Drawing.Color.Fuchsia
        Series15.IsVisibleInLegend = False
        Series15.Legend = "Legend1"
        Series15.Name = "Altitude"
        Series16.ChartArea = "Speed"
        Series16.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series16.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Series16.IsVisibleInLegend = False
        Series16.Legend = "Legend1"
        Series16.Name = "Speed"
        Me.chartVibrations.Series.Add(Series8)
        Me.chartVibrations.Series.Add(Series9)
        Me.chartVibrations.Series.Add(Series10)
        Me.chartVibrations.Series.Add(Series11)
        Me.chartVibrations.Series.Add(Series12)
        Me.chartVibrations.Series.Add(Series13)
        Me.chartVibrations.Series.Add(Series14)
        Me.chartVibrations.Series.Add(Series15)
        Me.chartVibrations.Series.Add(Series16)
        Me.chartVibrations.Size = New System.Drawing.Size(804, 685)
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
        'frmMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(1006, 695)
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
        Me.Controls.Add(Me.PictureBox1)
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
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
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
End Class
