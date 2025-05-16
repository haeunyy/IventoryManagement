Public Module mdlClass
    Public clsG_DBmng As New clsDBmng
    Public MainForm As frm_Main
    Public clsApiConnection As New clsApi
    Sub Main()

        MainForm = New frm_Main
        Application.Run(MainForm)

    End Sub
End Module
