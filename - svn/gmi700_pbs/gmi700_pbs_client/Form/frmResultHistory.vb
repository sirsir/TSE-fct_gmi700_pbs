﻿Public Class frmResultHistory

    Public m_clsLogger As clsLogger = New clsLogger("gmi700_pbs_client")

    Public da1 As fct_gmi700_pbsDataSetTableAdapters.v_Result_HistoryTableAdapter = New fct_gmi700_pbsDataSetTableAdapters.v_Result_HistoryTableAdapter()

    Public dv As DataView
    Public dt1 As fct_gmi700_pbsDataSet.v_Result_HistoryDataTable = New fct_gmi700_pbsDataSet.v_Result_HistoryDataTable

    Private Sub frmResultHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'Fct_gmi700_pbsDataSet.V_RESULT_HISTORY' table. You can move, or remove it, as needed.
        Me.V_Result_HistoryTableAdapter.Fill(Me.Fct_gmi700_pbsDataSet.V_RESULT_HISTORY)

        

    End Sub

    Private Sub frmResultHistory_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        

        DateTimePickerDateFrom.Value = Now
        DateTimePickerDateTo.Value = Now
        DateTimePickerDateFrom.Checked = True
        DateTimePickerDateTo.Checked = False

        DateTimePickerDateTo.Refresh()

        dataFilter()

        
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try

            'AddControls()
            SetupDataObj()
            SetControls()
            'dataFilter()

        Catch ex As Exception
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcDataAndWriteToFile", ex)
        End Try
    End Sub


    Private Sub AddControls()
        'Dim textbox1 As New TextBox
        'textbox1.Name = "Textbox1"
        'textbox1.Size = New Size(170, 20)
        'textbox1.Location = New Point(167, 32)
        'Me.TableLayoutPanel3.Controls.Add(textbox1, 0, 0)

        

        'AddLabelTextbox_to_Table(Me.TableLayoutPanel3, "Code No:", "CodeNo", 0, 0)
        'AddLabelTextbox_to_Table(Me.TableLayoutPanel3, "Model Type:", "ModelType", 2, 0)
        'AddLabelTextbox_to_Table(Me.TableLayoutPanel3, "Result:", "Result", 4, 0)
        'AddLabelTextbox_to_Table(Me.TableLayoutPanel3, "Process Filter:", "ProcessFilter", 0, 1)
        'AddLabelTextbox_to_Table(Me.TableLayoutPanel3, "Date From:", "DateFrom", 2, 1)
        'AddLabelTextbox_to_Table(Me.TableLayoutPanel3, "Date To:", "DateTo", 4, 1)

    End Sub


    'Private Sub AddLabelTextbox_to_Table(tbl1 As TableLayoutPanel, strLable As String, strName As String, intRow As Integer, intCol As Integer)
    '    tbl1.Controls.Add(New Label() With {.Text = strLable, .Anchor = AnchorStyles.Right, .AutoSize = True}, intRow, intCol)
    '    'tbl1.Controls.Add(New ComboBox() With {.Name = strName, .Dock = DockStyle.Fill}, intRow + 1, intCol)
    '    tbl1.Controls.Add(New TextBox() With {.Name = strName, .Dock = DockStyle.Fill}, intRow + 1, intCol)
    'End Sub

    Private Sub SetupDataObj()

        'dt1 = New fct_gmi700_pbsDataSet.V_RESULT_HISTORYDataTable()
        da1.Fill(dt1)
        'fct_gmi700_pbsDataSetTableAdapters.V_RESULT_HISTORYTableAdapter()
        'fct_gmi700_pbsDataSetTableAdapters.V_RESULT_HISTORYTableAdapter.

        'dv = New DataView(ds.Tables(0), "type = 'business' ", "type Desc", DataViewRowState.CurrentRows)

        dv = New DataView(dt1)

        CtrlPivotGrid1.DataSource = dv
    End Sub

    Private Sub SetControls()

        'Me.TableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset
        'Me.TableLayoutPanel3.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset

        ComboBoxPartNo.ResetText()
        ComboBoxResult.ResetText()
        ComboBoxModelType.ResetText()


        '--- Set ComboBoxModelType
        ComboBoxModelType.Items.Clear()
        ComboBoxModelType.Items.Add("")

        Dim daTemp = New fct_gmi700_pbsDataSetTableAdapters.RESULTTableAdapter()


        Dim dtTemp = New fct_gmi700_pbsDataSet.RESULTDataTable
        daTemp.Fill(dtTemp)


        For Each r As fct_gmi700_pbsDataSet.RESULTRow In dtTemp.Rows
            If Not ComboBoxModelType.Items.Contains(r.MODEL_TYPE) Then
                ComboBoxModelType.Items.Add(r.MODEL_TYPE)
            End If

        Next

        '--- Set ComboBoxPartNo
        ComboBoxPartNo.Items.Clear()
        ComboBoxPartNo.Items.Add("")
        Dim daTemp3 = New fct_gmi700_pbsDataSetTableAdapters.PART_NO_MAPPINGTableAdapter


        Dim dtTemp3 = New fct_gmi700_pbsDataSet.PART_NO_MAPPINGDataTable
        daTemp3.Fill(dtTemp3)


        For Each r As fct_gmi700_pbsDataSet.PART_NO_MAPPINGRow In dtTemp3.Rows
            If Not ComboBoxPartNo.Items.Contains(r.PART_NO) Then
                ComboBoxPartNo.Items.Add(r.PART_NO)
            End If

        Next


        '--- Set COmboBoxResult
        ComboBoxResult.Items.Clear()
        ComboBoxResult.Items.Add("")

        Dim daTemp2 = New fct_gmi700_pbsDataSetTableAdapters.RESULTTableAdapter()


        Dim dtTemp2 = New fct_gmi700_pbsDataSet.RESULTDataTable
        daTemp2.Fill(dtTemp2)

        For Each r As fct_gmi700_pbsDataSet.RESULTRow In dtTemp2.Rows
            If Not r.JUDGE_RESULT Is Nothing And r.JUDGE_RESULT.Replace(Convert.ToChar(0), "").Trim <> "" And Not ComboBoxResult.Items.Contains(r.JUDGE_RESULT.ToUpper) Then
                ComboBoxResult.Items.Add(r.JUDGE_RESULT.ToUpper)
            End If

        Next
        'ComboBoxModelType.Refresh()


        'DateTimePickerDateFrom.Checked = False
        'DateTimePickerDateTo.Checked = False

        'DateTimePickerDateFrom.Value = Now
        'DateTimePickerDateTo.Value = Now



    End Sub


    Private Sub dataFilter()
        'dv.RowFilter = "[Code No]='14000002'"



        Dim strFilter = ""

        If Me.TextBoxCodeNo.Text <> String.Empty Then
            strFilter = String.Format("[Code No] LIKE '{0}'", Me.TextBoxCodeNo.Text)
            'dv.RowFilter = String.Format("[Code No] LIKE '{0}' AND [Type]='RH'", Me.TextboxCodeNo.Text)

        End If

        If Me.ComboBoxPartNo.Text <> String.Empty Then

            If strFilter = String.Empty Then
                strFilter = String.Format("[Part No] LIKE '{0}'", Me.ComboBoxPartNo.Text)

            Else
                strFilter = String.Format("{1} AND [Part No] LIKE '{0}'", Me.ComboBoxPartNo.Text, strFilter)
            End If


        End If

        If Me.ComboBoxModelType.Text <> String.Empty Then

            If strFilter = String.Empty Then
                strFilter = String.Format("[Type] LIKE '{0}'", Me.ComboBoxModelType.Text)

            Else
                strFilter = String.Format("{1} AND [Type] LIKE '{0}'", Me.ComboBoxModelType.Text, strFilter)
            End If


        End If

        If Me.ComboBoxResult.Text <> String.Empty Then

            If strFilter = String.Empty Then
                strFilter = String.Format("([Injection|Result] LIKE '{0}' OR [Tear Cut|Result] LIKE '{0}' OR [Vibration|Result] LIKE '{0}' OR [Assy|Result] LIKE '{0}')", Me.ComboBoxResult.Text)

            Else
                strFilter = String.Format("{1} AND ([Injection|Result] LIKE '{0}' OR [Tear Cut|Result] LIKE '{0}' OR [Vibration|Result] LIKE '{0}' OR [Assy|Result] LIKE '{0}')", Me.ComboBoxResult.Text, strFilter)
            End If


        End If

        'If Me.DateTimePickerDateFrom.Text <> String.Empty Then

        If DateTimePickerDateFrom.Checked Then

            If strFilter = String.Empty Then
                strFilter = String.Format("([Injection|When]>='{0}' OR [Tear Cut|When]>='{0}' OR [Vibration|When]>='{0}' OR [Assy|When]>='{0}')", Me.DateTimePickerDateFrom.Text)

            Else
                strFilter = String.Format("{1} AND ([Injection|When]>='{0}' OR [Tear Cut|When]>='{0}' OR [Vibration|When]>='{0}' OR [Assy|When]>='{0}')", Me.DateTimePickerDateFrom.Text, strFilter)
            End If


        End If

        'If Me.DateTimePickerDateTo.Text <> String.Empty Then
        If DateTimePickerDateTo.Checked Then

            If strFilter = String.Empty Then
                'strFilter = String.Format("([Injection|When]<='{0}' OR [Tear Cut|When]<='{0}' OR [Vibration|When]<='{0}' OR [Assy|When]<='{0}')", DateTime.Parse(Me.DateTimePickerDateTo.Text).AddDays(1))
                strFilter = String.Format("([Injection|When]<='{0}' OR [Tear Cut|When]<='{0}' OR [Vibration|When]<='{0}' OR [Assy|When]<='{0}')", Me.DateTimePickerDateTo.Value.AddDays(1))

            Else
                'strFilter = String.Format("{1} AND ([Injection|When]<='{0}' OR [Tear Cut|When]<='{0}' OR [Vibration|When]<='{0}' OR [Assy|When]<='{0}')", DateTime.Parse(Me.DateTimePickerDateTo.Text).AddDays(1), strFilter)
                strFilter = String.Format("{1} AND ([Injection|When]<='{0}' OR [Tear Cut|When]<='{0}' OR [Vibration|When]<='{0}' OR [Assy|When]<='{0}')", Me.DateTimePickerDateTo.Value.AddDays(1), strFilter)
            End If


        End If



        'MsgBox(dv.Table.Columns.Item(0).ToString)
        'dv.RowFilter = "Type='RH'"

        dv.RowFilter = strFilter
    End Sub

    

    



    Private Sub ButtonFilter_Click(sender As Object, e As EventArgs) Handles ButtonFilter.Click
        Try
            dataFilter()
        Catch ex As Exception
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcDataAndWriteToFile", ex)
        End Try

    End Sub

    Private Sub dataRefresh()
        CtrlPivotGrid1.DataSource = Nothing

        dt1.Clear()
        da1.Fill(dt1)
        dv = New DataView(dt1)

        CtrlPivotGrid1.DataSource = dv

        'dv.RowFilter = [String].Empty
    End Sub

    


    Private Sub ButtonRefresh_Click(sender As Object, e As EventArgs) Handles ButtonRefresh.Click
        Try
            dataRefresh()
            dataFilter()
        Catch ex As Exception
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcDataAndWriteToFile", ex)
        End Try
    End Sub

    Private Sub ButtonExit_Click(sender As Object, e As EventArgs) Handles ButtonExit.Click
        Try
            Application.Exit()
        Catch ex As Exception
            m_clsLogger.AppendLog(Me.GetType.Name, "ReadPlcDataAndWriteToFile", ex)
        End Try
    End Sub

    
End Class