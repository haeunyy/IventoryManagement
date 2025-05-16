Imports System.Diagnostics.Eventing
Imports System.Diagnostics.Eventing.Reader
Imports System.Drawing.Text
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class clsApi

    Private strD_endPoint As String = ""
    Private ServerIP As String = "192.168.0.79"
    Private Port As Integer = 9999

    Public Sub GetDrugDataAsync()

        Dim strL_filePath As String = "C:\HanimacData\"
        Dim strL_fileName As String = "versionIndex.json"

        Dim versionFiles = Directory.GetFiles(strL_filePath, strL_fileName)  ' 버전정보가 담겨있는 JSON 파일 목록 가져오기

        If versionFiles.Length <> 0 Then
            ' JSON 파일을 읽고 파싱하여 버전 정보 추출
            Dim obj_Json As JObject = JObject.Parse(File.ReadAllText(strL_filePath + strL_fileName))
            strD_endPoint = "Version|" & obj_Json("version").ToString
        Else
            strD_endPoint = "Version|20010101"
        End If

        Dim clientSocket As New TcpClient

        clientSocket.Connect(ServerIP, Port)

        ' 서버 스트림을 얻고 엔드포인트 문자열 전송

        Dim reader As StreamReader = New StreamReader(clientSocket.GetStream, Encoding.UTF8)
        Dim 

        Dim serverStream As NetworkStream = clientSocket.GetStream
        Dim outStream As Byte() = System.Text.Encoding.Default.GetBytes(strD_endPoint)
        serverStream.Write(outStream, 0, outStream.Length)
        serverStream.Flush()

        Dim buffSize As Integer
        Dim inStream(1024) As Byte

        clientSocket.ReceiveBufferSize = 1024
        buffSize = clientSocket.ReceiveBufferSize
        Dim strL_ResData As String = Reader.ReadToEnd() : Reader.Close()
        serverStream.Read(inStream, 0, inStream.Length)
        Dim returndata As String = System.Text.Encoding.UTF8.GetString(inStream)
        Do Until returndata.Trim <> ""
            System.Windows.Forms.Application.DoEvents()
        Loop

        returndata = returndata.Replace(vbNullChar, "")

        If returndata.Length > 1 Then

            If returndata.Contains("|") Then

                Select Case returndata.Split("|")(0).ToUpper
                    Case "VERSION"
                        If returndata.Split("|")(1) <> "" Then
                            clientSocket = New TcpClient
                            clientSocket.Connect(ServerIP, Port)
                            serverStream = clientSocket.GetStream

                            outStream = System.Text.Encoding.Default.GetBytes("UPDATE|" & returndata.Split("|")(1))
                            serverStream.Write(outStream, 0, outStream.Length)
                            serverStream.Flush()

                            serverStream.Read(inStream, 0, inStream.Length)

                            returndata = System.Text.Encoding.Default.GetString(inStream)
                            Do Until returndata.Trim <> ""
                                System.Windows.Forms.Application.DoEvents()
                            Loop
                            'If returndata.Split("|")(0).ToUpper = "UPDATE" Then
                            '    MessageBox.Show(returndata.Split("|")(1))
                            'End If
                        End If
                    Case "UPDATE"
                        returndata.Split("|")(1)
                End Select
            End If
        Else
            Dim json As JObject = JObject.Parse(returnData?.ToString)

            File.WriteAllText(strL_filePath & "drug_" & Date.Today.ToString("yyyyMMdd") & ".json", json("data")?.ToString)

            Dim jsonFile As New JObject

            jsonFile("version") = json("version")
            jsonFile("fileName") = "drug_" & Date.Today.ToString("yyyyMMdd")

            File.WriteAllText(strL_filePath & strL_fileName, jsonFile?.ToString)
        End If

    End Sub

    Private Class DrugItem
        Public Property data As String
        Public Property totalCount
    End Class

    Private Async Sub Api_Call()

        Dim strL_baseUrl As String = "https://api.odcloud.kr/api"
        Dim strL_serviceKey As String = WebUtility.UrlEncode("vnmcN8SOE8d/50pMlqncKOD+4tiHW024CrK7fzIzeoUVyvaflZxcVMNxwYFN25hnbPuaLuq/C1SptJ8BfdE0wQ==")

        'https://api.odcloud.kr/api/15067462/v1/uddi:4c72e98f-1bf9-470f-bee0-f451d54cd871?page=1&perPage=10&serviceKey=vnmcN8SOE8d%2F50pMlqncKOD%2B4tiHW024CrK7fzIzeoUVyvaflZxcVMNxwYFN25hnbPuaLuq%2FC1SptJ8BfdE0wQ%3D%3D

        Using client As New HttpClient()

            Dim intL_Page As Integer = 1
            Dim intL_Row As Integer = 500
            Dim intL_LastPage As Integer = 2
            Dim Arr_jsonItems As New List(Of String)

            Try
                Dim jsonArray As New JArray()

                Do
                    Dim strL_Url As String = strL_baseUrl + strD_endPoint + $"?page={intL_Page}&perPage={intL_Row}&serviceKey={strL_serviceKey}"
                    Dim response As HttpResponseMessage = Await client.GetAsync(strL_Url)
                    response.EnsureSuccessStatusCode()

                    Dim responseBody As String = Await response.Content.ReadAsStringAsync()
                    Dim tmpJson As JObject = JObject.Parse(responseBody)

                    intL_LastPage = Math.Ceiling(CInt(tmpJson("totalCount")) / intL_Row)

                    Arr_jsonItems.Add(tmpJson("data").ToString)

                    Dim Arr_items As JArray = tmpJson("data")

                    For Each item As JObject In Arr_items
                        jsonArray.Add(item)
                    Next

                    intL_Page += 1

                Loop While intL_Page <= 3

                'MsgBox(intL_Page)

                'Dim sbL_txt As New StringBuilder

                'For Each item In Arr_jsonItems
                '    sbL_txt.Append(item)
                'Next

                Dim strL_filePath As String = "C:\HanimacData\"
                Dim strL_fileName As String = "drug_" & Date.Today.ToString("yyyyMMdd") & ".json"

                File.WriteAllText(strL_filePath & strL_fileName, jsonArray.ToString)

                Dim jsonFile As New JObject

                jsonFile("version") = strD_endPoint
                jsonFile("fileName") = strL_fileName

                File.WriteAllText(strL_filePath & "versionIndex.json", jsonFile.ToString())

            Catch ex As Exception
                MsgBox($"API 오류: {ex.Message}")
            End Try

        End Using
    End Sub


    ' API에서 최신 버전의 엔드포인트 정보를 가져오는 서브루틴
    Private Async Function Api_Version_Call() As Task

        Using client As New HttpClient

            Try
                ' API 문서 페이지의 JSON 구조로부터 엔드포인트 목록을 얻어옴
                Dim url As String = "https://infuser.odcloud.kr/oas/docs?namespace=15067462/v1"
                Dim response As HttpResponseMessage = Await client.GetAsync(url)
                response.EnsureSuccessStatusCode()

                Dim responseBody As String = Await response.Content.ReadAsStringAsync
                Dim json As JObject = JObject.Parse(responseBody)

                Dim paths As JObject = json("paths")
                Dim keys As New List(Of String)

                keys = paths.Properties().Select(Function(x) x.Name).ToList  ' paths 객체의 키(엔드포인트)들을 리스트로 가져옴

                strD_endPoint = keys.Last  ' 최신 키 저장

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using

    End Function
End Class


