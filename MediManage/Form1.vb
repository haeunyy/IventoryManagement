Imports System.CodeDom
Imports System.Diagnostics.Eventing.Reader
Imports System.Reflection.Emit



Public Class Form1


    Private Enum en_보험약_Col
        코드 = 1
        명칭
        제약사
    End Enum

    Private Enum en_보험약재고내역_col
        인덱스 = 0
        코드
        명칭
        입고일자
        수량
        단가
        입고일자_변환
    End Enum

    'Private intD_index As Integer = 0
    'Private intD_index_sg As Integer = 0
    'Private intD_index_in As Integer = 0
    'Private intD_o_index As Integer = 0
    'Private intD_o_index_sg As Integer = 0
    'Private intD_o_index_in As Integer = 0

    ''' <summary>
    ''' 페이지인덱스에 따라 폼 value 초기화
    ''' </summary>
    Private Sub sD_Return_Clear()
        If tab_페이지.SelectedIndex = 0 Then

            'If tab_혼합제재고.SelectedIndex = 0 Then grid_혼합제_inven.CurrentRow.Cells(en_보험약재고내역_col.인덱스) = 0
            'If tab_혼합제재고.SelectedIndex = 1 Then intD_o_index = 0

            dtp_Received.Value = Now
            txt_count.Text = ""
            txt_Price.Text = ""

        ElseIf tab_페이지.SelectedIndex = 1 Then

            'If tab_단미제재고.SelectedIndex = 0 Then intD_index_sg = 0
            'If tab_단미제재고.SelectedIndex = 1 Then intD_o_index_sg = 0

            dtp_received_sg.Value = Now
            txt_count_sg.Text = ""
            txt_price_sg.Text = ""

        ElseIf tab_페이지.SelectedIndex = 2 Then

            'If tab_치료재재고.SelectedIndex = 0 Then intD_index_in = 0
            'If tab_치료재재고.SelectedIndex = 1 Then intD_o_index_in = 0

            dtp_inven.Value = Now
            txt_count_inven.Text = ""
            txt_price_inven.Text = ""

        End If
    End Sub


    '''''''''''''''''''''''''출고에서 입고항목 눌러도 버튼 활성호ㅏ 됨 버튼 항목 나눠야할듯...
    ''' <summary>
    ''' btn 활성화 
    ''' </summary>
    Private Sub sD_btnTrue()
        If tab_페이지.SelectedIndex = 0 Then
            btn_Update.Visible = True
            btn_delete.Visible = True
        ElseIf tab_페이지.SelectedIndex = 1 Then
            btn_update_sg.Visible = True
            btn_del_sg.Visible = True
        ElseIf tab_페이지.SelectedIndex = 2 Then
            btn_update_inven.Visible = True
            btn_del_inven.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' btn 비활성화 
    ''' </summary>
    Private Sub sD_btnFalse()
        If tab_페이지.SelectedIndex = 0 Then
            btn_Save.Visible = False
            btn_Update.Visible = False
            btn_delete.Visible = False
        ElseIf tab_페이지.SelectedIndex = 1 Then
            btn_save_sg.Visible = False
            btn_update_sg.Visible = False
            btn_del_sg.Visible = False
        ElseIf tab_페이지.SelectedIndex = 2 Then
            btn_save_inven.Visible = False
            btn_update_inven.Visible = False
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
            dtp_Received.Enabled = False
            txt_count.Enabled = False
            txt_Price.Enabled = False
        ElseIf tab_페이지.SelectedIndex = 1 Then
            txt_code_sg.Enabled = False
            txt_name_sg.Enabled = False
            txt_comp_sg.Enabled = False
            dtp_received_sg.Enabled = False
            txt_count_sg.Enabled = False
            txt_price_sg.Enabled = False
        ElseIf tab_페이지.SelectedIndex = 2 Then
            txt_code_inven.Enabled = False
            txt_name_inven.Enabled = False
            txt_comp_inven.Enabled = False
            txt_수입업소.Enabled = False
            dtp_inven.Enabled = False
            txt_count_inven.Enabled = False
            txt_price_inven.Enabled = False
        End If
    End Sub

    'Private Sub sD_btnSave_True()
    '    If tab_페이지.SelectedIndex = 0 Then
    '        btn_Save.Enabled = True
    '    ElseIf tab_페이지.SelectedIndex = 1 Then
    '        btn_save_sg.Enabled = True
    '    ElseIf tab_페이지.SelectedIndex = 2 Then
    '        btn_save_inven.Enabled = True
    '    End If
    'End Sub

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

        grid_혼합제.DataSource = fD_formatData(dtL_data)
        grid_혼합제.Update()

        grid_혼합제.ClearSelection()

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

        grid_단미제.DataSource = fD_formatData(dtL_data)
        grid_단미제.Update()

        grid_단미제.ClearSelection()

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

        grid_치료재료대.DataSource = dtL_data
        grid_치료재료대.Update()

        grid_치료재료대.ClearSelection()

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
                row("일자") = CDate(strL_date).ToString("yyyy-MM-dd HH:mm") 'strL_date.Substring(0, strL_date.Length - 3)
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




    ''' <summary>
    ''' 1. 페이지 인덱스에 따라 재고리스트 조회 <br/>
    ''' 2. grid에 load
    ''' </summary>
    ''' <param name="strL_Code">처방코드</param>
    Private Sub sD_load_invenList(ByVal strL_Code As String)

        Dim dtL_dataList As DataTable = clsG_DBmng.sql_Get_Datatable(
                $"
                    select idx as 인덱스, 처방코드 as 코드, 명칭, 일자,  수량, 단가 from TB_재고
                    where 처방코드 = '{strL_Code}'
                ")

        If tab_페이지.SelectedIndex = 0 Then

            grid_혼합제_inven.DataSource = fD_formatData(dtL_dataList)
            grid_혼합제_inven.Columns("수량").DefaultCellStyle.Format = "#,##0.##"
            grid_혼합제_inven.Columns("단가").DefaultCellStyle.Format = "#,##0"
            grid_혼합제_inven.Update()

            sD_enableFalse()

        ElseIf tab_페이지.SelectedIndex = 1 Then

            grid_단미제_inven.DataSource = fD_formatData(dtL_dataList)
            grid_단미제_inven.Columns("수량_단미").DefaultCellStyle.Format = "#,##0.##"
            grid_단미제_inven.Columns("단가_단미").DefaultCellStyle.Format = "#,##0"
            grid_단미제_inven.Update()

            sD_enableFalse()

        ElseIf tab_페이지.SelectedIndex = 2 Then

            grid_재료대재고내역.DataSource = fD_formatData(dtL_dataList)
            grid_재료대재고내역.Columns("수량_재료").DefaultCellStyle.Format = "#,##0.##"
            grid_재료대재고내역.Columns("단가_재료").DefaultCellStyle.Format = "#,##0"
            grid_재료대재고내역.Update()

            sD_enableFalse()

        End If


    End Sub


    ''' <summary>
    ''' 기준처방 항목 클릭 이벤트
    ''' </summary>
    Private Sub grid_Medi_CellContentClick(sender As Object, e As EventArgs) Handles grid_혼합제.CellClick

        If grid_혼합제.CurrentRow Is Nothing Then Exit Sub

        sD_Return_Clear()
        txt_Code.Text = ""
        txt_Name.Text = ""
        txt_comp.Text = ""

        txt_Code.Text = grid_혼합제.CurrentRow.Cells(en_보험약_Col.코드).Value?.ToString
        txt_Name.Text = grid_혼합제.CurrentRow.Cells(en_보험약_Col.명칭).Value?.ToString
        txt_comp.Text = grid_혼합제.CurrentRow.Cells(en_보험약_Col.제약사).Value?.ToString

        sD_load_invenList(txt_Code.Text)

        sD_btnFalse()
        sD_enableFalse()

    End Sub


    ''' <summary>
    ''' 재고 항목 클릭 이벤트
    ''' </summary>
    Private Sub grid_보험약_CellContentClick(sender As Object, e As EventArgs) Handles grid_혼합제_inven.CellClick

        If grid_혼합제_inven.CurrentRow Is Nothing Then
            sD_btnFalse()
            Exit Sub
        Else
            sD_btnTrue()
            sD_enableFalse()
            tab_혼합제재고.SelectedIndex = 0
            dtp_Received.Value = Convert.ToDateTime(grid_혼합제_inven.CurrentRow.Cells(en_보험약재고내역_col.입고일자).Value)
            txt_count.Text = grid_혼합제_inven.CurrentRow.Cells(en_보험약재고내역_col.수량).Value
            txt_Price.Text = grid_혼합제_inven.CurrentRow.Cells(en_보험약재고내역_col.단가).Value
        End If

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

        sD_enableTrue()
        'sD_btnFalse()
        sD_Return_Clear()

    End Sub


    ''' <summary>
    ''' 1. 셀 선택여부 및 Input으로 입력된 값을 검증하여 반환값이 True인 경우 함수 종료
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

        If count.Text = "" Or price.Text = "" Then
            MessageBox.Show("항목을 입력해주세요.")
            Return True
        End If
    End Function


    ' 선택된 항목이 없는 경우 변환오류 날수있어서 오류 처리 어떻게 할지 생각해봐야함 

    ''' <summary>
    ''' 보험약 저장 버튼 클릭 이벤트
    ''' </summary>
    Private Sub sD_Save(sender As Object, e As EventArgs) Handles btn_Save.Click, btn_save_inven.Click, btn_save_sg.Click

        Dim temp_grid As DataGridView
        Dim temp_dtp As DateTimePicker
        Dim temp_count As Double
        Dim temp_price As TextBox

        Dim temp_index As Integer

        Dim temp_code As Integer
        Dim temp_name As Integer
        Dim temp_comp As Integer

        Dim intD_IO_type As Integer
        Dim intD_item_type As Integer

        If sender Is btn_Save Then 'Not grid_혼합제.CurrentRow Is Nothing AndAlso

            If sD_checkInput(grid_혼합제.CurrentRow, txt_count, txt_Price) Then Exit Sub

            temp_grid = grid_혼합제
            temp_code = en_보험약_Col.코드
            temp_name = en_보험약_Col.명칭
            temp_comp = en_보험약_Col.제약사

            temp_dtp = dtp_Received
            temp_count = CDbl(txt_count.Text)
            temp_price = txt_Price
            temp_index = grid_혼합제.CurrentRow.Cells(en_보험약재고내역_col.인덱스).Value
            intD_item_type = tab_페이지.SelectedIndex
            intD_IO_type = tab_혼합제재고.SelectedIndex

        ElseIf Not grid_단미제 Is Nothing AndAlso sender Is btn_save_sg Then 'Or sender Is btn_o_save_sg 

            If sD_checkInput(grid_단미제.CurrentRow, txt_count_sg, txt_price_sg) Then Exit Sub

            temp_grid = grid_단미제
            temp_code = en_보험약_Col.코드
            temp_name = en_보험약_Col.명칭
            temp_comp = en_보험약_Col.제약사

            temp_dtp = dtp_received_sg
            temp_count = CDbl(txt_count_sg.Text)
            temp_price = txt_price_sg
            temp_index = grid_단미제.CurrentRow.Cells(en_보험약재고내역_col.인덱스).Value
            intD_item_type = tab_페이지.SelectedIndex
            intD_IO_type = tab_단미제재고.SelectedIndex

        ElseIf Not grid_치료재료대 Is Nothing AndAlso sender Is btn_save_inven Then

            If sD_checkInput(grid_치료재료대.CurrentRow, txt_count_inven, txt_price_inven) Then Exit Sub

            temp_grid = grid_치료재료대
            temp_code = en_치료재료대_Col.코드
            temp_name = en_치료재료대_Col.명칭
            temp_comp = en_치료재료대_Col.제조사

            temp_dtp = dtp_inven
            temp_count = CDbl(txt_count_inven.Text)
            temp_price = txt_price_inven
            temp_index = grid_치료재료대.CurrentRow.Cells(en_치료재료대재고_Col.인덱스).Value
            intD_item_type = tab_페이지.SelectedIndex
            intD_IO_type = tab_치료재재고.SelectedIndex

        End If

        Dim strL_code = temp_grid.CurrentRow.Cells(temp_code).Value?.ToString
        Dim strL_name = temp_grid.CurrentRow.Cells(temp_name).Value?.ToString
        Dim strL_comp = temp_grid.CurrentRow.Cells(temp_comp).Value?.ToString

        Dim strL_date As String = temp_dtp.Value.ToString("yyyy-MM-dd HH:mm:ss")

        Dim strL_count = temp_count
        Dim int_price As Integer = Convert.ToInt32(temp_price.Text)

        If temp_index > 0 Then
            clsG_DBmng.sql_Exec_Query(
                $"
                    Update tb_재고 set 일자 = '{strL_date}', 수량 = '{strL_count}', 단가 = '{int_price}' 
                    where idx = '{temp_index}'
                ")
        Else
            temp_index = clsG_DBmng.sql_Exec_Query_returnindex(
             $"
                insert TB_재고 (처방코드,구분,입출고, 명칭, 업소, 일자,수량, 단가)
                values('{strL_code}', '{intD_item_type}','{intD_IO_type}', '{strL_name}', '{strL_comp}', '{strL_date}', '{strL_count}', '{int_price}');
                SELECT @@IDENTITY
             ")
        End If

        sD_Return_Clear()

        sD_load_invenList(strL_code)

    End Sub


    ''' <summary>
    ''' 수정 버튼 클릭 이벤트
    ''' </summary>
    Private Sub btn_Update_Click(sender As Object, e As EventArgs) Handles btn_Update.Click, btn_update_inven.Click, btn_update_sg.Click

        If sender Is btn_Update Then
            If grid_혼합제_inven.CurrentRow Is Nothing Then Exit Sub
        ElseIf sender Is btn_update_sg Then
            If grid_단미제_inven.CurrentRow Is Nothing Then Exit Sub
        ElseIf sender Is btn_update_inven Then
            If grid_재료대재고내역.CurrentRow Is Nothing Then Exit Sub
        End If

        sD_enableTrue()

    End Sub


    ''' <summary>
    ''' 삭제 버튼 클릭 이벤트
    ''' </summary>
    Private Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click, btn_del_inven.Click, btn_del_sg.Click

        Dim temp_grid As DataGridView
        Dim temp_enum As Integer
        Dim temp_index As Integer
        Dim temp_table As String

        If sender Is btn_delete Then
            temp_grid = grid_혼합제_inven
            temp_enum = en_보험약_Col.코드
            temp_index = grid_혼합제_inven.CurrentRow.Cells(en_보험약재고내역_col.인덱스).Value
        ElseIf sender Is btn_del_sg Then 'Or sender Is btn_o_del_sg
            temp_grid = grid_단미제_inven
            temp_enum = en_보험약_Col.코드
            temp_index = grid_단미제_inven.CurrentRow.Cells(en_보험약재고내역_col.인덱스).Value
        ElseIf sender Is btn_del_inven Then
            temp_grid = grid_재료대재고내역
            temp_enum = en_치료재료대재고_Col.처방코드
            temp_index = grid_재료대재고내역.CurrentRow.Cells(en_치료재료대재고_Col.인덱스).Value
        End If

        clsG_DBmng.sql_Exec_Query(
            $"
                delete tb_재고 Where idx = '{temp_index}'
            ")

        sD_Return_Clear()

        sD_load_invenList(temp_grid.CurrentRow.Cells(temp_enum).Value?.ToString)

    End Sub


    ''' <summary>
    ''' 단미제 항목 클릭 이벤트
    ''' </summary>
    Private Sub grid_단미제_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grid_단미제.CellClick, grid_단미제.CellContentClick

        '현재 단미제 컬럼 가지고와서 있는지 확인하고 조회 
        If grid_단미제.CurrentRow Is Nothing Then Exit Sub

        sD_Return_Clear()
        txt_code_sg.Text = ""
        txt_name_sg.Text = ""
        txt_comp_sg.Text = ""

        txt_code_sg.Text = grid_단미제.CurrentRow.Cells(en_보험약_Col.코드).Value?.ToString
        txt_name_sg.Text = grid_단미제.CurrentRow.Cells(en_보험약_Col.명칭).Value?.ToString
        txt_comp_sg.Text = grid_단미제.CurrentRow.Cells(en_보험약_Col.제약사).Value?.ToString

        sD_load_invenList(txt_code_sg.Text)

        sD_btnFalse()
        sD_enableFalse()

    End Sub


    ''' <summary>
    ''' 단미제 재고항목 클릭 이벤트
    ''' </summary>
    Private Sub grid_단미제_inven_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grid_단미제_inven.CellClick

        If grid_단미제_inven.CurrentRow Is Nothing Then
            sD_btnFalse()
            Exit Sub
        Else
            sD_btnTrue()
            dtp_received_sg.Value = Convert.ToDateTime(grid_단미제_inven.CurrentRow.Cells(en_보험약재고내역_col.입고일자).Value)
            txt_count_sg.Text = grid_단미제_inven.CurrentRow.Cells(en_보험약재고내역_col.수량).Value
            txt_price_sg.Text = grid_단미제_inven.CurrentRow.Cells(en_보험약재고내역_col.단가).Value
        End If

    End Sub


    ''' <summary>
    '''   치료재료대 enum 
    ''' </summary>
    ''' 
    Private Enum en_치료재료대_Col
        코드 = 0
        명칭
        제조사
        수입업소
    End Enum
    Private Enum en_치료재료대재고_Col
        인덱스 = 0
        처방코드
        명칭
        입고일자
        수량
        단가
        입고일자_변환
    End Enum


    ''' <summary>
    ''' 치료재료대 내역 클릭
    ''' </summary>
    Private Sub grid_치료재료대_CellClick(sender As Object, e As EventArgs) Handles grid_치료재료대.CellClick

        If grid_치료재료대.CurrentRow Is Nothing Then Exit Sub

        sD_Return_Clear()
        txt_code_inven.Text = ""
        txt_name_inven.Text = ""
        txt_comp_inven.Text = ""

        txt_code_inven.Text = grid_치료재료대.CurrentRow.Cells(en_치료재료대_Col.코드).Value?.ToString
        txt_name_inven.Text = grid_치료재료대.CurrentRow.Cells(en_치료재료대_Col.명칭).Value?.ToString
        txt_comp_inven.Text = grid_치료재료대.CurrentRow.Cells(en_치료재료대_Col.제조사).Value?.ToString
        txt_수입업소.Text = grid_치료재료대.CurrentRow.Cells(en_치료재료대_Col.수입업소).Value?.ToString

        sD_load_invenList(txt_code_inven.Text)

        sD_btnFalse()
        sD_enableFalse()

    End Sub



    ''' <summary>
    ''' 치료재료대 재고내역 클릭
    ''' </summary>
    Private Sub grid_재료대재고내역_CellContentClick(sender As Object, e As EventArgs) Handles grid_재료대재고내역.CellClick

        If grid_재료대재고내역.CurrentRow Is Nothing Then
            sD_btnFalse()
            Exit Sub
        Else
            sD_btnTrue()
            dtp_inven.Value = Convert.ToDateTime(grid_재료대재고내역.CurrentRow.Cells(en_치료재료대재고_Col.입고일자).Value)
            txt_count_inven.Text = grid_재료대재고내역.CurrentRow.Cells(en_치료재료대재고_Col.수량).Value
            txt_price_inven.Text = grid_재료대재고내역.CurrentRow.Cells(en_치료재료대재고_Col.단가).Value
        End If
    End Sub

    Private Sub tab_재고_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tab_단미제재고.SelectedIndexChanged, tab_혼합제재고.SelectedIndexChanged, tab_치료재재고.SelectedIndexChanged

        Dim isSender As String = ""

        If sender Is tab_혼합제재고 Then
            isSender = "혼합제"
        ElseIf sender Is tab_단미제재고 Then
            isSender = "단미제"
        ElseIf sender Is tab_치료재재고 Then
            isSender = "치료재료대"
        End If

        Select Case isSender

            Case "혼합제"

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

            Case "단미제"

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

            Case "치료재료대"

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

        End Select
    End Sub

End Class
