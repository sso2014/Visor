using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace Core.VO
{
    public class Panel
    {
        public Panel() {
            //IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 10000 + Convert.ToInt32(ID.Substring(2,3)));
            //sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //sck.Bind(endPoint);
        }

        public Panel(string ID)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 10000 + Convert.ToInt32(ID.Substring(2, 3)));
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sck.Bind(endPoint);
        }

        private Socket sck = null;
        
        public int ALARMA_SEG
        {
            get;
            set;
        }
        public int ANGULO
        {
            get;
            set;
        }
        public int APLICACION
        {
            get;
            set;
        }
        public int CAMINANDO
        {
            get;
            set;
        }
        public int ESPERANDO_PRES
        {
            get;
            set;
        }
        public int FALLA_ELECTRICA
        {
            get;
            set;
        }
        public int HABILITADO
        {
            get;
            set;
        }
        public string ID
        {
            get;
            set;
        }
        public int PRESION
        {
            get;
            set;
        }
        public int SECO
        {
            get;
            set;
        }
        public int SENTIDO
        {
            get;
            set;
        }
        public int TENSION
        {
            get;
            set;
        }
        
        public void Marcha()
        {
            
        }
        public void Parada()
        {
          
        }
        public void Foward()
        {
        }
        public void Reversa()
        {
           
        }
        
        public Socket GetConnection() {
            return sck;
        }
    }
}
