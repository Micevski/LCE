﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE.Components
{
    [Serializable]
    public class AndGate : TwoInputGate
    {


        public AndGate(Point TopLeft, int Width, int Height) : base(TopLeft, Width, Height)
        {
            GateImage = AND_IMAGE;
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(GateImage, TopLeft.X, TopLeft.Y, Width, Height);
            base.Draw(g);
        }

        protected override State evaluate()
        {
            if(Input1.Source==null || Input2.Source == null)
            {
                return State.Undefined;
            }
            return State.And(Input1.Source.Value, Input2.Source.Value);
        }
    }
}
