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
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

        private static string response = string.Empty;
         
        public static void Start() {

            Core.BUS.userBUS userbus = new BUS.userBUS();
            foreach (Equipo eq in userbus.GetEquipo())
            {
                Connect(eq.Panel);
            }
        }

        private static void Connect(Panel panel) {

            if (!panel.GetConnection().Connected)
            {
                panel.GetConnection().BeginConnect("105.1.0." + Convert.ToInt32(panel.ID.Substring(2, 3)), 10000, connectCallback, panel);
                connectDone.WaitOne(TimeSpan.FromSeconds(1));
                Send("(" + Convert.ToInt32(panel.ID.Substring(2, 3)) + "999RE", panel.GetConnection());
            }
            else { 
               
            }
        }
    private static void connectCallback(IAsyncResult ia){

            Panel p = (Panel)ia.AsyncState;

            if (p.GetConnection().Connected)
            {
                Console.WriteLine("#EQUIPO: {0} [ok]", p.ID);
                p.GetConnection().EndConnect(ia);
                //SendCommand(p, "QUERY");
                connectDone.Set();
            }
            else {
                Console.WriteLine("#EQUIPO: {0} [not connection]", p.ID);
                connectDone.Set();
       }         
    }

            
    public  static void Send (string data, Socket client)
    {
            data += CalculaCheckSum(data) + Convert.ToChar(13);
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data.ToCharArray());
            // Begin sending the data to the remote device.
            client.BeginSend(byteData, 0, byteData.Length, 0, SendCallback,client);
         
    }

    public  static void SendCallback(IAsyncResult ar)
    {
        try
        {
            // Retrieve the socket from the state object.
            Socket client = (Socket)ar.AsyncState;
            // Complete sending the data to the remote device.
            int bytesSent = client.EndSend(ar);
            Console.WriteLine("Sent {0} bytes to server.", bytesSent);
            // Signal that all bytes have been sent.
            sendDone.Set();
            Receive(client);
            receiveDone.WaitOne();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
    private static void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            // Retrieve the state object and the client socket 
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;
            // Read data from the remote device.
            int bytesRead = client.EndReceive(ar);
            if (bytesRead > 0)
            {
                // There might be more data, so store the data received so far.
                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                Console.WriteLine(state.sb.ToString());
                // Get the rest of the data.
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
               ReceiveCallback, state);
            }
            else
            {
                // All the data has arrived; put it in response.
                if (state.sb.Length > 1)
                {
                    response = state.sb.ToString();
                }
                // Signal that all bytes have been received.
                receiveDone.Set();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
    private static void Receive(Socket client)
    {
        try
        {
            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = client;

            // Begin receiving the data from the remote device.
            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                ReceiveCallback, state);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
        //private static void SendCommand(Panel p, CommandType cmd) { 
                    
        //    switch(cmd){
        //        case CommandType.QUERY:
        //            byte [] BytesSend = Encoding.ASCII.GetBytes("("+ p.ID.Substring(2,3) + "999RE");
        //            p.GetConnection().BeginSend(BytesSend, 0, BytesSend.Length, 0, new AsyncCallback(sendCallBack), p);
        //            break;
        //        case CommandType.MARCHA:
        //            break;
        //        case CommandType.PARADA:
        //            break;
        //        case CommandType.FOWARD:
        //            break;
        //        case CommandType.REVERSA:
        //            break;
        //        //case "MARCHAR":
        //        //    break;
        //        default:
        //            break;
        //    }

        //}
        //private static void sendCallBack(IAsyncResult ia) {
           
        //    Panel p = (Panel)ia.AsyncState;

            


        //}
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
        //private static void ReceiveCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        // Retrieve the state object and the client socket 
        //        // from the asynchronous state object.
        //        //StateObject state = (StateObject)ar.AsyncState;
        //        //Socket client = state.workSocket;
        //        // Read data from the remote device.
        //        int bytesRead = client.EndReceive(ar);


                
        //        if (bytesRead > 0)
        //        {
        //            // There might be more data, so store the data received so far.
        //            state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
        //            //  Get the rest of the data.
        //            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
        //                new AsyncCallback(ReceiveCallback), state);
        //        }
        //        else
        //        {
        //            // All the data has arrived; put it in response.
        //            if (state.sb.Length > 1)
        //            {
        //                response = state.sb.ToString();
        //            }
        //            // Signal that all bytes have been received.
        //            receiveDone.Set();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}
       
        //public  static void Send(string command)
        //{
        //   command = command + CalculaCheckSum(command) + Convert.ToChar(13);

        //    byte[] bytesSend = Encoding.ASCII.GetBytes(command.ToCharArray());
  
        //}
    }
}
