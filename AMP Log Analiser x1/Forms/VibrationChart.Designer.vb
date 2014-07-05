<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVibrationChart
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
        Dim Series8 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series9 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVibrationChart))
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblWarning = New System.Windows.Forms.Label()
        Me.richtextLogVibration = New System.Windows.Forms.RichTextBox()
        Me.picKey = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picKey, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Chart1
        '
        Me.Chart1.BackColor = System.Drawing.Color.Transparent
        ChartArea1.AxisX.LabelStyle.Enabled = False
        ChartArea1.AxisX.MajorTickMark.Interval = 10.0R
        ChartArea1.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.AxisX.Minimum = 0.0R
        ChartArea1.AxisY.IsStartedFromZero = False
        ChartArea1.AxisY.MajorGrid.Interval = 1.0R
        ChartArea1.AxisY.MajorTickMark.Interval = 1.0R
        ChartArea1.AxisY.Maximum = 5.0R
        ChartArea1.AxisY.Minimum = -5.0R
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
        ChartArea2.AxisY.Maximum = -3.0R
        ChartArea2.AxisY.Minimum = -17.0R
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
        Series1.Name = "AccX"
        Series2.ChartArea = "ChartArea1"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series2.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Series2.Legend = "Legend1"
        Series2.Name = "AccY"
        Series3.BorderWidth = 3
        Series3.ChartArea = "ChartArea1"
        Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series3.Color = System.Drawing.Color.Blue
        Series3.IsVisibleInLegend = False
        Series3.Legend = "Legend1"
        Series3.Name = "XYHighLine"
        Series4.BorderWidth = 3
        Series4.ChartArea = "ChartArea1"
        Series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series4.Color = System.Drawing.Color.Blue
        Series4.IsVisibleInLegend = False
        Series4.Legend = "Legend1"
        Series4.Name = "XYLowLine"
        Series5.ChartArea = "ChartArea2"
        Series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series5.Color = System.Drawing.Color.Blue
        Series5.Legend = "Legend1"
        Series5.Name = "AccZ"
        Series6.BorderWidth = 3
        Series6.ChartArea = "ChartArea2"
        Series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series6.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Series6.IsVisibleInLegend = False
        Series6.Legend = "Legend1"
        Series6.Name = "ZHighLine"
        Series7.BorderWidth = 3
        Series7.ChartArea = "ChartArea2"
        Series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series7.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Series7.IsVisibleInLegend = False
        Series7.Legend = "Legend1"
        Series7.Name = "ZLowLine"
        Series8.ChartArea = "ChartArea3"
        Series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series8.Color = System.Drawing.Color.Fuchsia
        Series8.IsVisibleInLegend = False
        Series8.Legend = "Legend1"
        Series8.Name = "Altitude"
        Series9.ChartArea = "ChartArea4"
        Series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series9.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Series9.IsVisibleInLegend = False
        Series9.Legend = "Legend1"
        Series9.Name = "Speed"
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Series.Add(Series2)
        Me.Chart1.Series.Add(Series3)
        Me.Chart1.Series.Add(Series4)
        Me.Chart1.Series.Add(Series5)
        Me.Chart1.Series.Add(Series6)
        Me.Chart1.Series.Add(Series7)
        Me.Chart1.Series.Add(Series8)
        Me.Chart1.Series.Add(Series9)
        Me.Chart1.Size = New System.Drawing.Size(984, 685)
        Me.Chart1.TabIndex = 0
        Me.Chart1.Text = "Chart1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(619, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(329, 26)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "XY Acceptable Range -3 ~ +3"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(619, 179)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(317, 26)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Z Acceptable Range -15 ~ -5"
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
        'picKey
        '
        Me.picKey.Image = CType(resources.GetObject("picKey.Image"), System.Drawing.Image)
        Me.picKey.InitialImage = CType(resources.GetObject("picKey.InitialImage"), System.Drawing.Image)
        Me.picKey.Location = New System.Drawing.Point(916, 53)
        Me.picKey.Name = "picKey"
        Me.picKey.Size = New System.Drawing.Size(90, 122)
        Me.picKey.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picKey.TabIndex = 5
        Me.picKey.TabStop = False
        Me.picKey.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(85, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(162, 26)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "AccX && AccY "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(85, 179)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 26)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "AccZ"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Green
        Me.Label5.Location = New System.Drawing.Point(85, 361)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 26)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Altitude"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(85, 530)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 26)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Speed"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Green
        Me.Label7.Location = New System.Drawing.Point(499, 361)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(449, 26)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Vibrations only Analysed when above 2m"
        '
        'frmVibrationChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 688)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.picKey)
        Me.Controls.Add(Me.richtextLogVibration)
        Me.Controls.Add(Me.lblWarning)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Chart1)
        Me.Name = "frmVibrationChart"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Vibration Chart"
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picKey, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Chart1 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblWarning As System.Windows.Forms.Label
    Friend WithEvents richtextLogVibration As System.Windows.Forms.RichTextBox
    Friend WithEvents picKey As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
