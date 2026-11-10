Imports Guna.Charts.WinForms
Imports System.Data.SqlClient
Imports System.Drawing
Public Class Dashboardcntrl

    Private Sub dashtable()
        Try
            DashItmtbl.Rows.Clear()

            query = "SELECT * FROM Items"

            Using comm As New SqlCommand(query, conn)
                Using dataRead = comm.ExecuteReader()
                    While dataRead.Read
                        With DashItmtbl.Rows
                            .Add(
                                dataRead("ItemID"),
                                dataRead("ItemName"),
                                dataRead("Category"),
                                dataRead("ItemType"),
                                Convert.ToDateTime(dataRead("DateReported")).ToString("yyyy-MM-dd"),
                                dataRead("Status")
                        )
                        End With
                    End While
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub pieChart()
        Try
            Dim maleCount As Integer = 0
            Dim femaleCount As Integer = 0
            Dim otherCount As Integer = 0
            query = "SELECT Gender, COUNT(*) AS TOTALCOUNT
                    FROM user_table
                    GROUP BY Gender"
            Using comm As New SqlCommand(query, conn)
                Using dataRead = comm.ExecuteReader()

                    While dataRead.Read
                        Dim gender As String = dataRead("Gender").ToString()
                        Dim count As Integer = Convert.ToInt32(dataRead("TOTALCOUNT"))

                        If gender = "Male" Then
                            maleCount = count
                        ElseIf gender = "Female" Then
                            femaleCount = count
                        Else
                            otherCount = count
                        End If
                    End While
                    GunaPieDataset1.DataPoints.Add("Male", maleCount)
                    GunaPieDataset1.DataPoints.Add("Female", femaleCount)
                    GunaPieDataset1.DataPoints.Add("Other", otherCount)
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub stackBarChart()

        Try
            Dim pendingCount As Integer = 0
            Dim rejectedCount As Integer = 0
            Dim approvedCount As Integer = 0

            query = "Select ClaimStatus, COUNT(*) As TOTALCOUNT
                    FROM Claims
                    GROUP BY ClaimStatus"

            Using comm As New SqlCommand(query, conn)

                Using dataRead = comm.ExecuteReader()

                    While dataRead.Read()
                        Dim claimStatus As String = dataRead("ClaimStatus").ToString()
                        Dim count As Integer = Convert.ToInt32(dataRead("TOTALCOUNT"))

                        If claimStatus = "Pending" Then
                            pendingCount = count
                        ElseIf claimStatus = "Rejected" Then
                            rejectedCount = count
                        ElseIf claimStatus = "Approved" Then
                            approvedCount = count
                        End If
                    End While

                    GunaStackedBarDataset1.DataPoints.Add("Pending", pendingCount)
                    GunaStackedBarDataset1.DataPoints.Add("Rejected", rejectedCount)
                    GunaStackedBarDataset1.DataPoints.Add("Approved", approvedCount)
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub dougnutChart()
        Try
            Dim lostCount As Integer = 0
            Dim foundCount As Integer = 0

            query = "SELECT ItemType, COUNT(*) AS TOTALCOUNT
                 FROM Items
                 GROUP BY ItemType"

            Using comm As New SqlCommand(query, conn)

                Using dataRead = comm.ExecuteReader()

                    While dataRead.Read()

                        Dim itemType As String = dataRead("ItemType").ToString()
                        Dim count As Integer = Convert.ToInt32(dataRead("TOTALCOUNT"))

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

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

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
        dashtable()
        totalLostItems()
        totalFoundItems()
        totalUnclaimed()
        totalClaimed()
        dougnutChart()
        stackBarChart()
        pieChart()
    End Sub
End Class
