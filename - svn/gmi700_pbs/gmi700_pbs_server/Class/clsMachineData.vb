Public Class clsMachineData

    Private m_lstrMachineData As List(Of String)
    'Private m_objLineSetting As clsLineSetting
    Private m_objLineSetting As PLC_MASTER
    Private m_strWritePath As String
    Private m_strTempPath As String
    Private m_strWriteHeader As String
    Private m_strWriteData As String
    Private m_strInsertValues As String

    Private m_strSerial As String
    Private m_strMode As String
    Private m_strMc As String
    Private m_strLotNo As String
    Private m_strFileName As String
    Private m_datDateTime As DateTime
    Private m_strStatus As String
    Private m_objLogger As clsLogger

    Private Const FILENAME_DATETIME_FORMAT As String = "yyyy-MM-dd_HH-mm-ss-ffff"

    Public Sub New(ByVal lineSetting As PLC_MASTER, ByVal logger As clsLogger)
        m_lstrMachineData = New List(Of String)
        m_objLineSetting = lineSetting
        m_objLogger = logger
        Me.Init()
    End Sub

    Public Sub Init()
        'm_lstrMachineData.Clear()
        'm_strWritePath = ExportPath()
        'm_strTempPath = TempPath()
    End Sub

    'Private Function ExportPath() As String
    '    If m_objLineSetting.WritePath.StartsWith(".\") Then
    '        Return My.Computer.FileSystem.CombinePath(m_objLineSetting.RootFolder, m_objLineSetting.WritePath)
    '    Else
    '        Return m_objLineSetting.WritePath
    '    End If
    'End Function

    'Private Function TempPath() As String
    '    If m_objLineSetting.WritePath.StartsWith(".\") Then
    '        Return My.Computer.FileSystem.CombinePath(m_objLineSetting.RootTempFolder, m_objLineSetting.WritePath)
    '    Else
    '        Return m_objLineSetting.WritePath
    '    End If
    'End Function

    Public Sub ExtractMachineData(ByVal strInput As String, ByVal aintInput() As Integer)
        'Dim strHeader As String = ""
        'Dim strCsvData As String = ""
        'Dim strValue As String = ""
        'Dim strInsertValues As String = Me.GetInsertValues("", "", "")

        'For Each objField As clsField In m_objLineSetting.Fields
        '    If objField.IsForWrite Then
        '        If m_strWriteHeader = "" Then
        '            strHeader = strHeader & objField.FieldName & ","
        '        End If

        '        strValue = objField.ExtractField(strInput, aintInput)
        '        strCsvData &= strValue & ","

        '        strInsertValues = Me.GetInsertValues(strValue, objField.ReplacePattern, strInsertValues)
        '    End If
        'Next
        'm_strInsertValues = Me.SetDefaultRemainingInsertValues(strInsertValues)
        'If m_strWriteHeader = "" AndAlso strHeader <> "" AndAlso strHeader.Substring(strHeader.Length - 1) = "," Then
        '    strHeader = strHeader.Remove(strHeader.Length - 1)
        '    m_strWriteHeader = strHeader
        'End If

        'If strCsvData <> "" AndAlso strCsvData.Substring(strCsvData.Length - 1) = "," Then
        '    strCsvData = strCsvData.Remove(strCsvData.Length - 1)
        'End If
        'm_strWriteData = strCsvData

        'm_strSerial = m_objLineSetting.FieldSerial.ExtractField(strInput, aintInput)
        'm_strMode = m_objLineSetting.FieldMode.ExtractField(strInput, aintInput)
        'm_strMc = m_objLineSetting.FieldMc.ExtractField(strInput, aintInput)
        'm_strLotNo = m_objLineSetting.FieldLotNo.ExtractField(strInput, aintInput)
        'm_strFileName = m_objLineSetting.FieldFileName.ExtractField(strInput, aintInput)
        'm_datDateTime = m_objLineSetting.FieldDateTime.ExtractDateTime(aintInput)
        'm_strStatus = m_objLineSetting.FieldStatus.ExtractField(strInput, aintInput)

    End Sub

    Private Function GetFileOutputPath() As String
        Dim strTemp As String = My.Computer.FileSystem.CombinePath(m_strWritePath, m_datDateTime.ToString("yyyyMMdd"))
        strTemp = My.Computer.FileSystem.CombinePath(strTemp, m_strLotNo)
        Return strTemp
    End Function

    Private Function GetFileTempOutputPath() As String
        Dim strTemp As String = My.Computer.FileSystem.CombinePath(m_strTempPath, m_datDateTime.ToString("yyyyMMdd"))
        strTemp = My.Computer.FileSystem.CombinePath(strTemp, m_strLotNo)
        Return strTemp
    End Function

    Public Sub WriteDataToFile()
        If Not My.Computer.FileSystem.DirectoryExists(Me.GetFileOutputPath) Then
            My.Computer.FileSystem.CreateDirectory(Me.GetFileOutputPath)
        End If

        Dim strOutFullPath As String = My.Computer.FileSystem.CombinePath(Me.GetFileOutputPath, m_strMc & "_" & m_strFileName & ".csv")
        Dim blnWriteHeader As Boolean = False
        If Not My.Computer.FileSystem.FileExists(strOutFullPath) Then
            blnWriteHeader = True
        End If

        Using stWrite As New IO.StreamWriter(strOutFullPath, True)
            If blnWriteHeader Then
                stWrite.WriteLine(m_strWriteHeader)
            End If
            stWrite.WriteLine(m_strWriteData)
        End Using
    End Sub

    Public Sub WriteDataToExcel()

        'Dim strTemplatePath As String
        'strTemplatePath = My.Computer.FileSystem.CombinePath(My.Application.Info.DirectoryPath, ".\Template") _
        '                            & "\" & m_objLineSetting.LineName & ".xls"

        'If Not My.Computer.FileSystem.FileExists(strTemplatePath) Then
        '    Return
        'End If

        'If Not My.Computer.FileSystem.DirectoryExists(Me.GetFileOutputPath) Then
        '    My.Computer.FileSystem.CreateDirectory(Me.GetFileOutputPath)
        'End If

        'Dim strOutTempPath As String = My.Computer.FileSystem.CombinePath(Me.GetFileTempOutputPath, m_strMc & "_" & m_strFileName & ".xls")
        'Dim strOutFullPath As String = My.Computer.FileSystem.CombinePath(Me.GetFileOutputPath, m_strMc & "_" & m_strFileName & ".xls")

        'Dim blnWriteHeader As Boolean = False
        'If Not My.Computer.FileSystem.FileExists(strOutTempPath) Then
        '    My.Computer.FileSystem.CopyFile(strTemplatePath, strOutTempPath)
        'End If

        'Dim sql As String
        'Using MyConnection As New System.Data.OleDb.OleDbConnection _
        '                            ("provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & _
        '                            "'" & strOutTempPath & "';" & _
        '                            "Extended Properties=""Excel 8.0;HDR=YES""")
        '    MyConnection.Open()
        '    Using myCommand As New System.Data.OleDb.OleDbCommand()
        '        myCommand.Connection = MyConnection
        '        sql = GetInsertHeader() & m_strInsertValues
        '        myCommand.CommandText = sql
        '        m_objLogger.AppendLog(Me.GetType.Name, "WriteDataToExcel", "Insert Excel = " & sql, "Info")
        '        myCommand.ExecuteNonQuery()
        '    End Using
        'End Using
        'Try
        '    My.Computer.FileSystem.CopyFile(strOutTempPath, strOutFullPath, True)
        'Catch ex As Exception
        '    m_objLogger.AppendLog(Me.GetType.Name, "WriteDataToExcel", "Copy to Output Foldel Failed: " & ex.Message, "Error")
        'End Try
    End Sub

    Public Sub CopyImage(ByVal blnHasImage As Boolean)
        'If Not My.Computer.FileSystem.DirectoryExists(Me.GetFileOutputPath) Then
        '    My.Computer.FileSystem.CreateDirectory(Me.GetFileOutputPath)
        'End If

        'Dim strOutFullPath As String
        'Dim intFileFound As Integer = 0

        'If blnHasImage Then
        '    Dim astr As System.Collections.ObjectModel.ReadOnlyCollection(Of String)
        '    astr = My.Computer.FileSystem.GetFiles(m_objLineSetting.CopyPath, FileIO.SearchOption.SearchTopLevelOnly, m_objLineSetting.CopyWildCard)
        '    For Each strFile As String In astr
        '        Dim fInfo As System.IO.FileInfo = My.Computer.FileSystem.GetFileInfo(strFile)

        '        Dim datFile As DateTime = Me.ExtractDateFromFilename(fInfo.Name)
        '        Dim intRunning As Integer
        '        Dim blnMoveSuccess As Boolean = False

        '        If m_datDateTime.Subtract(datFile).TotalDays > m_objLineSetting.CopyPurgePeriodDay Then
        '            intRunning = 0
        '            Dim strPurgeFullPath As String = My.Computer.FileSystem.CombinePath(m_objLineSetting.CopyPurgeOldPath, fInfo.Name)
        '            While Not blnMoveSuccess AndAlso intRunning < 5
        '                Try
        '                    My.Computer.FileSystem.MoveFile(strFile, strPurgeFullPath)
        '                    blnMoveSuccess = True
        '                Catch ex As Exception
        '                    intRunning += 1
        '                    Threading.Thread.Sleep(100)
        '                End Try
        '            End While

        '            If blnMoveSuccess Then
        '                m_objLogger.AppendLog(Me.GetType.Name, "CopyImage", "Purge Success: " & strFile & " to " & strPurgeFullPath, "Info")
        '                Continue For
        '            Else
        '                m_objLogger.AppendLog(Me.GetType.Name, "CopyImage", "Purge Failed: " & strFile, "Info")
        '            End If

        '        End If

        '        If Math.Abs(m_datDateTime.Subtract(datFile).TotalMilliseconds) > m_objLineSetting.CopyPeriodMilliSec Then
        '            Continue For
        '        End If

        '        intFileFound += 1

        '        Dim strOutFileName As String = Me.GetFileName(m_datDateTime, Me.ExtractDateFromFilename(fInfo.Name))

        '        strOutFullPath = My.Computer.FileSystem.CombinePath(Me.GetFileOutputPath, strOutFileName & fInfo.Extension)
        '        intRunning = 1
        '        While My.Computer.FileSystem.FileExists(strOutFullPath)
        '            m_objLogger.AppendLog(Me.GetType.Name, "CopyImage", "Duplicate " & strOutFullPath & "found", "Info")
        '            strOutFullPath = My.Computer.FileSystem.CombinePath(Me.GetFileOutputPath _
        '                                                    , strOutFileName & "(" & intRunning & ")" & fInfo.Extension)
        '            intRunning += 1
        '        End While

        '        intRunning = 0
        '        blnMoveSuccess = False
        '        While Not blnMoveSuccess AndAlso intRunning < 10
        '            Try
        '                My.Computer.FileSystem.MoveFile(strFile, strOutFullPath)
        '                blnMoveSuccess = True
        '            Catch ex As Exception
        '                intRunning += 1
        '                Threading.Thread.Sleep(100)
        '            End Try
        '        End While
        '        If blnMoveSuccess Then
        '            m_objLogger.AppendLog(Me.GetType.Name, "CopyImage", "Copy Success: " & strFile & " to " & strOutFullPath, "Info")
        '        Else
        '            m_objLogger.AppendLog(Me.GetType.Name, "CopyImage", "Copy Failed: " & strFile, "Info")
        '        End If
        '    Next
        'End If

        'If intFileFound = 0 Then
        '    Dim strOutFileName As String = Me.GetEmptyFileName(m_datDateTime, blnHasImage)
        '    strOutFullPath = My.Computer.FileSystem.CombinePath(Me.GetFileOutputPath, strOutFileName & ".txt")
        '    Dim intRunning As Integer = 1
        '    While My.Computer.FileSystem.FileExists(strOutFullPath)
        '        strOutFullPath = My.Computer.FileSystem.CombinePath(Me.GetFileOutputPath _
        '                                                , strOutFileName & "(" & intRunning & ")" & ".txt")
        '        intRunning += 1
        '    End While

        '    Using IO.File.Create(strOutFullPath)
        '    End Using
        '    m_objLogger.AppendLog(Me.GetType.Name, "CopyImage", "Create empty file success: " & strOutFullPath, "Info")

        'End If
    End Sub

    Private Function ExtractDateFromFilename(ByVal strFileName As String) As DateTime
        Dim strTemp As String = strFileName.Split(".")(0)
        Dim datTemp As DateTime = DateTime.ParseExact(strTemp, FILENAME_DATETIME_FORMAT, _
                                       System.Globalization.CultureInfo.InvariantCulture)
        Return datTemp
    End Function

    Private Function GetFileName(ByVal resultDate As DateTime, ByVal datFile As DateTime) As String
        Dim strTemp1 As String = ""
        'strTemp1 &= m_strFileName & "_" _
        '    & datFile.ToString(FILENAME_DATETIME_FORMAT) & "_" _
        '    & m_strMode
        strTemp1 &= m_strFileName & "_" _
            & resultDate.ToString(FILENAME_DATETIME_FORMAT) & "_" _
            & m_strMode
        Dim strTemp2 As String = ""
        If m_strSerial.Trim <> "" Then
            strTemp2 = "_" & m_strSerial
        End If

        Dim strSuffix As String = datFile.ToString(FILENAME_DATETIME_FORMAT)

        strTemp1 &= strTemp2 & "_" & m_strStatus & "_" & strSuffix
        Return strTemp1
    End Function

    Private Function GetEmptyFileName(ByVal resultDate As DateTime, ByVal blnHasImage As Boolean) As String
        Dim strTemp1 As String = ""
        strTemp1 &= m_strFileName & "_" _
            & resultDate.ToString(FILENAME_DATETIME_FORMAT) & "_" _
            & m_strMode
        Dim strTemp2 As String = ""
        If m_strSerial.Trim <> "" Then
            strTemp2 = "_" & m_strSerial
        End If

        Dim strSuffix As String = "_MISSING"
        'If m_strMode.Trim.ToUpper = "MT_2D" Then
        '    strSuffix = "_NO_IMAGE"
        'End If
        If Not blnHasImage Then
            strSuffix = "_NO_IMAGE"
        End If

        strTemp1 &= strTemp2 & "_" & m_strStatus & strSuffix
        Return strTemp1
    End Function

    Private Function GetInsertHeader() As String
        GetInsertHeader = "Insert into [DATA$] " _
                            & " ([ITEM],[SERIAL], [MODE], [MC], [E], [F], [G], [H], [I], [J], [K], [L], [M], [N], [O], [P], [Q], [R], [S], [T], [U], [V], [DATE], [TIME], [STATUS]) " _
                            & " values"
    End Function

    Private Function GetInsertValues(ByVal strValue As String, ByVal strReplacePattern As String, ByVal strLastValuesString As String) As String
        If strLastValuesString = "" Then
            strLastValuesString = "('','[SERIAL]', '[MODE]', '[MC]', '[E]', '[F]', '[G]', '[H]', '[I]', '[J]', '[K]', '[L]', '[M]', '[N]', '[O]', '[P]', '[Q]', '[R]', '[S]', '[T]', '[U]', '[V]', '[DATE]', '[TIME]', '[STATUS]')"
        End If
        If strReplacePattern = "" Then
            Return strLastValuesString
        End If

        strLastValuesString = strLastValuesString.Replace(strReplacePattern, strValue)
        Return strLastValuesString
    End Function

    Private Function SetDefaultRemainingInsertValues(ByVal strLastValuesString As String) As String
        strLastValuesString = strLastValuesString.Replace("[E]", "0")
        strLastValuesString = strLastValuesString.Replace("[F]", "0")
        strLastValuesString = strLastValuesString.Replace("[G]", "0")
        strLastValuesString = strLastValuesString.Replace("[H]", "0")
        strLastValuesString = strLastValuesString.Replace("[I]", "0")
        strLastValuesString = strLastValuesString.Replace("[J]", "0")
        strLastValuesString = strLastValuesString.Replace("[K]", "0")
        strLastValuesString = strLastValuesString.Replace("[L]", "0")
        strLastValuesString = strLastValuesString.Replace("[M]", "0")
        strLastValuesString = strLastValuesString.Replace("[N]", "0")
        strLastValuesString = strLastValuesString.Replace("[O]", "0")
        strLastValuesString = strLastValuesString.Replace("[P]", "0")
        strLastValuesString = strLastValuesString.Replace("[Q]", "0")
        strLastValuesString = strLastValuesString.Replace("[R]", "0")
        strLastValuesString = strLastValuesString.Replace("[S]", "0")
        strLastValuesString = strLastValuesString.Replace("[T]", "0")
        strLastValuesString = strLastValuesString.Replace("[U]", "0")
        strLastValuesString = strLastValuesString.Replace("[V]", "0")
        Return strLastValuesString
    End Function
End Class
