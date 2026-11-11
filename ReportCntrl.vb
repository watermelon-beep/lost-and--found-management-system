Imports Microsoft.Reporting.WinForms
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class ReportCntrl
    Private Sub LoadLostReport()

        Dim dt As New DataTable

        Dim connectionString As String =
            "Server=.\SQLEXPRESS;Database=lost_and_found;Trusted_Connection=True;TrustServerCertificate=True"

        Dim query As String =
            "SELECT ItemID, ItemName, Category, Description,
                DateLostFound, DateReported, Location, Status
         FROM Items
         WHERE ItemType = 'Lost'"

        Try

            Using conn As New SqlConnection(connectionString)

                Using comm As New SqlCommand(query, conn)

                    Using adapter As New SqlDataAdapter(comm)

                        adapter.Fill(dt)

                    End Using

                End Using

            End Using

            ReportViewer1.LocalReport.DataSources.Clear()

            ReportViewer1.LocalReport.ReportPath =
                Path.Combine(Application.StartupPath, "ReportLostFound.rdlc")

            Dim reportData As New ReportDataSource("DataSet1", dt)

            ReportViewer1.LocalReport.DataSources.Add(reportData)

            ReportViewer1.RefreshReport()

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub

    Private Sub ReportCntrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLostReport()
    End Sub
End Class
