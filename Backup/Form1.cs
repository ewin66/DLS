using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraPivotGrid;

namespace ChartChangeView
{
    public partial class Form1 : XtraForm
    {
        #region Ctors

        public Form1()
        {
            InitializeComponent();

            PrepareGrid();
        }

        #endregion
        #region Event handlers

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            /*for (int i = 0; i < chartControl1.Series.Count; i++)
            {
                chartControl1.Series[i].ChangeView(ViewType.Bar);
            }*/

            chartControl1.SeriesTemplate.ChangeView(ViewType.Bar);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            /*for (int i = 0; i < chartControl1.Series.Count; i++)
            {
                chartControl1.Series[i].ChangeView(ViewType.ManhattanBar);
            }*/

            chartControl1.SeriesTemplate.ChangeView(ViewType.ManhattanBar);
        }

        #endregion
        #region Private methods

        private void PrepareGrid()
        {
            // Create a connection object.
            OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Program Files\DevExpress 2009.3\Components\Demos\Data\nwind.mdb");

            // Create a data adapter.
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM SalesPerson", connection);

            // Create and fill a dataset.
            DataSet sourceDataSet = new DataSet();
            adapter.Fill(sourceDataSet, "SalesPerson");

            // Assign the data source to the XtraPivotGrid control.
            pivotGridControl1.DataSource = sourceDataSet.Tables["SalesPerson"];

            // Create a row pivot grid field bound to the Country datasource field.
            PivotGridField fieldCountry = new PivotGridField("Country", PivotArea.RowArea);

            // Create a row pivot grid field bound to the Sales Person datasource field.
            PivotGridField fieldCustomer = new PivotGridField("Sales Person", PivotArea.RowArea);
            fieldCustomer.Caption = "Customer";

            // Create a column pivot grid field bound to the OrderDate datasource field.
            PivotGridField fieldYear = new PivotGridField("OrderDate", PivotArea.ColumnArea);
            fieldYear.Caption = "Year";
            // Group field values by years.
            fieldYear.GroupInterval = PivotGroupInterval.DateYear;

            // Create a column pivot grid field bound to the CategoryName datasource field.
            PivotGridField fieldCategoryName = new PivotGridField("CategoryName", PivotArea.ColumnArea);
            fieldCategoryName.Caption = "Product Category";

            // Create a filter pivot grid field bound to the ProductName datasource field.
            PivotGridField fieldProductName = new PivotGridField("ProductName", PivotArea.FilterArea);
            fieldProductName.Caption = "Product Name";

            // Create a data pivot grid field bound to the 'Extended Price' datasource field.
            PivotGridField fieldExtendedPrice = new PivotGridField("Extended Price", PivotArea.DataArea);
            fieldExtendedPrice.CellFormat.FormatType = FormatType.Numeric;
            // Specify the formatting setting to format summary values as integer currency amount.
            fieldExtendedPrice.CellFormat.FormatString = "c0";

            // Add the fields to the control's field collection.         
            pivotGridControl1.Fields.AddRange(new PivotGridField[]
                                                  {fieldCountry, fieldCustomer, fieldCategoryName, fieldProductName, fieldYear, fieldExtendedPrice});

            // Arrange the row fields within the Row Header Area.
            fieldCountry.AreaIndex = 0;
            fieldCustomer.AreaIndex = 1;

            // Arrange the column fields within the Column Header Area.
            fieldCategoryName.AreaIndex = 0;
            fieldYear.AreaIndex = 1;

            // Customize the control's look-and-feel via the Default LookAndFeel object.
            UserLookAndFeel.Default.UseWindowsXPTheme = false;
            UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
            UserLookAndFeel.Default.SkinName = "Black";
        }

        #endregion
    }
}