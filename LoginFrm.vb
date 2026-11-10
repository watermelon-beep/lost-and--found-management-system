Imports System.Data.SqlClient

Public Class LoginFrm

    Dim count As Integer = 0
    Dim time As Integer = 30

    Private Sub LoginFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sqlconnection()
        Timer1.Stop()
    End Sub

    Private Function logVerification() As Boolean

        Try
            query = "SELECT UserPassword, StaffNo, AccountStatus FROM user_table WHERE Username = @Username"

            Using comm As New SqlCommand(query, conn)

                comm.Parameters.AddWithValue("@Username", Usrnmtxtbx.Text)

                Using dataRead = comm.ExecuteReader()
                    While dataRead.Read
                        If dataRead("UserPassword").ToString() = LogPasstxbx.Text And dataRead("AccountStatus").ToString() = "Active" Then
                            currentStaffNo = Convert.ToInt32(dataRead("StaffNo"))
                            currentUserName = Usrnmtxtbx.Text
                            Return True
                        End If
                    End While
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return False

    End Function

    Private Sub LogBtn_Click(sender As Object, e As EventArgs) Handles LogBtn.Click

        If count <> 5 And Not logVerification() Then
            MsgBox("Enter correct credentials")
            count += 1
        Else

        End If

        If logVerification() Then
            Usrnmtxtbx.Clear()
            LogPasstxbx.Clear()
            Adminfrm.Show()
            Me.Hide()
        Else
            If count = 5 And time <> 0 Then
                Timer1.Start()
                MsgBox("Too many failed attempts. Please try again later.")
                MsgBox(TimeSpan.FromSeconds(time).ToString("m\:ss"))
            ElseIf time = 0 Then
                MsgBox("You can now try again.")
                Timer1.Stop()
                time = 30
                count = 0
            End If
            LogPasstxbx.Clear()
        End If
    End Sub
    Dim passVisible As Boolean = False
    Private Sub LogPasstxbx_IconRightClick(sender As Object, e As EventArgs) Handles LogPasstxbx.IconRightClick

        If Not passVisible Then
            LogPasstxbx.IconRight = My.Resources.eye__2_
            LogPasstxbx.PasswordChar = ""
            passVisible = True
        Else
            LogPasstxbx.IconRight = My.Resources.eye_closed__1_
            LogPasstxbx.PasswordChar = "•"
            passVisible = False
        End If
    End Sub

    Private Sub Exitbtn_Click(sender As Object, e As EventArgs) Handles Exitbtn.Click
        Dim confirmResult As DialogResult = MessageBox.Show("Are you sure to exit?", "Confirm Exit", MessageBoxButtons.YesNo)
        If confirmResult = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If time <> 0 Then
            time -= 1
        End If

    End Sub

    Private Sub Usrnmtxtbx_TextChanged(sender As Object, e As EventArgs) Handles Usrnmtxtbx.TextChanged
        If count = 5 Then
            Usrnmtxtbx.Clear()
        End If
    End Sub

    Private Sub LogPasstxbx_TextChanged(sender As Object, e As EventArgs) Handles LogPasstxbx.TextChanged
        If count = 5 Then
            LogPasstxbx.Clear()
        End If
    End Sub
End Class