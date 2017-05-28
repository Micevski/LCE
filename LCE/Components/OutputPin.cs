using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE.Components
{
    public class OutputPin : Component
    {
        public WireHandle Input { get; set; }

        public OutputPin(Point TopLeft, int Width, int Height) : base(TopLeft, Width, Height)
        {
            Input = new WireHandle(new Point(TopLeft.X, TopLeft.Y + 25));
        }

        public override void Draw(Graphics g)
        {
            Color c;
            if(Value == State.Undefined)
            {
                c = Color.Magenta;
            }
            else if(Value == State.True)
            {
                c = Color.Green;
            }
            else
            {
                c = Color.Red;
            }
            Brush br = new SolidBrush(c);
            g.FillEllipse(br, TopLeft.X, TopLeft.Y, 50, 50);
            br.Dispose();
            Input.Draw(g);
        }

        protected override State evaluate()
        {
            if(Input.Source == null)
            {
                return State.Undefined;
            }
            return Input.Source.Value;
        }

        public override void Move(Point delta)
        {
            base.Move(delta);
            Input.Move(delta);
        }

        public override WireHandle HookInHandle(Point p)
        {
            if (Input.Selected(p))
            {
                return Input;
            }
            return null;
        }
    }
}
