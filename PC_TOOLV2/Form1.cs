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
        int NumberOfNode = 2;
        int IndexOfNode = 1; 
        int i = 0;
        Thread MainFunctionThread = null;
        InformationWarning last_Infor = new InformationWarning() { Distance = 0, Rotaion = 0 };
        private string g_DataReceive = null;
        private int g_Distance = 0;
        private int g_WarningDistance = 120;
        private int g_WarningRotation = 30;
        private Image originalImag = null;
        private float angle = 0;
        private SerialPort g_SerialPort;
        private SerialPort g_lastSerialPort;
        /*Đo thời gian nhận được các bản tin */
        Stopwatch checkTimeout = new Stopwatch();
        /*Đo thời gian từ lúc gửi bản tin tới lúc nhận bản tin */
        Stopwatch pingTimeout = new Stopwatch();
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
        RequestNode_t requestNode = RequestNode_t.NODE_DEINIT;
        SendRequest_t SendState = SendRequest_t.PC_TOOL_SEND_IDLE;
        PortState_t serailPort1_Config = PortState_t.PORT_INIT;
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
            RequestConnection("A0-00-00");
            while (true)
            {
                this.Invoke(new Action(() => { textBox3.Text = "START" + pcToolState.ToString();}));
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
                        break;
                    case PCTOOL_State_t.SYSTEM_RUN:

                        if (checkTimeout.ElapsedMilliseconds > 2000)
                        {
                            pcToolState = PCTOOL_State_t.SYSTEM_REQUEST_CHECK_CONNECTION;
                            checkTimeout.Reset();
                        }
                        if (requestNode == RequestNode_t.NODE_DEINIT && IndexOfNode == 1 )
                        {
                            RequestConnection("A1-00-00");
                            pcToolState = PCTOOL_State_t.SYSTEM_SENT_CONNECT_TO_NODE;
                            IndexOfNode++;
                            requestNode = RequestNode_t.NODE_INIT;
                        }
                        if (requestNode == RequestNode_t.NODE_DEINIT && IndexOfNode == 2 )
                        {
                            RequestConnection("A2-00-00");
                            pcToolState = PCTOOL_State_t.SYSTEM_SENT_CONNECT_TO_NODE;
                            IndexOfNode++;
                            requestNode = RequestNode_t.NODE_INIT;
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
                switch (SendState)
                {
                    case SendRequest_t.PC_TOOL_SENDING:
                        SendRequestThresshold();
                        break;
                    case SendRequest_t.PC_TOOL_SEND_IDLE:
                        break;
                    case SendRequest_t.PC_TOOL_SEND_DONE:
                        break;
                }
            }
        }
        private void SettingBTN_Click(object sender, EventArgs e)
        {
            setup newform = new setup();
            newform.WarningDistanceUpdated += UpdateWarningDistance;
            newform.StartPosition = FormStartPosition.Manual;
            newform.Location = new System.Drawing.Point(this.Location.X+50, this.Location.Y+50); // Đặt vị trí tùy ý
            newform.ReceiveData(new InformationWarning() { Rotaion = g_WarningRotation, Distance = g_WarningDistance });
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

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            volang_picturebox.Image = RotateImage(originalImag, trackBar1.Value -90);
            textBox1.Text =( trackBar1.Value - 90).ToString();
        }
        private void RequestConnection(string message)
        {
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
            }
            pingTimeout.Stop();
            pingTimeout.Reset();
            pingTimeout.Start();
            pcToolState = PCTOOL_State_t.SYSTEM_CHECK_CONNECTION;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            /*textBox3.Text = "START" + pcToolState.ToString();
            switch (pcToolState)
            {
                case PCTOOL_State_t.SYSTEM_INIT: 
                    break;
                case PCTOOL_State_t.SYSTEM_CHECK_CONNECTION:
                    if(pingTimeout.ElapsedMilliseconds > 100 )
                    {
                        pcToolState = PCTOOL_State_t.SYSTEM_DISCONNECTION;
                        pingTimeout.Stop();
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
                    RequestConnection();
                    break;
                case PCTOOL_State_t.SYSTEM_DISCONNECTION:
                    statusConnectBtn.BackColor = Color.Red;
                    timer2.Enabled = true; 
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
                    break;
                case SendRequest_t.PC_TOOL_SEND_DONE:
                    break;
            }*/
        }
        public void PCTOOL_MainFunction(string str)
        {
            i++;
            textBox2.Text = str + i.ToString();
            checkTimeout.Restart();
            timer1.Enabled = false;
            switch (pcToolState)
            {
                case PCTOOL_State_t.SYSTEM_INIT:
                    break;
                case PCTOOL_State_t.SYSTEM_CHECK_CONNECTION:
                    if (String.Compare(str, "A0-00-0F") == 0 && pingTimeout.ElapsedMilliseconds < 100 )
                    {
                        pingLabel.Text = "ping :"+pingTimeout.ElapsedMilliseconds.ToString() +" ms";
                        pcToolState = PCTOOL_State_t.SYSTEM_RUN;
                        timer2.Enabled = false;
                        statusConnectBtn.BackColor = Color.Green;
                    } 
                    break;
                case PCTOOL_State_t.SYSTEM_RUN:
                    break;
                case PCTOOL_State_t.SYSTEM_REQUEST_CHECK_CONNECTION:
                    requestNode = RequestNode_t.NODE_DEINIT;
                    if(string.Compare(str,"A1-00-FF") == 0)
                    {
                        statusNode1Btn.BackColor = Color.Green;
                    }
                    else if ( IndexOfNode == 1)
                    {
                        statusNode1Btn.BackColor = Color.Red;
                    }
                    if (string.Compare(str, "A2-00-FF") == 0)
                    {
                        statusNode2Btn.BackColor = Color.Green;
                    }
                    else if ( IndexOfNode == 2)
                    {
                        statusNode1Btn.BackColor = Color.Red;
                    }
                    break;
                default:
                    break;
            }
            switch(SendState)
            {
                case SendRequest_t.PC_TOOL_SENDING:
                    break;
                case SendRequest_t.PC_TOOL_SEND_IDLE:
                    if (String.Compare(str, "Confirm") == 0 && pingTimeout.ElapsedMilliseconds < 100)
                    {
                        SendState = SendRequest_t.PC_TOOL_SEND_DONE;
                    }
                    break;
                case SendRequest_t.PC_TOOL_SEND_DONE:
                    break;
            }
            timer1.Enabled = true;
            pingTimeout.Reset();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            originalImag = volang_picturebox.Image;
            if (serialPort1.IsOpen == true)
            {
                statusConnectBtn.BackColor = Color.Green;
            }
            else
            {
                statusConnectBtn.BackColor = Color.Red;
            }
            serialPort1.PortName = "COM9";
            serialPort1.BaudRate = 9600;
            StartThread();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            g_Distance = trackBar2.Value;
            distanceTB.Text = g_Distance.ToString() + "cm";
            if(g_Distance < g_WarningDistance)
            {
                button5.BackColor = Color.Red;
            }
            else
            {
                button5.BackColor = Color.Green;
            }
        }
        void SendRequestThresshold()
        {
            if (pcToolState == PCTOOL_State_t.SYSTEM_RUN)
            {
                if(last_Infor.Distance != g_WarningDistance)
                {
                    serialPort1.Write("Node1\n");
                }
                if (last_Infor.Rotaion != g_WarningRotation)
                {
                    serialPort1.Write("Node2\n");
                }
                SendState = SendRequest_t.PC_TOOL_SEND_IDLE;
            }
        }
        private void UpdateWarningDistance(object sender, InformationWarning setup)
        {
            last_Infor.Distance = g_WarningDistance;
            last_Infor.Rotaion = g_WarningRotation;
            g_WarningDistance = setup.Distance;
            g_WarningRotation = setup.Rotaion;
            distanceWariningTB.Text = "Safety:" + g_WarningDistance.ToString() + "cm";
            rotationWarningTb.Text = "Safety: ±" + g_WarningRotation.ToString() + "°";
            SendState = SendRequest_t.PC_TOOL_SENDING;
        }
        private void UpdateSerialPort(object sender, SerialPort Sender_SerialPort)
        {
            serailPort1_Config = PortState_t.PORT_CHANGE;
            g_lastSerialPort = g_SerialPort;
            g_SerialPort = Sender_SerialPort as SerialPort;
            if ( g_lastSerialPort != g_SerialPort)
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
                if(serialPort1.IsOpen == true)
                {
                    serialPort1.Close();
                }
                portnameLable.Text = "Port :" + g_SerialPort.PortName.ToString();
                serialPort1.PortName = g_SerialPort.PortName;
                timer1.Enabled = true;
            }
            serailPort1_Config = PortState_t.PORT_INIT;
        }
        private void SendataViaSerialPort(string s_data)
        {
            s_data = s_data + '\n' + '\0';
            serialPort1.Write(s_data);
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
