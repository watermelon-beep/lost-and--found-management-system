Imports System.Data.SqlClient

Public Class LoginFrm

    Private Sub LoginFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sqlconnection()
    End Sub

    Private Function logVerification() As Boolean

        Try
            query = "SELECT UserPassword, AccountStatus FROM user_table WHERE Username = @Username"

            Using comm As New SqlCommand(query, conn)

                comm.Parameters.AddWithValue("@Username", Usrnmtxtbx.Text)

                Using dataRead = comm.ExecuteReader()
                    While dataRead.Read
                        If dataRead("UserPassword").ToString() = LogPasstxbx.Text And dataRead("AccountStatus").ToString() = "Active" Then
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

        If logVerification() Then
            Adminfrm.Show()
            Me.Hide()
        Else
            MsgBox("Enter correct credentials")
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
End Class