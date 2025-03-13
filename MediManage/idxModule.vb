Module idxModule

    Public Function getValue(data As Decimal)

        If Not data = 0 AndAlso Not IsDBNull(data) AndAlso IsNumeric(data) Then
            Return data
        Else
            Return 0
        End If

    End Function

End Module
