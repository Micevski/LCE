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
        public WireHandle Initial { get; set; }
        public List<WireHandle> Inner { get; set; }
        public WireHandle Terminal { get; set; }
        public Point EndPosition { get; set; }

        public WireBuilder()
        {
            this.Dismiss();
        }

        public void Init(Element source, WireHandle initial)
        {
            this.Source = source;
            this.Initial = initial;
            this.Inner = new List<WireHandle>();
            this.Free = false;
        }

        public void Dismiss()
        {
            this.Source = null;
            this.Initial = null;
            this.Inner = null;
            this.Terminal = null;
            this.Free = true;
        }

        public void AddPoint(Point p)
        {
            this.Inner.Add(new WireHandle(p));
        }

        public Wire Finalize(WireHandle terminal)
        {
            this.Terminal = terminal;
            this.Terminal.Source = this.Source;
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
            WireHandle from = Initial;
            foreach (WireHandle to in Inner)
            {
                
                g.DrawLine(pen, from.Location, to.Location);
                from = to;
            }
            g.DrawLine(pen, from.Location, EndPosition);

            pen.Dispose();
        }

    }
}
