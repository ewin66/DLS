using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DevExpress.Utils;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using DevExpress.XtraExport;
using System.Xml;
using System.ServiceModel.Syndication;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors;
using System.ComponentModel;
using System.Collections;
using DevExpress.MailDemo.Win;
using DevExpress.CodeParser.CSharp;
using DevExpress.ProductsDemo.Win;

namespace DevExpress.MailClient.Win {
    public class Task : IDXDataErrorInfo {
        int priority = 1;
        int percentComplete = 0;
        DateTime createdDate;
        DateTime? startDate = null, dueDate = null, completedDate = null;
        string subject, description;
        TaskStatus status = TaskStatus.NotStarted;
        TaskCategory category;
        Contact assignTo = null;
        UserInfo assignTo2 = null;
        public Task(string subject, TaskCategory category)
            : this(subject, category, DateTime.Now) {
        }
        internal Task(string subject, TaskCategory category, DateTime date) {
            this.subject = subject;
            this.category = category;
            this.createdDate = date;
        }
        public int Priority { get { return priority; } set { priority = value; } }
        public int PercentComplete {
            get { return percentComplete; }
            set {
                if (value < 0)
                    value = 0;
                if (value > 100)
                    value = 100;
                if (percentComplete == value)
                    return;
                percentComplete = value;
                if (percentComplete == 100)
                    Status = TaskStatus.Completed;
                if (percentComplete > 0 && percentComplete < 100)
                    Status = TaskStatus.InProgress;
            }
        }
        public DateTime CreatedDate { get { return createdDate; } }
        public DateTime? StartDate { get { return startDate; } set { startDate = value; } }
        public DateTime? DueDate { get { return dueDate; } set { dueDate = value; } }
        public DateTime? CompletedDate { get { return completedDate; } set { completedDate = value; } }
        public string Subject { get { return subject; } set { subject = value; } }
        public string Description { get { return description; } set { description = value; } }
        public TaskCategory Category { get { return category; } set { category = value; } }
        public TaskStatus Status {
            get { return status; }
            set {
                status = value;
                if (status == TaskStatus.Completed) {
                    PercentComplete = 100;
                    CompletedDate = DateTime.Now;
                } else
                    CompletedDate = null;
                if (status == TaskStatus.NotStarted)
                    PercentComplete = 0;
                if (status == TaskStatus.InProgress && PercentComplete == 100)
                    PercentComplete = 75;
                if (status == TaskStatus.Deferred || status == TaskStatus.WaitingOnSomeoneElse)
                    DueDate = null;
            }
        }
        public Contact AssignTo { get { return assignTo; } set { assignTo = value; } }
        public UserInfo AssignTo2 { get { return assignTo2; } set { assignTo2 = value; } }
        internal TimeSpan TimeDiff { get { return (DateTime.Now - CreatedDate); } }
        public bool Overdue {
            get {
                if (Status == TaskStatus.Completed || !DueDate.HasValue)
                    return false;
                DateTime dDate = DueDate.Value.Date.AddDays(1);
                if (DateTime.Now >= dDate)
                    return true;
                return false;
            }
        }
        public bool Complete {
            get { return Status == TaskStatus.Completed; }
            set {
                if (value)
                    Status = TaskStatus.Completed;
                else
                    Status = TaskStatus.NotStarted;
            }
        }
        public int Icon { get { return Complete ? 0 : 1; } }
        public FlagStatus FlagStatus {
            get {
                DateTime today = DateTime.Today;
                if (Complete)
                    return FlagStatus.Completed;
                if (!DueDate.HasValue)
                    return FlagStatus.NoDate;
                if (DueDate.Value.Date.Equals(today))
                    return FlagStatus.Today;
                if (DueDate.Value.Date.Equals(today.AddDays(1)))
                    return FlagStatus.Tomorrow;
                DateTime thisWeekStart = DevExpress.Data.Filtering.Helpers.EvalHelpers.GetWeekStart(today);
                if (DueDate.Value.Date >= thisWeekStart && DueDate.Value.Date < thisWeekStart.AddDays(7))
                    return FlagStatus.ThisWeek;
                if (DueDate.Value.Date >= thisWeekStart.AddDays(7) && DueDate.Value.Date < thisWeekStart.AddDays(14))
                    return FlagStatus.NextWeek;
                return FlagStatus.Custom;
            }
        }
        public void Assign(Task task) {
            this.subject = task.Subject;
            this.priority = task.Priority;
            this.percentComplete = task.PercentComplete;
            this.createdDate = task.CreatedDate;
            this.startDate = task.StartDate;
            this.dueDate = task.DueDate;
            this.completedDate = task.CompletedDate;
            this.description = task.Description;
            this.category = task.Category;
            this.status = task.Status;
            this.assignTo = task.AssignTo;
        }
        public Task Clone() {
            Task task = new Task(this.Subject, this.Category);
            task.Assign(this);
            return task;
        }
        public string DueIn {
            get {
                if(DueDate.HasValue) {
                    int oDays = (DateTime.Today - DueDate.Value).Days;
                    return oDays > 0 ? string.Format("{0} day{1} overdue", oDays, oDays > 1 ? "s" : string.Empty) : string.Empty;
                }
                return string.Empty;
            }
        }
        #region IDXDataErrorInfo Members
        public void GetError(DevExpress.XtraEditors.DXErrorProvider.ErrorInfo info) { }

