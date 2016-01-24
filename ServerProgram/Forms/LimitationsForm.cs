using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPdfViewer;
using DevExpress.XtraPdfViewer.Native;

namespace PdfViewerDemo {
	public partial class LimitationsForm : XtraForm {
        readonly PdfViewer viewer;
        bool isNewDocument = true;

		public LimitationsForm() {
			InitializeComponent();
            labelControl.Font = new Font(new FontFamily("Tahoma"), 8.25f);
		}
        public LimitationsForm(PdfViewer viewer) : this() {
            this.viewer = viewer;
            Visible = false;
            foreach (Control control in viewer.Controls) {
                PdfDocumentViewer documentViewer = control as PdfDocumentViewer;
                if (documentViewer != null) {
                    documentViewer.FunctionalLimitsOccurred += new EventHandler(OnFunctionalLimitsOccurred);
                    break;
                }
            }
            viewer.DocumentChanged += new PdfDocumentChangedEventHandler(OnDocumentChanged);
        }
        void OnFunctionalLimitsOccurred(object sender, EventArgs e) {
            if (isNewDocument) {
                Form ownerForm = viewer.FindForm();
                Owner = ownerForm;
                Visible = true;
                Point ownerLocation = ownerForm.Location;
                Size ownerSize = ownerForm.Size;
                Size ownSize = Size;
                Location = new Point(ownerLocation.X + Convert.ToInt32((ownerSize.Width - ownSize.Width) / 2), ownerLocation.Y + Convert.ToInt32((ownerSize.Height - ownSize.Height) / 2));
                isNewDocument = false;
            }
        }
        void OnDocumentChanged(object sender, PdfDocumentChangedEventArgs e) {
            isNewDocument = true;
        }
        void OnOKClick(object sender, EventArgs e) {
            Visible = false;
        }
        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
            Visible = false;
            e.Cancel = true;
        }
	}
}
