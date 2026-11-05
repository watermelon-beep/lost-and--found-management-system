Imports Guna.Charts.WinForms
Imports System.Data.SqlClient
Imports System.Drawing
Public Class Dashboardcntrl

    Private Sub totalLostItems()
        Try
            query = "SELECT COUNT(*) FROM Items WHERE ItemType = 'Lost'"

            Using comm As New SqlCommand(query, conn)

                Dim count As Integer = Convert.ToInt32(comm.ExecuteScalar)

                Label2.Text = count.ToString()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub totalFoundItems()
        Try
            query = "SELECT COUNT(*) FROM Items WHERE ItemType = 'Found'"
            Using comm As New SqlCommand(query, conn)
                Dim count As Integer = Convert.ToInt32(comm.ExecuteScalar)
                Label5.Text = count.ToString()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub totalUnclaimed()
        Try
            query = "SELECT COUNT(*) FROM Items WHERE Status = 'Unclaimed'"
            Using comm As New SqlCommand(query, conn)
                Dim count As Integer = Convert.ToInt32(comm.ExecuteScalar)
                Label8.Text = count.ToString()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub totalClaimed()
        Try
            query = "SELECT COUNT(*) FROM Items WHERE Status = 'Claimed'"
            Using comm As New SqlCommand(query, conn)
                Dim count As Integer = Convert.ToInt32(comm.ExecuteScalar)
                Label15.Text = count.ToString()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Dashboardcntrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        totalLostItems()
        totalFoundItems()
        totalUnclaimed()
        totalClaimed()
    End Sub
End Class
