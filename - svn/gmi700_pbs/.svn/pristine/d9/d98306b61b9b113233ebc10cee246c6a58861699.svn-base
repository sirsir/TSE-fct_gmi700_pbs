Public MustInherit Class clsStateBase

#Region "Constant"
    
#End Region

#Region "Attribute"
    Private Shared m_dictLatestSendLogMsg As Dictionary(Of Integer, String)
    Private Shared m_dictLatestRecvLogMsg As Dictionary(Of Integer, String)

    Protected m_intPreviousStateCode As Integer
    Protected m_intCurrentStateCode As Integer
    Protected m_intPreviousPlcStatusCode As Integer
    Protected m_strErrorMessage As String

    'Protected m_intLineNo As String
    'Protected m_strLineName As String
    Protected m_blnWorkDone As Boolean

    Protected m_clsLogger As clsLogger
    Protected m_objPlc As clsPlcWrapper
    'Protected m_objLineSetting As clsLineSetting
    Public m_objPLCSetting As PLC_MASTER
    'Public m_objPLCSetting As PLC_MASTER
    Protected m_objPlcLineThread As clsPlcCommunication

    Protected intPlcId As Integer

    Public int_for_ReplyToPlc As Integer

#End Region

#Region "Constructor"
    'Dim m_intLineNo As Integer

    'TODO create new constructor
    Public Sub New(ByRef plcLineThread As clsPlcCommunication)


        Me.m_clsLogger = plcLineThread.Logger
        Me.m_objPlc = plcLineThread.PlcObject
        Me.m_objPlcLineThread = plcLineThread
        'Me.m_objPLCSetting = plcLineThread.LineSetting
        Me.m_objPLCSetting = plcLineThread.PLCSetting
        'Me.m_intLineNo = plcLineThread.PLCSetting.LineNo
        'Me.m_strLineName = plcLineThread.LineSetting.LineName
        'Me.m_intLineNo = plcLineThread.PLCSetting.PROCESS_ID
        'Me.m_strLineName = plcLineThread.PLCSetting.PLC_DESCR
        Me.m_blnWorkDone = False


        'plcLineThread.PLCSetting not works?
        intPlcId = plcLineThread.PLCSetting.PLC_ID
        'intPlcId = plcLineThread.PLCSetting.PLC_ID

    End Sub

#End Region

#Region "Property"

    'Public ReadOnly Property LineNo() As Integer
    '    Get
    '        Return m_intLineNo
    '    End Get
    'End Property

    'Public ReadOnly Property LineName() As String
    '    Get
    '        Return m_strLineName
    '    End Get
    'End Property

    Public ReadOnly Property GetErrorMessage() As String
        Get
            Return m_strErrorMessage
        End Get
    End Property

    Public WriteOnly Property SetErrorMessage()
        Set(ByVal value)
            m_strErrorMessage = value
        End Set
    End Property

    Public Property WorkDone() As Boolean
        Get
            Return m_blnWorkDone
        End Get
        Set(ByVal value As Boolean)
            m_blnWorkDone = value
        End Set
    End Property

    Public ReadOnly Property CurrentStateCode() As Integer
        Get
            Return m_intCurrentStateCode
        End Get
    End Property

    Public Property PreviousPlcStatusCode() As Integer
        Get
            Return m_intPreviousPlcStatusCode
        End Get
        Set(ByVal value As Integer)
            m_intPreviousPlcStatusCode = value
        End Set
    End Property

#End Region

