using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Core.VO;


namespace Core
{
    public class Remoting
    {
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        
        public static void Start() {

            Equipo equipo = new Pivot();

            equipo.ID = "P_029";
            equipo.Panel = new Panel();
            equipo.Panel.ID = "P_029";

            Connect(equipo.Panel);
        
        }

        private static void Connect(Panel panel) {
        
            panel.GetConnection().BeginConnect("105.1.0." + Convert.ToInt32(panel.ID.Substring(2, 3)),10000, new AsyncCallback(connectCallback), panel);
            connectDone.WaitOne(TimeSpan.FromSeconds(1));    
        }

        private static void connectCallback(IAsyncResult ia){

            Panel p = (Panel)ia.AsyncState;

            if (p.GetConnection().Connected)
            {
                Console.WriteLine("ok");
                p.GetConnection().EndConnect(ia);
                connectDone.Set();

            }
            else {
                Console.WriteLine("not connection");
                
            }         
        }
      
        private static void Send() { 
        
        }
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
        public  static void Send(string command)
        {
           command = command + CalculaCheckSum(command) + Convert.ToChar(13);

            byte[] bytesSend = Encoding.ASCII.GetBytes(command.ToCharArray());
  
        }
    }
}
