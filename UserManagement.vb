Imports System.Data.SqlClient
Imports System.IO
Imports Guna.UI2.WinForms
Imports System.Text.RegularExpressions

Public Class UserManagement

    Dim updateTriggered As Boolean = False

    Private Sub trimAllTextBoxes()
        FrstNmtxbx.Text = FrstNmtxbx.Text.Trim()
        MddlNmtxbx.Text = MddlNmtxbx.Text.Trim()
        Srnmtxbx.Text = Srnmtxbx.Text.Trim()
        Agetxbx.Text = Agetxbx.Text.Trim()
        CntctNumtxbx.Text = CntctNumtxbx.Text.Trim()
        Emailtxbx.Text = Emailtxbx.Text.Trim()
        RegUsrnmtxbx.Text = RegUsrnmtxbx.Text.Trim()
        RegPasstxbx.Text = RegPasstxbx.Text.Trim()
        CnfrmPasstxbx.Text = CnfrmPasstxbx.Text.Trim()
    End Sub

    Private Sub clearAllFields()
        FrstNmtxbx.Clear()
        MddlNmtxbx.Clear()
        Srnmtxbx.Clear()
        Gndrcmbx.SelectedIndex = 2
        Rolecmbx.SelectedIndex = 1
        Brthdt.Value = DateTime.Now
        Agetxbx.Clear()
        CntctNumtxbx.Clear()
        Emailtxbx.Clear()
        RegUsrnmtxbx.Clear()
        RegPasstxbx.Clear()
        CnfrmPasstxbx.Clear()
        Statuscmbx.SelectedIndex = 0
        UsrImg.Image = Nothing
    End Sub

    Private Sub searchUser()
        Try

            query = "SELECT StaffNo, 
                            StaffID, 
                            FirstName, 
                            MiddleName, 
                            LastName, 
                            Birthdate, 
                            Age,
                            Gender, 
                            Role, 
                            ContactNumber, 
                            Email, 
                            Username,
                            AccountStatus
                    FROM user_table
                    WHERE StaffID LIKE @search OR FirstName LIKE @search OR LastName LIKE @search"

            Using comm As New SqlCommand(query, conn)

                comm.Parameters.AddWithValue("@search", "%" & Searchtxbx.Text.Trim() & "%")

                Using dataRead = comm.ExecuteReader()
                    Usertbl.Rows.Clear()
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
                                    dataRead("AccountStatus"),
                                    "Update",
                                    "Delete",
                                    "View")
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
            query = "DELETE FROM user_table WHERE StaffID = @StaffID"

            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@StaffID", id)

                Dim rowsAffected As Integer = comm.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    MsgBox("User deleted successfully.")
                    viewUserTable()
                Else
                    MsgBox("No user found with the specified StaffID.")
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

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
                        Label16.Text = Convert.ToDateTime(dataRead("DateCreated")).ToString("MMMM dd, yyyy")
                    End While
                End Using
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub viewUserTable()
        Try
            comm.CommandTimeout = 120

            Usertbl.Rows.Clear()

            query = "SELECT StaffNo, 
                            StaffID, 
                            FirstName, 
                            MiddleName, 
                            LastName, 
                            Birthdate, 
                            Age,
                            Gender, 
                            Role, 
                            ContactNumber, 
                            Email, 
                            Username,
                            AccountStatus
                    FROM user_table"

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
            query = "SELECT UserImg FROM user_table WHERE StaffID = @StaffID"

            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@StaffID", id)
                Using dataRead = comm.ExecuteReader()
                    If dataRead.Read AndAlso Not IsDBNull(dataRead("UserImg")) Then
                        Dim img() As Byte = CType(dataRead("UserImg"), Byte())
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

    Private selectedStaffId As String = ""

    Private Sub updateAction(id As String)
        Try
            selectedStaffId = id

            query = "SELECT * FROM user_table WHERE StaffID = @StaffID"

            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@StaffID", id)

                Using dataRead = comm.ExecuteReader()
                    While dataRead.Read
                        FrstNmtxbx.Text = dataRead("FirstName").ToString()
                        MddlNmtxbx.Text = dataRead("MiddleName").ToString()
                        Srnmtxbx.Text = dataRead("LastName").ToString()
                        Gndrcmbx.Text = dataRead("Gender").ToString()
                        Rolecmbx.Text = dataRead("Role").ToString()
                        Brthdt.Value = Convert.ToDateTime(dataRead("Birthdate"))
                        Agetxbx.Text = dataRead("Age").ToString()
                        CntctNumtxbx.Text = dataRead("ContactNumber").ToString()
                        Emailtxbx.Text = dataRead("Email").ToString()
                        RegUsrnmtxbx.Text = dataRead("Username").ToString()
                        RegPasstxbx.Text = dataRead("UserPassword").ToString()
                        Statuscmbx.Text = dataRead("AccountStatus").ToString()
                    End While
                End Using
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub updateUser()
        Try
            If selectedStaffId = "" Then
                MsgBox("choose a user to update")
                Return
            End If

            Dim imgData As Object = DBNull.Value
            If UsrImg.Image IsNot Nothing Then
                Using ms As New MemoryStream
                    UsrImg.Image.Save(ms, Imaging.ImageFormat.Png)
                    imgData = ms.ToArray()
                End Using
            End If

            query = "UPDATE user_table SET FirstName = @FirstName,
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
                                       UserImg = ISNULL(@UserImg, UserImg),
                                       AccountStatus = @AccountStatus
                 WHERE StaffID = @StaffID"

            Using comm As New SqlCommand(query, conn)
                With comm.Parameters
                    .AddWithValue("@StaffID", selectedStaffId)
                    .AddWithValue("@FirstName", FrstNmtxbx.Text)
                    .AddWithValue("@MiddleName", MddlNmtxbx.Text)
                    .AddWithValue("@LastName", Srnmtxbx.Text)
                    .AddWithValue("@Gender", Gndrcmbx.Text)
                    .AddWithValue("@Role", Rolecmbx.Text)
                    .AddWithValue("@Birthdate", Brthdt.Value.Date)
                    .AddWithValue("@Age", Convert.ToInt32(Agetxbx.Text))
                    .AddWithValue("@ContactNumber", CntctNumtxbx.Text)
                    .AddWithValue("@Email", Emailtxbx.Text)
                    .AddWithValue("@Username", RegUsrnmtxbx.Text)
                    .AddWithValue("@UserPassword", RegPasstxbx.Text)
                    .Add("@UserImg", SqlDbType.VarBinary, -1).Value = imgData
                    .AddWithValue("@AccountStatus", Statuscmbx.Text)
                End With

                Dim rows As Integer = comm.ExecuteNonQuery()

                If rows > 0 Then
                    MsgBox("updated")
                    Usertbl.Rows.Clear()
                    viewUserTable()
                Else
                    MsgBox("no changes made")
                End If
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
                             @ContactNumber,
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
                    .AddWithValue("@ContactNumber", CntctNumtxbx.Text)
                    .AddWithValue("@Email", Emailtxbx.Text)
                    .AddWithValue("@Username", RegUsrnmtxbx.Text)
                    .AddWithValue("@UserPassword", RegPasstxbx.Text)
                    .Add("@UserImg", SqlDbType.VarBinary, -1).Value = imgData
                    .AddWithValue("@AccountStatus", Statuscmbx.Text)
                End With

                comm.ExecuteNonQuery()

                MsgBox("added")
                viewUserTable()
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub RegPasstxbx_TextChanged(sender As Object, e As EventArgs) Handles RegPasstxbx.TextChanged
        CnfrmPasstxbx.Text = RegPasstxbx.Text
        If RegPasstxbx.Text = "" Or
           Regex.IsMatch(RegPasstxbx.Text, ".[A-Z]") AndAlso
           Regex.IsMatch(RegPasstxbx.Text, ".[0-9]") AndAlso
           Regex.IsMatch(RegPasstxbx.Text, ".{6}") Then

            RegPasstxbx.BorderColor = Color.FromArgb(36, 93, 170)

        Else
            RegPasstxbx.BorderColor = Color.Red
        End If
    End Sub

    Private Sub AddUsrbtn_Click(sender As Object, e As EventArgs) Handles AddUsrbtn.Click
        trimAllTextBoxes()
        addUser()
        clearAllFields()
    End Sub

    Private Sub UsrImg_Click(sender As Object, e As EventArgs) Handles UsrImg.Click

        With OpenFileDialog1
            .FileName = ""
            .Filter = "Image Files|*.jpeg;*.jpg;*.png;*.bmp"
        End With

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then

            UsrImg.Image = Image.FromFile(OpenFileDialog1.FileName)
            Guna2Panel9.BorderColor = Color.Transparent
            UsrImg.BackgroundImage = Nothing

        End If
    End Sub

    Private Sub UserManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        viewUserTable()
    End Sub

    Private Sub Updtbtn_Click(sender As Object, e As EventArgs) Handles Updtbtn.Click
        trimAllTextBoxes()
        clearAllFields()
        updateUser()
        updateTriggered = False
    End Sub

    Private Sub Usertbl_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Usertbl.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim columnName As String = Usertbl.Columns(e.ColumnIndex).Name
        Dim selectedRow As DataGridViewRow = Usertbl.Rows(e.RowIndex)
        Dim rowId As String = Convert.ToString(selectedRow.Cells("DataGridViewTextBoxColumn9").Value)

        Select Case columnName
 
            Case "View"
                viewProfile(rowId)
                ViewPhoto(rowId, Guna2PictureBox1)
            Case "Update"
                updateTriggered = True
                updateAction(rowId)
                ViewPhoto(rowId, UsrImg)
            Case "Delete"
                Dim confirmResult As DialogResult = MessageBox.Show("Are you sure to delete this user?", "Confirm Delete", MessageBoxButtons.YesNo)
                If confirmResult = DialogResult.Yes Then
                    deleteUser(rowId)
                End If
        End Select

    End Sub

    Private Sub Searchtxbx_TextChanged(sender As Object, e As EventArgs) Handles Searchtxbx.TextChanged
        searchUser()
    End Sub

    Private Sub FrstNmtxbx_TextChanged(sender As Object, e As EventArgs) Handles FrstNmtxbx.TextChanged
        If Not Regex.IsMatch(FrstNmtxbx.Text, "^[a-zA-Z ]*$") Then
            FrstNmtxbx.Text = FrstNmtxbx.Text.Remove(FrstNmtxbx.Text.Length - 1)
            FrstNmtxbx.SelectionStart = FrstNmtxbx.Text.Length
        End If
    End Sub

    Private Sub MddlNmtxbx_TextChanged(sender As Object, e As EventArgs) Handles MddlNmtxbx.TextChanged
        If Not Regex.IsMatch(MddlNmtxbx.Text, "^[a-zA-Z ]*$") Then
            MddlNmtxbx.Text = MddlNmtxbx.Text.Remove(MddlNmtxbx.Text.Length - 1)
            MddlNmtxbx.SelectionStart = MddlNmtxbx.Text.Length
        End If
    End Sub

    Private Sub Srnmtxbx_TextChanged(sender As Object, e As EventArgs) Handles Srnmtxbx.TextChanged
        If Not Regex.IsMatch(Srnmtxbx.Text, "^[a-zA-Z ]*$") Then
            Srnmtxbx.Text = Srnmtxbx.Text.Remove(Srnmtxbx.Text.Length - 1)
            Srnmtxbx.SelectionStart = Srnmtxbx.Text.Length
        End If
    End Sub

    Private Sub Agetxbx_TextChanged(sender As Object, e As EventArgs) Handles Agetxbx.TextChanged

        If Not Regex.IsMatch(Agetxbx.Text, "^[0-9]*$") Or Agetxbx.Text.Length >= 3 Then
            Agetxbx.Text = Agetxbx.Text.Remove(Agetxbx.Text.Length - 1)
            Agetxbx.SelectionStart = Agetxbx.Text.Length
        End If
    End Sub

    Private Sub CntctNumtxbx_TextChanged(sender As Object, e As EventArgs) Handles CntctNumtxbx.TextChanged

        If contactNumExist(CntctNumtxbx.Text) And updateTriggered = False Then
            CntctNumtxbx.BorderColor = Color.Red
            MsgBox("Contact number already exists. Please choose a different contact number.")
        End If

        If CntctNumtxbx.Text.Length > 11 Then
            CntctNumtxbx.Text = CntctNumtxbx.Text.Remove(CntctNumtxbx.Text.Length - 1)
            CntctNumtxbx.SelectionStart = CntctNumtxbx.Text.Length
        End If

        If Not Regex.IsMatch(CntctNumtxbx.Text, "^[0-9]*$") Then
            CntctNumtxbx.Text = CntctNumtxbx.Text.Remove(CntctNumtxbx.Text.Length - 1)
            CntctNumtxbx.SelectionStart = CntctNumtxbx.Text.Length
        End If
    End Sub

    Private Sub Emailtxbx_TextChanged(sender As Object, e As EventArgs) Handles Emailtxbx.TextChanged

        If emailExist(Emailtxbx.Text) And updateTriggered = False Then
            Emailtxbx.BorderColor = Color.Red
            MsgBox("Email already exists. Please choose a different email.")
        End If

        If Not Emailtxbx.Text.Contains("@gmail.com") And Emailtxbx.Text <> "" Then
            Emailtxbx.BorderColor = Color.Red
        Else
            Emailtxbx.BorderColor = Color.FromArgb(36, 93, 170)
        End If
    End Sub

    Private Sub RegUsrnmtxbx_TextChanged(sender As Object, e As EventArgs) Handles RegUsrnmtxbx.TextChanged

        If userLogExist(RegUsrnmtxbx.Text) And updateTriggered = False Then
            RegUsrnmtxbx.BorderColor = Color.Red
            MsgBox("Username already exists. Please choose a different username.")
        End If

        If RegUsrnmtxbx.Text = "" Or
           Regex.IsMatch(RegUsrnmtxbx.Text, ".[A-Z]") AndAlso
           Regex.IsMatch(RegUsrnmtxbx.Text, ".[0-9]") AndAlso
           Regex.IsMatch(RegUsrnmtxbx.Text, ".{6}") Then

            RegUsrnmtxbx.BorderColor = Color.FromArgb(36, 93, 170)

        Else
            RegUsrnmtxbx.BorderColor = Color.Red
        End If
    End Sub

    Private Sub CnfrmPasstxbx_TextChanged(sender As Object, e As EventArgs) Handles CnfrmPasstxbx.TextChanged
        If CnfrmPasstxbx.Text = "" Or
           Regex.IsMatch(CnfrmPasstxbx.Text, ".[A-Z]") AndAlso
           Regex.IsMatch(CnfrmPasstxbx.Text, ".[0-9]") AndAlso
           Regex.IsMatch(CnfrmPasstxbx.Text, ".{6}") Then

            CnfrmPasstxbx.BorderColor = Color.FromArgb(36, 93, 170)

        Else
            CnfrmPasstxbx.BorderColor = Color.Red
        End If
    End Sub

    Private Sub Clearbtn_Click(sender As Object, e As EventArgs) Handles Clearbtn.Click
        clearAllFields()
    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        Clipboard.SetText(Label6.Text)
    End Sub

    Private Sub Viewbtn_Click(sender As Object, e As EventArgs) Handles Viewbtn.Click
        If Usertbl.ScrollBars = ScrollBars.Horizontal Then
            Usertbl.ScrollBars = ScrollBars.Both
        Else
            Usertbl.ScrollBars = ScrollBars.Horizontal
        End If
    End Sub
End Class
