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
Public Class aaTemplate
    ' Author: Eliot Landrum <elandrum@stonetek.com>
    ' Description: This class stores ArchestrA Galaxy template data in an XML serializable format

    <XmlAttribute("name")> Public Name As String
    Public Scripts As New Collection()
    Public FieldAttributesDiscrete As New Collection()
    Public FieldAttributesAnalog As New Collection()

    Public Sub New()

    End Sub

    Public Sub New(ByVal Name As String, ByVal Scripts As Collection, ByVal FieldAttributesDiscrete As Collection, ByVal FieldAttributesAnalog As Collection)
        Me.Name = Name
        Me.Scripts = Scripts
        Me.FieldAttributesDiscrete = FieldAttributesDiscrete
        Me.FieldAttributesAnalog = FieldAttributesAnalog
    End Sub

End Class

<Serializable()> _
Public Class aaScript
    ' Author: Eliot Landrum <elandrum@stonetek.com>
    ' Description: This class stores an individual script's data in an XML serializable format

    <XmlAttribute("name")> Public Name As String
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

    Public Sub New(ByVal Name As String, _
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

        Me.Name = Name
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

<Serializable()> _
Public Class aaFieldAttributeDiscrete
    ' Author: Eliot Landrum <elandrum@stonetek.com>
    ' Description: This class stores an individual attribute's parameters in an XML serializable format

    <XmlAttribute("name")> Public Name As String

    Public AttributeType As String
    Public AccessMode As String
    Public Category As String
    Public Buffered As Boolean
    Public Description As String
    Public InitialState As Boolean
    Public EventOnChange As Boolean
    Public InvertValue As Boolean
    Public IO As aaAttrIO
    Public StateLabels As aaAttrStateLabels
    Public History As aaAttrHistoryDiscrete
    Public StateAlarm As aaAttrStateAlarm
    Public BadValueAlarm As aaAttrBadValueAlarm
    Public Statistics As aaAttrStatistics

    Public Sub New()

    End Sub

    Public Sub New(ByVal Name As String, ByVal AccessMode As String, _
                   ByVal Category As String, ByVal Buffered As Boolean, _
                   ByVal Description As String, ByVal InitialState As Boolean, _
                   ByVal EventOnChange As Boolean, ByVal InvertValue As Boolean, _
                   ByVal IO As aaAttrIO, ByVal StateLabels As aaAttrStateLabels, _
                   ByVal History As aaAttrHistoryDiscrete, ByVal StateAlarm As aaAttrStateAlarm, _
                   ByVal BadValueAlarm As aaAttrBadValueAlarm, ByVal Statistics As aaAttrStatistics)

        Me.Name = Name
        Me.AttributeType = "Boolean"
        Me.AccessMode = AccessMode
        Me.Category = Category
        Me.Description = Description
        Me.InitialState = InitialState
        Me.EventOnChange = EventOnChange
        Me.InvertValue = InvertValue
        Me.IO = IO
        Me.StateLabels = StateLabels
        Me.History = History
        Me.StateAlarm = StateAlarm
        Me.BadValueAlarm = BadValueAlarm
        Me.Statistics = Statistics
    End Sub

End Class

<Serializable()> _
Public Class aaAttrIO
    Public InputSource As String
    Public OutputDiffers As Boolean
    Public OutputDest As String
    Public Sub New()
    End Sub
    Public Sub New(ByVal InputSource As String, ByVal OutputDiffers As Boolean, ByVal OutputDest As String)
        Me.InputSource = InputSource
        Me.OutputDiffers = OutputDiffers
        Me.OutputDest = OutputDest
    End Sub
End Class

<Serializable()> _
Public Class aaAttrStateLabels
    <XmlAttribute("enabled")> Public Enabled As Boolean
    Public OffMsg As String
    Public OnMsg As String
    Public Sub New()
    End Sub
    Public Sub New(ByVal Enabled As Boolean, ByVal OffMsg As String, ByVal OnMsg As String)
        Me.Enabled = Enabled
        Me.OffMsg = OffMsg
        Me.OnMsg = OnMsg
    End Sub
End Class

<Serializable()> _
Public Class aaAttrHistoryDiscrete
    <XmlAttribute("enabled")> Public Enabled As Boolean
    Public StoragePeriod As Integer
    Public Sub New()
    End Sub
    Public Sub New(ByVal Enabled As Boolean, ByVal StoragePeriod As Integer)
        Me.Enabled = Enabled
        Me.StoragePeriod = StoragePeriod
    End Sub
End Class

<Serializable()> _
Public Class aaAttrStateAlarm
    <XmlAttribute("enabled")> Public Enabled As Boolean
    Public ActiveAlarmState As Boolean
    Public Priority As Integer
    Public AlarmMsg As String
    Public Category As String
    Public Deadband As String
    Public Sub New()
    End Sub
    Public Sub New(ByVal Enabled As Boolean, ByVal ActiveAlarmState As Boolean, _
                   ByVal Priority As Integer, ByVal AlarmMsg As String, _
                   ByVal Category As String, ByVal Deadband As String)
        Me.Enabled = Enabled
        Me.ActiveAlarmState = ActiveAlarmState
        Me.Priority = Priority
        Me.AlarmMsg = AlarmMsg
        Me.Category = Category
        Me.Deadband = Deadband
    End Sub
End Class

<Serializable()> _
Public Class aaAttrBadValueAlarm
    <XmlAttribute("enabled")> Public Enabled As Boolean
    Public Priority As Integer
    Public AlarmMsg As String
    Public Sub New()
    End Sub
    Public Sub New(ByVal Enabled As Boolean, ByVal Priority As Integer, ByVal AlarmMsg As String)
        Me.Enabled = Enabled
        Me.Priority = Priority
        Me.AlarmMsg = AlarmMsg
    End Sub
End Class

<Serializable()> _
Public Class aaAttrStatistics
    <XmlAttribute("enabled")> Public Enabled As Boolean
    Public AutoResetOnBadInput As Boolean
    Public SampleSize As Integer ' only used on analog
    Public Sub New()
    End Sub
    Public Sub New(ByVal Enabled As Boolean, ByVal AutoResetOnBadInput As Boolean, ByVal SampleSize As Integer)
        Me.Enabled = Enabled
        Me.AutoResetOnBadInput = AutoResetOnBadInput
        Me.SampleSize = SampleSize
    End Sub
End Class