Imports System.IO
Imports System.Net.Security

Public Class Form2

    ''' <summary>
    ''' 1. 폼이 로드되면 sD_get_mainList 함수에 구분코드를 매개변수로 전달하여 DataGridView에 로드
    ''' </summary>
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles Me.Load
        sD_get_mainList(tab_재고현황.SelectedIndex)
        sD_get_mainList(tab_재고현황.SelectedIndex + 1)
        sD_get_mainList(tab_재고현황.SelectedIndex + 2)
    End Sub

    'Private Function fD_queryDate(code As Integer)
    '    If code = 0 Then
    '        Return "and left(일자,10)  between '" + dtp_시작일자_혼합제.Value + "' and '" + dtp_종료일자_혼합제.Value + "'"
    '    ElseIf code = 1 Then
    '        Return "and left(일자,10)  between '" + dtp_시작일자_단미제.Value + "' and '" + dtp_종료일자_단미제.Value + "'"
    '    ElseIf code = 2 Then
    '        Return "and left(일자,10)  between '" + dtp_시작일자_치료대.Value + "' and '" + dtp_종료일자_치료대.Value + "'"
    '    End If
    'End Function

    ''' <summary>
    ''' 1. 구분코드에 따라 조회 구문 실행<br/>
    ''' 2. DataGridView에 update
    ''' </summary>
    ''' <param name="code">구분 (0-혼합제/1-단미제/2-치료재료대)</param>
    Private Sub sD_get_mainList(code As Integer)

        Dim dtL_data As DataTable = clsG_DBmng.sql_Get_Datatable(
            $"
                select  
	                처방코드 AS 코드, 
	                명칭, 
                    업소,
	                sum(수량) - coalesce ( (select sum(수량) from tb_재고 
							                 where 구분 = {code} and 입출고 = 1 and 처방코드 = a.처방코드), 0) as 수량
                from 
	                tb_재고 as a
                where 
	                구분 = {code} and
	                입출고 = 0 
                group by 
	                처방코드, 명칭, 업소
            ")
        '{fD_queryDate()}

        If code = 0 Then
            grid_기준항목_혼합제.DataSource = dtL_data
            grid_기준항목_혼합제.Update()
            grid_기준항목_혼합제.ClearSelection()
        ElseIf code = 1 Then
            grid_기준항목_단미제.DataSource = dtL_data
            grid_기준항목_단미제.Update()
            grid_기준항목_단미제.ClearSelection()
        ElseIf code = 2 Then
            grid_기준항목_치료대.DataSource = dtL_data
            grid_기준항목_치료대.Update()
            grid_기준항목_치료대.ClearSelection()
        End If

    End Sub



End Class

