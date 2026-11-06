Imports Guna.Charts.WinForms
Imports System.Data.SqlClient
Imports System.Drawing
Public Class Dashboardcntrl

    Private Sub claimCharts()

        Dim lostCount As Integer = 0
        Dim foundCount As Integer = 0

        query = "SELECT ItemType, COUNT(*) AS TotalCount
             FROM Items
             GROUP BY ItemType"

        Using comm As New SqlCommand(query, conn)

            Using reader As SqlDataReader = comm.ExecuteReader()

                While reader.Read()

                    Dim itemType As String = reader("ItemType").ToString()
                    Dim count As Integer = Convert.ToInt32(reader("TotalCount"))

                    If itemType = "Lost" Then
                        lostCount = count

                    ElseIf itemType = "Found" Then
                        foundCount = count
                    End If

                End While

                GunaDoughnutDataset1.DataPoints.Add("Lost", lostCount)
                GunaDoughnutDataset1.DataPoints.Add("Found", foundCount)

            End Using
        End Using

    End Sub

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
        claimCharts()
    End Sub
End Class
