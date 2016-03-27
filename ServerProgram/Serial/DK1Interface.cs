using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Collections;
using System.IO.Ports;
using DevExpress.ProductsDemo.Win.Common;
using DevExpress.ProductsDemo.Win.Item;
using DevExpress.XtraCharts;

namespace DevExpress.ProductsDemo.Win.Serial
{
    public class DK1Interface : IDisposable
    {

        #region Events

        public event EventHandler<DK1EventArgs> eDataReceive;
        protected void eHandler(DK1EventArgs e)
        {
            EventHandler<DK1EventArgs> handler = eDataReceive;
            if (handler != null)
            {
                handler(this, e);
            }
        }



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

        /// <summary>
        /// 소멸자
        /// </summary>
        ~DK1Interface()
        {
            if (!isDispose)
            {
                Dispose();
            }
        }

        ArrayList recieveBuffer = new ArrayList();
        void mComport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int bytes = mComport.BytesToRead;

            byte[] buffer = new byte[bytes];

            mComport.Read(buffer, 0, bytes);


            recieveBuffer.AddRange(buffer);

            ArrayList remainder = ReceiveValue(recieveBuffer);

            recieveBuffer.Clear();
            recieveBuffer.AddRange(remainder);

            eHandler(new DK1DataArgs
            {
                address = "null",
                Data = DK1Util.ByteArrayToHexString(buffer)

            });
        }
        private ArrayList ReceiveValue(ArrayList cvalue)
        {
            ArrayList remainder = new ArrayList();
            int startIndex = 0;
            byte tlen,dataLen = 0;
            byte[] data = new byte[cvalue.Count];

            cvalue.CopyTo(0, data, 0, cvalue.Count);

            int nextstartIndex = 0;

            

            for (startIndex = 0; startIndex < cvalue.Count; )
            {

                try
                {

                    // STX  LEN  검침기ID   CMD
                    // 1    1       4       1
                    //int offset = 2;
                    int indexof = cvalue.IndexOf(byte.Parse("2"), startIndex);
                    if (indexof == -1)
                        break;

                    if (cvalue.Count >= indexof + 6) {
                        tlen = data[indexof + 1];
                    }
                    else break;

                    if( cvalue.Count >= tlen )
                    {
                        dataLen = getDataLength(data[indexof+6]);

                        DK1Util.GetCheckSum(data, indexof, tlen - 1);
                        DK1Util.GetCheckXOR(data, indexof, tlen - 2);

                        byte[] dk1data = new byte[dataLen];
                        cvalue.CopyTo(indexof+7, dk1data, 0, dk1data.Length);

                        updateData(dk1data);
                            //getDataLength
                        //tlen = data[indexof + 1];
                        //// 파싱한 패킷 길이가 수신된 패킷의 길이 보다 클경우 에러 패킷을 판단하고 초기화 한다.
                        //if (tlen > cvalue.Count)
                        //{
                        //    remainder = new ArrayList();
                        //    return remainder;
                        //}
         

                        startIndex = startIndex + tlen;
                        nextstartIndex = startIndex;


                    }
                    else
                    {
                        startIndex += indexof;
                    }

                }
                catch (Exception ex)
                {

                    //eHandler(new Dabom.CommPlugIn.Item.PlugInDriverState
                    //{
                    //    DeviceId = this.deviceid,
                    //    Message = string.Format(" Packet Error : [{0}][{1}]", data.Length, BitConverter.ToString(data, 0, data.Length).Replace("-", "")),
                    //    State = Dabom.CommPlugIn.Item.DriverState.Run_Read
                    //});
                    //eHandler(new Dabom.CommPlugIn.Item.PlugInDriverErrorArgs { DeviceId = this.deviceid, Mode = "Read:", Error = ex.Message });
                    remainder = new ArrayList();
                    return remainder;
                }


            }

            if (cvalue.Count > startIndex)
            {
                remainder.AddRange(cvalue.GetRange(nextstartIndex + 1, cvalue.Count - nextstartIndex - 1));
            }
            else
            {
                remainder = new ArrayList();
            }

            return remainder;
        }
        private byte getDataLength(byte cmd)
        {
            byte length = 0;

            switch (cmd)
            {
                case 0x11:
                    length = 34;
                    break;
                case 0x12:
                    length = 28;
                    break;

                default:

                    break;
            }

            return length;
        }

        /// <summary>
        /// 디바이스로 전달 받은 데이터 업데이트
        /// </summary>
        /// <param name="data"></param>
        private void updateData(byte[] data)
        {

            int offset = 0;
            DK1Data item = new DK1Data();

            item.DONG = (0x00ff & Convert.ToUInt32(data[offset++])) << 8;
            item.DONG |= (0x00ff & Convert.ToUInt32(data[offset++])) << 0;
            
            item.HO = (0x00ff & Convert.ToUInt32(data[offset++])) << 8;
            item.HO |= (0x00ff & Convert.ToUInt32(data[offset++])) << 0;

            for (int i = 0; i < 6; i++ )
            {
                item.SENSOR[i] = (0x000000ff & Convert.ToUInt32(data[offset++])) << 16;
                item.SENSOR[i] |= (0x000000ff & Convert.ToUInt32(data[offset++])) << 8;
                item.SENSOR[i] |= (0x000000ff & Convert.ToUInt32(data[offset++])) << 0;

                item.ERROR[i] = data[offset++];
            }

            // 리스트 테이블에 수신한 ID 찾기
            DataRow foundRow = mCommTable.Rows.Find(new string[2] { item.DONG.ToString(), item.HO.ToString() });
            
            // 있다면 업데이트
            if (foundRow != null)
            {
                //int index = mCommTable.Rows.IndexOf(foundRow);

                //전기	수도	온수	가스	열량	냉방
                foundRow["전기"] = item.SENSOR[0] / 10.0;
                foundRow["수도"] = item.SENSOR[1] / 10.0;
                foundRow["온수"] = item.SENSOR[2] / 10.0;
                foundRow["가스"] = item.SENSOR[3] / 10.0;
                foundRow["열량"] = item.SENSOR[4] / 10.0;
                foundRow["냉방"] = item.SENSOR[5] / 10.0;
                
            }

        }


        /// <summary>
        /// 장비에 데이터 요청
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pollingTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            byte[] item = new byte[9];
            UInt16 dong = 0;
            UInt16 ho = 0;

            foreach (DataRow row in mDataTable.Rows)
            {
                item[0] = 0x02;
                item[1] = 0x09;
                dong = Convert.ToUInt16(row.ItemArray[2]);
                item[2] = (byte)(dong >> 8);
                item[3] = (byte)(dong >> 0);
                ho = Convert.ToUInt16(row.ItemArray[3]);
                item[4] = (byte)(ho >> 8);
                item[5] = (byte)(ho >> 0);
                item[6] = 0x11;
                item[7] = 0x0F;
                item[8] = 0x1C;

                //foreach (DataRow row in mCommTable.Rows)

                if (mComport.IsOpen)
                {
                    mComport.Write(item, 0, item.Length);

                    this.pollingTimer.Enabled = true;
                }
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

            this.mComport.Close();
            //this.mComport.DataReceived -= mComport_DataReceived;

            this.pollingTimer.Enabled = false;
            this.actived = false;

            

        }

        #endregion

    }
}
