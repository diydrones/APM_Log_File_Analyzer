<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHelp
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
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.richtextHelp1 = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Franklin Gothic Medium", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.LimeGreen
        Me.lblTitle.Location = New System.Drawing.Point(106, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(294, 34)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "APM Log File Analyser"
        '
        'richtextHelp1
        '
        Me.richtextHelp1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.richtextHelp1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.richtextHelp1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.richtextHelp1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.richtextHelp1.ForeColor = System.Drawing.Color.LimeGreen
        Me.richtextHelp1.Location = New System.Drawing.Point(12, 60)
        Me.richtextHelp1.Name = "richtextHelp1"
        Me.richtextHelp1.ReadOnly = True
        Me.richtextHelp1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.richtextHelp1.Size = New System.Drawing.Size(480, 231)
        Me.richtextHelp1.TabIndex = 1
        Me.richtextHelp1.TabStop = False
        Me.richtextHelp1.Text = ""
        Me.richtextHelp1.WordWrap = False
        '
        'frmHelp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(504, 303)
        Me.Controls.Add(Me.richtextHelp1)
        Me.Controls.Add(Me.lblTitle)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHelp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "APM Log File Analyser - Help"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents richtextHelp1 As System.Windows.Forms.RichTextBox
End Class
