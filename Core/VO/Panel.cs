using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Core.VO
{
    public class Panel:StateObject
    {
        public Panel() {
            //IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 10000 + Convert.ToInt32(ID.Substring(2,3)));
            //sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //sck.Bind(endPoint);
        }
        public Panel(string ID) {
            
            this.ID = ID;

            Console.WriteLine("Creando Panel: {0}", this.ID);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("105.1.4.223"), 10000 + Convert.ToInt32(ID.Substring(2, 3)));
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sck.Bind(endPoint);
        }
        
        private Socket sck = null;
        
        public bool ALARMA_SEG
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
        public bool CAMINANDO
        {
            get;
            set;
        }
        public bool ESPERANDO_PRES
        {
            get;
            set;
        }
        public bool FALLA_ELECTRICA
        {
            get;
            set;
        }
        public bool HABILITADO
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
        public bool PRESION_NOR
        {
            get;
            set;
        }
        public int SECO
        {
            get;
            set;
        }
        public bool SENTIDO
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
        public string data = string.Empty;
        public Socket GetConnection() {
            return sck;
        }
    }
}
