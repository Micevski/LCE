using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LCE.Components
{
    public class InputPin : Component
    {
        private State Input { get; set; }

        public InputPin(Point TopLeft, int Width, int Height) : base(TopLeft, Width, Height)
        {
            Input = State.Undefined;
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
            Color color = Color.White;
            if(Input == State.True)
            {
                color = Color.Green;
            }
            else if(Input == State.False)
            {
                color = Color.Red;
            }else
            {
                color = Color.Gray;
            }
            Brush br = new SolidBrush(color);
            g.FillRectangle(br, TopLeft.X, TopLeft.Y, Width, Height);
            br.Dispose();
        }
    }
}
