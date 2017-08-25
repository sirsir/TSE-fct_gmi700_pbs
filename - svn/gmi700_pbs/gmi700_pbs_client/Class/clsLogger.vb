Public Class clsLogger

#Region "Constant"
    Public Const LOG_PATH As Object = ".\log"
#End Region

#Region "Attribure"
    Private m_strSubSystem As String
#End Region

#Region "Construtor"
    Sub New(Optional ByVal strSubSystem As String = "")
        If strSubSystem = String.Empty Then
            strSubSystem = My.Application.Info.ProductName
        End If

        m_strSubSystem = strSubSystem.ToLower

        reset()
    End Sub

    Public Sub reset()
    End Sub
#End Region

#Region "Method"
    '-----------------------------------------------------------------------------------------------------------------------
    '   Function Name   : AppendLog
    '   Purpose         : Append log text to log file
    '   Created         : Danzler Alan Smith 11/08/2008
    '   Modified        :
    '
    '   Syntax          : strLogText - Log text
    '
    '   Example         : objLogger.AppendLog("TSE is very best.")
    '-----------------------------------------------------------------------------------------------------------------------
    Public Sub AppendLog(ByVal strLogText As String, ByVal sLogType As String)
        Debug.Assert(sLogType.Length > 0)
        Try


            Dim strActualFileName As String
            Dim strActualLogText As String

            'If Not My.Settings.IsDebugMode AndAlso (sLogType.ToLower = "info" Or sLogType.ToLower = "debug") Then
            '    Return
            'End If

            strActualFileName = Me.GetLogPath

            'strActualFileName = Environment.CurrentDirectory & "\" & strActualFileName
            'strActualFileName = "D:\temp\" & strActualFileName

            If My.Settings.LogPath IsNot Nothing AndAlso My.Settings.LogPath <> "" Then
                If Not IO.Directory.Exists(Me.GetLogPath) Then
                    Try
                        IO.Directory.CreateDirectory(Me.GetLogPath)
                        strActualFileName = Me.GetLogPath
                    Catch ex As Exception

                    End Try
                End If
            Else
                'strActualFileName = Me.GetLogPath
            End If

            If m_strSubSystem <> String.Empty Then
                strActualFileName &= "\" & m_strSubSystem
            End If

            Dim strActualPath As String = strActualFileName
            If Not IO.Directory.Exists(strActualFileName) Then
                My.Computer.FileSystem.CreateDirectory(strActualFileName)
            End If

            Me.DeleteOldLog(strActualPath)

            strActualFileName &= "\" & m_strSubSystem & Now.ToString("yyyyMMdd") & "_" & sLogType & ".log"

            strActualLogText = "[" & Now.ToString("yyyy/MM/dd HH:mm:ss.ffff") & "] " & strLogText & vbCrLf

            Using writer As New IO.StreamWriter(strActualFileName, True)
                writer.WriteLine(strActualLogText)
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Public Sub AppendLog(ByVal strClass As String, ByVal strFunction As String, ByVal strLogText As String, ByVal sLogType As String)
        Dim strFormat As String = String.Format("[{0}][{1}] {2}", strClass, strFunction, strLogText)
        AppendLog(strFormat, sLogType)
    End Sub


    Private Function GetLogPath() As String
        Dim strDataPath As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData)
        Dim strCompany As String = IIf(My.Application.Info.CompanyName <> "", _
                                        My.Application.Info.CompanyName, _
                                        My.Application.Info.ProductName)
        'Dim strProduct As String = System.Diagnostics.Process.GetCurrentProcess.ProcessName.Split(".")(0)
        Dim strProduct As String = My.Application.Info.ProductName
        Dim strVersion As String = My.Application.Info.Version.ToString
        Dim strDefaultPath As String = My.Computer.FileSystem.CombinePath(strDataPath & "\" & strCompany & "\" & strProduct & "\" & strVersion, LOG_PATH)
        If My.Settings.LogPath = "" Then
            Return strDefaultPath
        ElseIf Strings.Left(My.Settings.LogPath, 1) = "." Then
            Return My.Computer.FileSystem.CombinePath(strDataPath & "\" & strCompany & "\" & strProduct & "\" & strVersion, My.Settings.LogPath)
        Else
            Return My.Settings.LogPath
        End If
    End Function

    Public Sub AppendLog(ByVal objException As Exception)
        AppendLog(objException.Message, "Exception_message")
        AppendLog(objException.ToString, "Exception_stack")
    End Sub

    Public Sub AppendLog(ByVal strClass As String, ByVal strFunction As String, ByVal objException As Exception)
        AppendLog(strClass, strFunction, objException.Message, "Exception_message")
        AppendLog(strClass, strFunction, objException.ToString, "Exception_stack")
    End Sub

    Private Sub DeleteOldLog(ByVal path As String)

        If Now.Hour = 0 AndAlso Now.Minute = 0 Then
            Try
                For Each strFile As String In My.Computer.FileSystem.GetFiles(path, _
                                                                              FileIO.SearchOption.SearchTopLevelOnly, _
                                                                              "*.log")
                    If My.Computer.FileSystem.GetFileInfo(strFile).LastWriteTime < Now.AddDays(0 - My.Settings.DayToKeepTextLog) Then
                        My.Computer.FileSystem.DeleteFile(strFile)
                    End If
                Next
            Catch ex As Exception

            End Try
        End If
    End Sub

#End Region

End Class
