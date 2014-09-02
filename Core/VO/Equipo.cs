using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.VO
{
    public abstract class Equipo
    {
        public Equipo() { 
        
        }

        public string ID
        {
            get;
            set;
        }
        public int TRAMOS
        {
            get;
            set;
        }
        public Panel Panel
        {
            get;
            set;
        }
    }
}
