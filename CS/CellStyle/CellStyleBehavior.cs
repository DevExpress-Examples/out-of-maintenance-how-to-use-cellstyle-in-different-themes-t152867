using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Core;
using System.Windows;
using DevExpress.Xpf.Grid.Themes;
using DevExpress.Mvvm.UI.Interactivity;

namespace CellStyle
{
    public class CellStyleBehavior : Behavior<TableView> {
        public static DependencyProperty CellStyleProperty =
            DependencyProperty.Register("CellStyle", typeof(Style), typeof(CellStyleBehavior), new PropertyMetadata());

        public Style CellStyle {
            get { return (Style)GetValue(CellStyleProperty); }
            set {
                if (value.TargetType == typeof(LightweightCellEditor)) {
                    SetValue(CellStyleProperty, value);
                    ApplyStyleToGrid(ThemeManager.ApplicationThemeName);
                }
            }
        }

        protected TableView View { get { return AssociatedObject; } }

        protected override void OnAttached() {
            base.OnAttached();
            ApplyStyleToGrid(ThemeManager.ApplicationThemeName);
            ThemeManager.ThemeChanged += ThemeChanged;
        }
        protected override void OnDetaching() {
            ThemeManager.ThemeChanged -= ThemeChanged;
            base.OnDetaching();
        }

        private Style CopyStyle(Style originalStyle) {
            Style copiedStyle = new Style();
            copiedStyle.TargetType = originalStyle.TargetType;

            foreach (var elem in originalStyle.Setters)
                copiedStyle.Setters.Add(elem);

            foreach (var elem in originalStyle.Triggers)
                copiedStyle.Triggers.Add(elem);
            return copiedStyle;
        }

        private void ApplyStyleToGrid(string themeName) {
            if (CellStyle == null) {
                View.CellStyle = null;
                return;
            }
            GridRowThemeKeyExtension newKey = new GridRowThemeKeyExtension();
            newKey.ResourceKey = GridRowThemeKeys.LightweightCellStyle;
            if (themeName != "DeepBlue")
                newKey.ThemeName = themeName;
            Style currStyle = CopyStyle(CellStyle);
            currStyle.BasedOn = View.TryFindResource(newKey) as Style;
            View.CellStyle = currStyle;
        }

        protected virtual void ThemeChanged(DependencyObject sender, ThemeChangedRoutedEventArgs e) {
            ApplyStyleToGrid(e.ThemeName);
        }
    }
}
