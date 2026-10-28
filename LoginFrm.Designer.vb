<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoginFrm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoginFrm))
        Me.Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Guna2Panel2 = New Guna.UI2.WinForms.Guna2Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Guna2Panel3 = New Guna.UI2.WinForms.Guna2Panel()
        Me.Guna2Button2 = New Guna.UI2.WinForms.Guna2Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LogBtn = New Guna.UI2.WinForms.Guna2Button()
        Me.LogPasstxbx = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Usrnmtxtbx = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Guna2Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Guna2Panel2.SuspendLayout()
        Me.Guna2Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2Panel1
        '
        Me.Guna2Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.Guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Guna2Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Guna2Panel1.Name = "Guna2Panel1"
        Me.Guna2Panel1.Size = New System.Drawing.Size(800, 450)
        Me.Guna2Panel1.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 315.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Guna2Panel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Guna2Panel3, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(800, 450)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Guna2Panel2
        '
        Me.Guna2Panel2.BackgroundImage = CType(resources.GetObject("Guna2Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Guna2Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Guna2Panel2.Controls.Add(Me.Label2)
        Me.Guna2Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Guna2Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Guna2Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Guna2Panel2.Name = "Guna2Panel2"
        Me.Guna2Panel2.Size = New System.Drawing.Size(485, 450)
        Me.Guna2Panel2.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("HoloLens MDL2 Assets", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(9, 361)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(321, 68)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Campus Lost and Found" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Management System"
        '
        'Guna2Panel3
        '
        Me.Guna2Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Guna2Panel3.Controls.Add(Me.Guna2Button2)
        Me.Guna2Panel3.Controls.Add(Me.Label1)
        Me.Guna2Panel3.Controls.Add(Me.LogBtn)
        Me.Guna2Panel3.Controls.Add(Me.LogPasstxbx)
        Me.Guna2Panel3.Controls.Add(Me.Usrnmtxtbx)
        Me.Guna2Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Guna2Panel3.Location = New System.Drawing.Point(488, 3)
        Me.Guna2Panel3.Name = "Guna2Panel3"
        Me.Guna2Panel3.Size = New System.Drawing.Size(309, 444)
        Me.Guna2Panel3.TabIndex = 1
        '
        'Guna2Button2
        '
        Me.Guna2Button2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Guna2Button2.BorderRadius = 10
        Me.Guna2Button2.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.Guna2Button2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.Guna2Button2.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.Guna2Button2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Guna2Button2.FillColor = System.Drawing.Color.Transparent
        Me.Guna2Button2.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2Button2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.Guna2Button2.Location = New System.Drawing.Point(243, 9)
        Me.Guna2Button2.Name = "Guna2Button2"
        Me.Guna2Button2.Size = New System.Drawing.Size(57, 33)
        Me.Guna2Button2.TabIndex = 5
        Me.Guna2Button2.Text = "Exit"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(26, 107)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 19)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Login"
        '
        'LogBtn
        '
        Me.LogBtn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LogBtn.BorderRadius = 20
        Me.LogBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.LogBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.LogBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.LogBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.LogBtn.FillColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.LogBtn.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LogBtn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.LogBtn.Location = New System.Drawing.Point(21, 279)
        Me.LogBtn.Name = "LogBtn"
        Me.LogBtn.Size = New System.Drawing.Size(269, 44)
        Me.LogBtn.TabIndex = 3
        Me.LogBtn.Text = "Login"
        '
        'LogPasstxbx
        '
        Me.LogPasstxbx.BorderColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.LogPasstxbx.BorderRadius = 10
        Me.LogPasstxbx.BorderThickness = 2
        Me.LogPasstxbx.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.LogPasstxbx.DefaultText = ""
        Me.LogPasstxbx.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.LogPasstxbx.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.LogPasstxbx.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.LogPasstxbx.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.LogPasstxbx.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LogPasstxbx.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LogPasstxbx.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LogPasstxbx.Location = New System.Drawing.Point(21, 187)
        Me.LogPasstxbx.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LogPasstxbx.Name = "LogPasstxbx"
        Me.LogPasstxbx.PlaceholderText = "Password"
        Me.LogPasstxbx.SelectedText = ""
        Me.LogPasstxbx.Size = New System.Drawing.Size(269, 46)
        Me.LogPasstxbx.TabIndex = 1
        '
        'Usrnmtxtbx
        '
        Me.Usrnmtxtbx.BorderColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.Usrnmtxtbx.BorderRadius = 10
        Me.Usrnmtxtbx.BorderThickness = 2
        Me.Usrnmtxtbx.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Usrnmtxtbx.DefaultText = ""
        Me.Usrnmtxtbx.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.Usrnmtxtbx.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.Usrnmtxtbx.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.Usrnmtxtbx.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.Usrnmtxtbx.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Usrnmtxtbx.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Usrnmtxtbx.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Usrnmtxtbx.Location = New System.Drawing.Point(21, 133)
        Me.Usrnmtxtbx.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Usrnmtxtbx.Name = "Usrnmtxtbx"
        Me.Usrnmtxtbx.PlaceholderText = "Username"
        Me.Usrnmtxtbx.SelectedText = ""
        Me.Usrnmtxtbx.Size = New System.Drawing.Size(269, 46)
        Me.Usrnmtxtbx.TabIndex = 0
        '
        'LoginFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Guna2Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximumSize = New System.Drawing.Size(816, 489)
        Me.Name = "LoginFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Guna2Panel1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Guna2Panel2.ResumeLayout(False)
        Me.Guna2Panel3.ResumeLayout(False)
        Me.Guna2Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Guna2Panel2 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Guna2Panel3 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Usrnmtxtbx As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents LogPasstxbx As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents LogBtn As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Guna2Button2 As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Label2 As Label
End Class
