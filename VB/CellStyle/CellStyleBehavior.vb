Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Core
Imports System.Windows
Imports DevExpress.Xpf.Grid.Themes
Imports DevExpress.Mvvm.UI.Interactivity

Namespace CellStyle
    Public Class CellStyleBehavior
        Inherits Behavior(Of GridControl)

        Public Shared CellStyleProperty As DependencyProperty = DependencyProperty.Register("CellStyle", GetType(Style), GetType(CellStyleBehavior), New PropertyMetadata(New Style() With {.TargetType=GetType(LightweightCellEditor)}))

        Public Property CellStyle() As Style
            Get
                Return CType(GetValue(CellStyleProperty), Style)
            End Get
            Set(ByVal value As Style)
                If value.TargetType Is GetType(LightweightCellEditor) Then
                    SetValue(CellStyleProperty, value)
                    ApplyStyleToGrid(ThemeManager.ApplicationThemeName)
                End If
            End Set
        End Property

        Private grid As GridControl
        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            Try
                grid = TryCast(Me.AssociatedObject, GridControl)
                If grid IsNot Nothing Then
                    AddHandler ThemeManager.ThemeChanged, AddressOf ThemeManager_ThemeChanged
                End If
            Finally
            End Try
        End Sub

        Private Function CopyStyle(ByVal originalStyle As Style) As Style
            Dim copiedStyle As New Style()
            copiedStyle.TargetType = originalStyle.TargetType

            For Each elem In originalStyle.Setters
                copiedStyle.Setters.Add(elem)
            Next elem

            For Each elem In originalStyle.Triggers
                copiedStyle.Triggers.Add(elem)
            Next elem
            Return copiedStyle
        End Function

        Private Sub ApplyStyleToGrid(ByVal themeName As String)
            Dim newKey As New GridRowThemeKeyExtension()
            newKey.ResourceKey = GridRowThemeKeys.LightweightCellStyle
            If themeName <> "DeepBlue" Then
                newKey.ThemeName = themeName
            End If
            Dim currStyle As Style = CopyStyle(CellStyle)
            currStyle.BasedOn = TryCast(Window.GetWindow(grid).FindResource(newKey), Style)
            grid.View.CellStyle = currStyle
        End Sub

        Private Sub ThemeManager_ThemeChanged(ByVal sender As DependencyObject, ByVal e As ThemeChangedRoutedEventArgs)
            ApplyStyleToGrid(e.ThemeName)
        End Sub
    End Class
End Namespace
