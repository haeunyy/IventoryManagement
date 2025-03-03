Public Class Form1

    Private Enum en_기준처방_Col
        코드 = 1
        명칭
        제약사
    End Enum

    Private Enum en_재고내역_col
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

    Private Sub sD_btnTrue()
        btn_Save.Visible = True
        btn_Update.Visible = True
        btn_delete.Visible = True
    End Sub

    Private Sub sD_btnFalse()
        btn_Update.Visible = False
        btn_delete.Visible = False
    End Sub

    Private Sub sD_txtEnableTrue()
        dtp_Received.Enabled = True
        txt_count.Enabled = True
        txt_Price.Enabled = True
    End Sub
    Private Sub sD_txtEnableFalse()
        txt_Code.Enabled = False
        txt_Name.Enabled = False
        txt_mediCompany.Enabled = False
        dtp_Received.Enabled = False
        txt_count.Enabled = False
        txt_Price.Enabled = False
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


        ' [가감구분]으로 기준처방(0,20)/단미제(1,2,11) 구분하여 코드,명칭,제약사 조회  00:00:00.047
        Dim dtL_mediList As DataTable = clsG_DBmng.sql_Get_Datatable(
            $"
                SELECT DISTINCT 

                    -- 단미제인 경우 청구코드를 코드항목으로 대입
                    CASE 
                        WHEN a.가감구분 IN ('1','2','11') AND a.청구코드 IS NOT NULL THEN a.청구코드 
                        ELSE a.기준코드 
                    END 기준코드, 

                    -- 기준처방/단미제 구분에 따라 각 테이블에서 명칭 조회 
	                CASE 
		                WHEN a.가감구분 IN ('0','20') then (select top 1 b.명칭 from TB_H_마스터혼합제 b where a.기준코드 = b.코드)
		                ELSE (select top 1 c.명칭 from TB_H_마스터단미제 c where c.코드 = a.청구코드) 
                    END 기준코드명칭,

                    -- 기준처방/단미제 구분에 따라 각 테이블에서 제약사 조회 
	                CASE 
		                WHEN a.가감구분 IN ('0','20') then (select top 1 b.업소 from TB_H_마스터혼합제 b where a.기준코드 = b.코드)
		                ELSE  (select top 1 c.업소 from TB_H_마스터단미제 c where c.코드 = a.청구코드)
	                END 기준코드제약사
                FROM tb_h_보험처방 a
                WHERE a.숨김 = '0'
                AND a.가감구분 <> '10';
            ")
        '    Select  DISTINCT
        '    -- 단미제인 경우 청구코드를 코드항목으로 대입
        '    Case 
        '        WHEN a.가감구분 IN ('1', '2', '11') AND a.청구코드 IS NOT NULL THEN a.청구코드 
        '        Else a.기준코드 
        '    End As 기준코드, 

        '    -- 기준처방(0,20) → 마스터혼합제에서 조회 / 단미제(1,2,11) → 마스터단미제에서 조회
        '    COALESCE(b.명칭, c.명칭) As 기준코드명칭,

        '    -- 기준처방(0,20) → 마스터혼합제에서 조회 / 단미제(1,2,11) → 마스터단미제에서 조회
        '    COALESCE(b.업소, c.업소) As 기준코드제약사

        'From tb_h_보험처방 a
        'Left Join TB_H_마스터혼합제 b 
        '    On a.기준코드 = b.코드
        '    And a.가감구분 IN ('0', '20')  -- 기준처방인 경우만 JOIN

        'Left Join TB_H_마스터단미제 c 
        '    On c.코드 = a.청구코드
        '    And a.가감구분 IN ('1', '2', '11')  -- 단미제인 경우만 JOIN

        'WHERE a.숨김 = '0'
        'And a.가감구분 <> '10';00:00:00.045join

        For Each row As DataRow In dtL_mediList.Rows

            If row("기준코드명칭") IsNot Nothing Then
                Dim strL_split_name As String() = row("기준코드명칭").ToString.Split("(")
                row("기준코드명칭") = strL_split_name(0)
            End If

        Next

        grid_기준처방.DataSource = dtL_mediList
        grid_기준처방.Update()

    End Sub


    ''' <summary>
    '''  보험약 조회 프로시저
    ''' </summary>
    ''' <param name="strL_Code">조회할 보험약의 코드</param>
    Private Sub sD_Get_infoList(ByVal strL_Code)
        Dim dtL_infoList As DataTable = clsG_DBmng.sql_Get_Datatable(
        $"
            select 처방코드 as 기준코드, 명칭 as 기준코드명칭, 입고일자,  수량, 단가, idx as 인덱스 from TB_보험약
            where 처방코드 = '{strL_Code}'
        ")

        If Not dtL_infoList.Columns.Contains("입고일자_변환") Then
            dtL_infoList.Columns.Add("입고일자_변환", GetType(String))
        End If

        For Each row As DataRow In dtL_infoList.Rows

            If row("입고일자") IsNot Nothing Then
                Dim strL_date = row("입고일자").ToString()
                row("입고일자_변환") = CDate(strL_date).ToString("yyyy-MM-dd HH:mm") 'strL_date.Substring(0, strL_date.Length - 3)
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

            If row("기준코드명칭") IsNot Nothing Then
                If row("기준코드명칭").ToString.Contains("(") Then
                    Dim strL_split_name As String() = row("기준코드명칭").ToString.Split("(")
                    row("기준코드명칭") = strL_split_name(0)
                End If
            End If
        Next

        grid_보험약.DataSource = dtL_infoList
        grid_보험약.Columns("입고일자").Visible = False
        grid_보험약.Update()

        sD_txtEnableFalse()

    End Sub


    ''' <summary>
    ''' 기준처방 항목 클릭 이벤트
    ''' </summary>
    Private Sub grid_Medi_CellContentClick(sender As Object, e As EventArgs) Handles grid_기준처방.CellClick

        If grid_기준처방.CurrentRow Is Nothing Then Exit Sub

        sD_Return_Clear()
        txt_Code.Text = ""
        txt_Name.Text = ""
        txt_mediCompany.Text = ""

        txt_Code.Text = grid_기준처방.CurrentRow.Cells(en_기준처방_Col.코드).Value?.ToString
        txt_Name.Text = grid_기준처방.CurrentRow.Cells(en_기준처방_Col.명칭).Value?.ToString
        txt_mediCompany.Text = grid_기준처방.CurrentRow.Cells(en_기준처방_Col.제약사).Value?.ToString

        sD_Get_infoList(txt_Code.Text)

        sD_btnFalse()

    End Sub

    ''' <summary>
    ''' 보험약재고 항목 클릭 이벤트
    ''' </summary>
    Private Sub grid_보험약_CellContentClick(sender As Object, e As EventArgs) Handles grid_보험약.CellClick

        If grid_보험약.CurrentRow Is Nothing Then
            sD_btnFalse()
            Exit Sub
        Else
            sD_btnTrue()
            'btn_Save.Visible = False
            dtp_Received.Value = Convert.ToDateTime(grid_보험약.CurrentRow.Cells(en_재고내역_col.입고일자).Value)
            txt_count.Text = grid_보험약.CurrentRow.Cells(en_재고내역_col.수량).Value
            txt_Price.Text = grid_보험약.CurrentRow.Cells(en_재고내역_col.단가).Value
            intD_index = grid_보험약.CurrentRow.Cells(en_재고내역_col.idx).Value
        End If

    End Sub

    ''' <summary>
    ''' 보험약 수정 버튼 클릭 이벤트
    ''' </summary>
    Private Sub btn_Update_Click(sender As Object, e As EventArgs) Handles btn_Update.Click

        If grid_보험약.CurrentRow Is Nothing Then Exit Sub

        sD_txtEnableTrue()

        sD_btnFalse()

    End Sub


    ''' <summary>
    ''' 보험약 저장 버튼 클릭 이벤트
    ''' </summary>
    Private Sub btn_Save_Click(sender As Object, e As EventArgs) Handles btn_Save.Click

        If grid_기준처방.CurrentRow Is Nothing Then
            MessageBox.Show("기준처방 항목을 선택해주세요.")
            Exit Sub
        End If

        If txt_count.Text = "" Or txt_Price.Text = "" Then
            MessageBox.Show("항목을 입력해주세요.")
            Exit Sub
        End If
        Dim strL_code = grid_기준처방.CurrentRow.Cells(en_기준처방_Col.코드).Value?.ToString
        Dim strL_name = grid_기준처방.CurrentRow.Cells(en_기준처방_Col.명칭).Value?.ToString
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
                insert TB_보험약(처방코드, 명칭, 입고일자,수량, 단가)
                values('{strL_code}', '{strL_name}', '{strL_date}', '{strL_count}', '{int_price}');
                SELECT @@IDENTITY
             ")


        End If

        sD_Get_infoList(strL_code)

    End Sub


    ''' <summary>
    ''' 보험약 새로입력 버튼 클릭 이벤트
    ''' </summary>
    Private Sub btn_New_Click(sender As Object, e As EventArgs) Handles btn_New.Click

        If grid_기준처방.CurrentRow Is Nothing Then
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
    ''' 보험약 삭제 버튼 클릭 이벤트
    ''' </summary>
    Private Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click

        clsG_DBmng.sql_Exec_Query(
            $"
                delete  From tb_보험약 Where idx = '{intD_index}'
            ")

        sD_Return_Clear()

        sD_Get_infoList(grid_기준처방.CurrentRow.Cells(en_기준처방_Col.코드).Value?.ToString)

    End Sub




    ''' <summary>
    '''   치료재료대 
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

    ''' <summary>
    ''' 재료대 수정, 저장, 삭제 버튼 활성화
    ''' </summary>
    Private Sub sD_btnTrue_in()
        btn_save_inven.Visible = True
        btn_update_inven.Visible = True
        btn_del_inven.Visible = True
    End Sub

    ''' <summary>
    ''' 재료대 수정, 삭제 버튼 비활성화
    ''' </summary>
    Private Sub sD_btnFalse_in()
        btn_update_inven.Visible = False
        btn_del_inven.Visible = False
    End Sub

    ''' <summary>
    ''' 입고일자, 수량, 단가 항목 수정 활성화
    ''' </summary>
    Private Sub sD_txtEnableTrue_in()
        dtp_inven.Enabled = True
        txt_count_inven.Enabled = True
        txt_price_inven.Enabled = True
    End Sub

    ''' <summary>
    ''' 재고항목 전체 수정 비활성화 <br/>
    ''' txt_code_inven, txt_name_inven, txt_comp_inven, txt_수입업소, dtp_inven, txt_count_inven, txt_price_inven
    ''' </summary>
    Private Sub sD_txtEnableFalse_in()
        txt_code_inven.Enabled = False
        txt_name_inven.Enabled = False
        txt_comp_inven.Enabled = False
        txt_수입업소.Enabled = False
        dtp_inven.Enabled = False
        txt_count_inven.Enabled = False
        txt_price_inven.Enabled = False
    End Sub


    Private Function GetGrid_재료대재고내역() As DataGridView
        Return grid_재료대재고내역
    End Function


    ''' <summary>
    ''' 치료재료대 탭 클릭 
    ''' </summary>
    Private Sub tap_치료재료대_Click(sender As Object, e As EventArgs) Handles tap_치료재료대.Click
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

        sD_get_inven(txt_code_inven.Text)
        sD_txtEnableFalse_in()
    End Sub


    ''' <summary>
    ''' 치료재료대 재고 조회
    ''' </summary>
    ''' <param name="vCode">처방코드</param>
    Private Sub sD_get_inven(ByVal vCode As String)

        Dim dtL_data As DataTable = clsG_DBmng.sql_Get_Datatable(
            $"
                Select a.idx as 인덱스, a.처방코드, a.명칭, a.입고일자, a.수량, a.단가  from TB_치료재료대 a
                where a.처방코드 = '{vCode}'
            ")

        grid_재료대재고내역.DataSource = dtL_data
        grid_재료대재고내역.Update()

    End Sub



    ''' <summary>
    ''' 치료재료대 재고내역 클릭
    ''' </summary>
    Private Sub grid_재료대재고내역_CellContentClick(sender As Object, e As EventArgs) Handles grid_재료대재고내역.CellClick
        If grid_재료대재고내역.CurrentRow Is Nothing Then
            sD_btnFalse_in()
            Exit Sub
        Else
            sD_btnTrue_in()
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

        sD_get_inven(strL_code)

    End Sub


    ''' <summary>
    ''' 수정버튼 클릭
    ''' </summary>
    Private Sub btn_update_inven_Click(sender As Object, e As EventArgs) Handles btn_update_inven.Click
        If grid_치료재료대.CurrentRow Is Nothing Then Exit Sub

        sD_txtEnableTrue_in()

        sD_btnFalse_in()
    End Sub


    ''' <summary>
    ''' 새로 입력 클릭 
    ''' </summary>
    Private Sub btn_new_inven_Click(sender As Object, e As EventArgs) Handles btn_new_inven.Click

        If grid_기준처방.CurrentRow Is Nothing Then
            MessageBox.Show("기준처방 항목을 먼저 선택해주세요.")
            Exit Sub
        End If

        sD_txtEnableTrue_in()

        btn_Save.Visible = True
        sD_btnFalse_in()

        sD_inven_Return_Clear()

    End Sub
End Class
