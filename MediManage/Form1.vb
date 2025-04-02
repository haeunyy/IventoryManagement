Imports System.CodeDom
Imports System.ComponentModel
Imports System.Diagnostics.Eventing.Reader
Imports System.Globalization
Imports System.Reflection
Imports System.Reflection.Emit



Public Class Form1


    Private Enum en_보험약_Col
        코드 = 0
        명칭
        제약사
    End Enum
    Private Enum en_치료재료대_Col
        코드 = 0
        명칭
        제조사
        수입업소
    End Enum

    Private Enum en_재고_col
        입출고 = 0
        코드
        명칭
        입고일자
        수량
        단가
        인덱스
        구분
    End Enum

    ''' <summary>
    ''' 페이지인덱스에 따라 폼 value 초기화
    ''' </summary>
    Private Sub sD_Return_Clear()
        If tab_페이지.SelectedIndex = 0 Then

            dtp_Received.Value = Now
            txt_count.Text = ""
            txt_Price.Text = ""

        ElseIf tab_페이지.SelectedIndex = 1 Then

            dtp_received_sg.Value = Now
            txt_count_sg.Text = ""
            txt_price_sg.Text = ""

        ElseIf tab_페이지.SelectedIndex = 2 Then

            dtp_inven.Value = Now
            txt_count_inven.Text = ""
            txt_price_inven.Text = ""

        End If
    End Sub


    ''' <summary>
    ''' btn 활성화 
    ''' </summary>
    Private Sub sD_btnTrue()
        If tab_페이지.SelectedIndex = 0 Then
            btn_delete.Visible = True
            btn_Save.Visible = True
        ElseIf tab_페이지.SelectedIndex = 1 Then
            btn_del_sg.Visible = True
            btn_save_sg.Visible = True
        ElseIf tab_페이지.SelectedIndex = 2 Then
            btn_del_inven.Visible = True
            btn_save_inven.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' btn 비활성화 
    ''' </summary>
    Private Sub sD_btnFalse()
        If tab_페이지.SelectedIndex = 0 Then
            btn_delete.Visible = False
        ElseIf tab_페이지.SelectedIndex = 1 Then
            btn_del_sg.Visible = False
        ElseIf tab_페이지.SelectedIndex = 2 Then
            btn_del_inven.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' textBox 및 save 버튼 활성화 
    ''' </summary>
    Private Sub sD_enableTrue()
        If tab_페이지.SelectedIndex = 0 Then
            btn_Save.Visible = True
            dtp_Received.Enabled = True
            txt_count.Enabled = True
            txt_Price.Enabled = True
        ElseIf tab_페이지.SelectedIndex = 1 Then
            btn_save_sg.Visible = True
            dtp_received_sg.Enabled = True
            txt_count_sg.Enabled = True
            txt_price_sg.Enabled = True
        ElseIf tab_페이지.SelectedIndex = 2 Then
            btn_save_inven.Visible = True
            dtp_inven.Enabled = True
            txt_count_inven.Enabled = True
            txt_price_inven.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' textBox 비활성화 
    ''' </summary>
    Private Sub sD_enableFalse()
        If tab_페이지.SelectedIndex = 0 Then
            txt_Code.Enabled = False
            txt_Name.Enabled = False
            txt_comp.Enabled = False
        ElseIf tab_페이지.SelectedIndex = 1 Then
            txt_code_sg.Enabled = False
            txt_name_sg.Enabled = False
            txt_comp_sg.Enabled = False
        ElseIf tab_페이지.SelectedIndex = 2 Then
            txt_code_inven.Enabled = False
            txt_name_inven.Enabled = False
            txt_comp_inven.Enabled = False
            txt_수입업소.Enabled = False
        End If
    End Sub



    Private Sub sD_btnSave_False()
        If tab_페이지.SelectedIndex = 0 Then
            btn_Save.Enabled = True
        ElseIf tab_페이지.SelectedIndex = 1 Then
            btn_save_sg.Enabled = True
        ElseIf tab_페이지.SelectedIndex = 2 Then
            btn_save_inven.Enabled = True
        End If
    End Sub


    ''' <summary>
    ''' TB_재고 테이블 존재하지 않을 경우 CREATE <br/> 
    ''' </summary>
    Private Sub sD_CreateTable()
        clsG_DBmng.sql_Exec_Query(
            "
                If Not exists(select *from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'TB_재고')
                begin
                CREATE TABLE [dbo].[TB_재고](
	                [idx] [int] IDENTITY(1,1) NOT NULL,
	                [구분] [tinyint] NOT NULL,
	                [입출고] [tinyint] NOT NULL,
	                [처방코드] [nvarchar](20) NOT NULL,
	                [명칭] [varchar](100) NULL,
	                [업소] [varchar](100) NULL,
	                [일자] [varchar](100) NOT NULL,
	                [수량] [decimal](10, 2) NOT NULL,
	                [단가] [int] NOT NULL
                ) ON [PRIMARY]
                End
            ")

        'clsG_DBmng.sql_Exec_Query(
        '    "
        '        If Not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'TB_보험약' and column_name = '단가')
        '        begin
        '            alter table TB_보험약 add 단가 int
        '        End
        '    ")
    End Sub

    ''' <summary>
    '''  Form 로드되면 기준처방 리스트 조회
    ''' </summary>
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        clsG_DBmng.sql_connection_open()

        sD_CreateTable()

        sD_sql_get_mixList()
        sD_sql_get_singleList()
        sD_sql_get_treatList()

    End Sub


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
            col.Width = maxTextWidth + 20
            If col.HeaderText = " " Then col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next

        graphics.Dispose()  ' 리소스 해제

    End Sub


    ''' <summary>
    ''' DataTable 데이터를 DataGridView에 추가하는 프로시저 
    ''' </summary>
    Private Sub sD_mainRowAdd(temp_grid As DataGridView, dtL_data As DataTable)

        For Each row As DataRow In dtL_data.Rows
            Dim intL_rowIndex = temp_grid.Rows.Add(row(0), row(1), row(2))
            If temp_grid.Columns.Contains("수입업소") Then ' 치료재료대는 수입업소 컬럼에 값 추가 
                temp_grid.Rows(intL_rowIndex).Cells(en_치료재료대_Col.수입업소).Value = row(3).ToString
            End If
        Next

        sD_girdWidthSet(temp_grid)
        temp_grid.ClearSelection()

    End Sub



    ''' <summary>
    ''' 복합제 리스트 조회 쿼리 
    ''' </summary>
    Private Sub sD_sql_get_mixList()

        Dim dtL_data As DataTable = clsG_DBmng.sql_Get_Datatable(
            $"
		        select distinct a.기준코드, b.명칭 as 기준코드명칭, a.기준코드제약사 from TB_H_보험처방 a
	            inner join TB_H_마스터혼합제 b
	            on a.기준코드 = b.코드
                where a.청구코드 = '' and a.가감구분 <> '10' and b.적용일자 = (Select max(적용일자) from TB_H_마스터혼합제  where 코드 = a.기준코드 and 적용일자 NOT LIKE '%[^0-9]%' AND LEN(적용일자) = 8   and 적용일자 <= convert(char(8), getdate(), 112))
            ")
        sD_mainRowAdd(grid_혼합제, dtL_data)
    End Sub

    ''' <summary>
    ''' 단미제 리스트 조회 쿼리 
    ''' </summary>
    Private Sub sD_sql_get_singleList()
        Dim dtL_data As DataTable = clsG_DBmng.sql_Get_Datatable(
            $"
                select distinct a.청구코드 as 기준코드, b.명칭 as 기준코드명칭, b.업소 as 기준코드제약사 from TB_H_보험처방 a
	            inner join TB_H_마스터단미제 b
	            on a.청구코드 = b.코드
                where a.청구코드 <> ''
            ")

        sD_mainRowAdd(grid_단미제, dtL_data)

    End Sub


    ''' <summary>
    ''' 치료재료대 리스트 조회 쿼리 
    ''' </summary>
    Private Sub sD_sql_get_treatList()
        Dim dtL_data As DataTable = clsG_DBmng.sql_Get_Datatable(
            $"
                select a.처방코드,한글명칭, b.제조회사 as 제약사, b.수입업소 from tb_h_처방코드 a
	            inner join TB_마스터재료 b
	            on a.처방코드 = b.코드
                where 코드구분 = 8 and b.적용일자 = (Select MAX(적용일자) from TB_마스터재료 where 코드 = a.처방코드 and 적용일자 <= convert(date, getdate(), 13))
            ")

        sD_mainRowAdd(grid_치료재료대, dtL_data)
    End Sub


    ''' <summary>
    ''' 보험약 데이터 리스트의 특정 컬럼 값을 가공하는 함수<br/>
    ''' 1. 기준코드명칭에서 괄호 '(' 이후 문자열 제거<br/>
    ''' 2. 입고일자를 "yyyy-MM-dd HH:mm" 형식으로 변환<br/>
    ''' 3. 수량 컬럼이 숫자가 아니면 0으로 설정<br/>
    ''' 4. 수량이 소수점 없는 정수면 정수로 변환<br/>
    ''' </summary>
    ''' <param name="dtL_data"> 보험약 리스트, DataTable 타입 </param>
    ''' <returns>변환된 DataTable </returns>
    Private Function fD_formatData(ByVal dtL_data As DataTable)

        For Each row As DataRow In dtL_data.Rows

            If dtL_data.Columns.Contains("기준코드명칭") AndAlso Not row("기준코드명칭") Is Nothing Then
                Dim strL_split_name As String() = row("기준코드명칭").ToString.Split("(")
                row("기준코드명칭") = strL_split_name(0)
            End If

            If dtL_data.Columns.Contains("일자") AndAlso Not row("일자") Is Nothing Then
                Dim strL_date = row("일자").ToString()
                row("일자") = CDate(strL_date).ToString("yyyy-MM-dd HH:mm")
            End If

            If dtL_data.Columns.Contains("수량") AndAlso IsNumeric(row("수량")?.ToString) Then
                'row("입고수량") = 0
                Dim dblL_수량 As Double = row("수량")?.ToString

                If dblL_수량 Mod 1 = 0 Then
                    row("수량") = Math.Truncate(dblL_수량)
                End If
            End If

            If dtL_data.Columns.Contains("명칭") AndAlso Not row("명칭") Is Nothing Then
                If row("명칭").ToString.Contains("(") Then
                    Dim strL_split_name As String() = row("명칭").ToString.Split("(")
                    row("명칭") = strL_split_name(0)
                End If
            End If
        Next

        Return dtL_data

    End Function


    Private Sub sD_inout(grid_data As DataGridView)

        For Each row In grid_data.Rows
            If row.Cells(en_재고_col.입출고).value = 0 Then
                row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption
                row.DefaultCellStyle.ForeColor = SystemColors.Highlight
            End If
        Next

    End Sub


    ''' <summary>
    ''' DataTable 데이터를 DataGridView에 추가하는 프로시저 
    ''' </summary>
    Private Sub sD_invenRowAdd(temp_grid As DataGridView, dtL_data As DataTable)

        temp_grid.RowCount = 0

        Dim strL_type As String

        For Each row As DataRow In dtL_data.Rows
            Dim intL_rowIndex = temp_grid.Rows.Add(If(row("입출고") = 0, "입고", "출고"), row(1), row(2), row(3), row(4), row(5), row(6), row(7))
        Next

        With temp_grid.Columns(en_재고_col.입출고)
            .Visible = True
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None ' 크기 자동 조정 해제
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ' 중앙 정렬 적용
        End With

        sD_girdWidthSet(temp_grid)
        temp_grid.ClearSelection()

    End Sub

    ''' <summary>
    ''' 1. 페이지 인덱스에 따라 재고리스트 조회 <br/>
    ''' 2. grid에 load
    ''' </summary>
    ''' <param name="strL_Code">처방코드</param>
    Private Sub sD_load_invenList(ByVal strL_Code As String)

        Dim dtL_data As DataTable = clsG_DBmng.sql_Get_Datatable(
                $"
                    select 입출고, 처방코드 as 코드, 명칭, 일자,  수량, 단가,idx as 인덱스, 구분  from TB_재고
                    where 처방코드 = '{strL_Code}'
                ")

        If tab_페이지.SelectedIndex = 0 Then
            sD_invenRowAdd(grid_혼합제재고, dtL_data)
            grid_혼합제재고.Update()
            'fD_formatData(dtL_data)
        ElseIf tab_페이지.SelectedIndex = 1 Then
            sD_invenRowAdd(grid_단미제재고, dtL_data)
            grid_단미제재고.Update()
        ElseIf tab_페이지.SelectedIndex = 2 Then
            sD_invenRowAdd(grid_치료대재고, dtL_data)
            grid_치료대재고.Update()
        End If

    End Sub


    ''' <summary>
    ''' 기준 항목 클릭 이벤트
    ''' </summary>

    Private Sub grid_CellContentClick(sender As Object, e As EventArgs) Handles grid_혼합제.CellClick, grid_단미제.CellClick, grid_치료재료대.CellClick

        'If grid_혼합제.CurrentRow Is Nothing Then Exit Sub

        Dim temp_grid As DataGridView
        Dim temp_code As TextBox
        Dim temp_name As TextBox
        Dim temp_comp As TextBox
        Dim temp_enum As List(Of Integer)

        If sender Is grid_혼합제 Then
            temp_grid = grid_혼합제
            temp_code = txt_Code
            temp_name = txt_Name
            temp_comp = txt_comp
            temp_enum = New List(Of Integer) From {en_보험약_Col.코드, en_보험약_Col.명칭, en_보험약_Col.제약사}
        ElseIf sender Is grid_단미제 Then
            temp_grid = grid_단미제
            temp_code = txt_code_sg
            temp_name = txt_name_sg
            temp_comp = txt_comp_sg
            temp_enum = New List(Of Integer) From {en_보험약_Col.코드, en_보험약_Col.명칭, en_보험약_Col.제약사}
        ElseIf sender Is grid_치료재료대 Then
            temp_grid = grid_치료재료대
            temp_code = txt_code_inven
            temp_name = txt_name_inven
            temp_comp = txt_comp_inven
            txt_수입업소.Text = grid_치료재료대.CurrentRow.Cells(en_치료재료대_Col.수입업소).Value?.ToString
            temp_enum = New List(Of Integer) From {en_치료재료대_Col.코드, en_치료재료대_Col.명칭, en_치료재료대_Col.제조사}
        End If

        temp_code.Text = ""
        temp_name.Text = ""
        temp_comp.Text = ""
        sD_Return_Clear()

        temp_code.Text = temp_grid.CurrentRow.Cells(temp_enum(0)).Value?.ToString
        temp_name.Text = temp_grid.CurrentRow.Cells(temp_enum(1)).Value?.ToString
        temp_comp.Text = temp_grid.CurrentRow.Cells(temp_enum(2)).Value?.ToString

        sD_load_invenList(temp_code.Text)

        sD_btnFalse()

    End Sub




    ''' <summary>
    ''' 재고 항목 클릭 이벤트
    ''' </summary>
    Private Sub grid_inven_CellContentClick(sender As Object, e As EventArgs) Handles grid_혼합제재고.CellClick, grid_단미제재고.CellClick, grid_치료대재고.CellClick

        Dim temp_tab As TabControl

        If sender Is grid_혼합제재고 Then
            temp_tab = tab_혼합제재고
        ElseIf sender Is grid_단미제재고 Then
            temp_tab = tab_단미제재고
        ElseIf sender Is grid_치료대재고 Then
            temp_tab = tab_치료재재고
        End If

        With DirectCast(sender, DataGridView)

            If Not .CurrentRow Is Nothing AndAlso .CurrentRow.Cells(en_재고_col.입출고).Value = "입고" Then
                temp_tab.SelectedIndex = 0
            ElseIf Not .CurrentRow Is Nothing AndAlso .CurrentRow.Cells(en_재고_col.입출고).Value = "출고" Then
                temp_tab.SelectedIndex = 1
            End If

            dtp_Received.Value = Convert.ToDateTime(.CurrentRow.Cells(en_재고_col.입고일자).Value)
            txt_count.Text = .CurrentRow.Cells(en_재고_col.수량).Value
            txt_Price.Text = .CurrentRow.Cells(en_재고_col.단가).Value

            btn_New.Tag = idxModule.getValue(.CurrentRow.Cells(en_재고_col.인덱스).Value)
        End With

        sD_btnTrue()

    End Sub


    ''' <summary>
    ''' 새로입력 버튼 클릭 이벤트
    ''' </summary>
    Private Sub btn_New_Click(sender As Object, e As EventArgs) Handles btn_New.Click, btn_new_inven.Click, btn_new_sg.Click

        Dim temp_grid As DataGridView

        If sender Is btn_New Then
            temp_grid = grid_혼합제
        ElseIf sender Is btn_new_sg Then
            temp_grid = grid_단미제
        ElseIf sender Is btn_new_inven Then
            temp_grid = grid_치료재료대
        End If

        If temp_grid.CurrentRow Is Nothing Then
            MessageBox.Show("기준처방 항목을 먼저 선택해주세요.")
            Exit Sub
        End If

        DirectCast(sender, Button).Tag = idxModule.getValue(0)

        sD_enableTrue()
        sD_btnFalse()
        sD_Return_Clear()

    End Sub


    ''' <summary>
    ''' 1. grid 행 선택여부 및 Input 입력값의 유효성 검증
    ''' </summary>
    ''' <param name="currentRow">grid에서 선택된 현재 row </param>
    ''' <param name="count">수량</param>
    ''' <param name="price">단가</param>
    ''' <returns>검증실패한 경우 True 반환</returns>
    Private Function sD_checkInput(currentRow As DataGridViewRow, count As TextBox, price As TextBox) As Boolean

        If currentRow Is Nothing Then
            MessageBox.Show("기준 항목을 선택해주세요.")
            Return True
        End If

        ' 빈 입력값 체크
        If String.IsNullOrWhiteSpace(count.Text) Or String.IsNullOrWhiteSpace(price.Text) Then
            MessageBox.Show("항목을 입력해주세요.")
            Return True
        End If

        ' Decimal(10,2) 형식의 유효성 검사
        If Not IsValidDecimal(count.Text) Or Not IsValidDecimal(price.Text) Then
            MessageBox.Show("유효하지 않은 값입니다. ")
            Return True
        End If

        Return False ' 유효한 경우 False 반환
    End Function


    ' Decimal(10,2) 유효성 검사 함수
    Private Function IsValidDecimal(input As String) As Boolean

        Dim decimalValue As Decimal

        ' 숫자 형식으로 변환 가능한지 검사
        If Not Decimal.TryParse(input, decimalValue) Then
            Return False
        End If

        Dim numericPart As String = input.Replace(".", "")

        ' 전체 자리수 10자리 이하 확인 (소수점 제외)
        If numericPart.Length > 10 Then
            Return False
        End If

        Dim parts() As String = input.Split("."c)

        ' 소수점 이하 2자리까지만 허용
        If parts.Length = 2 AndAlso parts(1).Length > 2 Then
            Return False
        End If

        Return True
    End Function


    ''' <summary>
    ''' TextBox의 수량과 단가가 입력될때마다 event 발생하여 문자입력 제한 
    ''' </summary>
    Private Sub txt_count_price_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_count.KeyPress, txt_count_sg.KeyPress, txt_count_inven.KeyPress, txt_Price.KeyPress, txt_price_sg.KeyPress, txt_price_inven.KeyPress
        ' 숫자, 백스페이스 또는 소수점(.)만 입력 허용
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If

        ' 소수점이 이미 한 번 입력되었다면 두 번째 소수점은 허용하지 않음
        Dim txtBox As TextBox = CType(sender, TextBox)
        If e.KeyChar = "."c AndAlso txtBox.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub



    ''' <summary>
    ''' 보험약 저장 버튼 클릭 이벤트
    ''' </summary>
    Private Sub sD_Save(sender As Object, e As EventArgs) Handles btn_Save.Click, btn_save_inven.Click, btn_save_sg.Click

        Dim temp_grid As DataGridViewRow
        Dim temp_dtp As DateTimePicker
        Dim temp_count As Double
        Dim temp_price As TextBox

        Dim temp_enum As List(Of Integer)
        Dim temp_idx As Decimal
        Dim temp_btn As Button

        Dim intD_IO_type As Integer
        Dim intD_item_type As Integer

        If sender Is btn_Save Then

            If sD_checkInput(grid_혼합제.CurrentRow, txt_count, txt_Price) Then Exit Sub

            temp_grid = grid_혼합제.CurrentRow
            temp_enum = New List(Of Integer) From {en_보험약_Col.코드, en_보험약_Col.명칭, en_보험약_Col.제약사}
            temp_btn = btn_New
            temp_idx = idxModule.getValue(temp_btn.Tag)

            temp_dtp = dtp_Received
            temp_count = CDbl(txt_count.Text)
            temp_price = txt_Price
            intD_item_type = tab_페이지.SelectedIndex
            intD_IO_type = tab_혼합제재고.SelectedIndex

        ElseIf Not grid_단미제 Is Nothing AndAlso sender Is btn_save_sg Then

            If sD_checkInput(grid_단미제.CurrentRow, txt_count_sg, txt_price_sg) Then Exit Sub

            temp_grid = grid_단미제.CurrentRow
            temp_enum = New List(Of Integer) From {en_보험약_Col.코드, en_보험약_Col.명칭, en_보험약_Col.제약사}

            temp_btn = btn_new_sg
            temp_idx = idxModule.getValue(temp_btn.Tag)

            temp_dtp = dtp_received_sg
            temp_count = CDbl(txt_count_sg.Text)
            temp_price = txt_price_sg
            intD_item_type = tab_페이지.SelectedIndex
            intD_IO_type = tab_단미제재고.SelectedIndex

        ElseIf Not grid_치료재료대 Is Nothing AndAlso sender Is btn_save_inven Then

            If sD_checkInput(grid_치료재료대.CurrentRow, txt_count_inven, txt_price_inven) Then Exit Sub

            temp_grid = grid_치료재료대.CurrentRow
            temp_enum = New List(Of Integer) From {en_치료재료대_Col.코드, en_치료재료대_Col.명칭, en_치료재료대_Col.제조사}

            temp_btn = btn_new_inven
            temp_idx = idxModule.getValue(temp_btn.Tag)

            temp_dtp = dtp_inven
            temp_count = CDbl(txt_count_inven.Text)
            temp_price = txt_price_inven
            intD_item_type = tab_페이지.SelectedIndex
            intD_IO_type = tab_치료재재고.SelectedIndex

        End If

        Dim strL_code = temp_grid.Cells(temp_enum(0)).Value?.ToString
        Dim strL_name = temp_grid.Cells(temp_enum(1)).Value?.ToString
        Dim strL_comp = temp_grid.Cells(temp_enum(2)).Value?.ToString

        Dim strL_date As String = temp_dtp.Value.ToString("yyyy-MM-dd HH:mm:ss")

        Dim strL_count = temp_count
        Dim int_price As Integer = Convert.ToInt32(temp_price.Text)


        If temp_idx > 0 Then
            clsG_DBmng.sql_Exec_Query(
                $"
                    Update tb_재고 set 일자 = '{strL_date}', 수량 = '{strL_count}', 단가 = '{int_price}' 
                    where idx = '{temp_idx}'
                ")
        Else
            temp_btn.Tag = clsG_DBmng.sql_Exec_Query_returnindex(
             $"
                insert TB_재고 (처방코드,구분,입출고, 명칭, 업소, 일자,수량, 단가)
                values('{strL_code}', '{intD_item_type}','{intD_IO_type}', '{strL_name}', '{strL_comp}', '{strL_date}', '{strL_count}', '{int_price}');
                SELECT @@IDENTITY
             ")
        End If

        sD_load_invenList(strL_code)

    End Sub


    ''' <summary>
    ''' 삭제 버튼 클릭 이벤트
    ''' </summary>
    Private Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click, btn_del_inven.Click, btn_del_sg.Click

        If (MessageBox.Show("해당 항목을 삭제하시겠습니까?", "재고삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = DialogResult.No Then Exit Sub

        Dim temp_grid As DataGridView
        Dim temp_enum As Integer
        Dim temp_index As Integer
        Dim temp_table As String

        If sender Is btn_delete Then
            temp_grid = grid_혼합제재고
            temp_index = grid_혼합제재고.CurrentRow.Cells(en_재고_col.인덱스).Value
        ElseIf sender Is btn_del_sg Then
            temp_grid = grid_단미제재고
            temp_index = grid_단미제재고.CurrentRow.Cells(en_재고_col.인덱스).Value
        ElseIf sender Is btn_del_inven Then
            temp_grid = grid_치료대재고
            temp_index = grid_치료대재고.CurrentRow.Cells(en_재고_col.인덱스).Value
        End If

        clsG_DBmng.sql_Exec_Query(
            $"
                delete tb_재고 Where idx = '{temp_index}'
            ")

        sD_Return_Clear()

        sD_load_invenList(temp_grid.CurrentRow.Cells(en_재고_col.코드).Value)

    End Sub


    ''' <summary>
    ''' 1. tab_재고 인덱스에 따라 panel 추가 
    ''' 2. 수량,일자 text 변경
    ''' 3. btn_새로입력 Tag 초기화 
    ''' </summary>
    Private Sub tab_재고_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tab_단미제재고.SelectedIndexChanged, tab_혼합제재고.SelectedIndexChanged, tab_치료재재고.SelectedIndexChanged

        If sender Is tab_혼합제재고 Then
            If tab_혼합제재고.SelectedIndex = 0 Then
                If Not pnl_혼합제_입출고.Parent Is tp_혼합제_입고 Then
                    tp_혼합제_입고.Controls.Add(pnl_혼합제_입출고)
                    lbl_혼합제_입출고_수량.Text = "입고수량(g)"
                    lbl_혼합제_입출고_일자.Text = "입고일자"
                End If
            Else
                If Not pnl_혼합제_입출고.Parent Is tp_혼합제_출고 Then
                    tp_혼합제_출고.Controls.Add(pnl_혼합제_입출고)
                    lbl_혼합제_입출고_수량.Text = "출고수량(g)"
                    lbl_혼합제_입출고_일자.Text = "출고일자"
                End If
            End If

            btn_New.Tag = 0

        ElseIf sender Is tab_단미제재고 Then
            If tab_단미제재고.SelectedIndex = 0 Then
                If Not pnl_단미제_입출고.Parent Is tp_단미제_입고 Then
                    tp_단미제_입고.Controls.Add(pnl_단미제_입출고)
                    lbl_단미제_입출고_수량.Text = "입고수량(g)"
                    lbl_단미제_입출고_일자.Text = "입고일자"
                End If
            Else
                If Not pnl_단미제_입출고.Parent Is tp_단미제_출고 Then
                    tp_단미제_출고.Controls.Add(pnl_단미제_입출고)
                    lbl_단미제_입출고_수량.Text = "출고수량(g)"
                    lbl_단미제_입출고_일자.Text = "출고일자"
                End If
            End If

            btn_new_sg.Tag = 0

        ElseIf sender Is tab_치료재재고 Then
            If tab_치료재재고.SelectedIndex = 0 Then
                If Not pnl_치료대_입출고.Parent Is tp_치료대_입고 Then
                    tp_치료대_입고.Controls.Add(pnl_치료대_입출고)
                    lbl_치료대_입출고_수량.Text = "입고수량(g)"
                    lbl_치료대_입출고_일자.Text = "입고일자"
                End If
            Else
                If Not pnl_치료대_입출고.Parent Is tp_치료대_출고 Then
                    tp_치료대_출고.Controls.Add(pnl_치료대_입출고)
                    lbl_치료대_입출고_수량.Text = "출고수량(g)"
                    lbl_치료대_입출고_일자.Text = "출고일자"
                End If
            End If

            btn_new_inven.Tag = 0

        End If

        sD_Return_Clear()
    End Sub


    ''' <summary>
    ''' tab_페이지 인덱스를 관리하기 위한 변수 선언
    ''' </summary>
    Private intL_tabIndex As Integer

    Private frmL_form2 As Form2 '============================================================ form 종료하고 재실행 안됨 

    ''' <summary>
    ''' 1. 재고현황 탭을 눌렀을 떄 Form2를 실행 <br/>
    ''' 2. 탭을 누르기 이전 인덱스로 돌아가기
    ''' </summary>
    Private Sub tab_페이지_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tab_페이지.SelectedIndexChanged

        If tab_페이지.SelectedIndex = 3 Then

            tab_페이지.SelectedIndex = intL_tabIndex

            If frmL_form2 Is Nothing Then
                frmL_form2 = New Form2()
                frmL_form2.Show()
                Exit Sub
            End If

        End If

        intL_tabIndex = tab_페이지.SelectedIndex
    End Sub

    ''' <summary>
    ''' 1. Form1이 종료될 때 재고현황도 같이 종료
    ''' </summary>
    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If frmL_form2 Is Nothing Then Exit Sub
        frmL_form2.Close()
    End Sub
    Private Sub helper_MouseHover(sender As Object, e As EventArgs) Handles btn_new_sg.MouseHover, btn_del_sg.MouseHover, btn_save_sg.MouseHover, btn_New.MouseHover, btn_delete.MouseHover, btn_Save.MouseHover, btn_new_inven.MouseHover, btn_del_inven.MouseHover, btn_save_inven.MouseHover
        If Not dicG_helper.ContainsKey(sender.Name) Then Exit Sub
        Dim strL_value = dicG_helper(sender.Name)
        frm_Main.lbl_도움말.Text = strL_value
    End Sub

    Private Sub helper_MouseLeave(sender As Object, e As EventArgs) Handles btn_new_sg.MouseLeave, btn_del_sg.MouseLeave, btn_save_sg.MouseHover, btn_New.MouseLeave, btn_delete.MouseLeave, btn_Save.MouseLeave, btn_new_inven.MouseLeave, btn_del_inven.MouseLeave, btn_save_inven.MouseLeave
        frm_Main.lbl_도움말.Text = ""
    End Sub




End Class
