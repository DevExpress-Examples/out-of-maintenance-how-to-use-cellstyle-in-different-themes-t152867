Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Grid.Themes

Namespace CellStyle
    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Partial Public Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            grid1.ItemsSource = Data.DataList
        End Sub

        Private Sub themesComboBox_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
            ThemeManager.ApplicationThemeName = (TryCast(e.AddedItems(0), Theme)).Name
        End Sub
    End Class


End Namespace