        public void GetPropertyError(string propertyName, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo info) {
            if (propertyName == "DueDate") {
                if ((DueDate.HasValue && StartDate.HasValue) && DueDate < StartDate)
                    SetErrorInfo(info, DevExpress.ProductsDemo.Win.Properties.Resources.DueDateError, ErrorType.Critical);
                if (!DueDate.HasValue && Status == TaskStatus.InProgress)
                    SetErrorInfo(info, DevExpress.ProductsDemo.Win.Properties.Resources.DueDateWarning, ErrorType.Warning);
            }
        }
        void SetErrorInfo(DevExpress.XtraEditors.DXErrorProvider.ErrorInfo info, string errorText, ErrorType errorType) {
            info.ErrorText = errorText;
            info.ErrorType = errorType;
        }
        #endregion
    }
    public class Contact : IComparable {
        DataRow customer, person;
        Image photo;
        FullName name;
        string email, phone, note;
        ContactGender gender;
        DateTime? birthDate;
        Address address;
        bool hasPhoto = false;
        public Contact() {
            name = new FullName(DevExpress.ProductsDemo.Win.Properties.Resources.NewFirstName, string.Empty, DevExpress.ProductsDemo.Win.Properties.Resources.NewLastName);
            address = new Address();
        }
        public Contact(Contact contact) {
            name = new FullName();
            address = new Address();
            this.Assign(contact);
        }
        public Contact(DataRow customer, DataRow person) {
            this.customer = customer;
            this.person = person;
            if (!(customer["Photo"] is DBNull)) {
                photo = ByteImageConverter.FromByteArray((byte[])customer["Photo"]);
                hasPhoto = true;
            } else
                photo = global::DevExpress.ProductsDemo.Win.Properties.Resources.Unknown_user;
            name = new FullName(string.Format("{0}", person["FirstName"]), string.Format("{0}", customer["MiddleName"]), string.Format("{0}", person["LastName"]));
            email = string.Format("{0}", customer["Email"]).Replace("dxvideorent.com", "dxmail.net");
            gender = (ContactGender)person["Gender"];
            birthDate = (DateTime?)person["BirthDate"];
            phone = string.Format("{0}", customer["Phone"]);
            address = new Address(string.Format("{0}", customer["Address"]));
        }
        public string Name { get { return name.ToString(); } }
        public string FirstName { get { return name.FirstName; } }
        public string MiddleName { get { return name.MiddleName; } }
        public string LastName { get { return name.LastName; } }
        public string Email { get { return email; } set { email = value; } }
        public ContactGender Gender { get { return gender; } set { gender = value; } }
        public DateTime? BirthDate { get { return birthDate; } }
        public DateTime BindingBirthDate {
            get {
                if (BirthDate.HasValue)
                    return BirthDate.Value;
                return DateTime.MinValue;
            }
            set {
                birthDate = value;
            }
        }
        public string Phone { get { return phone; } set { phone = value; } }
        public string State { get { return address.State; } }
        public string City { get { return address.City; } }
        public string Zip { get { return address.Zip; } }
        public string AddressLine { get { return address.AddressLine; } }
        public Address Address { get { return address; } }
        public FullName FullName { get { return name; } }
        public Image Photo { get { return photo; } set { photo = value; } }
        public string Note { get { return note; } set { note = value; } }
        public string GetContactInfoHtml() {
            string ret = string.Format("<size=+2><b>{0}</b><size=-2>", Name);
            ret += "<br>";
            if (BirthDate != null && BirthDate != DateTime.MinValue)
                ret += string.Format(DevExpress.ProductsDemo.Win.Properties.Resources.BirthDateHtml, BirthDate);
            if (!string.IsNullOrEmpty(Email))
                ret += string.Format(DevExpress.ProductsDemo.Win.Properties.Resources.EmailHtml, Email);
            if (!string.IsNullOrEmpty(Phone))
                ret += string.Format(DevExpress.ProductsDemo.Win.Properties.Resources.PhoneHtml, Phone);
            ret += string.Format(DevExpress.ProductsDemo.Win.Properties.Resources.AddressHtml, Address);

            return ret;
        }
        public override string ToString() { return Name; }
        public Image Icon {
            get {
                ContactTitle title = name.Title;
                if (title == ContactTitle.None && gender == ContactGender.Female)
                    title = ContactTitle.Mrs;
                switch (title) {
                    case ContactTitle.Dr:
                        return global::DevExpress.ProductsDemo.Win.Properties.Resources.Doctor;
                    case ContactTitle.Miss:
                        return global::DevExpress.ProductsDemo.Win.Properties.Resources.Miss;
                    case ContactTitle.Mrs:
                        return global::DevExpress.ProductsDemo.Win.Properties.Resources.Mrs;
                    case ContactTitle.Ms:
                        return global::DevExpress.ProductsDemo.Win.Properties.Resources.Ms;
                    case ContactTitle.Prof:
                        return global::DevExpress.ProductsDemo.Win.Properties.Resources.Professor;
                }
                return global::DevExpress.ProductsDemo.Win.Properties.Resources.Mr;
            }
        }
        internal bool HasPhoto { get { return hasPhoto; } }
        public void Assign(Contact contact) {
            this.photo = contact.Photo;
            this.name.Assign(contact.FullName);
            this.address.Assign(contact.Address);
            this.email = contact.Email;
            this.gender = contact.Gender;
            this.birthDate = contact.BirthDate;
            this.phone = contact.Phone;
            this.note = contact.Note;
        }
        public Contact Clone() {
            return new Contact(this);
        }
        #region IComparable Members

