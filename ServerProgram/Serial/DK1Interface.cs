using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Collections;
using System.IO.Ports;
using DevExpress.ProductsDemo.Win.Common;
using DevExpress.XtraCharts;

namespace DevExpress.ProductsDemo.Win.Serial
{
    public class DK1Interface : IDisposable
    {

        #region Events
        // 에러 발생 이벤트
        public event EventHandler<ErrorMessageEventArgs> DataRowErrorInfo;
        protected void OnErrorMessageChanged(ErrorMessageEventArgs e)
        {
            EventHandler<ErrorMessageEventArgs> handler = DataRowErrorInfo;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// 타임아웃 이벤트
        /// </summary>
        public event EventHandler<TimeoutEventArgs> TimeoutPort;
        protected void OnTimeoutChanged(TimeoutEventArgs e)
        {
            EventHandler<TimeoutEventArgs> handler = TimeoutPort;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        
        #region Variables

        private bool isDispose = false;

        private SerialPort mComport = new SerialPort();

        private DataTable mDataTable;   // contact에러시 기정의된 이름으로 출력하기 위한 참조 테이블
        private DataTable mCommTable;
        private DataTable mErrorTable;
        
        private DataTable mErrorListTable;
        private string mLineName;
        private string mBMSName;
        private string mCom;
        
        
        private IniFileHandle mErrorLog;

        public bool actived = false;

        public DataTable CommTable
        {
            get { return mCommTable; }
            set { mCommTable = value; }
        }
        public DataTable ErrorTable
        {
            get { return mErrorTable; }
            set { mErrorTable = value; }
        }
        public DataTable ErrorListTable
        {
            get { return mErrorListTable; }
            set { mErrorListTable = value; }
        }
        public string LineName
        {
            get { return mLineName; }
            set { mLineName = value; }
        }
        public string BMSName
        {
            get { return mBMSName; }
            set { mBMSName = value; }
        }

        #endregion



        #region public method


        public DK1Interface( DataTable dataTable, DataTable commTable, DataTable errorTable, UInt16 interval )
        {
            // COM 포트 설정
            mComport.BaudRate = 9600;
            mComport.DataBits = 8;
            mComport.Parity = Parity.None;
            mComport.StopBits = StopBits.One;
            mComport.PortName = "COM1";


            // COM 포트 열기
            mComport.Open();

            mComport.DataReceived += mComport_DataReceived;

            mDataTable = dataTable;
            mCommTable = commTable;
            mErrorTable = errorTable;

            this.pollingTimer = new System.Timers.Timer();
            this.pollingTimer.AutoReset = false;
            this.pollingTimer.Elapsed += pollingTimer_Elapsed;
            this.PollingInterval = interval;
            this.pollingTimer.Enabled = true;

            this.actived = true;
        }

        void mComport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void pollingTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            byte[] item = new byte[9];

            item[0] = 0x02;
            item[1] = 0x00;
            item[2] = 0xFF;
            item[3] = 0xFF;
            item[4] = 0xFF;
            item[5] = 0xFF;
            item[6] = 0x11;
            item[7] = 0x0F;
            item[8] = 0x1C;

            //foreach (DataRow row in mCommTable.Rows)
            {
                mComport.Write(item, 0, item.Length);


            }

            this.pollingTimer.Enabled = true;

        }

        ~DK1Interface()
        {
            if (!isDispose)
            {
                Dispose();
            }
        }

        private readonly System.Timers.Timer pollingTimer;
        private int pollingInterval;
        public int PollingInterval
        {
            get { return pollingInterval; }
            set
            {
                pollingInterval = value;
                // workaround from doc to avoid firing the event

                this.pollingTimer.AutoReset = true;
                //this.pollingCycle.Interval = 1000d * value;
                this.pollingTimer.Interval = value;
                this.pollingTimer.AutoReset = false;
            }
        }


        /// <summary>
        /// 실시간 데이터 읽기
        /// </summary>
        ///         
        public int ModbusSerialAsciiMasterReadRegisters(SerialPort master, UInt16 dong, UInt16 ho, byte cmd)
        {


            byte[] buildPacket = new byte[7];    // read protocol size
            string recvBuffer = "";
            
            byte[] checkBuffer = null;

            bool success = false;

            try
            {

                //3A30323034383030303030333234380D0A
                //3A 3032 3034 3830 3030 3030 3332 3438 0D 0A

                
                //modbus modbusUtil = new modbus();

                // 전송 패킷 생성
                //modbusUtil.BuildModbusAsciiMessage(slaveId, readType, startAddress, numRegisters, ref buildPacket);


                // 헥사 스트링으로 변환
                string convert = DK1Util.ByteArrayToHexString(buildPacket);

                // 공백문자제거
                convert = convert.Replace(" ", "");

                // STX, CR/LF 추가
                string sendMessage = string.Format(":{0}\r\n", convert);

                // 전송
                //success = modbusUtil.SendFc4(master, sendMessage, ref recvBuffer);
                if( success )
                {
                    checkBuffer = DK1Util.HexStringToByteArray(recvBuffer.Substring(1, recvBuffer.Length - 1));

                    //if (!modbusUtil.CheckLRCResponse(checkBuffer))
                    //    return -2;
                }
                else
                {
                    // 이벤트 전달
                    //TimeoutEventArgs timeoutPort = new TimeoutEventArgs(this.mLineName);
                    //OnTimeoutChanged(timeoutPort);

                    return -1;

                }

                string [] dongho = new string[2];
                dongho[0] = string.Format("{0}", dong);
                dongho[1] = string.Format("{0}", ho);
                // BMS 리스트 테이블에 수신한 ID 찾기
                DataRow foundRow = mCommTable.Rows.Find(dongho);
                DataRow foundErrRow = mErrorTable.Rows.Find(dongho);

                // 있다면 업데이트
                if (foundRow != null)
                {
                    int index = mCommTable.Rows.IndexOf(foundRow);


                    //foundRow["VOLT"] = string.Format("{0:F1}", curValue.Data[0]);

                }



            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }


            return 0;
        }

        #endregion



        #region IDisposable 멤버

        public void Dispose()
        {
            isDispose = true;

            this.pollingTimer.Enabled = false;
            this.actived = false;

            

        }

        #endregion

    }
}
