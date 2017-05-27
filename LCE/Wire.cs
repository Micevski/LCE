using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE
{
    public class Wire : Element
    {
        public static int WIRE_WIDTH = 3;

        private Element Source { get; set; }

        private Point initial;
        private List<Point> inner;
        private Point terminal;
        private Color color;

        public Wire(Element source, Point initial, List<Point> inner, Point terminal)
        {
            this.Source = source;
            this.initial = initial;
            this.inner = inner;
            this.terminal = terminal;
        }


        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(color, WIRE_WIDTH);
            Point from = initial;
            foreach (Point to in inner)
            {
                g.DrawLine(pen, from, to);
                from = to;
            }
            g.DrawLine(pen, from, terminal);

            pen.Dispose();
        }

        public override bool Selected(Point p)
        {
            throw new NotImplementedException();
        }

        protected override State evaluate()
        {
            return Source.Value;    
        }

    }
}
