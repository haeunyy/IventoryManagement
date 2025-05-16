<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기에서는 수정하지 마세요.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form2))
        Me.tab_재고현황 = New System.Windows.Forms.TabControl()
        Me.tp_혼합제 = New System.Windows.Forms.TabPage()
        Me.chk_혼합 = New System.Windows.Forms.CheckBox()
        Me.rb_전체항목_혼합 = New System.Windows.Forms.RadioButton()
        Me.rb_기준항목_혼합 = New System.Windows.Forms.RadioButton()
        Me.btn_조회_혼합 = New System.Windows.Forms.Button()
        Me.gb_1_3 = New System.Windows.Forms.GroupBox()
        Me.gb_1_2 = New System.Windows.Forms.GroupBox()
        Me.gb_1_1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtp_시작일자_혼합제 = New System.Windows.Forms.DateTimePicker()
        Me.dtp_종료일자_혼합제 = New System.Windows.Forms.DateTimePicker()
        Me.tp_단미제 = New System.Windows.Forms.TabPage()
        Me.gb_2_1 = New System.Windows.Forms.GroupBox()
        Me.chk_단미 = New System.Windows.Forms.CheckBox()
        Me.rb_전체항목_단미 = New System.Windows.Forms.RadioButton()
        Me.rb_기준항목_단미 = New System.Windows.Forms.RadioButton()
        Me.btn_조회_단미 = New System.Windows.Forms.Button()
        Me.gb_2_3 = New System.Windows.Forms.GroupBox()
        Me.gb_2_2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtp_시작일자_단미 = New System.Windows.Forms.DateTimePicker()
        Me.dtp_종료일자_단미 = New System.Windows.Forms.DateTimePicker()
        Me.tp_치료대 = New System.Windows.Forms.TabPage()
        Me.chk_치료대 = New System.Windows.Forms.CheckBox()
        Me.rb_전체항목_치료대 = New System.Windows.Forms.RadioButton()
        Me.rb_기준항목_치료대 = New System.Windows.Forms.RadioButton()
        Me.btn_조회_치료대 = New System.Windows.Forms.Button()
        Me.gb_3_3 = New System.Windows.Forms.GroupBox()
        Me.gb_3_2 = New System.Windows.Forms.GroupBox()
        Me.gb_3_1 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtp_시작일자_치료대 = New System.Windows.Forms.DateTimePicker()
        Me.dtp_종료일자_치료대 = New System.Windows.Forms.DateTimePicker()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.tab_재고현황.SuspendLayout()
        Me.tp_혼합제.SuspendLayout()
        Me.tp_단미제.SuspendLayout()
        Me.tp_치료대.SuspendLayout()
        Me.SuspendLayout()
        '
        'tab_재고현황
        '
        Me.tab_재고현황.Controls.Add(Me.tp_혼합제)
        Me.tab_재고현황.Controls.Add(Me.tp_단미제)
        Me.tab_재고현황.Controls.Add(Me.tp_치료대)
        Me.tab_재고현황.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tab_재고현황.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tab_재고현황.Location = New System.Drawing.Point(0, 0)
        Me.tab_재고현황.Name = "tab_재고현황"
        Me.tab_재고현황.SelectedIndex = 0
        Me.tab_재고현황.Size = New System.Drawing.Size(1344, 651)
        Me.tab_재고현황.TabIndex = 2
        '
        'tp_혼합제
        '
        Me.tp_혼합제.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tp_혼합제.Controls.Add(Me.chk_혼합)
        Me.tp_혼합제.Controls.Add(Me.rb_전체항목_혼합)
        Me.tp_혼합제.Controls.Add(Me.rb_기준항목_혼합)
        Me.tp_혼합제.Controls.Add(Me.btn_조회_혼합)
        Me.tp_혼합제.Controls.Add(Me.gb_1_3)
        Me.tp_혼합제.Controls.Add(Me.gb_1_2)
        Me.tp_혼합제.Controls.Add(Me.gb_1_1)
        Me.tp_혼합제.Controls.Add(Me.Label3)
        Me.tp_혼합제.Controls.Add(Me.Label4)
        Me.tp_혼합제.Controls.Add(Me.dtp_시작일자_혼합제)
        Me.tp_혼합제.Controls.Add(Me.dtp_종료일자_혼합제)
        Me.tp_혼합제.Location = New System.Drawing.Point(4, 24)
        Me.tp_혼합제.Name = "tp_혼합제"
        Me.tp_혼합제.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_혼합제.Size = New System.Drawing.Size(1336, 623)
        Me.tp_혼합제.TabIndex = 1
        Me.tp_혼합제.Text = " 혼합제  "
        '
        'chk_혼합
        '
        Me.chk_혼합.AutoSize = True
        Me.chk_혼합.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chk_혼합.Location = New System.Drawing.Point(642, 14)
        Me.chk_혼합.Name = "chk_혼합"
        Me.chk_혼합.Size = New System.Drawing.Size(74, 19)
        Me.chk_혼합.TabIndex = 48
        Me.chk_혼합.Text = "전체기간"
        Me.chk_혼합.UseVisualStyleBackColor = True
        '
        'rb_전체항목_혼합
        '
        Me.rb_전체항목_혼합.AutoSize = True
        Me.rb_전체항목_혼합.Checked = True
        Me.rb_전체항목_혼합.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rb_전체항목_혼합.Location = New System.Drawing.Point(13, 13)
        Me.rb_전체항목_혼합.Name = "rb_전체항목_혼합"
        Me.rb_전체항목_혼합.Size = New System.Drawing.Size(97, 19)
        Me.rb_전체항목_혼합.TabIndex = 47
        Me.rb_전체항목_혼합.TabStop = True
        Me.rb_전체항목_혼합.Text = "전체항목조회"
        Me.rb_전체항목_혼합.UseVisualStyleBackColor = True
        '
        'rb_기준항목_혼합
        '
        Me.rb_기준항목_혼합.AutoSize = True
        Me.rb_기준항목_혼합.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rb_기준항목_혼합.Location = New System.Drawing.Point(110, 13)
        Me.rb_기준항목_혼합.Name = "rb_기준항목_혼합"
        Me.rb_기준항목_혼합.Size = New System.Drawing.Size(101, 19)
        Me.rb_기준항목_혼합.TabIndex = 46
        Me.rb_기준항목_혼합.Text = "기준항목 조회"
        Me.rb_기준항목_혼합.UseVisualStyleBackColor = True
        '
        'btn_조회_혼합
        '
        Me.btn_조회_혼합.BackColor = System.Drawing.Color.Transparent
        Me.btn_조회_혼합.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btn_조회_혼합.FlatAppearance.BorderSize = 2
        Me.btn_조회_혼합.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_조회_혼합.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_조회_혼합.Location = New System.Drawing.Point(569, 10)
        Me.btn_조회_혼합.Name = "btn_조회_혼합"
        Me.btn_조회_혼합.Size = New System.Drawing.Size(50, 23)
        Me.btn_조회_혼합.TabIndex = 44
        Me.btn_조회_혼합.Text = "조회"
        Me.btn_조회_혼합.UseVisualStyleBackColor = False
        '
        'gb_1_3
        '
        Me.gb_1_3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gb_1_3.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.gb_1_3.Location = New System.Drawing.Point(886, 40)
        Me.gb_1_3.Name = "gb_1_3"
        Me.gb_1_3.Size = New System.Drawing.Size(443, 575)
        Me.gb_1_3.TabIndex = 42
        Me.gb_1_3.TabStop = False
        Me.gb_1_3.Text = "진료처방내역"
        '
        'gb_1_2
        '
        Me.gb_1_2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gb_1_2.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.gb_1_2.Location = New System.Drawing.Point(455, 40)
        Me.gb_1_2.Name = "gb_1_2"
        Me.gb_1_2.Size = New System.Drawing.Size(425, 575)
        Me.gb_1_2.TabIndex = 43
        Me.gb_1_2.TabStop = False
        Me.gb_1_2.Text = "입출고내역"
        '
        'gb_1_1
        '
        Me.gb_1_1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gb_1_1.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.gb_1_1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gb_1_1.Location = New System.Drawing.Point(8, 40)
        Me.gb_1_1.Name = "gb_1_1"
        Me.gb_1_1.Size = New System.Drawing.Size(441, 575)
        Me.gb_1_1.TabIndex = 41
        Me.gb_1_1.TabStop = False
        Me.gb_1_1.Text = "기준항목"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.Location = New System.Drawing.Point(434, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(15, 15)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "~"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.Location = New System.Drawing.Point(249, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 15)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "입출고일자"
        '
        'dtp_시작일자_혼합제
        '
        Me.dtp_시작일자_혼합제.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.dtp_시작일자_혼합제.CustomFormat = "yyyy-MM-dd"
        Me.dtp_시작일자_혼합제.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtp_시작일자_혼합제.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_시작일자_혼합제.Location = New System.Drawing.Point(322, 11)
        Me.dtp_시작일자_혼합제.Name = "dtp_시작일자_혼합제"
        Me.dtp_시작일자_혼합제.Size = New System.Drawing.Size(110, 23)
        Me.dtp_시작일자_혼합제.TabIndex = 38
        '
        'dtp_종료일자_혼합제
        '
        Me.dtp_종료일자_혼합제.CustomFormat = "yyyy-MM-dd"
        Me.dtp_종료일자_혼합제.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtp_종료일자_혼합제.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_종료일자_혼합제.Location = New System.Drawing.Point(451, 11)
        Me.dtp_종료일자_혼합제.Name = "dtp_종료일자_혼합제"
        Me.dtp_종료일자_혼합제.Size = New System.Drawing.Size(110, 23)
        Me.dtp_종료일자_혼합제.TabIndex = 39
        '
        'tp_단미제
        '
        Me.tp_단미제.Controls.Add(Me.gb_2_1)
        Me.tp_단미제.Controls.Add(Me.chk_단미)
        Me.tp_단미제.Controls.Add(Me.rb_전체항목_단미)
        Me.tp_단미제.Controls.Add(Me.rb_기준항목_단미)
        Me.tp_단미제.Controls.Add(Me.btn_조회_단미)
        Me.tp_단미제.Controls.Add(Me.gb_2_3)
        Me.tp_단미제.Controls.Add(Me.gb_2_2)
        Me.tp_단미제.Controls.Add(Me.Label1)
        Me.tp_단미제.Controls.Add(Me.Label2)
        Me.tp_단미제.Controls.Add(Me.dtp_시작일자_단미)
        Me.tp_단미제.Controls.Add(Me.dtp_종료일자_단미)
        Me.tp_단미제.Location = New System.Drawing.Point(4, 24)
        Me.tp_단미제.Name = "tp_단미제"
        Me.tp_단미제.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_단미제.Size = New System.Drawing.Size(1336, 623)
        Me.tp_단미제.TabIndex = 0
        Me.tp_단미제.Text = " 단미제  "
        Me.tp_단미제.UseVisualStyleBackColor = True
        '
        'gb_2_1
        '
        Me.gb_2_1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gb_2_1.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.gb_2_1.Location = New System.Drawing.Point(8, 40)
        Me.gb_2_1.Name = "gb_2_1"
        Me.gb_2_1.Size = New System.Drawing.Size(441, 575)
        Me.gb_2_1.TabIndex = 59
        Me.gb_2_1.TabStop = False
        Me.gb_2_1.Text = "기준항목"
        '
        'chk_단미
        '
        Me.chk_단미.AutoSize = True
        Me.chk_단미.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chk_단미.Location = New System.Drawing.Point(642, 14)
        Me.chk_단미.Name = "chk_단미"
        Me.chk_단미.Size = New System.Drawing.Size(74, 19)
        Me.chk_단미.TabIndex = 58
        Me.chk_단미.Text = "전체기간"
        Me.chk_단미.UseVisualStyleBackColor = True
        '
        'rb_전체항목_단미
        '
        Me.rb_전체항목_단미.AutoSize = True
        Me.rb_전체항목_단미.Checked = True
        Me.rb_전체항목_단미.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rb_전체항목_단미.Location = New System.Drawing.Point(13, 13)
        Me.rb_전체항목_단미.Name = "rb_전체항목_단미"
        Me.rb_전체항목_단미.Size = New System.Drawing.Size(97, 19)
        Me.rb_전체항목_단미.TabIndex = 57
        Me.rb_전체항목_단미.TabStop = True
        Me.rb_전체항목_단미.Text = "전체항목조회"
        Me.rb_전체항목_단미.UseVisualStyleBackColor = True
        '
        'rb_기준항목_단미
        '
        Me.rb_기준항목_단미.AutoSize = True
        Me.rb_기준항목_단미.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rb_기준항목_단미.Location = New System.Drawing.Point(110, 13)
        Me.rb_기준항목_단미.Name = "rb_기준항목_단미"
        Me.rb_기준항목_단미.Size = New System.Drawing.Size(101, 19)
        Me.rb_기준항목_단미.TabIndex = 56
        Me.rb_기준항목_단미.Text = "기준항목 조회"
        Me.rb_기준항목_단미.UseVisualStyleBackColor = True
        '
        'btn_조회_단미
        '
        Me.btn_조회_단미.BackColor = System.Drawing.Color.Transparent
        Me.btn_조회_단미.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btn_조회_단미.FlatAppearance.BorderSize = 2
        Me.btn_조회_단미.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_조회_단미.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_조회_단미.Location = New System.Drawing.Point(569, 10)
        Me.btn_조회_단미.Name = "btn_조회_단미"
        Me.btn_조회_단미.Size = New System.Drawing.Size(50, 23)
        Me.btn_조회_단미.TabIndex = 55
        Me.btn_조회_단미.Text = "조회"
        Me.btn_조회_단미.UseVisualStyleBackColor = False
        '
        'gb_2_3
        '
        Me.gb_2_3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gb_2_3.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.gb_2_3.Location = New System.Drawing.Point(886, 40)
        Me.gb_2_3.Name = "gb_2_3"
        Me.gb_2_3.Size = New System.Drawing.Size(443, 575)
        Me.gb_2_3.TabIndex = 53
        Me.gb_2_3.TabStop = False
        Me.gb_2_3.Text = "진료처방내역"
        '
        'gb_2_2
        '
        Me.gb_2_2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gb_2_2.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.gb_2_2.Location = New System.Drawing.Point(455, 40)
        Me.gb_2_2.Name = "gb_2_2"
        Me.gb_2_2.Size = New System.Drawing.Size(425, 575)
        Me.gb_2_2.TabIndex = 54
        Me.gb_2_2.TabStop = False
        Me.gb_2_2.Text = "입출고내역"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.Location = New System.Drawing.Point(434, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(15, 15)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "~"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.Location = New System.Drawing.Point(249, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 15)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "입출고일자"
        '
        'dtp_시작일자_단미
        '
        Me.dtp_시작일자_단미.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.dtp_시작일자_단미.CustomFormat = "yyyy-MM-dd"
        Me.dtp_시작일자_단미.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtp_시작일자_단미.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_시작일자_단미.Location = New System.Drawing.Point(322, 11)
        Me.dtp_시작일자_단미.Name = "dtp_시작일자_단미"
        Me.dtp_시작일자_단미.Size = New System.Drawing.Size(110, 23)
        Me.dtp_시작일자_단미.TabIndex = 49
        '
        'dtp_종료일자_단미
        '
        Me.dtp_종료일자_단미.CustomFormat = "yyyy-MM-dd"
        Me.dtp_종료일자_단미.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtp_종료일자_단미.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_종료일자_단미.Location = New System.Drawing.Point(451, 11)
        Me.dtp_종료일자_단미.Name = "dtp_종료일자_단미"
        Me.dtp_종료일자_단미.Size = New System.Drawing.Size(110, 23)
        Me.dtp_종료일자_단미.TabIndex = 50
        '
        'tp_치료대
        '
        Me.tp_치료대.Controls.Add(Me.chk_치료대)
        Me.tp_치료대.Controls.Add(Me.rb_전체항목_치료대)
        Me.tp_치료대.Controls.Add(Me.rb_기준항목_치료대)
        Me.tp_치료대.Controls.Add(Me.btn_조회_치료대)
        Me.tp_치료대.Controls.Add(Me.gb_3_3)
        Me.tp_치료대.Controls.Add(Me.gb_3_2)
        Me.tp_치료대.Controls.Add(Me.gb_3_1)
        Me.tp_치료대.Controls.Add(Me.Label5)
        Me.tp_치료대.Controls.Add(Me.Label6)
        Me.tp_치료대.Controls.Add(Me.dtp_시작일자_치료대)
        Me.tp_치료대.Controls.Add(Me.dtp_종료일자_치료대)
        Me.tp_치료대.Location = New System.Drawing.Point(4, 24)
        Me.tp_치료대.Name = "tp_치료대"
        Me.tp_치료대.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_치료대.Size = New System.Drawing.Size(1336, 623)
        Me.tp_치료대.TabIndex = 2
        Me.tp_치료대.Text = " 치료재료대  "
        Me.tp_치료대.UseVisualStyleBackColor = True
        '
        'chk_치료대
        '
        Me.chk_치료대.AutoSize = True
        Me.chk_치료대.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chk_치료대.Location = New System.Drawing.Point(642, 14)
        Me.chk_치료대.Name = "chk_치료대"
        Me.chk_치료대.Size = New System.Drawing.Size(74, 19)
        Me.chk_치료대.TabIndex = 59
        Me.chk_치료대.Text = "전체기간"
        Me.chk_치료대.UseVisualStyleBackColor = True
        '
        'rb_전체항목_치료대
        '
        Me.rb_전체항목_치료대.AutoSize = True
        Me.rb_전체항목_치료대.Checked = True
        Me.rb_전체항목_치료대.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rb_전체항목_치료대.Location = New System.Drawing.Point(13, 13)
        Me.rb_전체항목_치료대.Name = "rb_전체항목_치료대"
        Me.rb_전체항목_치료대.Size = New System.Drawing.Size(97, 19)
        Me.rb_전체항목_치료대.TabIndex = 57
        Me.rb_전체항목_치료대.TabStop = True
        Me.rb_전체항목_치료대.Text = "전체항목조회"
        Me.rb_전체항목_치료대.UseVisualStyleBackColor = True
        '
        'rb_기준항목_치료대
        '
        Me.rb_기준항목_치료대.AutoSize = True
        Me.rb_기준항목_치료대.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rb_기준항목_치료대.Location = New System.Drawing.Point(110, 13)
        Me.rb_기준항목_치료대.Name = "rb_기준항목_치료대"
        Me.rb_기준항목_치료대.Size = New System.Drawing.Size(101, 19)
        Me.rb_기준항목_치료대.TabIndex = 56
        Me.rb_기준항목_치료대.Text = "기준항목 조회"
        Me.rb_기준항목_치료대.UseVisualStyleBackColor = True
        '
        'btn_조회_치료대
        '
        Me.btn_조회_치료대.BackColor = System.Drawing.Color.Transparent
        Me.btn_조회_치료대.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btn_조회_치료대.FlatAppearance.BorderSize = 2
        Me.btn_조회_치료대.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_조회_치료대.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btn_조회_치료대.Location = New System.Drawing.Point(569, 10)
        Me.btn_조회_치료대.Name = "btn_조회_치료대"
        Me.btn_조회_치료대.Size = New System.Drawing.Size(50, 23)
        Me.btn_조회_치료대.TabIndex = 55
        Me.btn_조회_치료대.Text = "조회"
        Me.btn_조회_치료대.UseVisualStyleBackColor = False
        '
        'gb_3_3
        '
        Me.gb_3_3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gb_3_3.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.gb_3_3.Location = New System.Drawing.Point(886, 40)
        Me.gb_3_3.Name = "gb_3_3"
        Me.gb_3_3.Size = New System.Drawing.Size(443, 575)
        Me.gb_3_3.TabIndex = 53
        Me.gb_3_3.TabStop = False
        Me.gb_3_3.Text = "진료처방내역"
        '
        'gb_3_2
        '
        Me.gb_3_2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gb_3_2.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.gb_3_2.Location = New System.Drawing.Point(455, 40)
        Me.gb_3_2.Name = "gb_3_2"
        Me.gb_3_2.Size = New System.Drawing.Size(425, 575)
        Me.gb_3_2.TabIndex = 54
        Me.gb_3_2.TabStop = False
        Me.gb_3_2.Text = "입출고내역"
        '
        'gb_3_1
        '
        Me.gb_3_1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gb_3_1.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.gb_3_1.Location = New System.Drawing.Point(8, 40)
        Me.gb_3_1.Name = "gb_3_1"
        Me.gb_3_1.Size = New System.Drawing.Size(441, 575)
        Me.gb_3_1.TabIndex = 52
        Me.gb_3_1.TabStop = False
        Me.gb_3_1.Text = "기준항목"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.Location = New System.Drawing.Point(434, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(15, 15)
        Me.Label5.TabIndex = 51
        Me.Label5.Text = "~"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.Location = New System.Drawing.Point(249, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(67, 15)
        Me.Label6.TabIndex = 48
        Me.Label6.Text = "입출고일자"
        '
        'dtp_시작일자_치료대
        '
        Me.dtp_시작일자_치료대.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.dtp_시작일자_치료대.CustomFormat = "yyyy-MM-dd"
        Me.dtp_시작일자_치료대.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtp_시작일자_치료대.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_시작일자_치료대.Location = New System.Drawing.Point(322, 11)
        Me.dtp_시작일자_치료대.Name = "dtp_시작일자_치료대"
        Me.dtp_시작일자_치료대.Size = New System.Drawing.Size(110, 23)
        Me.dtp_시작일자_치료대.TabIndex = 49
        '
        'dtp_종료일자_치료대
        '
        Me.dtp_종료일자_치료대.CustomFormat = "yyyy-MM-dd"
        Me.dtp_종료일자_치료대.Font = New System.Drawing.Font("맑은 고딕", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtp_종료일자_치료대.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_종료일자_치료대.Location = New System.Drawing.Point(451, 11)
        Me.dtp_종료일자_치료대.Name = "dtp_종료일자_치료대"
        Me.dtp_종료일자_치료대.Size = New System.Drawing.Size(110, 23)
        Me.dtp_종료일자_치료대.TabIndex = 50
        '
        'Form2
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1344, 651)
        Me.Controls.Add(Me.tab_재고현황)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Form2"
        Me.Text = "재고현황"
        Me.tab_재고현황.ResumeLayout(False)
        Me.tp_혼합제.ResumeLayout(False)
        Me.tp_혼합제.PerformLayout()
        Me.tp_단미제.ResumeLayout(False)
        Me.tp_단미제.PerformLayout()
        Me.tp_치료대.ResumeLayout(False)
        Me.tp_치료대.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tab_재고현황 As TabControl
    Friend WithEvents tp_단미제 As TabPage
    Friend WithEvents tp_치료대 As TabPage
    Friend WithEvents tp_혼합제 As TabPage
    Friend WithEvents gb_1_3 As GroupBox
    Friend WithEvents gb_1_2 As GroupBox
    Friend WithEvents gb_1_1 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents dtp_시작일자_혼합제 As DateTimePicker
    Friend WithEvents dtp_종료일자_혼합제 As DateTimePicker
    Friend WithEvents btn_조회_혼합 As Button
    Friend WithEvents rb_전체항목_혼합 As RadioButton
    Friend WithEvents rb_기준항목_혼합 As RadioButton
    Friend WithEvents rb_전체항목_단미 As RadioButton
    Friend WithEvents rb_기준항목_단미 As RadioButton
    Friend WithEvents btn_조회_단미 As Button
    Friend WithEvents gb_2_3 As GroupBox
    Friend WithEvents gb_2_2 As GroupBox
    Friend WithEventsgb_기준항목_단미 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents dtp_시작일자_단미 As DateTimePicker
    Friend WithEvents dtp_종료일자_단미 As DateTimePicker
    Friend WithEvents rb_전체항목_치료대 As RadioButton
    Friend WithEvents rb_기준항목_치료대 As RadioButton
    Friend WithEvents btn_조회_치료대 As Button
    Friend WithEvents gb_3_3 As GroupBox
    Friend WithEvents gb_3_2 As GroupBox
    Friend WithEvents gb_3_1 As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents dtp_시작일자_치료대 As DateTimePicker
    Friend WithEvents dtp_종료일자_치료대 As DateTimePicker
    Friend WithEvents chk_혼합 As CheckBox
    Friend WithEvents chk_단미 As CheckBox
    Friend WithEvents chk_치료대 As CheckBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents gb_2_1 As GroupBox
End Class
