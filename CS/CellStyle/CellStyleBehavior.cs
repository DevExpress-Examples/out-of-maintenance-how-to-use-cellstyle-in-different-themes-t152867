using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Core;
using System.Windows;
using DevExpress.Xpf.Grid.Themes;
using DevExpress.Mvvm.UI.Interactivity;
using System.Windows.Data;
using System.Reflection;

namespace CellStyle {
    public class CellStyleBehavior : Behavior<DependencyObject> {
        public static DependencyProperty CellStyleProperty =
            DependencyProperty.Register("CellStyle", typeof(Style), typeof(CellStyleBehavior), new PropertyMetadata((d, _) => ((CellStyleBehavior)d).UpdateStyle()));
        public static readonly DependencyProperty ThemeNameProperty =
            DependencyProperty.Register("ThemeName", typeof(string), typeof(CellStyleBehavior), new PropertyMetadata((d, _) => ((CellStyleBehavior)d).UpdateStyle()));

        public Style Style {
            get { return (Style)GetValue(CellStyleProperty); }
            set { SetValue(CellStyleProperty, value); }
        }
        public string ThemeName {
            get { return (string)GetValue(ThemeNameProperty); }
            set { SetValue(ThemeNameProperty, value); }
        }

        static PropertyInfo ThemeNameInfo = typeof(ThemeTreeWalker).GetProperty(nameof(ThemeTreeWalker.ThemeName));
        protected override void OnAttached() {
            base.OnAttached();
            BindingOperations.SetBinding(this, ThemeNameProperty, new Binding { Path = new PropertyPath("(0).(1)", ThemeManager.TreeWalkerProperty, ThemeNameInfo), Source = AssociatedObject });
            UpdateStyle();
        }
        protected override void OnDetaching() {
            BindingOperations.ClearBinding(this, ThemeNameProperty);
            base.OnDetaching();
        }

        protected virtual void UpdateStyle() {
            if (AssociatedObject == null)
                return;
            if (Style == null) {
                UpdateStyle(null);
                return;
            }
            Style style = CopyStyle(Style);
            style.BasedOn = GetThemeStyle(new GridRowThemeKeyExtension { ResourceKey = GridRowThemeKeys.LightweightCellStyle, ThemeName = ThemeName == "DeepBlue" ? null : ThemeName });
            UpdateStyle(style);
        }
        protected virtual void UpdateStyle(Style newValue) {
            if (AssociatedObject is DataViewBase view)
                view.CellStyle = newValue;
            else if (AssociatedObject is GridColumn column)
                column.CellStyle = newValue;
        }
        protected virtual Style GetThemeStyle(object key) {
            if (AssociatedObject is FrameworkElement fe)
                return fe.TryFindResource(key) as Style;
            else if (AssociatedObject is FrameworkContentElement fce)
                return fce.TryFindResource(key) as Style;
            return null;
        }
        protected static Style CopyStyle(Style style) {
            Style copy = new Style(style.TargetType);
            foreach (var elem in style.Setters)
                copy.Setters.Add(elem);
            foreach (var elem in style.Triggers)
                copy.Triggers.Add(elem);
            return copy;
        }
    }
}