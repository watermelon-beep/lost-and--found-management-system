Imports System.Data.SqlClient
Imports System.IO
Imports Guna.UI2.WinForms

Public Class ManegClaimCntrl

    Dim selectedClaimId As Integer = 0

    Private Sub ClearField()
        selectedClaimId = 0
        PersonNameTxt.Clear()
        ContactTxt.Clear()
        ProofOfOwnTxt.Clear()
        If Unclaimcmbx.Items.Count > 0 Then Unclaimcmbx.SelectedIndex = 0
        StatusCmb.SelectedIndex = 0
        ClaimDate.Value = DateTime.Now
        ItemPic.Image = Nothing
    End Sub

    Private Function getSelectedItemId() As String
        If Unclaimcmbx.SelectedIndex < 0 Then Return ""
        Dim parts() As String = Unclaimcmbx.SelectedItem.ToString().Split(New String() {" - "}, StringSplitOptions.None)
        Return parts(0).Trim()
    End Function

    Private Function imageToBytes() As Object
        If ItemPic.Image Is Nothing Then Return DBNull.Value
        Using ms As New MemoryStream
            ItemPic.Image.Save(ms, Imaging.ImageFormat.Png)
            Return ms.ToArray()
        End Using
    End Function

    Private Sub refreshClaims()
        Dim s As String = Guna2ComboBox1.Text
        If s = "Pending" OrElse s = "Approved" OrElse s = "Rejected" Then
            loadClaims(s)
        Else
            loadClaims()
        End If
    End Sub

    Private Sub loadClaims(Optional status As String = "")
        Try
            Claimtbl.Rows.Clear()

            query = "SELECT c.ClaimNo, i.ItemID, i.ItemName, c.ClaimantName, c.ClaimantContact,
                            c.ClaimDate, c.ClaimStatus, u.StaffID AS ProcessedBy
                     FROM Claims c
                     INNER JOIN Items i ON c.ItemNo = i.ItemNo
                     LEFT JOIN user_table u ON c.ProcessedBy = u.StaffNo"
            If status <> "" Then query &= " WHERE c.ClaimStatus = @ClaimStatus"
            query &= " ORDER BY c.ClaimNo"

            Using comm As New SqlCommand(query, conn)
                If status <> "" Then comm.Parameters.AddWithValue("@ClaimStatus", status)

                Using dataRead = comm.ExecuteReader()
                    While dataRead.Read
                        Claimtbl.Rows.Add(
                            dataRead("ClaimNo"),
                            dataRead("ItemID"),
                            dataRead("ClaimantName"),
                            dataRead("ClaimantContact"),
                            Convert.ToDateTime(dataRead("ClaimDate")).ToString("yyyy-MM-dd"),
                            dataRead("ClaimStatus"),
                            dataRead("ProcessedBy").ToString(),
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

    Private Sub ViewItemManageClaim(id As Integer)
        Try
            query = "SELECT i.ItemID, i.ItemName, c.ClaimantName, c.ClaimantContact,
                            c.ClaimDate, c.ClaimStatus, u.StaffID AS ProcessedBy, c.ProofOfOwnership
                     FROM Claims c
                     INNER JOIN Items i ON c.ItemNo = i.ItemNo
                     LEFT JOIN user_table u ON c.ProcessedBy = u.StaffNo
                     WHERE c.ClaimNo = @ClaimNo"

            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@ClaimNo", id)

                Using dataRead = comm.ExecuteReader()
                    If dataRead.Read() Then
                        Label7.Text = dataRead("ClaimStatus").ToString()
                        Label8.Text = dataRead("ItemID").ToString()
                        Label1.Text = dataRead("ClaimantName").ToString()
                        Label4.Text = dataRead("ClaimantContact").ToString()
                        Label9.Text = dataRead("ProofOfOwnership").ToString()
                        Label2.Text = dataRead("ProcessedBy").ToString()
                        Label6.Text = Convert.ToDateTime(dataRead("ClaimDate")).ToString("MMMM dd, yyyy")
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function validateForm() As Boolean
        If PersonNameTxt.Text.Trim() = "" Then
            MsgBox("Claimant name is required.")
            Return False
        End If

        If ContactTxt.Text.Trim() = "" Then
            MsgBox("Claimant contact is required.")
            Return False
        End If

        If ProofOfOwnTxt.Text.Trim() = "" Then
            MsgBox("Proof of ownership is required.")
            Return False
        End If

        If ClaimDate.Value.Date > Date.Today Then
            MsgBox("Claim date cannot be in the future.")
            Return False
        End If

        If currentStaffNo = 0 Then
            MsgBox("No logged-in user found. Please log in again.")
            Return False
        End If

        Return True
    End Function

    Private Function UpdateClaim(id As Integer) As Boolean
        If id = 0 Then
            MsgBox("Choose a claim to edit first.")
            Return False
        End If

        If Not validateForm() Then Return False

        Dim newStatus As String = StatusCmb.Text

        Using tran As SqlTransaction = conn.BeginTransaction()
            Try
                Dim itemNo As Integer
                Dim oldStatus As String

                Using comm As New SqlCommand("SELECT ItemNo, ClaimStatus FROM Claims WHERE ClaimNo = @ClaimNo", conn, tran)
                    comm.Parameters.AddWithValue("@ClaimNo", id)
                    Using dataRead = comm.ExecuteReader()
                        If Not dataRead.Read() Then
                            dataRead.Close()
                            tran.Rollback()
                            MsgBox("No claim record was found.")
                            Return False
                        End If
                        itemNo = Convert.ToInt32(dataRead("ItemNo"))
                        oldStatus = dataRead("ClaimStatus").ToString()
                    End Using
                End Using

                If newStatus = "Approved" Then
                    Using comm As New SqlCommand("SELECT COUNT(*) FROM Claims WHERE ItemNo = @ItemNo AND ClaimStatus = 'Approved' AND ClaimNo <> @ClaimNo", conn, tran)
                        comm.Parameters.AddWithValue("@ItemNo", itemNo)
                        comm.Parameters.AddWithValue("@ClaimNo", id)
                        If Convert.ToInt32(comm.ExecuteScalar()) > 0 Then
                            tran.Rollback()
                            MsgBox("This item already has an approved claim.")
                            Return False
                        End If
                    End Using
                End If

                query = "UPDATE Claims SET
                         ClaimantName = @Name,
                         ClaimantContact = @Contact,
                         ProofOfOwnership = @Proof,
                         ClaimDate = @Date,
                         ClaimStatus = @ClaimStatus,
                         ProcessedBy = @User,
                         ProofImage = ISNULL(@Image, ProofImage)
                         WHERE ClaimNo = @ClaimNo"

                Using comm As New SqlCommand(query, conn, tran)
                    With comm.Parameters
                        .AddWithValue("@ClaimNo", id)
                        .AddWithValue("@Name", PersonNameTxt.Text.Trim())
                        .AddWithValue("@Contact", ContactTxt.Text.Trim())
                        .AddWithValue("@Proof", ProofOfOwnTxt.Text.Trim())
                        .AddWithValue("@Date", ClaimDate.Value.Date)
                        .AddWithValue("@ClaimStatus", newStatus)
                        .AddWithValue("@User", currentStaffNo)
                        .Add("@Image", SqlDbType.VarBinary, -1).Value = imageToBytes()
                    End With
                    comm.ExecuteNonQuery()
                End Using

                If newStatus = "Approved" Then
                    Using comm As New SqlCommand("UPDATE Items SET Status = 'Claimed' WHERE ItemNo = @ItemNo", conn, tran)
                        comm.Parameters.AddWithValue("@ItemNo", itemNo)
                        comm.ExecuteNonQuery()
                    End Using

                    Using comm As New SqlCommand("UPDATE Claims SET ClaimStatus = 'Rejected', ProcessedBy = @User WHERE ItemNo = @ItemNo AND ClaimNo <> @ClaimNo AND ClaimStatus = 'Pending'", conn, tran)
                        comm.Parameters.AddWithValue("@ItemNo", itemNo)
                        comm.Parameters.AddWithValue("@ClaimNo", id)
                        comm.Parameters.AddWithValue("@User", currentStaffNo)
                        comm.ExecuteNonQuery()
                    End Using
                ElseIf oldStatus = "Approved" Then
                    Using comm As New SqlCommand("UPDATE Items SET Status = 'Unclaimed' WHERE ItemNo = @ItemNo", conn, tran)
                        comm.Parameters.AddWithValue("@ItemNo", itemNo)
                        comm.ExecuteNonQuery()
                    End Using
                End If

                tran.Commit()
                MsgBox("Claim updated successfully!")
                Return True

            Catch ex As Exception
                Try
                    tran.Rollback()
                Catch
                End Try
                MsgBox(ex.ToString())
                Return False
            End Try
        End Using
    End Function

    Private Sub UpdateAction(id As Integer)
        Try
            selectedClaimId = id

            query = "SELECT c.ClaimantName, c.ClaimantContact, c.ProofOfOwnership, c.ClaimDate, c.ClaimStatus,
                            i.ItemID, i.ItemName
                     FROM Claims c
                     INNER JOIN Items i ON c.ItemNo = i.ItemNo
                     WHERE c.ClaimNo = @ClaimNo"

            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@ClaimNo", id)

                Using dataRead = comm.ExecuteReader()

                    If dataRead.Read() Then
                        PersonNameTxt.Text = dataRead("ClaimantName").ToString()
                        ContactTxt.Text = dataRead("ClaimantContact").ToString()
                        ProofOfOwnTxt.Text = dataRead("ProofOfOwnership").ToString()
                        ClaimDate.Value = Convert.ToDateTime(dataRead("ClaimDate"))
                        StatusCmb.Text = dataRead("ClaimStatus").ToString()

                        Dim itemText As String = dataRead("ItemID").ToString() & " - " & dataRead("ItemName").ToString()
                        Dim idx As Integer = Unclaimcmbx.FindStringExact(itemText)
                        If idx < 0 Then
                            Unclaimcmbx.Items.Add(itemText)
                            idx = Unclaimcmbx.Items.Count - 1
                        End If
                        Unclaimcmbx.SelectedIndex = idx
                    End If
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
        Using tran As SqlTransaction = conn.BeginTransaction()
            Try
                Dim itemNo As Integer = 0
                Dim oldStatus As String = ""

                Using comm As New SqlCommand("SELECT ItemNo, ClaimStatus FROM Claims WHERE ClaimNo = @ClaimNo", conn, tran)
                    comm.Parameters.AddWithValue("@ClaimNo", id)
                    Using dataRead = comm.ExecuteReader()
                        If dataRead.Read() Then
                            itemNo = Convert.ToInt32(dataRead("ItemNo"))
                            oldStatus = dataRead("ClaimStatus").ToString()
                        End If
                    End Using
                End Using

                If itemNo = 0 Then
                    tran.Rollback()
                    MsgBox("No claim found with the specified ClaimNo.")
                    Return
                End If

                Using comm As New SqlCommand("DELETE FROM Claims WHERE ClaimNo = @ClaimNo", conn, tran)
                    comm.Parameters.AddWithValue("@ClaimNo", id)
                    comm.ExecuteNonQuery()
                End Using

                If oldStatus = "Approved" Then
                    Using comm As New SqlCommand("UPDATE Items SET Status = 'Unclaimed' WHERE ItemNo = @ItemNo", conn, tran)
                        comm.Parameters.AddWithValue("@ItemNo", itemNo)
                        comm.ExecuteNonQuery()
                    End Using
                End If

                tran.Commit()
                MsgBox("Claim deleted successfully.")
                unclaimList()
                refreshClaims()

            Catch ex As Exception
                Try
                    tran.Rollback()
                Catch
                End Try
                MsgBox(ex.Message)
            End Try
        End Using
    End Sub

    Private Function AddClaim() As Boolean
        Try
            Dim itemId As String = getSelectedItemId()
            If itemId = "" Then
                MsgBox("Choose an item to claim.")
                Return False
            End If

            If Not validateForm() Then Return False

            query = "SELECT COUNT(*) FROM Claims c
                     INNER JOIN Items i ON c.ItemNo = i.ItemNo
                     WHERE i.ItemID = @ItemID AND c.ClaimantName = @Name AND c.ClaimStatus = 'Pending'"
            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@ItemID", itemId)
                comm.Parameters.AddWithValue("@Name", PersonNameTxt.Text.Trim())
                If Convert.ToInt32(comm.ExecuteScalar()) > 0 Then
                    MsgBox("This person already has a pending claim for this item.")
                    Return False
                End If
            End Using

            query = "INSERT INTO Claims (ItemNo, ClaimantName, ClaimantContact, ProofOfOwnership,
                     ClaimDate, ClaimStatus, ProcessedBy, ProofImage)
                     VALUES ((SELECT ItemNo FROM Items WHERE ItemID = @ItemID),
                     @Name, @Contact, @Proof, @Date, 'Pending', @User, @Image)"

            Using comm As New SqlCommand(query, conn)
                With comm.Parameters
                    .AddWithValue("@ItemID", itemId)
                    .AddWithValue("@Name", PersonNameTxt.Text.Trim())
                    .AddWithValue("@Contact", ContactTxt.Text.Trim())
                    .AddWithValue("@Proof", ProofOfOwnTxt.Text.Trim())
                    .AddWithValue("@Date", ClaimDate.Value.Date)
                    .AddWithValue("@User", currentStaffNo)
                    .Add("@Image", SqlDbType.VarBinary, -1).Value = imageToBytes()
                End With
                comm.ExecuteNonQuery()
            End Using

            MsgBox("Claim added as Pending.")
            Return True

        Catch ex As Exception
            MsgBox(ex.ToString())
            Return False
        End Try
    End Function

    Private Sub unclaimList()
        Try
            Unclaimcmbx.Items.Clear()

            query = "SELECT ItemName, ItemID FROM Items WHERE Status = 'Unclaimed'"

            Using comm As New SqlCommand(query, conn)
                Using dataRead = comm.ExecuteReader()
                    While dataRead.Read
                        Unclaimcmbx.Items.Add(dataRead("ItemID").ToString() & " - " & dataRead("ItemName").ToString())
                    End While
                End Using
            End Using

            If Unclaimcmbx.Items.Count > 0 Then Unclaimcmbx.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ManegClaimCntrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        unclaimList()
        loadClaims()
        If Claimtbl.Rows.Count > 0 Then
            Dim firstId As String = Claimtbl.Rows(0).Cells("DataGridViewTextBoxColumn8").Value.ToString()
            ViewItemManageClaim(firstId)
            ViewPhoto(firstId, ItmImgPrf)
        End If
    End Sub

    Private Sub Addbtn_Click(sender As Object, e As EventArgs) Handles Addbtn.Click
        If AddClaim() Then
            unclaimList()
            ClearField()
            refreshClaims()
        End If
    End Sub

    Private Sub ItemPic_Click(sender As Object, e As EventArgs) Handles ItemPic.Click
        With OpenFileDialog1
            .FileName = ""
            .Filter = "Image Files|*.jpeg;*.jpg;*.png;*.bmp"
        End With

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then

            Using temp As Image = Image.FromFile(OpenFileDialog1.FileName)
                ItemPic.Image = New Bitmap(temp)
            End Using
            Guna2Panel5.BorderColor = Color.Transparent
            ItemPic.BackgroundImage = Nothing

        End If
    End Sub

    Private Sub Claimtbl_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Claimtbl.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim columnName As String = Claimtbl.Columns(e.ColumnIndex).Name
        Dim selectedRow As DataGridViewRow = Claimtbl.Rows(e.RowIndex)
        Dim rowId As Integer = Convert.ToInt32(selectedRow.Cells("DataGridViewTextBoxColumn8").Value)

        Select Case columnName
            Case "Delete"
                Dim confirmResult As DialogResult = MessageBox.Show("Are you sure you want to delete this claim?", "Confirm Delete", MessageBoxButtons.YesNo)
                If confirmResult = DialogResult.Yes Then
                    deleteClaim(rowId)
                End If
            Case "View"
                ViewPhoto(rowId, ItmImgPrf)
                ViewItemManageClaim(rowId)
            Case "Edit"
                UpdateAction(rowId)
                ViewPhoto(rowId, ItemPic)
        End Select
    End Sub

    Private Sub Updatebtn_Click(sender As Object, e As EventArgs) Handles Updatebtn.Click
        If UpdateClaim(selectedClaimId) Then
            unclaimList()
            ClearField()
            refreshClaims()
        End If
    End Sub

    Private Sub Guna2ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Guna2ComboBox1.SelectedIndexChanged
        refreshClaims()
    End Sub

    Private Sub Clearbtn_Click(sender As Object, e As EventArgs) Handles Clearbtn.Click
        ClearField()
    End Sub
End Class