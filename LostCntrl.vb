Imports System.Data.SqlClient
Imports System.IO
Imports Guna.UI2.WinForms

Public Class LostCntrl
    Private Sub addItem()
        Try
            Dim imgData As Object = DBNull.Value
            If ItemPic.Image IsNot Nothing Then
                Using ms As New MemoryStream
                    ItemPic.Image.Save(ms, Imaging.ImageFormat.Png)
                    imgData = ms.ToArray()
                End Using
            End If

            query = "INSERT INTO Items (ItemName, Category, ItemType, Description, Location,
                                    StorageLocation, DateLostFound, PersonName,
                                    ContactNumber, ItemImg, Status, RecordedBy)
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
                    .AddWithValue("@ItemName", PersonNameTxt.Text)
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

    Private Sub LostCntrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
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
End Class
