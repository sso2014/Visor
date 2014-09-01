using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.VO
{
    public class Frontal : Equipo
    {
        public Frontal() { 
        }

        public Panel Panel
        {
            get;
            set;

        }
        public int MOTOR
        {
            get;
            set;
        }
    }
}
