Imports System.Data.SqlClient
Imports System.IO
Imports Guna.UI2.WinForms
Imports System.Text.RegularExpressions
Public Class ItemsCntrl

    Private Sub clearAllField()
        ItemNameTxt.Clear()
        LocationTxt.Clear()
        StorageTxt.Clear()
        PersonNameTxt.Clear()
        ContactTxt.Clear()
        CategoryCmb.SelectedIndex = 0
        TypeCmb.SelectedIndex = 0
        StatusCmb.SelectedIndex = 0
        ItemPic.Image = Nothing
        DateLostFoundDtp.Value = DateTime.Now
        DescriptionTxt.Clear()
    End Sub

    Public Function contactExist(contact As String) As Boolean
        Try
            query = "SELECT COUNT(*) FROM Items WHERE ContactNumber = @ContactNumber"
            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@ContactNumber", contact)
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
                    While dataRead.Read
                        Label8.Text = dataRead("ItemID").ToString()
                        Label1.Text = dataRead("ItemName").ToString()
                        Label2.Text = dataRead("Category").ToString()
                        Label5.Text = dataRead("ItemType").ToString()
                        Label6.Text = dataRead("DateReported").ToString()
                        Label10.Text = dataRead("Location").ToString()
                        Label7.Text = dataRead("Status").ToString()
                        Label9.Text = dataRead("Description").ToString()
                    End While
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

    Private Sub deleteUser(id As String)

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
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Dim selectedItemId As String
    Private Sub updateUser()
        Try
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
                                            ItemImg = @ItemImg,
                                            Status = @Status,
                                            RecordedBy = @RecordedBy
                 WHERE ItemID = @ItemID"

            Using comm As New SqlCommand(query, conn)
                With comm.Parameters
                    .AddWithValue("@ItemID", selectedItemId)

                    .AddWithValue("@ItemName", ItemNameTxt.Text)
                    .AddWithValue("@Category", CategoryCmb.Text)
                    .AddWithValue("@ItemType", TypeCmb.Text)
                    .AddWithValue("@Description", DescriptionTxt.Text)
                    .AddWithValue("@Location", LocationTxt.Text)
                    .AddWithValue("@StorageLocation", StorageTxt.Text)
                    .AddWithValue("@DateLostFound", DateLostFoundDtp.Value.Date)
                    .AddWithValue("@PersonName", PersonNameTxt.Text)
                    .AddWithValue("@ContactNumber", ContactTxt.Text)
                    .Add("@ItemImg", SqlDbType.VarBinary, -1).Value = imgData
                    .AddWithValue("@Status", StatusCmb.Text)
                    .AddWithValue("@RecordedBy", currentStaffNo)
                End With

                Dim rows As Integer = comm.ExecuteNonQuery()

                If rows > 0 Then
                    MsgBox("updated")
                    viewItemTable()
                Else
                    MsgBox("no changes made")
                End If
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

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
        Try
            selectedItemId = id

            query = "SELECT * FROM Items WHERE ItemID = @ItemID"

            Using comm As New SqlCommand(query, conn)

                comm.Parameters.AddWithValue("@ItemID", id)

                Using dataRead = comm.ExecuteReader()
                    While dataRead.Read
                        PersonNameTxt.Text = dataRead("PersonName").ToString()
                        CategoryCmb.Text = dataRead("Category").ToString()
                        TypeCmb.Text = dataRead("ItemType").ToString()
                        DescriptionTxt.Text = dataRead("Description").ToString()
                        LocationTxt.Text = dataRead("Location").ToString()
                        StorageTxt.Text = dataRead("StorageLocation").ToString()
                        DateLostFoundDtp.Value = Convert.ToDateTime(dataRead("DateLostFound")).ToString("ddd, MMMM dd, yyyy")
                        ItemNameTxt.Text = dataRead("ItemName").ToString()
                        ContactTxt.Text = dataRead("ContactNumber").ToString()
                        StatusCmb.Text = dataRead("Status").ToString()
                    End While
                End Using
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub addItem()
        Try
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
                    .AddWithValue("@ItemName", ItemNameTxt.Text)
                    .AddWithValue("@Category", CategoryCmb.Text)
                    .AddWithValue("@ItemType", TypeCmb.Text)
                    .AddWithValue("@Description", DescriptionTxt.Text)
                    .AddWithValue("@Location", LocationTxt.Text)
                    .AddWithValue("@StorageLocation", StorageTxt.Text)
                    .AddWithValue("@DateLostFound", DateLostFoundDtp.Value.Date)
                    .AddWithValue("@PersonName", PersonNameTxt.Text)
                    .AddWithValue("@ContactNumber", ContactTxt.Text)
                    .Add("@ItemImg", SqlDbType.VarBinary, -1).Value = imgData
                    .AddWithValue("@Status", StatusCmb.Text)
                    .AddWithValue("@RecordedBy", currentStaffNo)
                End With

                comm.ExecuteNonQuery()

                MsgBox("added")
                viewItemTable()
            End Using
            clearAllField()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Addbtn.Click
        addItem()
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

    Private Sub Updatebtn_Click(sender As Object, e As EventArgs) Handles Updatebtn.Click

        updateUser()
        clearAllField()

    End Sub

    Private Sub ItemsCntrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        viewItemTable()
        Dim firstRowIndex As Integer = Itemtbl.Rows(0).Index
        viewItem(Itemtbl.Rows(firstRowIndex).Cells("DataGridViewTextBoxColumn8").Value.ToString())
        ViewPhoto(Itemtbl.Rows(firstRowIndex).Cells("DataGridViewTextBoxColumn8").Value.ToString(), ItmImgPrf)
    End Sub

    Private Sub Itemtbl_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Itemtbl.CellContentClick
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
                    deleteUser(rowId)
                End If
            Case "View"
                viewItem(rowId)
                ViewPhoto(rowId, ItmImgPrf)
        End Select
    End Sub

    Private Sub Searchtxbx_TextChanged(sender As Object, e As EventArgs) Handles Searchtxbx.TextChanged
        searchItems()
    End Sub

    Private Sub ItemNameTxt_TextChanged(sender As Object, e As EventArgs) Handles ItemNameTxt.TextChanged

    End Sub

    Private Sub PersonNameTxt_TextChanged(sender As Object, e As EventArgs) Handles PersonNameTxt.TextChanged
        If Not Regex.IsMatch(PersonNameTxt.Text, "^[a-zA-Z ]*$") Then
            PersonNameTxt.Text = PersonNameTxt.Text.Remove(PersonNameTxt.Text.Length - 1)
            PersonNameTxt.SelectionStart = PersonNameTxt.Text.Length
        End If
    End Sub

    Private Sub ContactTxt_TextChanged(sender As Object, e As EventArgs) Handles ContactTxt.TextChanged

        If contactExist(ContactTxt.Text) Then
            ContactTxt.BorderColor = Color.Red
            MsgBox("Contact number already exists. Please choose a different contact number.")
        End If

        If Not Regex.IsMatch(ContactTxt.Text, "^[0-9]*$") Or ContactTxt.Text.Length > 11 Then
            ContactTxt.Text = ContactTxt.Text.Remove(ContactTxt.Text.Length - 1)
            ContactTxt.SelectionStart = ContactTxt.Text.Length
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
