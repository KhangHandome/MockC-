﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO.Ports;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PC_TOOLV2
{
    public partial class MainForm : Form
    {
        /*Main thread */
        private Thread MainFunctionThread = null;
        private Image originalImag = null; /*Image */
        private Information_t Information = new Information_t();
        private Information_t Data_Warning = new Information_t(30,50);
        private SerialPortInfor_t serialPort_Current = new SerialPortInfor_t();
        private SerialPortInfor_t serialPort_Old = new SerialPortInfor_t();
        private PCToolState_t pcToolState = PCToolState_t.PCTool_Deinit;
        private Queue<Message_t> dataReceivedQueue = new Queue<Message_t>();  /*Save data from forwarder*/
        private Queue<Message_t> requestResponseQueue = new Queue<Message_t>();   /*Data received after send a request */
        private Dictionary<int,string> mappingData = new Dictionary<int,string>();
        private Queue<SerialPortInfor_t> serialPortInforQueue = new Queue<SerialPortInfor_t>();
        private Message_t ParseData = new Message_t();
        private string IdDataNode1 = "C0";
        private string IdDataNode2 = "D0";
        private string IdFilterResponse = "";
        private int IdMappingData;
        private Stopwatch TimerCounter = new Stopwatch();
        private Stopwatch checkTimeout = new Stopwatch();
        private Stopwatch pingTimeout = new Stopwatch();
        private bool isResponExist(int ID)
        {
            bool retVal = true;
            if(mappingData.ContainsKey(ID) == true)
            {
                retVal = true;
            }
            else
            {
                retVal = false;
            }
            return retVal;
        }
        private int generateID()
        {
            int retVal = 0;
            retVal = new Random().Next(1, 999);
            return retVal;
        }
        public MainForm()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            originalImag = volang_picturebox.Image;
            rotationWarningLabel.Text = "Safety: " + Data_Warning.Rotaion.ToString() + "°";
            distanceWarningLabel.Text = "Safety: " + Data_Warning.Distance.ToString() + "cm";
            Forwader.PortName = "COM9";
            Forwader.BaudRate = 9600;
            StartThread();
        }
        private void StartThread()
        {
            MainFunctionThread = new Thread(new ThreadStart(PCToolMainFunction));
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
        private void PCToolMainFunction()
        {
            while(true)
            {
                this.Invoke(new Action(() =>
                {
                    debug1.Text = pcToolState.ToString();
                }));
                switch (pcToolState)
                {
                    case PCToolState_t.PCTool_Deinit:
                        Task_PCToolDeinit();
                        break;
                    case PCToolState_t.PCTool_Init:
                        Task_PCToolInit();
                        break;
                    case PCToolState_t.PCTool_Running:
                        Task_PCToolRunning();
                        break;
                    case PCToolState_t.PCTool_Pause:
                        Task_PCToolPause();
                        break;
                    case PCToolState_t.PCTool_Reconnect:
                        Task_PCToolReconnect();
                        break;
                    case PCToolState_t.PCTool_Disconnected:
                        Task_PCToolDisconnected();
                        break;
                    default:
                        break;
                }
                Thread.Sleep(1);
            }
        }
        private void Task_PCToolDeinit()
        {
            pcToolState = PCToolState_t.PCTool_Init;
        }
        private void Task_PCToolInit()
        {
            SendRequestToNode("A0", "00");
            if ( checkRequestConnection("A0", "FF") == true)
            {
                SendRequestToNode("A1", "00");
                if(checkRequestConnection("A1","FF") == true)
                {
                    this.Invoke(new Action(() =>
                    {
                        rotaionLabel.Text = "Connected";
                    }));                 
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        rotaionLabel.Text = "Disconnection";
                    }));
                }    
                SendRequestToNode("A2", "00");
                if (checkRequestConnection("A2", "FF") == true)
                {
                    this.Invoke(new Action(() =>
                    {
                        distanceLabel.Text = "Connected";
                    }));
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        distanceLabel.Text = "Disconnection";
                    }));
                }
                pcToolState = PCToolState_t.PCTool_Running;
            }
            else
            {
                pcToolState = PCToolState_t.PCTool_Disconnected;
            }
        }
        private void Task_PCToolDisconnected()
        {
            pcToolState = PCToolState_t.PCTool_Reconnect;
        }
        private void Task_PCToolReconnect()
        {
            TimerCounter.Start();
            while (TimerCounter.ElapsedMilliseconds < 1000) ;
            SendRequestToNode("A0", "00");
            if (checkRequestConnection("A0", "FF") == true)
            {
                pcToolState = PCToolState_t.PCTool_Running;
                TimerCounter.Stop();
                TimerCounter.Reset();
            }
        }
        private void Task_PCToolRunning()
        {
            Message_t tpmMessage = new Message_t();
            if (dataReceivedQueue.Count > 0)
            {
                tpmMessage = dataReceivedQueue.Dequeue();
                if(String.Compare(tpmMessage.Id,IdDataNode1) == 0 )
                {
                    if(String.Compare(tpmMessage.Message,"FF") == 0 )
                    {
                        this.Invoke(new Action(() =>
                        {
                            rotaionLabel.Text = "Disconnected";
                            RotationWarningBtn.BackColor = Color.Yellow;
                        }));
                    }
                    else
                    {
                        int.TryParse(tpmMessage.Message, out Information.Rotaion);
                        this.Invoke(new Action(() =>
                        {
                            rotaionLabel.Text = Information.Rotaion.ToString();
                            if (Information.Rotaion > Data_Warning.Rotaion)
                            {
                                RotationWarningBtn.BackColor = Color.Red;
                            }
                            else
                            {
                                RotationWarningBtn.BackColor = Color.Green;
                            }
                        }));
                        volang_picturebox.Image = RotateImage(originalImag, Information.Rotaion);
                    }
                }
                if (String.Compare(tpmMessage.Id, IdDataNode2) == 0)
                {
                    if (String.Compare(tpmMessage.Message, "FF") == 0)
                    {
                        this.Invoke(new Action(() =>
                        {
                            distanceLabel.Text = "Disconnected";
                            DistanceWarningBtn.BackColor = Color.Yellow;
                        }));
                    }
                    else
                    {
                        int.TryParse(tpmMessage.Message, out Information.Distance);
                        this.Invoke(new Action(() =>
                        {
                            distanceLabel.Text = Information.Distance.ToString();
                            if (Information.Distance < Data_Warning.Distance)
                            {
                                DistanceWarningBtn.BackColor = Color.Red;
                            }
                            else
                            {
                                DistanceWarningBtn.BackColor = Color.Green;
                            }
                        }));
                    }
                }
            }
            if(checkTimeout.ElapsedMilliseconds > 1000)
            {
                SendRequestToNode("A0", "00");
                if (checkRequestConnection("A0", "FF") != true)
                {
                    pcToolState = PCToolState_t.PCTool_Disconnected;
                }
            }
            if(serialPortInforQueue.Count > 0)
            {
                pcToolState = PCToolState_t.PCTool_Pause;
            }
        }
        private void Task_PCToolPause()
        {
            SerialPortInfor_t tpmSerialInfor = new SerialPortInfor_t();
            if(Forwader.IsOpen == true && serialPortInforQueue.Count > 0 )
            {
                tpmSerialInfor = serialPortInforQueue.Dequeue();
                Forwader.Close();
                Forwader.PortName = tpmSerialInfor.PortName;
                Forwader.BaudRate = tpmSerialInfor.Baudrate;
            }
            pcToolState = PCToolState_t.PCTool_Reconnect;
        }

        private bool checkRequestConnection(string ID,string Data)
        {
            bool retVal = true;
            Message_t buffer = new Message_t();
            while (requestResponseQueue.Count == 0 && pingTimeout.ElapsedMilliseconds < 50) ;
            if (requestResponseQueue.Count > 0  && pingTimeout.ElapsedMilliseconds < 50)
            {
                buffer = requestResponseQueue.Dequeue();
                if (String.Compare(buffer.Id, ID) == 0 && String.Compare(buffer.Message, Data) == 0)
                {
                    retVal = true;
                }
  
            }
            else
            {

                retVal = false;
            }
            this.Invoke(new Action(() =>
            {
                pingLabel.Text = pingTimeout.ElapsedMilliseconds.ToString() + " ms";
            }));
            pingTimeout.Stop();
            pingTimeout.Reset();
            IdFilterResponse = "";
            mappingData.Remove(IdMappingData);
            return retVal;
        }
        private void SendRequestToNode(string IdNode, string Data)
        {
            string message = IdNode + "-" + Data;
            IdMappingData = generateID();
            IdFilterResponse = IdNode;
            if (Forwader.IsOpen != true)
            {
                try
                {
                    Forwader.Open();
                }
                catch (Exception ex)
                {

                }
            }
            if(Forwader.IsOpen == true)
            {
                Forwader.WriteLine(message);
            }
            pingTimeout.Restart();
        }
        private void parseMessage(string message) 
        {
            string[] buffer = message.Split('-');
            ParseData.Id = buffer[0];
            ParseData.Message = buffer[1];
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
        private void UpdateInforWarning(object sender, Information_t setup)
        {
            Data_Warning.Rotaion = setup.Rotaion;
            Data_Warning.Distance = setup.Distance;
            rotationWarningLabel.Text = "Safety: " + Data_Warning.Rotaion.ToString() + "°";
            distanceWarningLabel.Text = "Safety: " + Data_Warning.Distance.ToString() + "cm";
        }
        private void UpdateSerialPort(object sender, SerialPortInfor_t SerialPortInfor)
        {
            serialPort_Old.PortName = serialPort_Current.PortName;
            serialPort_Old.Baudrate = serialPort_Current.Baudrate;

            serialPort_Current.PortName = SerialPortInfor.PortName;
            serialPort_Current.Baudrate = SerialPortInfor.Baudrate;

            if (String.Compare(serialPort_Old.PortName, serialPort_Current.PortName) != 0 ||
                serialPort_Old.Baudrate != serialPort_Current.Baudrate)
            {
                pcToolState = PCToolState_t.PCTool_Pause;
                serialPortInforQueue.Enqueue(new SerialPortInfor_t(serialPort_Current.PortName, serialPort_Current.Baudrate));
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            AdvanceSetup advanceModeForm = new AdvanceSetup();
            advanceModeForm.UpdateSerialPort += UpdateSerialPort;
            advanceModeForm.StartPosition = FormStartPosition.Manual;
            advanceModeForm.Location = new System.Drawing.Point(this.Location.X + 50, this.Location.Y + 50); // Đặt vị trí tùy ý
            advanceModeForm.Show();
        }
        private void SettingBTN_Click(object sender, EventArgs e)
        {
            setup newform = new setup();
            newform.WarningDistanceUpdated += UpdateInforWarning;
            newform.StartPosition = FormStartPosition.Manual;
            newform.Location = new System.Drawing.Point(this.Location.X + 50, this.Location.Y + 50); // Đặt vị trí tùy ý
            newform.ReceiveData(Data_Warning);
            newform.Show();
        }
        private void Forwader_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string buffer = null;
            buffer = Forwader.ReadLine();
            if (buffer != null)
            {
                checkTimeout.Restart();
                pingTimeout.Stop();
                parseMessage(buffer);
                if (isResponExist(IdMappingData) != true)
                {
                    if (String.Compare(IdFilterResponse, ParseData.Id) == 0)
                    {
                        mappingData.Add(IdMappingData, buffer);
                        requestResponseQueue.Enqueue(new Message_t(ParseData.Id, ParseData.Message));
                        pingTimeout.Stop();
                    }
                }
                if (String.Compare(IdDataNode1, ParseData.Id) == 0 || String.Compare(IdDataNode2, ParseData.Id) == 0)
                {
                    dataReceivedQueue.Enqueue(new Message_t(ParseData.Id,ParseData.Message));
                    pingTimeout.Stop();
                }
            }
        }
    }
}