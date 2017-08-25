Public Class clsINI
    'API関数定義
    '指定のIniファイルの指定キーの値を取得(文字列)
    Private Declare Function GetPrivateProfileString Lib "KERNEL32.DLL" Alias "GetPrivateProfileStringA" ( _
                                ByVal lpAppName As String, _
                                ByVal lpKeyName As String, _
                                ByVal lpDefault As String, _
                                ByVal lpReturnedString As System.Text.StringBuilder, _
                                ByVal nSize As Integer, _
                                ByVal lpFileName As String) As Integer

    '指定のIniファイルの指定キーの値を取得(整数値)
    Private Declare Function GetPrivateProfileInt Lib "KERNEL32.DLL" Alias "GetPrivateProfileIntA" ( _
                                ByVal lpAppName As String, _
                                ByVal lpKeyName As String, _
                                ByVal nDefault As Integer, _
                                ByVal lpFileName As String) As Integer

    '指定のIniファイルの指定キーの値を変更(文字列)
    Private Declare Function WritePrivateProfileString Lib "KERNEL32.DLL" Alias "WritePrivateProfileStringA" ( _
                                ByVal lpAppName As String, _
                                ByVal lpKeyName As String, _
                                ByVal lpString As String, _
                                ByVal lpFileName As String) As Integer

    '読み込みバッファサイズ
    Private Const READ_BUFF_SIZE As Integer = 1024


    '指定のIniファイルの指定キーの値を取得(文字列)
    'param  strAppName        [in]セクション名
    'param  strKeyName        [in]キー名
    'param  strDefault        [in]初期値
    'param  strFileName       [in]Iniファイル名
    'rerurn 読込んだ文字を返す
    Public Function GetString(ByVal strAppName As String, _
                              ByVal strKeyName As String, _
                              ByVal strDefault As String, _
                              ByVal strFileName As String) As String

        '読み込みバッファを生成
        Dim strGetBuff As New System.Text.StringBuilder
        strGetBuff.Capacity = READ_BUFF_SIZE

        'ファイルパスを生成
        Dim strIniPath = strFileName

        'API関数へ呼び変える
        Dim nReadSize As Integer = GetPrivateProfileString(strAppName, strKeyName, strDefault, strGetBuff, strGetBuff.Capacity, strIniPath)

        '読み込みバッファから引数へ変換
        GetString = strGetBuff.ToString

        If (GetString = strDefault) Then
            '初期値と戻り値が同じ場合、以下の可能性があるので、念の為書き込むようにする
            '・パラメータが記述されていない
            '・ファイルがない
            SetString(strAppName, strKeyName, strDefault, strFileName)
        End If

    End Function

    '指定のIniファイルの指定キーの値を取得(整数値)
    'param  strAppName        [in]セクション名
    'param  strKeyName        [in]キー名
    'param  nDefault          [in]初期値
    'param  strFileName       [in]Iniファイル名
    'rerurn 読込んだ整数値を返す
    Public Function GetInt(ByVal strAppName As String, _
                           ByVal strKeyName As String, _
                           ByVal nDefault As Integer, _
                           ByVal strFileName As String) As Integer

        'ファイルパスを生成
        Dim strIniPath = strFileName

        'API関数へ呼び変える
        GetInt = GetPrivateProfileInt(strAppName, strKeyName, nDefault, strIniPath)

        If (GetInt = nDefault) Then
            '初期値と戻り値が同じ場合、以下の可能性があるので、念の為書き込むようにする
            '・パラメータが記述されていない
            '・ファイルがない
            SetInt(strAppName, strKeyName, nDefault, strFileName)
        End If
    End Function

    '指定のIniファイルの指定キーの値を変更(文字列)
    'param  strAppName        [in]セクション名
    'param  strKeyName        [in]キー名
    'param  strParam         [in]書き込む値
    'param  strFileName       [in]Iniファイル名
    'rerurn 成功した場合True、失敗した場合はFalseを返す
    Public Function SetString(ByVal strAppName As String, _
                              ByVal strKeyName As String, _
                              ByVal strParam As String, _
                              ByVal strFileName As String) As Boolean

        'ファイルパスを生成
        Dim strIniPath = strFileName

        SetString = True
        'API関数へ呼び変える
        If WritePrivateProfileString(strAppName, strKeyName, strParam, strIniPath) = 0 Then
            SetString = False
        End If
    End Function

    '指定のIniファイルの指定キーの値を変更(文字列)
    'param  strAppName        [in]セクション名
    'param  strKeyName        [in]キー名
    'param  nParam            [in]書き込む値
    'param  strFileName       [in]Iniファイル名
    'rerurn 成功した場合True、失敗した場合はFalseを返す
    Public Function SetInt(ByVal strAppName As String, _
                           ByVal strKeyName As String, _
                           ByVal nParam As Integer, _
                           ByVal strFileName As String) As Boolean

        SetInt = True

        'ファイルパスを生成
        Dim strIniPath = strFileName

        Dim strParam As String = nParam
        'API関数へ呼び変える
        If WritePrivateProfileString(strAppName, strKeyName, strParam, strIniPath) = 0 Then
            SetInt = False
        End If

    End Function

    Public Sub New()

    End Sub
End Class
