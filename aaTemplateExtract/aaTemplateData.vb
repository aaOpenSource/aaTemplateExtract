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

Imports System.Xml.Serialization

<Serializable()> _
Public Class aaTemplateData
    ' Author: Eliot Landrum <elandrum@stonetek.com>
    ' Description: This class stores ArchestrA Galaxy template data in an XML serializable format

    Public name As String ' the template's name
    Public scripts As New Collection()
    Public attributes As New Collection()

    Public Sub New()

    End Sub

    Public Sub New(ByVal name As String)
        Me.name = Name
    End Sub

    Public Sub AddScript(ByVal ScriptData As aaScriptData)
        scripts.Add(ScriptData, ScriptData.ScriptName)
    End Sub

    Public Sub AddAttribute(ByVal AttributeData As aaAttributeData)
        attributes.Add(AttributeData, AttributeData.AttributeName)
    End Sub

End Class

<Serializable()> _
Public Class aaScriptData
    ' Author: Eliot Landrum <elandrum@stonetek.com>
    ' Description: This class stores an individual script's data in an XML serializable format

    Public ScriptName As String
    Public ScriptExecutionGroup As String
    Public ScriptOrder As Integer
    Public OffScanText As String
    Public OnScanText As String
    Public ShutdownText As String
    Public StartupText As String
    Public Expression As String
    Public TriggerType As String
    Public TriggerPeriod As String
    Public TriggerOnQualityChange As Boolean
    Public ExecuteTimeoutLimit As Integer
    Public RunsAsync As Boolean
    Public ExecuteText As String

    Public Sub New()

    End Sub

    Public Sub New(ByVal ScriptName As String, _
        ByVal ScriptExecutionGroup As String, _
        ByVal ScriptOrder As Integer, _
        ByVal OffScanText As String, _
        ByVal OnScanText As String, _
        ByVal ShutdownText As String, _
        ByVal StartupText As String, _
        ByVal Expression As String, _
        ByVal TriggerType As String, _
        ByVal TriggerPeriod As String, _
        ByVal TriggerOnQualityChange As Boolean, _
        ByVal ExecuteTimeoutLimit As Integer, _
        ByVal RunsAsync As Boolean, _
        ByVal ExecuteText As String)

        Me.ScriptName = ScriptName
        Me.ScriptExecutionGroup = ScriptExecutionGroup
        Me.ScriptOrder = ScriptOrder
        Me.OffScanText = OffScanText
        Me.OnScanText = OnScanText
        Me.ShutdownText = ShutdownText
        Me.StartupText = StartupText
        Me.Expression = Expression
        Me.TriggerType = TriggerType
        Me.TriggerPeriod = TriggerPeriod
        Me.TriggerOnQualityChange = TriggerOnQualityChange
        Me.ExecuteTimeoutLimit = ExecuteTimeoutLimit
        Me.RunsAsync = RunsAsync
        Me.ExecuteText = ExecuteText

    End Sub

End Class

Public Class aaAttributeData
    ' Author: Eliot Landrum <elandrum@stonetek.com>
    ' Description: This class stores an individual attribute's parameters in an XML serializable format

    Public AttributeName As String

    Public Sub New()

    End Sub

    Public Sub New(ByVal AttributeName As String)
        Me.AttributeName = AttributeName
    End Sub

End Class
