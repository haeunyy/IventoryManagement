<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Main))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tbn_입출고 = New System.Windows.Forms.ToolStripButton()
        Me.tbn_현황 = New System.Windows.Forms.ToolStripButton()
        Me.pnl_main = New System.Windows.Forms.Panel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lbl_상태 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.prg_상태바 = New System.Windows.Forms.ToolStripProgressBar()
        Me.lbl_도움말 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tbn_입출고, Me.tbn_현황})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1344, 35)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tbn_입출고
        '
        Me.tbn_입출고.AutoSize = False
        Me.tbn_입출고.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tbn_입출고.Image = CType(resources.GetObject("tbn_입출고.Image"), System.Drawing.Image)
        Me.tbn_입출고.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.tbn_입출고.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbn_입출고.Name = "tbn_입출고"
        Me.tbn_입출고.Size = New System.Drawing.Size(110, 32)
        Me.tbn_입출고.Text = "재고 입출고"
        '
        'tbn_현황
        '
        Me.tbn_현황.AutoSize = False
        Me.tbn_현황.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tbn_현황.Image = CType(resources.GetObject("tbn_현황.Image"), System.Drawing.Image)
        Me.tbn_현황.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.tbn_현황.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbn_현황.Name = "tbn_현황"
        Me.tbn_현황.Size = New System.Drawing.Size(110, 32)
        Me.tbn_현황.Text = " 재고현황"
        '
        'pnl_main
        '
        Me.pnl_main.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnl_main.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnl_main.Location = New System.Drawing.Point(0, 35)
        Me.pnl_main.Name = "pnl_main"
        Me.pnl_main.Size = New System.Drawing.Size(1344, 663)
        Me.pnl_main.TabIndex = 1
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lbl_상태, Me.prg_상태바, Me.lbl_도움말})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 699)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1344, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lbl_상태
        '
        Me.lbl_상태.Name = "lbl_상태"
        Me.lbl_상태.Size = New System.Drawing.Size(31, 17)
        Me.lbl_상태.Text = "준비"
        '
        'prg_상태바
        '
        Me.prg_상태바.Name = "prg_상태바"
        Me.prg_상태바.Size = New System.Drawing.Size(100, 16)
        '
        'lbl_도움말
        '
        Me.lbl_도움말.Name = "lbl_도움말"
        Me.lbl_도움말.Size = New System.Drawing.Size(1196, 17)
        Me.lbl_도움말.Spring = True
        Me.lbl_도움말.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frm_Main
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1344, 721)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.pnl_main)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(1360, 760)
        Me.Name = "frm_Main"
        Me.Text = "재고관리"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents tbn_입출고 As ToolStripButton
    Friend WithEvents tbn_현황 As ToolStripButton
    Friend WithEvents pnl_main As Panel
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lbl_상태 As ToolStripStatusLabel
    Friend WithEvents prg_상태바 As ToolStripProgressBar
    Friend WithEvents lbl_도움말 As ToolStripStatusLabel
End Class
