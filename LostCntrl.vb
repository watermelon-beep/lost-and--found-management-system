Imports System.Data.SqlClient
Imports System.IO
Imports Guna.UI2.WinForms

Public Class LostCntrl



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
                Else
                    MsgBox("no changes made")
                End If
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub ViewPhoto(id As String, images As Guna2PictureBox)
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
                    .AddWithValue("@ItemName", ItemNameTxt)
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

            End Using

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
            .Filter = "Image files| *.jpeg;*jpg;*.png;*.bmp"
        End With

        If OpenFileDialog1.ShowDialog Then
            ItemPic.Image = Image.FromFile(OpenFileDialog1.FileName)
            Guna2Panel1.BorderColor = Color.Transparent
            ItemPic.BackgroundImage = Nothing
        End If
    End Sub

    Private Sub Updatebtn_Click(sender As Object, e As EventArgs) Handles Updatebtn.Click

        Dim itemcntrl As New ItemsCntrl()

        updateUser()
        itemcntrl.viewItemTable()
    End Sub
End Class
