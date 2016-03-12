using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraGrid.Columns;

namespace DevExpress.ProductsDemo.Win.Controls {
    /// <summary>
    /// Summary description for GridRealTime.
    /// </summary>
    public partial class GridRealTime : BaseModule {
        public GridRealTime() {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            gridControl1.DataSource = CreateData();
            SetCaption();
            gridView1.Columns[0].FilterInfo = new ColumnFilterInfo(ColumnFilterType.Custom, null, "[Column0] >= 0");
        }

        bool processing = false;
        public const int ColumnCount = 10, RowCount = 40;
        Random rnd = new Random();
        int count = 0;

        private void timer1_Tick(object sender, System.EventArgs e) {
            if(processing) return;
            processing = true;
            try {
                for(int n = 0; n < 50; n++)
                    SetRandomValue();
            }
            finally {
                processing = false;
            }
        }

        public IList CreateData() {
            Random rnd = new Random(RowCount);
            RecordCollection coll = new RecordCollection();
            for(int n = 0; n < RowCount; n++) {
                Record row = new Record();
                coll.Add(row);
            }
            return coll;
        }


        void SetRandomValue() {
            //<label1>
            int c = rnd.Next(ColumnCount), r = rnd.Next(RowCount);
            SetValue(gridControl1.DataSource, r, c, rnd.Next(200) - 100);
            //</label1>
            if((++count % 500) == 0)
                label1.Text = string.Format("Update Count = {0}", count);
        }
        //<label1>
        void SetValue(object data, int row, int column, object val) {
            RecordCollection rc = data as RecordCollection;
            rc.SetValue(row, column, val);
        }
        //</label1>

        private void simpleButton1_Click(object sender, System.EventArgs e) {
            timer1.Enabled = !timer1.Enabled;
            SetCaption();
        }

        void SetCaption() {
            simpleButton1.Text = timer1.Enabled ? "Stop timer" : "Start timer";
        }
    }
    #region record
    public class RecordCollection : CollectionBase, IBindingList, ITypedList {
        public Record this[int i] { get { return (Record)List[i]; } }
        public void Add(Record record) {
            int res = List.Add(record);
            record.owner = this;
            record.Index = res;
        }
        public void SetValue(int row, int col, object val) {
            this[row].SetValue(col, val);
        }
        internal void OnListChanged(Record rec) {
            if(listChangedHandler != null) listChangedHandler(this, new ListChangedEventArgs(ListChangedType.ItemChanged, rec.Index, rec.Index));
        }

        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] accessors) {
            PropertyDescriptorCollection coll = TypeDescriptor.GetProperties(typeof(Record));
            ArrayList list = new ArrayList(coll);
            list.Sort(new PDComparer());
            PropertyDescriptorCollection res = new PropertyDescriptorCollection(null);
            for(int n = 0; n < GridRealTime.ColumnCount; n++) {
                res.Add(list[n] as PropertyDescriptor);
            }
            return res;
        }
        class PDComparer : IComparer {
            int IComparer.Compare(object a, object b) {
                return Comparer.Default.Compare(GetName(a), GetName(b));
            }
            int GetName(object a) {
                PropertyDescriptor pd = (PropertyDescriptor)a;
                if(pd.Name.StartsWith("Column")) return Convert.ToInt32(pd.Name.Substring(6));
                return -1;

            }
        }
        string ITypedList.GetListName(PropertyDescriptor[] accessors) { return ""; }
        public object AddNew() { return null; }
        public bool AllowEdit { get { return true; } }
        public bool AllowNew { get { return false; } }
        public bool AllowRemove { get { return false; } }

        private ListChangedEventHandler listChangedHandler;
        public event ListChangedEventHandler ListChanged {
            add { listChangedHandler += value; }
            remove { listChangedHandler -= value; }
        }
        public void AddIndex(PropertyDescriptor pd) { throw new NotSupportedException(); }
        public void ApplySort(PropertyDescriptor pd, ListSortDirection dir) { throw new NotSupportedException(); }
        public int Find(PropertyDescriptor property, object key) { throw new NotSupportedException(); }
        public bool IsSorted { get { return false; } }
        public void RemoveIndex(PropertyDescriptor pd) { throw new NotSupportedException(); }
        public void RemoveSort() { throw new NotSupportedException(); }
        public ListSortDirection SortDirection { get { throw new NotSupportedException(); } }
        public PropertyDescriptor SortProperty { get { throw new NotSupportedException(); } }
        public bool SupportsChangeNotification { get { return true; } }
        public bool SupportsSearching { get { return false; } }
        public bool SupportsSorting { get { return false; } }

    }
    public class Record {
        internal int Index = -1;
        internal RecordCollection owner;
        int[] values = new int[20];
        public int Column0 { get { return values[0]; } set { SetValue(0, value); } }
        public int Column1 { get { return values[1]; } set { SetValue(1, value); } }
        public int Column2 { get { return values[2]; } set { SetValue(2, value); } }
        public int Column3 { get { return values[3]; } set { SetValue(3, value); } }
        public int Column4 { get { return values[4]; } set { SetValue(4, value); } }
        public int Column5 { get { return values[5]; } set { SetValue(5, value); } }
        public int Column6 { get { return values[6]; } set { SetValue(6, value); } }
        public int Column7 { get { return values[7]; } set { SetValue(7, value); } }
        public int Column8 { get { return values[8]; } set { SetValue(8, value); } }
        public int Column9 { get { return values[9]; } set { SetValue(9, value); } }
        public int Column10 { get { return values[10]; } set { SetValue(10, value); } }
        public int Column11 { get { return values[11]; } set { SetValue(11, value); } }
        public int Column12 { get { return values[12]; } set { SetValue(12, value); } }
        public int Column13 { get { return values[13]; } set { SetValue(13, value); } }
        public int Column14 { get { return values[14]; } set { SetValue(14, value); } }
        public int Column15 { get { return values[15]; } set { SetValue(15, value); } }
        public int Column16 { get { return values[16]; } set { SetValue(16, value); } }
        public int Column17 { get { return values[17]; } set { SetValue(17, value); } }
        public int Column18 { get { return values[18]; } set { SetValue(18, value); } }
        public int Column19 { get { return values[19]; } set { SetValue(19, value); } }
        public int GetValue(int index) { return values[index]; }
        //<label1>
        public void SetValue(int index, object val) {
            values[index] = (int)val;
            if(this.owner != null) this.owner.OnListChanged(this);
        }
        //</label1>
    }
    #endregion

}
