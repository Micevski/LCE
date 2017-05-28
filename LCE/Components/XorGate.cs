using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE.Components
{
    [Serializable]
    public class XorGate : TwoInputGate
    {
        public XorGate(Point TopLeft, int Width, int Height) : base(TopLeft, Width, Height)
        {
            GateImage = XOR_IMAGE;
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(GateImage, TopLeft.X, TopLeft.Y, Width, Height);
            base.Draw(g);
        }

        protected override State evaluate()
        {
            if (Input1.Source == null || Input2.Source == null)
            {
                return State.Undefined;
            }
            return State.Xor(Input1.Source.Value, Input2.Source.Value);
        }
    }
}
