﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResultHistory
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonFilter = New System.Windows.Forms.Button()
        Me.ButtonRefresh = New System.Windows.Forms.Button()
        Me.ButtonExit = New System.Windows.Forms.Button()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxCodeNo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DateTimePickerDateFrom = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerDateTo = New System.Windows.Forms.DateTimePicker()
        Me.ComboBoxPartNo = New System.Windows.Forms.ComboBox()
        Me.ComboBoxResult = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelPartNo = New System.Windows.Forms.Label()
        Me.Fct_gmi700_pbsDataSet = New gmi700_pbs_client.fct_gmi700_pbsDataSet()
        Me.VResultHistoryBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.V_Result_HistoryTableAdapter = New gmi700_pbs_client.fct_gmi700_pbsDataSetTableAdapters.V_RESULT_HISTORYTableAdapter()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.ComboBoxModelType = New System.Windows.Forms.ComboBox()
        Me.CtrlPivotGrid1 = New gmi700_pbs_client.ctrlPivotGrid()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.Fct_gmi700_pbsDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VResultHistoryBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlPivotGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.YellowGreen
        Me.Panel1.Controls.Add(Me.TableLayoutPanel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1206, 29)
        Me.Panel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.AutoSize = True
        Me.TableLayoutPanel2.ColumnCount = 9
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.88262!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.983655!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label6, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1206, 29)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(142, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "GMI700 - Result History"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonFilter, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonRefresh, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonExit, 2, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 29)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1206, 32)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'ButtonFilter
        '
        Me.ButtonFilter.AutoSize = True
        Me.ButtonFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonFilter.Location = New System.Drawing.Point(0, 0)
        Me.ButtonFilter.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonFilter.Name = "ButtonFilter"
        Me.ButtonFilter.Size = New System.Drawing.Size(160, 32)
        Me.ButtonFilter.TabIndex = 1
        Me.ButtonFilter.Text = "Filter"
        Me.ButtonFilter.UseVisualStyleBackColor = True
        '
        'ButtonRefresh
        '
        Me.ButtonRefresh.AutoSize = True
        Me.ButtonRefresh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonRefresh.Location = New System.Drawing.Point(160, 0)
        Me.ButtonRefresh.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonRefresh.Name = "ButtonRefresh"
        Me.ButtonRefresh.Size = New System.Drawing.Size(160, 32)
        Me.ButtonRefresh.TabIndex = 2
        Me.ButtonRefresh.Text = "Refresh"
        Me.ButtonRefresh.UseVisualStyleBackColor = True
        '
        'ButtonExit
        '
        Me.ButtonExit.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExit.AutoSize = True
        Me.ButtonExit.Location = New System.Drawing.Point(320, 0)
        Me.ButtonExit.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonExit.Name = "ButtonExit"
        Me.ButtonExit.Size = New System.Drawing.Size(160, 32)
        Me.ButtonExit.TabIndex = 5
        Me.ButtonExit.Text = "Exit"
        Me.ButtonExit.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 10
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.99337!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.711443!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.74461!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.638474!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.950249!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.716418!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.897181!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label3, 4, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label2, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.TextBoxCodeNo, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label5, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label7, 2, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.DateTimePickerDateFrom, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.DateTimePickerDateTo, 3, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.ComboBoxResult, 5, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelPartNo, 4, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.ComboBoxPartNo, 5, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.ComboBoxModelType, 3, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 61)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1206, 64)
        Me.TableLayoutPanel3.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(670, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 32)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Result:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(362, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 32)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Model Type:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(66, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Code No:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBoxCodeNo
        '
        Me.TextBoxCodeNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCodeNo.Location = New System.Drawing.Point(124, 3)
        Me.TextBoxCodeNo.Name = "TextBoxCodeNo"
        Me.TextBoxCodeNo.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxCodeNo.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(59, 32)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 32)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Date From:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(379, 32)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 32)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Date To:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DateTimePickerDateFrom
        '
        Me.DateTimePickerDateFrom.Location = New System.Drawing.Point(124, 35)
        Me.DateTimePickerDateFrom.Name = "DateTimePickerDateFrom"
        Me.DateTimePickerDateFrom.ShowCheckBox = True
        Me.DateTimePickerDateFrom.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePickerDateFrom.TabIndex = 11
        '
        'DateTimePickerDateTo
        '
        Me.DateTimePickerDateTo.Location = New System.Drawing.Point(434, 35)
        Me.DateTimePickerDateTo.Name = "DateTimePickerDateTo"
        Me.DateTimePickerDateTo.ShowCheckBox = True
        Me.DateTimePickerDateTo.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePickerDateTo.TabIndex = 12
        '
        'ComboBoxPartNo
        '
        Me.ComboBoxPartNo.FormattingEnabled = True
        Me.ComboBoxPartNo.Location = New System.Drawing.Point(716, 35)
        Me.ComboBoxPartNo.Name = "ComboBoxPartNo"
        Me.ComboBoxPartNo.Size = New System.Drawing.Size(114, 21)
        Me.ComboBoxPartNo.TabIndex = 13
        '
        'ComboBoxResult
        '
        Me.ComboBoxResult.FormattingEnabled = True
        Me.ComboBoxResult.Location = New System.Drawing.Point(716, 3)
        Me.ComboBoxResult.Name = "ComboBoxResult"
        Me.ComboBoxResult.Size = New System.Drawing.Size(114, 21)
        Me.ComboBoxResult.TabIndex = 14
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.90909!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455!))
        Me.TableLayoutPanel4.Controls.Add(Me.CtrlPivotGrid1, 1, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 125)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 3
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545455!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.90909!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545455!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(1206, 346)
        Me.TableLayoutPanel4.TabIndex = 4
        '
        'LabelPartNo
        '
        Me.LabelPartNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelPartNo.AutoSize = True
        Me.LabelPartNo.Location = New System.Drawing.Point(664, 32)
        Me.LabelPartNo.Name = "LabelPartNo"
        Me.LabelPartNo.Size = New System.Drawing.Size(46, 32)
        Me.LabelPartNo.TabIndex = 0
        Me.LabelPartNo.Text = "Part No:"
        Me.LabelPartNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Fct_gmi700_pbsDataSet
        '
        Me.Fct_gmi700_pbsDataSet.DataSetName = "fct_gmi700_pbsDataSet"
        Me.Fct_gmi700_pbsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'VResultHistoryBindingSource
        '
        Me.VResultHistoryBindingSource.DataMember = "v_Result_History"
        Me.VResultHistoryBindingSource.DataSource = Me.Fct_gmi700_pbsDataSet
        '
        'V_Result_HistoryTableAdapter
        '
        Me.V_Result_HistoryTableAdapter.ClearBeforeFill = True
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(434, 68)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(114, 21)
        Me.ComboBox1.TabIndex = 13
        '
        'ComboBoxModelType
        '
        Me.ComboBoxModelType.FormattingEnabled = True
        Me.ComboBoxModelType.Location = New System.Drawing.Point(434, 3)
        Me.ComboBoxModelType.Name = "ComboBoxModelType"
        Me.ComboBoxModelType.Size = New System.Drawing.Size(114, 21)
        Me.ComboBoxModelType.TabIndex = 13
        '
        'CtrlPivotGrid1
        '
        Me.CtrlPivotGrid1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.CtrlPivotGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.CtrlPivotGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlPivotGrid1.GroupedDuplicateCells = False
        Me.CtrlPivotGrid1.HideDuplicateColumns = 0
        Me.CtrlPivotGrid1.Location = New System.Drawing.Point(57, 18)
        Me.CtrlPivotGrid1.Name = "CtrlPivotGrid1"
        Me.CtrlPivotGrid1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.CtrlPivotGrid1.Size = New System.Drawing.Size(1090, 308)
        Me.CtrlPivotGrid1.TabIndex = 0
        '
        'frmResultHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1206, 471)
        Me.Controls.Add(Me.TableLayoutPanel4)
        Me.Controls.Add(Me.TableLayoutPanel3)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Name = "frmResultHistory"
        Me.Text = "frmResultHistory"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        CType(Me.Fct_gmi700_pbsDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VResultHistoryBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlPivotGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Fct_gmi700_pbsDataSet As gmi700_pbs_client.fct_gmi700_pbsDataSet
    Friend WithEvents VResultHistoryBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents V_Result_HistoryTableAdapter As gmi700_pbs_client.fct_gmi700_pbsDataSetTableAdapters.v_Result_HistoryTableAdapter
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CtrlPivotGrid1 As gmi700_pbs_client.ctrlPivotGrid
    Friend WithEvents ButtonFilter As System.Windows.Forms.Button
    Friend WithEvents ButtonRefresh As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCodeNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePickerDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents ComboBoxPartNo As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonExit As System.Windows.Forms.Button
    Friend WithEvents ComboBoxResult As System.Windows.Forms.ComboBox
    Friend WithEvents LabelPartNo As System.Windows.Forms.Label
    Friend WithEvents ComboBoxModelType As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
End Class
