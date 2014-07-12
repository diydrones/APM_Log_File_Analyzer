<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPowerRails
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
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.lblWarning = New System.Windows.Forms.Label()
        Me.richtextLogVibration = New System.Windows.Forms.RichTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Chart1
        '
        Me.Chart1.BackColor = System.Drawing.Color.Transparent
        ChartArea1.AxisX.LabelStyle.Enabled = False
        ChartArea1.AxisX.MajorTickMark.Interval = 10.0R
        ChartArea1.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.AxisX.Minimum = 0.0R
        ChartArea1.AxisY.Interval = 0.2R
        ChartArea1.AxisY.IsStartedFromZero = False
        ChartArea1.AxisY.MajorGrid.Interval = 0.2R
        ChartArea1.AxisY.MajorTickMark.Interval = 1.0R
        ChartArea1.AxisY.Maximum = 5.8R
        ChartArea1.AxisY.Minimum = 4.2R
        ChartArea1.Name = "ChartArea1"
        ChartArea1.Position.Auto = False
        ChartArea1.Position.Height = 22.0!
        ChartArea1.Position.Width = 90.0!
        ChartArea1.Position.X = 3.0!
        ChartArea1.Position.Y = 3.0!
        ChartArea2.AxisX.LabelStyle.Enabled = False
        ChartArea2.AxisX.MajorTickMark.Interval = 10.0R
        ChartArea2.AxisX.Minimum = 0.0R
        ChartArea2.AxisY.Interval = 1.0R
        ChartArea2.AxisY.MajorGrid.Interval = 1.0R
        ChartArea2.AxisY.MajorTickMark.Interval = 1.0R
        ChartArea2.AxisY.Maximum = 17.0R
        ChartArea2.AxisY.Minimum = 9.0R
        ChartArea2.Name = "ChartArea2"
        ChartArea2.Position.Auto = False
        ChartArea2.Position.Height = 22.0!
        ChartArea2.Position.Width = 92.0!
        ChartArea2.Position.X = 1.0!
        ChartArea2.Position.Y = 27.0!
        ChartArea3.AxisX.LabelStyle.Enabled = False
        ChartArea3.Name = "ChartArea3"
        ChartArea3.Position.Auto = False
        ChartArea3.Position.Height = 22.0!
        ChartArea3.Position.Width = 90.0!
        ChartArea3.Position.X = 3.0!
        ChartArea3.Position.Y = 53.0!
        ChartArea4.Name = "ChartArea4"
        ChartArea4.Position.Auto = False
        ChartArea4.Position.Height = 22.0!
        ChartArea4.Position.Width = 90.0!
        ChartArea4.Position.X = 3.0!
        ChartArea4.Position.Y = 78.0!
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Me.Chart1.ChartAreas.Add(ChartArea2)
        Me.Chart1.ChartAreas.Add(ChartArea3)
        Me.Chart1.ChartAreas.Add(ChartArea4)
        Legend1.Enabled = False
        Legend1.IsDockedInsideChartArea = False
        Legend1.Name = "Legend1"
        Legend1.Title = "Key"
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(12, 0)
        Me.Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series1.Color = System.Drawing.Color.Red
        Series1.Legend = "Legend1"
        Series1.Name = "Vcc"
        Series2.BorderWidth = 3
        Series2.ChartArea = "ChartArea1"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series2.Color = System.Drawing.Color.Blue
        Series2.IsVisibleInLegend = False
        Series2.Legend = "Legend1"
        Series2.Name = "VccHighLine"
        Series3.BorderWidth = 3
        Series3.ChartArea = "ChartArea1"
        Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series3.Color = System.Drawing.Color.Blue
        Series3.IsVisibleInLegend = False
        Series3.Legend = "Legend1"
        Series3.Name = "VccLowLine"
        Series4.ChartArea = "ChartArea1"
        Series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series4.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Series4.Legend = "Legend1"
        Series4.Name = "VccOSDLine"
        Series5.ChartArea = "ChartArea2"
        Series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series5.Color = System.Drawing.Color.Blue
        Series5.Legend = "Legend1"
        Series5.Name = "Volts"
        Series6.ChartArea = "ChartArea3"
        Series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series6.Color = System.Drawing.Color.Fuchsia
        Series6.IsVisibleInLegend = False
        Series6.Legend = "Legend1"
        Series6.Name = "Amps"
        Series7.ChartArea = "ChartArea4"
        Series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series7.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Series7.IsVisibleInLegend = False
        Series7.Legend = "Legend1"
        Series7.Name = "Thrust"
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Series.Add(Series2)
        Me.Chart1.Series.Add(Series3)
        Me.Chart1.Series.Add(Series4)
        Me.Chart1.Series.Add(Series5)
        Me.Chart1.Series.Add(Series6)
        Me.Chart1.Series.Add(Series7)
        Me.Chart1.Size = New System.Drawing.Size(984, 685)
        Me.Chart1.TabIndex = 0
        Me.Chart1.Text = "Chart1"
        '
        'lblWarning
        '
        Me.lblWarning.AutoSize = True
        Me.lblWarning.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWarning.ForeColor = System.Drawing.Color.Red
        Me.lblWarning.Location = New System.Drawing.Point(59, 8)
        Me.lblWarning.Name = "lblWarning"
        Me.lblWarning.Size = New System.Drawing.Size(80, 16)
        Me.lblWarning.TabIndex = 3
        Me.lblWarning.Text = "WARNING"
        Me.lblWarning.Visible = False
        '
        'richtextLogVibration
        '
        Me.richtextLogVibration.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.richtextLogVibration.Location = New System.Drawing.Point(12, 8)
        Me.richtextLogVibration.Name = "richtextLogVibration"
        Me.richtextLogVibration.ReadOnly = True
        Me.richtextLogVibration.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.richtextLogVibration.Size = New System.Drawing.Size(41, 25)
        Me.richtextLogVibration.TabIndex = 4
        Me.richtextLogVibration.Text = ""
        Me.richtextLogVibration.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(85, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(213, 26)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Vcc - APM Voltage"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(85, 179)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(265, 26)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Volts - Main Flight Pack"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Green
        Me.Label5.Location = New System.Drawing.Point(85, 361)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(73, 26)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Amps"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(85, 530)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 26)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Thrust"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Green
        Me.Label8.Location = New System.Drawing.Point(862, 64)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(119, 17)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "OSD Max 5.25v"
        '
        'frmPowerRails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 688)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.richtextLogVibration)
        Me.Controls.Add(Me.lblWarning)
        Me.Controls.Add(Me.Chart1)
        Me.Name = "frmPowerRails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Power Rails Chart"
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Chart1 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents lblWarning As System.Windows.Forms.Label
    Friend WithEvents richtextLogVibration As System.Windows.Forms.RichTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
End Class
