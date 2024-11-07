using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_TOOLV2
{
    public class InformationWarning
    {
        public int Rotaion;
        public int Distance;
        public InformationWarning() { 
            Rotaion = 0;
            Distance = 0;
        }
    }
    public class SerialPortInfor
    {
        public string PortName;
        public UInt32 Baudrate;
    }
    public class DataNode
    {
        public string ID;
        public string Data;
        public string Status;
        public DataNode() {
            ID = "0";
            Data = "0";
            Status = "0";
        }
    }

}
