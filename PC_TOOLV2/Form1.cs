using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC_TOOLV2
{
    public partial class Form1 : Form
    {
        private enum PCTOOL_State_t
        {
            SYSTEM_INIT,  /*Trạng thái cài đặt hệ thống, yêu cầu kết nối ban đầu   */
            SYSTEM_CHECK_CONNECTION, /* Trạng thái kiểm tra kết nối, sau khi gửi đi gói tin Ping */
            SYSTEM_REQUEST_CHECK_CONNECTION, /* Trạng thái yêu cầu kiểm tra kết nối, ví dụ sau khi kh nhận dc bản tin nào trong 1s */
            SYSTEM_RUN, /*Trạng thái hoạt động bình thường */
            SYSTEM_DISCONNECTION, /*Trạng thái mất kết nối */
            SYSTEM_SENT_THRESSHOLD, /*Trang thai gui ban tin cap nhat thresshold*/
            SYSTEM_SENT_CONNECT_TO_NODE,
        }
        private enum SendRequest_t
        {
            PC_TOOL_SEND_IDLE,
            PC_TOOL_SENDING,
            PC_TOOL_SEND_DONE
        }
        private enum RequestNode_t
        {
            NODE_DEINIT,
            NODE_INIT,
            NODE_DISCONNECT
        }
        private enum PortState_t
        {
            PORT_INIT,  /* Trạng thái đang khởi tạo PORT */
            PORT_CHANGE /* Trạng thái thay đổi PORT      */
        }
        private enum DataStatus_t
        {
            NotSent,
            WaitingForReply,
            ResponseReceived
        }
        int NumberOfNode = 2;
        int IndexOfNode = 1; 
        int i = 0;
        Thread MainFunctionThread = null;
        Thread InitConnectionThread = null;
        InformationWarning last_InforWarning = new InformationWarning() { Distance = 0, Rotaion = 0 };
        InformationWarning InforWarning = new InformationWarning() { Distance = 120, Rotaion = 30 };
        private string g_DataReceive = null;
        private Image originalImag = null;
        private SerialPort g_SerialPort;
        private SerialPort g_lastSerialPort;
        /*Đo thời gian nhận được các bản tin */
        Stopwatch checkTimeout = new Stopwatch();
        /*Đo thời gian từ lúc gửi bản tin tới lúc nhận bản tin */
        Stopwatch pingTimeout = new Stopwatch();
        RequestNode_t requestNode = RequestNode_t.NODE_DEINIT;
        SendRequest_t SendState = SendRequest_t.PC_TOOL_SEND_IDLE;
        PortState_t serailPort1_Config = PortState_t.PORT_INIT;
        DataStatus_t SerialPort1Status = DataStatus_t.NotSent;
        /*Init varialbe state to handle */
        PCTOOL_State_t pcToolState = PCTOOL_State_t.SYSTEM_INIT;
        public Form1()
        {
            InitializeComponent();
        }
        private void StartThread()
        {
            MainFunctionThread = new Thread(new ThreadStart(MainFunction));
            MainFunctionThread.IsBackground = true;
            MainFunctionThread.Start();
        }
        private void InitSystem(string[] args)
        {
            RequestConnection("A0-00-00");
            while (true)
            {
                this.Invoke(new Action(() => { textBox3.Text = "START" + pcToolState.ToString(); }));
                switch (pcToolState)
                {
                    case PCTOOL_State_t.SYSTEM_INIT:
                        break;
                    case PCTOOL_State_t.SYSTEM_CHECK_CONNECTION:
                        if (pingTimeout.ElapsedMilliseconds > 100)
                        {
                            pcToolState = PCTOOL_State_t.SYSTEM_DISCONNECTION;
                            pingTimeout.Stop();
                            pingTimeout.Reset();
                            this.Invoke(new Action(() => { pingLabel.Text = "ping : 1000 "; }));
                        }
                        if (SerialPort1Status == DataStatus_t.ResponseReceived && string.Compare(g_DataReceive, "A0-00-0F") == 0 && pingTimeout.ElapsedMilliseconds < 100)
                        {
                            this.Invoke(new Action(() => { pingLabel.Text = "ping :" + pingTimeout.ElapsedMilliseconds.ToString() + " ms"; }));
                            pcToolState = PCTOOL_State_t.SYSTEM_RUN;
                            timer2.Enabled = false;
                            statusConnectBtn.BackColor = Color.Green;
                            pingTimeout.Reset();
                        }
                        break;
                    case PCTOOL_State_t.SYSTEM_RUN:
                        if (checkTimeout.ElapsedMilliseconds > 2000)
                        {
                            pcToolState = PCTOOL_State_t.SYSTEM_REQUEST_CHECK_CONNECTION;
                            checkTimeout.Reset();
                        }
                        break;
                    case PCTOOL_State_t.SYSTEM_REQUEST_CHECK_CONNECTION:
                        RequestConnection("A0-00-00");
                        break;
                    case PCTOOL_State_t.SYSTEM_DISCONNECTION:
                        this.Invoke(new Action(() =>
                        {
                            statusConnectBtn.BackColor = Color.Red;
                            timer2.Enabled = true;
                        }));
                        break;
                    case PCTOOL_State_t.SYSTEM_SENT_CONNECT_TO_NODE:
                        pcToolState = PCTOOL_State_t.SYSTEM_REQUEST_CHECK_CONNECTION;
                        break;
                    default:
                        break;
                }
                Thread.Sleep(10);
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if(MainFunctionThread != null && MainFunctionThread.IsAlive)
            {
                MainFunctionThread.Abort();
            }
        }
        private void MainFunction()
        {
            while (true)
            {
                this.Invoke(new Action(() => { textBox3.Text = "START" + pcToolState.ToString();}));
                switch (pcToolState)
                {
                    case PCTOOL_State_t.SYSTEM_INIT:
                        if (SerialPort1Status == DataStatus_t.NotSent )
                        {
                            RequestConnection("A0-00-00");
                        }
                        else if (SerialPort1Status == DataStatus_t.ResponseReceived || pingTimeout.ElapsedMilliseconds > 100)
                        {
                            if (pingTimeout.ElapsedMilliseconds > 100)
                            {
                                pcToolState = PCTOOL_State_t.SYSTEM_DISCONNECTION;
                                pingTimeout.Reset();
                                this.Invoke(new Action(() => { pingLabel.Text = "ping : 1000 "; }));                              
                            }
                            if (SerialPort1Status == DataStatus_t.ResponseReceived && string.Compare(g_DataReceive, "A0-00-0F") == 0 && pingTimeout.ElapsedMilliseconds < 100)
                            {
                                this.Invoke(new Action(() => { pingLabel.Text = "ping :" + pingTimeout.ElapsedMilliseconds.ToString() + " ms"; }));
                                pcToolState = PCTOOL_State_t.SYSTEM_SENT_CONNECT_TO_NODE;
                                statusConnectBtn.BackColor = Color.Green;
                                pingTimeout.Reset();
                            }
                            SerialPort1Status = DataStatus_t.NotSent;
                        }
                        break;
                    case PCTOOL_State_t.SYSTEM_CHECK_CONNECTION:
                        break;
                    case PCTOOL_State_t.SYSTEM_RUN:
                        if (checkTimeout.ElapsedMilliseconds > 2000)
                        {
                            pcToolState = PCTOOL_State_t.SYSTEM_REQUEST_CHECK_CONNECTION;
                            checkTimeout.Reset();
                        }
                        break;
                    case PCTOOL_State_t.SYSTEM_REQUEST_CHECK_CONNECTION:
                        if (SerialPort1Status == DataStatus_t.NotSent)
                        {
                            RequestConnection("A0-00-00");
                        }
                        else if (SerialPort1Status == DataStatus_t.ResponseReceived || pingTimeout.ElapsedMilliseconds > 100)
                        {
                            if (pingTimeout.ElapsedMilliseconds > 100)
                            {
                                pcToolState = PCTOOL_State_t.SYSTEM_DISCONNECTION;
                                pingTimeout.Stop();
                                pingTimeout.Reset();
                                this.Invoke(new Action(() => { pingLabel.Text = "ping : 1000 "; }));
                            }
                            if (SerialPort1Status == DataStatus_t.ResponseReceived && string.Compare(g_DataReceive, "A0-00-0F") == 0 && pingTimeout.ElapsedMilliseconds < 100)
                            {
                                this.Invoke(new Action(() => { pingLabel.Text = "ping :" + pingTimeout.ElapsedMilliseconds.ToString() + " ms"; }));
                                pcToolState = PCTOOL_State_t.SYSTEM_RUN;
                                timer2.Enabled = false;
                                statusConnectBtn.BackColor = Color.Green;
                                pingTimeout.Reset();
                            }
                            SerialPort1Status = DataStatus_t.NotSent;
                        }
                        break;
                    case PCTOOL_State_t.SYSTEM_DISCONNECTION:
                        this.Invoke(new Action(() =>
                        {
                            statusConnectBtn.BackColor = Color.Red;
                            timer2.Enabled = true;
                        }));
                        break;
                    case PCTOOL_State_t.SYSTEM_SENT_CONNECT_TO_NODE:
                        switch (IndexOfNode)
                        {
                            case 1:
                                if (SerialPort1Status == DataStatus_t.NotSent)
                                {
                                    RequestConnection("A1-00-00");
                                }
                                else if (SerialPort1Status == DataStatus_t.ResponseReceived || pingTimeout.ElapsedMilliseconds > 100)
                                {
                                    if (pingTimeout.ElapsedMilliseconds > 100)
                                    {
                                        pingTimeout.Reset();
                                        this.Invoke(new Action(() => { statusNode1Btn.BackColor = Color.Red; }));
                                    }
                                    if (SerialPort1Status == DataStatus_t.ResponseReceived && string.Compare(g_DataReceive, "A1-00-0F") == 0)
                                    {
                                        pingTimeout.Reset();
                                        this.Invoke(new Action(() => { statusNode1Btn.BackColor = Color.Green; }));
                                    }
                                    IndexOfNode++;
                                    SerialPort1Status = DataStatus_t.NotSent;
                                }
                                break;
                            case 2:
                                if (SerialPort1Status == DataStatus_t.NotSent)
                                {
                                    RequestConnection("A2-00-00");
                                }
                                else if (SerialPort1Status == DataStatus_t.ResponseReceived || pingTimeout.ElapsedMilliseconds > 100)
                                {
                                    if (pingTimeout.ElapsedMilliseconds > 100)
                                    {
                                        pingTimeout.Reset();
                                        this.Invoke(new Action(() => { statusNode2Btn.BackColor = Color.Red; }));
                                    }
                                    if (SerialPort1Status == DataStatus_t.ResponseReceived && string.Compare(g_DataReceive, "A2-00-0F") == 0)
                                    {
                                        pingTimeout.Reset();
                                        this.Invoke(new Action(() => { statusNode2Btn.BackColor = Color.Green; }));
                                    }
                                    IndexOfNode++;
                                    SerialPort1Status = DataStatus_t.NotSent;
                                }
                                break;
                            case 3:
                                pcToolState = PCTOOL_State_t.SYSTEM_RUN;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                switch (SendState)
                {
                    case SendRequest_t.PC_TOOL_SENDING:
                        SendRequestThresshold();
                        break;
                    case SendRequest_t.PC_TOOL_SEND_IDLE:
                        if (SerialPort1Status == DataStatus_t.ResponseReceived && String.Compare(g_DataReceive, "B0-00-0F") == 0 && pingTimeout.ElapsedMilliseconds < 100)
                        {
                            SendState = SendRequest_t.PC_TOOL_SEND_DONE;
                        }
                        break;
                    case SendRequest_t.PC_TOOL_SEND_DONE:
                        break;
                }
                Thread.Sleep(10);
            }
        }
        private void SettingBTN_Click(object sender, EventArgs e)
        {
            setup newform = new setup();
            newform.WarningDistanceUpdated += UpdateWarningDistance;
            newform.StartPosition = FormStartPosition.Manual;
            newform.Location = new System.Drawing.Point(this.Location.X+50, this.Location.Y+50); // Đặt vị trí tùy ý
            newform.ReceiveData(new InformationWarning() { Rotaion = InforWarning.Rotaion, Distance = InforWarning.Distance });
            newform.Show();
        }
        private Image RotateImage(Image img,float angle)
        {
            Bitmap rotateBmp = new Bitmap(img.Width,img.Height);
            using(Graphics g = Graphics.FromImage(rotateBmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // Dịch chuyển hệ tọa độ đến tâm của hình ảnh
                g.TranslateTransform((float)img.Width / 2, (float)img.Height / 2);

                // Quay hình ảnh theo góc chỉ định
                g.RotateTransform(angle);

                // Vẽ hình ảnh đã xoay vào Bitmap
                g.DrawImage(img, -img.Width / 2, -img.Height / 2);
            }
            return rotateBmp;
        }
        private void RequestConnection(string message)
        {
            SerialPort1Status = DataStatus_t.NotSent;
            if (serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.Open();
                }
                catch
                {

                }
            }
            if (serialPort1.IsOpen == true)
            {
                serialPort1.WriteLine(message);
                SerialPort1Status = DataStatus_t.WaitingForReply;
            }
            pingTimeout.Stop();
            pingTimeout.Reset();
            pingTimeout.Start();
        }
        public void PCTOOL_MainFunction(string str)
        {
            i++;
            textBox2.Text = str + i.ToString();
            checkTimeout.Restart();
            SerialPort1Status = DataStatus_t.ResponseReceived;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            originalImag = volang_picturebox.Image;
            serialPort1.PortName = "COM9";
            serialPort1.BaudRate = 9600;
            StartThread();
        }
        void SendRequestThresshold()
        {
            if (pcToolState == PCTOOL_State_t.SYSTEM_RUN)
            {
                if(last_InforWarning.Distance != InforWarning.Distance)
                {
                    serialPort1.Write("A2-00-01\n");
                }
                if (last_InforWarning.Rotaion != InforWarning.Rotaion)
                {
                    serialPort1.Write("A2-00-02\n");
                }
                SendState = SendRequest_t.PC_TOOL_SEND_IDLE;
            }
        }
        private void UpdateWarningDistance(object sender, InformationWarning setup)
        {
            last_InforWarning.Distance = InforWarning.Distance;
            last_InforWarning.Rotaion = InforWarning.Rotaion;
            InforWarning.Distance = setup.Distance;
            InforWarning.Rotaion = setup.Rotaion;
            distanceWariningTB.Text = "Safety:" + InforWarning.Distance.ToString() + "cm";
            rotationWarningTb.Text = "Safety: ±" + InforWarning.Rotaion.ToString() + "°";
            SendState = SendRequest_t.PC_TOOL_SENDING;
        }
        private void UpdateSerialPort(object sender, SerialPort Sender_SerialPort)
        {
            serailPort1_Config = PortState_t.PORT_CHANGE;
            g_lastSerialPort = g_SerialPort;
            g_SerialPort = Sender_SerialPort as SerialPort;
            if ( g_lastSerialPort != g_SerialPort)
            {
                timer2.Enabled = false;
                if(serialPort1.IsOpen == true)
                {
                    serialPort1.Close();
                }
                portnameLable.Text = "Port :" + g_SerialPort.PortName.ToString();
                serialPort1.PortName = g_SerialPort.PortName;
            }
            serailPort1_Config = PortState_t.PORT_INIT;
        }
        private void SendataViaSerialPort(string s_data)
        {
            if(serialPort1.IsOpen)
            {
                serialPort1.WriteLine(s_data);
            }
        }
        public void ParseData(string str)
        {
            textBox2.Text = str;
        }
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            pingTimeout.Stop();
            g_DataReceive = "";
            g_DataReceive = serialPort1.ReadLine();
            this.Invoke(new Action(() => PCTOOL_MainFunction(g_DataReceive)));
        }

        private void button2_Click(object sender, EventArgs e)
        {         
            Form2 advanceModeForm = new Form2();
            advanceModeForm.UpdateSerialPort += UpdateSerialPort;
            advanceModeForm.StartPosition = FormStartPosition.Manual;
            advanceModeForm.Location = new System.Drawing.Point(this.Location.X + 50, this.Location.Y + 50); // Đặt vị trí tùy ý
            advanceModeForm.Show();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            RequestConnection("A0-00-00");
        }
    }
}
