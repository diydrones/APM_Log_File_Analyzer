Public Class frmHelp

    Private Sub frmHelp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strTempText As String = ""
        strTempText = strTempText & "       AMP Log Analyser is Free to use, but if you" & vbNewLine
        strTempText = strTempText & "     find it useful then please send a small donation" & vbNewLine
        strTempText = strTempText & "       using the Pay Pal button as every bit helps :)" & vbNewLine
        strTempText = strTempText & vbNewLine
        strTempText = strTempText & "AMP Log Analyser should not be sold or used for commercial" & vbNewLine
        strTempText = strTempText & "    purposes without written permission etc. etc. etc." & vbNewLine
        strTempText = strTempText & vbNewLine
        strTempText = strTempText & "    For more support and instructions please visit the" & vbNewLine
        strTempText = strTempText & "               RC Groups dedicated thread." & vbNewLine
        strTempText = strTempText & "http://www.rcgroups.com/forums/showthread.php?t=2151318" & vbNewLine
        strTempText = strTempText & vbNewLine
        strTempText = strTempText & "              Thank you for your support :)" & vbNewLine
        strTempText = strTempText & "                 ---Version: " & MyCurrentVersionNumber & vbNewLine
        richtextHelp1.AppendText(strTempText)
        lblTitle.Focus()
    End Sub

    Private Sub richtextHelp1_LinkClicked(sender As Object, e As LinkClickedEventArgs) Handles richtextHelp1.LinkClicked
        Me.Close()
        System.Diagnostics.Process.Start(e.LinkText)
    End Sub
End Class