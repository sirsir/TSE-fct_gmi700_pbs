Imports System.ComponentModel
Imports System.Text.RegularExpressions

Public Class ctrlPivotGrid
    Inherits Windows.Forms.DataGridView

    Private _initialized As Boolean = False
    'Private m_dtColumnHeaderSource As DataSet1.V_SETTING_COLUMNSDataTable
    'Private m_strColumnHeaderCaptionDataMember As String
    'Private m_strColumnHeaderMappingDataMember As String

    'Private m_dtRowHeaderSource As DataTable
    'Private m_strRowHeaderCaptionDataMember As String
    Private m_astrRowHeaderMappingDataMember As String()

    'Private m_dtRawDataSource As DataTable
    Private m_strRawDataColumnMappingDataMember As String
    Private m_astrRawDataRowMappingDataMember As String()
    Private m_strRawDataValueDataMember As String

    Private m_dtDynamicDataSource As DataTable

    Private m_brushColumnHeaderBack As SolidBrush
    Private m_brushColumnHeaderFore As SolidBrush
    Private m_nColumnHeaderTextHeight As Integer = 0
    Private m_pGrid As Pen
    Private m_arrHeaderGroups As List(Of KeyValuePair(Of Integer, String)())
    Private m_buffHeaderGrouping As Bitmap
    Private Shared s_fmtColumnHeader As StringFormat = Nothing
    ' ''Private _RowGroupHashCodes As Integer() = {}
    ' ''Private _RowGroupSublings As Dictionary(Of Integer, DataGridViewRow())

    Private m_rows(-1) As DataGridViewRow
    Private m_cols(-1) As DataGridViewColumn
    Private m_arrFKCols As List(Of String)

    Public Shared CLR_READONLY As Color = Color.FromArgb(255, 255, 192)
    Public Shared CLR_READONLY_ALT As Color = Color.FromArgb(255, 255, 172)
    Public Shared CLR_PRIMARY_KEY As Color = Color.FromArgb(192, 255, 192)
    Public Shared CLR_EDITABLE As Color = Color.White
    Public Shared CLR_GRAYED As Color = Color.FromArgb(224, 224, 224)

    Public Enum nDataType
        nString = 1
        nFloat = 2
        nDateTime = 3
    End Enum

    <Browsable(False)> _
    Public ReadOnly Property DataTable As DataTable
        Get
            Return m_dtDynamicDataSource
        End Get
    End Property

    <Browsable(False)> _
    Public ReadOnly Property DataColumn(index) As DataColumn
        Get
            Dim dc As DataColumn = Nothing
            Dim dt As DataTable = Me.DataTable

            If dt IsNot Nothing Then
                Dim dgvc As DataGridViewColumn = Me.Columns(index)
                If dt.Columns.Contains(dgvc.DataPropertyName) Then
                    dc = dt.Columns(dgvc.DataPropertyName)
                End If
            End If

            Return dc
        End Get
    End Property

    'Public Shadows Property DataSource As Object
    '    Get
    '        Return MyBase.DataSource()
    '    End Get
    '    Set(value As Object)
    '        Me.SetDataSource(value _
    '                            , Nothing _
    '                            , Nothing _
    '                            , Nothing _
    '                            , Nothing _
    '                            , Nothing _
    '                            , Nothing _
    '                            , Nothing _
    '                            , Nothing _
    '                            , Nothing _
    '                            , False)
    '    End Set
    'End Property

    <Category("Appearance")> _
    Public Property HideDuplicateColumns As Integer = 0

    <Category("Appearance")> _
    Public Property GroupedDuplicateCells As Boolean = False

    ' ''<Browsable(False)> _
    ' ''Public ReadOnly Property RowGroupHashCode(index As Integer) As Integer
    ' ''    Get
    ' ''        Return _RowGroupHashCodes(index)
    ' ''    End Get
    ' ''End Property

    ' ''<Browsable(False)> _
    ' ''Public ReadOnly Property RowGroupSublings(key As Integer) As DataGridViewRow()
    ' ''    Get
    ' ''        Return _RowGroupSublings(key)
    ' ''    End Get
    ' ''End Property

    Public Sub New()
        Me.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        m_arrFKCols = New List(Of String)
        '_CreatedLastModifiedFont = New Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular)

        If s_fmtColumnHeader Is Nothing Then
            s_fmtColumnHeader = New StringFormat(StringFormatFlags.NoWrap)
            s_fmtColumnHeader.LineAlignment = StringAlignment.Center
            s_fmtColumnHeader.Alignment = StringAlignment.Center
        End If

        If Me.ColumnHeadersDefaultCellStyle IsNot Nothing Then
            m_brushColumnHeaderBack = New SolidBrush(Me.ColumnHeadersDefaultCellStyle.BackColor)
            m_brushColumnHeaderFore = New SolidBrush(Me.ColumnHeadersDefaultCellStyle.ForeColor)
        End If

        m_pGrid = New Pen(Me.GridColor)
        ' ''_RowGroupSublings = New Dictionary(Of Integer, DataGridViewRow())
        m_arrHeaderGroups = New List(Of KeyValuePair(Of Integer, String)())
    End Sub

    'Public Sub SetDataSource(ByVal dtStatic As DataTable _
    '                                    , ByVal dtDynamicDataString As DataTable _
    '                                    , ByVal dtDynamicDataInteger As DataTable _
    '                                    , ByVal dtDynamicDataDateTime As DataTable _
    '                                    , ByVal dtColumnHeader As DataSet1.V_SETTING_COLUMNSDataTable _
    '                                    , ByVal astrRowHeaderMappingDataMember As String() _
    '                                    , ByVal strRawDataColumnMappingDataMember As String _
    '                                    , ByVal astrRawDataRowMappingDataMember As String() _
    '                                    , ByVal strRawDataValueDataMember As String _
    '                                    , ByVal astrDisplayColumnNames As String() _
    '                                    , Optional ByVal blnReadOnly As Boolean = True)

    '    Dim intStaticColumnCount As Integer = 0
    '    m_astrRowHeaderMappingDataMember = astrRowHeaderMappingDataMember
    '    m_strRawDataColumnMappingDataMember = strRawDataColumnMappingDataMember
    '    m_astrRawDataRowMappingDataMember = astrRawDataRowMappingDataMember
    '    m_strRawDataValueDataMember = strRawDataValueDataMember
    '    'Dim dtDynamicDataSource As DataTable
    '    If dtStatic IsNot Nothing Then
    '        m_dtDynamicDataSource = dtStatic
    '    Else
    '        m_dtDynamicDataSource = New DataTable
    '    End If

    '    intStaticColumnCount = m_dtDynamicDataSource.Columns.Count

    '    If dtColumnHeader IsNot Nothing Then
    '        For i As Integer = 0 To dtColumnHeader.Rows.Count - 1
    '            With dtColumnHeader.Item(i)
    '                Select Case .DATA_TYPE
    '                    Case nDataType.nString
    '                        m_dtDynamicDataSource.Columns.Add("ID" & .ID.ToString("D3"), GetType(String))
    '                        m_dtDynamicDataSource.Columns("ID" & .ID.ToString("D3")).Caption = .COLUMN_NAME
    '                    Case nDataType.nFloat
    '                        m_dtDynamicDataSource.Columns.Add("ID" & .ID.ToString("D3"), GetType(Double))
    '                        m_dtDynamicDataSource.Columns("ID" & .ID.ToString("D3")).Caption = .COLUMN_NAME
    '                    Case nDataType.nDateTime
    '                        m_dtDynamicDataSource.Columns.Add("ID" & .ID.ToString("D3"), GetType(DateTime))
    '                        m_dtDynamicDataSource.Columns("ID" & .ID.ToString("D3")).Caption = .COLUMN_NAME
    '                End Select
    '            End With
    '        Next

    '        Dim adrDynamicDataOfCurrentRow As DataRow()
    '        Dim drStatic As DataRow
    '        For intRowIndex As Integer = 0 To dtStatic.Rows.Count - 1
    '            drStatic = m_dtDynamicDataSource(intRowIndex)

    '            Dim strTemp As String = ""
    '            For i As Integer = 0 To m_astrRawDataRowMappingDataMember.Length - 1
    '                If i <> 0 Then
    '                    strTemp &= " AND "
    '                End If
    '                strTemp &= m_astrRawDataRowMappingDataMember(i) & " = " & ValueForSQL(drStatic(m_astrRowHeaderMappingDataMember(i)))
    '            Next

    '            If dtDynamicDataString IsNot Nothing Then
    '                adrDynamicDataOfCurrentRow = dtDynamicDataString.Select(strTemp)
    '                For intDynamicDataIndex = 0 To adrDynamicDataOfCurrentRow.Length - 1
    '                    drStatic("ID" & CInt(adrDynamicDataOfCurrentRow(intDynamicDataIndex)(m_strRawDataColumnMappingDataMember)).ToString("D3")) = adrDynamicDataOfCurrentRow(intDynamicDataIndex)(m_strRawDataValueDataMember)
    '                Next
    '            End If

    '            If dtDynamicDataInteger IsNot Nothing Then
    '                adrDynamicDataOfCurrentRow = dtDynamicDataInteger.Select(strTemp)
    '                For intDynamicDataIndex = 0 To adrDynamicDataOfCurrentRow.Length - 1
    '                    drStatic("ID" & CInt(adrDynamicDataOfCurrentRow(intDynamicDataIndex)(m_strRawDataColumnMappingDataMember)).ToString("D3")) = adrDynamicDataOfCurrentRow(intDynamicDataIndex)(m_strRawDataValueDataMember)
    '                Next
    '            End If

    '            If dtDynamicDataDateTime IsNot Nothing Then
    '                adrDynamicDataOfCurrentRow = dtDynamicDataDateTime.Select(strTemp)
    '                For intDynamicDataIndex = 0 To adrDynamicDataOfCurrentRow.Length - 1
    '                    drStatic("ID" & CInt(adrDynamicDataOfCurrentRow(intDynamicDataIndex)(m_strRawDataColumnMappingDataMember)).ToString("D3")) = adrDynamicDataOfCurrentRow(intDynamicDataIndex)(m_strRawDataValueDataMember)
    '                Next
    '            End If

    '        Next
    '    End If


    '    RemoveHeaderHandlers()
    '    Me.DataSource = m_dtDynamicDataSource
    '    AddHeaderHandlers()

    '    Dim strHeaderText As String = ""
    '    Dim intColumnId As Integer = -1

    '    For i As Integer = 0 To Me.Columns.Count - 1
    '        If i < intStaticColumnCount Then
    '            Me.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
    '            'Me.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    '            If blnReadOnly Then
    '                Me.Columns(i).ReadOnly() = blnReadOnly
    '            End If
    '            Me.Columns(i).Visible = False
    '        Else

    '            strHeaderText = Me.Columns(i).DataPropertyName
    '            If strHeaderText Like "ID*" Then
    '                If IsNumeric(strHeaderText.Substring(2)) Then
    '                    intColumnId = CInt(strHeaderText.Substring(2))
    '                    Dim drSetting As DataSet1.V_SETTING_COLUMNSRow
    '                    drSetting = dtColumnHeader.FindByID(intColumnId)

    '                    If drSetting IsNot Nothing Then
    '                        ' ''Me.Columns(i).HeaderText() = drSetting.COLUMN_NAME
    '                        Select Case drSetting.DATA_TYPE
    '                            Case nDataType.nString
    '                                Me.Columns(i).DefaultCellStyle.Alignment() = DataGridViewContentAlignment.MiddleLeft
    '                            Case nDataType.nFloat
    '                                Me.Columns(i).DefaultCellStyle.Alignment() = DataGridViewContentAlignment.MiddleRight
    '                                If Not drSetting.IsFORMAT_STRINGNull Then
    '                                    Me.Columns(i).DefaultCellStyle.Format = Me.NumericFormatStringToDataGridViewFormatString(drSetting.FORMAT_STRING)
    '                                End If
    '                            Case nDataType.nDateTime
    '                                Me.Columns(i).DefaultCellStyle.Alignment() = DataGridViewContentAlignment.MiddleLeft
    '                        End Select
    '                        Me.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
    '                        'Me.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    '                        If blnReadOnly Then
    '                            Me.Columns(i).ReadOnly() = blnReadOnly
    '                        End If
    '                    End If

    '                End If

    '            End If
    '        End If

    '    Next
    '    'For Each strColName As String In astrHiddenColumnNames
    '    '    If Me.Columns(strColName) IsNot Nothing Then Me.Columns(strColName).Visible = False
    '    'Next

    '    If astrDisplayColumnNames IsNot Nothing AndAlso astrDisplayColumnNames.Length > 0 Then
    '        Try
    '            RemoveHeaderHandlers()
    '            Dim strColName As String = ""
    '            For i As Integer = 0 To astrDisplayColumnNames.Length - 1
    '                strColName = astrDisplayColumnNames(i)
    '                If Me.Columns(strColName) IsNot Nothing Then
    '                    Me.Columns(strColName).DisplayIndex = i
    '                    Me.Columns(strColName).Visible = True
    '                End If
    '            Next
    '        Catch ex As Exception

    '        Finally
    '            AddHeaderHandlers()
    '        End Try
    '    Else
    '        Try
    '            RemoveHeaderHandlers()
    '            Dim strColName As String = ""
    '            For i As Integer = 0 To Me.Columns.Count - 1
    '                If Me.Columns(i) IsNot Nothing Then
    '                    Me.Columns(i).Visible = True
    '                End If
    '            Next
    '        Catch ex As Exception

    '        Finally
    '            AddHeaderHandlers()
    '        End Try
    '    End If


    'End Sub

    Public Sub SetDisplayColumn(ByVal astrDisplayColumnNames As String())
        If astrDisplayColumnNames IsNot Nothing AndAlso astrDisplayColumnNames.Length > 0 Then
            Try
                RemoveHeaderHandlers()
                For i As Integer = 0 To Me.ColumnCount - 1
                    Me.Columns(i).Visible = False
                Next

                Dim strColName As String = ""
                For i As Integer = 0 To astrDisplayColumnNames.Length - 1
                    strColName = astrDisplayColumnNames(i)
                    If Me.Columns(strColName) IsNot Nothing Then
                        Me.Columns(strColName).DisplayIndex = i
                        Me.Columns(strColName).Visible = True
                    End If
                Next
            Catch ex As Exception

            Finally
                AddHeaderHandlers()
            End Try
        Else
            Try
                RemoveHeaderHandlers()
                Dim strColName As String = ""
                For i As Integer = 0 To Me.Columns.Count - 1
                    If Me.Columns(i) IsNot Nothing Then
                        Me.Columns(i).Visible = True
                    End If
                Next
            Catch ex As Exception

            Finally
                AddHeaderHandlers()
            End Try
        End If
    End Sub

    Public Sub AddHeaderHandlers()
        AddHandler Me.ColumnDisplayIndexChanged, AddressOf ctrlPivotGrid_ColumnDisplayIndexChanged
        AddHandler Me.ColumnAdded, AddressOf ctrlPivotGrid_ColumnAdded
        AddHandler Me.ColumnStateChanged, AddressOf ctrlPivotGrid_ColumnStateChanged
        AddHandler Me.CellValueChanged, AddressOf ctrlPivotGrid_CellValueChanged
        AddHandler Me.ColumnWidthChanged, AddressOf ctrlPivotGrid_ColumnWidthChanged
    End Sub

    Public Sub RemoveHeaderHandlers()
        RemoveHandler Me.ColumnDisplayIndexChanged, AddressOf ctrlPivotGrid_ColumnDisplayIndexChanged
        RemoveHandler Me.ColumnAdded, AddressOf ctrlPivotGrid_ColumnAdded
        RemoveHandler Me.ColumnStateChanged, AddressOf ctrlPivotGrid_ColumnStateChanged
        RemoveHandler Me.CellValueChanged, AddressOf ctrlPivotGrid_CellValueChanged
        RemoveHandler Me.ColumnWidthChanged, AddressOf ctrlPivotGrid_ColumnWidthChanged
    End Sub

    Private Sub RefreshHeaderGroupings(Optional blnResetHeaderGroupings As Boolean = False, Optional blnResetColProps As Boolean = False)
        Try
            Debug.Print("RefreshHeaderGroupings, " & blnResetHeaderGroupings.ToString & ", " & blnResetColProps)
            RemoveHeaderHandlers()

            If blnResetColProps Then
                ReDim m_cols(Me.Columns.Count - 1)
                Me.Columns.CopyTo(m_cols, 0)

                ResetColProps()
            End If

            If blnResetHeaderGroupings Then
                GetHeaderGroupings()
            End If

            DisposeBuffer()

            Debug.Assert(Me.RowHeadersVisible, "The RowHeader (Column -1) is required")

            Dim rtHeader As Rectangle = Me.DisplayRectangle
            rtHeader.Height = Me.GetCellDisplayRectangle(-1, -1, False).Height

            Me.Invalidate(rtHeader)
        Finally
            AddHeaderHandlers()
        End Try
    End Sub

    Private Sub ctrlPivotGrid_ColumnDisplayIndexChanged(sender As Object, e As DataGridViewColumnEventArgs) 'Handles Me.ColumnDisplayIndexChanged
        Try
            Debug.Print("ctrlPivotGrid_ColumnDisplayIndexChanged {0}", e.Column.Index)
            RefreshHeaderGroupings(True)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub ctrlPivotGrid_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) 'Handles Me.CellValueChanged
        Try
            If e.RowIndex < 0 AndAlso e.ColumnIndex >= 0 Then
                Debug.Print("ctrlPivotGrid_CellValueChanged Row={0} Cell={1}", e.RowIndex, e.ColumnIndex)
                RefreshHeaderGroupings(False)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub ctrlPivotGrid_ColumnStateChanged(sender As Object, e As DataGridViewColumnStateChangedEventArgs) 'Handles Me.ColumnStateChanged
        Try
            Debug.Print("ctrlPivotGrid_ColumnStateChanged")
            RefreshHeaderGroupings()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub ctrlPivotGrid_ColumnAdded(sender As Object, e As DataGridViewColumnEventArgs) 'Handles Me.ColumnAdded
        Try
            Debug.Print("ctrlPivotGrid_ColumnAdded")
            RefreshHeaderGroupings(True, True)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub ctrlPivotGrid_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles Me.CellBeginEdit
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Debug.Print("ctrlPivotGrid_CellBeginEdit")
            RemoveHeaderHandlers()
        End If
    End Sub

    Private Sub ctrlPivotGrid_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Me.CellEndEdit
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Debug.Print("ctrlPivotGrid_CellEndEdit")
            AddHeaderHandlers()
        End If
    End Sub

    Private Sub ctrlPivotGrid_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles Me.CellPainting
        Try
            Dim nRowIndex As Integer = e.RowIndex
            Dim nColumnIndex As Integer = e.ColumnIndex

            If nRowIndex = -1 AndAlso nColumnIndex >= 0 Then
                Debug.Print("ctrlPivotGrid_CellPainting Row={0} Cell={1}", e.RowIndex, e.ColumnIndex)
                e.Paint(e.CellBounds, DataGridViewPaintParts.All)

                If m_arrHeaderGroups.Count > 0 Then
                    DoHeaderGroupsCellPainting(e)
                End If

                ' Paint Filter and Sort indicator on header cells
                'Handle_Sort_CellPainting(e)
                'Handle_Filter_CellPainting(e)

                e.Handled = True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub ctrlPivotGrid_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Me.ColumnWidthChanged
        Try
            Debug.Print("ctrlPivotGrid_ColumnWidthChanged")
            RemoveHeaderHandlers()

            DisposeBuffer()

            Debug.Assert(Me.RowHeadersVisible, "The RowHeader (Column -1) is required")

            Dim rtHeader As Rectangle = Me.DisplayRectangle
            rtHeader.Height = Me.GetCellDisplayRectangle(-1, -1, False).Height

            Me.Invalidate(rtHeader)
        Catch ex As Exception
            LogException(ex)
        Finally
            AddHeaderHandlers()
        End Try
    End Sub

    Private Sub DoHeaderGroupsCellPainting(e As DataGridViewCellPaintingEventArgs)
        Dim g As Graphics = e.Graphics
        Dim nLastDisplayedColumnIndex As Integer = Me.FirstDisplayedScrollingColumnIndex + Me.DisplayedColumnCount(True) - 1

        If m_nColumnHeaderTextHeight = 0 Then
            'm_nColumnHeaderTextHeight = g.MeasureString("Sample Text", Me.ColumnHeadersDefaultCellStyle.Font).Height
            m_nColumnHeaderTextHeight = g.MeasureString("Sample Text", Me.ColumnHeadersDefaultCellStyle.Font).Height + 2
        End If

        Debug.Assert(Me.RowHeadersVisible, "The RowHeader (Column -1) is required")

        Dim rTmp As Rectangle = Me.GetCellDisplayRectangle(-1, -1, True)
        Dim nOffset As Integer = rTmp.Width

        Dim cols() As DataGridViewColumn = m_cols.OrderBy(Function(c) c.DisplayIndex).ToArray

        If m_buffHeaderGrouping Is Nothing Then

            rTmp.Width = cols.Sum(Function(c) c.Width)

            m_buffHeaderGrouping = New Bitmap(rTmp.Width, rTmp.Height)

            Dim gTmp As Graphics = Graphics.FromImage(m_buffHeaderGrouping)
            gTmp.FillRectangle(New SolidBrush(Drawing.Color.FromArgb(0, Color.Magenta)), rTmp)

            For idx As Integer = 0 To m_arrHeaderGroups.Count - 1
                Dim nFromIdx As Integer = idx

                Dim arrHeaderGroups() As KeyValuePair(Of Integer, String) = m_arrHeaderGroups(idx)

                For i As Integer = 0 To arrHeaderGroups.Count - 1
                    Dim cnt As Integer = arrHeaderGroups(i).Key

                    If cnt > 0 Then
                        Dim nToIdx As Integer = idx + cnt - 1
                        Dim w As Integer = cols.Where(Function(c) c.Visible AndAlso c.DisplayIndex >= nFromIdx AndAlso c.DisplayIndex <= nToIdx) _
                                               .Sum(Function(c) c.Width)

                        If w > 0 Then
                            Dim txt As String = arrHeaderGroups(i).Value
                            Dim x As Integer = cols.Where(Function(c) c.Visible AndAlso c.DisplayIndex < nFromIdx) _
                                                   .Sum(Function(c) c.Width)
                            'Dim r As New Rectangle(x + 2, i * m_nColumnHeaderTextHeight + 2, w - 3, m_nColumnHeaderTextHeight)
                            Dim r As New Rectangle(x + 2, i * m_nColumnHeaderTextHeight + 2, w - 3, m_nColumnHeaderTextHeight)

                            gTmp.FillRectangle(m_brushColumnHeaderBack, r)
                            gTmp.DrawLine(m_pGrid, r.X, r.Bottom - 2, r.Right - 1, r.Bottom - 2)

                            gTmp.DrawString(txt, Me.ColumnHeadersDefaultCellStyle.Font, m_brushColumnHeaderFore, r, s_fmtColumnHeader)
                        End If
                    End If
                Next i
            Next idx

            gTmp.Dispose()
        End If

        Dim nHiddenWidth As Integer = Me.FirstDisplayedScrollingColumnHiddenWidth

        If Me.FirstDisplayedScrollingColumnIndex >= 0 Then
            nHiddenWidth += cols.Where(Function(c) c.Visible AndAlso c.DisplayIndex < Me.Columns(Me.FirstDisplayedScrollingColumnIndex).DisplayIndex) _
                               .Sum(Function(c) c.Width)
        End If

        g.DrawImage(m_buffHeaderGrouping, nOffset - nHiddenWidth, 0)
    End Sub

    Private Sub DisposeBuffer()
        If m_buffHeaderGrouping IsNot Nothing Then
            m_buffHeaderGrouping.Dispose()
            m_buffHeaderGrouping = Nothing
        End If
    End Sub

    Private Sub ctrlPivotGrid_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles Me.DataBindingComplete
        Try
            If DesignMode Then Exit Try

            'RemoveHeaderHandlers()


            ReDim m_rows(Me.Rows.Count - 1)
            ReDim m_cols(Me.Columns.Count - 1)
            Me.Rows.CopyTo(m_rows, 0)
            Me.Columns.CopyTo(m_cols, 0)

            m_arrFKCols.Clear()

            Dim dt As DataTable = m_dtDynamicDataSource     'TODO Check

            For Each dgvc As DataGridViewColumn In Me.Columns
                dgvc.HeaderCell.SortGlyphDirection = SortOrder.None

                'If dgvc.DataPropertyName <> String.Empty AndAlso _
                '        Not dt.Columns.Contains(dgvc.DataPropertyName) Then
                '    m_arrFKCols.Add(dgvc.DataPropertyName)

                '    If dgvc.HeaderText = dgvc.DataPropertyName.Split(".").Last Then
                '        Titleize(dgvc.HeaderText)
                '    End If
                'End If
                'Titleize(dgvc.HeaderText)

                'If Me.RowCount = 0 Then
                Dim col As DataColumn = Me.DataColumn(dgvc.Index)
                If col Is Nothing Then Continue For

                If dt.PrimaryKey.Contains(col) Then
                    Static fnt As Font = New Font(Me.Font, FontStyle.Bold)
                    dgvc.HeaderCell.Style.Font = fnt
                    dgvc.DefaultCellStyle.BackColor = CLR_PRIMARY_KEY
                    'dgvc.Frozen = True
                End If

                If dgvc.HeaderText = col.ColumnName AndAlso col.ColumnName <> col.Caption Then
                    dgvc.HeaderText = col.Caption
                End If

                Select Case True
                    Case TypeOf dgvc Is DataGridViewTextBoxColumn
                        Dim txt As DataGridViewTextBoxColumn = dgvc
                        Select Case col.DataType
                            Case System.Type.GetType("System.String")
                                'System.Char
                                'System.Guid
                                'System.SByte
                                'System.String
                                If col.MaxLength >= 0 AndAlso txt.MaxInputLength > col.MaxLength Then
                                    txt.MaxInputLength = col.MaxLength
                                End If
                            Case System.Type.GetType("System.Int16"), _
                                     System.Type.GetType("System.Int32"), _
                                     System.Type.GetType("System.Int64")
                                Dim bits As Integer = col.DataType.Name.Replace("Int", "")

                                dgvc.DefaultCellStyle.Format = "N0"


                            Case System.Type.GetType("System.UInt16"), _
                                 System.Type.GetType("System.UInt32"), _
                                 System.Type.GetType("System.UInt64")
                                Dim bits As Integer = col.DataType.Name.Replace("Int", "")
                                Dim nMax As Int64 = Math.Pow(2, bits) - 1
                                Dim nMin As Int64 = 0

                                dgvc.DefaultCellStyle.Format = "N0"
                            Case System.Type.GetType("System.Double")
                                'System.DateTime
                                'System.DateTimeOffset
                                'System.TimeSpan
                                ' Do Nothing
                        End Select
                End Select

                If dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet Then
                    dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    dgvc.Width = dgvc.GetPreferredWidth(DataGridViewAutoSizeColumnMode.DisplayedCells, False)
                End If
            Next dgvc

            ' ''ReDim _RowGroupHashCodes(Me.RowCount - 1)

            ' ''_RowGroupSublings.Clear()

            GetHeaderGroupings()

        Catch ex As Exception
            LogException(ex)
        Finally
            'AddHeaderHandlers()
        End Try
    End Sub

    Private Sub ResetColProps()

        m_arrFKCols.Clear()

        Dim dt As DataTable = Me.DataTable

        For Each dgvc As DataGridViewColumn In Me.Columns
            dgvc.HeaderCell.SortGlyphDirection = SortOrder.None

            'If dgvc.DataPropertyName <> String.Empty AndAlso _
            '        Not dt.Columns.Contains(dgvc.DataPropertyName) Then
            '    m_arrFKCols.Add(dgvc.DataPropertyName)

            '    If dgvc.HeaderText = dgvc.DataPropertyName.Split(".").Last Then
            '        Titleize(dgvc.HeaderText)
            '    End If
            'End If

            Dim col As DataColumn = Me.DataColumn(dgvc.Index)
            If col Is Nothing Then Continue For

            If dt.PrimaryKey.Contains(col) Then
                Static fnt As Font = New Font(Me.Font, FontStyle.Bold)
                dgvc.HeaderCell.Style.Font = fnt
                dgvc.DefaultCellStyle.BackColor = CLR_PRIMARY_KEY
                dgvc.Frozen = True
            End If

            If dgvc.HeaderText = col.ColumnName AndAlso col.ColumnName <> col.Caption Then
                dgvc.HeaderText = col.Caption
            End If

            Select Case True
                Case TypeOf dgvc Is DataGridViewTextBoxColumn
                    Dim txt As DataGridViewTextBoxColumn = dgvc
                    Select Case col.DataType
                        Case System.Type.GetType("System.String")
                            'System.Char
                            'System.Guid
                            'System.SByte
                            'System.String
                            If col.MaxLength >= 0 AndAlso txt.MaxInputLength > col.MaxLength Then
                                txt.MaxInputLength = col.MaxLength
                            End If
                        Case System.Type.GetType("System.Int16"), _
                                 System.Type.GetType("System.Int32"), _
                                 System.Type.GetType("System.Int64")
                            Dim bits As Integer = col.DataType.Name.Replace("Int", "")

                            dgvc.DefaultCellStyle.Format = "N0"


                        Case System.Type.GetType("System.UInt16"), _
                             System.Type.GetType("System.UInt32"), _
                             System.Type.GetType("System.UInt64")
                            Dim bits As Integer = col.DataType.Name.Replace("Int", "")
                            Dim nMax As Int64 = Math.Pow(2, bits) - 1
                            Dim nMin As Int64 = 0

                            dgvc.DefaultCellStyle.Format = "N0"
                        Case System.Type.GetType("System.Double")
                            'System.DateTime
                            'System.DateTimeOffset
                            'System.TimeSpan
                            ' Do Nothing
                    End Select
            End Select

            If dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet Then
                dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                dgvc.Width = dgvc.GetPreferredWidth(DataGridViewAutoSizeColumnMode.DisplayedCells, False)
            End If
        Next dgvc
    End Sub


    Private Sub GetHeaderGroupings()
        If DesignMode Then Exit Sub
        Debug.Print("GetHeaderGroupings")


        m_arrHeaderGroups.Clear()

        If m_cols.Any(Function(c) Regex.IsMatch(c.HeaderText, "[|\n]")) Then
            Dim arrColFlags As New List(Of KeyValuePair(Of Integer, String)())

            Dim cols() As DataGridViewColumn = m_cols.OrderBy(Function(c) c.DisplayIndex).ToArray

            For Each dgvc As DataGridViewColumn In cols
                If dgvc.HeaderText Like "*|*" Then
                    dgvc.HeaderText = dgvc.HeaderText.Replace("|", vbNewLine)
                    'Titleize(dgvc.HeaderText)

                    If Me.ColumnHeadersDefaultCellStyle.Alignment <> DataGridViewContentAlignment.TopCenter Then
                        Me.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
                    End If
                End If
                Titleize(dgvc.HeaderText)

                Dim arrHeaderTexts() As String = dgvc.HeaderText.Split(vbNewLine)

                If arrHeaderTexts.Count > 1 Then
                    Dim nFlags(arrHeaderTexts.Count - 2) As KeyValuePair(Of Integer, String)
                    Dim strHeaderText As String = String.Empty

                    For i As Integer = 0 To nFlags.Count - 1
                        strHeaderText &= arrHeaderTexts(i)
                        nFlags(i) = New KeyValuePair(Of Integer, String)(strHeaderText.GetHashCode, arrHeaderTexts(i).Trim)
                    Next i

                    arrColFlags.Add(nFlags)
                Else
                    arrColFlags.Add({})
                End If
            Next dgvc

            For i As Integer = arrColFlags.Count - 1 To 0 Step -1
                Dim nFlags() As KeyValuePair(Of Integer, String) = arrColFlags(i)
                Dim nCounts(nFlags.Count - 1) As KeyValuePair(Of Integer, String)

                Dim nFlagsNxt() As KeyValuePair(Of Integer, String) = {}
                Dim nCountsNxt() As KeyValuePair(Of Integer, String) = {}

                If i < arrColFlags.Count - 1 Then
                    nFlagsNxt = arrColFlags(i + 1)
                    nCountsNxt = m_arrHeaderGroups.First
                End If

                For j As Integer = 0 To nCounts.Count - 1
                    If i < arrColFlags.Count - 1 AndAlso _
                            j <= nFlagsNxt.Count - 1 AndAlso _
                            nFlags(j).Key = nFlagsNxt(j).Key AndAlso _
                            (j = 0 OrElse nCountsNxt(j - 1).Key <> 1) Then
                        nCounts(j) = New KeyValuePair(Of Integer, String)(nCountsNxt(j).Key + 1, nFlags(j).Value)
                        nCountsNxt(j) = Nothing
                    Else
                        nCounts(j) = New KeyValuePair(Of Integer, String)(1, nFlags(j).Value)
                    End If
                Next j

                m_arrHeaderGroups.Insert(0, nCounts)
            Next i
        Else
            For i As Integer = 0 To Me.Columns.Count - 1
                Titleize(Me.Columns(i).HeaderText)
            Next
        End If
    End Sub

    Public Function ActiveCellStyle(RowIndex As Integer, ColumnIndex As Integer, Optional bIncludeAlternatingStyle As Boolean = False) As DataGridViewCellStyle
        Dim style As New DataGridViewCellStyle()
        Dim arrDefaultCellStyles() As DataGridViewCellStyle = { _
                Me.Rows(RowIndex).Cells(ColumnIndex).Style, _
                Me.Rows(RowIndex).DefaultCellStyle, _
                Me.Columns(ColumnIndex).DefaultCellStyle, _
                Me.AlternatingRowsDefaultCellStyle, _
                Me.DefaultCellStyle
            }

        For Each s As DataGridViewCellStyle In arrDefaultCellStyles
            If s Is Me.AlternatingRowsDefaultCellStyle AndAlso _
                    (Not bIncludeAlternatingStyle OrElse RowIndex Mod 2 = 0) Then
                Continue For
            End If

            With style
                If .Alignment = Nothing Then .Alignment = s.Alignment
                If .BackColor = Nothing Then .BackColor = s.BackColor
                If .ForeColor = Nothing Then .ForeColor = s.ForeColor
                If .Font Is Nothing Then .Font = s.Font
                If .Format = Nothing Then .Format = s.Format
                If .Padding = Nothing Then .Padding = s.Padding
                If .SelectionBackColor = Nothing Then .SelectionBackColor = s.SelectionBackColor
                If .SelectionForeColor = Nothing Then .SelectionForeColor = s.SelectionForeColor
                If .WrapMode = Nothing Then .WrapMode = s.WrapMode
            End With
        Next s

        Return style
    End Function

    Private Function StringFormatFromStyle(style As DataGridViewCellStyle)
        Dim fmt As New StringFormat

        Select Case style.Alignment
            Case DataGridViewContentAlignment.TopLeft, DataGridViewContentAlignment.TopCenter, DataGridViewContentAlignment.TopRight
                fmt.LineAlignment = StringAlignment.Near
            Case DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleRight
                fmt.LineAlignment = StringAlignment.Center
            Case DataGridViewContentAlignment.BottomLeft, DataGridViewContentAlignment.BottomCenter, DataGridViewContentAlignment.BottomRight
                fmt.LineAlignment = StringAlignment.Far
        End Select

        Select Case style.Alignment
            Case DataGridViewContentAlignment.TopLeft, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.BottomLeft
                fmt.Alignment = StringAlignment.Near
            Case DataGridViewContentAlignment.TopCenter, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.BottomCenter
                fmt.Alignment = StringAlignment.Center
            Case DataGridViewContentAlignment.TopRight, DataGridViewContentAlignment.MiddleRight, DataGridViewContentAlignment.BottomRight
                fmt.Alignment = StringAlignment.Far
        End Select

        If style.WrapMode = DataGridViewTriState.False Then
            fmt.FormatFlags = StringFormatFlags.NoWrap
        End If

        Return fmt
    End Function

    Public Function ValueForSQL(ByVal varValue As Object, _
                            Optional ByVal varDelimiter As Object = Nothing, _
                            Optional ByVal blnSupportThLang As Boolean = True) As String
        On Error Resume Next
        Dim strResult As String
        Dim strText As String
        Dim vtType As VariantType

        strResult = "NULL"

        If Not IsNothing(varValue) AndAlso Not IsDBNull(varValue) Then
            vtType = VarType(varValue)

            If Trim(varValue) <> "" Then
                Select Case vtType
                    Case VariantType.String     'Kong change from vbString --> VariantType.String
                        strText = RTrim(varValue)
                        If Asc(Strings.Right(strText, 1)) = 0 Then
                            strText = Strings.Left(strText, Len(strText) - 1)
                        End If
                        If Not IsNothing(varDelimiter) Then
                            strText = strText.Split(varDelimiter)(0)
                        End If

                        strResult = "'" & Replace(strText, "'", "''") & "'"

                        If blnSupportThLang Then
                            strResult = "N" & strResult
                        End If
                    Case VariantType.Date       'Kong change from vbDate --> VariantType.Date
                        strResult = GetDateValueForSQL(varValue)
                        'Debug.Assert(False)  'please use GetDateValueForSQL(dt,cnn) instead.
                    Case VariantType.Boolean    'Kong change from vbBoolean --> VariantType.Boolean
                        strResult = IIf(varValue, "1", "0")
                    Case VariantType.Double     'Kong add for handle Double.NaN
                        If Not Double.IsNaN(varValue) Then
                            strResult = CType(varValue, String)
                        End If
                    Case Else
                        strResult = CType(varValue, String)
                End Select
            End If
        End If

        Return strResult
    End Function

    Public Function GetDateValueForSQL(ByVal varDateTime As Object) As String

        Const cstrDateFormatForSQL As String = "/MM/dd"

        Dim strText As String
        Dim strResult As String

        strResult = ""

        'If IsDBNull(varDateTime) Then
        If IsDBNull(varDateTime) OrElse IsNothing(varDateTime) Then 'Kong 20091109 : add "OrElse IsNothing(varDateTime)"
            strResult = "NULL"
        Else
            strText = CDate(varDateTime).Year & Format(varDateTime, cstrDateFormatForSQL)
            'strText = Replace(strText, " 12:00:00", "")

            strResult = "CONVERT(datetime, '" & strText & "',111)"
        End If

        Return strResult
    End Function

    Public Sub LogException(ByVal ex As Exception)
        MsgBox(ex.Message & vbCrLf & ex.StackTrace, MsgBoxStyle.Exclamation)
    End Sub

    Public Sub Titleize(ByRef strText As String)
        Debug.Print("Titleize " & strText)
        Dim reId As New System.Text.RegularExpressions.Regex(" (id)$")
        Dim reNo As New System.Text.RegularExpressions.Regex(" (no)$")
        Dim reQty As New System.Text.RegularExpressions.Regex("(qty)$")
        Dim reSpace As New System.Text.RegularExpressions.Regex("[ _]+")

        If strText = strText.ToUpper Then
            strText = strText.ToLower
        End If
        strText = reSpace.Replace(strText, " ").Trim
        strText = reId.Replace(strText, "")
        strText = reNo.Replace(strText, " No.")
        strText = reQty.Replace(strText, "Q'ty")

        strText = Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(strText)
    End Sub

    Private Sub ctrlPivotGrid_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        Try
            _initialized = True
            RemoveHeaderHandlers()
            AddHeaderHandlers()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function NumericFormatStringToDataGridViewFormatString(ByVal strNumericFormat As String) As String
        NumericFormatStringToDataGridViewFormatString = ""
        Dim astr As String()
        If IsNumeric(strNumericFormat) Then
            astr = strNumericFormat.Replace(",", "").Split(".")
            If astr.Length > 1 Then
                NumericFormatStringToDataGridViewFormatString = "N" & astr(1).Length
            Else
                NumericFormatStringToDataGridViewFormatString = "N0"
            End If
        End If

    End Function
End Class
