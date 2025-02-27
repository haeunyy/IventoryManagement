Public Class Form1

    Private Enum en_기준처방_Col
        코드 = 0
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
    End Enum



    ' 수정버튼 활성화에 따라 저장버튼 insert / update 전환
    Private sD_update_YN As Boolean = False

    Private intD_index As Integer = 0


    'Private dtL_data As DataTable
    'Public Sub New(ByVal v보험약)

    '    ' 디자이너에서 이 호출이 필요합니다.
    '    InitializeComponent()

    '    dtL_data = v보험약
    '    ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하세요.

    'End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        clsG_DBmng.sql_connection_open()
        sD_CreateTable()
        'sD_Get_mediList()

        ' 수량을 조회해오지 않으면 명칭 cell이 nothing 이 됨 
        ' 셀 인덱스랑 안맞는데 무엇때문인지 모르겠음 ㅜ
        Dim dtL_mediList As DataTable = clsG_DBmng.sql_Get_Datatable(
$"
                    select distinct a.기준코드, a.기준코드명칭, a.기준코드제약사, b.수량
                    from tb_h_보험처방 as a
                    left join tb_보험약 as b
        			on a.기준코드 = b.처방코드
                    where a.가감구분 = '20'
                    and a.숨김 = '0'
                ")

        grid_기준처방.DataSource = dtL_mediList
        grid_기준처방.Update()

    End Sub
    Private Sub sD_CreateTable()
        clsG_DBmng.sql_Exec_Query(
            "
                if not exists(select *from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'TB_보험약')
                begin
                    CREATE TABLE [dbo].[TB_보험약](
	                    [idx] [int] IDENTITY(1,1) NOT NULL,
	                    [처방코드] [nvarchar](20) NULL,
	                    [명칭] [varchar](100) NULL,	
	                    [제약사] [varchar](100) NULL,
	                    [입고일자] [varchar](100) NULL,
	                    [수량] [decimal](10, 2) NULL
                    ) ON [PRIMARY]
                end
            ")

        clsG_DBmng.sql_Exec_Query(
            "
                if not exists(select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'TB_보험약' and column_name = '단가')
                begin
	                alter table TB_보험약 add 단가 int
                end
            ")
    End Sub


    Private Sub sD_Get_infoList(ByVal strL_Code)
        Dim dtL_infoList As DataTable = clsG_DBmng.sql_Get_Datatable(
$"
            select 처방코드 as 기준코드, 명칭 as 기준코드명칭, 입고일자,  수량, 단가, idx as 인덱스 from TB_보험약
            where 처방코드 = '{strL_Code}'
        ")

        For Each row As DataRow In dtL_infoList.Rows

            If row("입고일자") IsNot Nothing Then
                Dim strL_date = row("입고일자").ToString()
                row("입고일자") = strL_date.Substring(0, strL_date.Length - 3)
            End If

            Dim strL_split As String() = row("수량")?.ToString.Split(".")

            If strL_split(1) = "00" Then
                row("수량") = Convert.ToInt32(row("수량"))
            End If

            If row("기준코드명칭") IsNot Nothing Then
                Dim strL_split_name As String() = row("기준코드명칭").ToString.Split("(")
                row("기준코드명칭") = strL_split_name(0)
            End If
        Next

        grid_보험약.DataSource = dtL_infoList
        grid_보험약.Update()

    End Sub

    Private Sub sD_Get_mediList()

        Dim dtL_mediList As DataTable = clsG_DBmng.sql_Get_Datatable(
        $"
            select DISTINCT a.기준코드, a.기준코드명칭, a.기준코드제약사
            from tb_h_보험처방 as a
            left join tb_보험약 as b
			on a.기준코드 = b.처방코드
            where a.가감구분 = '20'
            and a.숨김 = '0'
        ")

        grid_기준처방.DataSource = dtL_mediList
        grid_기준처방.Update()

    End Sub

    Private Sub sD_Return_Clear()

        intD_index = 0
        dtp_Received.Value = Now
        txt_count.Text = ""
        txt_Price.Text = ""

        'Dim dtL_data As DataTable = grid_약재내역.DataSource

        'Dim lqL_data = (From data In dtL_data.AsEnumerable).CopyToDataTable

        'MessageBox.Show(lqL_data.Rows(0)("기준코드").ToString)
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

    ' 기준처방 셀을 눌렀을 때 
    Private Sub grid_Medi_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

        If grid_기준처방.CurrentRow Is Nothing Then Exit Sub

        sD_Return_Clear()
        txt_Code.Text = ""
        txt_Name.Text = ""
        txt_mediCompany.Text = ""

        txt_Code.Text = grid_기준처방.CurrentRow.Cells(en_기준처방_Col.코드).Value?.ToString
        txt_Name.Text = grid_기준처방.CurrentRow.Cells(en_기준처방_Col.제약사).Value?.ToString
        txt_mediCompany.Text = grid_기준처방.CurrentRow.Cells(en_기준처방_Col.제약사).Value?.ToString

        sD_Get_infoList(txt_Code.Text)

        'If grid_보험약.RowCount = 0 Then
        sD_btnFalse()
        'Else
        '    sD_btnTrue()
        'End If

    End Sub


    ' 수정 버튼 클릭
    ' 
    Private Sub btn_Update_Click(sender As Object, e As EventArgs) 

        If grid_기준처방.CurrentRow Is Nothing Then
            MessageBox.Show("기준처방 항목을 선택해주세요.")
            Exit Sub
        End If

        If txt_count.Text = "" Or txt_Code.Text = "" Then
            MessageBox.Show("항목을 모두 입력해주세요. ")
            Exit Sub
        End If

        If grid_보험약.CurrentRow Is Nothing Then Exit Sub

        Dim strL_code = grid_기준처방.CurrentRow.Cells(en_기준처방_Col.코드).Value?.ToString
        Dim strL_date As String = dtp_Received.Value.ToString("2025-02-26 09:23:23")
        'Dim strL_date As String = dtp_Received.Value.ToString("yyyy-MM-dd HH:mm:ss")
        Dim strL_count = txt_count.Text
        Dim int_price As Integer = Convert.ToInt32(txt_Price.Text)
        Dim int_idx As Integer = grid_보험약.CurrentRow.Cells(en_재고내역_col.idx).Value

        Dim dtL_data As DataTable = clsG_DBmng.sql_Get_Datatable(
            $"
                select COUNT(처방코드) as 카운트 from TB_보험약 where 처방코드 = '{strL_code}' 
            ")

        If dtL_data.Rows.Count > 0 Then
            If dtL_data.Rows.Count > 0 Or dtL_data(0)("카운트") > 0 Then
                clsG_DBmng.sql_Exec_Query(
                    $"
                        Update tb_보험약 set 입고일자 = '{strL_date}', 수량 = '{strL_count}', 단가 = '{int_price}' 
                        where idx = '{int_idx}' and 처방코드 = '{strL_code}'
                    ")
            End If
        End If

        sD_Return_Clear()

        sD_Get_infoList(strL_code)
        'grid_보험약.DataSource = dtL_data.AsEnumerable.Where(Function(x) x("처방코드").ToString = strL_code).CopyToDataTable

    End Sub


    ' 저장버튼 클릭
    Private Sub btn_Save_Click(sender As Object, e As EventArgs) 

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
                    where idx = '{intD_index}' and 처방코드 = '{strL_code}'
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

    ' 새로입력 버튼 클릭
    Private Sub btn_New_Click(sender As Object, e As EventArgs) 

        If grid_기준처방.CurrentRow Is Nothing Then
            MessageBox.Show("기준처방 항목을 먼저 선택해주세요.")
            Exit Sub
        End If

        sD_Return_Clear()

        btn_Save.Visible = True
        btn_delete.Visible = False
        btn_Update.Visible = False

    End Sub

    ' 보험약 셀을 눌렀을 때 새로입력, 수정, 삭제 버튼만 노출
    ' 셀의 값이  txt 박스에 들어가도록 

    Private Sub grid_보험약_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) 

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

    Private Sub btn_delete_Click(sender As Object, e As EventArgs) 
        MessageBox.Show("")
        If grid_기준처방.CurrentRow Is Nothing Then
            MessageBox.Show("기준처방 항목을 선택해주세요.")
            Exit Sub
        End If

        If grid_보험약.CurrentRow Is Nothing Then Exit Sub

        Dim strL_code = grid_기준처방.CurrentRow.Cells(en_기준처방_Col.코드).Value?.ToString
        Dim int_idx As Integer = grid_보험약.CurrentRow.Cells(en_재고내역_col.idx).Value

        clsG_DBmng.sql_Exec_Query(
            $"
                delete  From tb_보험약 Where 처방코드 = '{strL_code}' And idx = '{int_idx}'
            ")

        sD_Return_Clear()

        sD_Get_infoList(strL_code)

        'grid_보험약.DataSource = dtL_data.AsEnumerable.Where(Function(x) x("처방코드").ToString = strL_code).CopyToDataTable

    End Sub


End Class
