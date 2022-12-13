Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Core

Namespace CellStyle

    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.grid1.ItemsSource = Data.DataList
        End Sub

        Private Sub themesComboBox_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
            ThemeManager.ApplicationThemeName = TryCast(e.AddedItems(0), Theme).Name
        End Sub
    End Class
End Namespace
