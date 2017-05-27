using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE
{
    public class WireBuilder
    {
        public static Color BUILDER_COLOR = Color.Blue;
        public static int BUILDER_WIDTH = 3;

        public bool Free { get; set; }
        public Element Source { get; set; }
        public Point Initial { get; set; }
        public List<Point> Inner { get; set; }
        public Point Terminal { get; set; }

        public WireBuilder()
        {
            this.Dismiss();
        }

        public void Init(Element source, Point initial)
        {
            //this.Source = source;
            this.Initial = initial;
            this.Inner = new List<Point>();
            this.Free = false;
        }

        public void Dismiss()
        {
            this.Source = null;
            this.Initial = Point.Empty;
            this.Inner = null;
            this.Terminal = Point.Empty;
            this.Free = true;
        }

        public void AddPoint(Point p)
        {
            this.Inner.Add(p);
        }

        public Wire Finalize(Point terminal)
        {
            this.Terminal = terminal;
            Wire wire = new Wire(this.Source, this.Initial, this.Inner, this.Terminal);
            this.Dismiss();
            return wire;
        }

        public void Draw(Graphics g)
        {
            if (this.Free)
            {
                return;
            }
            Pen pen = new Pen(BUILDER_COLOR, BUILDER_WIDTH);
            Point from = Initial;
            foreach (Point to in Inner)
            {
                
                g.DrawLine(pen, from, to);
                from = to;
            }
            g.DrawLine(pen, from, Terminal);

            pen.Dispose();
        }

    }
}
