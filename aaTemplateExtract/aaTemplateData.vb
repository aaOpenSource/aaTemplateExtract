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

''' <summary>
''' Stores ArchestrA Galaxy template data in an XML serializable format.
''' </summary>
Public Module aaTemplateData

    ''' <summary>
    ''' The current XML schema version. This is useful in exports to determine 
    ''' if data may be missing from an older version export.
    ''' </summary>
    ''' <history>
    ''' 1.0 - Scripts only
    ''' 1.1 - Scripts and Attributes to two separate directories; added Discrete Field Attributes
    ''' 1.2 - Added version numbering to schema
    '''     - Added Analog Field Attributes
    '''     - Added script Declarations and Aliases
    '''     - Added template name and template revision number to each top level XML
    ''' </history>
    Public SchemaVersion As Double = 1.2

    ''' <summary>
    ''' The template itself. Contains scripts, field attributes, and user defined attributes.
    ''' </summary>
    <Serializable()> _
    Public Class aaTemplate
        <XmlAttribute("name")> Public Name As String
        <XmlAttribute("revision")> Public Revision As Integer
        <XmlAttribute("XmlVersion")> Public XmlVersion As Double
        Public Scripts As New Collection()
        Public FieldAttributesDiscrete As New Collection()
        Public FieldAttributesAnalog As New Collection()



        Public Sub New()

        End Sub

        Public Sub New(ByVal Name As String, ByVal Revision As Integer, ByVal Scripts As Collection, ByVal FieldAttributesDiscrete As Collection, ByVal FieldAttributesAnalog As Collection)
            Me.Name = Name
            Me.Revision = Revision
            Me.XmlVersion = SchemaVersion
            Me.Scripts = Scripts
            Me.FieldAttributesDiscrete = FieldAttributesDiscrete
            Me.FieldAttributesAnalog = FieldAttributesAnalog
        End Sub

    End Class

    ''' <summary>
    ''' Template script data structure.
    ''' </summary>
    <Serializable()> _
    Public Class aaScript
        <XmlAttribute("name")> Public Name As String
        <XmlAttribute("TemplateName")> Public TemplateName As String
        <XmlAttribute("TemplateRevision")> Public TemplateRevision As Integer
        <XmlAttribute("XmlVersion")> Public XmlVersion As Double
        Public ScriptExecutionGroup As String
        Public ScriptOrder As Integer
        'Public Aliases As New Collection()
        Public Aliases As String
        Public AliasReferences As String
        Public DeclarationsText As String
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
            ByVal TemplateName As String, _
            ByVal TemplateRevision As Integer, _
            ByVal ScriptExecutionGroup As String, _
            ByVal ScriptOrder As Integer, _
            ByVal Aliases As String, _
            ByVal AliasReferences As String, _
            ByVal DeclarationsText As String, _
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

            ' TODO: make the script texts a CDATA so that non-XML safe characters don't get rendered to entities
            Me.Name = Name
            Me.XmlVersion = SchemaVersion
            Me.TemplateName = TemplateName
            Me.TemplateRevision = TemplateRevision
            Me.ScriptOrder = ScriptOrder
            Me.ScriptExecutionGroup = ScriptExecutionGroup
            If Not String.IsNullOrEmpty(DeclarationsText) Then
                Me.DeclarationsText = vbCrLf & DeclarationsText & vbCrLf
            Else
                Me.DeclarationsText = ""
            End If
            If Not String.IsNullOrEmpty(OffScanText) Then
                Me.OffScanText = vbCrLf & OffScanText & vbCrLf
            Else
                Me.OffScanText = ""
            End If
            If Not String.IsNullOrEmpty(OnScanText) Then
                Me.OnScanText = vbCrLf & OnScanText & vbCrLf
            Else
                Me.OnScanText = ""
            End If
            If Not String.IsNullOrEmpty(ShutdownText) Then
                Me.ShutdownText = vbCrLf & ShutdownText & vbCrLf
            Else
                Me.ShutdownText = ""
            End If
            If Not String.IsNullOrEmpty(StartupText) Then
                Me.StartupText = vbCrLf & StartupText & vbCrLf
            Else
                Me.StartupText = ""
            End If
            Me.Expression = Expression
            Me.TriggerType = TriggerType
            Me.TriggerPeriod = TriggerPeriod
            Me.TriggerOnQualityChange = TriggerOnQualityChange
            Me.ExecuteTimeoutLimit = ExecuteTimeoutLimit
            Me.RunsAsync = RunsAsync
            If Not String.IsNullOrEmpty(ExecuteText) Then
                Me.ExecuteText = vbCrLf & ExecuteText & vbCrLf
            Else
                Me.ExecuteText = ""
            End If

            Me.Aliases = Aliases
            Me.AliasReferences = AliasReferences

            ' TODO: make a nice format to pair the aliases and references together.
            ' Thinking something like this: <aliases><alias name="AliasName1" reference="me.Tagname"/><alias name="AliasName2" reference="me.Description"/></aliases>
            ' This code is causing crashes for templates with no aliases when writing out to a file, so will need to come back to it.

            'Dim AliasCollection As New Collection()
            'If Not String.IsNullOrEmpty(Aliases) And Not Aliases.Contains("No Data") And Not String.IsNullOrEmpty(AliasReferences) And Not AliasReferences.Contains("No Data") Then
            '    ' Aliases come to us in two different attributes, both of which are a comma seperated list. 
            '    ' So, need to split them up and pair up the items in the list
            '    Dim AliasArray = Split(Aliases, ",")
            '    Dim ReferencesArray = Split(AliasReferences, ",")

            '    For i As Integer = 0 To AliasArray.Count - 1
            '        AliasCollection.Add(New aaAliases(AliasArray(i), ReferencesArray(i)))
            '    Next
            '    Me.Aliases = AliasCollection
            'Else
            '    Me.Aliases = vbEmpty
            'End If


        End Sub

    End Class

    ''' <summary>
    ''' Discrete Field Attribute data structure.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class aaFieldAttributeDiscrete
        <XmlAttribute("name")> Public Name As String
        <XmlAttribute("TemplateName")> Public TemplateName As String
        <XmlAttribute("TemplateRevision")> Public TemplateRevision As Integer
        <XmlAttribute("XmlVersion")> Public XmlVersion As Double
        Public AccessMode As String
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

        Public Sub New(ByVal Name As String, ByVal TemplateName As String, _
                       ByVal TemplateRevision As Integer, ByVal AccessMode As String, _
                       ByVal Buffered As Boolean, _
                       ByVal Description As String, ByVal InitialState As Boolean, _
                       ByVal EventOnChange As Boolean, ByVal InvertValue As Boolean, _
                       ByVal IO As aaAttrIO, ByVal StateLabels As aaAttrStateLabels, _
                       ByVal History As aaAttrHistoryDiscrete, ByVal StateAlarm As aaAttrStateAlarm, _
                       ByVal BadValueAlarm As aaAttrBadValueAlarm, ByVal Statistics As aaAttrStatistics)

            Me.Name = Name
            Me.XmlVersion = SchemaVersion
            Me.TemplateName = TemplateName
            Me.TemplateRevision = TemplateRevision
            Me.AccessMode = AccessMode
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

    ''' <summary>
    ''' Analog Field Attribute data structure.
    ''' </summary>
    <Serializable()> _
    Public Class aaFieldAttributeAnalog
        <XmlAttribute("name")> Public Name As String
        <XmlAttribute("TemplateName")> Public TemplateName As String
        <XmlAttribute("TemplateRevision")> Public TemplateRevision As Integer
        <XmlAttribute("XmlVersion")> Public XmlVersion As Double
        Public AccessMode As String
        Public AnalogType As String
        Public Buffered As Boolean
        Public Description As String
        Public InitialValue As Double
        Public Deadband As Double
        Public EngUnits As String
        Public EventOnChange As Boolean
        Public IO As aaAttrIO
        Public IOScaling As aaAttrIOScaling
        Public History As aaAttrHistoryAnalog
        Public LevelAlarm As aaAttrLevelAlarm
        Public ROCAlarm As aaAttrROCAlarm
        Public DeviationAlarm As aaAttrDeviationAlarm
        Public BadValueAlarm As aaAttrBadValueAlarm
        Public Statistics As aaAttrStatistics

        Public Sub New()

        End Sub

        Public Sub New(ByVal Name As String, ByVal TemplateName As String, _
                       ByVal TemplateRevision As Integer, ByVal AnalogType As String, _
                       ByVal AccessMode As String, _
                       ByVal Buffered As Boolean, ByVal Description As String, _
                       ByVal InitialValue As Double, ByVal EventOnChange As Boolean, _
                       ByVal IO As aaAttrIO, ByVal IOScaling As aaAttrIOScaling, _
                       ByVal History As aaAttrHistoryAnalog, ByVal LevelAlarm As aaAttrLevelAlarm, _
                       ByVal ROCAlarm As aaAttrROCAlarm, ByVal DeviationAlarm As aaAttrDeviationAlarm, _
                       ByVal BadValueAlarm As aaAttrBadValueAlarm, ByVal Statistics As aaAttrStatistics)

            Me.Name = Name
            Me.XmlVersion = SchemaVersion
            Me.TemplateName = TemplateName
            Me.TemplateRevision = TemplateRevision
            Me.AnalogType = AnalogType
            Me.AccessMode = AccessMode
            Me.Buffered = Buffered
            Me.Description = Description
            Me.InitialValue = InitialValue
            Me.EventOnChange = EventOnChange
            Me.IO = IO
            Me.IOScaling = IOScaling
            Me.History = History
            Me.LevelAlarm = LevelAlarm
            Me.ROCAlarm = ROCAlarm
            Me.DeviationAlarm = DeviationAlarm
            Me.BadValueAlarm = BadValueAlarm
            Me.Statistics = Statistics
        End Sub

    End Class

    '' -----------------------------------------------------------------------------
    '' The following are all used by the Field Attributes to group the individual parameters into groups that match the UI.
    '' -----------------------------------------------------------------------------

    <Serializable()> _
    Public Class aaAliases
        <XmlAttribute("name")> Public Name As String
        <XmlAttribute("reference")> Public Reference As String

        Public Sub New()
        End Sub
        Public Sub New(ByVal Name As String, ByVal Reference As String)
            Me.Name = Name
            Me.Reference = Reference
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

    <Serializable()> _
    Public Class aaAttrIOScaling
        <XmlAttribute("enabled")> Public Enabled As Boolean
        Public RawMin As Double
        Public RawMax As Double
        Public EngUnitsMin As Double
        Public EngUnitsMax As Double
        Public EngUnitsRangeMin As Double
        Public EngUnitsRangeMax As Double
        Public ConversionMode As String
        Public ClampInput As Boolean
        Public Sub New()
        End Sub
        Public Sub New(ByVal Enabled As Boolean, _
                       ByVal RawMin As Double, ByVal RawMax As Double, _
                       ByVal EngUnitsMin As Double, ByVal EngUnitsMax As Double, _
                       ByVal EngUnitsRangeMin As Double, ByVal EngUnitsRangeMax As Double, _
                       ByVal ConversionMode As String, ByVal ClampInput As Boolean)
            Me.Enabled = Enabled
            Me.RawMin = RawMin
            Me.RawMax = RawMax
            Me.EngUnitsMin = EngUnitsMin
            Me.EngUnitsMax = EngUnitsMax
            Me.EngUnitsRangeMin = EngUnitsRangeMin
            Me.EngUnitsRangeMax = EngUnitsRangeMax
            Me.ConversionMode = ConversionMode
            Me.ClampInput = ClampInput
        End Sub
    End Class

    <Serializable()> _
    Public Class aaAttrHistoryAnalog
        <XmlAttribute("enabled")> Public Enabled As Boolean
        Public StoragePeriod As Integer
        Public ValueDeadband As Double
        Public InterpolationType As String
        Public Rollover As Double
        Public TrendHi As Double
        Public TrendLo As Double
        Public SwingingDoor As Boolean
        Public RateDeadband As Double
        Public Sub New()
        End Sub
        Public Sub New(ByVal Enabled As Boolean, _
                       ByVal StoragePeriod As Integer, ByVal ValueDeadband As Double, _
                       ByVal InterpolationType As String, ByVal Rollover As Double, _
                       ByVal TrendHi As Double, ByVal TrendLo As Double, _
                       ByVal SwingingDoor As Boolean, ByVal RateDeadband As Double)
            Me.Enabled = Enabled
            Me.StoragePeriod = StoragePeriod
            Me.ValueDeadband = ValueDeadband
            Me.InterpolationType = InterpolationType
            Me.Rollover = Rollover
            Me.TrendHi = TrendHi
            Me.TrendLo = TrendLo
            Me.SwingingDoor = SwingingDoor
            Me.RateDeadband = RateDeadband
        End Sub
    End Class

    <Serializable()> _
    Public Class aaAttrAnalogLimit
        <XmlAttribute("enabled")> Public Enabled As Boolean
        Public Limit As Double
        Public Priority As Integer
        Public Description As String
        Public Sub New()
        End Sub
        Public Sub New(ByVal Enabled As Boolean, ByVal Limit As Double, ByVal Priority As Double, ByVal Description As String)
            Me.Enabled = Enabled
            Me.Limit = Limit
            Me.Priority = Priority
            Me.Description = Description
        End Sub
    End Class

    <Serializable()> _
    Public Class aaAttrLevelAlarm
        <XmlAttribute("enabled")> Public Enabled As Boolean
        Public HiHi As aaAttrAnalogLimit
        Public Hi As aaAttrAnalogLimit
        Public Lo As aaAttrAnalogLimit
        Public LoLo As aaAttrAnalogLimit
        Public ValueDeadband As Double
        Public TimeDeadband As String
        Public Sub New()
        End Sub
        Public Sub New(ByVal Enabled As Boolean, _
                       ByVal HiHi As aaAttrAnalogLimit, ByVal Hi As aaAttrAnalogLimit, _
                       ByVal Lo As aaAttrAnalogLimit, ByVal LoLo As aaAttrAnalogLimit, _
                       ByVal ValueDeadband As Double, ByVal TimeDeadband As String)
            Me.Enabled = Enabled
            Me.HiHi = HiHi
            Me.Hi = Hi
            Me.Lo = Lo
            Me.LoLo = Lo
            Me.ValueDeadband = ValueDeadband
            Me.TimeDeadband = TimeDeadband
        End Sub
    End Class

    <Serializable()> _
    Public Class aaAttrROCAlarm
        <XmlAttribute("enabled")> Public Enabled As Boolean
        Public Up As aaAttrAnalogLimit
        Public Down As aaAttrAnalogLimit
        Public RateUnits As String
        Public EvalPeriod As Integer
        Public Sub New()
        End Sub
        Public Sub New(ByVal Enabled As Boolean, _
                       ByVal Up As aaAttrAnalogLimit, ByVal Down As aaAttrAnalogLimit, _
                       ByVal RateUnits As String, ByVal EvalPeriod As Integer)
            Me.Enabled = Enabled
            Me.Up = Up
            Me.Down = Down
            Me.RateUnits = RateUnits
            Me.EvalPeriod = EvalPeriod
        End Sub
    End Class

    <Serializable()> _
    Public Class aaAttrDeviationAlarm
        <XmlAttribute("enabled")> Public Enabled As Boolean
        Public Minor As aaAttrAnalogLimit
        Public Major As aaAttrAnalogLimit
        Public Target As Double
        Public DeviationDeadband As Double
        Public SettlingPeriod As String
        Public Sub New()
        End Sub
        Public Sub New(ByVal Enabled As Boolean, _
                       ByVal Minor As aaAttrAnalogLimit, ByVal Major As aaAttrAnalogLimit, _
                       ByVal Target As Double, ByVal DeviationDeadband As Double, _
                       ByVal SettlingPeriod As String)
            Me.Enabled = Enabled
            Me.Minor = Minor
            Me.Major = Major
            Me.Target = Target
            Me.DeviationDeadband = DeviationDeadband
            Me.SettlingPeriod = SettlingPeriod
        End Sub
    End Class

End Module