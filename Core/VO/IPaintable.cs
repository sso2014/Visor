using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Core.VO
{
    public interface IPaintable
    {
        void Drawer(Graphics g);
        void Move(MouseEventArgs e);
        Point StartPoint { get; set; }
        Size StartSize { get; set; }
    }
}