        public int CompareTo(object obj) {
            return Comparer<string>.Default.Compare(Name, obj.ToString());
        }

        #endregion
    }

    /// <summary>
    /// 세대 정보
    /// </summary>
    public class AMR_MST04 : IComparable
    {
        UInt32 mst04sno;
        string mst04cmp;
        string mst04don;
        UInt32 mst04flr;
        string mst04hno;
        string mst04nam;
        string mst04phn;

        public UInt32 MST04SNO { get { return mst04sno; } set { mst04sno = value; } }
        public string MST04CMP { get { return mst04cmp; } set { mst04cmp = value; } }
        public string MST04DON { get { return mst04don; } set { mst04don = value; } }
        public UInt32 MST04FLR { get { return mst04flr; } set { mst04flr = value; } }
        public string MST04HNO { get { return mst04hno; } set { mst04hno = value; } }
        public string MST04NAM { get { return mst04nam; } set { mst04nam = value; } }
        public string MST04PHN { get { return mst04phn; } set { mst04phn = value; } }
        public AMR_MST04()
        {
        }
                
        public AMR_MST04(AMR_MST04 info)
        {
            this.Assign(info);
        }
                
        public void Assign(AMR_MST04 info)
        {
            this.MST04SNO = info.MST04SNO;
            this.MST04CMP = info.MST04CMP;
            this.MST04DON = info.MST04DON;
            this.MST04FLR = info.MST04FLR;
            this.MST04HNO = info.MST04HNO;
            this.MST04NAM = info.MST04NAM;
            this.MST04PHN = info.MST04PHN;

        }
        #region IComparable Members

