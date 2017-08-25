Public Class clsPlcCommunication
    Inherits System.ComponentModel.BackgroundWorker

#Region "Attribute"
    Private m_blnIsConnected As Boolean

    Private m_clsCurrentState As clsStateBase
    Public m_clsStateWaiting As clsStateBase
    Public m_clsStateProcessing As clsStateBase
    Public m_clsStateComplete As clsStateBase
    Public m_clsStateNoData As clsStateBase
    Public m_clsStateError As clsStateBase

    Private m_objLogger As clsLogger
    Private m_intSleepTime As Integer

    Private m_strLotNo As String

    Private m_plcObject As clsPlcWrapper
    'Private Me.m_objPLCSetting As clsLineSetting
    Public m_objPLCSetting As PLC_MASTER

    'Public dbEntities As fct_gmi700_pbsEntities = New fct_gmi700_pbsEntities()

    'Public intPlcId As Integer

    Private m_datNextSyncTime As DateTime
    Public m_dbEntities As fct_gmi700_pbsEntities
    Public m_memoryMap As System.Data.Entity.DbSet(Of gmi700_pbs_server.PLC_MEMORY_MAP)
    Public m_objDbMan As clsDatabaseMan
#End Region

#Region "Constructor"

    'Public Sub New(ByVal objLine As clsLineSetting
    Public Sub New(ByVal plc As PLC_MASTER)

        Me.m_objPLCSetting = New PLC_MASTER
        Me.m_objPLCSetting = plc

        'intPlcId = plc.PLC_ID

        m_dbEntities = New fct_gmi700_pbsEntities
        m_memoryMap = m_dbEntities.PLC_MEMORY_MAP
        m_objDbMan = New clsDatabaseMan

        m_strLotNo = String.Empty
        m_objLogger = New clsLogger(Me.m_objPLCSetting.PLC_DESCR)
        m_plcObject = New clsPlcWrapper
        'm_plcObject.ReceiveTimeLimit = 2000
        'm_plcObject.RetryCount = 10
        m_plcObject.ReceiveTimeLimit = g_ReceiveTimeLimit
        m_plcObject.RetryCount = g_RetryCount
        m_objLogger.AppendLog("Before InitializeAndConnect", "Info")
        m_plcObject.InitializeAndConnect(Me.m_objPLCSetting.PLC_NET, Me.m_objPLCSetting.PLC_NODE, Me.m_objPLCSetting.PLC_UNIT)
        m_objLogger.AppendLog("After InitializeAndConnect", "Info")
        
        'm_intSleepTime = Me.m_objPLCSetting.SleepInterval
        m_intSleepTime = PLC_SLEEP_INTERNAL

        m_blnIsConnected = False

        Me.m_clsStateWaiting = New clsStateWaiting(Me)
        Me.m_clsStateProcessing = New clsStateProcessing(Me)
        Me.m_clsStateComplete = New clsStateComplete(Me)
        Me.m_clsStateError = New clsStateError(Me)

        Me.SetState(Me.m_clsStateWaiting)
    End Sub

#End Region

#Region "Property"

    Public ReadOnly Property PlcObject As clsPlcWrapper
        Get
            Return m_plcObject
        End Get
    End Property

    Public ReadOnly Property IsConnected() As Boolean
        Get
            Return m_blnIsConnected
        End Get
    End Property

    Public Property LotNo As String
        Get
            Return m_strLotNo
        End Get
        Set(ByVal value As String)
            If value.Trim.Length > 0 Then
                m_strLotNo = value
            End If
        End Set
    End Property

    Public ReadOnly Property ErrorMessage() As String
        Get
            Return m_clsCurrentState.GetErrorMessage
        End Get
    End Property

    Public ReadOnly Property LineName As String
        Get
            Return m_clsCurrentState.m_objPLCSetting.PLC_DESCR
        End Get
    End Property

    'Public ReadOnly Property LineNo As Integer
    '    Get
    '        Return m_clsCurrentState.LineNo
    '    End Get
    'End Property

    Public ReadOnly Property Logger As clsLogger
        Get
            Return m_objLogger
        End Get
    End Property

    Public ReadOnly Property PLCSetting As PLC_MASTER
        Get
            Return Me.m_objPLCSetting
        End Get
    End Property
