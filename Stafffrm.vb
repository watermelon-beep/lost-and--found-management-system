Imports System.Data.SqlClient

Public Class Stafffrm

    Dim itmCntrl As New ItemsCntrl()
    Dim manageClaimCntrl As New ManegClaimCntrl()
    Private Sub Stafffrmvb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        sqlconnection()
        selectedControl(itmCntrl, DisplayPnl)
    End Sub

    Private Sub Itmbtn_Click(sender As Object, e As EventArgs) Handles Itmbtn.Click
        selectedControl(itmCntrl, DisplayPnl)
    End Sub

    Private Sub ManageClaimsbtn_Click(sender As Object, e As EventArgs) Handles ManageClaimsbtn.Click
        selectedControl(manageClaimCntrl, DisplayPnl)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label4.Text = DateAndTime.Now.ToString("MMMM dd, yyyy")
        Label5.Text = DateAndTime.Now.ToString("hh:mm tt")
    End Sub
End Class