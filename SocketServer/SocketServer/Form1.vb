Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Net.Sockets
Imports System.Runtime
Imports System.Runtime.CompilerServices
Imports System.Security.Policy
Imports System.Text
Imports System.Threading
Imports Newtonsoft.Json.Linq

Public Class Form1

    Private server As TcpListener
    Private serverThread As Thread
    Private isRunning As Boolean = True
    Private strD_Version As String = ""
    Dim jobD_sendData As New JObject

    Private Structure st_Version
        Public Version As String
        Public EndPoint As String
        Public Data As JArray
    End Structure
    Private lstD_Version As New List(Of st_Version)


    ' 1. 최초 실행시 저장되어있는 파일의 버전을 확인
    ' 2. 소켓 서버를 실행
    ' 3. 타이머 실행 (매일 오전 8시)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        sD_SaveVersionFormFile()

        server = New TcpListener(IPAddress.Any, 9999)

        serverThread = New Thread(AddressOf ListenForClients)
        serverThread.Start()

        UpdateLabel("서버 시작됨 (포트 9999)")

        btn.Enabled = True

        sD_timerForApi()

    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        isRunning = False
        server.Stop()
    End Sub

    Private Sub ListenForClients()

        server.Start()
        While isRunning
            Try
                Dim client As TcpClient = server.AcceptTcpClient()

                clientsocket = client
                Using networkStream As NetworkStream = clientsocket.GetStream()

                    clientsocket.ReceiveBufferSize = 1024

                    Dim bytesFrom(1024) As Byte
                    Dim buffsize As Integer
                    Dim dataFromClient As String

                    buffsize = clientsocket.ReceiveBufferSize

                    networkStream.Read(bytesFrom, 0, buffsize)

                    dataFromClient = System.Text.Encoding.Default.GetString(bytesFrom)
                    'MsgBox(dataFromClient)

                    'UpdateLabel("이거받아따: " & dataFromClient)

                    Dim RequestData As String = dataFromClient.ToUpper.Replace(vbNullChar, "")

                    If RequestData.Contains("|") Then

                        Select Case RequestData.Split("|")(0)
                            Case "VERSION"
                                If RequestData.Split("|")(1) = lstD_Version(0).Version.ToUpper Then
                                    Dim bytL_sendData = System.Text.Encoding.UTF8.GetBytes("Version|")
                                    networkStream.Write(System.Text.Encoding.Default.GetBytes("0"), 0, System.Text.Encoding.Default.GetBytes("0").Length)
                                Else
                                    Dim bytL_sendData = System.Text.Encoding.UTF8.GetBytes("Version|" & lstD_Version(0).Version.ToUpper)
                                    networkStream.Write(bytL_sendData, 0, bytL_sendData.Length)
                                End If
                            Case "UPDATE"
                                Dim bytL_sendData = System.Text.Encoding.UTF8.GetBytes("UPDATE|" & lstD_Version(0).Version & lstD_Version(0).Data.ToString)
                                networkStream.Write(bytL_sendData, 0, bytL_sendData.Length)
                        End Select

                        networkStream.Flush()

                    End If

                End Using

            Catch ex As Exception
                UpdateLabel("서버 종료 또는 에러: " & ex.Message)
            End Try
        End While
    End Sub

    Private clientsocket As TcpClient

    Private Sub UpdateLabel(text As String)
        If lbl.InvokeRequired Then
            lbl.Invoke(Sub() lbl.Text = text)
        Else
            lbl.Text = text
        End If
    End Sub


    ' 1. 파일이 존재하는지 확인
    ' 2. 파일이 존재하면 strD_endPoint에 version 저장
    ' 3. 파일이 존재하지 않는 경우 api 호출  
    Private Sub sD_SaveVersionFormFile()

        Dim path As String = "c:\hanimacdata\socket"

        Try
            If Not System.IO.Directory.Exists("c:\hanimacdata\socket") Then
                System.IO.Directory.CreateDirectory("c:\hanimacdata\socket")
            End If

            Dim dirInfo = New DirectoryInfo(path)
            Dim files As FileInfo() = dirInfo.GetFiles("drug_*")

            If files.Length <> 0 Then
                ' 파일이 여러개일 경우 최신 파일 하나만 반환
                Dim lastestFile As FileInfo = files.OrderByDescending(Function(x) x.LastWriteTime).First

                ' 파일내의 version을 strD_endPoint에 저장하고 최신 버전 확인 
                Dim strL_fileText As String = File.ReadAllText(lastestFile.FullName)
                Dim jobL_fileText As JObject = JObject.Parse(strL_fileText)
                strD_Version = jobL_fileText("version")?.ToString
            End If

        Catch ex As Exception
            Debug.Print("FileExistsCheck :" & ex.Message)
        End Try

        sD_ApiVersionRequest()

    End Sub


    ' 타이머,,, 똑딱
    Private Async Function sD_timerForApi() As Task

        While True
            Dim now As DateTime = DateTime.Now
            Dim targetTime As DateTime = New DateTime(now.Year, now.Month, now.Day, 8, 0, 0)

            ' 이미 시간이 지났으면 다음 날로 설정
            If now >= targetTime Then
                targetTime = targetTime.AddDays(1)
            End If

            Dim delayDuration As TimeSpan = targetTime - now
            Await Task.Delay(delayDuration)

            MsgBox("time now : " & targetTime)

            sD_ApiVersionRequest()
        End While

    End Function


    ' api 버전이 기존과 다른 경우 strD_endPoint에 저장하고 sD_ApiRequest 실행 
    Private Sub sD_ApiVersionRequest()

        Try
            Dim req_endPoint As HttpWebRequest = WebRequest.Create("https://infuser.odcloud.kr/oas/docs?namespace=15067462/v1")

            req_endPoint.Timeout = 15000

            Dim res_endPoint As HttpWebResponse = req_endPoint.GetResponse
            If res_endPoint.StatusCode = HttpStatusCode.OK Then
                Dim reader As System.IO.StreamReader = New System.IO.StreamReader(res_endPoint.GetResponseStream, System.Text.Encoding.UTF8)
                Dim strL_ResData As String = reader.ReadToEnd() : reader.Close()

                Dim json As JObject = JObject.Parse(strL_ResData)

                Dim paths As JObject = json("paths")

                For Each item In paths
                    Try
                        Dim Jobject_value As JObject = JObject.Parse(item.Value.ToString)
                        Jobject_value = JObject.Parse(Jobject_value("get").ToString)

                        Dim summary As String = Jobject_value("summary").ToString

                        If summary.Split("_").Count > 0 Then
                            Dim Version As String = summary.Split("_").Last
                            If Not IsNumeric(Version) Then
                                If IsDate(Version) Then
                                    Version = CDate(Version).ToString("yyyyMMdd")
                                Else
                                    Debug.Print("버전분석필요")
                                    Continue For
                                End If
                            End If
                            lstD_Version.Add(New st_Version With {.Version = Version, .EndPoint = item.Key.ToString})
                        Else
                            Debug.Print("버전분석필요")
                            Continue For
                        End If
                    Catch ex As Exception

                    End Try
                Next

                lstD_Version = lstD_Version.OrderByDescending(Function(x) x.Version).ToList

                If strD_Version = "" Or strD_Version < lstD_Version(0).Version Then
                    sD_ApiRequest()
                End If
            End If

            req_endPoint.Abort()
            res_endPoint.Close()
        Catch ex As Exception
            MsgBox($"sD_ApiVersionRequest : {ex.ToString}")
        End Try

    End Sub

    Private Sub sD_ApiRequest()

        Dim strL_baseUrl As String = "https://api.odcloud.kr/api"
        Dim strL_serviceKey As String = WebUtility.UrlEncode("vnmcN8SOE8d/50pMlqncKOD+4tiHW024CrK7fzIzeoUVyvaflZxcVMNxwYFN25hnbPuaLuq/C1SptJ8BfdE0wQ==")

        Dim intL_Page As Integer = 1
        Dim intL_LastPage As Integer = 2
        Dim intL_Row As Integer = 500

        Dim jArrL_drug As New JArray()

        Try
            Dim errorCount As Integer = 0
            Do
                Dim req_drug As HttpWebRequest = WebRequest.Create(strL_baseUrl + lstD_Version(0).EndPoint + $"?page={intL_Page}&perPage={intL_Row}&serviceKey={strL_serviceKey}")
                req_drug.Timeout = 700000

                Dim res_drug As HttpWebResponse = req_drug.GetResponse

                If res_drug.StatusCode = HttpStatusCode.OK Then
                    Dim reader As System.IO.StreamReader = New System.IO.StreamReader(res_drug.GetResponseStream, System.Text.Encoding.UTF8)
                    Dim strL_ResData As String = reader.ReadToEnd() : reader.Close()

                    Dim json As JObject = JObject.Parse(strL_ResData)

                    intL_LastPage = Math.Ceiling(CInt(json("totalCount")) / intL_Row)

                    Dim lst_temp As st_Version = lstD_Version(0)

                    If lst_temp.Data Is Nothing Then
                        lst_temp.Data = New JArray()
                    End If

                    lst_temp.Data.Merge(json("data"))
                    lstD_Version(0) = lst_temp

                    'Dim Arr_items As JArray = json("data")

                    'For Each item As JObject In json("data")
                    '    jArrL_drug.Add(item)
                    'Next

                    intL_Page += 1

                    req_drug.Abort()
                    res_drug.Close()

                ElseIf Not res_drug.StatusCode = HttpStatusCode.OK Then
                    errorCount += 1
                ElseIf errorCount = 2 Then
                    Throw New Exception
                End If

            Loop While intL_Page <= 3


            'Dim temp As st_Version = lstD_Version(0)
            'temp.Data = jArrL_drug
            'lstD_Version(0) = temp

            jobD_sendData("version") = lstD_Version(0).Version
            jobD_sendData("data") = lstD_Version(0).Data

            Debug.Print("api돌아따 : " & strD_Version)

            Dim filePath As String = "c:\hanimacdata\socket\"

            File.WriteAllText(filePath & "drug_" & Date.Now.ToString("yyyyMMdd") & ".json", jobD_sendData?.ToString)

        Catch ex As Exception
            Debug.Print("API : " & ex.Message)
        End Try

        'sD_timerForApi()

    End Sub

    Private Sub btn_Click(sender As Object, e As EventArgs) Handles btn.Click
        sD_SaveVersionFormFile()
    End Sub

End Class

