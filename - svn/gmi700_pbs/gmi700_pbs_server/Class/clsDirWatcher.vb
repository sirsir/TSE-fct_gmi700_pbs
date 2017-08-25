Public Class clsDirWatcher
    Inherits System.ComponentModel.BackgroundWorker

    Private m_objLogger As clsLogger
    Private m_objDbMan As clsDatabaseMan

    Public strDir As String

    Private ftpClient As clsFtpClient

    Public Sub New(ByVal strDir As String)
        m_objLogger = New clsLogger("DirWatcher BackgroundWorker")

        Me.WorkerSupportsCancellation = True
        Me.strDir = strDir
        m_objDbMan = New clsDatabaseMan


    End Sub




    Private Sub clsDirWatcher_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles Me.DoWork
        My.Application.ChangeCulture("en-US")
        My.Application.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd"
        My.Application.Culture.DateTimeFormat.LongDatePattern = "yyyy/MM/dd"

        Try
            ftpClient = clsFtpClient.GetInstance(g_strFtpIPAddress, g_strFtpUsername, g_strFtpPassword)
        Catch ex As Exception
            m_objLogger.AppendLog(Me.GetType.Name, "DoWork", ex)
        End Try

        While Not Me.CancellationPending
            Try

                ' backworker code
                'For Each foundFile As String In My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.SpecialDirectories.MyDocuments)

                Dim strFiles As System.Collections.ObjectModel.ReadOnlyCollection(Of String) = My.Computer.FileSystem.GetFiles(strDir)

                If strFiles.Count = 0 Then
                    DownloadFtpFile()
                Else
                    For Each foundFile As String In strFiles
                        fileOperation(foundFile)
                    Next
                End If





            Catch ex As Exception
                'm_objLogger.AppendLog(Me.GetType.Name, "DoWork", "Send KPAL Failed", "Error")
                m_objLogger.AppendLog(Me.GetType.Name, "DoWork", ex)
            End Try
            Threading.Thread.Sleep(500)
            'Loop While Not Me.CancellationPending
        End While
    End Sub

    Private Sub fileOperation(ByVal strFilepath As String)




        Dim strNewFilename As String = My.Computer.FileSystem.GetName(strFilepath)
        Dim strTempPath As String = IIf(Right(g_strFtpPath, 1) = "/", g_strFtpPath, g_strFtpPath & "/")

        Dim ftpSubPathToSave As String = ""

        'If Not (strNewFilename Like "RECORDED on *") Then
        'Dim reader As String
        'reader = My.Computer.FileSystem.ReadAllText(strFilename)
        'MsgBox(reader)



        Select Case m_objDbMan.SaveResultFromFile(strFilepath, 2, 1)
            Case clsDatabaseMan.SaveResultFromFile_RETURN_CODE.COMPLETE
                'strNewFilename = "RECORDED on " & DateTime.Now.ToString("yyyyMMdd-HHmmss - ") & strNewFilename

                'My.Computer.FileSystem.RenameFile(strFilename, strNewFilename)
                My.Computer.FileSystem.MoveFile(strFilepath, g_strLocalPathForBackup & "\" & strNewFilename, True)

                ftpSubPathToSave = "Backup/"
            Case Else
                'Move file even it has error
                My.Computer.FileSystem.MoveFile(strFilepath, g_strLocalPathForBackup & "\" & strNewFilename, True)

                ftpSubPathToSave = "BackupWithError/"

        End Select


        Try
            ''ftpClient.Delete(g_strFtpPath & "/" & strFtpFilename)


            m_objLogger.AppendLog("(ftpClient.MoveFile)Move to backup started from [" & strTempPath & strNewFilename & "] to [" & strTempPath & ftpSubPathToSave & strNewFilename & "]", "Info")
            ftpClient.MoveFile(strTempPath & strNewFilename, strTempPath & ftpSubPathToSave & strNewFilename)
            m_objLogger.AppendLog("(ftpClient.MoveFile)Move to backup completed from [" & strTempPath & strNewFilename & "] to [" & strTempPath & ftpSubPathToSave & strNewFilename & "]", "Info")


            ' FROM P'A
            'm_objLogger.AppendLog("(ftpClient.MoveFile)Move to backup started from [" & strTempPath & strNewFilename & "] to [" & strTempPath & ftpSubPathToSave & strNewFilename & "]", "Info")
            'ftpClient.MoveFile(strTempPath & strNewFilename.Substring(30), strTempPath & ftpSubPathToSave & strNewFilename)
            'm_objLogger.AppendLog("(ftpClient.MoveFile)Move to backup completed from [" & strTempPath & strNewFilename & "] to [" & strTempPath & ftpSubPathToSave & strNewFilename & "]", "Info")

        Catch ex As Exception
            'blnFoundFtpError = True
            'ftpClient.MoveFile(strTempPath & strNewFilename, strTempPath & "BackupWithError/" & strNewFilename)
            m_objLogger.AppendLog("(ftpClient.MoveFile)Move to backup failed from [" & strTempPath & strNewFilename & "] to [" & strTempPath & "BackupWithError/" & strNewFilename & "]", "Info")
            'm_lstPendingFilename.Add(strFtpFilename)
        End Try



        'End If


    End Sub

    Function DownloadFtpFile() As String
        ' This program is openning.
        ' Temp file before current case was moved to g_strFtpPathBackup.
        ' Then g_strFtpPath has 1 file from LET scan. (normal case)
        ' worst case : there are more than 1 file in g_strFtpPathBackup.

        If Not My.Computer.Network.Ping(g_strFtpIPAddress.Replace("ftp://", "")) Then
            Return "Cannot connect to server: " & g_strFtpIPAddress
        End If

        Dim strDownloadedFilename As String = String.Empty
        Dim blnIgnoreUnusedFile As Boolean = False
        'Dim blnFoundFtpError As Boolean = False
        Dim blnManyFile As Boolean = False          ' if there is only 1 file (ClearLetFiles before inspection) ::>> don't check date

        Dim strLatestFilename As String = String.Empty
        Dim dtLatestDate As Date = Date.Now

        Dim intActualFileCount As Integer = 0

        Dim strFtpFilename As String = ""

        'Check and create local directory
        If Not My.Computer.FileSystem.DirectoryExists(g_strLocalPathForDownload) Then
            My.Computer.FileSystem.CreateDirectory(g_strLocalPathForDownload)
        End If

        'Dim ftpClient As clsFtpClient = clsFtpClient.GetInstance(g_strFtpIPAddress, g_strFtpUsername, g_strFtpPassword)
        Dim lstFilename As List(Of String) = ftpClient.ListDirectory(g_strFtpPath)

        m_objLogger.AppendLog("List ftp files=>count =" & lstFilename.Count, "Info")
        While lstFilename.Contains("")
            lstFilename.Remove("")
        End While
        m_objLogger.AppendLog("List ftp files=>remove empty=>count =" & lstFilename.Count, "Info")

        'For Each strLetFilename In lstFilename
        '    If strLetFilename.Trim = String.Empty Or strLetFilename.ToLower() Like "*backup*" Then
        '        lstFilename.Remove(strLetFilename)
        '    End If
        'Next



        'For i As Integer = lstFilename.Count - 1 To 0 Step -1
        '    Dim strLetFilename As String = lstFilename.Item(i)
        '    If strLetFilename.Trim = String.Empty Or strLetFilename.ToLower() Like "*backup*" Then
        '        lstFilename.RemoveAt(i)
        '    End If

        'Next i

        Dim i As Integer = 0
        Dim strFilename As String = ""
        While i < lstFilename.Count
            strFilename = lstFilename(i)
            If strFilename.Trim = String.Empty Or strFilename.ToLower() Like "*backup*" Then
                m_objLogger.AppendLog("remove string filename =" & strFilename, "Info")
                lstFilename.RemoveAt(i)
            Else
                i += 1
                m_objLogger.AppendLog("string filename =" & strFilename & ", i = " & i, "Info")
            End If
        End While




        m_objLogger.AppendLog("List ftp files=>remove backup=>count =" & lstFilename.Count, "Info")

        If lstFilename.Count > 0 Then
            blnManyFile = True
            lstFilename.Sort()

            'Find match file
            For Each strLetFilename As String In lstFilename
                If strLetFilename.Trim = String.Empty Or strLetFilename.ToLower() Like "*backup*" Then
                    m_objLogger.AppendLog("Filename [" & strLetFilename & "] skipped.", "Info")
                    Continue For
                End If

                m_objLogger.AppendLog("Filename [" & strLetFilename & "] started.", "Info")
                strFtpFilename = strLetFilename
                Dim strTempPath As String = IIf(Right(g_strFtpPath, 1) = "/", g_strFtpPath, g_strFtpPath & "/")
                Try
                    ftpClient.Download(strTempPath & strFtpFilename, g_strLocalPathForDownload & "\" & strFtpFilename, True)
                    m_objLogger.AppendLog("Download completed from [" & strTempPath & strFtpFilename & "] to [" & g_strLocalPathForDownload & "\" & strFtpFilename & "]", "Info")
                    strDownloadedFilename = strFtpFilename
                Catch ex As Exception
                    strDownloadedFilename = "Cannot download File."
                    m_objLogger.AppendLog(strDownloadedFilename, "Info")
                    m_objLogger.AppendLog(ex)
                End Try

                'If ftpClient.Download(strTempPath & strFtpFilename, g_strFtpPathLocal & "\" & strFtpFilename, True) Then

                '    m_objLogger.AppendLog("Download completed from [" & strTempPath & strFtpFilename & "] to [" & g_strFtpPathLocal & "\" & strFtpFilename & "]", "Info")

                '    '== Move file will be done in function FileOperation
                '    'Try
                '    '    'ftpClient.Delete(g_strFtpPath & "/" & strFtpFilename)
                '    '    ftpClient.MoveFile(strTempPath & strFtpFilename, strTempPath & "Backup/" & strFtpFilename)
                '    '    m_objLogger.AppendLog("(ftpClient.MoveFile)Move to backup completed from [" & strTempPath & strFtpFilename & "] to [" & strTempPath & "Backup/" & strFtpFilename & "]", "Info")
                '    'Catch ex As Exception
                '    '    blnFoundFtpError = True
                '    '    m_objLogger.AppendLog("(ftpClient.MoveFile)Move to backup failed from [" & strTempPath & strFtpFilename & "] to [" & strTempPath & "Backup/" & strFtpFilename & "]", "Info")
                '    '    'm_lstPendingFilename.Add(strFtpFilename)
                '    'End Try

                '    strDownloadedFilename = strFtpFilename
                'Else
                '    strDownloadedFilename = "Cannot download File."
                '    m_objLogger.AppendLog(strDownloadedFilename, "Info")
                'End If
            Next
        End If
        Return strDownloadedFilename
    End Function

End Class
