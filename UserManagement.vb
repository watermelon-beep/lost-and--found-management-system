Imports System.Data.SqlClient
Imports System.IO
Imports Guna.UI2.WinForms

Public Class UserManagement

    Private Sub viewProfile(id As String)
        Try
            query = "SELECT * FROM user_table WHERE StaffID = @StaffID"

            Using comm As New SqlCommand(query, conn)

                comm.Parameters.AddWithValue("@StaffID", id)

                Using dataRead = comm.ExecuteReader()
                    While dataRead.Read
                        Label2.Text = dataRead("FirstName").ToString()
                        Label4.Text = dataRead("MiddleName").ToString()
                        Label5.Text = dataRead("LastName").ToString()
                        Label6.Text = dataRead("StaffID").ToString()
                        Label1.Text = dataRead("AccountStatus").ToString()
                        Label8.Text = dataRead("Age").ToString()
                        Label18.Text = Convert.ToDateTime(dataRead("Birthdate")).ToString("MMMM dd, yyyy")
                        Label10.Text = dataRead("ContactNumber").ToString()
                        Label12.Text = dataRead("Email").ToString()
                        Label14.Text = dataRead("Username").ToString()
                        Label16.Text = dataRead("AccountStatus").ToString()
                    End While
                End Using
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub viewUserTable()
        Try
            query = "SELECT * FROM user_table"

            Using comm As New SqlCommand(query, conn)
                Using dataRead = comm.ExecuteReader

                    While dataRead.Read
                        With Usertbl.Rows
                            .Add(
                                    dataRead("StaffNo"),
                                    dataRead("StaffID"),
                                    dataRead("FirstName"),
                                    dataRead("MiddleName"),
                                    dataRead("LastName"),
                                    Convert.ToDateTime(dataRead("Birthdate")).ToString("yyyy-MM-dd"),
                                    dataRead("Age").ToString(),
                                    dataRead("Gender"),
                                    dataRead("Role"),
                                    dataRead("ContactNumber"),
                                    dataRead("Email"),
                                    dataRead("Username"),
                                    dataRead("UserPassword"),
                                    dataRead("AccountStatus"),
                                    "Update",
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

    Private Sub ViewPhoto(id As String, images As Guna2PictureBox)
        Try
            query = "SELECT user_photo FROM staff_information WHERE staff_code = @staff_code"

            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@staff_code", id)
                Using dataRead = comm.ExecuteReader()
                    If dataRead.Read Then
                        If IsDBNull(dataRead("user_photo")) Then
                            images.Image = Nothing
                        Else
                            Dim img() As Byte = CType(dataRead("user_photo"), Byte())
                            Using MS As New IO.MemoryStream(img)
                                images.Image = Image.FromStream(MS)
                            End Using
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

    Private Sub updateAction(id As String)
        Try

            query = "SELECT * FROM user_table WHERE StaffID = @StaffID"

            Using comm As New SqlCommand(query, conn)

                comm.Parameters.AddWithValue("@StaffID", id)

                Using dataRead = comm.ExecuteReader()

                    While dataRead.Read

                        FrstNmtxbx.Text = dataRead("FirstName")
                        MddlNmtxbx.Text = dataRead("MiddleName")
                        Srnmtxbx.Text = dataRead("LastName")
                        Gndrcmbx.Text = dataRead("Gender")
                        Rolecmbx.Text = dataRead("Role")
                        Brthdt.Value = dataRead("Birthdate")
                        Agetxbx.Text = dataRead("Age")
                        CntctNumtxbx.Text = dataRead("ContactNumber")
                        Emailtxbx.Text = dataRead("Email")
                        RegUsrnmtxbx.Text = dataRead("Username")
                        RegPasstxbx.Text = dataRead("UserPassword")
                        Statuscmbx.Text = dataRead("AccountStatus")

                    End While

                End Using

            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub updateUser()

        Try

            Dim imgData As Object = DBNull.Value

            If UsrImg.Image IsNot Nothing Then
                Using ms As New MemoryStream
                    UsrImg.Image.Save(ms, UsrImg.Image.RawFormat)
                    imgData = ms.ToArray()
                End Using
            End If

            query = "UPDATE user_table SET  FirstName = @FirstName,
                                            MiddleName = @MiddleName, 
                                            LastName = @LastName,
                                            Gender = @Gender,
                                            Role = @Role,
                                            Birthdate = @Birthdate,
                                            Age = @Age,
                                            ContactNumber = @ContactNumber,
                                            Email = @Email,
                                            Username = @Username,
                                            UserPassword = @UserPassword,
                                            UserImg = @UserImg,
                                            AccountStatus = @AccountStatus"

            Using comm As New SqlCommand(query, conn)

                With comm.Parameters
                    .AddWithValue("@FirstName", FrstNmtxbx.Text)
                    .AddWithValue("@MiddleName", MddlNmtxbx.Text)
                    .AddWithValue("@LastName", Srnmtxbx.Text)
                    .AddWithValue("@Gender", Gndrcmbx.Text)
                    .AddWithValue("@Role", Rolecmbx.Text)
                    .AddWithValue("@Birthdate", Brthdt.Value.ToString("yyyy-MM-dd"))
                    .AddWithValue("@Age", Convert.ToInt32(Agetxbx.Text))
                    .AddWithValue("@ContactNNumber", CntctNumtxbx.Text)
                    .AddWithValue("@Email", Emailtxbx.Text)
                    .AddWithValue("@Username", RegUsrnmtxbx.Text)
                    .AddWithValue("@UserPassword", RegPasstxbx.Text)
                    .AddWithValue("@UserImg", imgData)
                    .AddWithValue("@AccountStatus", Statuscmbx.Text)
                End With
                comm.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub addUser()
        Try
            Dim imgData As Object = DBNull.Value
            If UsrImg.Image IsNot Nothing Then
                Using ms As New MemoryStream
                    UsrImg.Image.Save(ms, UsrImg.Image.RawFormat)
                    imgData = ms.ToArray()
                End Using
            End If

            query = "INSERT INTO user_table (FirstName, MiddleName, LastName, Gender, Role, Birthdate, Age, ContactNumber, Email, Username, UserPassword, UserImg, AccountStatus)
                     VALUES (
                             @FirstName,
                             @MiddleName,
                             @LastName,
                             @Gender,
                             @Role,
                             @Birthdate,
                             @Age,
                             @ContactNNumber,
                             @Email,
                             @Username,
                             @UserPassword,
                             @UserImg,
                             @AccountStatus
                            )"

            Using comm As New SqlCommand(query, conn)

                With comm.Parameters
                    .AddWithValue("@FirstName", FrstNmtxbx.Text)
                    .AddWithValue("@MiddleName", MddlNmtxbx.Text)
                    .AddWithValue("@LastName", Srnmtxbx.Text)
                    .AddWithValue("@Gender", Gndrcmbx.Text)
                    .AddWithValue("@Role", Rolecmbx.Text)
                    .AddWithValue("@Birthdate", Brthdt.Value.ToString("yyyy-MM-dd"))
                    .AddWithValue("@Age", Agetxbx.Text)
                    .AddWithValue("@ContactNNumber", CntctNumtxbx.Text)
                    .AddWithValue("@Email", Emailtxbx.Text)
                    .AddWithValue("@Username", RegUsrnmtxbx.Text)
                    .AddWithValue("@UserPassword", RegPasstxbx.Text)
                    .AddWithValue("@UserImg", imgData)
                    .AddWithValue("@AccountStatus", Statuscmbx.Text)
                End With

                comm.ExecuteNonQuery()

                MsgBox("added")

            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub RegPasstxbx_TextChanged(sender As Object, e As EventArgs) Handles RegPasstxbx.TextChanged
        CnfrmPasstxbx.Text = RegPasstxbx.Text
    End Sub

    Private Sub AddUsrbtn_Click(sender As Object, e As EventArgs) Handles AddUsrbtn.Click
        addUser()
    End Sub

    Private Sub UsrImg_Click(sender As Object, e As EventArgs) Handles UsrImg.Click

        With OpenFileDialog1
            .FileName = ""
            .Filter = "Image files| *.jpeg;*jpg;*.png;*.bmp"
        End With

        If OpenFileDialog1.ShowDialog Then
            UsrImg.Image = Image.FromFile(OpenFileDialog1.FileName)
            Guna2Panel9.BorderColor = Color.Transparent
            UsrImg.BackgroundImage = Nothing
        End If
    End Sub

    Private Sub UserManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sqlconnection()
        viewUserTable()
    End Sub

    Private Sub Updtbtn_Click(sender As Object, e As EventArgs) Handles Updtbtn.Click
        updateUser()
    End Sub

    Private Sub Usertbl_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Usertbl.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim columnName As String = Usertbl.Columns(e.ColumnIndex).Name
        Dim selectedRow As DataGridViewRow = Usertbl.Rows(e.RowIndex)
        Dim rowId As String = Convert.ToString(selectedRow.Cells("DataGridViewTextBoxColumn9").Value)

        Select Case columnName
 
            Case "View"
                viewProfile(rowId)
        End Select

    End Sub
End Class
