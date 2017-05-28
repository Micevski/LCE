using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE.Components
{
    [Serializable]
    public class OutputPin : Component
    {
        public WireHandle Input { get; set; }

        public OutputPin(Point TopLeft, int Width, int Height) : base(TopLeft, Width, Height)
        {
            Input = new WireHandle(new Point(TopLeft.X, TopLeft.Y + 30));
        }

        public override void Draw(Graphics g)
        {
            if(Value == State.True)
            {
                g.DrawImage(LED_ON, TopLeft.X, TopLeft.Y, Width, Height);
            }
            else
            {
                g.DrawImage(LED_OFF, TopLeft.X, TopLeft.Y, Width, Height);
            }
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
