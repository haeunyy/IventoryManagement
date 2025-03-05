Imports System.Reflection.Emit

Public Class Form1

    Private Enum en_보험약_Col
        코드 = 1
        명칭
        제약사
    End Enum

    Private Enum en_보험약재고내역_col
        코드 = 0
        명칭
        입고일자
        수량
        단가
        idx
        입고일자_변환
    End Enum

    Private intD_index As Integer = 0

    Private Sub sD_Return_Clear()
        intD_index = 0
        dtp_Received.Value = Now
        txt_count.Text = ""
        txt_Price.Text = ""
    End Sub

    ''' <summary>
    ''' textBox 활성화 
    ''' </summary>
    Private Sub sD_btnTrue()
        If tap_페이지.SelectedIndex = 0 Or tap_페이지.SelectedIndex = 9 Then
            btn_Save.Visible = True
            btn_Update.Visible = True
            btn_delete.Visible = True
        ElseIf tap_페이지.SelectedIndex = 1 Then
            btn_new_sg.Visible = True
            btn_update_sg.Visible = True
            btn_del_sg.Visible = True
        ElseIf tap_페이지.SelectedIndex = 2 Then
            btn_save_inven.Visible = True
            btn_update_inven.Visible = True
            btn_del_inven.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' btn 비활성화 
    ''' </summary>
    Private Sub sD_btnFalse()
        If tap_페이지.SelectedIndex = 0 Or tap_페이지.SelectedIndex = 9 Then
            btn_Update.Visible = False
            btn_delete.Visible = False
        ElseIf tap_페이지.SelectedIndex = 1 Then
            btn_update_sg.Visible = False
            btn_del_sg.Visible = False
        ElseIf tap_페이지.SelectedIndex = 2 Then
            btn_update_inven.Visible = False
            btn_del_inven.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' textBox 활성화 
    ''' </summary>
    Private Sub sD_txtEnableTrue()
        If tap_페이지.SelectedIndex = 0 Or tap_페이지.SelectedIndex = 9 Then
            dtp_Received.Enabled = True
            txt_count.Enabled = True
            txt_Price.Enabled = True
        ElseIf tap_페이지.SelectedIndex = 1 Then
            dtp_received_sg.Enabled = True
            txt_count_sg.Enabled = True
            txt_price_sg.Enabled = True
        ElseIf tap_페이지.SelectedIndex = 2 Then
            dtp_inven.Enabled = True
            txt_count_inven.Enabled = True
            txt_price_inven.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' textBox 비활성화 
    ''' </summary>
    Private Sub sD_txtEnableFalse()
        If tap_페이지.SelectedIndex = 0 Or tap_페이지.SelectedIndex = 9 Then
            txt_Code.Enabled = False
            txt_Name.Enabled = False
            txt_comp.Enabled = False
            dtp_Received.Enabled = False
            txt_count.Enabled = False
            txt_Price.Enabled = False
        ElseIf tap_페이지.SelectedIndex = 1 Then
            txt_code_sg.Enabled = False
            txt_name_sg.Enabled = False
            txt_comp_sg.Enabled = False
            dtp_received_sg.Enabled = False
            txt_count_sg.Enabled = False
            txt_price_sg.Enabled = False
        ElseIf tap_페이지.SelectedIndex = 2 Then
            txt_code_inven.Enabled = False
            txt_name_inven.Enabled = False
            txt_comp_inven.Enabled = False
            txt_수입업소.Enabled = False
            dtp_inven.Enabled = False
            txt_count_inven.Enabled = False
            txt_price_inven.Enabled = False
        End If
    End Sub


    ''' <summary>
    ''' TB_보험약, TB_치료재료대 테이블 존재하지 않을 경우 CREATE <br/> 
    ''' ++ TB_보험약 [단가] 컬럼 추가 
    ''' </summary>
    Private Sub sD_CreateTable()
        clsG_DBmng.sql_Exec_Query(
            "
                If Not exists(select *from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'TB_보험약')
                begin
            CREATE TABLE [dbo].[TB_보험약](
                        [idx] [Int] IDENTITY(1, 1) Not NULL,
	                    [처방코드] [nvarchar](20) NULL,
                        [명칭] [varchar](100) NULL,   
                        [제약사] [varchar](100) NULL,
                        [입고일자] [varchar](100) NULL,
                        [수량] [Decimal](10, 2) NULL
                    ) ON [PRIMARY]
                End

            If Not exists(select *from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'TB_치료재료대')
                begin
                CREATE TABLE [dbo].[TB_치료재료대](
                        [idx] [Int] IDENTITY(1, 1) Not NULL,
	                    [처방코드] [nvarchar](20) NULL,
                        [명칭] [varchar](100) NULL,   
                        [제약사] [varchar](100) NULL,
                        [입고일자] [varchar](100) NULL,
                        [수량] [Int] NULL,
                        [단가] [Int] NULL
                    ) ON [PRIMARY]
                End
            ")

        clsG_DBmng.sql_Exec_Query(
            "
                If Not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'TB_보험약' and column_name = '단가')
                begin
                    alter table TB_보험약 add 단가 int
                End
            ")
    End Sub

    ''' <summary>
    '''  Form 로드되면 기준처방 리스트 조회
    ''' </summary>
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        clsG_DBmng.sql_connection_open()
        sD_CreateTable()

        ' 기준처방 리스트 조회
        '   Dim dtL_mediList As DataTable = clsG_DBmng.sql_Get_Datatable(
        '           $"
        'select distinct 기준코드, 명칭 as 기준코드명칭 ,기준코드제약사, 가감구분 from tb_h_보험처방 
        'where 숨김 = '0'
        'and 가감구분 <> '10'
        '           ")
        sD_sql_get_mixList()

    End Sub


    ''' <summary>
    ''' 현재 페이지 인덱스 저장하는 변수,  초기값 9
    ''' </summary>
    Private intD_pageIndex As Integer = 9


    ''' <summary>
    ''' 1. TapControl 클릭하면 <br/>
    ''' 2. tap 인덱스에 따라 쿼리 조회<br/> 
    ''' 3. 인덱스 저장<br/> 
    ''' 4. form value 초기화<br/> 
    ''' </summary>
    Private Sub tap_페이지_Click(sender As Object, e As EventArgs) Handles tap_페이지.Click

        If tap_페이지.SelectedIndex = 0 Then
            sD_sql_get_mixList()
            sD_Return_Clear()
            intD_pageIndex = tap_페이지.SelectedIndex
        ElseIf tap_페이지.SelectedIndex = 1 Then
            sD_sql_get_singleList()
            sD_single_Return_Clear()
            txt_code_sg.Text = ""
            txt_name_sg.Text = ""
            txt_comp_sg.Text = ""

            intD_pageIndex = tap_페이지.SelectedIndex
        ElseIf tap_페이지.SelectedIndex = 2 Then
            sD_sql_get_treatList()
            sD_inven_Return_Clear()
            txt_수입업소.Text = ""
            txt_code_inven.Text = ""
            txt_name_inven.Text = ""
            txt_comp_inven.Text = ""

            intD_pageIndex = tap_페이지.SelectedIndex
            sD_txtEnableFalse()
        End If

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
                where a.청구코드 = '' and a.가감구분 <> '10' and b.적용일자 = (Select max(적용일자) from TB_H_마스터혼합제  where  적용일자 NOT LIKE '%[^0-9]%' AND LEN(적용일자) = 8   and 적용일자 <= convert(date, getdate(), 13))
            ")

        grid_혼합제.DataSource = fD_formatData(dtL_data)
        grid_혼합제.Update()

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
                where a.청구코드 <> ''  and b.적용일자 = (Select MAX(적용일자) from TB_H_마스터단미제  where  코드 = a.청구코드 and 적용일자 <= convert(date, getdate(), 13))
            ")

        grid_단미제.DataSource = fD_formatData(dtL_data)
        grid_단미제.Update()
    End Sub


    ''' <summary>
    ''' 치료재료대 리스트 조회 쿼리 
    ''' </summary>
    Private Sub sD_sql_get_treatList()
        Dim dtL_data As DataTable = clsG_DBmng.sql_Get_Datatable(
            $"
                select 처방코드,한글명칭, b.제조회사 as 제약사, b.수입업소 from tb_h_처방코드 a
	            inner join TB_마스터재료 b
	            on a.처방코드 = b.코드
                where 코드구분 = 8 and b.적용일자 = (Select MAX(적용일자) from TB_마스터재료 where 코드 = a.처방코드 and 적용일자 <= convert(date, getdate(), 13))
            ")

        grid_치료재료대.DataSource = dtL_data
        grid_치료재료대.Update()
    End Sub


    ''' <summary>
    ''' 보험약 데이터 리스트의 특정 컬럼 값을 가공하는 함수<br/>
    ''' 1. 기준코드명칭에서 괄호 '(' 이후 문자열 제거<br/>
    ''' 2. 입고일자를 "yyyy-MM-dd HH:mm" 형식으로 변환<br/>
    ''' 3. 수량 컬럼이 숫자가 아니면 0으로 설정<br/>
    ''' 4. 수량이 소수점 없는 정수면 정수로 변환<br/>
    ''' </summary>
    ''' <param name="dtL_data"> 보험약 리스트, DataTable 타입 </param>
    ''' <returns>괄호 제거한 DataTable </returns>
    Private Function fD_formatData(ByVal dtL_data As DataTable, Optional inven As String = "")

        If inven = "" Then
            For Each row As DataRow In dtL_data.Rows
                If row("기준코드명칭") IsNot Nothing Then
                    Dim strL_split_name As String() = row("기준코드명칭").ToString.Split("(")
                    row("기준코드명칭") = strL_split_name(0)
                End If
            Next
        Else
            For Each row As DataRow In dtL_data.Rows

                If row("입고일자") IsNot Nothing Then
                    Dim strL_date = row("입고일자").ToString()
                    row("입고일자") = CDate(strL_date).ToString("yyyy-MM-dd HH:mm") 'strL_date.Substring(0, strL_date.Length - 3)
                End If

                If Not IsNumeric(row("수량")?.ToString) Then
                    row("수량") = 0
                End If

                Dim dblL_수량 As Double = row("수량")?.ToString

                'mod 나머지구하기 dblL_수량 mod 1 
                ' \ 몫구하기 dblL_수량 \ 1 
                If dblL_수량 Mod 1 = 0 Then
                    row("수량") = Math.Truncate(dblL_수량)
                End If

                'Dim strL_split As String() = row("수량")?.ToString.Split(".")

                'If strL_split(1) = "00" Then
                '    row("수량") = Convert.ToInt32(row("수량"))
                'End If

                If row("명칭") IsNot Nothing Then
                    If row("명칭").ToString.Contains("(") Then
                        Dim strL_split_name As String() = row("명칭").ToString.Split("(")
                        row("명칭") = strL_split_name(0)
                    End If
                End If
            Next
        End If

        Return dtL_data

    End Function


    ''' <summary>
    ''' 보험약 재고내역 조회 
    ''' </summary>
    ''' <param name="strL_Code">코드 </param>
    ''' <returns>보험약재고 리스트 DataTable</returns>
    Private Function fD_Get_invenList(ByVal strL_Code As String)

        Dim dtL_infoList As DataTable = clsG_DBmng.sql_Get_Datatable(
        $"
            select 처방코드 as 기준코드, 명칭 as 기준코드명칭, 입고일자,  수량, 단가, idx as 인덱스 from TB_보험약
            where 처방코드 = '{strL_Code}'
        ")
        Return dtL_infoList
    End Function


    ''' <summary>
    ''' 1. 페이지 인덱스에 따라 재고리스트 조회 <br/>
    ''' 2. grid에 load
    ''' </summary>
    ''' <param name="strL_Code">처방코드</param>
    Private Sub sD_load_invenList(ByVal strL_Code As String)

        If intD_pageIndex = 0 Or intD_pageIndex = 9 Then
            Dim dtL_infoList As DataTable = clsG_DBmng.sql_Get_Datatable(
                $"
                    select 처방코드 as 코드, 명칭, 입고일자,  수량, 단가, idx as 인덱스 from TB_보험약
                    where 처방코드 = '{strL_Code}'
                ")

            'grid_혼합제_inven.DataSource = Nothing
            'grid_혼합제_inven.Rows.Clear()


            grid_혼합제_inven.DataSource = fD_formatData(dtL_infoList, "inven")
            'If grid_혼합제_inven.Columns.Contains("인덱스") Then
            '    grid_혼합제_inven.Columns("인덱스").Visible = False
            'End If
            grid_혼합제_inven.Update()

            sD_txtEnableFalse()

        ElseIf intD_pageIndex = 1 Then

            Dim dtL_sgList As DataTable = clsG_DBmng.sql_Get_Datatable(
                    $"
                    select 처방코드 as 코드, 명칭 , 입고일자,  수량, 단가, idx as 인덱스 from TB_보험약
                    where 처방코드 = '{strL_Code}'
                ")

            'grid_단미제_inven.DataSource = Nothing
            'grid_단미제_inven.Rows.Clear()

            grid_단미제_inven.DataSource = fD_formatData(dtL_sgList, "inven")
            'grid_단미제_inven.Columns("인덱스").Visible = False
            grid_단미제_inven.Update()

            sD_txtEnableFalse()

        ElseIf intD_pageIndex = 2 Then

            Dim dtL_data As DataTable = clsG_DBmng.sql_Get_Datatable(
            $"
                Select a.idx as 인덱스, a.처방코드 as 코드, a.명칭, a.입고일자, a.수량, a.단가  from TB_치료재료대 a
                where a.처방코드 = '{strL_Code}'
            ")

            'grid_재료대재고내역.DataSource = Nothing
            'grid_재료대재고내역.Rows.Clear()

            grid_재료대재고내역.DataSource = fD_formatData(dtL_data, "inven")
            'grid_재료대재고내역.Columns("인덱스").Visible = False
            grid_재료대재고내역.Update()

            sD_txtEnableFalse()

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

    End Sub

    ''' <summary>
    ''' 보험약재고 항목 클릭 이벤트
    ''' </summary>
    Private Sub grid_보험약_CellContentClick(sender As Object, e As EventArgs) Handles grid_혼합제_inven.CellClick

        If grid_혼합제_inven.CurrentRow Is Nothing Then
            sD_btnFalse()
            Exit Sub
        Else
            sD_btnTrue()
            'btn_Save.Visible = False
            dtp_Received.Value = Convert.ToDateTime(grid_혼합제_inven.CurrentRow.Cells(en_보험약재고내역_col.입고일자).Value)
            txt_count.Text = grid_혼합제_inven.CurrentRow.Cells(en_보험약재고내역_col.수량).Value
            txt_Price.Text = grid_혼합제_inven.CurrentRow.Cells(en_보험약재고내역_col.단가).Value
            intD_index = grid_혼합제_inven.CurrentRow.Cells(en_보험약재고내역_col.idx).Value
        End If

    End Sub


    ''' <summary>
    ''' 보험약 새로입력 버튼 클릭 이벤트
    ''' </summary>
    Private Sub btn_New_Click(sender As Object, e As EventArgs) Handles btn_New.Click

        If grid_혼합제.CurrentRow Is Nothing Then
            MessageBox.Show("기준처방 항목을 먼저 선택해주세요.")
            Exit Sub
        End If

        sD_txtEnableTrue()

        sD_Return_Clear()

        btn_Save.Visible = True
        btn_delete.Visible = False
        btn_Update.Visible = False

    End Sub


    ''' <summary>
    ''' 보험약 저장 버튼 클릭 이벤트
    ''' </summary>
    Private Sub btn_Save_Click(sender As Object, e As EventArgs) Handles btn_Save.Click

        If grid_혼합제.CurrentRow Is Nothing Then
            MessageBox.Show("기준처방 항목을 선택해주세요.")
            Exit Sub
        End If

        If txt_count.Text = "" Or txt_Price.Text = "" Then
            MessageBox.Show("항목을 입력해주세요.")
            Exit Sub
        End If
        Dim strL_code = grid_혼합제.CurrentRow.Cells(en_보험약_Col.코드).Value?.ToString
        Dim strL_name = grid_혼합제.CurrentRow.Cells(en_보험약_Col.명칭).Value?.ToString
        Dim strL_comp = grid_혼합제.CurrentRow.Cells(en_보험약_Col.제약사).Value?.ToString
        Dim strL_date As String = dtp_Received.Value.ToString("yyyy-MM-dd HH:mm:ss")
        Dim strL_count = txt_count.Text
        Dim int_price As Integer = Convert.ToInt32(txt_Price.Text)

        If intD_index > 0 Then
            clsG_DBmng.sql_Exec_Query(
                $"
                    Update tb_보험약 set 입고일자 = '{strL_date}', 수량 = '{strL_count}', 단가 = '{int_price}' 
                    where idx = '{intD_index}'
                ")
        Else
            intD_index = clsG_DBmng.sql_Exec_Query_returnindex(
             $"
                insert TB_보험약(처방코드, 명칭, 제약사, 입고일자,수량, 단가)
                values('{strL_code}', '{strL_name}', '{strL_comp}', '{strL_date}', '{strL_count}', '{int_price}');
                SELECT @@IDENTITY
             ")


        End If

        sD_load_invenList(strL_code)

    End Sub


    ''' <summary>
    ''' 보험약 수정 버튼 클릭 이벤트
    ''' </summary>
    Private Sub btn_Update_Click(sender As Object, e As EventArgs) Handles btn_Update.Click

        If grid_혼합제_inven.CurrentRow Is Nothing Then Exit Sub

        sD_txtEnableTrue()

        sD_btnFalse()

    End Sub


    ''' <summary>
    ''' 보험약 삭제 버튼 클릭 이벤트
    ''' </summary>
    Private Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click

        clsG_DBmng.sql_Exec_Query(
            $"
                delete  From tb_보험약 Where idx = '{intD_index}'
            ")

        sD_Return_Clear()

        sD_load_invenList(grid_혼합제.CurrentRow.Cells(en_보험약_Col.코드).Value?.ToString)

    End Sub

    '======================================================================================================== >> 단미제


    ''' <summary>
    ''' 현재 단미제 인덱스
    ''' </summary>
    Private intD_index_sg As Integer = 0

    ''' <summary>
    ''' 단미제 폼 value 초기화
    ''' </summary>
    Private Sub sD_single_Return_Clear()
        intD_index_sg = 0
        dtp_received_sg.Value = Now
        txt_count_sg.Text = ""
        txt_price_sg.Text = ""
    End Sub


    ''' <summary>
    ''' 단미제 항목 클릭 이벤트
    ''' </summary>
    Private Sub grid_단미제_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grid_단미제.CellClick, grid_단미제.CellContentClick

        '현재 단미제 컬럼 가지고와서 있는지 확인하고 조회 
        If grid_단미제.CurrentRow Is Nothing Then Exit Sub

        sD_single_Return_Clear()
        txt_code_sg.Text = ""
        txt_name_sg.Text = ""
        txt_comp_sg.Text = ""

        txt_code_sg.Text = grid_단미제.CurrentRow.Cells(en_보험약_Col.코드).Value?.ToString
        txt_name_sg.Text = grid_단미제.CurrentRow.Cells(en_보험약_Col.명칭).Value?.ToString
        txt_comp_sg.Text = grid_단미제.CurrentRow.Cells(en_보험약_Col.제약사).Value?.ToString

        sD_load_invenList(txt_code_sg.Text)

        sD_btnFalse()

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
            intD_index_sg = grid_단미제_inven.CurrentRow.Cells(en_보험약재고내역_col.idx).Value
        End If

    End Sub


    ''' <summary>
    ''' 단미제 새로 입력 클릭 
    ''' </summary>
    Private Sub btn_new_sg_Click(sender As Object, e As EventArgs) Handles btn_new_sg.Click

        If grid_단미제.CurrentRow Is Nothing Then
            MessageBox.Show("기준처방 항목을 먼저 선택해주세요.")
            Exit Sub
        End If

        sD_txtEnableTrue()

        btn_save_sg.Visible = True
        sD_btnFalse()

        sD_single_Return_Clear()

    End Sub


    ''' <summary>
    ''' 단미제 수정 클릭
    ''' </summary>
    Private Sub btn_update_sg_Click(sender As Object, e As EventArgs) Handles btn_update_sg.Click

        If grid_단미제.CurrentRow Is Nothing Then Exit Sub

        sD_txtEnableTrue()

        sD_btnFalse()

    End Sub


    ''' <summary>
    ''' 단미제 저장 클릭
    ''' </summary>
    Private Sub btn_save_sg_Click(sender As Object, e As EventArgs) Handles btn_save_sg.Click

        If grid_단미제.CurrentRow Is Nothing Then
            MessageBox.Show("기준처방 항목을 선택해주세요.")
            Exit Sub
        End If

        If txt_count_sg.Text = "" Or txt_price_sg.Text = "" Then
            MessageBox.Show("항목을 입력해주세요.")
            Exit Sub
        End If
        Dim strL_code = grid_단미제.CurrentRow.Cells(en_보험약_Col.코드).Value?.ToString
        Dim strL_name = grid_단미제.CurrentRow.Cells(en_보험약_Col.명칭).Value?.ToString
        Dim strL_comp = grid_단미제.CurrentRow.Cells(en_보험약_Col.제약사).Value?.ToString
        Dim strL_date As String = dtp_received_sg.Value.ToString("yyyy-MM-dd HH:mm:ss")
        Dim strL_count = txt_count_sg.Text
        Dim int_price As Integer = Convert.ToInt32(txt_price_sg.Text)

        If intD_index_sg > 0 Then
            clsG_DBmng.sql_Exec_Query(
                $"
                    Update tb_보험약 set 입고일자 = '{strL_date}', 수량 = '{strL_count}', 단가 = '{int_price}' 
                    where idx = '{intD_index_sg}'
                ")
        Else
            intD_index_sg = clsG_DBmng.sql_Exec_Query_returnindex(
             $"
                insert TB_보험약(처방코드, 명칭, 제약사, 입고일자,수량, 단가)
                values('{strL_code}', '{strL_name}', '{strL_comp}', '{strL_date}', '{strL_count}', '{int_price}');
                SELECT @@IDENTITY
             ")
        End If

        sD_load_invenList(strL_code)

    End Sub


    ''' <summary>
    ''' 단미제 삭제 클릭 이벤트
    ''' </summary>
    Private Sub btn_del_sg_Click(sender As Object, e As EventArgs) Handles btn_del_sg.Click

        clsG_DBmng.sql_Exec_Query(
            $"
                delete  From tb_보험약 Where idx = '{intD_index_sg}'
            ")

        sD_Return_Clear()

        sD_load_invenList(grid_단미제_inven.CurrentRow.Cells(en_보험약재고내역_col.코드).Value?.ToString)

    End Sub

    '======================================================================================================== >> 치료재료대
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
    ''' 현재 치료재료대 인덱스
    ''' </summary>
    Private intD_index_in As Integer = 0

    ''' <summary>
    ''' 재료대재고 폼 value 초기화
    ''' </summary>
    Private Sub sD_inven_Return_Clear()
        intD_index_in = 0
        dtp_inven.Value = Now
        txt_count_inven.Text = ""
        txt_price_inven.Text = ""
    End Sub

    'Private Function GetGrid_재료대재고내역() As DataGridView
    '    Return grid_재료대재고내역
    'End Function


    ''' <summary>
    ''' 치료재료대 내역 클릭
    ''' </summary>
    Private Sub grid_치료재료대_CellClick(sender As Object, e As EventArgs) Handles grid_치료재료대.CellClick

        If grid_치료재료대.CurrentRow Is Nothing Then Exit Sub

        sD_inven_Return_Clear()
        txt_code_inven.Text = ""
        txt_name_inven.Text = ""
        txt_comp_inven.Text = ""

        txt_code_inven.Text = grid_치료재료대.CurrentRow.Cells(en_치료재료대_Col.코드).Value?.ToString
        txt_name_inven.Text = grid_치료재료대.CurrentRow.Cells(en_치료재료대_Col.명칭).Value?.ToString
        txt_comp_inven.Text = grid_치료재료대.CurrentRow.Cells(en_치료재료대_Col.제조사).Value?.ToString
        txt_수입업소.Text = grid_치료재료대.CurrentRow.Cells(en_치료재료대_Col.수입업소).Value?.ToString

        sD_load_invenList(txt_code_inven.Text)

        sD_txtEnableFalse()

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
            intD_index_in = grid_재료대재고내역.CurrentRow.Cells(en_치료재료대재고_Col.인덱스).Value
        End If

    End Sub
    '

    ''' <summary>
    ''' 저장버튼 클릭
    ''' </summary>
    Private Sub btn_save_inven_Click(sender As Object, e As EventArgs) Handles btn_save_inven.Click

        If txt_count_inven.Text = "" Or txt_price_inven.Text = "" Then
            MessageBox.Show("항목을 입력해주세요.")
            Exit Sub
        End If

        Dim strL_code = grid_치료재료대.CurrentRow.Cells(en_치료재료대_Col.코드).Value?.ToString
        Dim strL_name = grid_치료재료대.CurrentRow.Cells(en_치료재료대_Col.명칭).Value?.ToString
        Dim strL_date As String = dtp_inven.Value.ToString("yyyy-MM-dd HH:mm:ss")
        Dim strL_count = txt_count_inven.Text
        Dim int_price As Integer = Convert.ToInt32(txt_price_inven.Text)

        If intD_index_in > 0 Then
            clsG_DBmng.sql_Exec_Query(
                $"
                    Update tb_치료재료대 set 입고일자 = '{strL_date}', 수량 = '{strL_count}', 단가 = '{int_price}' 
                    where idx = '{intD_index_in}' 
                ")
        Else
            intD_index_in = clsG_DBmng.sql_Exec_Query_returnindex(
             $"
                insert tb_치료재료대(처방코드, 명칭, 입고일자,수량, 단가)
                values('{strL_code}', '{strL_name}', '{strL_date}', '{strL_count}', '{int_price}');
                SELECT @@IDENTITY
             ")

        End If

        sD_load_invenList(strL_code)

    End Sub


    ''' <summary>
    ''' 수정버튼 클릭
    ''' </summary>
    Private Sub btn_update_inven_Click(sender As Object, e As EventArgs) Handles btn_update_inven.Click

        If grid_재료대재고내역.CurrentRow Is Nothing Then Exit Sub

        sD_txtEnableTrue()

        sD_btnFalse()

    End Sub


    ''' <summary>
    ''' 새로 입력 클릭 
    ''' </summary>
    Private Sub btn_new_inven_Click(sender As Object, e As EventArgs) Handles btn_new_inven.Click

        If grid_치료재료대.CurrentRow Is Nothing Then
            MessageBox.Show("기준처방 항목을 먼저 선택해주세요.")
            Exit Sub
        End If

        sD_txtEnableTrue()

        btn_Save.Visible = True
        sD_btnFalse()

        sD_inven_Return_Clear()

    End Sub

    Private Sub btn_del_inven_Click(sender As Object, e As EventArgs) Handles btn_del_inven.Click

        If grid_재료대재고내역.CurrentRow Is Nothing Then Exit Sub

        clsG_DBmng.sql_Exec_Query(
            $"
                delete  From tb_치료재료대 Where idx = '{intD_index_in}'
            ")

        sD_inven_Return_Clear()

        sD_load_invenList(grid_재료대재고내역.CurrentRow.Cells(en_치료재료대재고_Col.처방코드).Value?.ToString)

    End Sub


End Class
