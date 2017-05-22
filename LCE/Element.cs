using System;
using System.Collections.Generic;
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


        protected abstract State evaluate();

    }
}
