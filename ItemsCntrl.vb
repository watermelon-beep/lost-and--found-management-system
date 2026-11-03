Imports System.Data.SqlClient
Imports System.IO

Public Class ItemsCntrl

    Private Sub viewItemTable()
        Try
            Itemtbl.Rows.Clear()

            query = "SELECT ItemID, 
                            ItemName, 
                            Category, 
                            ItemType,
                            Location,
                            DateLostFound, 
                            DateReported, 
                            PersonName,
                            ContactNumber,
                            Status
                     FROM Items"

            Using comm As New SqlCommand(query, conn)
                comm.CommandTimeout = 120

                Using dataRead = comm.ExecuteReader

                    While dataRead.Read
                        With Itemtbl.Rows
                            .Add(
                                    dataRead("ItemID"),
                                    dataRead("ItemName"),
                                    dataRead("Category"),
                                    dataRead("ItemType"),
                                    dataRead("Location").ToString(),
                                    If(IsDBNull(dataRead("DateLostFound")), "", Convert.ToDateTime(dataRead("DateLostFound")).ToString("yyyy-MM-dd")),
                                    Convert.ToDateTime(dataRead("DateReported")).ToString("yyyy-MM-dd"),
                                    dataRead("PersonName").ToString(),
                                    dataRead("ContactNumber").ToString(),
                                    dataRead("Status"),
                                    "Edit",
                                    "Delete",
                                    "View"
                            )
                        End With
                    End While

                End Using
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private lostcntrl As New LostCntrl()
    Private Sub LostBtn_Click(sender As Object, e As EventArgs) Handles LostBtn.Click
        selectedControl(lostcntrl, Guna2Panel5)
    End Sub

    Private Sub ItemsCntrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        selectedControl(lostcntrl, Guna2Panel5)
        viewItemTable()
    End Sub

    Private Sub Itemtbl_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Itemtbl.CellContentClick

    End Sub
End Class
