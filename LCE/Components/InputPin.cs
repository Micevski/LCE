using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LCE.Components
{
    [Serializable]
    public class InputPin : Component
    {
        private State Input { get; set; }
        public WireHandle Output { get; set; }

        public InputPin(Point TopLeft, int Width, int Height) : base(TopLeft, Width, Height)
        {
            Input = State.Undefined;
            Output = new WireHandle(new Point(TopLeft.X + 60, TopLeft.Y + 30));
            Output.Source = this;
        }

        public void Toggle()
        {
            Input = State.Toggle(Input);
        }

        protected override State evaluate()
        {
            return Input;
        }

        public override void Draw(Graphics g)
        {
            if (Input == State.True)
            {
                g.DrawImage(LED_ON, TopLeft.X, TopLeft.Y, Width, Height);
            }
            else
            {
                g.DrawImage(LED_OFF, TopLeft.X, TopLeft.Y, Width, Height);
            }
            Output.Draw(g);
        }

        public override void Move(Point delta)
        {
            base.Move(delta);
            Output.Move(delta);
        }

        public override WireHandle HookOutHandle(Point p)
        {
            if (Output.Selected(p))
            {
                return Output;
            }
            return null;
        }

    }
}
