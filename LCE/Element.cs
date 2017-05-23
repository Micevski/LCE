using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE
{
    public abstract class Element
    {
        public bool ShouldDelete { get; set; }
        public State Value { get { return evaluate(); } }

        public Element()
        {
            ShouldDelete = false;
        }

        public abstract void Draw(Graphics g);
        protected abstract State evaluate();
        public abstract bool Selected(Point p);

    }
}
