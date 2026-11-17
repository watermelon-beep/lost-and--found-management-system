Imports System.Data.SqlClient
Imports System.IO
Imports Guna.UI2.WinForms
Imports System.Text.RegularExpressions
Public Class ItemsCntrl

    Dim selectedItemId As String = ""
    Dim isLoading As Boolean = False
    Dim defaultBorder As Color = Color.FromArgb(36, 93, 170)

    Private Sub clearAllField()
        isLoading = True
        Try
            selectedItemId = ""
            ItemNameTxt.Clear()
            LocationTxt.Clear()
            StorageTxt.Clear()
            PersonNameTxt.Clear()
            ContactTxt.Clear()
            CategoryCmb.SelectedIndex = 0
            TypeCmb.SelectedIndex = 0
            StatusCmb.SelectedIndex = 0
            ItemPic.Image = Nothing
            DateLostFoundDtp.Value = DateLostFoundDtp.MaxDate
            DescriptionTxt.Clear()
            ContactTxt.BorderColor = defaultBorder
        Finally
            isLoading = False
        End Try
    End Sub

    Public Function contactExist(contact As String, Optional ignoreId As String = "") As Boolean
        Try
            query = "SELECT COUNT(*) FROM Items WHERE ContactNumber = @ContactNumber AND (@ItemID = '' OR ItemID <> @ItemID)"
            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@ContactNumber", contact)
                comm.Parameters.AddWithValue("@ItemID", ignoreId)
                Dim count As Integer = Convert.ToInt32(comm.ExecuteScalar)
                Return count > 0
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return False
    End Function

    Public Sub viewItemTable()
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

    Private Sub viewItem(id As String)
        Try
            query = "SELECT * FROM Items WHERE ItemID = @ItemID"

            Using comm As New SqlCommand(query, conn)

                comm.Parameters.AddWithValue("@ItemID", id)

                Using dataRead = comm.ExecuteReader()
                    If dataRead.Read() Then
                        Label8.Text = dataRead("ItemID").ToString()
                        Label1.Text = dataRead("ItemName").ToString()
                        Label2.Text = dataRead("Category").ToString()
                        Label5.Text = dataRead("ItemType").ToString()
                        Label6.Text = Convert.ToDateTime(dataRead("DateReported")).ToString("MMMM dd, yyyy")
                        Label10.Text = dataRead("Location").ToString()
                        Label7.Text = dataRead("Status").ToString()
                        Label9.Text = dataRead("Description").ToString()
                    End If
                End Using
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub searchItems()
        Try

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
                    FROM Items
                    WHERE ItemID LIKE @search OR ItemName LIKE @search"

            Using comm As New SqlCommand(query, conn)

                comm.Parameters.AddWithValue("@search", "%" & Searchtxbx.Text.Trim() & "%")

                Using dataRead = comm.ExecuteReader()
                    Itemtbl.Rows.Clear()
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

    Private Sub deleteItem(id As String)

        Try
            query = "DELETE FROM Items WHERE ItemID = @ItemID"

            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@ItemID", id)

                Dim rowsAffected As Integer = comm.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    MsgBox("Item deleted successfully.")
                    viewItemTable()
                Else
                    MsgBox("No item found with the specified ItemID.")
                End If
            End Using
        Catch ex As SqlException When ex.Number = 547
            MsgBox("This item cannot be deleted because it already has claims. Delete or resolve its claims first.")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function validateForm() As Boolean
        If ItemNameTxt.Text.Trim() = "" Then
            MsgBox("Item name is required.")
            Return False
        End If

        If LocationTxt.Text.Trim() = "" Then
            MsgBox("Location is required.")
            Return False
        End If

        If DateLostFoundDtp.Value.Date > Date.Today Then
            MsgBox("Date cannot be in the future.")
            Return False
        End If

        Return True
    End Function

    Private Function updateUser() As Boolean
        Try
            If selectedItemId = "" Then
                MsgBox("Choose an item to edit first.")
                Return False
            End If

            If Not validateForm() Then Return False

            Dim imgData As Object = DBNull.Value
            If ItemPic.Image IsNot Nothing Then
                Using ms As New MemoryStream
                    ItemPic.Image.Save(ms, Imaging.ImageFormat.Png)
                    imgData = ms.ToArray()
                End Using
            End If

            query = "UPDATE Items SET ItemName = @ItemName,
                                            Category = @Category,
                                            ItemType = @ItemType,
                                            Description = @Description,
                                            Location = @Location,
                                            StorageLocation = @StorageLocation,
                                            DateLostFound = @DateLostFound,
                                            PersonName = @PersonName,
                                            ContactNumber = @ContactNumber,
                                            ItemImg = ISNULL(@ItemImg, ItemImg),
                                            Status = @Status
                 WHERE ItemID = @ItemID"

            Using comm As New SqlCommand(query, conn)
                With comm.Parameters
                    .AddWithValue("@ItemID", selectedItemId)

                    .AddWithValue("@ItemName", ItemNameTxt.Text.Trim())
                    .AddWithValue("@Category", CategoryCmb.Text)
                    .AddWithValue("@ItemType", TypeCmb.Text)
                    .AddWithValue("@Description", DescriptionTxt.Text.Trim())
                    .AddWithValue("@Location", LocationTxt.Text.Trim())
                    .AddWithValue("@StorageLocation", StorageTxt.Text.Trim())
                    .AddWithValue("@DateLostFound", DateLostFoundDtp.Value.Date)
                    .AddWithValue("@PersonName", PersonNameTxt.Text.Trim())
                    .AddWithValue("@ContactNumber", ContactTxt.Text.Trim())
                    .Add("@ItemImg", SqlDbType.VarBinary, -1).Value = imgData
                    .AddWithValue("@Status", StatusCmb.Text)
                End With

                Dim rows As Integer = comm.ExecuteNonQuery()

                If rows > 0 Then
                    MsgBox("Updated.")
                    viewItemTable()
                    Return True
                Else
                    MsgBox("No changes made.")
                    Return False
                End If
            End Using

        Catch ex As Exception
            MsgBox(ex.ToString())
            Return False
        End Try
    End Function

    Private Sub ViewPhoto(id As String, images As Guna2PictureBox)
        Try
            query = "SELECT ItemImg FROM Items WHERE ItemID = @ItemID"

            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@ItemID", id)
                Using dataRead = comm.ExecuteReader()
                    If dataRead.Read AndAlso Not IsDBNull(dataRead("ItemImg")) Then
                        Dim img() As Byte = CType(dataRead("ItemImg"), Byte())
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

    Public Sub updateAction(id As String)
        isLoading = True
        Try
            selectedItemId = id

            query = "SELECT * FROM Items WHERE ItemID = @ItemID"

            Using comm As New SqlCommand(query, conn)

                comm.Parameters.AddWithValue("@ItemID", id)

                Using dataRead = comm.ExecuteReader()
                    If dataRead.Read() Then
                        PersonNameTxt.Text = dataRead("PersonName").ToString()
                        CategoryCmb.Text = dataRead("Category").ToString()
                        TypeCmb.Text = dataRead("ItemType").ToString()
                        DescriptionTxt.Text = dataRead("Description").ToString()
                        LocationTxt.Text = dataRead("Location").ToString()
                        StorageTxt.Text = dataRead("StorageLocation").ToString()

                        If IsDBNull(dataRead("DateLostFound")) Then
                            DateLostFoundDtp.Value = DateLostFoundDtp.MaxDate
                        Else
                            Dim d As Date = Convert.ToDateTime(dataRead("DateLostFound"))
                            If d < DateLostFoundDtp.MinDate Then d = DateLostFoundDtp.MinDate
                            If d > DateLostFoundDtp.MaxDate Then d = DateLostFoundDtp.MaxDate
                            DateLostFoundDtp.Value = d
                        End If

                        ItemNameTxt.Text = dataRead("ItemName").ToString()
                        ContactTxt.Text = dataRead("ContactNumber").ToString()
                        StatusCmb.Text = dataRead("Status").ToString()
                    End If
                End Using
            End Using

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            isLoading = False
            ContactTxt.BorderColor = defaultBorder
        End Try
    End Sub

    Private Function addItem() As Boolean
        Try
            If Not validateForm() Then Return False

            If currentStaffNo = 0 Then
                MsgBox("No logged-in user found. Please log in again.")
                Return False
            End If

            Dim imgData As Object = DBNull.Value
            If ItemPic.Image IsNot Nothing Then
                Using ms As New MemoryStream
                    ItemPic.Image.Save(ms, Imaging.ImageFormat.Png)
                    imgData = ms.ToArray()
                End Using
            End If

            query = "INSERT INTO Items (ItemName,
                                        Category,
                                        ItemType,
                                        Description,
                                        Location,
                                        StorageLocation,
                                        DateLostFound,
                                        PersonName,
                                        ContactNumber,
                                        ItemImg,
                                        Status,
                                        RecordedBy)
                     VALUES (
                             @ItemName,
                             @Category,
                             @ItemType,
                             @Description,
                             @Location,
                             @StorageLocation,
                             @DateLostFound,
                             @PersonName,
                             @ContactNumber,
                             @ItemImg,
                             @Status,
                             @RecordedBy
                            )"

            Using comm As New SqlCommand(query, conn)

                With comm.Parameters
                    .AddWithValue("@ItemName", ItemNameTxt.Text.Trim())
                    .AddWithValue("@Category", CategoryCmb.Text)
                    .AddWithValue("@ItemType", TypeCmb.Text)
                    .AddWithValue("@Description", DescriptionTxt.Text.Trim())
                    .AddWithValue("@Location", LocationTxt.Text.Trim())
                    .AddWithValue("@StorageLocation", StorageTxt.Text.Trim())
                    .AddWithValue("@DateLostFound", DateLostFoundDtp.Value.Date)
                    .AddWithValue("@PersonName", PersonNameTxt.Text.Trim())
                    .AddWithValue("@ContactNumber", ContactTxt.Text.Trim())
                    .Add("@ItemImg", SqlDbType.VarBinary, -1).Value = imgData
                    .AddWithValue("@Status", StatusCmb.Text)
                    .AddWithValue("@RecordedBy", currentStaffNo)
                End With

                comm.ExecuteNonQuery()

                MsgBox("Added.")
                viewItemTable()
                Return True
            End Using

        Catch ex As Exception
            MsgBox(ex.ToString())
            Return False
        End Try
    End Function

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Addbtn.Click
        If addItem() Then
            clearAllField()
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

    Private Sub Updatebtn_Click(sender As Object, e As EventArgs) Handles Updatebtn.Click
        If updateUser() Then
            clearAllField()
        End If
    End Sub

    Private Sub ItemsCntrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        defaultBorder = ContactTxt.BorderColor
        viewItemTable()
        If Itemtbl.Rows.Count > 0 Then
            Dim firstId As String = Itemtbl.Rows(0).Cells("DataGridViewTextBoxColumn8").Value.ToString()
            viewItem(firstId)
            ViewPhoto(firstId, ItmImgPrf)
        End If
    End Sub

    Private Sub Itemtbl_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Itemtbl.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim columnName As String = Itemtbl.Columns(e.ColumnIndex).Name
        Dim selectedRow As DataGridViewRow = Itemtbl.Rows(e.RowIndex)
        Dim rowId As String = Convert.ToString(selectedRow.Cells("DataGridViewTextBoxColumn8").Value)

        Select Case columnName
            Case "Edit"
                updateAction(rowId)
                ViewPhoto(rowId, ItemPic)
            Case "Delete"
                Dim confirmResult As DialogResult = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo)
                If confirmResult = DialogResult.Yes Then
                    deleteItem(rowId)
                End If
            Case "View"
                viewItem(rowId)
                ViewPhoto(rowId, ItmImgPrf)
        End Select
    End Sub

    Private Sub Searchtxbx_TextChanged(sender As Object, e As EventArgs) Handles Searchtxbx.TextChanged
        searchItems()
    End Sub

    Private Sub PersonNameTxt_TextChanged(sender As Object, e As EventArgs) Handles PersonNameTxt.TextChanged
        If Not Regex.IsMatch(PersonNameTxt.Text, "^[a-zA-Z ]*$") Then
            PersonNameTxt.Text = PersonNameTxt.Text.Remove(PersonNameTxt.Text.Length - 1)
            PersonNameTxt.SelectionStart = PersonNameTxt.Text.Length
        End If
    End Sub

    Private Sub ContactTxt_TextChanged(sender As Object, e As EventArgs) Handles ContactTxt.TextChanged
        If isLoading Then Return

        If Not Regex.IsMatch(ContactTxt.Text, "^[0-9]*$") Or ContactTxt.Text.Length > 11 Then
            ContactTxt.Text = ContactTxt.Text.Remove(ContactTxt.Text.Length - 1)
            ContactTxt.SelectionStart = ContactTxt.Text.Length
            Return
        End If

        If ContactTxt.Text <> "" AndAlso contactExist(ContactTxt.Text, selectedItemId) Then
            ContactTxt.BorderColor = Color.Red
            MsgBox("Contact number already exists. Please choose a different contact number.")
        Else
            ContactTxt.BorderColor = defaultBorder
        End If
    End Sub

    Private Sub Guna2Button2_Click_1(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        If Itemtbl.ScrollBars = ScrollBars.Horizontal Then
            Itemtbl.ScrollBars = ScrollBars.Both
        Else
            Itemtbl.ScrollBars = ScrollBars.Horizontal
        End If
    End Sub
End Class