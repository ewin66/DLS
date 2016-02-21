using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Layout.ViewInfo;
using DevExpress.ProductsDemo.Win.Forms;
using DevExpress.MailClient.Win;
using DevExpress.MailDemo.Win;

using System.Collections;

namespace DevExpress.ProductsDemo.Win.Modules {
    public partial class AptManage : BaseModule {
        public override string ModuleName { get { return Properties.Resources.ContactsName; } }
        public AptManage()
        {
            InitializeComponent();
            EditorHelper.InitPersonComboBox(repositoryItemImageComboBox1);
            gidControlAptManage.DataSource = DataHelper.AMR_MST04s;
            gridView1.ShowFindPanel();
            InitIndex(DataHelper.AMR_MST04s);
        }
        protected override DevExpress.XtraGrid.GridControl Grid { get { return gidControlAptManage; } }
        internal override void ShowModule(bool firstShow) {
            base.ShowModule(firstShow);
            gidControlAptManage.Focus();
            UpdateActionButtons();
            if(firstShow) {
                ButtonClick(TagResources.ContactList);
                gidControlAptManage.ForceInitialize();
                GridHelper.SetFindControlImages(gidControlAptManage);
                if (DataHelper.AMR_MST04s.Count == 0) UpdateCurrentContact();
            }
        }
        void UpdateActionButtons() {
            OwnerForm.EnableLayoutButtons(gidControlAptManage.MainView != layoutView1);
            OwnerForm.EnableZoomControl(gidControlAptManage.MainView != layoutView1);
        }
        AMR_MST04 CurrentContact {
            get { return gridView1.GetRow(gridView1.FocusedRowHandle) as AMR_MST04; }
        }
        private void gridView1_ColumnFilterChanged(object sender, EventArgs e) {
            UpdateCurrentContact();
        }
        private void gridView1_FocusedRowObjectChanged(object sender, FocusedRowObjectChangedEventArgs e) {
            if(e.FocusedRowHandle == GridControl.AutoFilterRowHandle)
                gridView1.FocusedColumn = colSno;
            else if(e.FocusedRowHandle >= 0)
                gridView1.FocusedColumn = null;
            UpdateCurrentContact();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) {
        }

        void UpdateCurrentContact() {
            
            gridView1.MakeRowVisible(gridView1.FocusedRowHandle);
            OwnerForm.EnableEditContact(CurrentContact != null);
        }
        protected internal override void ButtonClick(string tag) {
            switch(tag) {
                case TagResources.ContactList:
                    UpdateMainView(gridView1);
                    ClearSortingAndGrouping();
                    break;
                case TagResources.ContactAlphabetical:
                    UpdateMainView(gridView1);
                    ClearSortingAndGrouping();
                    colSno.Group();
                    break;
                //case TagResources.ContactByState:
                //    UpdateMainView(gridView1);
                //    ClearSortingAndGrouping();
                //    colUserName.Group();
                //    colCity.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                //    break;
                case TagResources.ContactCard:
                    UpdateMainView(layoutView1);
                    break;
                case TagResources.FlipLayout:
                    layoutControl1.Root.FlipLayout();
                    break;
                case TagResources.ContactDelete:
                    if(CurrentContact == null) return;
                    int index = gridView1.FocusedRowHandle;
                    gidControlAptManage.MainView.BeginDataUpdate();
                    try
                    {
                        DataHelper.AMR_MST04s.Remove(CurrentContact);
                    }
                    finally
                    {
                        gidControlAptManage.MainView.EndDataUpdate();
                    }
                    if(index > gridView1.DataRowCount - 1) index--;
                    gridView1.FocusedRowHandle = index;
                    ShowInfo(gridView1);
                    break;
                case TagResources.ContactNew:
                    AMR_MST04 contact = new AMR_MST04();
                    if(EditUser(contact) == DialogResult.OK) {
                        gidControlAptManage.MainView.BeginDataUpdate();
                        try
                        {
                            DataHelper.AMR_MST04s.Add(contact);
                        }
                        finally
                        {
                            gidControlAptManage.MainView.EndDataUpdate();
                        }
                        ColumnView view = gidControlAptManage.MainView as ColumnView;
                        if(view != null) {
                            GridHelper.GridViewFocusObject(view, contact);
                            ShowInfo(view);
                        }
                    }
                    break;
                case TagResources.ContactEdit:
                    EditUser(CurrentContact);
                    break;
            }
            UpdateCurrentContact();
            UpdateInfo();
        }
        void UpdateMainView(ColumnView view) {
            bool isGrid = view is GridView;
            gidControlAptManage.MainView = view;
            splitterItem1.Visibility = isGrid ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            
            GridHelper.SetFindControlImages(gidControlAptManage);
            UpdateActionButtons();
        }
        private void ClearSortingAndGrouping() {
            gridView1.ClearGrouping();
            gridView1.ClearSorting();
        }
        protected override bool AllowZoomControl { get { return true; } }

