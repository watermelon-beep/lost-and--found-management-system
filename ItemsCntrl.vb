Public Class ItemsCntrl

    Private lostcntrl As New LostCntrl()
    Private Sub LostBtn_Click(sender As Object, e As EventArgs) Handles LostBtn.Click
        selectedControl(lostcntrl, Guna2Panel5)
    End Sub

    Private Sub ItemsCntrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        selectedControl(lostcntrl, Guna2Panel5)
    End Sub
End Class
