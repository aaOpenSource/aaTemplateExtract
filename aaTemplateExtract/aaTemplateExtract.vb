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

''' <summary>
''' The aaTemplateExtract class interfaces with GRAccess to connect, collect, and parse data from the Galaxy.
''' All the hard work is in this class, but aaTemplateData contains the actual data structure.
''' </summary>
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

    ''' <summary>
    ''' Initializes and sets up our GRAccess client app.
    ''' </summary>
    Public Sub New()
        grAccess = New aaGRAccessApp.GRAccessApp
        loggedIn = False
        showLogin = True
    End Sub

    ''' <summary>
    ''' Queries for all the galaxy names on this node.
    ''' </summary>
    ''' <param name="NodeName">The node name as a string (e.g. "localhost").</param>
    ''' <returns>A collection of galaxy names as strings.</returns>
    Public Function getGalaxies(ByVal NodeName As String) As Collection
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

    ''' <summary>
    ''' Given a Galaxy Name, sets our Client to that Galaxy.
    ''' </summary>
    ''' <param name="galaxyName">A string that is the Galaxy name.</param>
    ''' <returns>Nothing</returns>
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

    ''' <summary>
    ''' Determines what authentication type is required for this Galaxy.
    ''' </summary>
    ''' <returns>The authentication mode.</returns>
    ''' <remarks>Not currently working.</remarks>
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

    ''' <summary>
    ''' Tries to login to the Galaxy.
    ''' </summary>
    ''' <param name="user">User name, include Domain (e.g. "Domain\user") if authenticating against a domain.</param>
    ''' <param name="password">Password</param>
    ''' <returns>Status of login attempt.</returns>
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

    ''' <summary>
    ''' Discovers all of the templates in this Galaxy.
    ''' </summary>
    ''' <param name="HideBaseTemplates">If set, will not return the base templates that are in every Galaxy.</param>
    ''' <returns>A collection of strings with the template names listed.</returns>
    ''' <remarks>Does not return instances or checked in templates.</remarks>
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

    ''' <summary>
    ''' This is the master function that is initiated for each template. Sub-functions will gather all of the attributes and scripts.
    ''' </summary>
    ''' <param name="TemplateName">The template name that is desired to get data from.</param>
    ''' <returns>An aaTemplate class of all of the template data (scripts, UDAs, field attributes).</returns>
    Public Function getTemplateData(ByVal TemplateName As String) As aaTemplate
        Dim templateList(1) As String
        Dim gTemplates As aaGRAccessApp.IgObjects
        Dim gTemplate As aaGRAccessApp.ITemplate
        Dim templateData As New aaTemplate()
        Dim scriptData As New Collection()

        Try
            If loggedIn Then
                ' Convert the individual template name to an array. 
                ' The QueryObjectsByName function can take a bunch of template names, but I want this function to just get one template's data
                templateList(0) = TemplateName
                ' query the galaxy for this template's data
                gTemplates = myGalaxy.QueryObjectsByName(aaGRAccessApp.EgObjectIsTemplateOrInstance.gObjectIsTemplate, templateList)
                resultStatus = myGalaxy.CommandResult.Successful
                If resultStatus And (gTemplates IsNot Nothing) And (gTemplates.count > 0) Then
                    gTemplate = gTemplates(1)

                    ' get all of the Configurable Attributes, which is where the script data lives
                    Dim gAttributes = gTemplate.ConfigurableAttributes

                    ' instantiate our new template data object with all of our collected information
                    templateData = New aaTemplate(gTemplate.Tagname, _
                                                  GetAttrInteger("ConfigVersion", gAttributes), _
                                                  getScripts(gAttributes),
                                                  GetFieldAttributesDiscrete(gAttributes), _
                                                  GetFieldAttributesAnalog(gAttributes))
                End If
            Else
                Throw New ApplicationException("Not Logged In")
            End If

        Catch e As Exception
            MessageBox.Show("Error occurred: " & e.Message)
        End Try

        Return templateData

    End Function

    ''' <summary>
    ''' Gets all of the scripts for the given template and puts it into a nice collection of scripts.
    ''' </summary>
    ''' <param name="gAttributes">All of the Configurable Attributes that were found for this template.</param>
    ''' <returns>A collection of all of the scripts using the aaScript class.</returns>
    Private Function getScripts(gAttributes As aaGRAccess.IAttributes) As Collection
        Dim scriptList As New Collection
        Dim scriptData As aaScript
        Dim templateScripts As New Collection()

        ' get all the script names
        For Each gAttribute As aaGRAccessApp.IAttribute In gAttributes
            If InStr(gAttribute.Name, ".ExecuteText") > 0 Then
                scriptList.Add(Replace(gAttribute.Name, ".ExecuteText", ""))
            End If
        Next

        ' loop through each script name and get all of the attributes for that script
        For Each script In scriptList

            ' build a script object with the attributes
            scriptData = New aaScript(script, _
                GetAttrString("Tagname", gAttributes), _
                GetAttrInteger("ConfigVersion", gAttributes), _
                GetAttrString(script + ".ScriptExecutionGroup", gAttributes), _
                GetAttrString(script + ".ScriptOrder", gAttributes), _
                GetAttrString(script + ".Aliases", gAttributes), _
                GetAttrString(script + ".AliasReferences", gAttributes), _
                GetAttrString(script + ".DeclarationsText", gAttributes), _
                GetAttrString(script + ".OffScanText", gAttributes), _
                GetAttrString(script + ".OnScanText", gAttributes), _
                GetAttrString(script + ".ShutdownText", gAttributes), _
                GetAttrString(script + ".StartupText", gAttributes), _
                GetAttrString(script + ".Expression", gAttributes), _
                GetAttrString(script + ".TriggerType", gAttributes), _
                GetAttrString(script + ".TriggerPeriod", gAttributes), _
                GetAttrBoolean(script + ".TriggerOnQualityChange", gAttributes), _
                GetAttrInteger(script + ".ExecuteTimeout.Limit", gAttributes), _
                GetAttrString(script + ".RunsAsync", gAttributes), _
                GetAttrString(script + ".ExecuteText", gAttributes))

            ' pop it on to a collection of scripts for this template
            templateScripts.Add(scriptData, script)
        Next

        Return templateScripts
    End Function

    ''' <summary>
    ''' Gets all of the Discrete Field Attributes for a given template. 
    ''' </summary>
    ''' <param name="gAttributes">All of the Configurable Attributes that were found for this template.</param>
    ''' <returns>A collection of all of the Discrete Field Attributes using the aaFieldAttributeDiscrete class.</returns>
    ''' <remarks></remarks>
    Private Function GetFieldAttributesDiscrete(gAttributes As aaGRAccess.IAttributes) As Collection
        Dim FieldAttributes As New Collection()

        ' A list of the Field Attributes are stored in an XML fragment in the UserAttrData attribute
        Dim UserAttrData As XElement = XElement.Parse(gAttributes.Item("UserAttrData").value.GetString)

        Dim attrList = UserAttrData.<DiscreteAttr>.Attributes("Name")

        For Each attr In attrList
            Dim attrName = attr.Value

            ' These are all groupings on the Field Attributes editing screen. 
            ' They are not necessarily grouped nicely in the Attributes list. 
            ' I prefer that it be organized like the screen for easy comparing.

            ' The basic input/output attributes
            Dim IO = New aaAttrIO(GetAttrString(attrName + ".Input.InputSource", gAttributes), _
                GetAttrBoolean(attrName + ".Output.DiffAddr", gAttributes), _
                GetAttrString(attrName + ".Output.OutputDest", gAttributes))

            ' State Labels, can be disabled
            Dim StateLabels = New aaAttrStateLabels(GetAttrBoolean(attrName + ".UseOffOnMsg", gAttributes), _
                GetAttrString(attrName + ".OffMsg", gAttributes), _
                GetAttrString(attrName + ".OnMsg", gAttributes))

            ' Historization, can be disabled
            Dim History = New aaAttrHistoryDiscrete(GetAttrBoolean(attrName + ".Historized", gAttributes), _
                GetAttrInteger(attrName + ".ForceStoragePeriod", gAttributes))

            ' State Alarms, can be disabled
            Dim StateAlarm = New aaAttrStateAlarm(GetAttrBoolean(attrName + ".Alarmed", gAttributes), _
                GetAttrBoolean(attrName + ".ActiveAlarmState", gAttributes), _
                GetAttrInteger(attrName + ".Priority", gAttributes), _
                GetAttrString(attrName + ".DescAttrName", gAttributes), _
                GetAttrString(attrName + ".Category", gAttributes), _
                GetAttrString(attrName + ".TimeDeadband", gAttributes))

            ' Bad Value Alarm, can be disabled
            Dim BadValueAlarm = New aaAttrBadValueAlarm(GetAttrBoolean(attrName + ".Bad.Alarmed", gAttributes), _
                GetAttrInteger(attrName + ".Bad.Priority", gAttributes), _
                GetAttrInteger(attrName + ".Bad.DescAttrName", gAttributes))

            ' Statistics, can be disabled
            Dim Statistics = New aaAttrStatistics(GetAttrBoolean(attrName + ".HasStatistics", gAttributes), _
                GetAttrBoolean(attrName + ".Stats.AutoResetOnBadInput", gAttributes), 0)

            ' Now, put all of the info together into one data set for this attribute
            Dim DiscreteAttrData = New aaFieldAttributeDiscrete(attrName, _
                GetAttrString("Tagname", gAttributes), _
                GetAttrInteger("ConfigVersion", gAttributes), _
                GetAttrString(attrName + ".AccessMode", gAttributes), _
                GetAttrString(attrName + "._HasBuffer", gAttributes), _
                GetAttrString(attrName + ".Desc", gAttributes), _
                GetAttrBoolean(attrName, gAttributes), _
                GetAttrBoolean(attrName + ".LogDataChangeEvent", gAttributes), _
                GetAttrBoolean(attrName + ".InvertValue", gAttributes), _
                IO, StateLabels, History, StateAlarm, BadValueAlarm, Statistics)


            ' Finally, add it to a (growing) collection of field attributes
            FieldAttributes.Add(DiscreteAttrData)
        Next

        Return FieldAttributes
    End Function

    ''' <summary>
    ''' Gets all of the Analog Field Attributes for a given template. 
    ''' </summary>
    ''' <param name="gAttributes">All of the Configurable Attributes that were found for this template.</param>
    ''' <returns>A collection of all of the Analog Field Attributes using the aaFieldAttributeAnalog class.</returns>
    ''' <remarks></remarks>
    Private Function GetFieldAttributesAnalog(gAttributes As aaGRAccess.IAttributes) As Collection
        Dim FieldAttributes As New Collection()

        ' A list of the Field Attributes are stored in an XML fragment in the UserAttrData attribute
        Dim UserAttrData As XElement = XElement.Parse(gAttributes.Item("UserAttrData").value.GetString)

        Dim attrList = UserAttrData.<AnalogAttr>.Attributes("Name")

        For Each attr In attrList
            Dim attrName = attr.Value

            ' These are all groupings on the Field Attributes editing screen. 
            ' They are not necessarily grouped nicely in the Attributes list. 
            ' I prefer that it be organized like the screen for easy comparing.

            ' Input/Output
            Dim IO = New aaAttrIO(GetAttrString(attrName + ".Input.InputSource", gAttributes), _
                GetAttrBoolean(attrName + ".Output.DiffAddr", gAttributes), _
                GetAttrString(attrName + ".Output.OutputDest", gAttributes))

            ' IO scaling, can be disabled
            Dim IOScaling = New aaAttrIOScaling(GetAttrBoolean(attrName + ".Scaled", gAttributes), _
                GetAttrFloat(attrName + ".RawMin", gAttributes), _
                GetAttrFloat(attrName + ".RawMax", gAttributes), _
                GetAttrFloat(attrName + ".EngUnitsMin", gAttributes), _
                GetAttrFloat(attrName + ".EngUnitsMax", gAttributes), _
                GetAttrFloat(attrName + ".EngUnitsRangeMin", gAttributes), _
                GetAttrFloat(attrName + ".EngUnitsRangeMax", gAttributes), _
                GetAttrString(attrName + ".ConversionMode", gAttributes), _
                GetAttrString(attrName + ".ClampEnabled", gAttributes))

            ' Historization, can be disabled
            Dim History = New aaAttrHistoryAnalog(GetAttrBoolean(attrName + ".Historized", gAttributes), _
                GetAttrInteger(attrName + ".ForceStoragePeriod", gAttributes), _
                GetAttrInteger(attrName + ".ValueDeadband", gAttributes), _
                GetAttrString(attrName + ".InterpolationType", gAttributes), _
                GetAttrFloat(attrName + ".RolloverValue", gAttributes), _
                GetAttrFloat(attrName + ".TrendHi", gAttributes), _
                GetAttrFloat(attrName + ".Trendlo", gAttributes), _
                GetAttrBoolean(attrName + ".EnableSwingingDoor", gAttributes), _
                GetAttrFloat(attrName + ".RateDeadBand", gAttributes))

            ' Level Alarm, can be disabled
            Dim LevelAlarm = New aaAttrLevelAlarm(GetAttrBoolean(attrName + ".LevelAlarmed", gAttributes), _
                New aaAttrAnalogLimit(GetAttrBoolean(attrName + ".HiHi.Alarmed", gAttributes), _
                    GetAttrFloat(attrName + ".HiHi.Limit", gAttributes), _
                    GetAttrInteger(attrName + ".HiHi.Priority", gAttributes), _
                    GetAttrString(attrName + ".HiHi.DescAttrName", gAttributes)), _
                New aaAttrAnalogLimit(GetAttrBoolean(attrName + ".Hi.Alarmed", gAttributes), _
                    GetAttrFloat(attrName + ".Hi.Limit", gAttributes), _
                    GetAttrInteger(attrName + ".Hi.Priority", gAttributes), _
                    GetAttrString(attrName + ".Hi.DescAttrName", gAttributes)), _
                New aaAttrAnalogLimit(GetAttrBoolean(attrName + ".Lo.Alarmed", gAttributes), _
                    GetAttrFloat(attrName + ".Lo.Limit", gAttributes), _
                    GetAttrInteger(attrName + ".Lo.Priority", gAttributes), _
                    GetAttrString(attrName + ".Lo.DescAttrName", gAttributes)), _
                New aaAttrAnalogLimit(GetAttrBoolean(attrName + ".LoLo.Alarmed", gAttributes), _
                    GetAttrFloat(attrName + ".LoLo.Limit", gAttributes), _
                    GetAttrInteger(attrName + ".LoLo.Priority", gAttributes), _
                    GetAttrString(attrName + ".LoLo.DescAttrName", gAttributes)), _
                GetAttrFloat(attrName + ".ValueDeadband", gAttributes), _
                GetAttrFloat(attrName + ".TimeDeadband", gAttributes))

            ' Rate of Change Alarm, can be disabled
            Dim ROCAlarm = New aaAttrROCAlarm(GetAttrBoolean(attrName + ".ROCAlarmed", gAttributes), _
                New aaAttrAnalogLimit(GetAttrBoolean(attrName + ".Roc.IncreasingHi.Alarmed", gAttributes), _
                    GetAttrFloat(attrName + ".Roc.IncreasingHi.Limit", gAttributes), _
                    GetAttrInteger(attrName + ".Roc.IncreasingHi.Priority", gAttributes), _
                    GetAttrString(attrName + ".Roc.IncreasingHi.DescAttrName", gAttributes)), _
                New aaAttrAnalogLimit(GetAttrBoolean(attrName + ".Roc.DecreasingHi.Alarmed", gAttributes), _
                    GetAttrFloat(attrName + ".Roc.DecreasingHi.Limit", gAttributes), _
                    GetAttrInteger(attrName + ".Roc.DecreasingHi.Priority", gAttributes), _
                    GetAttrString(attrName + ".Roc.DecreasingHi.DescAttrName", gAttributes)), _
                    GetAttrString(attrName + ".Roc.Rate.Units", gAttributes), _
                    GetAttrInteger(attrName + ".Roc.EvalPeriod", gAttributes))

            ' Deviation Alarm, can be disabled
            Dim DeviationAlarm = New aaAttrDeviationAlarm(GetAttrBoolean(attrName + ".DeviationAlarmed", gAttributes), _
                New aaAttrAnalogLimit(GetAttrBoolean(attrName + ".Dev.Major.Alarmed", gAttributes), _
                    GetAttrFloat(attrName + ".Dev.Major.Tolerance", gAttributes), _
                    GetAttrInteger(attrName + ".Dev.Major.Priority", gAttributes), _
                    GetAttrString(attrName + ".Dev.Major.DescAttrName", gAttributes)), _
                New aaAttrAnalogLimit(GetAttrBoolean(attrName + ".Dev.Minor.Alarmed", gAttributes), _
                     GetAttrFloat(attrName + ".Dev.Minor.Tolerance", gAttributes), _
                     GetAttrInteger(attrName + ".Dev.Minor.Priority", gAttributes), _
                     GetAttrString(attrName + ".Dev.Minor.DescAttrName", gAttributes)),
                GetAttrFloat(attrName + ".Dev.Target", gAttributes), _
                GetAttrFloat(attrName + ".Dev.Deadband", gAttributes), _
                GetAttrString(attrName + ".Dev.SettlingPeriod", gAttributes))

            ' Bad Value Alarm, can be disabled
            Dim BadValueAlarm = New aaAttrBadValueAlarm(GetAttrBoolean(attrName + ".Bad.Alarmed", gAttributes), _
                GetAttrInteger(attrName + ".Bad.Priority", gAttributes), _
                GetAttrString(attrName + ".Bad.DescAttrName", gAttributes))

            ' Statistics, can be disabled
            Dim Statistics = New aaAttrStatistics(GetAttrBoolean(attrName + ".HasStatistics", gAttributes), _
                GetAttrBoolean(attrName + ".Stats.AutoResetOnBadInput", gAttributes), _
                GetAttrInteger(attrName + ".Stats.SampleSize", gAttributes))

            ' Now, put all of the info together into one data set for this attribute
            Dim AnalogAttrData = New aaFieldAttributeAnalog(attrName,
                GetAttrString("Tagname", gAttributes), _
                GetAttrInteger("ConfigVersion", gAttributes), _
                GetAttrString(attrName + ".AnalogType", gAttributes), _
                GetAttrString(attrName + ".AccessMode", gAttributes), _
                GetAttrBoolean(attrName + "._HasBuffer", gAttributes), _
                GetAttrString(attrName + ".Desc", gAttributes), _
                GetAttrFloat(attrName, gAttributes), _
                GetAttrBoolean(attrName + ".LogDataChangeEvent", gAttributes), _
                IO, IOScaling, History, LevelAlarm, ROCAlarm, DeviationAlarm, _
                BadValueAlarm, Statistics)

            ' Finally, add it to a (growing) collection of field attributes
            FieldAttributes.Add(AnalogAttrData)
        Next

        Return FieldAttributes
    End Function

    ''' <summary>
    ''' Since the attribute may not exist, need to provide a safe, concise way to get it without crashing the program.
    ''' </summary>
    ''' <param name="AttributeName">The Attribute Name that exists in the template.</param>
    ''' <param name="gAttributes">All of the Configurable Attributes that were found for this template.</param>
    ''' <returns>Either the value of the Attribute or a blank string.</returns>
    ''' <remarks></remarks>
    Private Function GetAttrString(ByVal AttributeName As String, gAttributes As aaGRAccess.IAttributes) As String
        ' 
        Try
            Return gAttributes.Item(AttributeName).value.GetString()
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Since the attribute may not exist, need to provide a safe, concise way to get it without crashing the program.
    ''' </summary>
    ''' <param name="AttributeName">The Attribute Name that exists in the template.</param>
    ''' <param name="gAttributes">All of the Configurable Attributes that were found for this template.</param>
    ''' <returns>Either the value of the Attribute or a boolean false.</returns>
    ''' <remarks></remarks>
    Private Function GetAttrBoolean(ByVal AttributeName As String, gAttributes As aaGRAccess.IAttributes) As Boolean
        ' Since the attribute may not exist, need to provide a safe, concise way to get it without crashing the program
        Try
            Return gAttributes.Item(AttributeName).value.GetBoolean()
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Since the attribute may not exist, need to provide a safe, concise way to get it without crashing the program.
    ''' </summary>
    ''' <param name="AttributeName">The Attribute Name that exists in the template.</param>
    ''' <param name="gAttributes">All of the Configurable Attributes that were found for this template.</param>
    ''' <returns>Either the value of the Attribute or a zero.</returns>
    ''' <remarks></remarks>
    Private Function GetAttrInteger(ByVal AttributeName As String, gAttributes As aaGRAccess.IAttributes) As Integer
        ' Since the attribute may not exist, need to provide a safe, concise way to get it without crashing the program
        Try
            Return gAttributes.Item(AttributeName).value.GetInteger()
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function GetAttrFloat(ByVal AttributeName As String, gAttributes As aaGRAccess.IAttributes) As Double
        ' Since the attribute may not exist, need to provide a safe, concise way to get it without crashing the program
        Try
            Return gAttributes.Item(AttributeName).value.GetFloat()
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Determines if the given attribute is locked or not.
    ''' </summary>
    ''' <param name="AttributeName">The Attribute Name that exists in the template.</param>
    ''' <param name="gAttributes">All of the Configurable Attributes that were found for this template.</param>
    ''' <returns>Either the attribute locked status or a false.</returns>
    ''' <remarks></remarks>
    Private Function GetLockedStatus(ByVal AttributeName As String, gAttributes As aaGRAccess.IAttributes) As Boolean
        Try
            Return gAttributes.Item(AttributeName).Locked
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class