Public Class Adminfrm

    Private dashcntrl As New Dashboardcntrl()

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
End Class
