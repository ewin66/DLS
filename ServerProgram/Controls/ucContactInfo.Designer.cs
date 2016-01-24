namespace DevExpress.MailClient.Win.Controls {
    partial class ucContactInfo {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.lcMain = new DevExpress.XtraLayout.LayoutControl();
            this.teNotes = new DevExpress.XtraEditors.MemoEdit();
            this.teAddress = new DevExpress.XtraEditors.TextEdit();
            this.tePhone = new DevExpress.XtraEditors.TextEdit();
            this.teEmail = new DevExpress.XtraEditors.HyperLinkEdit();
            this.teBirthDate = new DevExpress.XtraEditors.TextEdit();
            this.lbName = new DevExpress.XtraEditors.LabelControl();
            this.lcgMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.pePhoto = new DevExpress.MailClient.Win.PictureEditSimpleZoom();
            this.lciPhoto = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            this.lcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teBirthDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pePhoto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // lcMain
            // 
            this.lcMain.AllowCustomization = false;
            this.lcMain.Controls.Add(this.teNotes);
            this.lcMain.Controls.Add(this.teAddress);
            this.lcMain.Controls.Add(this.tePhone);
            this.lcMain.Controls.Add(this.teEmail);
            this.lcMain.Controls.Add(this.teBirthDate);
            this.lcMain.Controls.Add(this.lbName);
            this.lcMain.Controls.Add(this.pePhoto);
            this.lcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcMain.Location = new System.Drawing.Point(0, 0);
            this.lcMain.Name = "lcMain";
            this.lcMain.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(669, 200, 450, 350);
            this.lcMain.Root = this.lcgMain;
            this.lcMain.Size = new System.Drawing.Size(227, 397);
            this.lcMain.TabIndex = 0;
            this.lcMain.Text = "layoutControl1";
            // 
            // teNotes
            // 
            this.teNotes.Location = new System.Drawing.Point(2, 381);
            this.teNotes.Name = "teNotes";
            this.teNotes.Properties.ReadOnly = true;
            this.teNotes.Size = new System.Drawing.Size(223, 16);
            this.teNotes.StyleController = this.lcMain;
            this.teNotes.TabIndex = 11;
            this.teNotes.UseOptimizedRendering = true;
            // 
            // teAddress
            // 
            this.teAddress.Location = new System.Drawing.Point(2, 341);
            this.teAddress.Name = "teAddress";
            this.teAddress.Properties.ReadOnly = true;
            this.teAddress.Size = new System.Drawing.Size(223, 20);
            this.teAddress.StyleController = this.lcMain;
            this.teAddress.TabIndex = 10;
            // 
            // tePhone
            // 
            this.tePhone.Location = new System.Drawing.Point(57, 301);
            this.tePhone.Name = "tePhone";
            this.tePhone.Properties.ReadOnly = true;
            this.tePhone.Size = new System.Drawing.Size(168, 20);
            this.tePhone.StyleController = this.lcMain;
            this.tePhone.TabIndex = 9;
            // 
            // teEmail
            // 
            this.teEmail.Location = new System.Drawing.Point(57, 277);
            this.teEmail.Name = "teEmail";
            this.teEmail.Properties.ReadOnly = true;
            this.teEmail.Size = new System.Drawing.Size(168, 20);
            this.teEmail.StyleController = this.lcMain;
            this.teEmail.TabIndex = 8;
            this.teEmail.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.teEmail_OpenLink);
            // 
            // teBirthDate
            // 
            this.teBirthDate.Location = new System.Drawing.Point(57, 253);
            this.teBirthDate.Name = "teBirthDate";
            this.teBirthDate.Properties.ReadOnly = true;
            this.teBirthDate.Size = new System.Drawing.Size(168, 20);
            this.teBirthDate.StyleController = this.lcMain;
            this.teBirthDate.TabIndex = 7;
            // 
            // lbName
            // 
            this.lbName.Appearance.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lbName.Location = new System.Drawing.Point(2, 224);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(223, 25);
            this.lbName.StyleController = this.lcMain;
            this.lbName.TabIndex = 6;
            this.lbName.Text = "Name";
            // 
            // lcgMain
            // 
            this.lcgMain.CustomizationFormText = "lcgMain";
            this.lcgMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgMain.GroupBordersVisible = false;
            this.lcgMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciPhoto,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem1});
            this.lcgMain.Location = new System.Drawing.Point(0, 0);
            this.lcgMain.Name = "Root";
            this.lcgMain.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgMain.Size = new System.Drawing.Size(227, 397);
            this.lcgMain.Text = "Root";
            this.lcgMain.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.teBirthDate;
            this.layoutControlItem2.CustomizationFormText = "Birth Date:";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 251);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(227, 24);
            this.layoutControlItem2.Text = "Birth Date:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(52, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.teEmail;
            this.layoutControlItem3.CustomizationFormText = "Email:";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 275);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(227, 24);
            this.layoutControlItem3.Text = "Email:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(52, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.tePhone;
            this.layoutControlItem4.CustomizationFormText = "Phone:";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 299);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(227, 24);
            this.layoutControlItem4.Text = "Phone:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(52, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.teAddress;
            this.layoutControlItem5.CustomizationFormText = "Address:";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 323);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(227, 40);
            this.layoutControlItem5.Text = "Address:";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(52, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.teNotes;
            this.layoutControlItem6.CustomizationFormText = "Notes:";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 363);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 0);
            this.layoutControlItem6.Size = new System.Drawing.Size(227, 34);
            this.layoutControlItem6.Text = "Notes:";
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(52, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.lbName;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 222);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(227, 29);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // pePhoto
            // 
            this.pePhoto.Location = new System.Drawing.Point(0, 0);
            this.pePhoto.Name = "pePhoto";
            this.pePhoto.Properties.AllowFocused = false;
            this.pePhoto.Properties.NullText = " ";
            this.pePhoto.Properties.PictureInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            this.pePhoto.Properties.ReadOnly = true;
            this.pePhoto.Properties.ShowMenu = false;
            this.pePhoto.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pePhoto.Size = new System.Drawing.Size(225, 222);
            this.pePhoto.StyleController = this.lcMain;
            this.pePhoto.TabIndex = 4;
            this.pePhoto.Resize += new System.EventHandler(this.pePhoto_Resize);
            // 
            // lciPhoto
            // 
            this.lciPhoto.Control = this.pePhoto;
            this.lciPhoto.CustomizationFormText = "lciPhoto";
            this.lciPhoto.Location = new System.Drawing.Point(0, 0);
            this.lciPhoto.Name = "lciPhoto";
            this.lciPhoto.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 2, 0, 0);
            this.lciPhoto.Size = new System.Drawing.Size(227, 222);
            this.lciPhoto.Text = "lciPhoto";
            this.lciPhoto.TextSize = new System.Drawing.Size(0, 0);
            this.lciPhoto.TextToControlDistance = 0;
            this.lciPhoto.TextVisible = false;
            // 
            // ucContactInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lcMain);
            this.Name = "ucContactInfo";
            this.Size = new System.Drawing.Size(227, 397);
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            this.lcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teBirthDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pePhoto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciPhoto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcMain;
        private PictureEditSimpleZoom pePhoto;
        private DevExpress.XtraLayout.LayoutControlGroup lcgMain;
        private DevExpress.XtraLayout.LayoutControlItem lciPhoto;
        private XtraEditors.TextEdit teAddress;
        private XtraEditors.TextEdit tePhone;
        private XtraEditors.HyperLinkEdit teEmail;
        private XtraEditors.TextEdit teBirthDate;
        private XtraEditors.LabelControl lbName;
        private XtraLayout.LayoutControlItem layoutControlItem1;
        private XtraLayout.LayoutControlItem layoutControlItem2;
        private XtraLayout.LayoutControlItem layoutControlItem3;
        private XtraLayout.LayoutControlItem layoutControlItem4;
        private XtraLayout.LayoutControlItem layoutControlItem5;
        private XtraEditors.MemoEdit teNotes;
        private XtraLayout.LayoutControlItem layoutControlItem6;
    }
}
