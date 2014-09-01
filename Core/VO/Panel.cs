using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.VO
{
    public class Panel
    {

        public Panel() { 
        
        }

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
        public int ID
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
            throw new System.NotImplementedException();
        }
        public void Parada()
        {
            throw new System.NotImplementedException();
        }
        public void Foward()
        {
            throw new System.NotImplementedException();
        }
        public void Reversa()
        {
            throw new System.NotImplementedException();
        }
    }
}
