Imports System.Data.SqlClient

Module DbConnection
    Public query As String
    Public conn As New SqlConnection
    Public comm As New SqlCommand
    Public dataRead As SqlDataReader

    Public Sub sqlconnection()

        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.ConnectionString = "Server= .\SQLEXPRESS; Database = lost_and_found; Trusted_Connection = True; MultipleActiveResultSets = True"
            conn.Open()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Function userLogExist(username As String) As Boolean
        Try
            query = "SELECT COUNT(*) FROM user_table WHERE Username = @Username"

            Using comm As New SqlCommand(query, conn)
                comm.Parameters.AddWithValue("@Username", username)
                Dim count As Integer = Convert.ToInt32(comm.ExecuteScalar)

                Return count > 0
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return False
    End Function

End Module
