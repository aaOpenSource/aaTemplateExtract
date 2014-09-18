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
Imports aaGRAccessApp

Public Class aaTemplateExtract

    ' Author: Eliot Landrum <elandrum@stonetek.com>
    ' Description: This class is the interface to GRAccess to communicate and pull data out of the Galaxy

    Public authMode As String
    Public showLogin As Boolean
    Public errorMessage As String
    Public loggedIn As Boolean

    Private resultStatus As Boolean
    Private resultCount As Integer
    Private galaxyNode As String
    Private galaxyName As String
    Private templateName As String
    Private grAccess As aaGRAccessApp.GRAccessApp
    Private myGalaxy As aaGRAccessApp.IGalaxy


    Public Sub New()
        ' set up all the initialization of this GRAccess client app
        grAccess = New aaGRAccessApp.GRAccessApp
        loggedIn = False
        showLogin = True
    End Sub

    Public Function getGalaxies(ByVal NodeName As String) As Collection

        ' this function just returns a collection of all the galaxy names on this node

        Dim galaxies As aaGRAccessApp.IGalaxies
        Dim galaxy As aaGRAccessApp.IGalaxy
        Dim galaxyList As New Collection

        galaxyNode = NodeName

        Try
            galaxies = grAccess.QueryGalaxies(galaxyNode)
            resultStatus = grAccess.CommandResult.Successful
            If resultStatus And (galaxies IsNot Nothing) And galaxies.count > 0 Then
                For Each galaxy In galaxies
                    galaxyList.Add(galaxy.Name)
                Next
            Else
                Throw New ApplicationException("No Galaxies detected on this node")
            End If

        Catch e As Exception
            MessageBox.Show("Error occurred: " & e.Message)
        End Try
        Return galaxyList
    End Function

    Public Function setGalaxy(ByVal galaxyName)
        showLogin = True
        loggedIn = False
        Try
            If (galaxyNode IsNot Nothing) And (galaxyName IsNot Nothing) Then
                myGalaxy = grAccess.QueryGalaxies(galaxyNode)(galaxyName)



                ' TODO: Would like to dynamically show or hide the login dialog box based on security, 
                ' but the API call inside of getAuthType() is not working as expected.

                authMode = getAuthType()
                'If authMode = "None" Then
                '    ShowLogin = False
                '    login("", "")
                'Else
                '    ShowLogin = True
                '    LoggedIn = False
                'End If
            Else
                Throw New ApplicationException("No Node or Galaxy Selected")
            End If
        Catch e As Exception
            MessageBox.Show("Error occurred: " & e.Message)
        End Try

        Return 0
    End Function

    Private Function getAuthType() As String
        Dim mode As String
        'Dim galaxySecurity As aaGRAccessApp.IGalaxySecurity
        Dim authMode As New aaGRAccessApp.EAUTHMODE
        mode = ""

        ' TODO: find out why get a "object not set to reference" exception when this code is run. 
        ' The GetReadOnlySecurity method doesn't seem to work correctly.

        'Try
        '    galaxy = grAccess.QueryGalaxies(nodeName)(galaxyName)
        '    galaxySecurity = galaxy.GetReadOnlySecurity
        '    authMode = galaxySecurity.AuthenticationMode
        '    Select Case authMode
        '        Case aaGRAccessApp.EAUTHMODE.eNone
        '            mode = "None"
        '        Case aaGRAccessApp.EAUTHMODE.eGalaxyOnly
        '            mode = "Galaxy Only"
        '        Case aaGRAccessApp.EAUTHMODE.eOSUserBased
        '            mode = "OS User"
        '        Case aaGRAccessApp.EAUTHMODE.eOSGroupBased
        '            mode = "OS Group"
        '    End Select
        'Catch e As Exception
        '    MessageBox.Show("Error occurred: " & e.Message)
        'End Try

        mode = "Unknown. Click Login with blank User/Pass if no security."

        Return mode
    End Function

    Public Function getTemplates(Optional ByVal HideBaseTemplates As Boolean = False) As Collection
        Dim templateList As New Collection
        Dim gTemplates As aaGRAccessApp.IgObjects
        Dim gTemplate As aaGRAccessApp.IgObject

        Dim BaseTemplates() As String = New String() {"$Boolean", "$Integer", "$Double", "$Float", "$String", "$FieldReference", "$UserDefined", "$AnalogDevice", "$AppEngine", "$Area", "$DDESuiteLinkClient", "$DiscreteDevice", "$InTouchProxy", "$InTouchViewApp", "$OPCClient", "$RedundantDIObject", "$Sequencer", "$SQLData", "$Switch", "$ViewEngine", "$WinPlatform"}

        Try
            If loggedIn Then
                gTemplates = myGalaxy.QueryObjects(aaGRAccessApp.EgObjectIsTemplateOrInstance.gObjectIsTemplate,
                                                aaGRAccessApp.EConditionType.checkoutStatusIs,
                                                aaGRAccessApp.ECheckoutStatus.notCheckedOut,
                                                aaGRAccessApp.EMatch.MatchCondition)
                resultStatus = myGalaxy.CommandResult.Successful
                If resultStatus And (gTemplates IsNot Nothing) And (gTemplates.count > 0) Then
                    For Each gTemplate In gTemplates
                        If Not (HideBaseTemplates And BaseTemplates.Contains(gTemplate.Tagname)) Then
                            templateList.Add(gTemplate.Tagname)
                        End If
                    Next
                Else
                    Throw New ApplicationException("No templates found")
                End If
            End If

        Catch e As Exception
            MessageBox.Show("Error occurred: " & e.Message)
        End Try

        Return templateList
    End Function

    Public Function login(ByVal user, ByVal password) As Integer
        Try
            myGalaxy.Login(user, password)
            If myGalaxy.CommandResult.Successful Then
                loggedIn = True
                Return 0
            Else
                loggedIn = False
                Return -1
            End If
        Catch e As Exception
            loggedIn = False
            MessageBox.Show("Error occurred: " & e.Message)
            Return -2
        End Try

    End Function

    Public Function getTemplateData(ByVal TemplateName As String) As aaTemplateData
        Dim templateList(1) As String
        Dim gTemplates As aaGRAccessApp.IgObjects
        Dim gTemplate As aaGRAccessApp.ITemplate
        Dim scriptList As New Collection
        Dim scriptData As aaScriptData
        Dim templateData As aaTemplateData

        templateData = New aaTemplateData()

        Try
            If loggedIn Then
                ' Convert the individual template name to an array. 
                ' The QueryObjectsByName function can take a bunch of template names, but I want this function to just get one template's data
                templateList(0) = TemplateName
                ' query the galaxy for this template's data
                gTemplates = myGalaxy.QueryObjectsByName(aaGRAccessApp.EgObjectIsTemplateOrInstance.gObjectIsTemplate, templateList)
                resultStatus = myGalaxy.CommandResult.Successful
                If resultStatus And (gTemplates IsNot Nothing) And (gTemplates.count > 0) Then
                    For Each gTemplate In gTemplates
                        ' in reality we will only have one template, so this loop will only happen once

                        ' instantiate our new template data object
                        templateData = New aaTemplateData(gTemplate.Tagname)

                        ' get all of the Configurable Attributes, which is where the script data lives
                        Dim gAttributes = gTemplate.ConfigurableAttributes

                        ' get all the script names
                        For Each gAttribute As aaGRAccessApp.IAttribute In gAttributes
                            If InStr(gAttribute.Name, ".ExecuteText") > 0 Then
                                scriptList.Add(Replace(gAttribute.Name, ".ExecuteText", ""))
                            End If
                        Next

                        ' loop through each script name and get all of the attributes for that script
                        For Each script In scriptList

                            ' build a script object with the attributes
                            scriptData = New aaScriptData(script, _
                                                            gAttributes.Item(script + ".ScriptExecutionGroup").value.GetString, _
                                                            gAttributes.Item(script + ".ScriptOrder").value.GetString, _
                                                            gAttributes.Item(script + ".OffScanText").value.GetString, _
                                                            gAttributes.Item(script + ".OnScanText").value.GetString, _
                                                            gAttributes.Item(script + ".ShutdownText").value.GetString, _
                                                            gAttributes.Item(script + ".StartupText").value.GetString, _
                                                            gAttributes.Item(script + ".Expression").value.GetString, _
                                                            gAttributes.Item(script + ".TriggerType").value.GetString, _
                                                            gAttributes.Item(script + ".TriggerPeriod").value.GetString, _
                                                            gAttributes.Item(script + ".TriggerOnQualityChange").value.GetBoolean, _
                                                            gAttributes.Item(script + ".ExecuteTimeout.Limit").value.GetInteger, _
                                                            gAttributes.Item(script + ".RunsAsync").value.GetString, _
                                                            gAttributes.Item(script + ".ExecuteText").value.GetString)

                            ' pop it into our template data object
                            templateData.AddScript(scriptData)
                        Next

                    Next

                End If

            End If

        Catch e As Exception
            MessageBox.Show("Error occurred: " & e.Message)
        End Try

        Return templateData

    End Function

End Class