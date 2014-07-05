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
        Me.SuspendLayout()
        '
        'GLabel2
        '
        Me.GLabel2.Feather = 255
        Me.GLabel2.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.GLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GLabel2.Font = New System.Drawing.Font("Verdana", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GLabel2.ForeColor = System.Drawing.Color.White
        Me.GLabel2.GlowColor = System.Drawing.Color.Black
        Me.GLabel2.Location = New System.Drawing.Point(50, 42)
        Me.GLabel2.Name = "GLabel2"
        Me.GLabel2.Pulse = True
        Me.GLabel2.ShadowColor = System.Drawing.Color.Black
        Me.GLabel2.ShadowOffset = New System.Drawing.Point(6, 5)
        Me.GLabel2.ShadowState = True
        Me.GLabel2.Size = New System.Drawing.Size(463, 42)
        Me.GLabel2.TabIndex = 3
        Me.GLabel2.Text = "ArduPilot Log File Analyzer"
        Me.GLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.GLabel2.UseCompatibleTextRendering = True
        '
        'SplashScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(562, 402)
        Me.Controls.Add(Me.GLabel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SplashScreen"
        Me.Text = "SplashScreen"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GLabel2 As gLabel.gLabel
End Class
