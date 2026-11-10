Imports System.Data.SqlClient
Imports Guna.UI2.WinForms

Public Class Adminfrm

    Private dashcntrl As New Dashboardcntrl()
    Private itemCntrl As New ItemsCntrl()
    Private usrmanagement As New UserManagement()

    Private Sub ViewPhoto(username As String, images As Guna2CirclePictureBox)
        Try
            query = "SELECT UserImg FROM user_table WHERE Username = @Username"

            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@Username", username)
                Using dataRead = comm.ExecuteReader()
                    If dataRead.Read AndAlso Not IsDBNull(dataRead("UserImg")) Then
                        Dim img() As Byte = CType(dataRead("UserImg"), Byte())
                        If img.Length > 0 Then
                            Using MS As New IO.MemoryStream(img)
                                Using temp As Image = Image.FromStream(MS)
                                    images.Image = New Bitmap(temp)
                                End Using
                            End Using
                            images.BackgroundImage = Nothing
                        Else
                            images.Image = Nothing
                        End If
                    Else
                        images.Image = Nothing
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label4.Text = DateAndTime.Now.ToString("MMMM dd, yyyy")
        Label5.Text = DateAndTime.Now.ToString("hh:mm tt")
    End Sub
    Private Sub Itmbtn_Click(sender As Object, e As EventArgs) Handles Itmbtn.Click
        selectedControl(itemCntrl, DisplayPnl)
        Label1.Text = "Items"
    End Sub

    Private Sub DisplayPnl_Paint(sender As Object, e As PaintEventArgs) Handles DisplayPnl.Paint

    End Sub

    Private Sub Usrsbtn_Click(sender As Object, e As EventArgs) Handles Usrsbtn.Click
        selectedControl(usrmanagement, DisplayPnl)
        Label1.Text = "User Management"
    End Sub

    Private Sub Adminfrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        sqlconnection()
        selectedControl(dashcntrl, DisplayPnl)
        ViewPhoto(currentUserName, UserPrfl)
    End Sub

    Private Sub Adminfrm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()
    End Sub

    Private Sub ManageClaimsbtn_Click(sender As Object, e As EventArgs) Handles ManageClaimsbtn.Click
        Dim mngClms As New ManegClaimCntrl()
        selectedControl(mngClms, DisplayPnl)
        Label1.Text = "Manage Claims"
    End Sub

    Private Sub Logoutbtn_Click(sender As Object, e As EventArgs) Handles Logoutbtn.Click
        LoginFrm.Show()
        Me.Hide()
    End Sub

    Private Sub Dashboardbtn_Click(sender As Object, e As EventArgs) Handles Dashboardbtn.Click
        selectedControl(dashcntrl, DisplayPnl)
        Label1.Text = "Dashboard"
    End Sub

    Private Sub Reportbtn_Click(sender As Object, e As EventArgs) Handles Reportbtn.Click
        Label1.Text = "Reports"
    End Sub
End Class
