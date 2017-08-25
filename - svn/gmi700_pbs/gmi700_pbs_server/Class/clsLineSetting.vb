Imports System.Xml

Public Class clsLineSetting

#Region "Attribute"
    Private m_intLineNo As Integer
    Private m_strLineName As String
    Private m_intNet As Integer
    Private m_intNode As Integer
    Private m_intUnit As Integer
    Private m_strSyncTimeWhen As String

    Private m_mtpReadStatusMemory As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
    Private m_intReadStatusAddress As Integer
    Private m_intReadStatusLength As Integer

    Private m_mtpReadDataMemory As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
    Private m_intReadDataAddress As Integer
    Private m_intReadDataAsciiLength As Integer
    Private m_intReadDataBcdLength As Integer

    Private m_mtpWriteStatusMemory As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
    Private m_intWriteStatusAddress As Integer

    Private m_mtpWriteLifeMemory As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
    Private m_intWriteLifeAddress As Integer

    Private m_mtpWriteSyncMemory As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
    Private m_intWriteSyncAddress As Integer

    Private m_intSleepInterval As Integer
    Private m_strWritePath As String
    Private m_strRootFolder As String
    Private m_strRootTempFolder As String
    Private m_strFormat As String
    Private m_aWriteField As List(Of clsField)
    Private m_intUseCsvMode As Integer
    Private m_intUseXlsMode As Integer

    Private m_objFieldSerial As clsField
    Private m_objFieldMode As clsField
    Private m_objFieldMc As clsField
    Private m_objFieldLotNo As clsField
    Private m_objFieldFileName As clsField
    Private m_objFieldDateTime As clsField
    Private m_objFieldStatus As clsField

    Private m_intCopyFile As Integer
    Private m_strCopyPath As String
    Private m_strCopyWildCard As String
    Private m_strCopyPurgeOldPath As String
    Private m_intCopyPeriodMilliSec As Integer
    Private m_intCopyPurgePeriodDay As Integer
#End Region

