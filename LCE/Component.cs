using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE
{
    public abstract class Component : Element
    {    

        public Point TopLeft { get; set; }
        protected int Width { get; set; }
        protected int Height { get; set; }

        public Component(Point TopLeft, int Width, int Height)
        {
            Component = true;
            this.TopLeft = TopLeft;
            this.Width = Width;
            this.Height = Height;
        }

        public override bool Selected(Point p)
        {
            return (p.X >= TopLeft.X && p.Y >= TopLeft.Y && p.X <= TopLeft.X + Width && p.Y <= TopLeft.Y + Height);
        }

        public virtual void Move(Point delta)
        {
            TopLeft = new Point(TopLeft.X + delta.X, TopLeft.Y + delta.Y);
            //TopLeft = Simulator.GridSnap(TopLeft);
        }

    }
}
