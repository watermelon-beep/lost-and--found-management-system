Public Class Adminfrm

    Private dashcntrl As New Dashboardcntrl()
    Private itemCntrl As New ItemsCntrl()
    Private usrmanagement As New UserManagement()
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label4.Text = DateAndTime.Now.ToString("MMMM dd, yyyy")
        Label5.Text = DateAndTime.Now.ToString("hh:mm tt")
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()

        selectedControl(dashcntrl, DisplayPnl)
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        selectedControl(dashcntrl, DisplayPnl)
    End Sub

    Private Sub Itmbtn_Click(sender As Object, e As EventArgs) Handles Itmbtn.Click
        selectedControl(itemCntrl, DisplayPnl)
    End Sub

    Private Sub Usrsbtn_Click(sender As Object, e As EventArgs) Handles Usrsbtn.Click


        selectedControl(usrmanagement, DisplayPnl)
    End Sub
End Class
