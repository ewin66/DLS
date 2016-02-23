using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
namespace DevExpress.ProductsDemo.Win.Modules
{
    public partial class SystemStatus : BaseModule
    {
        private string mCom;
        private string mInterval;
        private SerialPort mComport = new SerialPort();

        private bool mTimeSync = false;
        Thread workerThread;

        //private System.Windows.Forms.Timer timer1;
        


        public SystemStatus()
        {
            InitializeComponent();

            mCom = "COM1";
            mInterval = "1000";

            //timer1.Interval = 1000;
            //timer1.Tick += new EventHandler(intervalTimer_Tick);

            Run();

            button2.BackColor = Color.LightGreen;

            
            this.simpleButton37.BackColor = Color.LightGreen;
            this.simpleButton36.BackColor = Color.LightGreen;
            this.simpleButton34.BackColor = Color.LightGreen;
            this.simpleButton33.BackColor = Color.LightGreen;
            this.simpleButton32.BackColor = Color.LightGreen;
            this.simpleButton31.BackColor = Color.LightGreen;
            this.simpleButton30.BackColor = Color.LightGreen;
            this.simpleButton29.BackColor = Color.LightGreen;
            this.simpleButton28.BackColor = Color.LightGreen;
            this.simpleButton27.BackColor = Color.LightGreen;
            this.simpleButton26.BackColor = Color.OrangeRed;
            this.simpleButton25.BackColor = Color.LightGreen;
            this.simpleButton24.BackColor = Color.LightGreen;
            this.simpleButton23.BackColor = Color.LightGreen;
            this.simpleButton22.BackColor = Color.LightGreen;
            this.simpleButton21.BackColor = Color.LightGreen;
            this.simpleButton20.BackColor = Color.LightGreen;
            this.simpleButton19.BackColor = Color.LightGreen;
            this.simpleButton18.BackColor = Color.LightGreen;
            this.simpleButton17.BackColor = Color.LightGreen;
            this.simpleButton16.BackColor = Color.LightGreen;
            this.simpleButton15.BackColor = Color.LightGreen;
            this.simpleButton14.BackColor = Color.LightGreen;
            this.simpleButton5.BackColor = Color.OrangeRed;
            this.simpleButton13.BackColor = Color.LightGreen;
            this.simpleButton12.BackColor = Color.OrangeRed;
            this.simpleButton11.BackColor = Color.LightGreen;
            this.simpleButton10.BackColor = Color.LightGreen;
            this.simpleButton9.BackColor = Color.LightGreen;
            this.simpleButton3.BackColor = Color.LightGreen;
            this.simpleButton2.BackColor = Color.LightGreen;
            this.simpleButton4.BackColor = Color.LightGreen;
            this.simpleButton8.BackColor = Color.LightGreen;
            this.simpleButton7.BackColor = Color.LightGreen;
            this.simpleButton6.BackColor = Color.LightGreen;
            this.simpleButton1.BackColor = Color.LightGreen;

            //this.serialConnection.Open("COM1", 115200, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One);
            //this.serialConnection
            //SerialPort sp = new SerialPort("COM1", 115200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
            //sp.Open();
            //sp.Write("hi1234");
        }

        /// <summary>
        /// UI 업데이트 타이머
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void intervalTimer_Tick(object sender, EventArgs e)
        {
            //int index = 0;

            //mCommTable = mComm.CommTable;
            //mErrorTable = mComm.ErrorTable;
            //DataTable errorList = mComm.ErrorListTable;
            //foreach (DataRow row in mCommTable.Rows)
            //{
            //    index = mCommTable.Rows.IndexOf(row);
            //    BMSStatusDataGridViewInvoke(dataGridViewBMSStatus, index, row, mErrorTable.Rows[index]);
            //}
            //mComport.Write("hi123433");

        }


              /// <summary>
        /// 모드버스 통신 스레드
        /// </summary>
        /// <param name="cycle"></param>
        public void DoWork(object cycle)
        {
            int timer = Convert.ToInt32(cycle);
            while (true)
            {
                mComport.Write("hi123433");
                System.Threading.Thread.Sleep(timer);
            }
        }


        /// <summary>
        /// 통신 시작
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {

            //// COM 포트가 이미 열려 있다면 
            //if (mComport.IsOpen)
            //{
            //    // 모드버스 통신 스레드가 시작되지 않았다면 시작
            //    if (!workerThread.IsAlive)
            //    {
            //        workerThread.Start(this.mInterval);
            //        workerThread.IsBackground = true;
            //    }

            //    // UI 업데이트 타이머 시작되지 않았다면 시작
            //    if (!timer1.Enabled)
            //        timer1.Enabled = true;

            //    return false;
            //}



            try
            {

                // COM 포트 설정
                mComport.BaudRate = 19200;
                mComport.DataBits = 8;
                mComport.Parity = Parity.None;
                mComport.StopBits = StopBits.One;
                mComport.PortName = mCom;


                // COM 포트 열기
                mComport.Open();




                //// UI 업데이트 타이머 시작
                ////timer1.Start();
                //// UI 업데이트 타이머 시작되지 않았다면 시작
                //if (!timer1.Enabled)
                //    timer1.Enabled = true;


                // 모드버스 인터페이스 스레드
                workerThread = new Thread(new ParameterizedThreadStart(DoWork));


                if (!workerThread.IsAlive)
                {
                    workerThread.Start(this.mInterval);
                    workerThread.IsBackground = true;
                }


            }
            catch (Exception e)
            {
                //MetroMessageBox.Show(mParent, e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 통신 종료
        /// </summary>
        public void Stop()
        {

            try
            {

                //dataGridViewBMSStatus.SafeInvoke(d => d.Rows.Clear());
                //foreach (DataRow row in mDataTable.Rows)
                //{
                //    dataGridViewBMSStatus.SafeInvoke(d => d.Rows.Add(row["Name"], "null", "null", "null", "null", "null", "null", "null", "null", row["Contact1"], row["Contact2"], row["Contact3"], row["Contact4"]));
                //}



                //// UI 업데이트 종료
                ////timer1.Stop();
                //timer1.Enabled = false;

                // 모드버스 통신 스레드 종료
                //RequestStop();


                // COM 포트 닫기
                if (mComport.IsOpen)
                    mComport.Close();


                //// 에러카운트 초기화(에러 발생시 탭이 점멸시 확인하는 에러 갯수 - 하나의 에러가 취소 되더라도 다른 애러가 있다면 점멸을 계속 유지하기)
                //mErrorCount = 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



        }

    }
}
