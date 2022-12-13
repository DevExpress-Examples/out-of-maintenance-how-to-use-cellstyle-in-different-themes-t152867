Imports System.Collections.Generic

Namespace CellStyle

    Public Class Data

        Public Shared ReadOnly Property DataList As List(Of Node)
            Get
                Dim list As List(Of Node) = New List(Of Node)()
                For i As Integer = 0 To 10 - 1
                    list.Add(New Node("key" & i, "line " & i))
                Next

                Return list
            End Get
        End Property
    End Class

    Public Class Node

        Public Property Key As String

        Public Property Text As String

        Public Sub New(ByVal key As String, ByVal text As String)
            Me.Key = key
            Me.Text = text
        End Sub
    End Class
End Namespace
