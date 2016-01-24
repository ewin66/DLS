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

namespace DevExpress.ProductsDemo.Win.Forms {
    public partial class frmEditUser : XtraForm {
        UserInfo contact, bindingContact;
        public frmEditUser() {
            InitializeComponent();
        }
        public frmEditUser(UserInfo contact, IDXMenuManager menuManager)
        {
            InitializeComponent();
            this.contact = contact;
            this.bindingContact = contact.Clone();
            InitEditors();
            InitMenuManager(menuManager);
            peUserPhoto.Image = bindingContact.Photo;

            teUserID.DataBindings.Add("Text", bindingContact, "Id");    // Id
            tePassword.DataBindings.Add("Text", bindingContact, "Password");
            teConfirmPassword.DataBindings.Add("Text", bindingContact, "ConfirmPassword");
            icbUserLevel.DataBindings.Add("EditValue", bindingContact, "Level");    // Level
            teUserName.DataBindings.Add("Text", bindingContact, "Name");    // Name
            teUserPhone.DataBindings.Add("Text", bindingContact, "Phone");  // Phone
            meNotes.DataBindings.Add("Text", bindingContact, "Note");
            UpdateCaption();
            InitValidationProvider();
        }

        void InitValidationProvider() {
            dxValidationProvider1.SetValidationRule(teUserID, ValidationRulesHelper.RuleIsNotBlank);
            dxValidationProvider1.SetValidationRule(tePassword, ValidationRulesHelper.RuleIsNotBlank);
        }
        void UpdateCaption() {
            Text = bindingContact.Id;
        }
        void InitMenuManager(IDXMenuManager menuManager) {
            foreach(Control ctrl in lcMain.Controls) {
                BaseEdit edit = ctrl as BaseEdit;
                if(edit != null) {
                    edit.MenuManager = menuManager;
                }
            }
        }
        void InitEditors() {
            //EditorHelper.InitPersonComboBox(icbGender.Properties);
            EditorHelper.InitTitleComboBox(icbUserLevel.Properties);
            //cbeCity.Properties.Items.AddRange(EditorHelper.GetCities());
            //cbeState.Properties.Items.AddRange(EditorHelper.GetStates());
        }
        private void sbOK_Click(object sender, EventArgs e) {
            bindingContact.Photo = peUserPhoto.Image;
            contact.Assign(bindingContact);
        }

    }
}
