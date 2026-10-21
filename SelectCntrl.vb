Module SelectCntrl
    Public Sub selectedControl(control As UserControl, panel As Panel)

        panel.Controls.Clear()
        control.Dock = DockStyle.Fill
        panel.Controls.Add(control)
    End Sub
End Module
