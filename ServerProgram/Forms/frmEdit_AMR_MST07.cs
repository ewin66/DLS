﻿using System;
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
    public partial class frmEdit_AMR_MST07 : XtraForm {
        AMR_MST07 contact, bindingContact;
        public frmEdit_AMR_MST07() {
            InitializeComponent();
        }
        public frmEdit_AMR_MST07(AMR_MST07 contact, IDXMenuManager menuManager)
        {
            InitializeComponent();
            this.contact = contact;
            this.bindingContact = contact.Clone();
            InitEditors();
            InitMenuManager(menuManager);


            teMST07IDE.DataBindings.Add("Text", bindingContact, "MST07IDE");
            teMST07NAM.DataBindings.Add("Text", bindingContact, "MST07NAM");
            
            UpdateCaption();
            InitValidationProvider();
        }

        void InitValidationProvider() {
            //dxValidationProvider1.SetValidationRule(teMST04CMP, ValidationRulesHelper.RuleIsNotBlank);
            //dxValidationProvider1.SetValidationRule(teMST04DON, ValidationRulesHelper.RuleIsNotBlank);
        }
        void UpdateCaption() {
            Text = bindingContact.MST07IDE;
        }
        void InitMenuManager(IDXMenuManager menuManager) {
            foreach (Control ctrl in lcMain.Controls)
            {
                BaseEdit edit = ctrl as BaseEdit;
                if (edit != null)
                {
                    edit.MenuManager = menuManager;
                }
            }
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
