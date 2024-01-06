using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GuiFasel
{
    class ConfigUdp
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        UdpClient udpClient;
        public ConfigUdp()
        {
            //hread ThreadStart = new Thread(new ThreadStart(GetStart));
            //ThreadStart.Start();
        }
        public void GetStart()
        {
            IPEndPoint iPNode = new IPEndPoint(IPAddress.Parse("127.0.0.1"),1254);
            EndPoint endPoint = iPNode;
            socket.Bind(endPoint);
            socket.Connect(iPNode);
        }
    }
    public class EventClass : EventArgs
    {
        public int Value;
        public int Opcod;
        public EventClass(int value, int opcod)
        {
            this.Value = value;
            this.Opcod = opcod;
        }
    }
    public enum OpCode
    {
        upConvert = 1,
        DownConvert = 2,
        MainSwitch = 3,
        RFHead = 4,
        RXBeanFormer = 5,
        TXBeanFormer = 6,
        power1 = 7,
    }
}