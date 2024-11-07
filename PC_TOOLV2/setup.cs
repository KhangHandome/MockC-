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
        public event EventHandler<InformationWarning> WarningDistanceUpdated;
        private InformationWarning data = new InformationWarning();
        public void ReceiveData(InformationWarning Data)
        {
            data.Rotaion = Data.Rotaion;
            data.Distance = Data.Distance;
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
            InformationWarning setup = new InformationWarning();
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
