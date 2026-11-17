Imports System.Data.SqlClient

Public Class Stafffrm

    Dim itmCntrl As New ItemsCntrl()
    Dim manageClaimCntrl As New ManegClaimCntrl()
    Private Sub Stafffrmvb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sqlconnection()
        selectedControl(itmCntrl, DisplayPnl)
    End Sub

    Private Sub Itmbtn_Click(sender As Object, e As EventArgs) Handles Itmbtn.Click
        selectedControl(itmCntrl, DisplayPnl)
    End Sub

    Private Sub ManageClaimsbtn_Click(sender As Object, e As EventArgs) Handles ManageClaimsbtn.Click
        selectedControl(manageClaimCntrl, DisplayPnl)
    End Sub
End Class