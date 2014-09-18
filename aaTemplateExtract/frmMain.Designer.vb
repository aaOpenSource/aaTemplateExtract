<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.cmboGalaxyList = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.grpLoginInput = New System.Windows.Forms.GroupBox()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPwdInput = New System.Windows.Forms.TextBox()
        Me.txtUserInput = New System.Windows.Forms.TextBox()
        Me.lstTemplates = New System.Windows.Forms.ListBox()
        Me.linkLblSelectAll = New System.Windows.Forms.LinkLabel()
        Me.linkLblSelectNone = New System.Windows.Forms.LinkLabel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dlgFolderBrowser = New System.Windows.Forms.FolderBrowserDialog()
        Me.btnBrowseFolders = New System.Windows.Forms.Button()
        Me.lblFolderPath = New System.Windows.Forms.Label()
        Me.lblSecurityType = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtNodeName = New System.Windows.Forms.TextBox()
        Me.btnRefreshGalaxies = New System.Windows.Forms.Button()
        Me.btnRefreshTemplates = New System.Windows.Forms.Button()
        Me.chkHideBaseTemplates = New System.Windows.Forms.CheckBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.grpLoginInput.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmboGalaxyList
        '
        Me.cmboGalaxyList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmboGalaxyList.FormattingEnabled = True
        Me.cmboGalaxyList.Location = New System.Drawing.Point(93, 51)
        Me.cmboGalaxyList.Name = "cmboGalaxyList"
        Me.cmboGalaxyList.Size = New System.Drawing.Size(253, 21)
        Me.cmboGalaxyList.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Galaxy:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(32, 198)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Templates:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(32, 352)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Export Path:"
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(33, 381)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(75, 23)
        Me.btnExport.TabIndex = 10
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(126, 386)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(216, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Warning! May overwrite existing files in path."
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(32, 77)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Security type:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(110, 77)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(0, 13)
        Me.Label8.TabIndex = 13
        Me.Label8.Tag = "authenticationMode"
        '
        'grpLoginInput
        '
        Me.grpLoginInput.Controls.Add(Me.btnLogin)
        Me.grpLoginInput.Controls.Add(Me.Label3)
        Me.grpLoginInput.Controls.Add(Me.Label2)
        Me.grpLoginInput.Controls.Add(Me.txtPwdInput)
        Me.grpLoginInput.Controls.Add(Me.txtUserInput)
        Me.grpLoginInput.Location = New System.Drawing.Point(103, 93)
        Me.grpLoginInput.Name = "grpLoginInput"
        Me.grpLoginInput.Size = New System.Drawing.Size(233, 99)
        Me.grpLoginInput.TabIndex = 14
        Me.grpLoginInput.TabStop = False
        '
        'btnLogin
        '
        Me.btnLogin.Location = New System.Drawing.Point(99, 68)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(105, 21)
        Me.btnLogin.TabIndex = 11
        Me.btnLogin.Text = "Login"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Password:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "User:"
        '
        'txtPwdInput
        '
        Me.txtPwdInput.Location = New System.Drawing.Point(74, 42)
        Me.txtPwdInput.Name = "txtPwdInput"
        Me.txtPwdInput.Size = New System.Drawing.Size(147, 20)
        Me.txtPwdInput.TabIndex = 8
        Me.txtPwdInput.UseSystemPasswordChar = True
        '
        'txtUserInput
        '
        Me.txtUserInput.Location = New System.Drawing.Point(74, 15)
        Me.txtUserInput.Name = "txtUserInput"
        Me.txtUserInput.Size = New System.Drawing.Size(147, 20)
        Me.txtUserInput.TabIndex = 7
        '
        'lstTemplates
        '
        Me.lstTemplates.FormattingEnabled = True
        Me.lstTemplates.Location = New System.Drawing.Point(93, 198)
        Me.lstTemplates.Name = "lstTemplates"
        Me.lstTemplates.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstTemplates.Size = New System.Drawing.Size(253, 95)
        Me.lstTemplates.Sorted = True
        Me.lstTemplates.TabIndex = 15
        '
        'linkLblSelectAll
        '
        Me.linkLblSelectAll.AutoSize = True
        Me.linkLblSelectAll.Location = New System.Drawing.Point(289, 300)
        Me.linkLblSelectAll.Name = "linkLblSelectAll"
        Me.linkLblSelectAll.Size = New System.Drawing.Size(18, 13)
        Me.linkLblSelectAll.TabIndex = 16
        Me.linkLblSelectAll.TabStop = True
        Me.linkLblSelectAll.Text = "All"
        '
        'linkLblSelectNone
        '
        Me.linkLblSelectNone.AutoSize = True
        Me.linkLblSelectNone.Location = New System.Drawing.Point(313, 300)
        Me.linkLblSelectNone.Name = "linkLblSelectNone"
        Me.linkLblSelectNone.Size = New System.Drawing.Size(33, 13)
        Me.linkLblSelectNone.TabIndex = 17
        Me.linkLblSelectNone.TabStop = True
        Me.linkLblSelectNone.Text = "None"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(243, 300)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(40, 13)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "Select:"
        '
        'dlgFolderBrowser
        '
        Me.dlgFolderBrowser.RootFolder = System.Environment.SpecialFolder.MyDocuments
        '
        'btnBrowseFolders
        '
        Me.btnBrowseFolders.Location = New System.Drawing.Point(33, 326)
        Me.btnBrowseFolders.Name = "btnBrowseFolders"
        Me.btnBrowseFolders.Size = New System.Drawing.Size(104, 23)
        Me.btnBrowseFolders.TabIndex = 19
        Me.btnBrowseFolders.Text = "Select Export Path"
        Me.btnBrowseFolders.UseVisualStyleBackColor = True
        '
        'lblFolderPath
        '
        Me.lblFolderPath.AutoSize = True
        Me.lblFolderPath.Location = New System.Drawing.Point(98, 352)
        Me.lblFolderPath.Name = "lblFolderPath"
        Me.lblFolderPath.Size = New System.Drawing.Size(10, 13)
        Me.lblFolderPath.TabIndex = 20
        Me.lblFolderPath.Text = " "
        '
        'lblSecurityType
        '
        Me.lblSecurityType.AutoSize = True
        Me.lblSecurityType.Location = New System.Drawing.Point(100, 77)
        Me.lblSecurityType.Name = "lblSecurityType"
        Me.lblSecurityType.Size = New System.Drawing.Size(10, 13)
        Me.lblSecurityType.TabIndex = 21
        Me.lblSecurityType.Text = " "
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(32, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(36, 13)
        Me.Label10.TabIndex = 22
        Me.Label10.Text = "Node:"
        '
        'txtNodeName
        '
        Me.txtNodeName.Location = New System.Drawing.Point(93, 20)
        Me.txtNodeName.Name = "txtNodeName"
        Me.txtNodeName.Size = New System.Drawing.Size(214, 20)
        Me.txtNodeName.TabIndex = 23
        '
        'btnRefreshGalaxies
        '
        Me.btnRefreshGalaxies.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnRefreshGalaxies.Location = New System.Drawing.Point(316, 20)
        Me.btnRefreshGalaxies.Name = "btnRefreshGalaxies"
        Me.btnRefreshGalaxies.Size = New System.Drawing.Size(30, 25)
        Me.btnRefreshGalaxies.TabIndex = 24
        Me.btnRefreshGalaxies.Text = "q"
        Me.btnRefreshGalaxies.UseVisualStyleBackColor = True
        '
        'btnRefreshTemplates
        '
        Me.btnRefreshTemplates.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnRefreshTemplates.Location = New System.Drawing.Point(48, 214)
        Me.btnRefreshTemplates.Name = "btnRefreshTemplates"
        Me.btnRefreshTemplates.Size = New System.Drawing.Size(30, 25)
        Me.btnRefreshTemplates.TabIndex = 27
        Me.btnRefreshTemplates.Text = "q"
        Me.btnRefreshTemplates.UseVisualStyleBackColor = True
        '
        'chkHideBaseTemplates
        '
        Me.chkHideBaseTemplates.AutoSize = True
        Me.chkHideBaseTemplates.Checked = True
        Me.chkHideBaseTemplates.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkHideBaseTemplates.Location = New System.Drawing.Point(96, 298)
        Me.chkHideBaseTemplates.Name = "chkHideBaseTemplates"
        Me.chkHideBaseTemplates.Size = New System.Drawing.Size(127, 17)
        Me.chkHideBaseTemplates.TabIndex = 28
        Me.chkHideBaseTemplates.Text = "Hide Base Templates"
        Me.chkHideBaseTemplates.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 417)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(390, 22)
        Me.StatusStrip1.TabIndex = 29
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 17)
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(390, 439)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.chkHideBaseTemplates)
        Me.Controls.Add(Me.btnRefreshTemplates)
        Me.Controls.Add(Me.btnRefreshGalaxies)
        Me.Controls.Add(Me.txtNodeName)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.lblSecurityType)
        Me.Controls.Add(Me.lblFolderPath)
        Me.Controls.Add(Me.btnBrowseFolders)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.linkLblSelectNone)
        Me.Controls.Add(Me.linkLblSelectAll)
        Me.Controls.Add(Me.lstTemplates)
        Me.Controls.Add(Me.grpLoginInput)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmboGalaxyList)
        Me.Name = "frmMain"
        Me.Text = "aaTemplateExtract"
        Me.grpLoginInput.ResumeLayout(False)
        Me.grpLoginInput.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmboGalaxyList As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents grpLoginInput As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPwdInput As System.Windows.Forms.TextBox
    Friend WithEvents txtUserInput As System.Windows.Forms.TextBox
    Friend WithEvents lstTemplates As System.Windows.Forms.ListBox
    Friend WithEvents linkLblSelectAll As System.Windows.Forms.LinkLabel
    Friend WithEvents linkLblSelectNone As System.Windows.Forms.LinkLabel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dlgFolderBrowser As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btnBrowseFolders As System.Windows.Forms.Button
    Friend WithEvents lblFolderPath As System.Windows.Forms.Label
    Friend WithEvents lblSecurityType As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtNodeName As System.Windows.Forms.TextBox
    Friend WithEvents btnRefreshGalaxies As System.Windows.Forms.Button
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents btnRefreshTemplates As System.Windows.Forms.Button
    Friend WithEvents chkHideBaseTemplates As System.Windows.Forms.CheckBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel

End Class
