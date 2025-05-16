Public Class clsException

    Inherits Exception

    Public ErrorCode As Integer
    Public LoggedTime As DateTime
    Public ContextInfo As String

    Public Sub New(message As String, errorCode As Integer, Optional context As String = "")
        MyBase.New(message)
        Me.ErrorCode = errorCode
        Me.LoggedTime = DateTime.Now
        Me.ContextInfo = context
    End Sub

    Public Overrides Function ToString() As String
        Return $"[{LoggedTime}] {vbNewLine} 에러 코드 {ErrorCode}: {Message} (상황: {ContextInfo})"
    End Function
End Class