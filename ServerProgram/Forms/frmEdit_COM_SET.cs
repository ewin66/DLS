using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Ribbon;
using DevExpress.MailClient.Win;
using ServerProgram;

namespace DevExpress.ProductsDemo.Win.Forms {
    public partial class frmEdit_COM_SET : XtraForm {
        AMR_MST04Model contact, bindingContact;
        public frmEdit_COM_SET() {
            InitializeComponent();
        }
        public frmEdit_COM_SET(AMR_MST04Model contact, IDXMenuManager menuManager)
        {
            InitializeComponent();
            this.contact = contact;
            this.bindingContact = contact.Clone();
            InitEditors();
            InitMenuManager(menuManager);


            //teMST04CMP.DataBindings.Add("Text", bindingContact, "MST04CMP");
            //teMST04DON.DataBindings.Add("Text", bindingContact, "MST04DON");
            //teMST04HNO.DataBindings.Add("Text", bindingContact, "MST04HNO");
            //teMST04NAM.DataBindings.Add("Text", bindingContact, "MST04NAM");  
            //teMST04PHN.DataBindings.Add("Text", bindingContact, "MST04PHN"); 
            
            UpdateCaption();
            InitValidationProvider();
        }

        void InitValidationProvider() {
            //dxValidationProvider1.SetValidationRule(teMST04CMP, ValidationRulesHelper.RuleIsNotBlank);
            //dxValidationProvider1.SetValidationRule(teMST04DON, ValidationRulesHelper.RuleIsNotBlank);
        }
        void UpdateCaption() {
            Text = bindingContact.MST04CMP;
        }
        void InitMenuManager(IDXMenuManager menuManager) {
            //foreach(Control ctrl in lcMain.Controls) {
            //    BaseEdit edit = ctrl as BaseEdit;
            //    if(edit != null) {
            //        edit.MenuManager = menuManager;
            //    }
            //}
        }
        void InitEditors() {
            //EditorHelper.InitPersonComboBox(icbGender.Properties);
            //EditorHelper.InitTitleComboBox(icbUserLevel.Properties);
            //cbeCity.Properties.Items.AddRange(EditorHelper.GetCities());
            //cbeState.Properties.Items.AddRange(EditorHelper.GetStates());
        }
        private void sbOK_Click(object sender, EventArgs e) {
            
            contact.Assign(bindingContact);
        }

    }
}
