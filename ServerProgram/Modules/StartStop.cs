using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.MailDemo.Win;

namespace DevExpress.ProductsDemo.Win.Modules
{
    public partial class StartStop : BaseModule
    {
        public StartStop()
        {
            InitializeComponent();
        }

        internal override void ShowModule(bool firstShow)
        {
            base.ShowModule(firstShow);
            //gidControlAptManage.Focus();
            //UpdateActionButtons();
            if (firstShow)
            {
                ButtonClick(TagResources.StartStopRealtimeStatus);
            //    gidControlAptManage.ForceInitialize();
            //    GridHelper.SetFindControlImages(gidControlAptManage);
            //    if (DataHelper.AMR_MST04s.Count == 0) UpdateCurrentContact();
            }

        }

        void UpdateActionButtons()
        {
            OwnerForm.EnableLayoutButtons(true);
            OwnerForm.EnableZoomControl(true);
        }
        protected internal override void ButtonClick(string tag)
        {
            switch (tag)
            {
                case TagResources.StartStopRealtimeStatus:
                    break;
                case TagResources.StartStopComStart:
                    break;
                case TagResources.StartStopComStop:
                    break;
            }
        }
    }
}
