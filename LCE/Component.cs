using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE
{
    [Serializable]
    public abstract class Component : Element
    {
        public static Image AND_IMAGE;
        public static Image OR_IMAGE;
        public static Image XOR_IMAGE;
        public static Image NOT_IMAGE;
        public static Image LED_ON;
        public static Image LED_OFF;

        static Component()
        {
            AND_IMAGE = Properties.Resources.AND;
            OR_IMAGE = Properties.Resources.OR;
            XOR_IMAGE = Properties.Resources.XOR;
            NOT_IMAGE = Properties.Resources.NOT;
            LED_ON = Properties.Resources.LED_ON;
            LED_OFF = Properties.Resources.LED_OFF;
        }

        public Point TopLeft { get; set; }
        protected int Width { get; set; }
        protected int Height { get; set; }

        public Image GateImage { get; set; }

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
