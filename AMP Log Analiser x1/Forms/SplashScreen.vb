Imports System
Imports System.Drawing
Imports System.Drawing.Text
Imports System.Drawing.Drawing2D
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.Deployment.Application
Imports gLabel

Public Class SplashScreen
    Dim MyCurrentVersionNumber As String = frmMainForm.BuildVers()

    Private Sub SplashScreen_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        e.Graphics.Clear(Color.FromArgb(255, 0, 0, 0))
        e.Graphics.FillPolygon(Brushes.DarkGreen, _
            New PointF() { _
            New Point(122, 36), New Point(180, 102), _
            New Point(262, 126), New Point(189, 173), _
            New Point(217, 230), New Point(132, 194), _
            New Point(62, 228), New Point(62, 160), _
            New Point(13, 112), New Point(73, 97) _
            })
    End Sub
    Private Sub SplashScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        GLabel1.Text = MyCurrentVersionNumber
        Dim t As Timer = New Timer()
        If (ApplicationDeployment.IsNetworkDeployed) Then
            t.Interval = 5000
        Else
            t.Interval = 50
        End If
        AddHandler t.Tick, AddressOf HandleTimerTick
        t.Start()
        Me.Refresh()
    End Sub
    Private Sub SplashScreen_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

    End Sub
    Private Sub HandleTimerTick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmMainForm.Show()
        Me.Close()
    End Sub

End Class