<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LostCntrl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LostCntrl))
        Me.PersonNameTxt = New Guna.UI2.WinForms.Guna2TextBox()
        Me.ItemNameTxt = New Guna.UI2.WinForms.Guna2TextBox()
        Me.CategoryCmb = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.TypeCmb = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.DescriptionTxt = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.ItemPic = New Guna.UI2.WinForms.Guna2PictureBox()
        Me.Addbtn = New Guna.UI2.WinForms.Guna2Button()
        Me.Clearbtn = New Guna.UI2.WinForms.Guna2Button()
        Me.LocationTxt = New Guna.UI2.WinForms.Guna2TextBox()
        Me.StorageTxt = New Guna.UI2.WinForms.Guna2TextBox()
        Me.DateLostFoundDtp = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.ContactTxt = New Guna.UI2.WinForms.Guna2TextBox()
        Me.StatusCmb = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Updatebtn = New Guna.UI2.WinForms.Guna2Button()
        Me.Guna2Panel1.SuspendLayout()
        CType(Me.ItemPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PersonNameTxt
        '
        Me.PersonNameTxt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PersonNameTxt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.PersonNameTxt.BorderRadius = 20
        Me.PersonNameTxt.BorderThickness = 2
        Me.PersonNameTxt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.PersonNameTxt.DefaultText = ""
        Me.PersonNameTxt.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.PersonNameTxt.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.PersonNameTxt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.PersonNameTxt.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.PersonNameTxt.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.PersonNameTxt.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.PersonNameTxt.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.PersonNameTxt.Location = New System.Drawing.Point(37, 76)
        Me.PersonNameTxt.Name = "PersonNameTxt"
        Me.PersonNameTxt.PlaceholderText = "Person name"
        Me.PersonNameTxt.SelectedText = ""
        Me.PersonNameTxt.Size = New System.Drawing.Size(224, 44)
        Me.PersonNameTxt.TabIndex = 0
        '
        'ItemNameTxt
        '
        Me.ItemNameTxt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ItemNameTxt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.ItemNameTxt.BorderRadius = 20
        Me.ItemNameTxt.BorderThickness = 2
        Me.ItemNameTxt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ItemNameTxt.DefaultText = ""
        Me.ItemNameTxt.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ItemNameTxt.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ItemNameTxt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.ItemNameTxt.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.ItemNameTxt.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ItemNameTxt.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ItemNameTxt.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ItemNameTxt.Location = New System.Drawing.Point(37, 17)
        Me.ItemNameTxt.Name = "ItemNameTxt"
        Me.ItemNameTxt.PlaceholderText = "Item name"
        Me.ItemNameTxt.SelectedText = ""
        Me.ItemNameTxt.Size = New System.Drawing.Size(224, 44)
        Me.ItemNameTxt.TabIndex = 1
        '
        'CategoryCmb
        '
        Me.CategoryCmb.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CategoryCmb.BackColor = System.Drawing.Color.Transparent
        Me.CategoryCmb.BorderColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.CategoryCmb.BorderRadius = 20
        Me.CategoryCmb.BorderThickness = 2
        Me.CategoryCmb.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CategoryCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CategoryCmb.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CategoryCmb.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CategoryCmb.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.CategoryCmb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.CategoryCmb.ItemHeight = 40
        Me.CategoryCmb.Items.AddRange(New Object() {"Others", "Electronics", "ID", "Documents", "Bags", "Clothing", "Personal Items"})
        Me.CategoryCmb.Location = New System.Drawing.Point(37, 147)
        Me.CategoryCmb.Name = "CategoryCmb"
        Me.CategoryCmb.Size = New System.Drawing.Size(224, 46)
        Me.CategoryCmb.StartIndex = 0
        Me.CategoryCmb.TabIndex = 8
        Me.CategoryCmb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TypeCmb
        '
        Me.TypeCmb.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TypeCmb.BackColor = System.Drawing.Color.Transparent
        Me.TypeCmb.BorderColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.TypeCmb.BorderRadius = 20
        Me.TypeCmb.BorderThickness = 2
        Me.TypeCmb.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.TypeCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TypeCmb.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TypeCmb.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TypeCmb.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TypeCmb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.TypeCmb.ItemHeight = 40
        Me.TypeCmb.Items.AddRange(New Object() {"All", "Lost", "Found"})
        Me.TypeCmb.Location = New System.Drawing.Point(267, 147)
        Me.TypeCmb.Name = "TypeCmb"
        Me.TypeCmb.Size = New System.Drawing.Size(224, 46)
        Me.TypeCmb.StartIndex = 0
        Me.TypeCmb.TabIndex = 9
        Me.TypeCmb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DescriptionTxt
        '
        Me.DescriptionTxt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.DescriptionTxt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.DescriptionTxt.BorderRadius = 10
        Me.DescriptionTxt.BorderThickness = 2
        Me.DescriptionTxt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.DescriptionTxt.DefaultText = ""
        Me.DescriptionTxt.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.DescriptionTxt.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.DescriptionTxt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.DescriptionTxt.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.DescriptionTxt.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DescriptionTxt.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.DescriptionTxt.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DescriptionTxt.Location = New System.Drawing.Point(1063, 20)
        Me.DescriptionTxt.Multiline = True
        Me.DescriptionTxt.Name = "DescriptionTxt"
        Me.DescriptionTxt.PlaceholderText = "Description..."
        Me.DescriptionTxt.SelectedText = ""
        Me.DescriptionTxt.Size = New System.Drawing.Size(473, 173)
        Me.DescriptionTxt.TabIndex = 10
        '
        'Guna2Panel1
        '
        Me.Guna2Panel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Guna2Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2Panel1.BorderColor = System.Drawing.Color.Silver
        Me.Guna2Panel1.BorderRadius = 10
        Me.Guna2Panel1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash
        Me.Guna2Panel1.BorderThickness = 5
        Me.Guna2Panel1.Controls.Add(Me.ItemPic)
        Me.Guna2Panel1.Location = New System.Drawing.Point(732, 29)
        Me.Guna2Panel1.Name = "Guna2Panel1"
        Me.Guna2Panel1.Size = New System.Drawing.Size(321, 173)
        Me.Guna2Panel1.TabIndex = 11
        '
        'ItemPic
        '
        Me.ItemPic.BackColor = System.Drawing.Color.Transparent
        Me.ItemPic.BackgroundImage = CType(resources.GetObject("ItemPic.BackgroundImage"), System.Drawing.Image)
        Me.ItemPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ItemPic.BorderRadius = 10
        Me.ItemPic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ItemPic.FillColor = System.Drawing.Color.Transparent
        Me.ItemPic.ImageRotate = 0!
        Me.ItemPic.Location = New System.Drawing.Point(0, 0)
        Me.ItemPic.Name = "ItemPic"
        Me.ItemPic.Size = New System.Drawing.Size(321, 173)
        Me.ItemPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ItemPic.TabIndex = 0
        Me.ItemPic.TabStop = False
        '
        'Addbtn
        '
        Me.Addbtn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Addbtn.BorderRadius = 10
        Me.Addbtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.Addbtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.Addbtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.Addbtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Addbtn.FillColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.Addbtn.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Addbtn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.Addbtn.Location = New System.Drawing.Point(37, 239)
        Me.Addbtn.Name = "Addbtn"
        Me.Addbtn.Size = New System.Drawing.Size(496, 52)
        Me.Addbtn.TabIndex = 12
        Me.Addbtn.Text = "Submit"
        '
        'Clearbtn
        '
        Me.Clearbtn.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Clearbtn.BackColor = System.Drawing.Color.Transparent
        Me.Clearbtn.BorderColor = System.Drawing.Color.Transparent
        Me.Clearbtn.BorderRadius = 10
        Me.Clearbtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.Clearbtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.Clearbtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.Clearbtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Clearbtn.FillColor = System.Drawing.Color.Transparent
        Me.Clearbtn.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Clearbtn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.Clearbtn.Location = New System.Drawing.Point(1040, 239)
        Me.Clearbtn.Name = "Clearbtn"
        Me.Clearbtn.Size = New System.Drawing.Size(496, 52)
        Me.Clearbtn.TabIndex = 13
        Me.Clearbtn.Text = "Clear"
        '
        'LocationTxt
        '
        Me.LocationTxt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.LocationTxt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.LocationTxt.BorderRadius = 20
        Me.LocationTxt.BorderThickness = 2
        Me.LocationTxt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.LocationTxt.DefaultText = ""
        Me.LocationTxt.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.LocationTxt.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.LocationTxt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.LocationTxt.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.LocationTxt.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LocationTxt.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LocationTxt.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LocationTxt.Location = New System.Drawing.Point(267, 17)
        Me.LocationTxt.Name = "LocationTxt"
        Me.LocationTxt.PlaceholderText = "Location"
        Me.LocationTxt.SelectedText = ""
        Me.LocationTxt.Size = New System.Drawing.Size(224, 44)
        Me.LocationTxt.TabIndex = 14
        '
        'StorageTxt
        '
        Me.StorageTxt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.StorageTxt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.StorageTxt.BorderRadius = 20
        Me.StorageTxt.BorderThickness = 2
        Me.StorageTxt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.StorageTxt.DefaultText = ""
        Me.StorageTxt.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.StorageTxt.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.StorageTxt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.StorageTxt.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.StorageTxt.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.StorageTxt.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.StorageTxt.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.StorageTxt.Location = New System.Drawing.Point(497, 17)
        Me.StorageTxt.Name = "StorageTxt"
        Me.StorageTxt.PlaceholderText = "Storage Location"
        Me.StorageTxt.SelectedText = ""
        Me.StorageTxt.Size = New System.Drawing.Size(224, 44)
        Me.StorageTxt.TabIndex = 15
        '
        'DateLostFoundDtp
        '
        Me.DateLostFoundDtp.BorderRadius = 20
        Me.DateLostFoundDtp.Checked = True
        Me.DateLostFoundDtp.FillColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.DateLostFoundDtp.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateLostFoundDtp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.DateLostFoundDtp.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.DateLostFoundDtp.Location = New System.Drawing.Point(497, 76)
        Me.DateLostFoundDtp.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DateLostFoundDtp.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DateLostFoundDtp.Name = "DateLostFoundDtp"
        Me.DateLostFoundDtp.Size = New System.Drawing.Size(224, 44)
        Me.DateLostFoundDtp.TabIndex = 20
        Me.DateLostFoundDtp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.DateLostFoundDtp.Value = New Date(2026, 10, 28, 9, 49, 28, 860)
        '
        'ContactTxt
        '
        Me.ContactTxt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ContactTxt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.ContactTxt.BorderRadius = 20
        Me.ContactTxt.BorderThickness = 2
        Me.ContactTxt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ContactTxt.DefaultText = ""
        Me.ContactTxt.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ContactTxt.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ContactTxt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.ContactTxt.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.ContactTxt.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ContactTxt.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ContactTxt.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ContactTxt.Location = New System.Drawing.Point(267, 76)
        Me.ContactTxt.Name = "ContactTxt"
        Me.ContactTxt.PlaceholderText = "Contact Number"
        Me.ContactTxt.SelectedText = ""
        Me.ContactTxt.Size = New System.Drawing.Size(224, 44)
        Me.ContactTxt.TabIndex = 21
        '
        'StatusCmb
        '
        Me.StatusCmb.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.StatusCmb.BackColor = System.Drawing.Color.Transparent
        Me.StatusCmb.BorderColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.StatusCmb.BorderRadius = 20
        Me.StatusCmb.BorderThickness = 2
        Me.StatusCmb.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.StatusCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.StatusCmb.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.StatusCmb.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.StatusCmb.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.StatusCmb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.StatusCmb.ItemHeight = 40
        Me.StatusCmb.Items.AddRange(New Object() {"Unclaimed", "Claimed"})
        Me.StatusCmb.Location = New System.Drawing.Point(497, 147)
        Me.StatusCmb.Name = "StatusCmb"
        Me.StatusCmb.Size = New System.Drawing.Size(224, 46)
        Me.StatusCmb.StartIndex = 0
        Me.StatusCmb.TabIndex = 22
        Me.StatusCmb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Updatebtn
        '
        Me.Updatebtn.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Updatebtn.BackColor = System.Drawing.Color.Transparent
        Me.Updatebtn.BorderColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.Updatebtn.BorderRadius = 10
        Me.Updatebtn.BorderThickness = 2
        Me.Updatebtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.Updatebtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.Updatebtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.Updatebtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Updatebtn.FillColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.Updatebtn.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Updatebtn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.Updatebtn.Location = New System.Drawing.Point(539, 239)
        Me.Updatebtn.Name = "Updatebtn"
        Me.Updatebtn.Size = New System.Drawing.Size(496, 52)
        Me.Updatebtn.TabIndex = 23
        Me.Updatebtn.Text = "Update"
        '
        'LostCntrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.Controls.Add(Me.Updatebtn)
        Me.Controls.Add(Me.StatusCmb)
        Me.Controls.Add(Me.ContactTxt)
        Me.Controls.Add(Me.DateLostFoundDtp)
        Me.Controls.Add(Me.StorageTxt)
        Me.Controls.Add(Me.LocationTxt)
        Me.Controls.Add(Me.Clearbtn)
        Me.Controls.Add(Me.Addbtn)
        Me.Controls.Add(Me.Guna2Panel1)
        Me.Controls.Add(Me.DescriptionTxt)
        Me.Controls.Add(Me.TypeCmb)
        Me.Controls.Add(Me.CategoryCmb)
        Me.Controls.Add(Me.ItemNameTxt)
        Me.Controls.Add(Me.PersonNameTxt)
        Me.Name = "LostCntrl"
        Me.Size = New System.Drawing.Size(1585, 309)
        Me.Guna2Panel1.ResumeLayout(False)
        CType(Me.ItemPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PersonNameTxt As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents ItemNameTxt As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents CategoryCmb As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents TypeCmb As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents DescriptionTxt As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents ItemPic As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents Addbtn As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Clearbtn As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents LocationTxt As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents StorageTxt As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents DateLostFoundDtp As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents ContactTxt As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents StatusCmb As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Updatebtn As Guna.UI2.WinForms.Guna2Button
End Class
