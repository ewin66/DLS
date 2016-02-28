using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevExpress.ProductsDemo.Win.Common
{
    public class DK1EventArgs : EventArgs
    {
        private DataRow newErrorList;

        public DK1EventArgs(DataRow d)
        {
            newErrorList = d;
        }
        public DataRow NewErrorList
        {
            get { return newErrorList; }
        }

    }

    public class ErrorSetClass
    {
        public byte slaveId { get; set; }
        public ushort startAddress { get; set; }
        public ushort[] data { get; set; }
        public string message { get; set; }

        public ErrorSetClass()
        {
            slaveId = 0;
            startAddress = 0;
            data = new ushort[3];
            message = "";
        }
    }
    public class ErrorTabPageDrawClass
    {
        public string com { get; set; }
        public bool error { get; set; }


        public ErrorTabPageDrawClass(string com, bool error)
        {
            this.com = com;
            this.error = error;

        }
    }



    public class ErrorTabPageDrawEventArgs : EventArgs
    {
        private ErrorTabPageDrawClass errorTabPageDraw;

        public ErrorTabPageDrawEventArgs(ErrorTabPageDrawClass d)
        {
            errorTabPageDraw = d;
        }
        public ErrorTabPageDrawClass ErrorTabPageDraw
        {
            get { return errorTabPageDraw; }
        }

    }


    public class ErrorSetEventArgs : EventArgs
    {
        private ErrorSetClass errorStatusSetMessage;

        public ErrorSetEventArgs(ErrorSetClass d)
        {
            errorStatusSetMessage = d;
        }
        public ErrorSetClass ErrorStatusSetMessage
        {
            get { return errorStatusSetMessage; }
        }

    }


    public class ErrorMessageEventArgs : EventArgs
    {
        private DataRow dataRowErrorInfo;

        public ErrorMessageEventArgs(DataRow d)
        {
            dataRowErrorInfo = d;
        }
        public DataRow DataRowErrorInfo
        {
            get { return dataRowErrorInfo; }
        }

    }


    public class TimeoutEventArgs : EventArgs
    {
        private string timeoutPort;
        public TimeoutEventArgs(string d)
        {
            timeoutPort = d;
        }
        public string TimeoutPort
        {
            get { return timeoutPort; }
        }

    }

}
