Imports System.Data.SqlClient

Public Class ManegClaimCntrl

    Private Sub mngClmTbl()
        Claimtbl.Rows.Clear()
        Try
            query = "Select c.ClaimNo, i.ItemID, i.ItemName, c.ClaimantName, c.ClaimantContact, " &
                    "c.ClaimDate, c.ClaimStatus, u.StaffID As ProcessedBy " &
                    "FROM Claims c " &
                    "INNER JOIN Items i On c.ItemNo = i.ItemNo " &
                    "LEFT JOIN user_table u On c.ProcessedBy = u.StaffNo " &
                    "ORDER BY c.ClaimNo"

            Using comm As New SqlCommand(query, conn)
                Using dataRead = comm.ExecuteReader()
                    While dataRead.Read
                        Claimtbl.Rows.Add(
                        dataRead("ClaimNo"),
                        dataRead("ItemID"),
                        dataRead("ClaimantName"),
                        dataRead("ClaimantContact"),
                        dataRead("ClaimDate"),
                        dataRead("ClaimStatus"),
                        dataRead("ProcessedBy"),
                        "Edit",
                        "Delete",
                        "View"
                        )

                    End While
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub unclaimList()
        Try
            query = "SELECT ItemName FROM Items WHERE Status = 'Unclaimed'"

                        Using comm As New SqlCommand(query, conn)
                Using dataRead = comm.ExecuteReader()
                    While dataRead.Read
                        Unclaimcmbx.Items.Add(dataRead("ItemName").ToString())
                    End While

                End Using
            End Using
            Unclaimcmbx.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ManegClaimCntrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        unclaimList()
        mngClmTbl()
    End Sub
End Class
