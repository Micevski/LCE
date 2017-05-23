using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE
{
    public class Wire : Element
    {
        private Element Source { get; set; }

        public override void Draw(Graphics g)
        {
            throw new NotImplementedException();
        }

        public override bool Selected(Point p)
        {
            throw new NotImplementedException();
        }

        protected override State evaluate()
        {
            return Source.Value;    
        }

    }
}
