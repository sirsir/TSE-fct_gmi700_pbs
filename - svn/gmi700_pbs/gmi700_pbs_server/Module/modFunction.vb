Imports System.Data
Imports System.IO

Module modFunction

    Public Function IntegerToBCD_1WORD(ByVal input As Integer) As Integer
        If input > 65535 Then
            Return -1
        End If

        If input < 0 Then
            Return -1
        End If

        Dim strHex As String = Hex$(input).PadLeft(4, "0")
        Dim intTemp As Integer = -1

        If Not Integer.TryParse(strHex, intTemp) Then
            Return -1
        Else
            Return intTemp
        End If
    End Function

    Public Function IntegerToBCD_1WORD_Revert(ByVal input As Integer) As Integer
        If input > 65535 Then
            Return -1
        End If

        If input < 0 Then
            Return -1
        End If

        Dim strHex As String = Hex$(input).PadLeft(4, "0")
        strHex = strHex.Substring(2, 2) & strHex.Substring(0, 2)
        Dim intTemp As Integer = -1

        If Not Integer.TryParse(strHex, intTemp) Then
            Return -1
        Else
            Return intTemp
        End If
    End Function

    Public Function ArrIntegerToArrBCD(ByVal input() As Integer) As Integer()
        Dim aintTemp() As Integer = input
        For i As Integer = 0 To input.Length - 1
            aintTemp(i) = IntegerToBCD_1WORD(input(i))
        Next
        Return aintTemp
    End Function

    Public Sub Main()
        Try
            'Try
            '    Dim sql As String
            '    Using MyConnection As New System.Data.OleDb.OleDbConnection _
            '    ("provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & _
            '    "'D:\work\OMRON\IHI\IHI_DataFilingSystem\20150121\ManualConvertProgram\Template - Copy\OP10.xls';" & _
            '    "Extended Properties=""Excel 8.0;HDR=YES""")
            '        MyConnection.Open()
            '        Using myCommand As New System.Data.OleDb.OleDbCommand()
            '            myCommand.Connection = MyConnection
            '            sql = "Insert into [DATA$] " _
            '                & " ([ITEM],[SERIAL], [MODE], [MC], [E], [F], [G], [H], [I], [J], [K], [L], [M], [N], [O], [P], [Q], [R], [S], [T], [U], [V], [DATE], [TIME], [STATUS]) " _
            '                & " values('','ABCDEFGHIJ000001','MASTER','10','0.01','0.02','0.02','0.02','0.02','0.02','0.02','0.02','0.02','0.02','0.02','0.02','0.03','0.03','0.03','0.03','0.03','0.03','2015:01:31','21','1')"
            '            myCommand.CommandText = sql
            '            myCommand.ExecuteNonQuery()
            '        End Using
            '    End Using
            '    My.Computer.FileSystem.CopyFile("D:\work\OMRON\IHI\IHI_DataFilingSystem\20150121\ManualConvertProgram\Template - Copy\OP10.xls",
            '                                    "D:\work\OMRON\IHI\IHI_DataFilingSystem\20150121\ManualConvertProgram\Template - Copy\OP10_copy.xls")
            'Catch ex As Exception
            '    'MsgBox(ex.ToString)
            'End Try

            Dim sv As New Service1
            sv.DebugStart() ' for test sevice
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & ex.StackTrace)
        End Try
    End Sub

    Public Sub WriteToFile(strFileName As String, strString As String)

        Dim strDir As String = Path.GetDirectoryName(strFileName)
        If Not IO.Directory.Exists(strDir) Then
            Try
                IO.Directory.CreateDirectory(strDir)
            Catch ex As Exception

            End Try
        End If

        Using writer As New IO.StreamWriter(strFileName, True)
            writer.WriteLine(strString)
        End Using
    End Sub
End Module
