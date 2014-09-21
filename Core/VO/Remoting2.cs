using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace Core.VO
{
    public class Remoting2
    {
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);
        private static string response = string.Empty;
  
        public static void Start() {

            Panel[] panelList = new Panel[2];
            
            panelList[0] = new Panel();
            panelList[0].ID = "002";
            panelList[1] = new Panel();
            panelList[1].ID = "003";
            
            //panelList[2] = new Panel();
            //panelList[2].ID = "004";
            //panelList[3] = new Panel();
            //panelList[3].ID = "005";
            //panelList[4] = new Panel();
            //panelList[4].ID = "006";
            //panelList[5] = new Panel();
            //panelList[5].ID = "007";
            //panelList[6] = new Panel();
            //panelList[6].ID = "008";
            //BUS.userBUS bus = new BUS.userBUS();
            //List<Core.VO.Equipo> equipoList = bus.GetEquipo();
            //foreach (Equipo e in equipoList){
            //Connected(e.Panel);
            //}   

            foreach (Panel p in panelList)
            {
                Connected(p);
            }
        }        
        #region Connect
        private static void Connected(Panel p) {

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("105.1.4.233"), 10000 + Convert.ToInt32(p.ID));
            p.workSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            p.workSocket.Bind(ip);
            p.workSocket.BeginConnect("105.1.0." + Convert.ToInt32(p.ID),
            10000, new AsyncCallback(connectCallBack), p.workSocket);
            connectDone.WaitOne(TimeSpan.FromSeconds(1));
            //send
            Send("(" + p.ID + "999RE", p.workSocket);
            sendDone.WaitOne();
            //receive
            //Receive(p.workSocket);
            //receiveDone.WaitOne();
        }

        private static void connectCallBack(IAsyncResult ia) {

            try
            {
                Socket s = ia.AsyncState as Socket;
                Console.WriteLine("Conectado: {0}", s.RemoteEndPoint.ToString());
                s.EndConnect(ia);
                connectDone.Set();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        #endregion
        #region Send
        private static void Send(string data, Socket s)
        {
            try
            {
                data += CalculaCheckSum(data) + Convert.ToChar(13);
                byte[] byteData = Encoding.ASCII.GetBytes(data);
                s.BeginSend(byteData, 0, byteData.Length, 0,
                SendCallBack, s);
            }
            catch (Exception exp) {
                Console.WriteLine(exp.Message);
            }
        }

        private static void SendCallBack(IAsyncResult ia)
        {
            try
            {
                Socket s = (Socket)ia.AsyncState;
                int byteSend = s.EndSend(ia);
                sendDone.Set();
                Receive(s);
                receiveDone.WaitOne();
            }
            catch (Exception exp) {
                Console.WriteLine(exp.Message);
            }
        }
        #endregion
        #region Receive
        private static void Receive(Socket s) {

            try
            {
                Panel p = new Panel();
                p.workSocket = s;
                s.BeginReceive(p.buffer, 0, Panel.BufferSize, 0,
               ReceiveCallBack, p);
            }
            catch (Exception exp) {
                Console.WriteLine(exp.Message);
            }
        }
        private static void ReceiveCallBack(IAsyncResult ia)
        {
            try
            {
                Panel p = (Panel)ia.AsyncState;
                Socket s = p.workSocket;
                
                int bytesRead = s.EndReceive(ia);

                if (bytesRead > 0)
                {
                    p.sb.Append(Encoding.ASCII.GetString(p.buffer, 0, bytesRead));
                    Console.WriteLine(p.sb.ToString());
                    s.BeginReceive(p.buffer, 0, Panel.BufferSize, 0,
                      ReceiveCallBack, p);
                }
                else
                {
                    if (p.sb.Length > 1)
                    {
                        response = p.sb.ToString();
                    }
                }

                receiveDone.Set();
            }
            catch (Exception exp) {
                Console.WriteLine(exp.Message);
            }
        }
        #endregion 
        #region CheckSum
        private static bool CheckSum(string trama)
        {
            int suma = 0;
            string suma_hex;

            for (int i = 0; i < 30; i++)
            {
                suma = (int)((suma + char.ConvertToUtf32(trama, i)) & 255);
            }

            suma_hex = suma.ToString("X2");

            if (suma_hex.Length == 1)
            {
                suma_hex = "0" + suma_hex;
            }

            if (suma_hex == trama.Substring(30, 2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static string CalculaCheckSum(string trama)
        {
            int suma = 0;

            for (int i = 0; i < trama.Length; i++)
            {
                suma = (int)((suma + char.ConvertToUtf32(trama, i)) & 255);
            }

            if (suma.ToString("X2").Length == 1)
            {
                return "0" + suma.ToString("X2");
            }
            else
            {
                return suma.ToString("X2");
            }
        }
        #endregion
    }
}