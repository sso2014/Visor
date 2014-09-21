using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Core.VO
{
    public class Pivot : Equipo, IPaintable
    {
        public Pivot() {         
        }

        public int RADIO { get; set; }

        public void Drawer(Graphics g)
        {
            g.FillPie(Brushes.Green,
                new Rectangle(this.StartPoint.X, this.StartPoint.Y, this.StartSize.Width, this.StartSize.Height),
                270, this.Panel.ANGULO);
           
            //this.Lamina = new Lamina();
            //this.Lamina.Inicio = 270;
            //this.Lamina.Final = 300;
            //this.Lamina.StartPoint = new Point(this.StartPoint.X, this.StartPoint.Y);
            //this.Lamina.StartSize = new Size(this.StartSize.Width, this.StartSize.Height);

            //g.FillPie(Brushes.Blue,
            //    new Rectangle(0, 0, 200, 200),
            //    270, 300);
        }
        public void Move(MouseEventArgs e)
        {
            
        }
        public Point StartPoint { get; set; }
        public Size StartSize { get; set; }
        public List<Lamina> Lamina; 
    }
}
