<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplashScreen
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
        Me.GLabel2 = New gLabel.gLabel()
        Me.GLabel1 = New gLabel.gLabel()
        Me.GLabel3 = New gLabel.gLabel()
        Me.GLabel4 = New gLabel.gLabel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GLabel2
        '
        Me.GLabel2.BackColor = System.Drawing.Color.Transparent
        Me.GLabel2.CheckedColor = System.Drawing.Color.Transparent
        Me.GLabel2.Feather = 255
        Me.GLabel2.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.GLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GLabel2.Font = New System.Drawing.Font("Verdana", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLabel2.ForeColor = System.Drawing.Color.White
        Me.GLabel2.Glow = 5
        Me.GLabel2.GlowColor = System.Drawing.Color.Black
        Me.GLabel2.Location = New System.Drawing.Point(12, 9)
        Me.GLabel2.MouseOverColor = System.Drawing.Color.Transparent
        Me.GLabel2.Name = "GLabel2"
        Me.GLabel2.Pulse = True
        Me.GLabel2.ShadowColor = System.Drawing.Color.DimGray
        Me.GLabel2.ShadowOffset = New System.Drawing.Point(6, 5)
        Me.GLabel2.ShadowState = True
        Me.GLabel2.Size = New System.Drawing.Size(586, 62)
        Me.GLabel2.TabIndex = 3
        Me.GLabel2.Text = "ArduPilot Log File Analyzer"
        Me.GLabel2.UseCompatibleTextRendering = True
        '
        'GLabel1
        '
        Me.GLabel1.BackColor = System.Drawing.Color.Transparent
        Me.GLabel1.ForeColor = System.Drawing.Color.Yellow
        Me.GLabel1.Location = New System.Drawing.Point(64, 166)
        Me.GLabel1.MouseOverForeColor = System.Drawing.Color.Yellow
        Me.GLabel1.Name = "GLabel1"
        Me.GLabel1.Size = New System.Drawing.Size(114, 34)
        Me.GLabel1.TabIndex = 4
        Me.GLabel1.Text = "GLabel1"
        '
        'GLabel3
        '
        Me.GLabel3.BackColor = System.Drawing.Color.Transparent
        Me.GLabel3.CheckedColor = System.Drawing.Color.Transparent
        Me.GLabel3.Feather = 255
        Me.GLabel3.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.GLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GLabel3.Font = New System.Drawing.Font("Verdana", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(134, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GLabel3.Glow = 14
        Me.GLabel3.GlowColor = System.Drawing.Color.Black
        Me.GLabel3.Location = New System.Drawing.Point(234, 121)
        Me.GLabel3.MouseOverColor = System.Drawing.Color.Transparent
        Me.GLabel3.Name = "GLabel3"
        Me.GLabel3.Pulse = True
        Me.GLabel3.ShadowColor = System.Drawing.Color.Black
        Me.GLabel3.ShadowOffset = New System.Drawing.Point(6, 5)
        Me.GLabel3.Size = New System.Drawing.Size(269, 101)
        Me.GLabel3.TabIndex = 5
        Me.GLabel3.Text = "Making logs readable for everyone"
        Me.GLabel3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.GLabel3.UseCompatibleTextRendering = True
        '
        'GLabel4
        '
        Me.GLabel4.BackColor = System.Drawing.Color.Transparent
        Me.GLabel4.BorderColor = System.Drawing.Color.CornflowerBlue
        Me.GLabel4.CheckedColor = System.Drawing.Color.Transparent
        Me.GLabel4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLabel4.ForeColor = System.Drawing.Color.AliceBlue
        Me.GLabel4.Location = New System.Drawing.Point(49, 287)
        Me.GLabel4.Name = "GLabel4"
        Me.GLabel4.ShadowColor = System.Drawing.Color.Transparent
        Me.GLabel4.Size = New System.Drawing.Size(534, 40)
        Me.GLabel4.TabIndex = 6
        Me.GLabel4.Text = "Copyright 2014 Kevin Guest. All rights reserved." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "With special thanks to Craig Wo" & _
    "rden. * now with more cowbell!"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.APMLogFileAnaliser.My.Resources.Resources.loggerwindow
        Me.PictureBox1.Location = New System.Drawing.Point(67, 101)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(111, 74)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'SplashScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(606, 340)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.GLabel4)
        Me.Controls.Add(Me.GLabel3)
        Me.Controls.Add(Me.GLabel1)
        Me.Controls.Add(Me.GLabel2)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SplashScreen"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SplashScreen"
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.Black
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GLabel2 As gLabel.gLabel
    Friend WithEvents GLabel1 As gLabel.gLabel
    Friend WithEvents GLabel3 As gLabel.gLabel
    Friend WithEvents GLabel4 As gLabel.gLabel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
