Imports System
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Core
Imports System.Windows
Imports DevExpress.Xpf.Grid.Themes
Imports DevExpress.Mvvm.UI.Interactivity
Imports System.Windows.Data
Imports System.Reflection

Namespace CellStyle

    Public Class CellStyleBehavior
        Inherits Behavior(Of DependencyObject)

        Public Shared CellStyleProperty As DependencyProperty = DependencyProperty.Register("CellStyle", GetType(Style), GetType(CellStyleBehavior), New PropertyMetadata(Sub(d, __) CType(d, CellStyleBehavior).UpdateStyle()))

        Public Shared ReadOnly ThemeNameProperty As DependencyProperty = DependencyProperty.Register("ThemeName", GetType(String), GetType(CellStyleBehavior), New PropertyMetadata(Sub(d, __) CType(d, CellStyleBehavior).UpdateStyle()))

        Public Property Style As Style
            Get
                Return CType(GetValue(CellStyleProperty), Style)
            End Get

            Set(ByVal value As Style)
                SetValue(CellStyleProperty, value)
            End Set
        End Property

        Public Property ThemeName As String
            Get
                Return CStr(GetValue(ThemeNameProperty))
            End Get

            Set(ByVal value As String)
                SetValue(ThemeNameProperty, value)
            End Set
        End Property

        Private Shared ThemeNameInfo As PropertyInfo = GetType(ThemeTreeWalker).GetProperty(NameOf(ThemeTreeWalker.ThemeName))

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            Call BindingOperations.SetBinding(Me, ThemeNameProperty, New Binding With {.Path = New PropertyPath("(0).(1)", ThemeManager.TreeWalkerProperty, ThemeNameInfo), .Source = AssociatedObject})
            UpdateStyle()
        End Sub

        Protected Overrides Sub OnDetaching()
            BindingOperations.ClearBinding(Me, ThemeNameProperty)
            MyBase.OnDetaching()
        End Sub

        Protected Overridable Sub UpdateStyle()
            If AssociatedObject Is Nothing Then Return
            If Me.Style Is Nothing Then
                UpdateStyle(Nothing)
                Return
            End If

            Dim style As Style = CopyStyle(Me.Style)
            style.BasedOn = GetThemeStyle(New GridRowThemeKeyExtension With {.ResourceKey = GridRowThemeKeys.LightweightCellStyle, .ThemeName = If(Equals(ThemeName, "DeepBlue"), Nothing, ThemeName)})
            UpdateStyle(style)
        End Sub

        Protected Overridable Sub UpdateStyle(ByVal newValue As Style)
            Dim view As DataViewBase = Nothing, column As GridColumn = Nothing
            If CSharpImpl.__Assign(view, TryCast(AssociatedObject, DataViewBase)) IsNot Nothing Then
                view.CellStyle = newValue
            ElseIf CSharpImpl.__Assign(column, TryCast(AssociatedObject, GridColumn)) IsNot Nothing Then
                column.CellStyle = newValue
            End If
        End Sub

        Protected Overridable Function GetThemeStyle(ByVal key As Object) As Style
            Dim fe As FrameworkElement = Nothing, fce As FrameworkContentElement = Nothing
            If CSharpImpl.__Assign(fe, TryCast(AssociatedObject, FrameworkElement)) IsNot Nothing Then
                Return TryCast(fe.TryFindResource(key), Style)
            ElseIf CSharpImpl.__Assign(fce, TryCast(AssociatedObject, FrameworkContentElement)) IsNot Nothing Then
                Return TryCast(fce.TryFindResource(key), Style)
            End If

            Return Nothing
        End Function

        Protected Shared Function CopyStyle(ByVal style As Style) As Style
            Dim copy As Style = New Style(style.TargetType)
            For Each elem In style.Setters
                copy.Setters.Add(elem)
            Next

            For Each elem In style.Triggers
                copy.Triggers.Add(elem)
            Next

            Return copy
        End Function

        Private Class CSharpImpl

            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class
End Namespace
