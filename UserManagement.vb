Imports System.Data.SqlClient
Imports System.IO
Imports Guna.UI2.WinForms
Imports System.Text.RegularExpressions

Public Class UserManagement

    Dim updateTriggered As Boolean = False
    Dim isLoading As Boolean = False
    Private selectedStaffId As String = ""

    Private Function calcAge(birth As Date) As Integer
        Dim result As Integer = Date.Today.Year - birth.Year
        If birth.Date > Date.Today.AddYears(-result) Then result -= 1
        If result < 0 Then result = 0
        Return result
    End Function

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
        Brthdt.Value = Brthdt.MaxDate
        Agetxbx.Clear()
        CntctNumtxbx.Clear()
        Emailtxbx.Clear()
        RegUsrnmtxbx.Clear()
        RegPasstxbx.Clear()
        CnfrmPasstxbx.Clear()
        Statuscmbx.SelectedIndex = 0
        UsrImg.Image = Nothing
    End Sub

    Private Sub resetBorders()
        Dim normal As Color = Color.FromArgb(36, 93, 170)
        CntctNumtxbx.BorderColor = normal
        Emailtxbx.BorderColor = normal
        RegUsrnmtxbx.BorderColor = normal
        RegPasstxbx.BorderColor = normal
        CnfrmPasstxbx.BorderColor = normal
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
            Dim oldNo As Integer
            Using c As New SqlCommand("SELECT StaffNo FROM user_table WHERE StaffID = @id", conn)
                c.Parameters.AddWithValue("@id", id)
                Dim r As Object = c.ExecuteScalar()
                If r Is Nothing Then
                    MsgBox("No user found with the specified StaffID.")
                    Return
                End If
                oldNo = Convert.ToInt32(r)
            End Using

            If oldNo = currentStaffNo Then
                MsgBox("You cannot delete your own account.")
                Return
            End If

            Using tran As SqlTransaction = conn.BeginTransaction()
                Try
                    Dim sqls() As String = {
                    "UPDATE Items SET RecordedBy = @new WHERE RecordedBy = @old",
                    "UPDATE Claims SET ProcessedBy = @new WHERE ProcessedBy = @old",
                    "DELETE FROM user_table WHERE StaffNo = @old"
                }

                    For Each s As String In sqls
                        Using cmd As New SqlCommand(s, conn, tran)
                            cmd.Parameters.AddWithValue("@old", oldNo)
                            cmd.Parameters.AddWithValue("@new", currentStaffNo)
                            cmd.ExecuteNonQuery()
                        End Using
                    Next

                    tran.Commit()
                Catch
                    tran.Rollback()
                    Throw
                End Try
            End Using

            MsgBox("User deleted. Their items and claims were transferred to you.")
            viewUserTable()

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
                    If dataRead.Read() Then
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
                    End If
                End Using
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub viewUserTable()
        Try
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
                comm.CommandTimeout = 120

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

    Private Sub updateAction(id As String)
        isLoading = True
        Try
            selectedStaffId = id

            query = "SELECT * FROM user_table WHERE StaffID = @StaffID"

            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@StaffID", id)

                Using dataRead = comm.ExecuteReader()
                    If dataRead.Read() Then
                        FrstNmtxbx.Text = dataRead("FirstName").ToString()
                        MddlNmtxbx.Text = dataRead("MiddleName").ToString()
                        Srnmtxbx.Text = dataRead("LastName").ToString()
                        Gndrcmbx.Text = dataRead("Gender").ToString()
                        Rolecmbx.Text = dataRead("Role").ToString()
                        Brthdt.Value = Convert.ToDateTime(dataRead("Birthdate"))
                        Agetxbx.Text = calcAge(Brthdt.Value).ToString()
                        CntctNumtxbx.Text = dataRead("ContactNumber").ToString()
                        Emailtxbx.Text = dataRead("Email").ToString()
                        RegUsrnmtxbx.Text = dataRead("Username").ToString()
                        RegPasstxbx.Clear()
                        CnfrmPasstxbx.Clear()
                        Statuscmbx.Text = dataRead("AccountStatus").ToString()
                    End If
                End Using
            End Using

        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            isLoading = False
            resetBorders()
        End Try
    End Sub

    Private Function updateUser() As Boolean
        Try
            If selectedStaffId = "" Then
                MsgBox("Choose a user to update.")
                Return False
            End If

            If FrstNmtxbx.Text.Trim() = "" OrElse Srnmtxbx.Text.Trim() = "" OrElse RegUsrnmtxbx.Text.Trim() = "" Then
                MsgBox("First name, surname and username are required.")
                Return False
            End If

            Dim birth As Date = Brthdt.Value.Date
            If birth > Date.Today Then
                MsgBox("Birthdate cannot be in the future.")
                Return False
            End If
            Dim userAge As Integer = calcAge(birth)

            Dim changePassword As Boolean = RegPasstxbx.Text <> ""
            If changePassword AndAlso RegPasstxbx.Text <> CnfrmPasstxbx.Text Then
                MsgBox("Passwords do not match.")
                Return False
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
                                       UserImg = ISNULL(@UserImg, UserImg),
                                       AccountStatus = @AccountStatus"
            If changePassword Then query &= ", UserPassword = @UserPassword"
            query &= " WHERE StaffID = @StaffID"

            Using comm As New SqlCommand(query, conn)
                With comm.Parameters
                    .AddWithValue("@StaffID", selectedStaffId)
                    .AddWithValue("@FirstName", FrstNmtxbx.Text.Trim())
                    .AddWithValue("@MiddleName", MddlNmtxbx.Text.Trim())
                    .AddWithValue("@LastName", Srnmtxbx.Text.Trim())
                    .AddWithValue("@Gender", Gndrcmbx.Text)
                    .AddWithValue("@Role", Rolecmbx.Text)
                    .AddWithValue("@Birthdate", birth)
                    .AddWithValue("@Age", userAge)
                    .AddWithValue("@ContactNumber", CntctNumtxbx.Text.Trim())
                    .AddWithValue("@Email", Emailtxbx.Text.Trim())
                    .AddWithValue("@Username", RegUsrnmtxbx.Text.Trim())
                    .Add("@UserImg", SqlDbType.VarBinary, -1).Value = imgData
                    .AddWithValue("@AccountStatus", Statuscmbx.Text)
                    If changePassword Then .AddWithValue("@UserPassword", RegPasstxbx.Text)
                End With

                Dim rows As Integer = comm.ExecuteNonQuery()

                If rows > 0 Then
                    MsgBox("Updated.")
                    viewUserTable()
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

    Private Function addUser() As Boolean
        Try
            If FrstNmtxbx.Text.Trim() = "" OrElse Srnmtxbx.Text.Trim() = "" OrElse RegUsrnmtxbx.Text.Trim() = "" Then
                MsgBox("First name, surname and username are required.")
                Return False
            End If

            If RegPasstxbx.Text = "" Then
                MsgBox("Password is required.")
                Return False
            End If

            If RegPasstxbx.Text <> CnfrmPasstxbx.Text Then
                MsgBox("Passwords do not match.")
                Return False
            End If

            Dim birth As Date = Brthdt.Value.Date
            If birth > Date.Today Then
                MsgBox("Birthdate cannot be in the future.")
                Return False
            End If

            Dim imgData As Object = DBNull.Value
            If UsrImg.Image IsNot Nothing Then
                Using ms As New MemoryStream
                    UsrImg.Image.Save(ms, Imaging.ImageFormat.Png)
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
                    .AddWithValue("@FirstName", FrstNmtxbx.Text.Trim())
                    .AddWithValue("@MiddleName", MddlNmtxbx.Text.Trim())
                    .AddWithValue("@LastName", Srnmtxbx.Text.Trim())
                    .AddWithValue("@Gender", Gndrcmbx.Text)
                    .AddWithValue("@Role", Rolecmbx.Text)
                    .AddWithValue("@Birthdate", birth)
                    .AddWithValue("@Age", calcAge(birth))
                    .AddWithValue("@ContactNumber", CntctNumtxbx.Text.Trim())
                    .AddWithValue("@Email", Emailtxbx.Text.Trim())
                    .AddWithValue("@Username", RegUsrnmtxbx.Text.Trim())
                    .AddWithValue("@UserPassword", RegPasstxbx.Text)
                    .Add("@UserImg", SqlDbType.VarBinary, -1).Value = imgData
                    .AddWithValue("@AccountStatus", Statuscmbx.Text)
                End With

                comm.ExecuteNonQuery()

                MsgBox("Added.")
                viewUserTable()
                Return True
            End Using

        Catch ex As Exception
            MsgBox(ex.ToString())
            Return False
        End Try
    End Function

    Private Sub RegPasstxbx_TextChanged(sender As Object, e As EventArgs) Handles RegPasstxbx.TextChanged
        If isLoading Then Return

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
        If addUser() Then
            clearAllFields()
        End If
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
        If Usertbl.Rows.Count > 0 Then
            Dim firstId As String = Usertbl.Rows(0).Cells("DataGridViewTextBoxColumn9").Value.ToString()
            viewProfile(firstId)
            ViewPhoto(firstId, Guna2PictureBox1)
        End If
    End Sub

    Private Sub Updtbtn_Click(sender As Object, e As EventArgs) Handles Updtbtn.Click
        trimAllTextBoxes()
        If updateUser() Then
            clearAllFields()
            selectedStaffId = ""
            updateTriggered = False
        End If
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

    Private Sub CntctNumtxbx_TextChanged(sender As Object, e As EventArgs) Handles CntctNumtxbx.TextChanged
        If isLoading Then Return

        If Not Regex.IsMatch(CntctNumtxbx.Text, "^[0-9]*$") Or CntctNumtxbx.Text.Length > 11 Then
            CntctNumtxbx.Text = CntctNumtxbx.Text.Remove(CntctNumtxbx.Text.Length - 1)
            CntctNumtxbx.SelectionStart = CntctNumtxbx.Text.Length
            Return
        End If

        If CntctNumtxbx.Text <> "" AndAlso updateTriggered = False AndAlso contactNumExist(CntctNumtxbx.Text) Then
            CntctNumtxbx.BorderColor = Color.Red
            MsgBox("Contact number already exists. Please choose a different contact number.")
        End If
    End Sub

    Private Sub Emailtxbx_TextChanged(sender As Object, e As EventArgs) Handles Emailtxbx.TextChanged
        If isLoading Then Return

        If Emailtxbx.Text <> "" AndAlso updateTriggered = False AndAlso emailExist(Emailtxbx.Text) Then
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
        If isLoading Then Return

        If RegUsrnmtxbx.Text <> "" AndAlso updateTriggered = False AndAlso userLogExist(RegUsrnmtxbx.Text) Then
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
        If isLoading Then Return

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
        selectedStaffId = ""
        updateTriggered = False
        resetBorders()
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

    Private Sub Brthdt_ValueChanged(sender As Object, e As EventArgs) Handles Brthdt.ValueChanged
        Agetxbx.Text = calcAge(Brthdt.Value).ToString()
    End Sub
End Class