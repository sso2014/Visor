using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Core
{
    public interface IPaintable
    {
        void Drawer(Graphics g);
        void Move(MouseEventArgs e);
    }
}
