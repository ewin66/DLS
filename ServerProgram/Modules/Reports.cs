using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using XtraReportsDemos;
using System.IO;

namespace DevExpress.ProductsDemo.Win.Modules {
    public partial class ReportsModule : BaseModule {
        static ReportsModule() {
            string path = DevExpress.Utils.FilesHelper.FindingFileName(AppDomain.CurrentDomain.BaseDirectory, @"Data\nwind.mdb", false);
            ConnectionHelper.ApplyDataDirectory(Path.GetDirectoryName(path));
        }
        public ReportsModule() {
            InitializeComponent();
        }

        internal override void ShowModule(bool firstShow) {
            base.ShowModule(firstShow);
            if(firstShow) {
                reportDesigner1.ContainerControl = this;
                XtraReport report = new XtraReportsDemos.MasterDetailReport.Report();
                report.ReportPrintOptions.DetailCountAtDesignTime = 0;
                foreach(XtraReportBase item in report.AllControls<XtraReportBase>()) {
                    item.ReportPrintOptions.DetailCountAtDesignTime = 0;
                }
                reportDesigner1.OpenReport(report);
                MainRibbon.AutoHideEmptyItems = true;
                MainRibbon.SelectedPage = MainRibbon.MergedPages.GetPageByText("VIEW");
                MainRibbon.SelectedPage = MainRibbon.MergedPages.GetPageByName(ribbonPagePreview.Name);
                var reportControl = reportDesigner1.ActiveDesignPanel.GetService(typeof(DevExpress.XtraReports.Design.ReportTabControl)) as DevExpress.XtraReports.Design.ReportTabControl;
                if(reportControl == null || reportControl.PreviewControl == null) return;
                DevExpress.XtraBars.Docking.DockPanel navigationDockPanel = reportControl.PreviewControl.DockManager.Panels[new System.Guid("6b2e64eb-afd0-4676-bc3d-eca7e99946aa")];
                if(navigationDockPanel != null) {
                    navigationDockPanel.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Right;
                }
                return;
            }
            MainRibbon.SelectedPage = MainRibbon.MergedPages.GetPageByName(ribbonPagePreview.Name);
        }

        protected override bool AutoMergeRibbon { get { return true; } }
    }
}
