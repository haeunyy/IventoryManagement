Imports System.IO
Imports System.Net.Security
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form2

    Private Enum en_기준항목_Col
        코드 = 1
        명칭
        업소
        수량
    End Enum

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

        If code = 0 Then
            grid_기준항목_혼합제.DataSource = dtL_data
            grid_기준항목_혼합제.Update()
            grid_기준항목_혼합제.ClearSelection()
            sD_Get_mediList()
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

        Dim invenQuery As String
        Dim mediQuery As String
        Dim typeQuery As String
        Dim temp_startDate As DateTimePicker
        Dim temp_endDate As DateTimePicker
        Dim temp_grid As DataGridView

        If tab_재고현황.SelectedIndex = 0 Then

            temp_startDate = dtp_시작일자_혼합제
            temp_endDate = dtp_종료일자_혼합제

            Dim row = grid_기준항목_혼합제.CurrentRow

            If rb_기준항목_혼합.Checked Then
                If row Is Nothing Then Exit Sub
                invenQuery = "and 처방코드 = '" + row.Cells(en_기준항목_Col.코드).Value + "'"
                mediQuery = "and b.처방코드 = '" + row.Cells(en_기준항목_Col.코드).Value + "'"
            End If

            typeQuery = "b.등록구분 = 29 and"

            temp_grid = grid_입출고_혼합제

        ElseIf tab_재고현황.SelectedIndex = 1 Then

            temp_startDate = dtp_시작일자_단미
            temp_endDate = dtp_종료일자_단미

            Dim row = grid_기준항목_단미제.CurrentRow

            If rb_기준항목_단미.Checked Then
                invenQuery = "and 처방코드 = '" + row.Cells(en_기준항목_Col.코드).Value + "'"
                mediQuery = "and b.처방코드 = '" + row.Cells(en_기준항목_Col.코드).Value + "'"
            End If

            typeQuery = "b.등록구분 in(25,26,28) and"

            temp_grid = grid_입출고_단미제

        ElseIf tab_재고현황.SelectedIndex = 2 Then

            temp_startDate = dtp_시작일자_치료대
            temp_endDate = dtp_종료일자_치료대

            Dim row = grid_기준항목_치료대.CurrentRow

            If rb_기준항목_치료대.Checked Then
                invenQuery = "and 처방코드 = '" + row.Cells(en_기준항목_Col.코드).Value + "'"
                mediQuery = "and b.처방코드 = '" + row.Cells(en_기준항목_Col.코드).Value + "'"
            End If

            typeQuery = "b.코드구분 = 8 and"

            temp_grid = grid_입출고_치료대

        End If

        Dim dtL_data = clsG_DBmng.sql_Get_Datatable(
            $"

                Select 
                    convert(nvarchar(10), 일자, 13) as 일자, case when 입출고 = 0 then '재고 입고' else '재고 출고' end as 입출고, 처방코드 as 코드, 명칭 as 명칭, 수량 as 수량, cast(단가 as int) as 단가, cast(수량 * 단가 as bigint) as 금액
                from
                    TB_재고 a
                where
                    구분 = {tab_재고현황.SelectedIndex} and 
                    일자 between '{CDate(temp_startDate.Value).ToString("yyyy-MM-dd")}' and '{CDate(temp_endDate.Value).ToString("yyyy-MM-dd")}' 
                    {invenQuery}

                Union ALL

                Select
                    a.진료일자 as 일자, '진료입력 출고' as 입출고, b.처방코드 as 코드, b.명칭 as 명칭,sum(b.투여량 * b.일수) as 수량, cast(b.단가 as int) as 단가, cast(sum(b.투여량 * b.일수 * b.단가) as bigint) as 금액
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
                    {typeQuery}
                    a.진료상태 <> 4 and 
                    a.진료일자 between convert(date,'{CDate(temp_startDate.Value).ToString("yyyy-MM-dd")}') and convert(date,'{CDate(temp_endDate.Value).ToString("yyyy-MM-dd")}')
                    {mediQuery}
                group by
	                a.진료일자,  b.처방코드, b.명칭, b.단가
                order by
                    일자
            ")

        If dtL_data.Rows.Count = 0 Then Exit Sub

        Dim dr_row As DataRow

        dr_row = dtL_data.NewRow

        dr_row("일자") = "합계"
        dr_row("수량") = dtL_data.AsEnumerable.Sum(Function(x) x("수량"))
        dr_row("단가") = dtL_data.AsEnumerable.Sum(Function(x) x("단가"))
        dr_row("금액") = dtL_data.AsEnumerable.Sum(Function(x) CDbl(x("금액")))

        dtL_data.Rows.Add(dr_row)

        temp_grid.DataSource = dtL_data

        Dim Arr_Temp = {en_입출고_Col.일자.GetHashCode, en_입출고_Col.수량.GetHashCode, en_입출고_Col.단가.GetHashCode, en_입출고_Col.금액.GetHashCode}

        For intL_i As Integer = 0 To temp_grid.ColumnCount - 1
            temp_grid.Rows(temp_grid.RowCount - 1).Cells(intL_i).Style.BackColor = Color.Yellow
            temp_grid.Rows(temp_grid.RowCount - 1).Cells(intL_i).Style.Font = New Font("맑은 고딕", 9, FontStyle.Bold)
        Next

        temp_grid.Refresh()
    End Sub

    Private Sub btn_조회_Click(sender As Object, e As EventArgs) Handles btn_조회_혼합.Click, btn_조회_단미.Click, btn_조회_치료대.Click
        sD_Get_InOutList()
    End Sub



    ''' <summary>
    ''' 조회만 됨 
    ''' 동적쿼리로 바꿔야함 
    ''' 
    ''' 조건 - 진료년월일
    ''' 
    ''' </summary>
    Private Sub sD_Get_mediList()
        Dim dtL_data = clsG_DBmng.sql_Get_Datatable(
            $"
                select 
				    (a.진료년 +'-'+ a.진료월 + '-' + a.진료일) as 일자, b.수진자명, a.명칭 ,sum(a.투여량 * a.일수) as 수량, cast(a.단가 as int) as 단가, cast(sum(a.투여량 * a.일수 * a.단가) as bigint) as 금액 
			    from 
				    tb_h_진료내역 a 
			    left join 
				    TB_인적사항 as b 
			    on 
				    a.챠트번호 = b.챠트번호
			    where 
				    a.처방코드 = '655001150' 
			    group by 
				    a.진료년 ,a.진료월 ,a.진료일, b.수진자명, a.단가, a.명칭
            ")

        grid_처방_혼합제.DataSource = dtL_data
        grid_처방_혼합제.Refresh()
    End Sub


End Class

