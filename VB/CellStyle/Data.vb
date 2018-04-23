Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace CellStyle
    Public Class Data
        Public Shared ReadOnly Property DataList() As List(Of Node)
            Get
                Dim list As New List(Of Node)()
                For i As Integer = 0 To 9
                    list.Add(New Node("key" & i, "line " & i))
                Next i
                Return list
            End Get
        End Property
    End Class

    Public Class Node
        Public Property Key() As String
        Public Property Text() As String

        Public Sub New(ByVal key As String, ByVal text As String)
            Me.Key = key
            Me.Text = text
        End Sub
    End Class
End Namespace
