Imports System.Text.RegularExpressions

Module modGlobalVariable
    'Public g_strINIpath As String = "config.ini"
    'Dim myIniFile As New clsINI("config.ini")
    Dim myIniFile As clsINI = New clsINI()

    'Public g_strFtpFullPath As String = "ftp://192.168.1.2/BALL_TEST"
    Public g_strFtpFullPath As String
    Public g_strFtpIPAddress As String

    Public g_GmProcessId As Integer = 5

    Public g_strLocalPathForDownload As String
    Public g_strLocalPathForBackup As String

    Public g_ReceiveTimeLimit As Integer
    Public g_RetryCount As Integer


    Public g_strLocalPathForData As String

    Public g_strFtpUsername As String
    Public g_strFtpPassword As String
    Public g_strFtpPath As String
    Public g_strFtpPathLocalMode As String


    'Public strFtpFilename As String

    Public clsFileSystem As clsFileSystem = New clsFileSystem()

    Private m_objLogger As clsLogger = New clsLogger("Global Variable")



    Enum SaveResultFromQRcode_RETURN_CODE
        COMPLETE
        DUPLICATE
        INVALID
        DATABASE_SAVE_ERROR
        DATABASE_CONNECTION_ERROR
        PLC_CONNECTION_ERROR
    End Enum

    Sub Initialize()

        CopyIniIfNotExist()
        'g_strFtpFullPath = "ftp://192.168.1.2/BALL_TEST"
        'g_strFtpFullPath = modINI.ReadIni(g_strINIpath, "config", "FTPpath", "")
        'g_strFtpFullPath = myIniFile.GetString("FTP Server", "path", "ftp://192.168.150.90", GetIniPath() & "\config.ini")

        'g_strFtpIPAddress = "192.168.1.2"
        'g_strFtpIPAddress = Regex.Replace(g_strFtpFullPath, "ftp://(.*?)/.*", "$1")
        g_strFtpIPAddress = myIniFile.GetString("FTP Server", "ip", "ftp://192.168.150.90", GetIniPath() & "\config.ini")

        'g_strFtpPath = "/BALL_TEST"
        'g_strFtpPath = Regex.Replace(g_strFtpFullPath, "ftp://.*?(/.*)", "$1")
        g_strFtpPath = myIniFile.GetString("FTP Server", "dir", ".", GetIniPath() & "\config.ini")
        g_strFtpPathLocalMode = myIniFile.GetString("FTP Server", "dirLocalMode", "E:\Protocal\FTPupload", GetIniPath() & "\config.ini")

        g_strFtpPathLocalMode = IIf(Right(g_strFtpPathLocalMode, 1) = "\", g_strFtpPathLocalMode, g_strFtpPathLocalMode & "\")

        'g_strFtpPathLocal = "C:\AnEasyBrowseDir\backgroundWorkerTest"
        'g_strFtpPathLocalRecorded = "C:\AnEasyBrowseDir\backgroundWorkerTestRecorded"

        g_strLocalPathForDownload = myIniFile.GetString("Local Path", "DownloadFolder", "E:\FTP\Download", GetIniPath() & "\config.ini")
        g_strLocalPathForBackup = myIniFile.GetString("Local Path", "BackupFolder", "E:\FTP\Backup", GetIniPath() & "\config.ini")
        g_strLocalPathForData = myIniFile.GetString("Local Path", "DataFolder", "E:\FTP\Data", GetIniPath() & "\config.ini")

        'g_strFtpUsername = "sa"
        'g_strFtpPassword = "d4168f"
        g_strFtpUsername = myIniFile.GetString("FTP Server", "username", "laser", GetIniPath() & "\config.ini")
        g_strFtpPassword = myIniFile.GetString("FTP Server", "password", "VotanA-221", GetIniPath() & "\config.ini")


        g_ReceiveTimeLimit = CInt(myIniFile.GetString("PLC", "ReceiveTimeLimit", "1000", GetIniPath() & "\config.ini"))
        g_RetryCount = CInt(myIniFile.GetString("PLC", "RetryCount", "1", GetIniPath() & "\config.ini"))

        g_GmProcessId = CInt(myIniFile.GetString("Process", "GmProcessId", "1000", GetIniPath() & "\config.ini"))


        m_objLogger.AppendLog("modGlobal", "Initialize", "g_strFtpIPAddress=" & g_strFtpIPAddress, "Info")
        m_objLogger.AppendLog("modGlobal", "Initialize", "g_strFtpPath=" & g_strFtpPath, "Info")
        m_objLogger.AppendLog("modGlobal", "Initialize", "g_strFtpPathLocal=" & g_strLocalPathForDownload, "Info")
        m_objLogger.AppendLog("modGlobal", "Initialize", "g_strFtpPathLocalRecorded=" & g_strLocalPathForBackup, "Info")
        m_objLogger.AppendLog("modGlobal", "Initialize", "g_strLocalPathForData=" & g_strLocalPathForData, "Info")
        m_objLogger.AppendLog("modGlobal", "Initialize", "g_strFtpUsername=" & g_strFtpUsername, "Info")
        m_objLogger.AppendLog("modGlobal", "Initialize", "g_strFtpPassword=" & g_strFtpPassword, "Info")
        m_objLogger.AppendLog("modGlobal", "Initialize", "g_ReceiveTimeLimit=" & g_ReceiveTimeLimit, "Info")
        m_objLogger.AppendLog("modGlobal", "Initialize", "g_RetryCount=" & g_RetryCount, "Info")
        m_objLogger.AppendLog("modGlobal", "Initialize", "g_GmProcessId=" & g_GmProcessId, "Info")

        'strFtpFilename = ""
    End Sub

    Private Function GetIniPath() As String
        Dim strDataPath As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData)
        Dim strCompany As String = IIf(My.Application.Info.CompanyName <> "", _
                                        My.Application.Info.CompanyName, _
                                        My.Application.Info.ProductName)
        'Dim strProduct As String = System.Diagnostics.Process.GetCurrentProcess.ProcessName.Split(".")(0)
        Dim strProduct As String = My.Application.Info.ProductName
        Dim strVersion As String = My.Application.Info.Version.ToString
        Dim strDefaultPath As String = strDataPath & "\" & strCompany & "\" & strProduct & "\" & strVersion
        Return strDefaultPath
    End Function

    Public Sub CopyIniIfNotExist()
        If Not My.Computer.FileSystem.DirectoryExists(GetIniPath()) Then
            My.Computer.FileSystem.CreateDirectory(GetIniPath())
        End If

        If Not My.Computer.FileSystem.FileExists(GetIniPath() & "\config.ini") Then
            My.Computer.FileSystem.CopyFile(My.Computer.FileSystem.CurrentDirectory & "\config.ini", GetIniPath() & "\config.ini")
        End If
    End Sub

End Module