#Region "Properties"
    Public ReadOnly Property LineNo As Integer
        Get
            Return m_intLineNo
        End Get
    End Property

    Public ReadOnly Property LineName As String
        Get
            Return m_strLineName
        End Get
    End Property

    Public ReadOnly Property Net As Integer
        Get
            Return m_intNet
        End Get
    End Property

    Public ReadOnly Property Node As Integer
        Get
            Return m_intNode
        End Get
    End Property

    Public ReadOnly Property Unit As Integer
        Get
            Return m_intUnit
        End Get
    End Property

    Public ReadOnly Property SyncTimeWhen As String
        Get
            Return m_strSyncTimeWhen
        End Get
    End Property

    Public WriteOnly Property ReadStatusMemory As String
        Set(value As String)
            Dim astrTemp() As String = value.Split("_")

            Select Case astrTemp(0)
                Case "DM"
                    m_mtpReadStatusMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM
                Case "E0"
                    m_mtpReadStatusMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM0
                Case "E1"
                    m_mtpReadStatusMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM1
                Case Else
                    m_mtpReadStatusMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM
            End Select

            If Not Integer.TryParse(astrTemp(1), m_intReadStatusAddress) Then
                m_intReadStatusAddress = 5000
            End If
        End Set
    End Property

    Public ReadOnly Property ReadStatusMemoryType As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
        Get
            Return m_mtpReadStatusMemory
        End Get
    End Property

    Public ReadOnly Property ReadStatusAddress As Integer
        Get
            Return m_intReadStatusAddress
        End Get
    End Property

    Public ReadOnly Property ReadStatusLength As Integer
        Get
            Return m_intReadStatusLength
        End Get
    End Property

    Public WriteOnly Property ReadDataMemory As String
        Set(value As String)
            Dim astrTemp() As String = value.Split("_")

            Select Case astrTemp(0)
                Case "DM"
                    m_mtpReadDataMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM
                Case "E0"
                    m_mtpReadDataMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM0
                Case "E1"
                    m_mtpReadDataMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM1
                Case Else
                    m_mtpReadDataMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM0
            End Select

            If Not Integer.TryParse(astrTemp(1), m_intReadDataAddress) Then
                m_intReadDataAddress = 32700
            End If
        End Set
    End Property

    Public ReadOnly Property ReadDataMemoryType As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
        Get
            Return m_mtpReadDataMemory
        End Get
    End Property

    Public ReadOnly Property ReadDataAddress As Integer
        Get
            Return m_intReadDataAddress
        End Get
    End Property

    Public ReadOnly Property ReadDataAsciiLength As Integer
        Get
            Return m_intReadDataAsciiLength
        End Get
    End Property

    Public ReadOnly Property ReadDataBcdLength As Integer
        Get
            Return m_intReadDataBcdLength
        End Get
    End Property

    Public ReadOnly Property WriteStatusMemoryType As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
        Get
            Return m_mtpWriteStatusMemory
        End Get
    End Property

    Public WriteOnly Property WriteStatusMemory As String
        Set(value As String)
            Dim astrTemp() As String = value.Split("_")

            Select Case astrTemp(0)
                Case "DM"
                    m_mtpWriteStatusMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM
                Case "E0"
                    m_mtpWriteStatusMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM0
                Case "E1"
                    m_mtpWriteStatusMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM1
                Case Else
                    m_mtpWriteStatusMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM
            End Select

            If Not Integer.TryParse(astrTemp(1), m_intWriteStatusAddress) Then
                m_intWriteStatusAddress = 5001
            End If
        End Set
    End Property

    Public ReadOnly Property WriteStatusAddress As Integer
        Get
            Return m_intWriteStatusAddress
        End Get
    End Property

    Public ReadOnly Property WriteLifeMemoryType As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
        Get
            Return m_mtpWriteLifeMemory
        End Get
    End Property

    Public WriteOnly Property WriteLifeMemory As String
        Set(value As String)
            Dim astrTemp() As String = value.Split("_")

            Select Case astrTemp(0)
                Case "DM"
                    m_mtpWriteLifeMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM
                Case "E0"
                    m_mtpWriteLifeMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM0
                Case "E1"
                    m_mtpWriteLifeMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM1
                Case Else
                    m_mtpWriteLifeMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM
            End Select

            If Not Integer.TryParse(astrTemp(1), m_intWriteLifeAddress) Then
                m_intWriteLifeAddress = 5002
            End If
        End Set
    End Property

    Public ReadOnly Property WriteLifeAddress() As UInteger
        Get
            Return m_intWriteLifeAddress
        End Get
    End Property

    Public ReadOnly Property WriteSyncMemoryType As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes
        Get
            Return m_mtpWriteSyncMemory
        End Get
    End Property

    Public WriteOnly Property WriteSyncMemory As String
        Set(value As String)
            Dim astrTemp() As String = value.Split("_")

            Select Case astrTemp(0)
                Case "DM"
                    m_mtpWriteSyncMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM
                Case "E0"
                    m_mtpWriteSyncMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM0
                Case "E1"
                    m_mtpWriteSyncMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.ExDM1
                Case Else
                    m_mtpWriteSyncMemory = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM
            End Select

            If Not Integer.TryParse(astrTemp(1), m_intWriteSyncAddress) Then
                m_intWriteSyncAddress = 5010
            End If
        End Set
    End Property

    Public ReadOnly Property WriteSyncAddress() As UInteger
        Get
            Return m_intWritesyncAddress
        End Get
    End Property

    Public ReadOnly Property WritePath As String
        Get
            Return m_strWritePath
        End Get
    End Property

    Public ReadOnly Property SleepInterval As Integer
        Get
            Return m_intSleepInterval
        End Get
    End Property

    Public ReadOnly Property RootFolder As String
        Get
            Return m_strRootFolder
        End Get
    End Property

    Public ReadOnly Property RootTempFolder As String
        Get
            Return m_strRootTempFolder
        End Get
    End Property

    Public ReadOnly Property Format As String
        Get
            Return m_strFormat
        End Get
    End Property

    Public ReadOnly Property Fields As List(Of clsField)
        Get
            Return m_aWriteField
        End Get
    End Property

    Public ReadOnly Property UseCsvMode As Boolean
        Get
            Return m_intUseCsvMode = 1
        End Get
    End Property

    Public ReadOnly Property UseXlsMode As Boolean
        Get
            Return m_intUseXlsMode = 1
        End Get
    End Property

    Public ReadOnly Property FieldSerial As clsField
        Get
            Return m_objFieldSerial
        End Get
    End Property

    Public ReadOnly Property FieldMode As clsField
        Get
            Return m_objFieldMode
        End Get
    End Property

    Public ReadOnly Property FieldMc As clsField
        Get
            Return m_objFieldMc
        End Get
    End Property

    Public ReadOnly Property FieldLotNo As clsField
        Get
            Return m_objFieldLotNo
        End Get
    End Property

    Public ReadOnly Property FieldFileName As clsField
        Get
            Return m_objFieldFileName
        End Get
    End Property

    Public ReadOnly Property FieldDateTime As clsField
        Get
            Return m_objFieldDateTime
        End Get
    End Property

    Public ReadOnly Property FieldStatus As clsField
        Get
            Return m_objFieldStatus
        End Get
    End Property

    Public ReadOnly Property DoCopyFile As Boolean
        Get
            Return m_intCopyFile = 1
        End Get
    End Property

    Public ReadOnly Property CopyPath As String
        Get
            Return m_strCopyPath
        End Get
    End Property

    Public ReadOnly Property CopyWildCard As String
        Get
            Return m_strCopyWildCard
        End Get
    End Property

    Public ReadOnly Property CopyPeriodMilliSec As Integer
        Get
            Return m_intCopyPeriodMilliSec
        End Get
    End Property

    Public ReadOnly Property CopyPurgePeriodDay As Integer
        Get
            Return m_intCopyPurgePeriodDay
        End Get
    End Property

    Public ReadOnly Property CopyPurgeOldPath As String
        Get
            Return m_strCopyPurgeOldPath
        End Get
    End Property
