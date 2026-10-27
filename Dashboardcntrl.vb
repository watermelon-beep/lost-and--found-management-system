Imports Guna.Charts.WinForms
Imports System.Drawing
Public Class Dashboardcntrl
    Private Sub Dashboardcntrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' BAR CHART: Lost vs Found
        lostFoundChart.Datasets.Clear()

        Dim lostData As New GunaBarDataset()

        lostData.Label = "Items"
        lostData.FillColors.Add(Color.DodgerBlue)
        lostData.FillColors.Add(Color.DeepSkyBlue)

        lostData.DataPoints.Add(New LPoint With {
            .Label = "Lost Items",
            .Y = 12
        })

        lostData.DataPoints.Add(New LPoint With {
            .Label = "Found Items",
            .Y = 7
        })

        lostFoundChart.Datasets.Add(lostData)


        ' DOUGHNUT CHART: Claim Status
        claimChart.Datasets.Clear()

        Dim claimData As New GunaDoughnutDataset()

        claimData.Label = "Claims"

        claimData.FillColors.Add(Color.Orange)
        claimData.FillColors.Add(Color.DodgerBlue)
        claimData.FillColors.Add(Color.IndianRed)
        claimData.FillColors.Add(Color.SeaGreen)

        claimData.DataPoints.Add(New LPoint With {
            .Label = "Pending",
            .Y = 8
        })

        claimData.DataPoints.Add(New LPoint With {
            .Label = "Approved",
            .Y = 5
        })

        claimData.DataPoints.Add(New LPoint With {
            .Label = "Rejected",
            .Y = 2
        })

        claimData.DataPoints.Add(New LPoint With {
            .Label = "Completed",
            .Y = 12
        })

        claimChart.Datasets.Add(claimData)
    End Sub

End Class
