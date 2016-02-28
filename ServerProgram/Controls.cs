﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Data.OleDb;
using DevExpress.MailClient.Win;
using DevExpress.Utils;
using DevExpress.Utils.Design;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraNavBar;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSplashScreen;
using MySql.Data.MySqlClient;
using DevExpress.ProductsDemo.Win.Common;
using DevExpress.ProductsDemo.Win.Serial;

namespace DevExpress.ProductsDemo.Win {
    public class ModulesNavigator {
        RibbonControl ribbon;
        PanelControl panel;
        public ModulesNavigator(RibbonControl ribbon, PanelControl panel) {
            this.ribbon = ribbon;
            this.panel = panel;
        }
        public void ChangeSelectedItem(NavBarItemLink link, object moduleData) {
            bool allowSetVisiblePage = true;
            NavBarGroupTagObject groupObject = link.Item.Tag as NavBarGroupTagObject;
            if (groupObject == null)
                return;
            List<RibbonPage> deferredPagesToShow = new List<RibbonPage>();
            foreach (RibbonPage page in ribbon.Pages) {
                if (!string.IsNullOrEmpty(string.Format("{0}", page.Tag))) {
                    bool isPageVisible = groupObject.Name.Equals(page.Tag);
                    if (isPageVisible != page.Visible && isPageVisible)
                        deferredPagesToShow.Add(page);
                    else
                        page.Visible = isPageVisible;
                }
                if (page.Visible && allowSetVisiblePage) {
                    //page.Text = "Home";
                    ribbon.SelectedPage = page;
                    allowSetVisiblePage = false;
                }
            }
            bool firstShow = groupObject.Module == null;
            if (firstShow) {
                if (SplashScreenManager.Default == null)
                    SplashScreenManager.ShowForm(ribbon.FindForm(), typeof(wfMain), false, true);
                ConstructorInfo constructorInfoObj = groupObject.ModuleType.GetConstructor(Type.EmptyTypes);
                if (constructorInfoObj != null) {
                    groupObject.Module = constructorInfoObj.Invoke(null) as BaseModule;
                    groupObject.Module.InitModule(ribbon, moduleData);
                }
                if (SplashScreenManager.Default != null) {
                    Form frm = moduleData as Form;
                    if (frm != null) {
                        if (SplashScreenManager.FormInPendingState)
                            SplashScreenManager.CloseForm();
                        else
                            SplashScreenManager.CloseForm(false, 500, frm);
                    }
                    else
                        SplashScreenManager.CloseForm();
                }
            }

            foreach (RibbonPage page in deferredPagesToShow) {
                page.Visible = true;
            }
            foreach (RibbonPage page in ribbon.Pages) {
                if (page.Visible) {
                    ribbon.SelectedPage = page;
                    break;
                }
            }

            if (groupObject.Module != null) {
                if (panel.Controls.Count > 0) {
                    BaseModule currentModule = panel.Controls[0] as BaseModule;
                    if (currentModule != null)
                        currentModule.HideModule();
                }
                panel.Controls.Clear();
                panel.Controls.Add(groupObject.Module);
                groupObject.Module.Dock = DockStyle.Fill;
                groupObject.Module.ShowModule(firstShow);
            }
        }
        public BaseModule CurrentModule {
            get {
                if (panel.Controls.Count == 0)
                    return null;
                return panel.Controls[0] as BaseModule;
            }
        }
    }
    public class BaseControl : XtraUserControl {
        public BaseControl() {
            if (!DesignTimeTools.IsDesignMode)
                LookAndFeel.ActiveLookAndFeel.StyleChanged += new EventHandler(ActiveLookAndFeel_StyleChanged);
            this.VisibleChanged += new EventHandler(BaseControl_VisibleChanged);
        }
        void BaseControl_VisibleChanged(object sender, EventArgs e) {
            if (this.Visible) {
                ShowControlFirstTime();
                this.VisibleChanged -= new EventHandler(BaseControl_VisibleChanged);
            }
        }
        internal virtual void ShowControlFirstTime() { }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (!DesignTimeTools.IsDesignMode)
                LookAndFeelStyleChanged();
        }
        protected override void Dispose(bool disposing) {
            if (disposing && !DesignTimeTools.IsDesignMode)
                LookAndFeel.ActiveLookAndFeel.StyleChanged -= new EventHandler(ActiveLookAndFeel_StyleChanged);
            base.Dispose(disposing);
        }
        void ActiveLookAndFeel_StyleChanged(object sender, EventArgs e) {
            LookAndFeelStyleChanged();
        }
        protected virtual void LookAndFeelStyleChanged() { }


    }
    public class BaseModule : BaseControl {
        protected string partName = string.Empty;
        protected OleDbConnection connection;
        protected MySqlConnection mariaDbConnection;
        protected Modbus serialConnection;

        public BaseModule() { }

        internal frmMain OwnerForm { get { return this.FindForm() as frmMain; } }
        protected RibbonControl MainRibbon { get { return OwnerForm.Ribbon; } }

        internal virtual void ShowModule(bool firstShow) {
            if (OwnerForm == null)
                return;
            if (AutoMergeRibbon && ChildRibbon != null) {
                OwnerForm.Ribbon.MergeRibbon(ChildRibbon);
                    RibbonPage page = OwnerForm.Ribbon.Pages.GetPageByText("VIEW");
                    if(page != null) {
                        OwnerForm.Ribbon.MergedPages.Remove(page);
                        OwnerForm.Ribbon.MergedPages.Insert(OwnerForm.Ribbon.MergedPages.Count, page);
                    }
                if(ChildRibbonStatusBar != null) {
                    OwnerForm.RibbonStatusBar.MergeStatusBar(ChildRibbonStatusBar);
                    OwnerForm.ShowInfo(false);
                }
            }
            OwnerForm.SaveAsMenuItem.Enabled = SaveAsEnable;
            OwnerForm.SaveAttachmentMenuItem.Enabled = SaveAttachmentEnable;
            ShowReminder();
            ShowInfo();
            OwnerForm.ZoomManager.ZoomFactor = (int)(ZoomFactor * 100);
            SetZoomCaption();
            OwnerForm.EnableZoomControl(AllowZoomControl);
            OwnerForm.OnModuleShown(this);
        }
        internal virtual void FocusObject(object obj) { }
        protected virtual void ShowReminder() {
            if(OwnerForm != null)
                OwnerForm.ShowReminder(null);
        }
        internal void ShowInfo() {
            if (OwnerForm == null)
                return;
            if (Grid == null) {
                OwnerForm.ShowInfo(null);
                return;
            }
            ICollection list = Grid.DataSource as ICollection;
            if (list == null)
                OwnerForm.ShowInfo(null);
            else
                OwnerForm.ShowInfo(list.Count);
        }
        internal virtual void HideModule() {
            if(AutoMergeRibbon && OwnerForm != null) {
                if(OwnerForm.Ribbon.MergedRibbon == ChildRibbon) {
                    RibbonPage page = OwnerForm.Ribbon.MergedPages.GetPageByText("VIEW");
                    if(page != null) OwnerForm.Ribbon.Pages.Add(page);
                    OwnerForm.Ribbon.UnMergeRibbon();
                }
                OwnerForm.RibbonStatusBar.UnMergeStatusBar();
                OwnerForm.ShowInfo(true);
            }
        }
        internal virtual void InitModule(IDXMenuManager manager, object data) {
            SetMenuManager(this.Controls, manager);
            if (Grid != null && Grid.MainView is ColumnView) {
                ((ColumnView)Grid.MainView).ColumnFilterChanged += new EventHandler(BaseModule_ColumnFilterChanged);
            }
            CapitalizeChildRibbonPages();
        }
        void CapitalizeChildRibbonPages() {
            if (ChildRibbon == null)
                return;
            foreach (RibbonPage page in ChildRibbon.Pages)
                page.Text = page.Text.ToUpper();
            foreach (RibbonPageCategory category in ChildRibbon.PageCategories) {
                foreach (RibbonPage page in category.Pages)
                    page.Text = page.Text.ToUpper();
            }
        }
        internal void ShowInfo(ColumnView view) {
            if (OwnerForm == null) return;
            ShowReminder();
            OwnerForm.ShowInfo(view.DataRowCount);
        }
        void BaseModule_ColumnFilterChanged(object sender, EventArgs e) {
            ShowInfo(sender as ColumnView);
        }
        void SetMenuManager(ControlCollection controlCollection, IDXMenuManager manager) {
            foreach (Control ctrl in controlCollection) {
                GridControl grid = ctrl as GridControl;
                if (grid != null) {
                    grid.MenuManager = manager;
                    break;
                }
                PivotGridControl pivot = ctrl as PivotGridControl;
                if(pivot != null) {
                    pivot.MenuManager = manager;
                    break;
                }
                BaseEdit edit = ctrl as BaseEdit;
                if (edit != null) {
                    edit.MenuManager = manager;
                    break;
                }
                SetMenuManager(ctrl.Controls, manager);
            }
        }
        RibbonControl FindRibbon(ControlCollection controls) {
            RibbonControl res = controls.OfType<Control>().FirstOrDefault(x => x is RibbonControl) as RibbonControl;
            if (res != null)
                return res;
            foreach (Control control in controls) {
                if (control.HasChildren) {
                    res = FindRibbon(control.Controls);
                    if (res != null)
                        return res;
                }
            }
            return null;
        }

        protected virtual void InitNWindData()
        {
            //string DBFileName = string.Empty;

            //DBFileName = DevExpress.Utils.FilesHelper.FindingFileName(Application.StartupPath, "demo.mdb");
            //if (DBFileName != string.Empty)
            //{
            //    InitMDBData("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DBFileName);
            //}


                InitMDBData("SERVER=localhost; DATABASE=AMR; UID=root; PASSWORD=root;");

            //string MyConString = "SERVER=localhost; DATABASE=AMR; UID=root; PASSWORD=root;";

            //mariaDbConnection = new MySqlConnection(MyConString);

            //mariaDbConnection.Open();

        }
        protected virtual void InitMDBData(string connectionString)
        {
        }
        protected virtual OleDbConnection Connection { 
            get { return connection; }
            set { connection = value; }
        }

        protected virtual MySqlConnection MariaDbConnection
        {
            get { return mariaDbConnection; }
            set { mariaDbConnection = value; }
        }
        protected virtual Modbus SerialConnection
        {
            get { return serialConnection; }
            set { serialConnection = value; }
        }
        protected virtual bool AllowZoomControl { get { return false; } }
        protected virtual void SetZoomCaption() { }
        public virtual float ZoomFactor {
            get { return 1; }
            set { }
        }
        public virtual IPrintable PrintableComponent { get { return Grid; } }
        public virtual IPrintable ExportComponent { get { return Grid; } }
        public virtual bool AllowRtfTitle { get { return true; } }
        protected virtual GridControl Grid { get { return null; } }
        protected virtual bool SaveAsEnable { get { return false; } }
        protected virtual bool SaveAttachmentEnable { get { return false; } }
        protected virtual bool SaveCalendarVisible { get { return false; } }
        protected internal virtual void ButtonClick(string tag) { }
        protected internal virtual void SendKeyDown(KeyEventArgs e) { }
        protected internal virtual RichEditControl CurrentRichEdit { get { return null; } }
        public virtual string ModuleName { get { return this.GetType().Name; } }
        public string PartName { get { return partName; } }
        [DefaultValue(false)]
        protected virtual bool AutoMergeRibbon { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        protected virtual RibbonControl ChildRibbon {
            get {
                if (!AutoMergeRibbon)
                    return null;
                return FindRibbon(Controls);
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        protected virtual RibbonStatusBar ChildRibbonStatusBar {
            get {
                if(ChildRibbon != null) return ChildRibbon.StatusBar;
                return null;
            }
        }
    }
    public class NavBarGroupTagObject {
        string name;
        Type moduleType;
        BaseModule module;
        public NavBarGroupTagObject(string name, Type moduleType) {
            this.name = name;
            this.moduleType = moduleType;
            module = null;
        }
        public string Name { get { return name; } }
        public Type ModuleType { get { return moduleType; } }
        public BaseModule Module {
            get { return module; }
            set { module = value; }
        }
    }
    public class BackstageViewLabel : LabelControl {
        public BackstageViewLabel() {
            Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            LineLocation = DevExpress.XtraEditors.LineLocation.Bottom;
            LineVisible = true;
            ShowLineShadow = false;
        }
    }
    public class ZoomManager {
        ZoomTrackBarControl zoomControl;
        int zoomFactor = 0;
        List<int> zoomValues = new List<int>() { 100, 115, 130, 150, 200, 250, 300, 350, 400, 500 };
        RibbonControl ribbon;
        BarEditItem beiZoom;
        ModulesNavigator modulesNavigator;
        public ZoomManager(RibbonControl ribbon, ModulesNavigator modulesNavigator, BarEditItem beItem) {
            this.ribbon = ribbon;
            this.modulesNavigator = modulesNavigator;
            this.beiZoom = beItem;
            this.beiZoom.HiddenEditor += new DevExpress.XtraBars.ItemClickEventHandler(this.beiZoom_HiddenEditor);
            this.beiZoom.ShownEditor += new DevExpress.XtraBars.ItemClickEventHandler(this.beiZoom_ShownEditor);
        }
        ZoomTrackBarControl ZoomControl { get { return zoomControl; } }
        public int ZoomFactor {
            get { return zoomFactor; }
            set {
                zoomFactor = value;
                beiZoom.Caption = string.Format(" {0}%", ZoomFactor);
                int index = zoomValues.IndexOf(ZoomFactor);
                if (index == -1)
                    beiZoom.EditValue = ZoomFactor / 10;
                else
                    beiZoom.EditValue = 10 + index;
                modulesNavigator.CurrentModule.ZoomFactor = (float)ZoomFactor / 100;
            }
        }
        public void SetZoomCaption(string caption) {
            beiZoom.Caption = caption;
        }
        private void beiZoom_ShownEditor(object sender, ItemClickEventArgs e) {
            this.zoomControl = ribbon.Manager.ActiveEditor as ZoomTrackBarControl;
            if (ZoomControl != null) {
                ZoomControl.ValueChanged += new EventHandler(OnZoomTackValueChanged);
                OnZoomTackValueChanged(ZoomControl, EventArgs.Empty);
            }
        }
        private void beiZoom_HiddenEditor(object sender, ItemClickEventArgs e) {
            ZoomControl.ValueChanged -= new EventHandler(OnZoomTackValueChanged);
            this.zoomControl = null;
        }
        private void OnZoomTackValueChanged(object sender, EventArgs e) {
            int val = val = ZoomControl.Value * 10;
            if (ZoomControl.Value > 10)
                val = zoomValues[ZoomControl.Value - 10];
            ZoomFactor = val;
        }
    }
    public class ObjectToolTipController : IDisposable {
        ToolTipController controller;
        Control parent;
        object editObject;
        public object EditObject { get { return editObject; } }
        public ObjectToolTipController(Control parent) {
            this.parent = parent;
            this.parent.Disposed += new EventHandler(delegate { Dispose(); });
            this.controller = new ToolTipController();
            this.controller.ToolTipType = ToolTipType.SuperTip;
            this.controller.AllowHtmlText = true;
            this.controller.ReshowDelay = controller.InitialDelay;
            this.controller.AutoPopDelay = 10000;
            parent.MouseDown += new MouseEventHandler(delegate { HideHint(false); });
            parent.MouseLeave += new EventHandler(delegate { HideHint(true); });
        }
        public void ShowHint(object editObject, Point location) {
            if (object.Equals(editObject, this.editObject))
                return;
            this.editObject = editObject;
            ToolTipControlInfo info = new ToolTipControlInfo();
            ToolTipItem item = new ToolTipItem();
            InitToolTipItem(item);
            item.ImageToTextDistance = 10;
            info.Object = DateTime.Now.Ticks;
            info.SuperTip = new SuperToolTip();
            info.SuperTip.Items.Add(item);
            info.ToolTipPosition = this.parent.PointToScreen(location);
            controller.ShowHint(info);
        }
        protected virtual void InitToolTipItem(ToolTipItem item) {
        }
        public void HideHint(bool clearCurrentObject) {
            if (clearCurrentObject)
                this.editObject = null;
            this.controller.HideHint();
        }
        #region IDisposable Members
        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                this.controller.Dispose();
            }
        }
        ~ObjectToolTipController() {
            Dispose(false);
        }
        #endregion
    }
    public class ContactToolTipController : ObjectToolTipController {
        const int MaxPhotoWidth = 120, MaxPhotoHeight = 120;
        public ContactToolTipController(Control parent) : base(parent) { }
        Contact CurrentContact { get { return EditObject as Contact; } }
        protected override void InitToolTipItem(ToolTipItem item) {
            if (CurrentContact == null)
                return;
            if (CurrentContact.Photo != null)
                item.Image = ImageCreator.CreateImage(CurrentContact.Photo, MaxPhotoWidth, MaxPhotoHeight);
            item.Text = CurrentContact.GetContactInfoHtml();
        }
    }
    public class ImageCreator {
        public static Image CreateImage(Image srcImage, int maxWidth, int maxHeight) {
            if (srcImage == null)
                return null;
            Size size = GetPhotoSize(srcImage, maxWidth, maxHeight);
            Image ret = new Bitmap(size.Width, size.Height);
            using (Graphics gr = Graphics.FromImage(ret)) {
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gr.DrawImage(srcImage, new Rectangle(0, 0, size.Width, size.Height));
            }
            return ret;
        }
        static Size GetPhotoSize(Image image, int maxWidth, int maxHeight) {
            int width = Math.Min(maxWidth, image.Width),
                height = width * image.Height / image.Width;
            if (height > maxHeight) {
                height = maxHeight;
                width = height * image.Width / image.Height;
            }
            return new Size(width, height);
        }
        public static Rectangle GetZoomDestRectangle(Rectangle r, Image img) {
            float horzRatio = Math.Min((float)r.Width / img.Width, 1);
            float vertRatio = Math.Min((float)r.Height / img.Height, 1);
            float zoomRatio = Math.Min(horzRatio, vertRatio);

            return new Rectangle(
                r.Left + (int)(r.Width - img.Width * zoomRatio) / 2,
                r.Top + (int)(r.Height - img.Height * zoomRatio) / 2,
                (int)(img.Width * zoomRatio),
                (int)(img.Height * zoomRatio));
        }
    }
}
