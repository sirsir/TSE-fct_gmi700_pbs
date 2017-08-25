Public Class clsStartAllWorker
#Region "Attribute"
    'Private m_bkgList As List(Of clsPlcCommunication)
    Private m_bkgList As List(Of System.ComponentModel.BackgroundWorker)
    Private m_objLogger As clsLogger
#End Region

#Region "Constructor"
    Public Sub New()
        'Me.m_bkgList = New List(Of clsPlcCommunication)
        Me.m_bkgList = New List(Of System.ComponentModel.BackgroundWorker)
        m_objLogger = New clsLogger
    End Sub
#End Region

#Region "Method"
    Public Sub StartAll()
        Try
            'Dim aobjLine As List(Of clsLineSetting) = clsLineSetting.FindAll


            ''modDatabase.Init()
            ''For Each objLine As clsLineSetting In aobjLine
            m_objLogger.AppendLog("StartAll", "Info")
            Using dbEntities As fct_gmi700_pbsEntities = New fct_gmi700_pbsEntities()
                m_objLogger.AppendLog("New Entity", "Info")
                For Each plc In dbEntities.PLC_MASTER
                    Dim objPlc As New clsPlcCommunication(plc)
                    m_objLogger.AppendLog("New clsPlcComm", "Info")
                    With objPlc
                        .WorkerReportsProgress = True
                        .WorkerSupportsCancellation = True
                        .RunWorkerAsync()
                    End With
                    Me.m_bkgList.Add(objPlc)
                Next
            End Using


            'Dim bwkDirWatcher As clsDirWatcher = New clsDirWatcher(g_strFtpPathLocal)
            Dim bwkDirWatcher As clsDirWatcherLocalMode = New clsDirWatcherLocalMode(g_strLocalPathForDownload)
            With bwkDirWatcher
                .WorkerReportsProgress = True
                .WorkerSupportsCancellation = True
                .RunWorkerAsync()
            End With
            Me.m_bkgList.Add(bwkDirWatcher)

        Catch ex As Exception
            m_objLogger.AppendLog(ex)
        End Try
    End Sub

    Public Sub StopAll()

        For Each objPlc As System.ComponentModel.BackgroundWorker In m_bkgList
            Try
                If objPlc.IsBusy AndAlso Not objPlc.CancellationPending Then objPlc.CancelAsync()
            Catch ex As Exception
                m_objLogger.AppendLog(ex)
            End Try
        Next

        'For Each objPlc As clsPlcCommunication In m_bkgList
        '    Try
        '        If objPlc.IsBusy AndAlso Not objPlc.CancellationPending Then objPlc.CancelAsync()
        '    Catch ex As Exception
        '        m_objLogger.AppendLog(ex)
        '    End Try
        'Next

        'Try
        '    If bwkDirWatcher.IsBusy AndAlso Not bwkDirWatcher.CancellationPending Then bwkDirWatcher.CancelAsync()
        'Catch ex As Exception
        '    m_objLogger.AppendLog(ex)
        'End Try

    End Sub
#End Region

End Class
