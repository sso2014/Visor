using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Console
{

    class ObjectState {
        
        public Socket sck = null;
        
        public const int BufferSize = 1024;
        
        public byte [] Buffer = new byte[BufferSize];

        public StringBuilder data = new StringBuilder();
        
        public string ID { get; set; }
       
    }


 


    class AsyncronusConnection {
        


        private ManualResetEvent connectedDone = new ManualResetEvent(false);
        
        private ManualResetEvent receiveDone = new ManualResetEvent(false);
        
        private ManualResetEvent disconnectedDone = new ManualResetEvent(false);
        
        private ManualResetEvent readDone = new ManualResetEvent(false);

        public static void Start() {
        
        }
        private static void Connect(ObjectState obj) {

            obj.sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            obj.sck.BeginConnect("105.1.0." + Convert.ToInt32(obj.ID),10000, new AsyncCallback(ConnectCallBack),obj);
        }
        
        private static void Send(string data) {
            data = data + " ";
        }
     
        private static void Receive() {
        }
      
        private static void Read() { 
        
        }
        
        private static void ConnectCallBack(IAsyncResult ia)
        {
            ObjectState obj = ia.AsyncState as ObjectState;

            if (obj.sck.Connected) { 
               
            } 
            
        }
        
        private static void ReceiveCallBack(IAsyncResult ia)
        {

        }
        
        private static void SendCallBack(IAsyncResult ia)
        {

        }
       
        private static void ReadCallBack(IAsyncResult ia)
        {

        }
        
        private string CalculateChecksum(string dataToCalculate)
        {
            byte[] byteToCalculate = Encoding.ASCII.GetBytes(dataToCalculate);
            int checksum = 0;
            foreach (byte chData in byteToCalculate)
            {
                checksum += chData;
            }
            checksum &= 0xff;
            return checksum.ToString("X2");
        }
    
    }
    
    class Program
    {
        static void Main(string[] args)
        {

        }


    }
}
