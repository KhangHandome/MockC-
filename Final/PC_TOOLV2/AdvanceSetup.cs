using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace PC_TOOLV2
{
    public partial class AdvanceSetup : Form
    {
        /*
            Đăng ký sự kiện cho form 2 
         */
        public event EventHandler<SerialPortInfor_t> UpdateSerialPort;
        private Stopwatch stopwatch;
        public AdvanceSetup()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();
            stopwatch.Stop();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string[] baudrate = { "9600", "115200" };
            string[] porrName = SerialPort.GetPortNames();
            listPortCb.DataSource = porrName;
            listBaundrate.DataSource = baudrate;
        }
  
        private void connectBtn_Click(object sender, EventArgs e)
        {
            Int32 baundrate = 0;
            SerialPortInfor_t SerialPort = new SerialPortInfor_t();
            Int32.TryParse(listBaundrate.Text.ToString(), out baundrate);
            SerialPort.PortName = listPortCb.SelectedValue.ToString();
            SerialPort.Baudrate = baundrate;
            UpdateSerialPort?.Invoke(this, SerialPort);
            this.Close();
        }

        private void showdata(string s )
        {
            
        }
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] baudrate = { "9600", "115200" };
            string[] porrName = SerialPort.GetPortNames();
            listPortCb.DataSource = porrName;
            listBaundrate.DataSource = baudrate;
        }
    }
}
