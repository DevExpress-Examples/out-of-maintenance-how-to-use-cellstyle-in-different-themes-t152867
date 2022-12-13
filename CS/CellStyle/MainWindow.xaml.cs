using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;

namespace CellStyle {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
        private void EditValueChanged(object sender, EditValueChangedEventArgs e) {
            ThemeManager.ApplicationThemeName = (e.NewValue as Theme)?.Name;
        }
    }
}