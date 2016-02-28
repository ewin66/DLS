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
    public partial class SystemMonitoring : BaseModule
    {
        public SystemMonitoring()
        {
            InitializeComponent();
        }

        internal override void ShowModule(bool firstShow)
        {
            base.ShowModule(firstShow);
            
            if (firstShow)
            {
                ButtonClick(TagResources.StartStopRealtimeStatus);
            
            }

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
