Module modMSG_Checks_v3_3

    Public Sub MSG_Checks_v3_3()

        ' v3.3 - FMT, 133, 75, MSG, QZ, TimeUS,Message

        ' This code was only added to detect the "New Mission" list at the
        ' beginning of v3.3 logs

        If UCase(DataArray(2)) = "NEW" Then
            AUTO_NewMission = True
            Debug.Print("Detected New Mission List")
        End If

    End Sub

End Module