        public int CompareTo(object obj)
        {
            return Comparer<string>.Default.Compare(MST04CMP, obj.ToString());
        }

        #endregion
    }

    public class UserInfo : IComparable
    {
        
        
        DataRow customer, person;
        Image photo;
        
        string id, password, confirmPassword, name, phone, note;
        UserLevel level;
        
        bool hasPhoto = false;
        public UserInfo()
        {
            
            
        }
        public UserInfo(UserInfo userinfo)
        {
            this.Assign(userinfo);
        }
        public UserInfo(DataRow customer, DataRow person)
        {
            this.customer = customer;
            this.person = person;
            if (!(customer["Photo"] is DBNull))
            {
                photo = ByteImageConverter.FromByteArray((byte[])customer["Photo"]);
                hasPhoto = true;
            }
            else
                photo = global::DevExpress.ProductsDemo.Win.Properties.Resources.Unknown_user;
            
            
            
            
            name = string.Format("{0}", person["Name"]);
            
            phone = string.Format("{0}", customer["Phone"]);
            
        }

        
        public Image Photo { get { return photo; } set { photo = value; } }
        public string Id { get { return id; } set { id = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string ConfirmPassword { get { return confirmPassword; } set { confirmPassword = value; } }
        public UserLevel Level { get { return level; } set { level = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Phone { get { return phone; } set { phone = value; } }
        public string Note { get { return note; } set { note = value; } }
        
        public override string ToString() { return Name; }

        public Image Icon
        {
            get
            {
                
                //if (level == UserLevel.None)
                //    level = UserLevel.Level1;
                switch (level)
                {
                    case UserLevel.Level1:
                        return global::DevExpress.ProductsDemo.Win.Properties.Resources.Doctor;
                    case UserLevel.Level2:
                        return global::DevExpress.ProductsDemo.Win.Properties.Resources.Miss;
                    case UserLevel.Level3:
                        return global::DevExpress.ProductsDemo.Win.Properties.Resources.Mrs;

                }
                return global::DevExpress.ProductsDemo.Win.Properties.Resources.Mr;
            }
        }
        internal bool HasPhoto { get { return hasPhoto; } }
        public void Assign(UserInfo userinfo)
        {
            this.id = userinfo.id;
            this.password = userinfo.password;
            this.confirmPassword = userinfo.confirmPassword;
            this.level = userinfo.level;
            this.name = userinfo.name;
            this.phone = userinfo.Phone;
            this.note = userinfo.Note;
        }
        public UserInfo Clone()
        {
            return new UserInfo(this);
        }
        #region IComparable Members

        public int CompareTo(object obj)
        {
            return Comparer<string>.Default.Compare(Name, obj.ToString());
        }

        #endregion
    }
    public class User
    {
        UserLevel level;
        string name;
        public User() : this(string.Empty) { }
        public User(string name) : this(UserLevel.Level1, name) { }
        public User(UserLevel level, string name)
        {
            this.level = level;
            this.name = name;
   
        }
        public UserLevel Level { get { return level; } set { level = value; } }
        public string Name { get { return name; } set { name = value; } }

        public override string ToString()
        {
            return string.Format("{0}{1}", GetFormatString(EditorHelper.GetLevelByUserLevel(level)),
                GetFormatString(Name));
        }
        string GetFormatString(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            return string.Format("{0} ", name);
        }
        public void Assign(User user)
        {
            this.level = user.Level;
            this.name = user.Name;
            
        }
    }
    public class FullName {
        ContactTitle title;
        string first, middle, last;
        public FullName() : this(string.Empty, string.Empty, string.Empty) { }
        public FullName(string first, string middle, string last) : this(ContactTitle.None, first, middle, last) { }
        public FullName(ContactTitle title, string first, string middle, string last) {
            this.title = title;
            this.first = first;
            this.middle = middle;
            this.last = last;
        }
        public ContactTitle Title { get { return title; } set { title = value; } }
        public string FirstName { get { return first; } set { first = value; } }
        public string MiddleName { get { return middle; } set { middle = value; } }
        public string LastName { get { return last; } set { last = value; } }
        public override string ToString() {
            return string.Format("{0}{1}{2}{3}", GetFormatString(EditorHelper.GetTitleNameByContactTitle(Title)),
                GetFormatString(FirstName), GetFormatString(MiddleName), LastName);
        }
        string GetFormatString(string name) {
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            return string.Format("{0} ", name);
        }
        public void Assign(FullName name) {
            this.title = name.Title;
            this.first = name.FirstName;
            this.middle = name.MiddleName;
            this.last = name.LastName;
        }
    }
    public class Address {
        string address, city = string.Empty, state = string.Empty, zip;
        public Address() : this(string.Empty) { }
        public Address(string address, string city, string state, string zip) {
            this.address = address;
            this.city = city;
            this.state = state;
            this.zip = zip;
        }
        internal Address(string addressString) {
            if (string.IsNullOrEmpty(addressString))
                return;
            try {
                string[] lines = addressString.Split(',');
                this.address = lines[0].Trim();
                this.city = lines[1].Trim();
                this.state = lines[2].Trim().Substring(0, 2);
                string temp = lines[2].Trim();
                this.zip = temp.Substring(3, temp.Length - 3);
            } catch { }
        }
        public string AddressLine { get { return address; } set { address = value; } }
        public string State { get { return state; } set { state = value; } }
        public string City { get { return city; } set { city = value; } }
        public string Zip { get { return zip; } set { zip = value; } }
        public override string ToString() {
            return string.Format("{0}{1}{2}{3}", GetFormatString(AddressLine), GetFormatString(City), GetFormatString(State), Zip);
        }
        string GetFormatString(string name) {
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            return string.Format("{0}, ", name);
        }
        public void Assign(Address address) {
            this.address = address.AddressLine;
            this.state = address.State;
            this.city = address.City;
            this.zip = address.Zip;
        }
    }
    public class DataHelper {

        static List<AMR_MST04> amr_mst04s = null;
        static List<Contact> contacts = null;
        static List<UserInfo> userinfos = null;
        static List<Task> tasks = null;
        internal static string[] ApplicationArguments;
        static DataTable calendarResourcesTable;
        static DataTable calendarAppointmentsTable;

        public static List<Contact> Contacts {
            get {
                if (contacts == null)
                    contacts = GetContacts();
                return contacts;
            }
        }
        public static List<UserInfo> UserInfos
        {
            get
            {
                if (userinfos == null)
                    userinfos = GetUserInfos();
                return userinfos;
            }
        }
        public static List<AMR_MST04> AMR_MST04s
        {
            get
            {
                if (amr_mst04s == null)
                    amr_mst04s = new List<AMR_MST04>();
                return amr_mst04s;
            }
        }
        public static List<Task> Tasks {
            get {
                if (tasks == null)
                    tasks = GenerateTasks();
                return tasks;
            }
        }
        internal static DataTable CalendarResources {
            get {
                if (calendarResourcesTable == null) {
                    string table = "Resources";
                    calendarResourcesTable = CreateDataTable(table);
                }
                return calendarResourcesTable;
            }
        }
        internal static DataTable CalendarAppointments {
            get {
                if (calendarAppointmentsTable == null) {
                    string table = "Appointments";
                    calendarAppointmentsTable = CreateDataTable(table);
                }
                return calendarAppointmentsTable;
            }
        }


        static List<Task> GenerateTasks() {
            List<Task> ret = new List<Task>();
            for (int i = 0; i < TaskGenerator.CustomerCount; i++)
                foreach (string s in CollectionResources.OfficeTasks)
                    ret.Add(TaskGenerator.CreateTask(s, TaskCategory.Office));
            foreach (string s in CollectionResources.HouseTasks)
                ret.Add(TaskGenerator.CreateTask(s, TaskCategory.HouseChores));
            foreach (string s in CollectionResources.ShoppingTasks)
                ret.Add(TaskGenerator.CreateTask(s, TaskCategory.Shopping));
            return ret;
        }
        internal static List<Contact> GetContacts() {
            List<Contact> ret = new List<Contact>();
            DataSet temp = new DataSet();
            string dbName = FilesHelper.FindingFileName(Application.StartupPath, "Data\\VideoRent.xml", false);
            if (string.IsNullOrEmpty(dbName))
                return ret;
            temp.ReadXml(dbName);
            DataTable tbl = temp.Relations["FK_CustomerOidOidPerson"].ChildTable;
            for (int i = 0; i < tbl.Rows.Count; i++)
                ret.Add(new Contact(tbl.Rows[i], tbl.Rows[i].GetParentRow("FK_CustomerOidOidPerson")));
            return ret;
        }
        internal static List<UserInfo> GetUserInfos()
        {
            List<UserInfo> ret = new List<UserInfo>();
            DataSet temp = new DataSet();
            string dbName = FilesHelper.FindingFileName(Application.StartupPath, "Data\\VideoRent.xml", false);
            if (string.IsNullOrEmpty(dbName))
                return ret;
            temp.ReadXml(dbName);
            DataTable tbl = temp.Relations["FK_CustomerOidOidPerson"].ChildTable;
            for (int i = 0; i < tbl.Rows.Count; i++)
                ret.Add(new UserInfo(tbl.Rows[i], tbl.Rows[i].GetParentRow("FK_CustomerOidOidPerson")));
            return ret;
        }
        private static DataTable CreateDataTable(string table) {
            DataSet dataSet = new DataSet();
            string dataFile = FilesHelper.FindingFileName(Application.StartupPath, "Data\\Mail.xml");
            if (dataFile != string.Empty) {
                FileInfo fi = new FileInfo(dataFile);
                dataSet.ReadXml(fi.FullName);
                return dataSet.Tables[table];
            }
            return null;
        }
    }

    internal class TaskGenerator
    {
        public static int CustomerCount = 10;
        static Random rndGenerator = new Random();
        static List<UserInfo> customers;
        internal static List<UserInfo> Customers
        {
            get
            {
                if (customers == null)
                {
                    customers = new List<UserInfo>();
                    List<UserInfo> temp = DataHelper.GetUserInfos();
                    if (temp.Count > CustomerCount)
                    {
                        while (customers.Count < CustomerCount)
                        {
                            UserInfo contact = GetCustomer(rndGenerator.Next(temp.Count - 1), customers, temp);
                            if (contact != null)
                                customers.Add(contact);
                        }
                    }
                }
                return customers;
            }
        }
        static UserInfo GetCustomer(int index, List<UserInfo> customers, List<UserInfo> contacts)
        {
            UserInfo contact = contacts[index];
            if (!contact.HasPhoto)
                return null;
            foreach (UserInfo c in customers)
                if (ReferenceEquals(c, contact))
                    return null;
            return contact;
        }
        public static Task CreateTask(string subject, TaskCategory category)
        {
            Task task = new Task(subject, category, DateTime.Now.AddHours(-rndGenerator.Next(96)));
            int rndStatus = rndGenerator.Next(10);
            if (task.TimeDiff.TotalHours > 12)
            {
                if (task.TimeDiff.TotalHours > 80)
                {
                    task.Status = TaskStatus.Completed;

                }
                else
                {
                    task.Status = TaskStatus.InProgress;
                    task.PercentComplete = rndGenerator.Next(9) * 10;
                }
                task.StartDate = task.CreatedDate.AddMinutes(rndGenerator.Next(720)).Date;
            }
            if (rndStatus != 5)
                task.DueDate = task.CreatedDate.AddHours((90 - rndStatus * 9) + 24).Date;
            if (rndStatus > 8)
                task.Priority = 2;
            if (rndStatus < 3)
                task.Priority = 0;
            if (rndStatus == 6 && task.Status == TaskStatus.InProgress)
                task.Status = TaskStatus.Deferred;
            if (rndStatus == 4 && task.Status == TaskStatus.InProgress && task.PercentComplete < 40)
                task.Status = TaskStatus.WaitingOnSomeoneElse;
            if (task.Category == TaskCategory.Office && rndStatus != 7 && Customers.Count > 0)
                task.AssignTo2 = Customers[rndGenerator.Next(Customers.Count)];
            if (task.Status == TaskStatus.Completed)
            {
                if (!task.StartDate.HasValue)
                    task.StartDate = task.CreatedDate.AddHours(12).Date;
                task.CompletedDate = task.StartDate.Value.AddHours(rndGenerator.Next(48) + 24);
            }
            return task;
        }

    }
    //internal class TaskGenerator {
    //    public static int CustomerCount = 10;
    //    static Random rndGenerator = new Random();
    //    static List<Contact> customers;
    //    internal static List<Contact> Customers {
    //        get {
    //            if (customers == null) {
    //                customers = new List<Contact>();
    //                List<Contact> temp = DataHelper.GetContacts();
    //                if (temp.Count > CustomerCount) {
    //                    while (customers.Count < CustomerCount) {
    //                        Contact contact = GetCustomer(rndGenerator.Next(temp.Count - 1), customers, temp);
    //                        if (contact != null)
    //                            customers.Add(contact);
    //                    }
    //                }
    //            }
    //            return customers;
    //        }
    //    }
    //    static Contact GetCustomer(int index, List<Contact> customers, List<Contact> contacts) {
    //        Contact contact = contacts[index];
    //        if (!contact.HasPhoto)
    //            return null;
    //        foreach (Contact c in customers)
    //            if (ReferenceEquals(c, contact))
    //                return null;
    //        return contact;
    //    }
    //    public static Task CreateTask(string subject, TaskCategory category) {
    //        Task task = new Task(subject, category, DateTime.Now.AddHours(-rndGenerator.Next(96)));
    //        int rndStatus = rndGenerator.Next(10);
    //        if (task.TimeDiff.TotalHours > 12) {
    //            if (task.TimeDiff.TotalHours > 80) {
    //                task.Status = TaskStatus.Completed;

    //            } else {
    //                task.Status = TaskStatus.InProgress;
    //                task.PercentComplete = rndGenerator.Next(9) * 10;
    //            }
    //            task.StartDate = task.CreatedDate.AddMinutes(rndGenerator.Next(720)).Date;
    //        }
    //        if (rndStatus != 5)
    //            task.DueDate = task.CreatedDate.AddHours((90 - rndStatus * 9) + 24).Date;
    //        if (rndStatus > 8)
    //            task.Priority = 2;
    //        if (rndStatus < 3)
    //            task.Priority = 0;
    //        if (rndStatus == 6 && task.Status == TaskStatus.InProgress)
    //            task.Status = TaskStatus.Deferred;
    //        if (rndStatus == 4 && task.Status == TaskStatus.InProgress && task.PercentComplete < 40)
    //            task.Status = TaskStatus.WaitingOnSomeoneElse;
    //        if (task.Category == TaskCategory.Office && rndStatus != 7 && Customers.Count > 0)
    //            task.AssignTo = Customers[rndGenerator.Next(Customers.Count)];
    //        if (task.Status == TaskStatus.Completed) {
    //            if (!task.StartDate.HasValue)
    //                task.StartDate = task.CreatedDate.AddHours(12).Date;
    //            task.CompletedDate = task.StartDate.Value.AddHours(rndGenerator.Next(48) + 24);
    //        }
    //        return task;
    //    }
        
    //}
    public class LayoutOption {
        public static bool TaskCollapsed = false;
    }
}
