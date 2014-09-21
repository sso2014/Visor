using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Core.VO
{
    public class Lamina:IPaintable
    {
        public Lamina() { }
        public int Inicio { get; set;}
        public int Final { get; set; }
        public void Drawer(Graphics g)
        {
            g.FillPie(Brushes.Blue,
                new Rectangle(StartPoint.X, StartPoint.Y,
                    StartSize.Width, StartSize.Height),
                    Inicio, Final);
        }
        public void Move(System.Windows.Forms.MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
        public System.Drawing.Point StartPoint
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public System.Drawing.Size StartSize
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
