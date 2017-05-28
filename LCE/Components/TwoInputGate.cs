using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE.Components
{
    public abstract class TwoInputGate : Component
    {
        public WireHandle Input1 { get; set; }
        public WireHandle Input2 { get; set; }

        public WireHandle Output { get; set; }

        public TwoInputGate(Point TopLeft, int Width, int Height) : base(TopLeft, Width, Height)
        {
            Input1 = new WireHandle(new Point(TopLeft.X, TopLeft.Y + 20));
            Input2 = new WireHandle(new Point(TopLeft.X, TopLeft.Y + 40));
            Output = new WireHandle(new Point(TopLeft.X + 60, TopLeft.Y + 30));
            Output.Source = this;
        }

        public override void Draw(Graphics g)
        {
            Input1.Draw(g);
            Input2.Draw(g);
            Output.Draw(g);
        }

        public override WireHandle HookOutHandle(Point p)
        {
            if (Output.Selected(p))
            {
                return Output;
            }
            return null;
        }

        public override WireHandle HookInHandle(Point p)
        {
            if (Input1.Selected(p))
            {
                return Input1;
            }
            else if (Input2.Selected(p))
            {
                return Input2;
            }
            return null;
        }

        public override void Move(Point delta)
        {
            base.Move(delta);
            Input1.Move(delta);
            Input2.Move(delta);
            Output.Move(delta);
        }
    }
}
