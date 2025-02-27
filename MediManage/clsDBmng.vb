Imports System.Data.SqlClient

Public Class clsDBmng

    Private Declare Auto Function GetPrivateProfileString Lib "kernel32" (ByVal lpAppName As String,
    ByVal lpKeyName As String,
    ByVal lpDefault As String,
    ByVal lpReturnedString As String,
    ByVal nSize As Integer,
    ByVal lpFileName As String) As Integer

    Private Const conD_HanimacNewINI = "C:\HanimacNew\Bin\HanimacNew.ini" '한의맥# DB접속정보가 담겨있는 ini파일

    Private sql_connection As New SqlConnection() With {
                    .ConnectionString = "Server=" & WinAPI_ReadINI("Database Server", "server_name", conD_HanimacNewINI, "localhost") & "; 
                        Database=" & WinAPI_ReadINI("Database Server", "database_name", conD_HanimacNewINI, "hanimacNew") & "; 
                        User Id=" & WinAPI_ReadINI("Database Server", "server_id", conD_HanimacNewINI, "hanimac4xq") & ";     
                        Password=" & WinAPI_ReadINI("Database Server", "server_pwd", conD_HanimacNewINI, "magic4guard") & ";TrustServerCertificate=True     "}

    Private Function WinAPI_ReadINI(strR_session As String, strR_key As String, strR_filePath As String, Optional strR_default_value As String = "") As String
        WinAPI_ReadINI = ""
        Try
            Dim lngL_result As Long
            Dim strL_value As String

            strL_value = Space(1024)
            lngL_result = GetPrivateProfileString(strR_session, strR_key, strR_default_value, strL_value, 1024, strR_filePath)
            WinAPI_ReadINI = Strings.Left(strL_value, InStr(strL_value, Chr(0)) - 1)
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
    End Function

    Public Sub sql_connection_open()
        Try
            sql_connection.Open()
        Catch ex As Exception
            MsgBox("Error : sql Connection Error " & vbNewLine & "Description : " & ex.Message)
            sql_connection.Close()
        End Try

    End Sub
    Public Function sql_Get_Datatable(ByVal vQuery As String) As DataTable

        If sql_connection.State <> ConnectionState.Open Then sql_connection_open()

        Dim dtL_datatable As New DataTable

        Try
            Using daL_adapter As New SqlDataAdapter(vQuery, sql_connection)
                daL_adapter.Fill(dtL_datatable)
            End Using
        Catch ex As Exception
            MsgBox("Error : sql Command Error " & vbNewLine & "Description : " & ex.Message)
        End Try

        Return dtL_datatable

    End Function

    Public Sub sql_Exec_Query(ByVal vQuery As String)

        If sql_connection.State <> ConnectionState.Open Then sql_connection_open()

        Debug.Print(vQuery)
        Try
            Using sqlcmd As New SqlCommand(vQuery, sql_connection)
                sqlcmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MsgBox("Error : sql Command Error " & vbNewLine & "Description : " & ex.Message)
        End Try

    End Sub

    Public Function sql_Exec_Query_returnindex(ByVal vQuery As String) As Integer

        If sql_connection.State <> ConnectionState.Open Then sql_connection_open()

        Debug.Print(vQuery)
        Try
            Using sqlcmd As New SqlCommand(vQuery, sql_connection)
                Return sqlcmd.ExecuteScalar()
            End Using
        Catch ex As Exception
            MsgBox("Error : sql Command Error " & vbNewLine & "Description : " & ex.Message)
        End Try

    End Function
End Class
