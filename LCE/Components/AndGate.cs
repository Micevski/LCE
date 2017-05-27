using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE.Components
{
    public class AndGate : Component
    {
        public AndGate(Point TopLeft, int Width, int Height) : base(TopLeft, Width, Height)
        {
        }

        public override void Draw(Graphics g)
        {
            Brush br = new SolidBrush(Color.Red);
            g.FillRectangle(br, TopLeft.X, TopLeft.Y, Width, Height);
            br.Dispose();
        }

        protected override State evaluate()
        {
            throw new NotImplementedException();
        }
    }
}