#Region "Method"

    Public MustOverride Sub Process()

    Public MustOverride Sub Initial(Optional ByVal inObj As Object = Nothing)

    Public Function IsPlcConnect() As Boolean
        Try
            'Read for test plc connection
            'Dim intTemp As Integer = m_objPlc.ReadMemoryWordInteger(m_objPLCSetting.ReadDataMemoryType _
            '                                                        , m_objPLCSetting.ReadStatusAddress _
            '                                                        , m_objPLCSetting.ReadStatusLength _
            '                                                        , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)

            Dim memoryMap As PLC_MEMORY_MAP
            'Using dbEntities As fct_gmi700_pbsEntities = New fct_gmi700_pbsEntities()
            '    memoryMap = (From mem In dbEntities.PLC_MEMORY_MAP
            '                                  Where mem.DESCR = "PC Online").First
            'End Using
            memoryMap = (From mem In m_objPlcLineThread.m_memoryMap
                                              Where mem.DESCR = "PC Online").First
            

            Dim intTemp As Integer = m_objPlc.ReadMemoryWordInteger(PLC_MEMORY_TYPE _
                                                                    , CInt(memoryMap.OFFSET) + m_objPlcLineThread.m_objDbMan.PLC_GetAddress(Me.m_objPLCSetting.PLC_ID) _
                                                                    , memoryMap.NO_OF_WORDS _
                                                                    , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)

            'Dim intTemp As Integer = m_objPlc.ReadMemoryWordInteger(PLC_MEMORY_TYPE _
            '                                                        , CInt(memoryMap.OFFSET) + PLC_GetAddress(intPlcId) _
            '                                                        , memoryMap.NO_OF_WORDS _
            '                                                        , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)

            Return True
            'Return intTemp
        Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
            m_strErrorMessage = "PLC Time Out"
            Return False
        Catch ex As Exception
            m_strErrorMessage = ex.Message
            Return False
        End Try
    End Function


    Public Function ReadPlcStatus() As Integer
        Dim intReadStatus As Integer

        'm_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "Start Function", "Info")

        Try
            'intReadStatus = m_objPlc.ReadMemoryWordInteger(m_objLineSetting.ReadStatusMemoryType _
            '                                               , m_objLineSetting.ReadStatusAddress _
            '                                               , m_objLineSetting.ReadStatusLength _
            '                                               , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)

            Dim memoryMap As PLC_MEMORY_MAP

            'Using dbEntities As fct_gmi700_pbsEntities = New fct_gmi700_pbsEntities()

            '    memoryMap = (From mem In dbEntities.PLC_MEMORY_MAP
            '                                     Where mem.DESCR = "PLC Status").First
            'End Using
            memoryMap = (From mem In m_objPlcLineThread.m_memoryMap
                                            Where mem.DESCR = "PLC Status").First

            Dim addressTemp As Integer = CInt(memoryMap.OFFSET) + m_objPlcLineThread.m_objDbMan.PLC_GetAddress(Me.m_objPLCSetting.PLC_ID)



            intReadStatus = m_objPlc.ReadMemoryWordInteger(PLC_MEMORY_TYPE _
                                                           , addressTemp _
                                                                    , memoryMap.NO_OF_WORDS _
                                                                    , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)


            'm_clsLogger.AppendLog("ReadPlcStatus = " & CStr(intReadStatus), "Info")
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "[" & CStr(addressTemp) & "] = " & intReadStatus, "PLC")
        Catch ex As Exception
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", ex)
            Return PLC_READ_STATUS_READ_FAILED
        End Try

        'If Not IsNewPlcMessage(intReadStatus) Then
        '    Return PLC_READ_STATUS_SAME_STATUS
        'End If

        'm_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "End Function", "Info")
        Return intReadStatus
    End Function

    Public Function SendKeepAlive() As Boolean
        Try
            'm_objPlc.WriteMemoryWordInteger(m_objPLCSetting.WriteLifeMemoryType _
            '                                , m_objPLCSetting.WriteLifeAddress _
            '                                , {1} _
            '                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)
            Dim memoryMap As PLC_MEMORY_MAP
            'Using dbEntities As fct_gmi700_pbsEntities = New fct_gmi700_pbsEntities()
            '    memoryMap = (From mem In dbEntities.PLC_MEMORY_MAP
            '                                  Where mem.DESCR = "PC Online").First
            'End Using
            memoryMap = (From mem In m_objPlcLineThread.m_memoryMap
                                            Where mem.DESCR = "PC Online").First

            m_objPlc.WriteMemoryWordInteger(PLC_MEMORY_TYPE _
                                                                    , CInt(memoryMap.OFFSET) + m_objPlcLineThread.m_objDbMan.PLC_GetAddress(Me.m_objPLCSetting.PLC_ID) _
                                                                    , {1} _
                                                                    , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)

            Return True
        Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
            m_strErrorMessage = "PLC Time Out"
            Return False
        Catch ex As Exception
            m_strErrorMessage = ex.Message
            Return False
        End Try
    End Function

   


    Public Sub ReplyToPlc(Optional ByVal int2sent As Integer = -1)
        If int2sent = -1 Then
            int2sent = int_for_ReplyToPlc

        End If

        Dim blnSent As Boolean = False
        While Not blnSent
            Try
                'm_objPlc.WriteMemoryWordInteger(m_objLineSetting.WriteStatusMemoryType _
                '                                , m_objLineSetting.WriteStatusAddress _
                '                                , {PLC_WRITE_STATUS_PROCESSING} _
                '                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)


                Dim memoryMap As PLC_MEMORY_MAP


                'Using dbEntities As fct_gmi700_pbsEntities = New fct_gmi700_pbsEntities()
                '    memoryMap = (From mem In dbEntities.PLC_MEMORY_MAP
                '                             Where mem.DESCR = "PC Status").First
                'End Using

                memoryMap = (From mem In m_objPlcLineThread.m_memoryMap
                                            Where mem.DESCR = "PC Status").First


                Dim addressTemp As Integer = CInt(memoryMap.OFFSET) + m_objPlcLineThread.m_objDbMan.PLC_GetAddress(Me.m_objPLCSetting.PLC_ID)



                m_objPlc.WriteMemoryWordInteger(PLC_MEMORY_TYPE _
                                                               , addressTemp _
                                                                        , {int2sent} _
                                                                        , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)

                'm_clsLogger.AppendLog(Me.GetType.Name, "ReplyToPlc()", "[" & CStr(CInt(memoryMap.OFFSET) + PLC_GetAddress(Me.m_objPLCSetting.PLC_ID)) & "]" & int2sent, "PLC")
                m_clsLogger.AppendLog(Me.GetType.Name, "ReplyToPlc", "[" & CStr(addressTemp) & "] <= " & int2sent, "PLC")

                blnSent = True
            Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
                m_clsLogger.AppendLog(Me.GetType.Name, "ReplyToPlc=" & int2sent, "Time Out", "Error")
            Catch ex As Exception
                m_clsLogger.AppendLog(Me.GetType.Name, "ReplyToPlc=" & int2sent, ex)
            End Try

            Me.m_objPlcLineThread.ReportProgress(0)
            Threading.Thread.Sleep(TimeSpan.FromSeconds(1))
        End While

        'm_clsLogger.AppendLog(Me.GetType.Name, "ReplyToPlc=" & int2sent, "End Function", "Info")
        m_clsLogger.AppendLog(Me.GetType.Name, "ReplyToPlc=" & int2sent, "ReplyToPlc()", "Info")


    End Sub

    'Public Sub ReplyZeroToPLC()
    '    Dim blnSent As Boolean = False
    '    While Not blnSent
    '        Try
    '            'm_objPlc.WriteMemoryWordInteger(m_objLineSetting.WriteStatusMemoryType _
    '            '                                , m_objLineSetting.WriteStatusAddress _
    '            '                                , {PLC_WRITE_STATUS_WAITING} _
    '            '                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)


    '            Dim memoryMap As PLC_MEMORY_MAP = (From mem In dbEntities.PLC_MEMORY_MAP
    '                                         Where mem.DESCR = "PC Status").First


    '            m_objPlc.WriteMemoryWordInteger(PLC_MEMORY_TYPE _
    '                                                           , CInt(memoryMap.OFFSET) + PLC_GetAddress(Me.m_objPLCSetting.PLC_ID) _
    '                                                                    , {0} _
    '                                                                    , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)



    '            blnSent = True
    '        Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
    '            m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC", "Time Out", "Error")
    '        Catch ex As Exception
    '            m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC", ex)
    '        End Try

    '        Me.m_objPlcLineThread.ReportProgress(0)
    '        Threading.Thread.Sleep(TimeSpan.FromSeconds(1))
    '    End While
    '    m_clsLogger.AppendLog(Me.GetType.Name, "ReplyZeroToPLC", "End Function", "Info")
    'End Sub

    'Public Sub Reply1ToPlc()
    '    Dim blnSent As Boolean = False
    '    While Not blnSent
    '        Try
    '            'm_objPlc.WriteMemoryWordInteger(m_objLineSetting.WriteStatusMemoryType _
    '            '                                , m_objLineSetting.WriteStatusAddress _
    '            '                                , {PLC_WRITE_STATUS_PROCESSING} _
    '            '                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)

    '            Dim memoryMap As PLC_MEMORY_MAP = (From mem In dbEntities.PLC_MEMORY_MAP
    '                                         Where mem.DESCR = "PC Status").First


    '            m_objPlc.WriteMemoryWordInteger(PLC_MEMORY_TYPE _
    '                                                           , CInt(memoryMap.OFFSET) + PLC_GetAddress(Me.m_objPLCSetting.PLC_ID) _
    '                                                                    , {1} _
    '                                                                    , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)


    '            blnSent = True
    '        Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
    '            m_clsLogger.AppendLog(Me.GetType.Name, "Reply1ToPlc", "Time Out", "Error")
    '        Catch ex As Exception
    '            m_clsLogger.AppendLog(Me.GetType.Name, "Reply1ToPlc", ex)
    '        End Try

    '        Me.m_objPlcLineThread.ReportProgress(0)
    '        Threading.Thread.Sleep(TimeSpan.FromSeconds(1))
    '    End While

    '    m_clsLogger.AppendLog(Me.GetType.Name, "Reply1ToPlc", "End Function", "Info")
    'End Sub

    'Public Sub Reply2ToPlc()
    '    Dim blnSent As Boolean = False
    '    While Not blnSent
    '        Try
    '            'm_objPlc.WriteMemoryWordInteger(m_objLineSetting.WriteStatusMemoryType _
    '            '                                , m_objLineSetting.WriteStatusAddress _
    '            '                                , {PLC_WRITE_STATUS_COMPLETE} _
    '            '                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)

    '            Dim memoryMap As PLC_MEMORY_MAP = (From mem In dbEntities.PLC_MEMORY_MAP
    '                                         Where mem.DESCR = "PC Status").First


    '            m_objPlc.WriteMemoryWordInteger(PLC_MEMORY_TYPE _
    '                                                           , CInt(memoryMap.OFFSET) + PLC_GetAddress(Me.m_objPLCSetting.PLC_ID) _
    '                                                                    , {2} _
    '                                                                    , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)


    '            blnSent = True
    '        Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
    '            m_clsLogger.AppendLog(Me.GetType.Name, "Reply2ToPlc", "Time Out", "Error")
    '        Catch ex As Exception
    '            m_clsLogger.AppendLog(Me.GetType.Name, "Reply2ToPlc", ex)
    '        End Try

    '        Me.m_objPlcLineThread.ReportProgress(0)
    '        Threading.Thread.Sleep(TimeSpan.FromSeconds(1))
    '    End While

    '    m_clsLogger.AppendLog(Me.GetType.Name, "Reply2ToPlc", "End Function", "Info")
    'End Sub

    'Public Sub Reply3ToPlc()
    '    Dim blnSent As Boolean = False
    '    While Not blnSent
    '        Try
    '            'm_objPlc.WriteMemoryWordInteger(m_objLineSetting.WriteStatusMemoryType _
    '            '                                , m_objLineSetting.WriteStatusAddress _
    '            '                                , {PLC_WRITE_STATUS_PROCESSING} _
    '            '                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)

    '            Dim memoryMap As PLC_MEMORY_MAP = (From mem In dbEntities.PLC_MEMORY_MAP
    '                                         Where mem.DESCR = "PC Status").First


    '            m_objPlc.WriteMemoryWordInteger(PLC_MEMORY_TYPE _
    '                                                           , CInt(memoryMap.OFFSET) + PLC_GetAddress(Me.m_objPLCSetting.PLC_ID) _
    '                                                                    , {3} _
    '                                                                    , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)


    '            blnSent = True
    '        Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
    '            m_clsLogger.AppendLog(Me.GetType.Name, "Reply1ToPlc", "Time Out", "Error")
    '        Catch ex As Exception
    '            m_clsLogger.AppendLog(Me.GetType.Name, "Reply1ToPlc", ex)
    '        End Try

    '        Me.m_objPlcLineThread.ReportProgress(0)
    '        Threading.Thread.Sleep(TimeSpan.FromSeconds(1))
    '    End While

    '    m_clsLogger.AppendLog(Me.GetType.Name, "Reply1ToPlc", "End Function", "Info")
    'End Sub

    'Public Sub Reply4ToPlc()
    '    Dim blnSent As Boolean = False
    '    While Not blnSent
    '        Try
    '            'm_objPlc.WriteMemoryWordInteger(m_objLineSetting.WriteStatusMemoryType _
    '            '                                , m_objLineSetting.WriteStatusAddress _
    '            '                                , {PLC_WRITE_STATUS_PROCESSING} _
    '            '                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)

    '            Dim memoryMap As PLC_MEMORY_MAP = (From mem In dbEntities.PLC_MEMORY_MAP
    '                                         Where mem.DESCR = "PC Status").First


    '            m_objPlc.WriteMemoryWordInteger(PLC_MEMORY_TYPE _
    '                                                           , CInt(memoryMap.OFFSET) + PLC_GetAddress(Me.m_objPLCSetting.PLC_ID) _
    '                                                                    , {4} _
    '                                                                    , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)


    '            blnSent = True
    '        Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
    '            m_clsLogger.AppendLog(Me.GetType.Name, "Reply1ToPlc", "Time Out", "Error")
    '        Catch ex As Exception
    '            m_clsLogger.AppendLog(Me.GetType.Name, "Reply1ToPlc", ex)
    '        End Try

    '        Me.m_objPlcLineThread.ReportProgress(0)
    '        Threading.Thread.Sleep(TimeSpan.FromSeconds(1))
    '    End While

    '    m_clsLogger.AppendLog(Me.GetType.Name, "Reply1ToPlc", "End Function", "Info")
    'End Sub

    'Public Function SendSyncTime() As Boolean
    '    Try
    '        'Dim datSync As DateTime = Now
    '        'Dim aintSync(2) As Integer

    '        'aintSync(0) = CInt(datSync.ToString("yyMM"))
    '        'aintSync(1) = CInt(datSync.ToString("ddHH"))
    '        'aintSync(2) = CInt(datSync.ToString("mmss"))

    '        'm_objPlc.WriteMemoryWordInteger(m_objLineSetting.WriteSyncMemoryType _
    '        '                               , m_objLineSetting.WriteSyncAddress _
    '        '                                , aintSync _
    '        '                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)

    '        Dim astrSyncFormat(2) As String
    '        astrSyncFormat(0) = "mmss"
    '        astrSyncFormat(1) = "ddHH"
    '        astrSyncFormat(2) = "yyMM"
    '        m_objPlc.WriteMemoryWordSyncDateTime(m_objPLCSetting.WriteSyncMemoryType _
    '                                       , m_objPLCSetting.WriteSyncAddress _
    '                                        , astrSyncFormat _
    '                                        , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)

    '        m_clsLogger.AppendLog(Me.GetType.Name, "SendSyncTime", "Sync Time: ", "Info")
    '        Return True
    '    Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
    '        m_clsLogger.AppendLog(Me.GetType.Name, "SendSyncTime", "Sync Time Failed", "Error")
    '        Return False
    '    Catch ex As Exception
    '        m_clsLogger.AppendLog(Me.GetType.Name, "SendSyncTime", "Sync Time Failed", "Error")
    '        Return False
    '    End Try
    'End Function

    Protected Function IsNewPlcMessage(ByVal readPlcStatus As Integer) As Boolean
        If readPlcStatus <> Me.m_intPreviousPlcStatusCode Then
            Me.m_intPreviousPlcStatusCode = readPlcStatus
            Return True
        Else
            Return False
        End If
    End Function

#End Region

End Class
