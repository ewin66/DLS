using System;
using PdfViewerDemo;

namespace DevExpress.ProductsDemo.Win.Modules {
    public partial class PdfViewerModule : BaseModule {
        const string fileName = "Demo.pdf";

        readonly LimitationsForm limitationsForm;

        protected override bool AutoMergeRibbon { get { return true; } }

        public PdfViewerModule() {
            InitializeComponent();
            pdfViewer.DocumentCreator = "PDF Viewer Demo";
            pdfViewer.DocumentProducer = "Developer Express Inc., " + AssemblyInfo.Version;
            pdfViewer.CreateRibbon();
            string path = DemoUtils.GetRelativePath(fileName);
            if (!String.IsNullOrEmpty(path))
                pdfViewer.LoadDocument(path);
            limitationsForm = new LimitationsForm(pdfViewer);
        }
        internal override void ShowModule(bool firstShow) {
            base.ShowModule(firstShow);
            MainRibbon.SelectedPage = MainRibbon.MergedPages[0];
        }
        internal override void HideModule() {
            base.HideModule();
            if (pdfViewer != null)
                pdfViewer.HideFindDialog(true);
            limitationsForm.Visible = false;
        }
    }
}
