Public Class clsField
    'FieldName,Type,Position,Length,Multiplier,WriteFormat
#Region "Attribute"
    Private m_strFieldName As String
    Private m_strFieldType As String
    Private m_intStartPos As Integer
    Private m_intLength As Integer
    Private m_dblMultiplier As Double
    Private m_strWriteFormat As String
    Private m_strReplacePattern As String
    Private m_nType As nFieldType
    Private m_blnWrite As Boolean
#End Region

    Private Enum nFieldType
        ASCII
        BCD
        CONDITION
        DTIME
    End Enum

#Region "Properties"

    Public WriteOnly Property FieldType As String
        Set(value As String)
            Select Case value.ToLower
                Case "s"
                    m_nType = nFieldType.ASCII
                Case "b"
                    m_nType = nFieldType.BCD
                Case "c"
                    m_nType = nFieldType.CONDITION
                Case "d"
                    m_nType = nFieldType.DTIME
            End Select
            m_strFieldType = value
        End Set
    End Property

    Public WriteOnly Property WriteFormat As String
        Set(value As String)
            If value = "" Then
                m_blnWrite = False
            Else
                m_blnWrite = True
            End If
            m_strWriteFormat = value
        End Set
    End Property

    Public ReadOnly Property FieldName As String
        Get
            Return m_strFieldName
        End Get
    End Property

    Public ReadOnly Property IsForWrite As Boolean
        Get
            Return m_blnWrite
        End Get
    End Property

    Public ReadOnly Property ReplacePattern As String
        Get
            Return m_strReplacePattern
        End Get
    End Property

#End Region

    Public Sub New(ByVal strFieldFormat As String)
        Me.Reset()
        Dim astrFieldFormat() As String = strFieldFormat.Split({","}, StringSplitOptions.None)
        If astrFieldFormat.Length <> 7 Then
            Throw New Exception("Invalid Field Setting: " & strFieldFormat)
        End If

        m_strFieldName = astrFieldFormat(0)
        Me.FieldType = astrFieldFormat(1)
        If Not Integer.TryParse(astrFieldFormat(2), m_intStartPos) Then
            Throw New Exception("Invalid Field Setting:Position: " & astrFieldFormat(2))
        End If

        If Not Integer.TryParse(astrFieldFormat(3), m_intLength) Then
            Throw New Exception("Invalid Field Setting:Length: " & astrFieldFormat(3))
        End If

        If astrFieldFormat(4) <> "" Then
            If Not Double.TryParse(astrFieldFormat(4), m_dblMultiplier) Then
                Throw New Exception("Invalid Field Setting:Multiplier: " & astrFieldFormat(4))
            End If
        End If

        Me.WriteFormat = astrFieldFormat(5)
        m_strReplacePattern = astrFieldFormat(6)
    End Sub

