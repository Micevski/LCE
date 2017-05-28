using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE
{
    public class Wire : Element
    {
        public static int WIRE_WIDTH = 2;

        private Element Source { get; set; }

        private WireHandle initial;
        private List<WireHandle> inner;
        private WireHandle terminal;
        private Color color;

        public Wire(Element source, WireHandle initial, List<WireHandle> inner, WireHandle terminal)
        {
            Component = false;
            this.Source = source;
            this.initial = initial;
            this.inner = inner;
            this.terminal = terminal;
            this.color = Color.Black;
        }


        public override void Draw(Graphics g)
        {
            Pen pen;
            if(Value == State.Undefined)
            {
                pen = new Pen(Color.Magenta, WIRE_WIDTH);
            }else if(Value == State.True)
            {
                pen = new Pen(Color.Green, WIRE_WIDTH);
            }
            else
            {
                pen = new Pen(Color.Red, WIRE_WIDTH);
            }
            WireHandle from = initial;
            foreach (WireHandle to in inner)
            {
                g.DrawLine(pen, from.Location, to.Location);
                from = to;
            }
            g.DrawLine(pen, from.Location, terminal.Location);

            pen.Dispose();
        }

        public override bool Selected(Point p)
        {
            throw new NotImplementedException();
            return false;
        }

        protected override State evaluate()
        {
            return Source.Value;    
        }

    }
}
