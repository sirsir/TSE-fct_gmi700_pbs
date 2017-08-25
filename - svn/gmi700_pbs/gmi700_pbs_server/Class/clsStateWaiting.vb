Public Class clsStateWaiting
    Inherits clsStateBase

    Public Sub New(ByRef plcLineThread As clsPlcCommunication)
        MyBase.New(plcLineThread)
    End Sub

    Public Overrides Sub Initial(Optional inObj As Object = Nothing)
        'TODO: Implement
    End Sub

    Public Overrides Sub Process()
        'TODO: Implement
        m_clsLogger.AppendLog(Me.GetType.Name, "Process", "Start Function", "Info")
        If Not m_blnWorkDone Then
            'Me.ReplyZeroToPLC()
            Me.ReplyToPlc(0)
            Me.m_blnWorkDone = True
        Else
            m_clsLogger.AppendLog(Me.GetType.Name, "Process", "m_blnWorkDone = True", "Info")
            Dim intReadPlcStatus = Me.ReadPlcStatus()
            If intReadPlcStatus = PLC_READ_STATUS_REQUEST Then
                m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "Read Request status", "Info")
                Me.m_objPlcLineThread.SetState(Me.m_objPlcLineThread.m_clsStateProcessing)
                m_objPlcLineThread.ForceToProcessCurrentState()
                m_blnWorkDone = True
            ElseIf intReadPlcStatus = PLC_READ_STATUS_SAME_STATUS Then
                'DO NOTHING
            ElseIf intReadPlcStatus = PLC_READ_STATUS_WAITING Then
                'DO NOTHING
            ElseIf intReadPlcStatus = PLC_READ_STATUS_READ_FAILED Then
                'DO NOTHING
            Else
                'm_clsLogger.AppendLog(Me.GetType.Name, "Process", "Read PLC Status : Other, Go to error state", "Error")
                'Me.m_objPlcLineThread.SetState(Me.m_objPlcLineThread.m_clsStateError)
                'Me.m_objPlcLineThread.ForceToProcessCurrentState()
            End If
        End If
        m_clsLogger.AppendLog(Me.GetType.Name, "Process", "End Function", "Info")
    End Sub

    'Private Function ReadPlcStatus() As Integer
    '    Dim intReadStatus As Integer

    '    m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "Start Function", "Info")

    '    Try
    '        'intReadStatus = m_objPlc.ReadMemoryWordInteger(m_objLineSetting.ReadStatusMemoryType _
    '        '                                               , m_objLineSetting.ReadStatusAddress _
    '        '                                               , m_objLineSetting.ReadStatusLength _
    '        '                                               , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)

    '        Dim memoryMap As PLC_MEMORY_MAP = (From mem In dbEntities.PLC_MEMORY_MAP
    '                                         Where mem.DESCR = "PLC Status").First


    '        intReadStatus = m_objPlc.ReadMemoryWordInteger(PLC_MEMORY_TYPE _
    '                                                       , CInt(memoryMap.OFFSET) + PLC_OFFSET(m_intLineNo) _
    '                                                                , memoryMap.NO_OF_WORDS _
    '                                                                , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)

    '    Catch ex As Exception
    '        m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", ex)
    '        Return PLC_READ_STATUS_READ_FAILED
    '    End Try

    '    If Not IsNewPlcMessage(intReadStatus) Then
    '        Return PLC_READ_STATUS_SAME_STATUS
    '    End If

    '    m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "End Function", "Info")
    '    Return intReadStatus
    'End Function

    

End Class
