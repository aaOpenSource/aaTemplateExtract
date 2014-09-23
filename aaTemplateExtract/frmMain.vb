'The MIT License (MIT)
'
'Copyright (c) 2014 Eliot Landrum
'
'Permission is hereby granted, free of charge, to any person obtaining a copy of
'this software and associated documentation files (the "Software"), to deal in
'the Software without restriction, including without limitation the rights to
'use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
'the Software, and to permit persons to whom the Software is furnished to do so,
'subject to the following conditions:
'
'The above copyright notice and this permission notice shall be included in all
'copies or substantial portions of the Software.
'
'THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
'IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
'FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
'COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
'IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
'CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Imports System.IO
Imports System.Xml.Serialization

Public Class frmMain

    Dim aaTemplateExtract As aaTemplateExtract
    Dim AuthenticationMode As String
    Dim ExportFolder As String

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        lblStatus.Text = "Initializing"

        ' set up the new GRAccess client
        aaTemplateExtract = New aaTemplateExtract

        ' fill in a default node name
        txtNodeName.Text = Environment.MachineName

        ' get the list of Galaxies from the local node and fill the combo box with the collection
        cmboGalaxyList.DataSource = aaTemplateExtract.getGalaxies(txtNodeName.Text)

        ' do any UI clean up work that might be needed when the galaxy name changes
        refreshGalaxyInfo(cmboGalaxyList.SelectedValue)

        btnExport.Enabled = False

        lblStatus.Text = ""

    End Sub

    Private Sub cmboGalaxyList_SelectionChangeCommitted(ByVal sender As Object, ByVal e As EventArgs) Handles cmboGalaxyList.SelectionChangeCommitted
        Dim senderComboBox As ComboBox = CType(sender, ComboBox)

        If (senderComboBox.SelectedValue IsNot Nothing) Then
            refreshGalaxyInfo(senderComboBox.SelectedValue)
        Else
            clearGalaxyInfo()
        End If
    End Sub

    Public Sub refreshGalaxyInfo(ByVal GalaxyName)
        lblStatus.Text = "Refreshing Galaxy Info"
        aaTemplateExtract.setGalaxy(GalaxyName)
        grpLoginInput.Visible = aaTemplateExtract.showLogin
        lblSecurityType.Text = aaTemplateExtract.authMode

        If aaTemplateExtract.loggedIn Then
            ' this would happen if there's no security and we automatically login
            lstTemplates.DataSource = aaTemplateExtract.getTemplates()
        Else
            lstTemplates.Items.Clear()
        End If
        lblStatus.Text = ""
    End Sub

    Public Sub clearGalaxyInfo()
        lstTemplates.DataSource = Nothing
        txtUserInput.Clear()
        txtPwdInput.Clear()
        btnExport.Enabled = False
    End Sub

    Public Sub exportTemplatesToFile(ByVal FilePath As String, ByVal TemplateNames As String())
        Dim objStreamWriter As StreamWriter
        Dim TemplateDirectory As String
        Dim ScriptFile As String
        Dim AttributeFile As String
        Dim TemplateName As String

        ' I'm not a fan of all the xmlns:xsd and xmlns:xsi in the top of our XML document, so creating a new, blank namespace for writing
        Dim ns = New System.Xml.Serialization.XmlSerializerNamespaces()
        ns.Add("", "")

        Try
            For Each TemplateName In TemplateNames

                ' Setup our main template directory that each script file will be put into
                TemplateDirectory = FilePath + "\" + Replace(TemplateName, "$", "") + "\"
                My.Computer.FileSystem.CreateDirectory(TemplateDirectory)

                Dim TemplateData = aaTemplateExtract.getTemplateData(TemplateName)

                My.Computer.FileSystem.CreateDirectory(TemplateDirectory + "\Scripts\")
                For Each ScriptData As aaScript In TemplateData.Scripts
                    ScriptFile = TemplateDirectory + "\Scripts\" + ScriptData.Name + ".xml"
                    objStreamWriter = New StreamWriter(ScriptFile)
                    Dim x As New XmlSerializer(ScriptData.GetType)
                    x.Serialize(objStreamWriter, ScriptData, ns)
                    objStreamWriter.Close()
                Next

                My.Computer.FileSystem.CreateDirectory(TemplateDirectory + "\Attributes\")

                If TemplateData.FieldAttributesDiscrete.Count > 0 Then

                    AttributeFile = TemplateDirectory + "\Attributes\Field Attributes - Discrete.xml"
                    objStreamWriter = New StreamWriter(AttributeFile)
                    For Each AttributeData As aaFieldAttributeDiscrete In TemplateData.FieldAttributesDiscrete
                        Dim x As New XmlSerializer(AttributeData.GetType)
                        x.Serialize(objStreamWriter, AttributeData, ns)
                    Next
                    objStreamWriter.Close()

                End If

            Next
        Catch e As Exception
            MessageBox.Show("Error occurred: " & e.Message)
        End Try

    End Sub

    Private Sub linkLblSelectAll_Click(sender As Object, e As EventArgs) Handles linkLblSelectAll.Click
        For x = 1 To (lstTemplates.Items.Count - 1)
            lstTemplates.SetSelected(x, True)
        Next x
    End Sub


    Private Sub linkLblSelectNone_Click(sender As Object, e As EventArgs) Handles linkLblSelectNone.Click
        For x = 1 To (lstTemplates.Items.Count - 1)
            lstTemplates.SetSelected(x, False)
        Next x
    End Sub


    Private Sub btnBrowseFolders_Click(sender As Object, e As EventArgs) Handles btnBrowseFolders.Click
        If dlgFolderBrowser.ShowDialog() = DialogResult.OK Then
            ExportFolder = dlgFolderBrowser.SelectedPath
            lblFolderPath.Text = ExportFolder

            If lstTemplates.SelectedItems.Count > 0 And ExportFolder IsNot Nothing And aaTemplateExtract.loggedIn Then
                btnExport.Enabled = True
            Else
                btnExport.Enabled = False
            End If
        End If
    End Sub



    Private Sub btnRefreshGalaxies_Click(sender As Object, e As EventArgs) Handles btnRefreshGalaxies.Click
        lblStatus.Text = "Refreshing Available Galaxies"
        clearGalaxyInfo()

        ' get the list of Galaxies from the local node and fill the combo box with the collection
        cmboGalaxyList.DataSource = aaTemplateExtract.getGalaxies(txtNodeName.Text)

        ' do any UI clean up work that might be needed when the galaxy name changes
        refreshGalaxyInfo(cmboGalaxyList.SelectedValue)
        lblStatus.Text = ""
    End Sub


    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click

        ' There's probably a better way to convert a Collection to a String Array, but this will do for now
        Dim y As Integer
        y = lstTemplates.SelectedItems.Count - 1
        Dim TemplateNames(0 To y) As String

        Dim x As Integer = 0
        For Each Template In lstTemplates.SelectedItems
            TemplateNames(x) = Template
            x += 1
        Next

        lblStatus.Text = "Exporting " & lstTemplates.SelectedItems.Count.ToString & " Templates"

        'aaTemplateExtract.exportSelectedTemplates(ExportFolder, TemplateNames)

        exportTemplatesToFile(ExportFolder, TemplateNames)

        lblStatus.Text = ""

    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        lblStatus.Text = "Logging in"

        refreshGalaxyInfo(cmboGalaxyList.SelectedValue)

        If aaTemplateExtract.login(txtUserInput.Text, txtPwdInput.Text) >= 0 Then
            lstTemplates.DataSource = aaTemplateExtract.getTemplates(chkHideBaseTemplates.CheckState)
            lblStatus.Text = ""
        Else
            lblStatus.Text = "Error logging in"
        End If

    End Sub


    Private Sub btnRefreshTemplates_Click(sender As Object, e As EventArgs) Handles btnRefreshTemplates.Click
        lblStatus.Text = "Refreshing Template List"
        lstTemplates.DataSource = aaTemplateExtract.getTemplates(chkHideBaseTemplates.CheckState)
        lblStatus.Text = ""
    End Sub
End Class
