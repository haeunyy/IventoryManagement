Imports System.Net


Module Module1
    Dim baseUrl As String = "https://apis.data.go.kr/1471000/DrugPrdtPrmsnInfoService06"
    Dim endpoint As String = "/getDrugPrdtPrmsnlng06"
    Dim serviceKey As String = WebUtility.UrlEncode("vnmcN8SOE8d/50pMlqncKOD+4tiHW024CrK7fzIzeoUVyvaflZxcVMNxwYFN25hnbPuaLuq/C1SptJ8BfdE0wQ==")
    Dim queryParams As String = $"?serviceKey={serviceKey}&type=json&pageNo=1&numOfRows=10"
End Module