#End Region

#Region "Event"

    Private Sub clsPlcCommunication_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Me.DoWork

        My.Application.ChangeCulture("en-US")
        My.Application.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd"
        My.Application.Culture.DateTimeFormat.LongDatePattern = "yyyy/MM/dd"

        Dim dat As New Date()
        Dim intThreadCount As Integer = 0
        Dim intTimeOutCount As Integer = 0
        m_plcObject.Active = True

        m_datNextSyncTime = GetNextSyncTime()
        While Not CancellationPending
            Try
                intThreadCount += 1

                If m_clsCurrentState.IsPlcConnect() Then
                    m_blnIsConnected = True

                    If m_clsCurrentState.SendKeepAlive() Then
                        Me.ReportProgress(100)
                        m_clsCurrentState.Process()
                    Else
                        m_blnIsConnected = False
                        m_objLogger.AppendLog(Me.GetType.Name, "DoWork", "Send KPAL Failed", "Error")
                        Me.ReportProgress(0)
                    End If
                Else
                    intTimeOutCount += 1
                    m_blnIsConnected = False
                    m_objLogger.AppendLog(Me.GetType.Name, "DoWork", "PLC Connect Failed", "Error")
                    Me.ReportProgress(0)
                End If
            Catch ex As Exception
                Me.m_blnIsConnected = False
                Me.m_clsCurrentState.SetErrorMessage = "Process is terminated Because exception happen"
                Me.ReportProgress(0)
                m_objLogger.AppendLog(Me.GetType.Name, "DoWork", ex)
            Finally
                If intThreadCount > 1000 Then
                    m_objLogger.AppendLog(Me.GetType.Name, "DoWork", "Thread Still Alive", "Info")
                    intThreadCount = 0
                End If

                If intTimeOutCount > 100 Then
                    m_objLogger.AppendLog(Me.GetType.Name, "DoWork", "PLC Timeout Exceeds 30 Times", "Error")
                    intTimeOutCount = 0
                End If
            End Try
            'Threading.Thread.Sleep(Me.m_objPLCSetting.SleepInterval)
            Threading.Thread.Sleep(PLC_SLEEP_INTERNAL)
        End While
    End Sub

#End Region

#Region "Method"

    Public Sub SetState(ByRef state As clsStateBase)
        If Me.m_clsCurrentState Is Nothing Then
            m_objLogger.AppendLog(Me.GetType.Name, "SetState", "Change State To :" & state.GetType.Name, "Info")
            state.PreviousPlcStatusCode = -1

        Else
            state.SetErrorMessage = Me.m_clsCurrentState.GetErrorMessage
            m_objLogger.AppendLog(Me.GetType.Name, "SetState", "Change State From :" & m_clsCurrentState.GetType.Name & " To " & state.GetType.Name, "Info")
            state.PreviousPlcStatusCode = Me.m_clsCurrentState.PreviousPlcStatusCode

            state.int_for_ReplyToPlc = Me.m_clsCurrentState.int_for_ReplyToPlc

        End If

        state.WorkDone = False
        Me.m_clsCurrentState = state
    End Sub

    Public Sub ForceToProcessCurrentState()
        If Me.m_clsCurrentState IsNot Nothing Then
            m_objLogger.AppendLog(Me.GetType.Name, "ForceToProcessCurrentState", "Current Process is forced to start", "Info")
            m_clsCurrentState.Process()
        End If
    End Sub

    Private Function GetNextSyncTime() As DateTime
        'Dim strHour As String = Me.m_objPLCSetting.SyncTimeWhen.Split(":")(0)
        'Dim strMinute As String = Me.m_objPLCSetting.SyncTimeWhen.Split(":")(1)
        Dim strHour As String = PLC_SYNC_TIME_WHEN.Split(":")(0)
        Dim strMinute As String = PLC_SYNC_TIME_WHEN.Split(":")(1)
        Dim datTemp As DateTime = New DateTime(Now.Year, Now.Month, Now.Day, CInt(strHour), CInt(strMinute), 0)

        If DateTime.Compare(datTemp, Now) <= 0 Then
            datTemp = datTemp.AddDays(1)
        End If

        Return datTemp
    End Function

#End Region

End Class
