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

    Private Class cls_grids

        Private mGrids As DataGridView

        Public Sub New(index As Integer)
            mGrids = New DataGridView

            With mGrids
                If index = 0 Then
                    .Width = 432
                    .Columns.Add("코드", "코드")
                    .Columns.Add("명칭", "명칭")
                    .Columns.Add("업소", "업소")
                    .Columns.Add("업소", "수량")
                ElseIf index = 1 Then
                    .Width = 414
                    .Columns.Add("일자", "일자")
                    .Columns.Add("입출고", "입출고")
                    .Columns.Add("코드", "코드")
                    .Columns.Add("명칭", "명칭")
                    .Columns.Add("수량", "수량")
                    .Columns.Add("단가", "단가")
                    .Columns.Add("금액", "금액")
                ElseIf index = 2 Then
                    .Width = 427
                    .Columns.Add("일자", "일자")
                    .Columns.Add("수진자명", "수진자명")
                    .Columns.Add("명칭", "명칭")
                    .Columns.Add("수량", "수량")
                    .Columns.Add("단가", "단가")
                    .Columns.Add("금액", "금액")
                End If

                .Height = 554
                .AllowUserToAddRows = False
                .MultiSelect = False
                .ReadOnly = True
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .Anchor = AnchorStyles.Bottom Or AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Left
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .RowHeadersWidth = 15
                .BackgroundColor = Color.WhiteSmoke
                .BorderStyle = BorderStyle.None
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                With mGrids.ColumnHeadersDefaultCellStyle
                    .BackColor = SystemColors.Control
                    .ForeColor = SystemColors.WindowText
                    .SelectionBackColor = Color.White
                    .SelectionForeColor = Color.Black
                    .WrapMode = DataGridViewTriState.True
                    .Alignment = DataGridViewContentAlignment.MiddleCenter
                End With
                .ColumnHeadersHeight = 23
                .ColumnHeadersVisible = True
                With mGrids.DefaultCellStyle
                    .BackColor = SystemColors.Window
                    .ForeColor = SystemColors.ControlText
                    .SelectionBackColor = SystemColors.GradientInactiveCaption
                    .SelectionForeColor = SystemColors.Highlight
                    .WrapMode = DataGridViewTriState.False
                    .Alignment = DataGridViewContentAlignment.MiddleLeft
                End With
                With mGrids.RowHeadersDefaultCellStyle
                    .BackColor = SystemColors.Control
                    .ForeColor = SystemColors.WindowText
                    .SelectionBackColor = Color.White
                    .SelectionForeColor = Color.Black
                    .WrapMode = DataGridViewTriState.True
                    .Alignment = DataGridViewContentAlignment.MiddleLeft
                End With
                .Location = New Point(4, 16)
            End With
        End Sub


        Public Property name As String
            Get
                Return mGrids.Name
            End Get
            Set(value As String)
                mGrids.Name = value
            End Set
        End Property


        Public Property Grids As DataGridView
            Get
                Return mGrids
            End Get
            Set(value As DataGridView)
                If Not mGrids Is Nothing Then mGrids.Dispose()
                value.Visible = True
                mGrids = value
            End Set
        End Property
    End Class

    Private Class cls_gridList
        Public m탭구분 As en_탭구분
        Public grd_기준항목 As DataGridView
        Public grd_입출고내역 As DataGridView
        Public grd_진료처방내역 As DataGridView
    End Class

    Private lstD_Grid As New List(Of cls_gridList)

    Private Enum en_탭구분
        혼합제 = 0
        단미제
        치료재료대
    End Enum

    Private Enum en_기준항목_Col
        코드 = 0
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

        For int_i As Integer = 0 To tab_재고현황.TabCount - 1

            Dim tab As TabPage = tab_재고현황.TabPages(int_i)

            For int_j As Integer = 1 To 3
                Dim gbName As String = $"gb_{int_i + 1}_{int_j}"
                Dim gb As GroupBox '= TryCast(tab.Controls(gbName), GroupBox)

                For Each item In tab.Controls
                    If Not TryCast(item, GroupBox) Is Nothing Then
                        If DirectCast(item, GroupBox).Name.ToString.ToUpper = gbName.ToUpper Then
                            gb = item
                            Exit For
                        End If
                    End If
                Next

                If Not gb Is Nothing Then
                    Dim grid As New cls_grids(int_j - 1)
                    grid.name = $"grid_{int_i}_{int_j}"
                    gb.Controls.Add(grid.Grids)

                    If int_j = 1 Then
                        lstD_Grid.Add(New cls_gridList With {.m탭구분 = int_i, .grd_기준항목 = grid.Grids})
                    Else
                        If int_j = 2 Then
                            lstD_Grid(int_i).grd_입출고내역 = grid.Grids
                            grid.Grids.Columns(en_입출고_Col.수량).DefaultCellStyle.Format = "#,##0.00"
                            grid.Grids.Columns(en_입출고_Col.단가).DefaultCellStyle.Format = "#,##0"
                            grid.Grids.Columns(en_입출고_Col.금액).DefaultCellStyle.Format = "#,##0"

                            AddHandler grid.Grids.Click, AddressOf grid_입출고_Click
                            AddHandler grid.Grids.Sorted, AddressOf grid_입출고_Sorted
                            AddHandler grid.Grids.SortCompare, AddressOf grid_입출고_SortCompare
                        Else
                            lstD_Grid(int_i).grd_진료처방내역 = grid.Grids
                            lstD_Grid(int_i).grd_입출고내역.Tag = lstD_Grid(int_i).grd_진료처방내역
                            AddHandler grid.Grids.Sorted, AddressOf grid_입출고_Sorted
                            AddHandler grid.Grids.SortCompare, AddressOf grid_입출고_SortCompare
                        End If
                    End If

                    If int_j = 1 Then

                        Dim dtL_data As DataTable = clsG_DBmng.sql_Get_Datatable(
                        $"
                            Select 
                                처방코드 AS 코드,
                                case when charindex('(', 명칭) > 0 then left(명칭, charindex('(', 명칭) - 1) else 명칭 end  as 명칭,
                                업소,
                                SUM(case when 입출고 = 1 then -abs(수량) else abs(수량) end) as 수량
                            From
                                tb_재고 As a
                            Where
                                구분 = {int_i} And
                                입출고 = 0
                            Group By
                                처방코드, 명칭, 업소
                        ")

                        Dim intL_i As Integer = 0

                        For Each row In dtL_data.Rows
                            intL_i = grid.Grids.Rows.Add(row(en_기준항목_Col.코드), row(en_기준항목_Col.명칭), row(en_기준항목_Col.업소))
                            grid.Grids.Rows(intL_i).Cells(en_기준항목_Col.수량).Value = row(en_기준항목_Col.수량)
                            intL_i += 1
                        Next

                        sD_format(grid.Grids)
                        sD_girdWidthSet(grid.Grids)
                        grid.Grids.Update()
                        grid.Grids.AreAllCellsSelected(False)
                    End If
                End If
            Next
        Next
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
                If Not row.Cells(col.Index).Value Is Nothing Then
                    If col.HeaderText = "코드" Then ' 처방코드 Column은 열너비 고정
                        maxTextWidth = 70
                    Else
                        Dim text As String = row.Cells(col.Index).Value.ToString()
                        Dim textWidth As Integer = TextRenderer.MeasureText(text, font).Width
                        maxTextWidth = Math.Max(maxTextWidth, textWidth)
                    End If
                End If
            Next

            ' 최종 너비 설정 (여백 포함)
            col.Width = maxTextWidth + 15
        Next

        graphics.Dispose()  ' 리소스 해제

        ' 가로스크롤이 없는 경우에만 마지막 column에 fill 생성
        With temp_grid
            If .Controls.OfType(Of HScrollBar)().Any(Function(x) x.Visible) Then Exit Sub
            Dim intD_index As Integer

            If Not .Columns.Cast(Of DataGridViewColumn)().Any(Function(x) x.HeaderText.Trim = "") Then
                intD_index = .Columns.Add("비고", " ")
                .Columns(intD_index).DisplayIndex = .ColumnCount - 1
                .Columns(intD_index).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If

            .Update()
        End With
    End Sub


    ''' <summary>
    ''' 입출고내역 조회
    ''' </summary>
    ''' <param name="index">구분 (0-혼합제/1-단미제/2-치료재료대)</param>
    Private Async Sub sD_Get_InOutList(index As Integer)

        MainForm.prg_상태바.Value = 0
        MainForm.lbl_상태.Text = "데이터를 불러오는 중입니다."
        MainForm.StatusStrip1.Update()

        Dim invenQuery As String
        Dim mediQuery As String
        Dim typeQuery As String
        Dim dateInvenQuery As String
        Dim dateMediQuery As String
        Dim temp_startDate As DateTimePicker
        Dim temp_endDate As DateTimePicker
        Dim temp_grid As New DataGridView

        If index = 0 Then

            temp_startDate = dtp_시작일자_혼합제
            temp_endDate = dtp_종료일자_혼합제
            temp_grid = lstD_Grid(index).grd_입출고내역

            If dtp_시작일자_혼합제.Tag Is Nothing Then dtp_시작일자_혼합제.Tag = chk_혼합

            Dim row = DirectCast(lstD_Grid(index).grd_기준항목, DataGridView).CurrentRow

            If Not row Is Nothing AndAlso rb_기준항목_혼합.Checked Then '기준항목조회 라디오에 체크가 되어있다면 조건에 처방코드 추가  
                invenQuery = " And 처방코드 = '" + row.Cells(en_기준항목_Col.코드).Value + "'"
                mediQuery = "and b.처방코드 = '" + row.Cells(en_기준항목_Col.코드).Value + "'"
            End If

            typeQuery = "b.등록구분 = 29 and" 'tb_h_진료내역에서 혼합제 조건 

        ElseIf index = 1 Then

            temp_startDate = dtp_시작일자_단미
            temp_endDate = dtp_종료일자_단미
            temp_grid = lstD_Grid(index).grd_입출고내역

            If dtp_시작일자_단미.Tag Is Nothing Then dtp_시작일자_단미.Tag = chk_단미

            Dim row = DirectCast(lstD_Grid(index).grd_기준항목, DataGridView).CurrentRow

            If Not row Is Nothing AndAlso rb_기준항목_단미.Checked Then '기준항목조회 라디오에 체크가 되어있다면 조건에 처방코드 추가 
                invenQuery = "and 처방코드 = '" + row.Cells(en_기준항목_Col.코드).Value + "'"
                mediQuery = "and b.처방코드 = '" + row.Cells(en_기준항목_Col.코드).Value + "'"
            End If

            typeQuery = "b.등록구분 in(25,26,28) and" 'tb_h_진료내역에서 단미제 조건

        ElseIf index = 2 Then

            temp_startDate = dtp_시작일자_치료대
            temp_endDate = dtp_종료일자_치료대
            temp_grid = lstD_Grid(index).grd_입출고내역

            If dtp_시작일자_치료대.Tag Is Nothing Then dtp_시작일자_치료대.Tag = chk_치료대

            Dim row = DirectCast(lstD_Grid(index).grd_기준항목, DataGridView).CurrentRow

            If Not row Is Nothing AndAlso rb_기준항목_치료대.Checked Then '기준항목조회 라디오에 체크가 되어있다면 조건에 처방코드 추가 
                invenQuery = "and 처방코드 = '" + row.Cells(en_기준항목_Col.코드).Value + "'"
                mediQuery = "and b.처방코드 = '" + row.Cells(en_기준항목_Col.코드).Value + "'"
            End If

            typeQuery = "b.코드구분 = 8 and" 'tb_h_진료내역에서 치료대 조건 
        End If

        ' 시작일자가 종료일자보다 크다면 종료일자에 맞춤
        If temp_startDate.Value > temp_endDate.Value Then
            temp_startDate.Value = temp_endDate.Value
        End If

        ' 전체기간 체크박스가 활성화 되어있다면 전체기간(조건없음), 아니라면 datetimepicker set에 따라 기간 조회  
        If Not temp_startDate.Tag.checked Then
            dateInvenQuery = "and 일자 between '" + CDate(temp_startDate.Value).ToString("yyyy-MM-dd") + "' and '" + CDate(temp_endDate.Value).ToString("yyyy-MM-dd") + "'"
            dateMediQuery = "and a.진료일자 between convert(date,'" + CDate(temp_startDate.Value).ToString("yyyy-MM-dd") + "') and convert(date,'" + CDate(temp_endDate.Value).ToString("yyyy-MM-dd") + "')"
        Else
            dateInvenQuery = ""
            dateMediQuery = ""
        End If

        temp_grid.RowCount = 0

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

        sD_Total(temp_grid, dtL_data, True)
        sD_girdWidthSet(temp_grid)
        sD_format(temp_grid)


        'temp_grid.Update()
        temp_grid.ResumeLayout()
        Me.ResumeLayout()
        temp_grid.ClearSelection()

        If temp_grid.Rows.Count = 0 Then
            MainForm.lbl_상태.Text = "데이터가 없습니다."
        Else
            MainForm.lbl_상태.Text = "데이터 조회가 완료되었습니다."
        End If
        MainForm.StatusStrip1.Update()


    End Sub

    Private Sub sD_format(temp_grid As DataGridView)

        If temp_grid.RowCount = 0 Then Exit Sub

        For intL_i As Integer = 0 To temp_grid.ColumnCount - 1
            With temp_grid.Columns(intL_i)
                Select Case temp_grid.Columns(intL_i).HeaderText
                    Case "코드"
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    Case "입출고"
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    Case "수량"
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        .DefaultCellStyle.Format = "#,##0.00"
                    Case "단가"
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        .DefaultCellStyle.Format = "#,##0"
                    Case "금액"
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        .DefaultCellStyle.Format = "#,##0"
                    Case "일자"
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    Case "수진자명"
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End Select
            End With
        Next

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

        MainForm.prg_상태바.Value = 0
        MainForm.prg_상태바.Maximum = dtL_data.Rows.Count
        MainForm.StatusStrip1.Update()

        ' 데이터 테이블의 각 행을 순회하며 DataGridView에 값 채우기
        For intL_i As Integer = 0 To dtL_data.Rows.Count - 1

            MainForm.prg_상태바.Value += 1
            MainForm.StatusStrip1.Update()

            If isInOut Then
                temp_grid.Rows.Add(
                    dtL_data.Rows(intL_i)(en_입출고_Col.일자),
                    dtL_data.Rows(intL_i)(en_입출고_Col.입출고),
                    dtL_data.Rows(intL_i)(en_입출고_Col.코드),
                    dtL_data.Rows(intL_i)(en_입출고_Col.명칭),
                    dtL_data.Rows(intL_i)(en_입출고_Col.수량),
                    dtL_data.Rows(intL_i)(en_입출고_Col.단가),
                    dtL_data.Rows(intL_i)(en_입출고_Col.금액))
            Else
                temp_grid.Rows.Add(
                        dtL_data.Rows(intL_i)(en_처방_Col.일자),
                        dtL_data.Rows(intL_i)(en_처방_Col.수진자명),
                        dtL_data.Rows(intL_i)(en_처방_Col.명칭),
                        dtL_data.Rows(intL_i)(en_처방_Col.수량),
                        dtL_data.Rows(intL_i)(en_처방_Col.단가),
                        dtL_data.Rows(intL_i)(en_처방_Col.금액))
            End If

            For intL_j As Integer = 0 To temp_grid.Columns.Count - 1
                If temp_grid.Columns(intL_j).HeaderText.Trim <> "" Then
                    ' 수량, 단가, 금액 컬럼이면 합계를 계산
                    If Array.IndexOf({"수량", "단가", "금액"}, temp_grid.Columns(intL_j).HeaderText) >= 0 Then
                        temp_grid.Rows(temp_grid.RowCount - 1).Cells(intL_j).Value = CDbl(dtL_data.Rows(intL_i)(intL_j)).ToString(If(temp_grid.Columns(intL_j).HeaderText = "수량",
                                                                                                                                                                "#,##0.00", "#,##0"))
                        If temp_grid.Columns(intL_j).HeaderText = "수량" Then dblL_수량 += getValue(dtL_data.Rows(intL_i)(temp_en(0)).ToString)
                        If temp_grid.Columns(intL_j).HeaderText = "단가" Then lngL_단가 += getValue(dtL_data.Rows(intL_i)(temp_en(1)).ToString)
                        If temp_grid.Columns(intL_j).HeaderText = "금액" Then lngL_금액 += getValue(dtL_data.Rows(intL_i)(temp_en(2)).ToString)
                    Else
                        temp_grid.Rows(temp_grid.RowCount - 1).Cells(intL_j).Value = dtL_data.Rows(intL_i)(temp_grid.Columns(intL_j).HeaderText).ToString
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

        temp_grid.Update()
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
    Private Sub grid_입출고_Click(sender As DataGridView, e As EventArgs)

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

        sD_Total(DirectCast(sender.Tag, DataGridView), dtL_data, False)
        sD_girdWidthSet(DirectCast(sender.Tag, DataGridView))
        sD_format(DirectCast(sender.Tag, DataGridView))

        DirectCast(sender.Tag, DataGridView).Update()
        DirectCast(sender.Tag, DataGridView).ClearSelection()

        If dtL_data.Rows.Count = 0 Then
            MainForm.lbl_상태.Text = "데이터가 없습니다."
        Else
            MainForm.lbl_상태.Text = "데이터 조회가 완료되었습니다."
        End If

        MainForm.StatusStrip1.Update()

    End Sub

    ''' <summary>
    ''' 입출고 내역 정렬 시 "합계" 행이 항상 마지막에 위치하도록 조정하는 프로시저
    ''' </summary>
    Private Sub grid_입출고_Sorted(sender As DataGridView, e As EventArgs)

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

    Private Sub grid_입출고_SortCompare(sender As Object, e As DataGridViewSortCompareEventArgs)

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


    Private Sub tbn_입출고_MouseHover(sender As Object, e As EventArgs) Handles rb_기준항목_혼합.MouseHover, rb_기준항목_단미.MouseHover, rb_기준항목_치료대.MouseHover, rb_전체항목_혼합.MouseHover, rb_전체항목_단미.MouseHover, rb_전체항목_치료대.MouseHover, chk_혼합.MouseHover, chk_단미.MouseHover, chk_치료대.MouseHover, btn_조회_치료대.MouseHover, btn_조회_단미.MouseHover, btn_조회_혼합.MouseHover
        If Not dicG_helper.ContainsKey(sender.Name) Then Exit Sub
        Dim strL_value = dicG_helper(sender.Name)
        MainForm.lbl_도움말.Text = strL_value
    End Sub

    Private Sub tbn_입출고_MouseLeave(sender As Object, e As EventArgs) Handles rb_기준항목_혼합.MouseLeave, rb_기준항목_단미.MouseLeave, rb_기준항목_치료대.MouseLeave, rb_전체항목_혼합.MouseLeave, rb_전체항목_단미.MouseLeave, rb_전체항목_치료대.MouseLeave, chk_혼합.MouseLeave, chk_단미.MouseLeave, chk_치료대.MouseHover, btn_조회_치료대.MouseLeave, btn_조회_단미.MouseLeave, btn_조회_혼합.MouseLeave
        MainForm.lbl_도움말.Text = ""
    End Sub

End Class