#End Region

#Region "Constructor"
    Public Sub New()
        Me.Init()
    End Sub
#End Region

#Region "Method"
    Public Sub Init()
        m_intLineNo = -1
        m_strLineName = ""
        m_intNet = -1
        m_intNode = -1
        m_intUnit = -1
        m_intReadStatusAddress = -1
        m_intReadStatusLength = -1
        m_intReadDataAddress = -1
        m_intReadDataAsciiLength = -1
        m_intReadDataBcdLength = -1
        m_intWriteStatusAddress = -1
        m_intWriteLifeAddress = -1
        m_intWriteSyncAddress = -1
        m_intUseCsvMode = 1
        m_intUseXlsMode = 1

        m_intSleepInterval = -1
        m_strWritePath = ""
        m_strRootFolder = ""
        m_strRootTempFolder = ""
        m_strFormat = ""
        m_aWriteField = Nothing

        m_objFieldSerial = Nothing
        m_objFieldMode = Nothing
        m_objFieldMc = Nothing
        m_objFieldLotNo = Nothing
        m_objFieldFileName = Nothing
        m_objFieldDateTime = Nothing
        m_objFieldStatus = Nothing

        m_intCopyFile = 0
        m_strCopyPath = ""
        m_strCopyWildCard = ""
        m_strCopyPurgeOldPath = ""
        m_intCopyPeriodMilliSec = 5000
        m_intCopyPurgePeriodDay = 2

        m_strSyncTimeWhen = ""

    End Sub

    Public Shared Function FindAll() As List(Of clsLineSetting)

        Dim xmlFilePath As String = GetSettingPath("line")

        Dim objDoc As New XmlDocument
        Dim lstLine As New List(Of clsLineSetting)

        Try
            objDoc.Load(xmlFilePath)
            Dim nodeCommonSetting As XmlNodeList = objDoc.GetElementsByTagName("common")
            If nodeCommonSetting.Count <> 1 Then
                Throw New Exception("Invalid common setting in line.xml format")
            End If

            Dim intSleepInterval As Integer = nodeCommonSetting.Item(0).Attributes.GetNamedItem("sleep_interval").Value
            Dim strRootFolder As String = nodeCommonSetting.Item(0).Attributes.GetNamedItem("root").Value
            Dim strRootTempFolder As String = nodeCommonSetting.Item(0).Attributes.GetNamedItem("root_temp").Value
            Dim intReadDataAsciiLength As Integer = nodeCommonSetting.Item(0).Attributes.GetNamedItem("readdataasciilength").Value
            Dim intReadDataBcdLength As Integer = nodeCommonSetting.Item(0).Attributes.GetNamedItem("readdatabcdlength").Value
            Dim strSyncTimeWhen As String = nodeCommonSetting.Item(0).Attributes.GetNamedItem("synctimewhen").Value
            Dim intUseCsvMode As Integer = nodeCommonSetting.Item(0).Attributes.GetNamedItem("usecsvmode").Value
            Dim intUseXlsMode As Integer = nodeCommonSetting.Item(0).Attributes.GetNamedItem("usexlsmode").Value

            'Dim nodeLineSetting As XmlNodeList = objDoc.GetElementsByTagName("line")
            Using context As fct_gmi700_pbsEntities = New fct_gmi700_pbsEntities()


                'For i = 0 To nodeLineSetting.Count - 1
                'For i = 0 To context.PLC_MASTER.Count - 1
                For Each plc In context.PLC_MASTER

                    Dim line As New clsLineSetting
                    'line.m_intNet = nodeLineSetting.Item(i).Attributes.GetNamedItem("net").Value
                    'line.m_intNode = nodeLineSetting.Item(i).Attributes.GetNamedItem("node").Value
                    'line.m_intUnit = nodeLineSetting.Item(i).Attributes.GetNamedItem("unit").Value
                    line.m_intNet = plc.PLC_NET
                    line.m_intNode = plc.PLC_NODE
                    line.m_intUnit = plc.PLC_UNIT

                    line.m_intSleepInterval = intSleepInterval
                    line.m_strRootFolder = strRootFolder
                    line.m_strRootTempFolder = strRootTempFolder
                    line.m_strSyncTimeWhen = strSyncTimeWhen
                    line.m_intUseCsvMode = intUseCsvMode
                    line.m_intUseXlsMode = intUseXlsMode


                    line.m_intLineNo = nodeLineSetting.Item(i).Attributes.GetNamedItem("no").Value
                    line.m_strLineName = nodeLineSetting.Item(i).Attributes.GetNamedItem("name").Value
                    line.ReadStatusMemory = nodeLineSetting.Item(i).Attributes.GetNamedItem("readstatusaddress").Value
                    line.m_intReadStatusLength = nodeLineSetting.Item(i).Attributes.GetNamedItem("readstatuslength").Value
                    line.ReadDataMemory = nodeLineSetting.Item(i).Attributes.GetNamedItem("readdataaddress").Value
                    line.m_intReadDataAsciiLength = intReadDataAsciiLength
                    line.m_intReadDataBcdLength = intReadDataBcdLength
                    line.WriteStatusMemory = nodeLineSetting.Item(i).Attributes.GetNamedItem("writestatusaddress").Value
                    line.WriteLifeMemory = nodeLineSetting.Item(i).Attributes.GetNamedItem("writelifeaddress").Value
                    line.WriteSyncMemory = nodeLineSetting.Item(i).Attributes.GetNamedItem("writesyncaddress").Value
                    line.m_strWritePath = nodeLineSetting.Item(i).Attributes.GetNamedItem("path").Value

                    line.m_intCopyFile = nodeLineSetting.Item(i).Attributes.GetNamedItem("copyfile").Value
                    line.m_strCopyPath = nodeLineSetting.Item(i).Attributes.GetNamedItem("copypath").Value
                    line.m_strCopyWildCard = nodeLineSetting.Item(i).Attributes.GetNamedItem("copywildcard").Value
                    line.m_strCopyPurgeOldPath = nodeLineSetting.Item(i).Attributes.GetNamedItem("copypurgeoldpath").Value
                    line.m_intCopyPeriodMilliSec = nodeLineSetting.Item(i).Attributes.GetNamedItem("copyperiodmillisec").Value
                    line.m_intCopyPurgePeriodDay = nodeLineSetting.Item(i).Attributes.GetNamedItem("copypurgeperiodday").Value

                    line.m_objFieldSerial = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("serial").Value.Trim)
                    line.m_objFieldMode = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("mode").Value.Trim)
                    line.m_objFieldMc = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("mc").Value.Trim)
                    line.m_objFieldLotNo = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("lotno").Value.Trim)
                    line.m_objFieldFileName = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("filename").Value.Trim)
                    line.m_objFieldDateTime = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("datetime").Value.Trim)
                    line.m_objFieldStatus = New clsField(nodeLineSetting.Item(i).Attributes.GetNamedItem("status").Value.Trim)

                    line.m_strFormat = nodeLineSetting.Item(i).Attributes.GetNamedItem("format").Value
                    line.m_aWriteField = clsField.GetFieldList(line.m_strFormat)
                    lstLine.Add(line)
                Next


            End Using


            Return lstLine
        Catch ex As Exception

            Return lstLine
        End Try
    End Function

    Private Shared Function GetSettingPath(ByVal settingName As String) As String
        Dim strFileName As String = settingName & ".xml"
        Dim strProgramDataFileName As String = My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\" & strFileName
        Dim strAppPathFileName As String = My.Application.Info.DirectoryPath & "\" & strFileName
        If My.Computer.FileSystem.FileExists(strProgramDataFileName) Then
            Return strProgramDataFileName
        Else
            Try
                My.Computer.FileSystem.CopyFile(strAppPathFileName, strProgramDataFileName, True)
                Return strProgramDataFileName
            Catch ex As Exception
                Return strAppPathFileName
            End Try
        End If
    End Function
#End Region

End Class