        protected override void SetZoomCaption() {
            base.SetZoomCaption();
            if(ZoomFactor == 1)
                OwnerForm.ZoomManager.SetZoomCaption(Properties.Resources.Picture100Zoom);
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e) {
            if(e.Button == MouseButtons.Left && e.RowHandle >= 0 && e.Clicks == 2)
                EditUser(CurrentContact);
        }

        private void layoutView1_MouseDown(object sender, MouseEventArgs e) {
            if(e.Clicks == 2 && e.Button == MouseButtons.Left) {
                LayoutViewHitInfo info = layoutView1.CalcHitInfo(e.Location);
                if(info.InCard) {
                    AMR_MST04 current = layoutView1.GetRow(info.RowHandle) as AMR_MST04;
                    if(current != null) {
                        EditUser(current);
                        layoutView1.UpdateCurrentRow();
                    }
                }
            }
        }
        DialogResult EditUser(AMR_MST04 contact)
        {
            if(contact == null) return DialogResult.Ignore;
            DialogResult ret = DialogResult.Cancel;
            Cursor.Current = Cursors.WaitCursor;
            //using (frmEditUser frm = new frmEditUser(contact, OwnerForm.Ribbon))
            //{
            //    ret = frm.ShowDialog(OwnerForm);
            //}
            UpdateCurrentContact();
            Cursor.Current = Cursors.Default;
            return ret;
        }
        private void gridView1_KeyDown(object sender, KeyEventArgs e) {
            if(e.KeyData == Keys.Enter && gridView1.FocusedRowHandle >=0)
                EditUser(CurrentContact);
        }
        void UpdateInfo() {
            ShowInfo(gidControlAptManage.MainView as ColumnView);
        }
        private void layoutView1_ColumnFilterChanged(object sender, EventArgs e) {
            UpdateInfo();
        }

        private void repositoryItemHyperLinkEdit1_OpenLink(object sender, OpenLinkEventArgs e) {
            if(e.EditValue != null) e.EditValue = "mailto:" + e.EditValue.ToString();
        }
        protected void InitIndex(List<AMR_MST04> list)
        {
            this.extractName = (s) => {
                string name = ((AMR_MST04)s).MST04CMP;
                if(string.IsNullOrEmpty(name)) return null; //todo?
                return AlphaIndex_AptManage.Group(name.Substring(0, 1));
            };
            List<AlphaIndex_AptManage> dict = Generate(list, extractName);
            SetupGrid(dict, indexGridControl);
        }
        public void SetupGrid(List<AlphaIndex_AptManage> list, GridControl grid) {
            GridView view = grid.MainView as GridView;
            view.Columns.AddVisible("Index");
            grid.DataSource = list;
            view.FocusedRowChanged += view_FocusedRowChanged;

        }
        Timer alphaChange = null;
        void view_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e) {
            if(alphaChange != null) alphaChange.Dispose();
            alphaChange = new Timer();
            alphaChange.Interval = 200;
            alphaChange.Tick += (s, ee) => {
                gidControlAptManage.DataSource = ApplyFilter(DataHelper.AMR_MST04s, ((GridView)sender).GetFocusedRow() as AlphaIndex_AptManage);
                ((Timer)s).Dispose();
                this.alphaChange = null;
                UpdateInfo();
            };
            alphaChange.Start();
        }
        GetAlphaMehtod_AptManage extractName;
        IList ApplyFilter(List<AMR_MST04> list, AlphaIndex_AptManage alpha)
        {
            if(alpha == null || alpha == AlphaIndex_AptManage.All) return list;
            var res = from q in list
                    where alpha.Match(extractName(q))
                    select q;
            return res.ToList();

        }
        public List<AlphaIndex_AptManage> Generate(List<AMR_MST04> values, GetAlphaMehtod_AptManage extractName)
        {
            var data = from q in values
                       where extractName(q) != null
                       group q by extractName(q) into g
                       orderby g.Key
                       select new AlphaIndex_AptManage() { Index = g.Key, Count = g.Count() };
            var res = data.ToList();
            res.Insert(0, AlphaIndex_AptManage.All);
            return res;

        }



    }
    public class AlphaIndex_AptManage {
        public string Index { get; set; }
        public int Count { get; set; }
        public override string ToString() {
            return string.Format("{0}: {1}", Index, Count);
        }
        public bool Match(string text) {
            if(Group(text) == Index) return true;
            return false;
        }
        public static string Group(string text) {
            if(text.Length == 1) {
                char ch = text[0];
                if(Char.IsNumber(ch)) return "0-9";
            }
            return text.ToUpper();
        }
        static AlphaIndex_AptManage all, alphaNumber;
        public static AlphaIndex_AptManage All {
            get {
                if(all == null) all = new AlphaIndex_AptManage() { Count = 0, Index = "ALL" };
                return all;
            }
        }
        public static AlphaIndex_AptManage AlphaNumber {
            get {
                if(alphaNumber == null) alphaNumber = new AlphaIndex_AptManage() { Count = 0, Index = "0-9" };
                return alphaNumber;
            }
        }
    }
    public delegate string GetAlphaMehtod_AptManage(object target);
}
