Imports System.Data.SqlClient
Imports System.IO
Imports Guna.UI2.WinForms

Public Class ManegClaimCntrl

    Private Sub ClearField()
        PersonNameTxt.Clear()
        ContactTxt.Clear()
        Unclaimcmbx.SelectedIndex = 0
        StatusCmb.SelectedIndex = 0
        ClaimDate.Value = DateTime.Now
        ItemPic.Image = Nothing
    End Sub

    Private Sub ViewItemManageClaim(id As Integer)
        Try
            query = "SELECT i.ItemID, i.ItemName, c.ClaimantName, c.ClaimantContact, 
                    c.ClaimDate, c.ClaimStatus, u.StaffID As ProcessedBy, ProofOfOwnership
                    FROM Claims c
                    INNER JOIN Items i On c.ItemNo = i.ItemNo
                    LEFT JOIN user_table u On c.ProcessedBy = u.StaffNo
                    WHERE ClaimNo = @ClaimNo
                    ORDER BY c.ClaimNo"

            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@ClaimNo", id)

                Using dataRead = comm.ExecuteReader()
                    While dataRead.Read
                        Label7.Text = dataRead("ClaimStatus").ToString()
                        Label8.Text = dataRead("ItemID").ToString()
                        Label1.Text = dataRead("ClaimantName").ToString()
                        Label4.Text = dataRead("ClaimantContact").ToString()
                        Label9.Text = dataRead("ProofOfOwnership").ToString()
                        Label2.Text = dataRead("ProcessedBy").ToString()
                        Label6.Text = dataRead("ClaimDate").ToString()
                    End While
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FilterStatus(Status As String)
        Try
            Claimtbl.Rows.Clear()
            query = "Select c.ClaimNo, i.ItemID, i.ItemName, c.ClaimantName, c.ClaimantContact, 
                    c.ClaimDate, c.ClaimStatus, u.StaffID As ProcessedBy
                    FROM Claims c
                    INNER JOIN Items i On c.ItemNo = i.ItemNo
                    LEFT JOIN user_table u On c.ProcessedBy = u.StaffNo
                    WHERE ClaimStatus = @ClaimStatus
                    ORDER BY c.ClaimNo"

            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@ClaimStatus", Status)

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

    Private Sub UpdateClaim(id As Integer)

        Try
            Dim imgData As Object = DBNull.Value
            If ItemPic.Image IsNot Nothing Then
                Using ms As New MemoryStream
                    ItemPic.Image.Save(ms, Imaging.ImageFormat.Png)
                    imgData = ms.ToArray()
                End Using
            End If

            query = "UPDATE Claims SET
                     ItemNo = (SELECT ItemNo FROM Items WHERE ItemID = @ItemID),
                     ClaimantName = @Name,
                     ClaimantContact = @Contact,
                     ProofOfOwnership = @Proof,
                     ClaimDate = @Date,
                     ClaimStatus = @ClaimStatus,
                     ProcessedBy = @User,
                     ProofImage = @Image
                     WHERE ClaimNo = @ClaimNo"

            Using comm As New SqlCommand(query, conn)
                With comm.Parameters
                    .AddWithValue("@ClaimNo", id)
                    .AddWithValue("@ItemID", selectedItemId)
                    .AddWithValue("@Name", PersonNameTxt.Text)
                    .AddWithValue("@Contact", ContactTxt.Text)
                    .AddWithValue("@Proof", ProofOfOwnTxt.Text)
                    .AddWithValue("@Date", ClaimDate.Value)
                    .AddWithValue("@ClaimStatus", StatusCmb.Text)
                    .AddWithValue("@User", currentStaffNo)
                    .Add("@Image", SqlDbType.VarBinary, -1).Value = imgData
                End With

                Dim rowsAffected As Integer = comm.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    MsgBox("Claim updated successfully!")
                    ClearField()
                    unclaimList()
                Else
                    MsgBox("No claim record was found.")
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub UpdateAction(id As Integer)
        Try
            query = "SELECT * FROM Claims WHERE ClaimNo = @ClaimNo"

            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@ClaimNo", id)

                Using dataRead = comm.ExecuteReader()

                    If dataRead.Read() Then
                        PersonNameTxt.Text = dataRead("ClaimantName").ToString()
                        ContactTxt.Text = dataRead("ClaimantContact").ToString()
                        ProofOfOwnTxt.Text = dataRead("ProofOfOwnership").ToString()
                        ClaimDate.Value = Convert.ToDateTime(dataRead("ClaimDate"))
                        StatusCmb.Text = dataRead("ClaimStatus").ToString()
                        ProofOfOwnTxt.Text = dataRead("ProofOfOwnership").ToString()
                    End If
                    selectedClaimId = id
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ViewPhoto(id As Integer, images As Guna2PictureBox)
        Try
            query = "SELECT ProofImage FROM Claims WHERE ClaimNo = @ClaimNo"

            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@ClaimNo", id)
                Using dataRead = comm.ExecuteReader()
                    If dataRead.Read AndAlso Not IsDBNull(dataRead("ProofImage")) Then
                        Dim img() As Byte = CType(dataRead("ProofImage"), Byte())
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

    Private Sub deleteClaim(id As Integer)
        Try
            query = "DELETE FROM Claims WHERE ClaimNo = @ClaimNo"

            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@ClaimNo", id)

                Dim rowsAffected As Integer = comm.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    MsgBox("Item deleted successfully.")
                    mngClmTbl()
                Else
                    MsgBox("No item found with the specified ItemID.")
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub AddClaim()
        Try

            Dim imgData As Object = DBNull.Value
            If ItemPic.Image IsNot Nothing Then
                Using ms As New MemoryStream
                    ItemPic.Image.Save(ms, Imaging.ImageFormat.Png)
                    imgData = ms.ToArray()
                End Using
            End If

            query = "INSERT INTO Claims (ItemNo, ClaimantName, ClaimantContact, ProofOfOwnership,
                     ClaimDate, ClaimStatus, ProcessedBy, ProofImage)
                     VALUES ((SELECT ItemNo FROM Items WHERE ItemID = @ItemID),
                     @Name, @Contact, @Proof, @Date, @ClaimStatus, @User, @Image)"

            Using comm As New SqlCommand(query, conn)
                With comm.Parameters
                    .AddWithValue("@ItemId", selectedItemId)
                    .AddWithValue("@Name", PersonNameTxt.Text)
                    .AddWithValue("@Contact", ContactTxt.Text)
                    .AddWithValue("@Proof", ProofOfOwnTxt.Text)
                    .AddWithValue("@Date", ClaimDate.Value)
                    .AddWithValue("@ClaimStatus", StatusCmb.Text)
                    .AddWithValue("@User", currentStaffNo)
                    .Add("@Image", SqlDbType.VarBinary, -1).Value = imgData
                End With
                comm.ExecuteNonQuery()
                MsgBox("added")
                ClearField()
                unclaimList()

            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub mngClmTbl()
        Claimtbl.Rows.Clear()
        Try
            query = "Select c.ClaimNo, i.ItemID, i.ItemName, c.ClaimantName, c.ClaimantContact, 
                    c.ClaimDate, c.ClaimStatus, u.StaffID As ProcessedBy
                    FROM Claims c
                    INNER JOIN Items i On c.ItemNo = i.ItemNo
                    LEFT JOIN user_table u On c.ProcessedBy = u.StaffNo
                    ORDER BY c.ClaimNo"

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

    Dim selectedItemId As String = ""
    Private Sub unclaimList()
        Try
            Unclaimcmbx.Items.Clear()

            query = "SELECT ItemName, ItemID FROM Items WHERE Status = 'Unclaimed'"

            Using comm As New SqlCommand(query, conn)
                Using dataRead = comm.ExecuteReader()
                    While dataRead.Read
                        Unclaimcmbx.Items.Add(dataRead("ItemID").ToString() & " - " & dataRead("ItemName").ToString())

                        selectedItemId = dataRead("ItemID").ToString()

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

    Private Sub Addbtn_Click(sender As Object, e As EventArgs) Handles Addbtn.Click
        AddClaim()

    End Sub

    Private Sub ItemPic_Click(sender As Object, e As EventArgs) Handles ItemPic.Click
        With OpenFileDialog1
            .FileName = ""
            .Filter = "Image Files|*.jpeg;*.jpg;*.png;*.bmp"
        End With

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then

            ItemPic.Image = Image.FromFile(OpenFileDialog1.FileName)
            Guna2Panel5.BorderColor = Color.Transparent
            ItemPic.BackgroundImage = Nothing

        End If
    End Sub

    Dim selectedClaimId As Integer
    Private Sub Claimtbl_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Claimtbl.CellContentClick
        Dim columnName As String = Claimtbl.Columns(e.ColumnIndex).Name
        Dim selectedRow As DataGridViewRow = Claimtbl.Rows(e.RowIndex)
        Dim rowId As Integer = Convert.ToInt32(selectedRow.Cells("DataGridViewTextBoxColumn8").Value)

        Select Case columnName
            Case "Delete"
                deleteClaim(rowId)
            Case "View"
                ViewPhoto(rowId, ItmImgPrf)
                ViewItemManageClaim(rowId)
            Case "Edit"
                UpdateAction(rowId)
                ViewPhoto(rowId, ItemPic)
        End Select
    End Sub

    Private Sub Updatebtn_Click(sender As Object, e As EventArgs) Handles Updatebtn.Click
        UpdateClaim(selectedClaimId)
        mngClmTbl()
    End Sub

    Private Sub Guna2ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Guna2ComboBox1.SelectedIndexChanged
        Dim selectedstatus As String = Guna2ComboBox1.SelectedItem

        Select Case selectedstatus
            Case "Pending"
                FilterStatus(selectedstatus)
            Case "Approved"
                FilterStatus(selectedstatus)
            Case "Rejected"
                FilterStatus(selectedstatus)
            Case Else
                mngClmTbl()
        End Select
    End Sub

    Private Sub Clearbtn_Click(sender As Object, e As EventArgs) Handles Clearbtn.Click
        ClearField()
    End Sub
End Class