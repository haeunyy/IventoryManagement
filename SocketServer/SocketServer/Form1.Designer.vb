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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbl = New System.Windows.Forms.Label()
        Me.btn = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lbl)
        Me.Panel1.Controls.Add(Me.btn)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(425, 191)
        Me.Panel1.TabIndex = 0
        '
        'lbl
        '
        Me.lbl.Location = New System.Drawing.Point(27, 80)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(374, 87)
        Me.lbl.TabIndex = 1
        Me.lbl.Text = "연결 대기중"
        Me.lbl.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btn
        '
        Me.btn.Enabled = False
        Me.btn.Location = New System.Drawing.Point(95, 43)
        Me.btn.Name = "btn"
        Me.btn.Size = New System.Drawing.Size(243, 27)
        Me.btn.TabIndex = 0
        Me.btn.Text = "업데이트"
        Me.btn.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(425, 191)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Form1"
        Me.Text = "Socket Server"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents lbl As Label
    Friend WithEvents btn As Button
End Class
