Public Class clsStateError
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
            'Me.Reply4ToPLC()
            Me.ReplyToPlc(int_for_ReplyToPlc)
            Me.m_blnWorkDone = True
        Else
            m_clsLogger.AppendLog(Me.GetType.Name, "Process", "m_blnWorkDone = True", "Info")
            Dim intReadPlcStatus = Me.ReadPlcStatus()
            If intReadPlcStatus = PLC_READ_STATUS_WAITING Then
                m_objPlcLineThread.m_objDbMan.ClearPreviousProcessTestResult(m_objPLCSetting, m_objPlc)
                m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "Read Waiting status", "Info")
                Me.m_objPlcLineThread.SetState(Me.m_objPlcLineThread.m_clsStateWaiting)
                m_blnWorkDone = True
            ElseIf intReadPlcStatus = PLC_READ_STATUS_SAME_STATUS Then
                'DO NOTHING
            ElseIf intReadPlcStatus = PLC_READ_STATUS_READ_FAILED Then
                'DO NOTHING
            ElseIf intReadPlcStatus = PLC_READ_STATUS_REQUEST Then
                'DO NOTHING
            Else
                'm_clsLogger.AppendLog(Me.GetType.Name, "Process", "Read PLC Status : Other, Go to error state", "Error")
                'Me.m_objPlcLineThread.SetState(Me.m_objPlcLineThread.m_clsStateError)
                'Me.m_objPlcLineThread.ForceToProcessCurrentState()
            End If
        End If
        m_clsLogger.AppendLog(Me.GetType.Name, "Process", "End Function", "Info")
    End Sub

    'Private Sub Reply4ToPLC()
    'Dim blnSent As Boolean = False
    'While Not blnSent
    '    Try
    '        m_objPlc.WriteMemoryWordInteger(m_objLineSetting.WriteStatusMemoryType _
    '                                        , m_objLineSetting.WriteStatusAddress _
    '                                        , {PLC_WRITE_STATUS_ERROR} _
    '                                        , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)
    '        blnSent = True
    '    Catch exCon As OMRON.Compolet.SYSMAC.SysmacIOException
    '        m_clsLogger.AppendLog(Me.GetType.Name, "Reply4ToPLC", "Time Out", "Error")
    '    Catch ex As Exception
    '        m_clsLogger.AppendLog(Me.GetType.Name, "Reply4ToPLC", ex)
    '    End Try

    '    Me.m_objPlcLineThread.ReportProgress(0)
    '    Threading.Thread.Sleep(TimeSpan.FromSeconds(1))
    'End While
    'm_clsLogger.AppendLog(Me.GetType.Name, "Reply4ToPLC", "End Function", "Info")
    'End Sub

    'Private Function ReadPlcStatus() As Integer
    '    Dim intReadStatus As Integer

    '    m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcStatus", "Start Function", "Info")

    '    Try
    '        intReadStatus = m_objPlc.ReadMemoryWordInteger(m_objLineSetting.ReadStatusMemoryType _
    '                                                       , m_objLineSetting.ReadStatusAddress _
    '                                                       , m_objLineSetting.ReadStatusLength _
    '                                                       , OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
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
