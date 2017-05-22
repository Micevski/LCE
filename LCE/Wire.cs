using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCE
{
    public class Wire : Element
    {
        private Element Source { get; set; }


        protected override State evaluate()
        {
            return Source.Value;    
        }
    }
}
