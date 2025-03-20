Imports System.CodeDom.Compiler
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
                inner join
                     TB_재고 c
                on
                     b.처방코드 = c.처방코드
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


        'If dtL_data.Rows.Count = 0 Then Exit Sub
        'Dim dr_row As DataRow
        'dr_row = dtL_data.NewRow
        'dr_row("일자") = "합계"
        'dr_row("수량") = dtL_data.AsEnumerable.Sum(Function(x) x("수량"))
        'dr_row("단가") = dtL_data.AsEnumerable.Sum(Function(x) x("단가"))
        'dr_row("금액") = dtL_data.AsEnumerable.Sum(Function(x) CDbl(x("금액")))
        'dtL_data.Rows.Add(dr_row)
        'temp_grid.DataSource = dtL_data
        'temp_grid.SuspendLayout()

        temp_grid.RowCount = 0
        temp_grid.Columns(en_입출고_Col.수량).Tag = 0
        temp_grid.Columns(en_입출고_Col.단가).Tag = 0
        temp_grid.Columns(en_입출고_Col.금액).Tag = 0

        Dim dblL_수량 As Double = 0
        Dim lngL_단가 As Long = 0
        Dim lngL_금액 As Long = 0

        For intL_i As Integer = 0 To dtL_data.Rows.Count - 1

            temp_grid.RowCount += 1

            For intL_j As Integer = 0 To temp_grid.Columns.Count - 1
                If temp_grid.Columns(intL_j).DataPropertyName <> "" Then

                    If Array.IndexOf({"수량", "단가", "금액"}, temp_grid.Columns(intL_j).DataPropertyName) >= 0 Then
                        temp_grid.Rows(temp_grid.RowCount - 1).Cells(intL_j).Value = CDbl(dtL_data.Rows(intL_i)(temp_grid.Columns(intL_j).DataPropertyName)).ToString(If(temp_grid.Columns(intL_j).DataPropertyName = "수량",
                                                                                                                                                                "#,##0.00", "#,##0"))
                        If temp_grid.Columns(intL_j).DataPropertyName = "수량" Then dblL_수량 += getValue(dtL_data.Rows(intL_i)(temp_grid.Columns(intL_j).DataPropertyName).ToString)
                        If temp_grid.Columns(intL_j).DataPropertyName = "단가" Then lngL_단가 += getValue(dtL_data.Rows(intL_i)(temp_grid.Columns(intL_j).DataPropertyName).ToString)
                        If temp_grid.Columns(intL_j).DataPropertyName = "금액" Then lngL_금액 += getValue(dtL_data.Rows(intL_i)(temp_grid.Columns(intL_j).DataPropertyName).ToString)
                    Else
                        temp_grid.Rows(temp_grid.RowCount - 1).Cells(intL_j).Value = dtL_data.Rows(intL_i)(temp_grid.Columns(intL_j).DataPropertyName).ToString
                    End If

                End If
            Next
        Next

        If temp_grid.RowCount <> 0 Then
            temp_grid.RowCount += 1
            temp_grid.Rows(temp_grid.RowCount - 1).Cells(en_입출고_Col.일자).Value = "합계"
            temp_grid.Rows(temp_grid.RowCount - 1).Cells(en_입출고_Col.수량).Value = dblL_수량.ToString("#,##0.00")
            temp_grid.Columns(en_입출고_Col.수량).Tag = dblL_수량.ToString("#,##0.00")
            temp_grid.Rows(temp_grid.RowCount - 1).Cells(en_입출고_Col.단가).Value = lngL_단가.ToString("#,##0")
            temp_grid.Columns(en_입출고_Col.단가).Tag = lngL_단가.ToString("#,##0")
            temp_grid.Rows(temp_grid.RowCount - 1).Cells(en_입출고_Col.금액).Value = lngL_금액.ToString("#,##0")
            temp_grid.Columns(en_입출고_Col.금액).Tag = lngL_금액.ToString("#,##0")
            Dim Arr_Temp = {en_입출고_Col.일자.GetHashCode, en_입출고_Col.수량.GetHashCode, en_입출고_Col.단가.GetHashCode, en_입출고_Col.금액.GetHashCode}

            For intL_i As Integer = 0 To temp_grid.ColumnCount - 1
                temp_grid.Rows(temp_grid.RowCount - 1).Cells(intL_i).Style.BackColor = Color.Yellow
                temp_grid.Rows(temp_grid.RowCount - 1).Cells(intL_i).Style.Font = New Font("맑은 고딕", 9, FontStyle.Bold)
            Next
        End If

        'temp_grid.ResumeLayout()

    End Sub

    Private Sub btn_조회_Click(sender As Object, e As EventArgs) Handles btn_조회_혼합.Click, btn_조회_단미.Click, btn_조회_치료대.Click
        sD_Get_InOutList()
    End Sub

    Private Sub grid_입출고_Click(sender As Object, e As EventArgs) Handles grid_입출고_혼합제.Click, grid_입출고_단미제.Click, grid_입출고_치료대.Click

        Dim temp_grid As DataGridView
        Dim temp_load As DataGridView
        Dim Arr_date As String()
        Dim temp_code As String

        If sender Is grid_입출고_혼합제 Then

            temp_grid = grid_입출고_혼합제

            If temp_grid Is Nothing AndAlso temp_grid.CurrentRow.Cells(en_입출고_Col.일자).Value = "" Then Exit Sub

            Arr_date = Split(temp_grid.CurrentRow.Cells(en_입출고_Col.일자).Value?.ToString, "-")
            temp_code = temp_grid.CurrentRow.Cells(en_입출고_Col.코드).Value?.ToString

            temp_load = grid_처방_혼합제

        ElseIf sender Is grid_입출고_단미제 Then

            temp_grid = grid_입출고_단미제

            If temp_grid Is Nothing AndAlso temp_grid.CurrentRow.Cells(en_입출고_Col.일자).Value = "" Then Exit Sub

            Arr_date = Split(temp_grid.CurrentRow.Cells(en_입출고_Col.일자).Value?.ToString, "-")
            temp_code = temp_grid.CurrentRow.Cells(en_입출고_Col.코드).Value?.ToString

            temp_load = grid_처방_단미제

        ElseIf sender Is grid_입출고_치료대 Then

            temp_grid = grid_입출고_치료대

            If temp_grid Is Nothing AndAlso temp_grid.CurrentRow.Cells(en_입출고_Col.일자).Value = "" Then Exit Sub

            Arr_date = Split(temp_grid.CurrentRow.Cells(en_입출고_Col.일자).Value?.ToString, "-")
            temp_code = temp_grid.CurrentRow.Cells(en_입출고_Col.코드).Value?.ToString

            temp_load = grid_처방_치료대

        End If


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
				    a.처방코드 = '{temp_code}' and  
				    a.진료년 = '{Arr_date(0)}' and  
				    a.진료월 = '{Arr_date(1)}' and 
				    a.진료일 = '{Arr_date(2)}' 
			    group by 
				    a.진료년 ,a.진료월 ,a.진료일, b.수진자명, a.단가, a.명칭
            ")

        temp_load.DataSource = dtL_data
        temp_load.Update()

    End Sub


    Private Sub grid_입출고_Sorted(sender As Object, e As EventArgs) Handles grid_입출고_혼합제.Sorted, grid_입출고_단미제.Sorted, grid_입출고_치료대.Sorted

        Dim temp_grid As DataGridView

        If sender Is grid_입출고_혼합제 Then
            temp_grid = grid_입출고_혼합제
        ElseIf sender Is grid_입출고_단미제 Then
            temp_grid = grid_입출고_단미제
        ElseIf sender Is grid_입출고_치료대 Then
            temp_grid = grid_입출고_치료대
        End If

        With temp_grid
            For intL_i = .RowCount - 1 To 0 Step -1
                If .Rows(intL_i).Cells(en_입출고_Col.일자).Value = "합계" Then
                    .Rows.RemoveAt(intL_i)
                    Exit For
                End If
            Next

            temp_grid.RowCount += 1
            temp_grid.Rows(temp_grid.RowCount - 1).Cells(en_입출고_Col.일자).Value = "합계"
            temp_grid.Rows(temp_grid.RowCount - 1).Cells(en_입출고_Col.수량).Value = getValue(temp_grid.Columns(en_입출고_Col.수량).Tag).ToString("#,##0.00")
            temp_grid.Rows(temp_grid.RowCount - 1).Cells(en_입출고_Col.단가).Value = getValue(temp_grid.Columns(en_입출고_Col.수량).Tag).ToString("#,##0")
            temp_grid.Rows(temp_grid.RowCount - 1).Cells(en_입출고_Col.금액).Value = getValue(temp_grid.Columns(en_입출고_Col.수량).Tag).ToString("#,##0")
            Dim Arr_Temp = {en_입출고_Col.일자.GetHashCode, en_입출고_Col.수량.GetHashCode, en_입출고_Col.단가.GetHashCode, en_입출고_Col.금액.GetHashCode}

            For intL_i As Integer = 0 To temp_grid.ColumnCount - 1
                temp_grid.Rows(temp_grid.RowCount - 1).Cells(intL_i).Style.BackColor = Color.Yellow
                temp_grid.Rows(temp_grid.RowCount - 1).Cells(intL_i).Style.Font = New Font("맑은 고딕", 9, FontStyle.Bold)
            Next
        End With

    End Sub

    Private Sub grid_입출고_SortCompare(sender As Object, e As DataGridViewSortCompareEventArgs) Handles grid_입출고_혼합제.SortCompare, grid_입출고_단미제.SortCompare, grid_입출고_치료대.SortCompare
        Dim value1 As Decimal
        Dim value2 As Decimal

        Dim isValue1Numeric = Decimal.TryParse(e.CellValue1?.ToString(), value1)
        Dim isValue2Numeric = Decimal.TryParse(e.CellValue2?.ToString(), value2)

        If isValue1Numeric AndAlso isValue2Numeric Then
            e.SortResult = value1.CompareTo(value2)
        Else
            e.SortResult = Comparer(Of Object).Default.Compare(e.CellValue1, e.CellValue2)
        End If

        e.Handled = True

    End Sub


End Class

