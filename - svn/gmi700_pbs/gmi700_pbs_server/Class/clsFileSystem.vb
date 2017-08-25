
Public Class clsFileSystem
#Region "Method"
    Public Shared Function ValidateFileName( _
            ByVal sFileName As String, _
            Optional ByVal bAllowSpace As Boolean = False, _
            Optional ByVal nLimit As Integer = -1) As String
        Dim sSpc As String = IIf(bAllowSpace, "_", " ")
        ValidateFileName = sFileName _
                .Replace("/", "_") _
                .Replace("\", "_") _
                .Replace(":", "_") _
                .Replace("*", "_") _
                .Replace("?", "_") _
                .Replace("""", "_") _
                .Replace("<", "_") _
                .Replace(">", "_") _
                .Replace("|", "_") _
                .Replace("""", "_") _
                .Replace("'", "_") _
                .Replace("[", "_") _
                .Replace("]", "_") _
                .Replace(sSpc, "_") _
                .Replace(vbTab, "_") _
                .Replace(vbCr, "_") _
                .Replace(vbLf, "_")

        If nLimit <> -1 AndAlso ValidateFileName.Length > nLimit Then ValidateFileName = ValidateFileName.Substring(0, nLimit)

    End Function
















    Public Shared Function FullPath(ByVal relativePath As String) As String
        If Strings.Left(relativePath, 1) = "." Then
            FullPath = _
            My.Computer.FileSystem.CombinePath(My.Application.Info.DirectoryPath, relativePath)
        Else
            FullPath = relativePath
        End If
    End Function

    Public Shared Function TryDeleteFile(ByVal file As String) As Boolean
        Try
            My.Computer.FileSystem.DeleteFile(file)
            TryDeleteFile = True
        Catch ex As Exception
            TryDeleteFile = False
        End Try
    End Function

#End Region
End Class
