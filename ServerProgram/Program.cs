using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.MailClient.Win;
using DevExpress.MailDemo.Win;
using DevExpress.ProductsDemo.Win.Forms;
using DevExpress.ProductsDemo.Win.Controls;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;

namespace DevExpress.ProductsDemo.Win {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arguments) {
            WindowsFormsSettings.ApplyDemoSettings();
            //new DevExpress.DemoReports.ConnectionStringConfigurator(System.Configuration.ConfigurationManager.ConnectionStrings)
            //    .SelectDbEngine()
            //    .ExpandDataDirectory(fileName => DevExpress.DemoData.Helpers.DataFilesHelper.FindFile(fileName, DevExpress.DemoData.Helpers.DataFilesHelper.DataPath));

            //string path = DevExpress.Utils.FilesHelper.FindingFileName(AppDomain.CurrentDomain.BaseDirectory, @"Data\NWind.mdf", false);
            //XtraReportsDemos.ConnectionHelper.ApplyDataDirectory(System.IO.Path.GetDirectoryName(path));

            DataHelper.ApplicationArguments = arguments;

            
            //System.Globalization.CultureInfo enUs = new System.Globalization.CultureInfo("en-US");
            //System.Threading.Thread.CurrentThread.CurrentCulture = enUs;
            //System.Threading.Thread.CurrentThread.CurrentUICulture = enUs;
            DevExpress.Utils.LocalizationHelper.SetCurrentCulture(DataHelper.ApplicationArguments);
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Utils.AppearanceObject.DefaultFont = new Font("Segoe UI", 8);
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2013");
            SkinManager.EnableFormSkins();
            EnumProcessingHelper.RegisterEnum<TaskStatus>();


            //SplashScreenManager.ShowForm(null, typeof(ssMain), true, true, false, 1000);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            

            fmLogin login = new fmLogin();
            Application.Run(login);

            if( login.DialogResult == DialogResult.OK )
            {
                Application.Run(new frmMain());

                OleDbUserControl db = new OleDbUserControl();
                
                db.InsertLoginHistory(login.User, "로그아웃");
            }

            
        }
    }
}
