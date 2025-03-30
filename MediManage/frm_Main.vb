Public Class frm_Main

    Private Class cls_forms
        Private mForms As Form
        Private mButton As ToolStripButton

        Public Property Forms As Form
            Get
                Return mForms
            End Get
            Set(value As Form)
                If Not mForms Is Nothing Then mForms.Dispose()

                value.TopLevel = False
                frm_Main.pnl_main.Controls.Add(value)
                value.Visible = True 'form load 탐
                value.Dock = DockStyle.Fill
                value.FormBorderStyle = FormBorderStyle.None

                mForms = value
            End Set
        End Property

        Public Property Button As ToolStripButton
            Get
                Return mButton
            End Get
            Set(value As ToolStripButton)
                If Not mButton Is Nothing Then RemoveHandler mButton.Click, AddressOf sD_ButtonClick

                mButton = value

                AddHandler mButton.Click, AddressOf sD_ButtonClick

            End Set
        End Property

        Private Sub sD_ButtonClick()
            For Each item As Form In frm_Main.pnl_main.Controls
                If item Is mForms Then
                    item.Visible = True
                    frm_Main.Update()
                Else
                    item.Visible = False
                    frm_Main.Update()
                End If
            Next
        End Sub

    End Class

    Private lstD_Forms As New List(Of cls_forms)

    Private Sub sD_Load_Froms()
        lstD_Forms.Add(New cls_forms With {.Forms = New Form1, .Button = tbn_입출고})
        lstD_Forms.Add(New cls_forms With {.Forms = New Form2, .Button = tbn_현황})

        lstD_Forms(0).Button.PerformClick()
    End Sub

    Private Sub frm_Main_Load(sender As Object, e As EventArgs) Handles Me.Load
        sD_Load_Froms()
        '로드 될때에는 실행되고 있는 과정이기때문에 의미없음 

        'Dim blnL_Boolean As Boolean = Me.Visible 

        'Me.Visible = False
    End Sub

End Class