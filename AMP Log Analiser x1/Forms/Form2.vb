Imports System.IO
Imports System.Runtime.InteropServices

Public Class frmUpdate
#Region " Functions and Constants "
    <DllImport("user32.dll")> _
    Public Shared Function ReleaseCapture() As Boolean
    End Function
    <DllImport("user32.dll")> _
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
#End Region
#Region " Moving & Resizing methods "
    Public Sub MoveForm()
        ReleaseCapture()
        SendMessage(Me.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0)
    End Sub
#End Region
    Public Const EM_GETLINECOUNT = &HBA
    Public Const EM_LINESCROLL = &HB6
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
        frm.SetBounds( _
            GetSetting(app_name, "Geometry", "Left", Me.RestoreBounds.Left), _
            GetSetting(app_name, "Geometry", "Top", Me.RestoreBounds.Top), _
            GetSetting(app_name, "Geometry", "Width", Me.RestoreBounds.Width), _
            GetSetting(app_name, "Geometry", "Height", Me.RestoreBounds.Height) _
        )
        Me.WindowState = GetSetting(app_name, "Geometry", "WindowState", Me.WindowState)
    End Sub
    Private Sub frmUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RestorePosition(Me, "UpdateFRM")
    End Sub
    Private Sub frmUpdate_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        'saves form position
        SavePosition(Me, "UpdateFRM")
    End Sub
End Class