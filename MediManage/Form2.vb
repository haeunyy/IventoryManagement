Imports System.IO
Imports System.Net.Security
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form2

    Private Enum en_입출고_Col
        일자 = 0
        입출고
        코드
        명칭
        수량
        단가
        금액
    End Enum

    ''' <summary>
    ''' 1. 폼이 로드되면 sD_get_mainList 함수에 구분코드를 매개변수로 전달하여 DataGridView에 로드
    ''' </summary>
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles Me.Load
        tab_재고현황.SelectedIndex = 0
        sD_get_mainList(tab_재고현황.SelectedIndex)
        sD_get_mainList(tab_재고현황.SelectedIndex + 1)
        sD_get_mainList(tab_재고현황.SelectedIndex + 2)
    End Sub


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
	                SUM(case when 입출고 = 1 then -abs(수량) else abs(수량) end) as 수량
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

            sD_Get_InOutList()
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

    Private Sub sD_Get_InOutList()

        grid_입출고_혼합제.DataSource = clsG_DBmng.sql_Get_Datatable(
            $"

                Select 
                    convert(nvarchar(10), 일자, 13) as 일자, case when 입출고 = 0 then '재고 입고' else '재고 출고' end as 입출고, 처방코드 as 코드, 명칭 as 명칭, 수량 as 수량, cast(단가 as int) as 단가, cast(수량 * 단가 as bigint) as 금액
                from
                    TB_재고 a
                where
                    일자 between '{CDate(dtp_시작일자_혼합제.Value).ToString("yyyy-MM-dd")}' and '{CDate(dtp_종료일자_혼합제.Value).ToString("yyyy-MM-dd")}' and
                    구분 = 0

                Union ALL

                Select
                    a.진료일자 as 일자, '진료입력 출고', b.처방코드 as 코드, b.명칭 as 명칭,sum(b.투여량 * b.일수) as 수량, sum(cast(b.단가 as int) )as 단가, sum(cast(b.투여량 * b.일수 * b.단가 as bigint)) as 금액
                from
                    TB_진료기본 a
                inner join
                    TB_H_진료내역 b
                on
                    a.진료년 = b.진료년 and
                    a.진료월 = b.진료월 and
                    a.진료일 = b.진료일 and
                    a.진료번호 = b.진료번호 and
                    a.챠트번호 = b.챠트번호
                where
                    b.등록구분 = 29 and
                    a.진료상태 <> 4 and
                    a.진료일자 between convert(date,'{CDate(dtp_시작일자_혼합제.Value).ToString("yyyy-MM-dd")}') and convert(date,'{CDate(dtp_종료일자_혼합제.Value).ToString("yyyy-MM-dd")}')
                group by
	                a.진료일자,  b.처방코드, b.명칭,  b.투여량 ,  b.일수, b.단가
                order by
                    일자
            ")
        grid_입출고_혼합제.Refresh()
    End Sub

    Private Sub btn_조회_Click(sender As Object, e As EventArgs) Handles btn_조회.Click
        sD_get_mainList(tab_재고현황.SelectedIndex)

    End Sub

    Private Sub grid_입출고_혼합제_ColumnHeadersHeightSizeModeChanged(sender As Object, e As EventArgs) Handles grid_입출고_혼합제.DataSourceChanged
        Dim count = 0
        Dim price = 0
        Dim totalPrice = 0
        For Each row As DataGridViewRow In grid_입출고_혼합제.Rows
            If Not row.IsNewRow AndAlso Not String.IsNullOrEmpty(row.Cells(4).Value?.ToString()) Then
                count += Convert.ToDouble(row.Cells(en_입출고_Col.수량).Value)
                price += Convert.ToDouble(row.Cells(en_입출고_Col.단가).Value)
                totalPrice += Convert.ToDouble(row.Cells(en_입출고_Col.금액).Value)
            End If
        Next

        If grid_입출고_혼합제.Rows.Count > 0 Then 'AndAlso grid_입출고_혼합제.Rows(grid_입출고_혼합제.Rows.Count - 1).Cells(1).Value?.ToString() = "Total Sales" 
            'Dim summaryRow As DataGridViewRow = 
            'grid_입출고_혼합제.Rows(
            'grid_입출고_혼합제.Rows.Add()
            ')
            'summaryRow.Cells(1).Value = "Total Sales"
            'summaryRow.Cells(2).Value = myTotal.ToString("C")
            'summaryRow.DefaultCellStyle.BackColor = Color.LightGray 
            txt_수량.Text = Format(count, "#,##0.##")
            txt_단가.Text = Format(price, "#,##0")
            txt_금액.Text = Format(totalPrice, "#,##0")
        End If
    End Sub

End Class

