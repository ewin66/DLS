using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using System.IO;
using DevExpress.ProductsDemo.Win.Forms;
using DevExpress.Spreadsheet;
using DevExpress.XtraPrinting;

namespace DevExpress.ProductsDemo.Win.Modules {
    public partial class SpreadsheetModule : BaseModule {
        const string FileName = "LoanCalculator.xlsx";
        //const string Accounting = "_(\\$* #,##0.00_);_(\\$ (#,##0.00);_(\\$* \" - \"??_);_(@_)";
        //readonly HashSet<String> incomeCategorys = new HashSet<string>();
        //readonly HashSet<String> expenseCategorys = new HashSet<string>();
        public SpreadsheetModule() {
            InitializeComponent();
            string filePath = DemoUtils.GetRelativePath(FileName);
            if (String.IsNullOrEmpty(filePath))
                return;
            this.spreadsheetControl1.LoadDocument(filePath);
        }
                
        protected override bool AutoMergeRibbon { get { return true; } }
        public override IPrintable PrintableComponent { get { return spreadsheetControl1; } }
        public override IPrintable ExportComponent { get { return spreadsheetControl1; } }
        public override bool AllowRtfTitle { get { return false; } }
        internal override void ShowModule(bool firstShow) {
            base.ShowModule(firstShow);
            MainRibbon.SelectedPage = MainRibbon.MergedPages.GetPageByName(homeRibbonPage1.Name);
        }

    }
}
