using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.OleDb;
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
    public partial class UserManage : BaseModule {
        public override string ModuleName { get { return Properties.Resources.ContactsName; } }
        public UserManage() {
            InitializeComponent();
            //EditorHelper.InitPersonComboBox(repositoryItemImageComboBox1);
            //gidControlUserManage.DataSource = DataHelper.UserInfos;
            gridView1.ShowFindPanel();
            InitIndex(DataHelper.UserInfos);

            InitNWindData();
        }
        protected override DevExpress.XtraGrid.GridControl Grid { get { return gidControlUserManage; } }
        internal override void ShowModule(bool firstShow) {
            
            base.ShowModule(firstShow);
            gidControlUserManage.Focus();
            UpdateActionButtons();
            if(firstShow) {
                ButtonClick(TagResources.ContactCard);
                gidControlUserManage.ForceInitialize();
                GridHelper.SetFindControlImages(gidControlUserManage);
                if (DataHelper.UserInfos.Count == 0) UpdateCurrentContact();
            }

            
        }
        void UpdateActionButtons() {
            OwnerForm.EnableLayoutButtons(gidControlUserManage.MainView != layoutView1);
            OwnerForm.EnableZoomControl(gidControlUserManage.MainView != layoutView1);
        }
        UserInfo CurrentContact {
            get { return gridView1.GetRow(gridView1.FocusedRowHandle) as UserInfo; }
        }
        private void gridView1_ColumnFilterChanged(object sender, EventArgs e) {
            UpdateCurrentContact();
        }
        private void gridView1_FocusedRowObjectChanged(object sender, FocusedRowObjectChangedEventArgs e) {
            if(e.FocusedRowHandle == GridControl.AutoFilterRowHandle)
                gridView1.FocusedColumn = colUserID;
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
                    colUserID.Group();
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
                    gidControlUserManage.MainView.BeginDataUpdate();
                    try
                    {
                        DataHelper.UserInfos.Remove(CurrentContact);
                    }
                    finally
                    {
                        gidControlUserManage.MainView.EndDataUpdate();
                    }
                    if(index > gridView1.DataRowCount - 1) index--;
                    gridView1.FocusedRowHandle = index;
                    ShowInfo(gridView1);
                    break;
                case TagResources.ContactNew:
                    UserInfo contact = new UserInfo();
                    if(EditUser(contact) == DialogResult.OK) {
                        gidControlUserManage.MainView.BeginDataUpdate();
                        try
                        {
                            DataHelper.UserInfos.Add(contact);

                            string connetionString = null;
                            OleDbConnection connection ;
                            OleDbDataAdapter oledbAdapter = new OleDbDataAdapter();
                            string sql = null;
                            connetionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=demo.mdb;";
                            connection = new OleDbConnection(connetionString);
                            sql = string.Format("insert into UserInfo values('{0}', '{1}', '{2}', '{3}')", contact.Id, contact.Level, contact.Name, contact.Phone);

                            connection.Open();
                            oledbAdapter.InsertCommand = new OleDbCommand(sql, connection);
                            oledbAdapter.InsertCommand.ExecuteNonQuery();


                        }
                        finally
                        {
                            gidControlUserManage.MainView.EndDataUpdate();
                        }
                        ColumnView view = gidControlUserManage.MainView as ColumnView;
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
            gidControlUserManage.MainView = view;
            splitterItem1.Visibility = isGrid ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            
            GridHelper.SetFindControlImages(gidControlUserManage);
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
                    UserInfo current = layoutView1.GetRow(info.RowHandle) as UserInfo;
                    if(current != null) {
                        EditUser(current);
                        layoutView1.UpdateCurrentRow();
                    }
                }
            }
        }
        DialogResult EditUser(UserInfo contact)
        {
            if(contact == null) return DialogResult.Ignore;
            DialogResult ret = DialogResult.Cancel;
            Cursor.Current = Cursors.WaitCursor;
            using (frmEditUser frm = new frmEditUser(contact, OwnerForm.Ribbon))
            {
                ret = frm.ShowDialog(OwnerForm);
            }
            UpdateCurrentContact();
            Cursor.Current = Cursors.Default;
            return ret;
        }
        private void gridView1_KeyDown(object sender, KeyEventArgs e) {
            if(e.KeyData == Keys.Enter && gridView1.FocusedRowHandle >=0)
                EditUser(CurrentContact);
        }
        void UpdateInfo() {
            ShowInfo(gidControlUserManage.MainView as ColumnView);
        }
        private void layoutView1_ColumnFilterChanged(object sender, EventArgs e) {
            UpdateInfo();
        }

        private void repositoryItemHyperLinkEdit1_OpenLink(object sender, OpenLinkEventArgs e) {
            if(e.EditValue != null) e.EditValue = "mailto:" + e.EditValue.ToString();
        }
        protected void InitIndex(List<UserInfo> list)
        {
            this.extractName = (s) => {
                string name = ((UserInfo)s).Name;
                if(string.IsNullOrEmpty(name)) return null; //todo?
                return AlphaIndex.Group(name.Substring(0, 1));
            };
            List<AlphaIndex> dict = Generate(list, extractName);
            SetupGrid(dict, indexGridControl);
        }
        public void SetupGrid(List<AlphaIndex> list, GridControl grid) {
            GridView view = grid.MainView as GridView;
            view.Columns.AddVisible("Index");
            grid.DataSource = list;
            view.FocusedRowChanged += view_FocusedRowChanged;

        }
        Timer alphaChange = null;
        void view_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e) {
            if (alphaChange != null) alphaChange.Dispose();
            alphaChange = new Timer();
            alphaChange.Interval = 200;
            alphaChange.Tick += (s, ee) =>
            {
                gidControlUserManage.DataSource = ApplyFilter(DataHelper.UserInfos, ((GridView)sender).GetFocusedRow() as AlphaIndex);
                ((Timer)s).Dispose();
                this.alphaChange = null;
                UpdateInfo();
            };
            alphaChange.Start();
        }
        GetAlphaMehtod extractName;
        IList ApplyFilter(List<UserInfo> list, AlphaIndex alpha)
        {
            if(alpha == null || alpha == AlphaIndex.All) return list;
            var res = from q in list
                    where alpha.Match(extractName(q))
                    select q;
            return res.ToList();

        }
        public List<AlphaIndex> Generate(List<UserInfo> values, GetAlphaMehtod extractName)
        {
            var data = from q in values
                       where extractName(q) != null
                       group q by extractName(q) into g
                       orderby g.Key
                       select new AlphaIndex() { Index = g.Key, Count = g.Count() };
            var res = data.ToList();
            res.Insert(0, AlphaIndex.All);
            return res;

        }

        private void UserManage_Load(object sender, EventArgs e)
        {
            
        }
        string tblGrid = "[UserInfo]";
        
        protected override void InitMDBData(string connectionString)
        {
            DataSet ds = new DataSet();
            System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter("SELECT UserID, Grade, UserName, Phone FROM " + tblGrid, connectionString);
            oleDbDataAdapter.Fill(ds, tblGrid);
            //oleDbDataAdapter = new System.Data.OleDb.OleDbDataAdapter("SELECT * FROM " + tblLookUp, connectionString);
            //oleDbDataAdapter.Fill(ds, tblLookUp);

            
            foreach( DataRow row in ds.Tables[tblGrid].Rows)
            {
                UserInfo contact = new UserInfo();

                contact.Id = row["UserID"].ToString();
                contact.Level = UserLevel.Level1;
                contact.Name = row["UserName"].ToString();
                contact.Phone = row["Phone"].ToString();

                DataHelper.UserInfos.Add(contact);
            }
            
            gidControlUserManage.DataSource = DataHelper.UserInfos;
            //gidControlUserManage.DataSource = ds.Tables[tblGrid];
            //repositoryItemLookUpEdit1.DataSource = ds.Tables[tblLookUp];
                    //gridControl1.DataSource = ((NavBarData)item.Tag).Data;
            //gridControl1.DataSource as List<Task>)

        }


    }
    public class AlphaIndex {
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
        static AlphaIndex all, alphaNumber;
        public static AlphaIndex All {
            get {
                if(all == null) all = new AlphaIndex() { Count = 0, Index = "ALL" };
                return all;
            }
        }
        public static AlphaIndex AlphaNumber {
            get {
                if(alphaNumber == null) alphaNumber = new AlphaIndex() { Count = 0, Index = "0-9" };
                return alphaNumber;
            }
        }
    }
    public delegate string GetAlphaMehtod(object target);
}
