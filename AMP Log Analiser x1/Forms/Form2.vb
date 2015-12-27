Imports System.Runtime.InteropServices

Public Class frmUpdate
#Region " Functions and Constants "
    <DllImport("user32.dll")>
    Public Shared Function ReleaseCapture() As Boolean
    End Function
    <DllImport("user32.dll")>
    Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function
    Public Const WM_NCLBUTTONDOWN As Integer = &HA1
    Public Const HTBORDER As Integer = 18
    Public Const HTBOTTOM As Integer = 15
    Public Const HTBOTTOMLEFT As Integer = 16
    Public Const HTBOTTOMRIGHT As Integer = 17
    Public Const HTCAPTION As Integer = 2
    Public Const HTLEFT As Integer = 10
    Public Const HTRIGHT As Integer = 11
    Public Const HTTOP As Integer = 12
    Public Const HTTOPLEFT As Integer = 13
    Public Const HTTOPRIGHT As Integer = 14
    Public Const EM_GETLINECOUNT = &HBA
    Public Const EM_LINESCROLL = &HB6
#End Region
#Region " Moving & Resizing methods "
    Public Sub MoveForm()
        ReleaseCapture()
        SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0)
    End Sub
#End Region

    Private Sub SavePosition(ByVal frm As Form, ByVal app_name As String)
        SaveSetting(app_name, "Geometry", "WindowState", frm.WindowState)
        If frm.WindowState = FormWindowState.Normal Then
            SaveSetting(app_name, "Geometry", "Left", frm.Left)
            SaveSetting(app_name, "Geometry", "Top", frm.Top)
            SaveSetting(app_name, "Geometry", "Width", frm.Width)
            SaveSetting(app_name, "Geometry", "Height", frm.Height)
        Else
            SaveSetting(app_name, "Geometry", "Left", frm.RestoreBounds.Left)
            SaveSetting(app_name, "Geometry", "Top", frm.RestoreBounds.Top)
            SaveSetting(app_name, "Geometry", "Width", frm.RestoreBounds.Width)
            SaveSetting(app_name, "Geometry", "Height", frm.RestoreBounds.Height)
        End If
    End Sub
    Private Sub RestorePosition(ByVal frm As Form, ByVal app_name As String)
        frm.SetBounds(
            GetSetting(app_name, "Geometry", "Left", RestoreBounds.Left),
            GetSetting(app_name, "Geometry", "Top", RestoreBounds.Top),
            GetSetting(app_name, "Geometry", "Width", RestoreBounds.Width),
            GetSetting(app_name, "Geometry", "Height", RestoreBounds.Height)
        )
        WindowState = GetSetting(app_name, "Geometry", "WindowState", WindowState)
    End Sub

    Private Sub frmUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RestorePosition(Me, "UpdateFRM")
        ActivateUpdateLabels(False, "", "")
        Width = 320
        Height = 260
        Refresh()
    End Sub

    Private Sub frmUpdate_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Call AutoUpdateHttp(CurrentPublishVersionNumber)
        ' This routine will call the updater but will not wait for the updater to finish before
        ' carrying on to the next line of code. Therefore we need to create a loop until the
        ' update code finishes. It will signal 98 when done.
        While UpdateYesNo <> 98
            Application.DoEvents()
        End While
        Close()
    End Sub

    Private Sub frmUpdate_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        'saves form position
        SavePosition(Me, "UpdateFRM")
        frmMainForm.Show()
    End Sub

    Private Sub lblReviewChanges_Click(sender As Object, e As EventArgs) Handles lblReviewChanges.Click
        ' open the change log in the users web browser
        Process.Start("http://apmloganalyser.x10host.com/versions/ChangeLog_v1.0.html")
        'Process.Start("http://apmloganalyser.net63.net//versions/ChangeLog_v1.0.html")
    End Sub

    Public Sub ActivateUpdateLabels(ByVal Switch As Boolean, ByVal CurrentVersionNo As String, ByVal UpdateVersionNo As String)
        lblCurrentVersionNo.Text = CurrentVersionNo
        lblUpdateVersionNo.Text = UpdateVersionNo
        lblSearchForUpdates.Visible = Not Switch
        lblCurrentVersion.Visible = Switch
        lblUpdateVersion.Visible = Switch
        lblCurrentVersionNo.Visible = Switch
        lblUpdateVersionNo.Visible = Switch
        lblLikeToUpdate.Visible = Switch
        lblReviewChanges.Visible = Switch
        lblUpdateAvailable.Visible = Switch
        picYes.Visible = Switch
        picNo.Visible = Switch
        UpdateYesNo = 99  'signal waiting for response
    End Sub

    Private Sub picYes_Click(sender As Object, e As EventArgs) Handles picYes.Click
        UpdateYesNo = True   'signal we want to update to modUpdateHttp
    End Sub

    Private Sub picNo_Click(sender As Object, e As EventArgs) Handles picNo.Click
        UpdateYesNo = False   'signal we do not want to update to modUpdateHttp
    End Sub

End Class