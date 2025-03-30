Imports System.CodeDom.Compiler
Imports System.ComponentModel
Imports System.Data.Common
Imports System.Data.OleDb
Imports System.IO
Imports System.Net.Security
Imports System.Runtime.InteropServices
Imports System.Runtime.Remoting.Metadata
Imports System.Windows.Forms.VisualStyles
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
    Private Enum en_처방_Col
        일자 = 0
        수진자명
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

        sD_iniControl()

        sD_get_mainList(tab_재고현황.SelectedIndex)
        sD_get_mainList(tab_재고현황.SelectedIndex + 1)
        sD_get_mainList(tab_재고현황.SelectedIndex + 2)

    End Sub


    ''' <summary>
    ''' load시에 grid_처방 Object를 grid_입출고 태그에 선언
    ''' </summary>
    Private Sub sD_iniControl()
        grid_기준항목_혼합제.Tag = grid_입출고_혼합제
        grid_기준항목_단미제.Tag = grid_입출고_단미제
        grid_기준항목_치료대.Tag = grid_입출고_치료대
        grid_입출고_혼합제.Tag = grid_처방_혼합제
        grid_입출고_단미제.Tag = grid_처방_단미제
        grid_입출고_치료대.Tag = grid_처방_치료대
    End Sub


    ''' <summary>
    ''' 1. 구분코드에 따라 조회 구문 실행<br/>
    ''' 2. DataGridView에 update
    ''' </summary>
    ''' <param name="index">구분 (0-혼합제/1-단미제/2-치료재료대)</param>
    Private Sub sD_get_mainList(index As Integer)

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
	                구분 = {index} and
	                입출고 = 0 
                group by 
	                처방코드, 명칭, 업소
            ")
        Dim temp_grid As DataGridView

        If index = 0 Then
            temp_grid = grid_기준항목_혼합제
        ElseIf index = 1 Then
            temp_grid = grid_기준항목_단미제
        ElseIf index = 2 Then
            temp_grid = grid_기준항목_치료대
        End If

        temp_grid.DataSource = dtL_data
        sD_girdWidthSet(temp_grid)

        temp_grid.Update()
        temp_grid.ClearSelection()

    End Sub

    Dim intL_width As Integer = 0

    ''' <summary>
    ''' grid 넓이 길이에 따라 여백 주기 
    ''' </summary>
    Private Sub sD_girdWidthSet(temp_grid As DataGridView)
        Dim font As Font = temp_grid.DefaultCellStyle.Font  ' 현재 그리드 폰트 가져오기
        Dim graphics As Graphics = temp_grid.CreateGraphics() ' 글자 폭 계산용 그래픽 객체

        For Each col As DataGridViewColumn In temp_grid.Columns

            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None

            Dim maxTextWidth As Integer = 0  ' 해당 컬럼에서 가장 긴 텍스트의 너비

            ' 컬럼 헤더 텍스트 길이 측정
            maxTextWidth = Math.Max(maxTextWidth, TextRenderer.MeasureText(col.HeaderText, font).Width)

            ' 각 셀의 텍스트 길이 측정
            For Each row As DataGridViewRow In temp_grid.Rows
                If row.Cells(col.Index).Value IsNot Nothing Then
                    Dim text As String = row.Cells(col.Index).Value.ToString()
                    Dim textWidth As Integer = TextRenderer.MeasureText(text, font).Width
                    maxTextWidth = Math.Max(maxTextWidth, textWidth)
                End If
            Next

            ' 최종 너비 설정 (여백 포함)
            col.Width = maxTextWidth + 15
        Next

        graphics.Dispose()  ' 리소스 해제

    End Sub

    ''' <summary>
    ''' 입출고내역 조회
    ''' </summary>
    ''' <param name="index">구분 (0-혼합제/1-단미제/2-치료재료대)</param>
    Private Sub sD_Get_InOutList(index As Integer)

        Dim invenQuery As String
        Dim mediQuery As String
        Dim typeQuery As String
        Dim dateInvenQuery As String
        Dim dateMediQuery As String
        Dim temp_startDate As DateTimePicker
        Dim temp_endDate As DateTimePicker
        Dim temp_grid As DataGridView

        If index = 0 Then

            temp_startDate = dtp_시작일자_혼합제
            temp_endDate = dtp_종료일자_혼합제

            If dtp_시작일자_혼합제.Tag Is Nothing Then dtp_시작일자_혼합제.Tag = chk_혼합

            Dim row = grid_기준항목_혼합제.CurrentRow

            If rb_기준항목_혼합.Checked Then '기준항목조회 라디오에 체크가 되어있다면 조건에 처방코드 추가  
                If row Is Nothing Then Exit Sub
                invenQuery = " And 처방코드 = '" + row.Cells(en_기준항목_Col.코드).Value + "'"
                mediQuery = "and b.처방코드 = '" + row.Cells(en_기준항목_Col.코드).Value + "'"
            End If

            typeQuery = "b.등록구분 = 29 and" 'tb_h_진료내역에서 혼합제 조건 

            temp_grid = grid_입출고_혼합제

        ElseIf index = 1 Then

            temp_startDate = dtp_시작일자_단미
            temp_endDate = dtp_종료일자_단미

            If dtp_시작일자_단미.Tag Is Nothing Then dtp_시작일자_단미.Tag = chk_단미

            Dim row = grid_기준항목_단미제.CurrentRow

            If rb_기준항목_단미.Checked Then '기준항목조회 라디오에 체크가 되어있다면 조건에 처방코드 추가 
                invenQuery = "and 처방코드 = '" + row.Cells(en_기준항목_Col.코드).Value + "'"
                mediQuery = "and b.처방코드 = '" + row.Cells(en_기준항목_Col.코드).Value + "'"
            End If

            typeQuery = "b.등록구분 in(25,26,28) and" 'tb_h_진료내역에서 단미제 조건

            temp_grid = grid_입출고_단미제

        ElseIf index = 2 Then

            temp_startDate = dtp_시작일자_치료대
            temp_endDate = dtp_종료일자_치료대

            If dtp_시작일자_치료대.Tag Is Nothing Then dtp_시작일자_치료대.Tag = chk_치료대

            Dim row = grid_기준항목_치료대.CurrentRow

            If rb_기준항목_치료대.Checked Then '기준항목조회 라디오에 체크가 되어있다면 조건에 처방코드 추가 
                invenQuery = "and 처방코드 = '" + row.Cells(en_기준항목_Col.코드).Value + "'"
                mediQuery = "and b.처방코드 = '" + row.Cells(en_기준항목_Col.코드).Value + "'"
            End If

            typeQuery = "b.코드구분 = 8 and" 'tb_h_진료내역에서 치료대 조건 

            temp_grid = grid_입출고_치료대

        End If

        '전체기간 체크박스가 활성화 되어있다면 전체기간(조건없음), 아니라면 datetimepicker set에 따라 기간 조회  
        If Not temp_startDate.Tag.checked Then
            dateInvenQuery = "and 일자 between '" + CDate(temp_startDate.Value).ToString("yyyy-MM-dd") + "' and '" + CDate(temp_endDate.Value).ToString("yyyy-MM-dd") + "'"
            dateMediQuery = "and a.진료일자 between convert(date,'" + CDate(temp_startDate.Value).ToString("yyyy-MM-dd") + "') and convert(date,'" + CDate(temp_endDate.Value).ToString("yyyy-MM-dd") + "')"
        Else
            dateInvenQuery = ""
            dateMediQuery = ""
        End If

        Dim dtL_data = clsG_DBmng.sql_Get_Datatable(
            $"

                Select 
                    convert(nvarchar(10), 일자, 13) as 일자, case when 입출고 = 0 then '재고 입고' else '재고 출고' end as 입출고, 처방코드 as 코드
                                                                        , case when charindex('(', 명칭) > 0 then left(명칭, charindex('(', 명칭) - 1) else 명칭 end  as 명칭
                                                                        , 수량 as 수량, cast(단가 as int) as 단가, cast(수량 * 단가 as bigint) as 금액
                from
                    TB_재고 a
                where
                    구분 = {tab_재고현황.SelectedIndex} 
                    {dateInvenQuery}
                    {invenQuery}

                Union ALL

                Select
                    a.진료일자 as 일자, '진료입력 출고' as 입출고, b.처방코드 as 코드
                                                                        , case when charindex('(', 명칭) > 0 then left(명칭, charindex('(', 명칭) - 1) else 명칭 end  as 명칭
                                                                        , sum(b.투여량 * b.일수) as 수량, cast(b.단가 as int) as 단가, cast(sum(b.투여량 * b.일수 * b.단가) as bigint) as 금액
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
                     (Select distinct 처방코드 from TB_재고) c
                on
                     b.처방코드 = c.처방코드
                where
                    {typeQuery}
                    a.진료상태 <> 4 
                    {dateMediQuery}
                    {mediQuery}
                group by
	                a.진료일자,  b.처방코드, b.명칭, b.단가
                order by
                    일자
            ")

        temp_grid.SuspendLayout()
        Me.SuspendLayout()

        sD_Total(temp_grid, dtL_data, True)
        sD_girdWidthSet(temp_grid)

        temp_grid.ResumeLayout()
        Me.ResumeLayout()

    End Sub

    ''' <summary>
    ''' DataGridView에 DataTable 데이터를 로드하고, 수량/단가/금액 합계를 계산하여 표시 
    ''' </summary>
    ''' <param name="temp_grid">대상 DataGridView  </param>
    ''' <param name="dtL_data">로드할 DataTable  </param>
    ''' <param name="isInOut">True=입출고, False=처방 (Enum 선택) </param>
    Private Sub sD_Total(temp_grid As DataGridView, dtL_data As DataTable, isInOut As Boolean)

        ' 선택할 Enum 배열 설정 
        Dim temp_en() As Integer = If(isInOut,
                                        {en_입출고_Col.수량, en_입출고_Col.단가, en_입출고_Col.금액},
                                        {en_처방_Col.수량, en_처방_Col.단가, en_처방_Col.금액})

        ' DataGridView 초기화
        temp_grid.RowCount = 0

        ' 컬럼 태그 값 초기화
        temp_grid.Columns(temp_en(0)).Tag = 0
        temp_grid.Columns(temp_en(1)).Tag = 0
        temp_grid.Columns(temp_en(2)).Tag = 0

        Dim dblL_수량 As Double = 0
        Dim lngL_단가 As Long = 0
        Dim lngL_금액 As Long = 0

        ' 데이터 테이블의 각 행을 순회하며 DataGridView에 값 채우기
        For intL_i As Integer = 0 To dtL_data.Rows.Count - 1

            temp_grid.RowCount += 1

            For intL_j As Integer = 0 To temp_grid.Columns.Count - 1
                If temp_grid.Columns(intL_j).DataPropertyName <> "" Then

                    ' 수량, 단가, 금액 컬럼이면 합계를 계산
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

        ' 데이터가 존재하면 합계 행 추가
        If temp_grid.RowCount <> 0 Then
            temp_grid.RowCount += 1
            temp_grid.Rows(temp_grid.RowCount - 1).Cells(en_입출고_Col.일자).Value = "합계"
            temp_grid.Rows(temp_grid.RowCount - 1).Cells(temp_en(0)).Value = dblL_수량.ToString("#,##0.00")
            temp_grid.Columns(temp_en(0)).Tag = dblL_수량.ToString("#,##0.00")
            temp_grid.Rows(temp_grid.RowCount - 1).Cells(temp_en(1)).Value = lngL_단가.ToString("#,##0")
            temp_grid.Columns(temp_en(1)).Tag = lngL_단가.ToString("#,##0")
            temp_grid.Rows(temp_grid.RowCount - 1).Cells(temp_en(2)).Value = lngL_금액.ToString("#,##0")
            temp_grid.Columns(temp_en(2)).Tag = lngL_금액.ToString("#,##0")

            For intL_i As Integer = 0 To temp_grid.ColumnCount - 1
                temp_grid.Rows(temp_grid.RowCount - 1).Cells(intL_i).Style.BackColor = Color.Yellow
                temp_grid.Rows(temp_grid.RowCount - 1).Cells(intL_i).Style.Font = New Font("맑은 고딕", 9, FontStyle.Bold)
            Next
        End If

    End Sub

    ''' <summary>
    ''' 조회 버튼클릭시 입출고 내역 조회 
    ''' </summary>
    Private Sub btn_조회_Click(sender As Object, e As EventArgs) Handles btn_조회_혼합.Click, btn_조회_단미.Click, btn_조회_치료대.Click
        sD_Get_InOutList(tab_재고현황.SelectedIndex)
    End Sub


    ''' <summary>
    ''' 입출고내역 클릭했을때 처방내역 조회 
    ''' </summary>
    Private Sub grid_입출고_Click(sender As DataGridView, e As EventArgs) Handles grid_입출고_혼합제.Click, grid_입출고_단미제.Click, grid_입출고_치료대.Click

        Dim Arr_date As String()
        Dim temp_code As String

        If sender.RowCount = 0 Then Exit Sub

        If sender.CurrentRow.Cells(en_입출고_Col.일자).Value.ToString.Trim = "합계" Then '클릭된 행이 합계 행인 경우 진료처방 내역 클리어 
            DirectCast(sender.Tag, DataGridView).RowCount = 0
            Exit Sub
        End If

        Arr_date = Split(sender.CurrentRow.Cells(en_입출고_Col.일자).Value?.ToString, "-") ' 진료내역에 조건으로 입력하기 위해 일자를 split하여 진료년/월/일 배열로 나열 
        temp_code = sender.CurrentRow.Cells(en_입출고_Col.코드).Value?.ToString

        Dim dtL_data = clsG_DBmng.sql_Get_Datatable(
            $"
                select 
				    (a.진료년 +'-'+ a.진료월 + '-' + a.진료일) as 일자, b.수진자명 
                                                                        , case when charindex('(', a.명칭) > 0 then left(a.명칭, charindex('(', a.명칭) - 1) else 명칭 end  as 명칭 
                                                                        , sum(a.투여량 * a.일수) as 수량, cast(a.단가 as int) as 단가, cast(sum(a.투여량 * a.일수 * a.단가) as bigint) as 금액 
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

        sD_girdWidthSet(DirectCast(sender.Tag, DataGridView))
        sD_Total(DirectCast(sender.Tag, DataGridView), dtL_data, False)

    End Sub

    ''' <summary>
    ''' 입출고 내역 정렬 시 "합계" 행이 항상 마지막에 위치하도록 조정하는 프로시저
    ''' </summary>
    Private Sub grid_입출고_Sorted(sender As DataGridView, e As EventArgs) Handles grid_처방_혼합제.Sorted, grid_처방_단미제.Sorted, grid_처방_치료대.Sorted, grid_입출고_혼합제.Sorted, grid_입출고_단미제.Sorted, grid_입출고_치료대.Sorted

        ' 선택할 Enum 배열 설정 
        Dim temp_en() As Integer = If(sender.RowCount = 8,
                                        {en_입출고_Col.수량, en_입출고_Col.단가, en_입출고_Col.금액},
                                        {en_처방_Col.수량, en_처방_Col.단가, en_처방_Col.금액})

        With sender
            If .RowCount = 0 Then Exit Sub

            For intL_i = .RowCount - 1 To 0 Step -1
                If .Rows(intL_i).Cells(en_입출고_Col.일자).Value = "합계" Then
                    .Rows.RemoveAt(intL_i)
                    Exit For
                End If
            Next

            .RowCount += 1
            .Rows(.RowCount - 1).Cells(en_입출고_Col.일자).Value = "합계"
            .Rows(.RowCount - 1).Cells(temp_en(0)).Value = getValue(.Columns(temp_en(0)).Tag).ToString("#,##0.00")
            .Rows(.RowCount - 1).Cells(temp_en(1)).Value = getValue(.Columns(temp_en(1)).Tag).ToString("#,##0")
            .Rows(.RowCount - 1).Cells(temp_en(2)).Value = getValue(.Columns(temp_en(2)).Tag).ToString("#,##0")

            For intL_i As Integer = 0 To .ColumnCount - 1
                .Rows(.RowCount - 1).Cells(intL_i).Style.BackColor = Color.Yellow
                .Rows(.RowCount - 1).Cells(intL_i).Style.Font = New Font("맑은 고딕", 9, FontStyle.Bold)
            Next
        End With

    End Sub

    Private Sub grid_입출고_SortCompare(sender As Object, e As DataGridViewSortCompareEventArgs) Handles grid_처방_혼합제.SortCompare, grid_처방_단미제.SortCompare, grid_처방_치료대.SortCompare, grid_입출고_혼합제.SortCompare, grid_입출고_단미제.SortCompare, grid_입출고_치료대.SortCompare

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


    ''' <summary>
    ''' 기준항목내역 클릭시 라디오에 기준항목이 선택되어 있다면 처방내역 클리어하고 입출고내역 조회 
    ''' </summary>
    Private Sub grid_기준항목_혼합제_Click(sender As Object, e As EventArgs) Handles grid_기준항목_혼합제.Click, grid_기준항목_단미제.Click, grid_기준항목_치료대.Click

        If sender Is grid_기준항목_혼합제 Then
            If rb_전체항목_혼합.Checked Then Exit Sub
            If grid_처방_혼합제.RowCount > 0 Then grid_처방_혼합제.RowCount = 0
            sD_Get_InOutList(tab_재고현황.SelectedIndex)
        ElseIf sender Is grid_기준항목_단미제 Then
            If rb_전체항목_단미.Checked Then Exit Sub
            If grid_처방_단미제.RowCount > 0 Then grid_처방_단미제.RowCount = 0
            sD_Get_InOutList(tab_재고현황.SelectedIndex)
        ElseIf sender Is grid_기준항목_치료대 Then
            If rb_전체항목_치료대.Checked Then Exit Sub
            If grid_처방_치료대.RowCount > 0 Then grid_처방_치료대.RowCount = 0
            sD_Get_InOutList(tab_재고현황.SelectedIndex)
        End If

    End Sub

End Class

