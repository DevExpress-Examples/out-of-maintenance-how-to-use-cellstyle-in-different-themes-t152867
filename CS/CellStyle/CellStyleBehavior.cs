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
    public class CellStyleBehavior : Behavior<GridControl>
    {
        public static DependencyProperty CellStyleProperty = DependencyProperty.Register(
            "CellStyle", typeof(Style), typeof(CellStyleBehavior),
            new PropertyMetadata(new Style() { TargetType=typeof(LightweightCellEditor)}));

        public Style CellStyle
        {
            get { return (Style)GetValue(CellStyleProperty); }
            set
            {
                if (value.TargetType == typeof(LightweightCellEditor))
                {
                    SetValue(CellStyleProperty, value);
                    ApplyStyleToGrid(ThemeManager.ApplicationThemeName);
                }
            }
        }

        GridControl grid;
        protected override void OnAttached()
        {
            base.OnAttached();
            try
            {
                grid = this.AssociatedObject as GridControl;
                if (grid != null)
                    ThemeManager.ThemeChanged += new ThemeChangedRoutedEventHandler(ThemeManager_ThemeChanged);
            }
            finally { }
        }

        private Style CopyStyle(Style originalStyle)
        {
            Style copiedStyle = new Style();
            copiedStyle.TargetType = originalStyle.TargetType;

            foreach (var elem in originalStyle.Setters)
                copiedStyle.Setters.Add(elem);

            foreach (var elem in originalStyle.Triggers)
                copiedStyle.Triggers.Add(elem);
            return copiedStyle;
        }

        private void ApplyStyleToGrid(string themeName)
        {
            GridRowThemeKeyExtension newKey = new GridRowThemeKeyExtension();
            newKey.ResourceKey = GridRowThemeKeys.LightweightCellStyle;
            if (themeName != "DeepBlue")
                newKey.ThemeName = themeName;
            Style currStyle = CopyStyle(CellStyle);
            currStyle.BasedOn = Window.GetWindow(grid).FindResource(newKey) as Style;
            grid.View.CellStyle = currStyle;
        }

        void ThemeManager_ThemeChanged(DependencyObject sender, ThemeChangedRoutedEventArgs e)
        {
            ApplyStyleToGrid(e.ThemeName);
        }
    }
}
