using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE
{
    public class Selection
    { 
        public Point TopLeft { get; set; }
        public Point BottomRight { get; set; }

        private List<Element> Elements { get; set; }
        public bool IsEmpty { get { return Elements.Count == 0; } } 

    }
}
