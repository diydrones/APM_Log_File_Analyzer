<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdate))
        Me.lblSearchForUpdates = New System.Windows.Forms.Label()
        Me.lblUpdateAvailable = New System.Windows.Forms.Label()
        Me.lblCurrentVersion = New System.Windows.Forms.Label()
        Me.lblUpdateVersion = New System.Windows.Forms.Label()
        Me.lblCurrentVersionNo = New System.Windows.Forms.Label()
        Me.lblUpdateVersionNo = New System.Windows.Forms.Label()
        Me.lblLikeToUpdate = New System.Windows.Forms.Label()
        Me.picYes = New System.Windows.Forms.PictureBox()
        Me.picNo = New System.Windows.Forms.PictureBox()
        Me.lblReviewChanges = New System.Windows.Forms.Label()
        CType(Me.picYes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblSearchForUpdates
        '
        Me.lblSearchForUpdates.AutoSize = True
        Me.lblSearchForUpdates.Location = New System.Drawing.Point(10, 9)
        Me.lblSearchForUpdates.Name = "lblSearchForUpdates"
        Me.lblSearchForUpdates.Size = New System.Drawing.Size(122, 13)
        Me.lblSearchForUpdates.TabIndex = 1
        Me.lblSearchForUpdates.Text = "Searching for Updates..."
        '
        'lblUpdateAvailable
        '
        Me.lblUpdateAvailable.AutoSize = True
        Me.lblUpdateAvailable.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUpdateAvailable.Location = New System.Drawing.Point(10, 9)
        Me.lblUpdateAvailable.Name = "lblUpdateAvailable"
        Me.lblUpdateAvailable.Size = New System.Drawing.Size(113, 16)
        Me.lblUpdateAvailable.TabIndex = 2
        Me.lblUpdateAvailable.Text = "Update Available"
        '
        'lblCurrentVersion
        '
        Me.lblCurrentVersion.AutoSize = True
        Me.lblCurrentVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentVersion.Location = New System.Drawing.Point(57, 48)
        Me.lblCurrentVersion.Name = "lblCurrentVersion"
        Me.lblCurrentVersion.Size = New System.Drawing.Size(102, 16)
        Me.lblCurrentVersion.TabIndex = 3
        Me.lblCurrentVersion.Text = "Current Version:"
        '
        'lblUpdateVersion
        '
        Me.lblUpdateVersion.AutoSize = True
        Me.lblUpdateVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUpdateVersion.Location = New System.Drawing.Point(57, 72)
        Me.lblUpdateVersion.Name = "lblUpdateVersion"
        Me.lblUpdateVersion.Size = New System.Drawing.Size(105, 16)
        Me.lblUpdateVersion.TabIndex = 4
        Me.lblUpdateVersion.Text = "Update Version:"
        '
        'lblCurrentVersionNo
        '
        Me.lblCurrentVersionNo.AutoSize = True
        Me.lblCurrentVersionNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentVersionNo.Location = New System.Drawing.Point(165, 48)
        Me.lblCurrentVersionNo.Name = "lblCurrentVersionNo"
        Me.lblCurrentVersionNo.Size = New System.Drawing.Size(52, 16)
        Me.lblCurrentVersionNo.TabIndex = 5
        Me.lblCurrentVersionNo.Text = "v0.0.0.0"
        '
        'lblUpdateVersionNo
        '
        Me.lblUpdateVersionNo.AutoSize = True
        Me.lblUpdateVersionNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUpdateVersionNo.Location = New System.Drawing.Point(165, 72)
        Me.lblUpdateVersionNo.Name = "lblUpdateVersionNo"
        Me.lblUpdateVersionNo.Size = New System.Drawing.Size(52, 16)
        Me.lblUpdateVersionNo.TabIndex = 6
        Me.lblUpdateVersionNo.Text = "v0.0.0.0"
        '
        'lblLikeToUpdate
        '
        Me.lblLikeToUpdate.AutoSize = True
        Me.lblLikeToUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLikeToUpdate.Location = New System.Drawing.Point(12, 106)
        Me.lblLikeToUpdate.Name = "lblLikeToUpdate"
        Me.lblLikeToUpdate.Size = New System.Drawing.Size(162, 16)
        Me.lblLikeToUpdate.TabIndex = 7
        Me.lblLikeToUpdate.Text = "Would you like to update?"
        '
        'picYes
        '
        Me.picYes.Image = CType(resources.GetObject("picYes.Image"), System.Drawing.Image)
        Me.picYes.Location = New System.Drawing.Point(35, 142)
        Me.picYes.Name = "picYes"
        Me.picYes.Size = New System.Drawing.Size(100, 50)
        Me.picYes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picYes.TabIndex = 8
        Me.picYes.TabStop = False
        '
        'picNo
        '
        Me.picNo.Image = CType(resources.GetObject("picNo.Image"), System.Drawing.Image)
        Me.picNo.Location = New System.Drawing.Point(168, 142)
        Me.picNo.Name = "picNo"
        Me.picNo.Size = New System.Drawing.Size(100, 50)
        Me.picNo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picNo.TabIndex = 9
        Me.picNo.TabStop = False
        '
        'lblReviewChanges
        '
        Me.lblReviewChanges.AutoSize = True
        Me.lblReviewChanges.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReviewChanges.Location = New System.Drawing.Point(204, 109)
        Me.lblReviewChanges.Name = "lblReviewChanges"
        Me.lblReviewChanges.Size = New System.Drawing.Size(88, 13)
        Me.lblReviewChanges.TabIndex = 10
        Me.lblReviewChanges.Text = "Review Changes"
        '
        'frmUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(304, 221)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblReviewChanges)
        Me.Controls.Add(Me.picNo)
        Me.Controls.Add(Me.picYes)
        Me.Controls.Add(Me.lblLikeToUpdate)
        Me.Controls.Add(Me.lblUpdateVersionNo)
        Me.Controls.Add(Me.lblCurrentVersionNo)
        Me.Controls.Add(Me.lblUpdateVersion)
        Me.Controls.Add(Me.lblCurrentVersion)
        Me.Controls.Add(Me.lblUpdateAvailable)
        Me.Controls.Add(Me.lblSearchForUpdates)
        Me.ForeColor = System.Drawing.Color.LimeGreen
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUpdate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "APM Log File Analiser"
        Me.TopMost = True
        CType(Me.picYes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSearchForUpdates As System.Windows.Forms.Label
    Friend WithEvents lblUpdateAvailable As Label
    Friend WithEvents lblCurrentVersion As Label
    Friend WithEvents lblUpdateVersion As Label
    Friend WithEvents lblCurrentVersionNo As Label
    Friend WithEvents lblUpdateVersionNo As Label
    Friend WithEvents lblLikeToUpdate As Label
    Friend WithEvents picYes As PictureBox
    Friend WithEvents picNo As PictureBox
    Friend WithEvents lblReviewChanges As Label
End Class