#Region "Method"
    Private Sub Reset()
        m_strFieldName = ""
        m_strFieldType = ""
        m_intStartPos = -1
        m_intLength = -1
        m_dblMultiplier = 1
        m_strWriteFormat = ""
        m_strReplacePattern = ""
        m_nType = nFieldType.BCD
        m_blnWrite = False
    End Sub

    Public Shared Function GetFieldList(ByVal strFormat As String) As List(Of clsField)
        Dim aobjField As New List(Of clsField)
        Dim astrFormat() As String = strFormat.Replace(vbCrLf, "").Split("|")

        For i As Integer = 0 To astrFormat.Length - 1
            Dim objField As New clsField(astrFormat(i).Trim)
            aobjField.Add(objField)
        Next

        Return aobjField
    End Function

    Public Function ExtractFieldAscii(ByVal input As String) As String
        'If Not m_blnWrite Then
        '    Return ""
        'End If

        ExtractFieldAscii = String.Format(m_strWriteFormat, input.Substring(m_intStartPos, m_intLength))
    End Function

    Public Function ExtractFieldBcd(ByVal input() As Integer) As String
        'If Not m_blnWrite Then
        '    Return ""
        'End If

        Dim intTemp As Integer = IntegerToBCD_1WORD(input(m_intStartPos))
        If intTemp < 0 Then
            Return ""
        End If

        Dim dblOutValue As Double = intTemp * IIf(m_dblMultiplier >= 0, m_dblMultiplier, 1)
        Return dblOutValue.ToString(m_strWriteFormat)
    End Function

    Public Function ExtractFieldConditional(ByVal input As String) As String
        'If Not m_blnWrite Then
        '    Return ""
        'End If

        Dim strTemp As String = input.Substring(m_intStartPos, m_intLength)
        Select Case strTemp.Trim
            Case "OK"
                strTemp = "1"
            Case "NG"
                strTemp = "0"
            Case Else
                strTemp = strTemp.Trim
        End Select
        ExtractFieldConditional = String.Format(m_strWriteFormat, strTemp)
    End Function

    Public Function ExtractFieldDateTime(ByVal input() As Integer) As String
        'If Not m_blnWrite Then
        '    Return ""
        'End If

        Dim aintTemp(2) As Integer
        Dim aintBcd(2) As Integer
        aintTemp(0) = input(m_intStartPos)
        aintTemp(1) = input(m_intStartPos + 1)
        aintTemp(2) = input(m_intStartPos + 2)

        aintBcd = modFunction.ArrIntegerToArrBCD(aintTemp)
        Dim strYear As String = aintBcd(2).ToString("D4").Substring(0, 2)
        Dim strMonth As String = aintBcd(2).ToString("D4").Substring(2, 2)
        Dim strDay As String = aintBcd(1).ToString("D4").Substring(0, 2)
        Dim strHour As String = aintBcd(1).ToString("D4").Substring(2, 2)
        Dim strMinute As String = aintBcd(0).ToString("D4").Substring(0, 2)
        Dim strSecond As String = aintBcd(0).ToString("D4").Substring(2, 2)
        Dim strTemp As String = m_strWriteFormat.Replace("yy", strYear).Replace("MM", strMonth).Replace("dd", strDay).Replace("HH", strHour).Replace("mm", strMinute).Replace("ss", strSecond)
        Return strTemp
    End Function

    Public Function ExtractField(ByVal strInput As String, ByVal intInput() As Integer) As String
        Select Case Me.m_nType
            Case nFieldType.ASCII
                ExtractField = Me.ExtractFieldAscii(strInput)
            Case nFieldType.BCD
                ExtractField = Me.ExtractFieldBcd(intInput)
            Case nFieldType.CONDITION
                ExtractField = Me.ExtractFieldConditional(strInput)
            Case nFieldType.DTIME
                ExtractField = Me.ExtractFieldDateTime(intInput)
            Case Else
                ExtractField = Me.ExtractFieldBcd(intInput)
        End Select
    End Function

    Public Function ExtractDateTime(ByVal intInput() As Integer) As DateTime
        Dim aintTemp(2) As Integer
        Dim aintBcd(2) As Integer
        aintTemp(0) = intInput(m_intStartPos)
        aintTemp(1) = intInput(m_intStartPos + 1)
        aintTemp(2) = intInput(m_intStartPos + 2)

        aintBcd = modFunction.ArrIntegerToArrBCD(aintTemp)
        Dim strYear As String = aintBcd(2).ToString("D4").Substring(0, 2)
        Dim strMonth As String = aintBcd(2).ToString("D4").Substring(2, 2)
        Dim strDay As String = aintBcd(1).ToString("D4").Substring(0, 2)
        Dim strHour As String = aintBcd(1).ToString("D4").Substring(2, 2)
        Dim strMinute As String = aintBcd(0).ToString("D4").Substring(0, 2)
        Dim strSecond As String = aintBcd(0).ToString("D4").Substring(2, 2)
        Dim strFormat As String = "20{0}-{1}-{2} {3}:{4}:{5}"
        Dim strDateTim As String = String.Format(strFormat, strYear, strMonth, strDay, strHour, strMinute, strSecond)
        Dim datTemp As DateTime = DateTime.ParseExact(strDateTim, "yyyy-MM-dd HH:mm:ss", _
                                       System.Globalization.CultureInfo.InvariantCulture)
        Return datTemp
    End Function

#End Region
End Class
