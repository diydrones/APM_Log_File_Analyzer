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
    'Dim t As Timer = New Timer()

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
        Debug.Print("Loading Splash Screen")
        Debug.Print("About to call the first gLabel")
        GLabel1.Text = MyCurrentVersionNumber
        Debug.Print("First gLabel has been called")

        Me.Refresh()
        'If (ApplicationDeployment.IsNetworkDeployed) Then
        '    Debug.Print("Application is Deployed")
        '    't.Interval = 3000
        '    Threading.Thread.Sleep(3000)

        'Else
        '    Debug.Print("Application is NOT Deployed")
        '    't.Interval = 50
        '    Threading.Thread.Sleep(50)

        'End If
        'Debug.Print("About to request the handler")
        ''AddHandler t.Tick, AddressOf HandleTimerTick

        'Debug.Print("About to sleep")
        'Me.Refresh()
        ''t.Start()
        'Debug.Print("Sleep Finished")
        'Me.Refresh()
        'frmUpdate.Show()
        'Me.Close()
    End Sub
    Private Sub SplashScreen_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        frmUpdate.Show()
    End Sub
    Private Sub HandleTimerTick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'frmMainForm.Show()
        't.stop()
        'frmUpdate.Show()
        'Me.Close()
    End Sub

    Private Sub SplashScreen_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.Refresh()
        If IsDeveloping() = False Then
            Debug.Print("Application is Deployed by End User")
            't.Interval = 3000
            Threading.Thread.Sleep(3000)

        Else
            Debug.Print("Application is running in a GitHub folder / path and assumed to be in development mode")
            't.Interval = 50
            Threading.Thread.Sleep(50)

        End If
        Debug.Print("About to request the handler")
        'AddHandler t.Tick, AddressOf HandleTimerTick

        Debug.Print("About to sleep")
        Me.Refresh()
        't.Start()
        Debug.Print("Sleep Finished")
        Me.Refresh()
        Me.Close()


    End Sub
End Class