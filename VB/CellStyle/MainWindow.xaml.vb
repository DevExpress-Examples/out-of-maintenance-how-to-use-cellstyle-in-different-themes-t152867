Imports System.Windows
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Editors

Namespace CellStyle

    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            ThemeManager.ApplicationThemeName = TryCast(e.NewValue, Theme)?.Name
        End Sub
    End Class
End Namespace
