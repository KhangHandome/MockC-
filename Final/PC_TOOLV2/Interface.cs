using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_TOOLV2
{
    public enum PCToolState_t
    {
        PCTool_Deinit,
        PCTool_Init,
        PCTool_Running,
        PCTool_Pause,
        PCTool_Reconnect,
        PCTool_Disconnected
    }
    public enum SerialState_t
    {
        Serial_Idle,
        Serial_Sending,
        Serial_Receiving,
        Serial_Waiting,
        Serial_Received
    }
    public class Message_t
    {
        public string Id;
        public string Message;
        public Message_t()
        {
        }
        public Message_t(string ID, string Message)
        {
            this.Id = ID;
            this.Message = Message;
        }
    }
    public class SerialPortInfor_t
    {
        public string PortName;
        public int Baudrate;
        public SerialPortInfor_t()
        {

        }
        public SerialPortInfor_t(string PortName, int Baudrate)
        {
            this.PortName = PortName;
            this.Baudrate = Baudrate;
        }
    }
    public class Information_t
    {
        public int Rotaion;
        public int Distance;
        public Information_t()
        {
            Rotaion = 0;
            Distance = 0;
        }
        public Information_t(int Rotation,int Distance)
        {
            this.Rotaion = Rotation;
            this.Distance = Distance;
        }
    }

}
