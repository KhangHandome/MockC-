using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC_TOOLV2
{
    public partial class setup : Form
    {
        public event EventHandler<Information_t> WarningDistanceUpdated;
        private Information_t data = new Information_t();
        public void ReceiveData(Information_t Data)
        {
            if (Data != null)
            {
                data.Rotaion = Data.Rotaion;
                data.Distance = Data.Distance;
            }
            else
            {
                data.Rotaion = 0;
                data.Distance = 0;
            }
        }
        public setup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = data.Distance.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Information_t setup = new Information_t();
            Int32.TryParse(textBox1.Text.ToString(),out setup.Distance);
            Int32.TryParse(textBox2.Text.ToString(), out setup.Rotaion);
            if ( setup.Rotaion > 180 )
            {
                MessageBox.Show("Gia tri nhap vuot qua nguong cho phep ");
            }
            else
            {
                WarningDistanceUpdated?.Invoke(this, setup);
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = data.Rotaion.ToString();
        }

        private void setup_Load(object sender, EventArgs e)
        {
            textBox1.Text = data.Distance.ToString();
            textBox2.Text = data.Rotaion.ToString();
        }
    }
}
