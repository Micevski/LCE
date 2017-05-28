using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE.Components
{
    [Serializable]
    public class NotGate : OneInputGate
    {
        public NotGate(Point TopLeft, int Width, int Height) : base(TopLeft, Width, Height)
        {
            GateImage = NOT_IMAGE;
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(GateImage, TopLeft.X, TopLeft.Y, Width, Height);
            base.Draw(g);
        }

        protected override State evaluate()
        {
            if(Input.Source == null)
            {
                return State.Undefined;
            }
            return State.Not(Input.Source.Value);
        }
    }
}
