Imports System.ComponentModel
Imports System.Runtime.InteropServices.ComTypes

Public Class frm_Main

    Private Class cls_forms
        Private mForms As Form
        Private mButton As ToolStripButton

        Public Property Forms As Form
            Get
                Return mForms
            End Get
            Set(value As Form)
                'If Not mForms Is Nothing Then mForms.Dispose()

                'value.TopLevel = False
                'MainForm.pnl_main.Controls.Add(value)
                'value.Visible = True 'form load 탐
                'value.Dock = DockStyle.Fill
                'value.FormBorderStyle = FormBorderStyle.None

                'mForms = value

                If Not mForms Is Nothing Then mForms.Dispose()
                value.TopLevel = False
                MainForm.pnl_main.Controls.Add(value)
                value.FormBorderStyle = FormBorderStyle.None
                value.Dock = DockStyle.Fill

                MainForm.pnl_main.PerformLayout()
                MainForm.pnl_main.Refresh()

                value.Visible = True

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
            For Each item As Form In MainForm.pnl_main.Controls
                If item Is mForms Then
                    item.Visible = True
                    MainForm.Update()
                Else
                    item.Visible = False
                    MainForm.Update()
                End If
            Next
        End Sub

    End Class

    'Private Sub sD_CreateTable()
    '    clsG_DBmng.sql_Exec_Query(
    '            $"
    '                If Not exists(select *from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'TB_도움말')
    '                begin
    '                CREATE TABLE [dbo].[TB_도움말](
    '                    [idx] [int] IDENTITY(1,1) NOT NULL,
    '                    [항목] [nvarchar](200) NOT NULL,
    '                    [설명] [nvarchar](200) NOT NULL,
    '                    [숨김] [tinyint]NOT NULL
    '                ) ON [PRIMARY]
    '                end
    '            ")
    'End Sub

    Private lstD_Forms As New List(Of cls_forms)

    Private Sub sD_Load_Froms()
        lstD_Forms.Add(New cls_forms With {.Forms = New Form1, .Button = tbn_입출고})

        lstD_Forms.Add(New cls_forms With {.Forms = New Form2, .Button = tbn_현황})

        lstD_Forms(0).Button.PerformClick()

        'sD_CreateTable()
        'sD_loadHelper()
    End Sub

    Private Sub frm_Main_Load(sender As Object, e As EventArgs) Handles Me.Load
        sD_Load_Froms()
    End Sub


    Private Sub tbn_입출고_MouseHover(sender As Object, e As EventArgs) Handles tbn_입출고.MouseHover, tbn_현황.MouseHover
        If Not dicG_helper.ContainsKey(sender.Name) Then Exit Sub
        Dim strL_value = dicG_helper(sender.Name)
        lbl_도움말.Text = strL_value
    End Sub

    Private Sub tbn_입출고_MouseLeave(sender As Object, e As EventArgs) Handles tbn_입출고.MouseLeave, tbn_현황.MouseLeave
        lbl_도움말.Text = ""
    End Sub

End Class