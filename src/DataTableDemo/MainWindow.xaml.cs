using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using Errata.Data;
using Excel;

namespace DataTableDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtCompare_Click(object sender, RoutedEventArgs e)
        {
         
            var dataTable1 = FromExcel(txtExcelPath1.Text );
            var dataTable2 = FromExcel(txtExcelPath2.Text);
           Debug.WriteLine( dataTable1.IsSameAs(dataTable2)) ;
            var keys1 = dataTable1.UniqueValueHash<double>("key");
            var keys2 = dataTable2.UniqueValueHash<double>("key");


            var onlyIn1 = (from double item in keys1
                where !keys2.Contains(item)
                select item).ToList() ;

            var dtOnlyIn1 = dataTable1.RowsOfValue<double>("key", onlyIn1);
            grdOnly1.ItemsSource = new DataView( dtOnlyIn1);


            var onlyIn2 = (from double item in keys2
                          where !keys1.Contains(item)
                          select item).ToList() ;

            var dtOnlyIn2 = dataTable2.RowsOfValue<double>("key", onlyIn2);
            grdOnly2.ItemsSource = new DataView(dtOnlyIn2);
        }

        private DataTable FromExcel(string path)
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                {
                    excelReader.IsFirstRowAsColumnNames = true;
                    var dataSet = excelReader.AsDataSet();
                    var dataTable = dataSet.Tables[0];
                    return dataTable;
                }
            }

        }
    }
}
