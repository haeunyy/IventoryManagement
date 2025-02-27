<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기에서는 수정하지 마세요.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.보험약 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.grid_기준처방 = New System.Windows.Forms.DataGridView()
        Me.grid_Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grid_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grid_MediCompany = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grid_Count = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.약재정보 = New System.Windows.Forms.GroupBox()
        Me.grid_보험약 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_idx = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txt_Price = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btn_Update = New System.Windows.Forms.Button()
        Me.btn_Save = New System.Windows.Forms.Button()
        Me.btn_delete = New System.Windows.Forms.Button()
        Me.btn_New = New System.Windows.Forms.Button()
        Me.dtp_Received = New System.Windows.Forms.DateTimePicker()
        Me.txt_count = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_mediCompany = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_Code = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_Name = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.치료재료대 = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabControl1.SuspendLayout()
        Me.보험약.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grid_기준처방, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.약재정보.SuspendLayout()
        CType(Me.grid_보험약, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.치료재료대.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.보험약)
        Me.TabControl1.Controls.Add(Me.치료재료대)
        Me.TabControl1.Location = New System.Drawing.Point(10, 11)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(795, 603)
        Me.TabControl1.TabIndex = 5
        '
        '보험약
        '
        Me.보험약.Controls.Add(Me.GroupBox2)
        Me.보험약.Controls.Add(Me.약재정보)
        Me.보험약.Location = New System.Drawing.Point(4, 22)
        Me.보험약.Margin = New System.Windows.Forms.Padding(2)
        Me.보험약.Name = "보험약"
        Me.보험약.Padding = New System.Windows.Forms.Padding(2)
        Me.보험약.Size = New System.Drawing.Size(787, 577)
        Me.보험약.TabIndex = 1
        Me.보험약.Text = "보험약"
        Me.보험약.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.grid_기준처방)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(763, 291)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "기준처방"
        '
        'grid_기준처방
        '
        Me.grid_기준처방.AllowUserToAddRows = False
        Me.grid_기준처방.AllowUserToDeleteRows = False
        Me.grid_기준처방.AllowUserToOrderColumns = True
        Me.grid_기준처방.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grid_기준처방.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.grid_Code, Me.grid_Name, Me.grid_MediCompany, Me.grid_Count})
        Me.grid_기준처방.Location = New System.Drawing.Point(17, 20)
        Me.grid_기준처방.Name = "grid_기준처방"
        Me.grid_기준처방.ReadOnly = True
        Me.grid_기준처방.RowTemplate.Height = 23
        Me.grid_기준처방.Size = New System.Drawing.Size(730, 257)
        Me.grid_기준처방.TabIndex = 2
        '
        'grid_Code
        '
        Me.grid_Code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.grid_Code.DataPropertyName = "기준코드"
        Me.grid_Code.HeaderText = "코드"
        Me.grid_Code.Name = "grid_Code"
        Me.grid_Code.ReadOnly = True
        Me.grid_Code.Width = 54
        '
        'grid_Name
        '
        Me.grid_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.grid_Name.DataPropertyName = "기준코드명칭"
        Me.grid_Name.HeaderText = "명칭"
        Me.grid_Name.Name = "grid_Name"
        Me.grid_Name.ReadOnly = True
        Me.grid_Name.Width = 54
        '
        'grid_MediCompany
        '
        Me.grid_MediCompany.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.grid_MediCompany.DataPropertyName = "기준코드제약사"
        Me.grid_MediCompany.HeaderText = "제약사"
        Me.grid_MediCompany.Name = "grid_MediCompany"
        Me.grid_MediCompany.ReadOnly = True
        Me.grid_MediCompany.Width = 66
        '
        'grid_Count
        '
        Me.grid_Count.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.grid_Count.DataPropertyName = "수량"
        Me.grid_Count.HeaderText = "수량"
        Me.grid_Count.Name = "grid_Count"
        Me.grid_Count.ReadOnly = True
        Me.grid_Count.Visible = False
        '
        '약재정보
        '
        Me.약재정보.Controls.Add(Me.grid_보험약)
        Me.약재정보.Controls.Add(Me.txt_Price)
        Me.약재정보.Controls.Add(Me.Label6)
        Me.약재정보.Controls.Add(Me.btn_Update)
        Me.약재정보.Controls.Add(Me.btn_Save)
        Me.약재정보.Controls.Add(Me.btn_delete)
        Me.약재정보.Controls.Add(Me.btn_New)
        Me.약재정보.Controls.Add(Me.dtp_Received)
        Me.약재정보.Controls.Add(Me.txt_count)
        Me.약재정보.Controls.Add(Me.Label5)
        Me.약재정보.Controls.Add(Me.Label4)
        Me.약재정보.Controls.Add(Me.txt_mediCompany)
        Me.약재정보.Controls.Add(Me.Label3)
        Me.약재정보.Controls.Add(Me.txt_Code)
        Me.약재정보.Controls.Add(Me.Label2)
        Me.약재정보.Controls.Add(Me.txt_Name)
        Me.약재정보.Controls.Add(Me.Label1)
        Me.약재정보.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.약재정보.Location = New System.Drawing.Point(12, 305)
        Me.약재정보.Name = "약재정보"
        Me.약재정보.Size = New System.Drawing.Size(763, 267)
        Me.약재정보.TabIndex = 5
        Me.약재정보.TabStop = False
        Me.약재정보.Text = "약재정보"
        '
        'grid_보험약
        '
        Me.grid_보험약.AllowUserToAddRows = False
        Me.grid_보험약.AllowUserToDeleteRows = False
        Me.grid_보험약.AllowUserToOrderColumns = True
        Me.grid_보험약.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grid_보험약.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.col_idx})
        Me.grid_보험약.Location = New System.Drawing.Point(284, 19)
        Me.grid_보험약.Name = "grid_보험약"
        Me.grid_보험약.ReadOnly = True
        Me.grid_보험약.RowTemplate.Height = 23
        Me.grid_보험약.Size = New System.Drawing.Size(463, 232)
        Me.grid_보험약.TabIndex = 21
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "기준코드"
        Me.DataGridViewTextBoxColumn1.HeaderText = "코드"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 54
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "기준코드명칭"
        Me.DataGridViewTextBoxColumn2.HeaderText = "명칭"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 54
        '
        'DataGridViewTextBoxColumn
        '
        Me.DataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn.DataPropertyName = "입고일자"
        Me.DataGridViewTextBoxColumn.HeaderText = "입고일자"
        Me.DataGridViewTextBoxColumn.Name = "DataGridViewTextBoxColumn"
        Me.DataGridViewTextBoxColumn.ReadOnly = True
        Me.DataGridViewTextBoxColumn.Width = 78
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "수량"
        Me.DataGridViewTextBoxColumn4.HeaderText = "수량"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 54
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "단가"
        Me.DataGridViewTextBoxColumn5.HeaderText = "단가"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 54
        '
        'col_idx
        '
        Me.col_idx.DataPropertyName = "인덱스"
        Me.col_idx.HeaderText = "인덱스"
        Me.col_idx.Name = "col_idx"
        Me.col_idx.ReadOnly = True
        Me.col_idx.Visible = False
        '
        'txt_Price
        '
        Me.txt_Price.Location = New System.Drawing.Point(78, 170)
        Me.txt_Price.Name = "txt_Price"
        Me.txt_Price.Size = New System.Drawing.Size(182, 21)
        Me.txt_Price.TabIndex = 20
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 177)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "단가"
        '
        'btn_Update
        '
        Me.btn_Update.Location = New System.Drawing.Point(77, 223)
        Me.btn_Update.Name = "btn_Update"
        Me.btn_Update.Size = New System.Drawing.Size(63, 23)
        Me.btn_Update.TabIndex = 15
        Me.btn_Update.Text = "수정"
        Me.btn_Update.UseVisualStyleBackColor = True
        Me.btn_Update.Visible = False
        '
        'btn_Save
        '
        Me.btn_Save.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btn_Save.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.btn_Save.Location = New System.Drawing.Point(216, 223)
        Me.btn_Save.Name = "btn_Save"
        Me.btn_Save.Size = New System.Drawing.Size(63, 23)
        Me.btn_Save.TabIndex = 17
        Me.btn_Save.Text = "저장"
        Me.btn_Save.UseVisualStyleBackColor = False
        '
        'btn_delete
        '
        Me.btn_delete.Location = New System.Drawing.Point(146, 223)
        Me.btn_delete.Name = "btn_delete"
        Me.btn_delete.Size = New System.Drawing.Size(63, 23)
        Me.btn_delete.TabIndex = 16
        Me.btn_delete.Text = "삭제"
        Me.btn_delete.UseVisualStyleBackColor = True
        Me.btn_delete.Visible = False
        '
        'btn_New
        '
        Me.btn_New.Location = New System.Drawing.Point(8, 223)
        Me.btn_New.Name = "btn_New"
        Me.btn_New.Size = New System.Drawing.Size(63, 23)
        Me.btn_New.TabIndex = 4
        Me.btn_New.Text = "새로입력"
        Me.btn_New.UseVisualStyleBackColor = True
        '
        'dtp_Received
        '
        Me.dtp_Received.CausesValidation = False
        Me.dtp_Received.Location = New System.Drawing.Point(78, 117)
        Me.dtp_Received.Name = "dtp_Received"
        Me.dtp_Received.Size = New System.Drawing.Size(182, 21)
        Me.dtp_Received.TabIndex = 14
        Me.dtp_Received.Value = New Date(2025, 2, 25, 0, 0, 0, 0)
        '
        'txt_count
        '
        Me.txt_count.Location = New System.Drawing.Point(78, 143)
        Me.txt_count.Name = "txt_count"
        Me.txt_count.Size = New System.Drawing.Size(182, 21)
        Me.txt_count.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 149)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 12)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "수량(g)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 122)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "입고일자"
        '
        'txt_mediCompany
        '
        Me.txt_mediCompany.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txt_mediCompany.Location = New System.Drawing.Point(78, 89)
        Me.txt_mediCompany.Name = "txt_mediCompany"
        Me.txt_mediCompany.ReadOnly = True
        Me.txt_mediCompany.Size = New System.Drawing.Size(182, 21)
        Me.txt_mediCompany.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 95)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "제약사"
        '
        'txt_Code
        '
        Me.txt_Code.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txt_Code.Location = New System.Drawing.Point(78, 62)
        Me.txt_Code.Name = "txt_Code"
        Me.txt_Code.ReadOnly = True
        Me.txt_Code.Size = New System.Drawing.Size(182, 21)
        Me.txt_Code.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "코드"
        '
        'txt_Name
        '
        Me.txt_Name.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txt_Name.Location = New System.Drawing.Point(78, 35)
        Me.txt_Name.Name = "txt_Name"
        Me.txt_Name.ReadOnly = True
        Me.txt_Name.Size = New System.Drawing.Size(182, 21)
        Me.txt_Name.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 12)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "명칭"
        '
        '치료재료대
        '
        Me.치료재료대.Controls.Add(Me.GroupBox1)
        Me.치료재료대.Controls.Add(Me.GroupBox3)
        Me.치료재료대.Location = New System.Drawing.Point(4, 22)
        Me.치료재료대.Margin = New System.Windows.Forms.Padding(2)
        Me.치료재료대.Name = "치료재료대"
        Me.치료재료대.Padding = New System.Windows.Forms.Padding(2)
        Me.치료재료대.Size = New System.Drawing.Size(787, 577)
        Me.치료재료대.TabIndex = 2
        Me.치료재료대.Text = "치료재료대"
        Me.치료재료대.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DataGridView1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(763, 291)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "치료재료대"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8})
        Me.DataGridView1.Location = New System.Drawing.Point(17, 20)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(730, 257)
        Me.DataGridView1.TabIndex = 2
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.DataGridView2)
        Me.GroupBox3.Controls.Add(Me.TextBox2)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Button2)
        Me.GroupBox3.Controls.Add(Me.Button3)
        Me.GroupBox3.Controls.Add(Me.Button4)
        Me.GroupBox3.Controls.Add(Me.Button5)
        Me.GroupBox3.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox3.Controls.Add(Me.TextBox3)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.TextBox4)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.TextBox5)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.TextBox6)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GroupBox3.Location = New System.Drawing.Point(12, 305)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(763, 267)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "재료대정보"
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AllowUserToOrderColumns = True
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn9, Me.DataGridViewTextBoxColumn10, Me.DataGridViewTextBoxColumn11, Me.DataGridViewTextBoxColumn12, Me.DataGridViewTextBoxColumn13, Me.DataGridViewTextBoxColumn14})
        Me.DataGridView2.Location = New System.Drawing.Point(284, 19)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.RowTemplate.Height = 23
        Me.DataGridView2.Size = New System.Drawing.Size(463, 232)
        Me.DataGridView2.TabIndex = 21
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "기준코드"
        Me.DataGridViewTextBoxColumn9.HeaderText = "코드"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Width = 54
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "기준코드명칭"
        Me.DataGridViewTextBoxColumn10.HeaderText = "명칭"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Width = 54
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "입고일자"
        Me.DataGridViewTextBoxColumn11.HeaderText = "입고일자"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Width = 78
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "수량"
        Me.DataGridViewTextBoxColumn12.HeaderText = "수량"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Width = 54
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "단가"
        Me.DataGridViewTextBoxColumn13.HeaderText = "단가"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.Width = 54
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "인덱스"
        Me.DataGridViewTextBoxColumn14.HeaderText = "인덱스"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.Visible = False
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(78, 170)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(182, 21)
        Me.TextBox2.TabIndex = 20
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(15, 177)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 12)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "단가"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(77, 223)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(63, 23)
        Me.Button2.TabIndex = 15
        Me.Button2.Text = "수정"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Button3
        '
        Me.Button3.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.Button3.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Button3.Location = New System.Drawing.Point(216, 223)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(63, 23)
        Me.Button3.TabIndex = 17
        Me.Button3.Text = "저장"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(146, 223)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(63, 23)
        Me.Button4.TabIndex = 16
        Me.Button4.Text = "삭제"
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(8, 223)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(63, 23)
        Me.Button5.TabIndex = 4
        Me.Button5.Text = "새로입력"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CausesValidation = False
        Me.DateTimePicker1.Location = New System.Drawing.Point(78, 117)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(182, 21)
        Me.DateTimePicker1.TabIndex = 14
        Me.DateTimePicker1.Value = New Date(2025, 2, 25, 0, 0, 0, 0)
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(78, 143)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(182, 21)
        Me.TextBox3.TabIndex = 13
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(15, 149)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(29, 12)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "수량"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(15, 122)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 12)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "입고일자"
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.TextBox4.Location = New System.Drawing.Point(78, 89)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ReadOnly = True
        Me.TextBox4.Size = New System.Drawing.Size(182, 21)
        Me.TextBox4.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(15, 95)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(41, 12)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "제조사"
        '
        'TextBox5
        '
        Me.TextBox5.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.TextBox5.Location = New System.Drawing.Point(78, 62)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.ReadOnly = True
        Me.TextBox5.Size = New System.Drawing.Size(182, 21)
        Me.TextBox5.TabIndex = 7
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(15, 69)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 12)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "처방코드"
        '
        'TextBox6
        '
        Me.TextBox6.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.TextBox6.Location = New System.Drawing.Point(78, 35)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.ReadOnly = True
        Me.TextBox6.Size = New System.Drawing.Size(182, 21)
        Me.TextBox6.TabIndex = 5
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(15, 41)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(29, 12)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "명칭"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "기준코드"
        Me.DataGridViewTextBoxColumn3.HeaderText = "코드"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 54
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "기준코드명칭"
        Me.DataGridViewTextBoxColumn6.HeaderText = "명칭"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 54
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "기준코드제약사"
        Me.DataGridViewTextBoxColumn7.HeaderText = "제조사"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Width = 66
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "수량"
        Me.DataGridViewTextBoxColumn8.HeaderText = "수량"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Visible = False
        Me.DataGridViewTextBoxColumn8.Width = 54
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(817, 625)
        Me.Controls.Add(Me.TabControl1)
        Me.KeyPreview = True
        Me.Name = "Form1"
        Me.Text = "재고관리"
        Me.TabControl1.ResumeLayout(False)
        Me.보험약.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grid_기준처방, System.ComponentModel.ISupportInitialize).EndInit()
        Me.약재정보.ResumeLayout(False)
        Me.약재정보.PerformLayout()
        CType(Me.grid_보험약, System.ComponentModel.ISupportInitialize).EndInit()
        Me.치료재료대.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents 보험약 As TabPage
    Friend WithEvents 치료재료대 As TabPage
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents grid_기준처방 As DataGridView
    Friend WithEvents grid_Code As DataGridViewTextBoxColumn
    Friend WithEvents grid_Name As DataGridViewTextBoxColumn
    Friend WithEvents grid_MediCompany As DataGridViewTextBoxColumn
    Friend WithEvents grid_Count As DataGridViewTextBoxColumn
    Friend WithEvents 약재정보 As GroupBox
    Friend WithEvents grid_보험약 As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents col_idx As DataGridViewTextBoxColumn
    Friend WithEvents txt_Price As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents btn_Update As Button
    Friend WithEvents btn_Save As Button
    Friend WithEvents btn_delete As Button
    Friend WithEvents btn_New As Button
    Friend WithEvents dtp_Received As DateTimePicker
    Friend WithEvents txt_count As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txt_mediCompany As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txt_Code As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_Name As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn9 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As DataGridViewTextBoxColumn
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
End Class
