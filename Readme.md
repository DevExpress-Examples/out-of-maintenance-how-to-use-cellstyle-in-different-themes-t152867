<!-- default file list -->
*Files to look at*:

* **[CellStyleBehavior.cs](./CS/CellStyle/CellStyleBehavior.cs) (VB: [CellStyleBehavior.vb](./VB/CellStyle/CellStyleBehavior.vb))**
* [Data.cs](./CS/CellStyle/Data.cs) (VB: [Data.vb](./VB/CellStyle/Data.vb))
* [MainWindow.xaml](./CS/CellStyle/MainWindow.xaml) (VB: [MainWindow.xaml.vb](./VB/CellStyle/MainWindow.xaml.vb))
* [MainWindow.xaml.cs](./CS/CellStyle/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/CellStyle/MainWindow.xaml.vb))
<!-- default file list end -->
# How to use CellStyle in different themes

### Starting with v18.2, CellStyle is theme-independent. Therefore, it's not necessary to set its BasedOn property and the approach illustrated in this example is not required. Use this approach in v18.1 and lower.


<p>To implement a cell style of <strong>GridControl</strong>, it is necessary to create a new style based on the already existing cell style. When an application theme is changed, the basic cell style must be also changed. To do this, you can create behavior that will change this basic style. It is necessary to add the <strong>ThemeManager's ThemeChanged </strong>event handler to the behavior.</p>

<br/>


