using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE.Components
{
    [Serializable]
    public abstract class OneInputGate : Component
    {
        public WireHandle Input { get; set; }
        public WireHandle Output { get; set; }

        public OneInputGate(Point TopLeft, int Width, int Height) : base(TopLeft, Width, Height)
        {
            Input = new WireHandle(new Point(TopLeft.X, TopLeft.Y + 30));
            Output = new WireHandle(new Point(TopLeft.X + 60, TopLeft.Y + 30));
            Output.Source = this;
        }

        public override void Draw(Graphics g)
        {
            Input.Draw(g);
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
            if (Input.Selected(p))
            {
                return Input;
            }
            return null;
        }

        public override void Move(Point delta)
        {
            base.Move(delta);
            Input.Move(delta);
            Output.Move(delta);
        }

    }
}
