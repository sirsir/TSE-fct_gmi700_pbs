Imports System.ComponentModel
Imports System.Text.RegularExpressions

Module GUIcontrols


    'Public Sub DoHeaderGroupsCellPainting(e As DataGridViewCellPaintingEventArgs, DataGridView1 As DataGridView)

    '    Dim m_pGrid As Pen

    '    Dim m_nColumnHeaderTextHeight As Integer = 0
    '    Dim m_brushColumnHeaderBack As SolidBrush = New SolidBrush(DataGridView1.ColumnHeadersDefaultCellStyle.BackColor)
    '    Dim m_brushColumnHeaderFore As SolidBrush = New SolidBrush(DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor)

    '    Dim m_buffHeaderGrouping As Bitmap = Nothing
    '    Dim s_fmtColumnHeader As StringFormat = Nothing

    '    Dim m_arrHeaderGroups As List(Of KeyValuePair(Of Integer, String)())
    '    m_arrHeaderGroups = GetHeaderGroupings(DataGridView1)

    '    Dim m_rows(-1) As DataGridViewRow
    '    Dim m_cols(-1) As DataGridViewColumn
    '    ReDim m_cols(DataGridView1.Columns.Count - 1)
    '    DataGridView1.Columns.CopyTo(m_cols, 0)


    '    Dim g As Graphics = e.Graphics
    '    Dim nLastDisplayedColumnIndex As Integer = DataGridView1.FirstDisplayedScrollingColumnIndex + DataGridView1.DisplayedColumnCount(True) - 1

    '    If m_nColumnHeaderTextHeight = 0 Then
    '        'm_nColumnHeaderTextHeight = g.MeasureString("Sample Text", DataGridView1.ColumnHeadersDefaultCellStyle.Font).Height
    '        m_nColumnHeaderTextHeight = g.MeasureString("Sample Text", DataGridView1.ColumnHeadersDefaultCellStyle.Font).Height + 2
    '    End If

    '    Debug.Assert(DataGridView1.RowHeadersVisible, "The RowHeader (Column -1) is required")

    '    Dim rTmp As Rectangle = DataGridView1.GetCellDisplayRectangle(-1, -1, True)
    '    Dim nOffset As Integer = rTmp.Width

    '    Dim cols() As DataGridViewColumn = m_cols.OrderBy(Function(c) c.DisplayIndex).ToArray

    '    If m_buffHeaderGrouping Is Nothing Then

    '        rTmp.Width = cols.Sum(Function(c) c.Width)

    '        m_buffHeaderGrouping = New Bitmap(rTmp.Width, rTmp.Height)

    '        Dim gTmp As Graphics = Graphics.FromImage(m_buffHeaderGrouping)
    '        gTmp.FillRectangle(New SolidBrush(Drawing.Color.FromArgb(0, Color.Magenta)), rTmp)

    '        For idx As Integer = 0 To m_arrHeaderGroups.Count - 1
    '            Dim nFromIdx As Integer = idx

    '            Dim arrHeaderGroups() As KeyValuePair(Of Integer, String) = m_arrHeaderGroups(idx)

    '            For i As Integer = 0 To arrHeaderGroups.Count - 1
    '                Dim cnt As Integer = arrHeaderGroups(i).Key

    '                If cnt > 0 Then
    '                    Dim nToIdx As Integer = idx + cnt - 1
    '                    Dim w As Integer = cols.Where(Function(c) c.Visible AndAlso c.DisplayIndex >= nFromIdx AndAlso c.DisplayIndex <= nToIdx) _
    '                                           .Sum(Function(c) c.Width)

    '                    If w > 0 Then
    '                        Dim txt As String = arrHeaderGroups(i).Value
    '                        Dim x As Integer = cols.Where(Function(c) c.Visible AndAlso c.DisplayIndex < nFromIdx) _
    '                                               .Sum(Function(c) c.Width)
    '                        'Dim r As New Rectangle(x + 2, i * m_nColumnHeaderTextHeight + 2, w - 3, m_nColumnHeaderTextHeight)
    '                        Dim r As New Rectangle(x + 2, i * m_nColumnHeaderTextHeight + 2, w - 3, m_nColumnHeaderTextHeight)

    '                        gTmp.FillRectangle(m_brushColumnHeaderBack, r)
    '                        gTmp.DrawLine(m_pGrid, r.X, r.Bottom - 2, r.Right - 1, r.Bottom - 2)

    '                        gTmp.DrawString(txt, DataGridView1.ColumnHeadersDefaultCellStyle.Font, m_brushColumnHeaderFore, r, s_fmtColumnHeader)
    '                    End If
    '                End If
    '            Next i
    '        Next idx

    '        gTmp.Dispose()
    '    End If

    '    Dim nHiddenWidth As Integer = DataGridView1.FirstDisplayedScrollingColumnHiddenWidth

    '    If DataGridView1.FirstDisplayedScrollingColumnIndex >= 0 Then
    '        nHiddenWidth += cols.Where(Function(c) c.Visible AndAlso c.DisplayIndex < DataGridView1.Columns(DataGridView1.FirstDisplayedScrollingColumnIndex).DisplayIndex) _
    '                           .Sum(Function(c) c.Width)
    '    End If

    '    g.DrawImage(m_buffHeaderGrouping, nOffset - nHiddenWidth, 0)
    'End Sub

    'Private Function GetHeaderGroupings(DataGridView1 As DataGridView) As List(Of KeyValuePair(Of Integer, String)())
    '    'If DesignMode Then Exit Sub

    '    'Debug.Print("GetHeaderGroupings")

    '    Dim m_arrHeaderGroups As List(Of KeyValuePair(Of Integer, String)()) = New List(Of KeyValuePair(Of Integer, String)())
    '    Dim m_rows(-1) As DataGridViewRow
    '    Dim m_cols(-1) As DataGridViewColumn

    '    m_arrHeaderGroups.Clear()

    '    If m_cols.Any(Function(c) Regex.IsMatch(c.HeaderText, "[|\n]")) Then
    '        Dim arrColFlags As New List(Of KeyValuePair(Of Integer, String)())

    '        Dim cols() As DataGridViewColumn = m_cols.OrderBy(Function(c) c.DisplayIndex).ToArray

    '        For Each dgvc As DataGridViewColumn In cols
    '            If dgvc.HeaderText Like "*|*" Then
    '                dgvc.HeaderText = dgvc.HeaderText.Replace("|", vbNewLine)
    '                'Titleize(dgvc.HeaderText)

    '                If DataGridView1.ColumnHeadersDefaultCellStyle.Alignment <> DataGridViewContentAlignment.TopCenter Then
    '                    DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
    '                End If
    '            End If
    '            Titleize(dgvc.HeaderText)

    '            Dim arrHeaderTexts() As String = dgvc.HeaderText.Split(vbNewLine)

    '            If arrHeaderTexts.Count > 1 Then
    '                Dim nFlags(arrHeaderTexts.Count - 2) As KeyValuePair(Of Integer, String)
    '                Dim strHeaderText As String = String.Empty

    '                For i As Integer = 0 To nFlags.Count - 1
    '                    strHeaderText &= arrHeaderTexts(i)
    '                    nFlags(i) = New KeyValuePair(Of Integer, String)(strHeaderText.GetHashCode, arrHeaderTexts(i).Trim)
    '                Next i

    '                arrColFlags.Add(nFlags)
    '            Else
    '                arrColFlags.Add({})
    '            End If
    '        Next dgvc

    '        For i As Integer = arrColFlags.Count - 1 To 0 Step -1
    '            Dim nFlags() As KeyValuePair(Of Integer, String) = arrColFlags(i)
    '            Dim nCounts(nFlags.Count - 1) As KeyValuePair(Of Integer, String)

    '            Dim nFlagsNxt() As KeyValuePair(Of Integer, String) = {}
    '            Dim nCountsNxt() As KeyValuePair(Of Integer, String) = {}

    '            If i < arrColFlags.Count - 1 Then
    '                nFlagsNxt = arrColFlags(i + 1)
    '                nCountsNxt = m_arrHeaderGroups.First
    '            End If

    '            For j As Integer = 0 To nCounts.Count - 1
    '                If i < arrColFlags.Count - 1 AndAlso _
    '                        j <= nFlagsNxt.Count - 1 AndAlso _
    '                        nFlags(j).Key = nFlagsNxt(j).Key AndAlso _
    '                        (j = 0 OrElse nCountsNxt(j - 1).Key <> 1) Then
    '                    nCounts(j) = New KeyValuePair(Of Integer, String)(nCountsNxt(j).Key + 1, nFlags(j).Value)
    '                    nCountsNxt(j) = Nothing
    '                Else
    '                    nCounts(j) = New KeyValuePair(Of Integer, String)(1, nFlags(j).Value)
    '                End If
    '            Next j

    '            m_arrHeaderGroups.Insert(0, nCounts)
    '        Next i
    '    Else
    '        For i As Integer = 0 To DataGridView1.Columns.Count - 1
    '            Titleize(DataGridView1.Columns(i).HeaderText)
    '        Next
    '    End If

    '    Return m_arrHeaderGroups
    'End Function

    'Public Sub Titleize(ByRef strText As String)
    '    Debug.Print("Titleize " & strText)
    '    Dim reId As New System.Text.RegularExpressions.Regex(" (id)$")
    '    Dim reNo As New System.Text.RegularExpressions.Regex(" (no)$")
    '    Dim reQty As New System.Text.RegularExpressions.Regex("(qty)$")
    '    Dim reSpace As New System.Text.RegularExpressions.Regex("[ _]+")

    '    If strText = strText.ToUpper Then
    '        strText = strText.ToLower
    '    End If
    '    strText = reSpace.Replace(strText, " ").Trim
    '    strText = reId.Replace(strText, "")
    '    strText = reNo.Replace(strText, " No.")
    '    strText = reQty.Replace(strText, "Q'ty")

    '    strText = Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(strText)
    'End Sub
End Module
