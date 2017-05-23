using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE
{
    public class Scene
    {
        public int Count { get { return Elements.Count; } }

        private List<Element> Elements { get; set; }

        public Scene()
        {
            Elements = new List<Element>();
        }

        public void AddElement(Element e)
        {
            Elements.Add(e);
        }

        public void Draw(Graphics g)
        {
            foreach(Element e in Elements)
            {
                e.Draw(g);
            }
        }

        public Element ClickSelect(Point mouse)
        {
            // Starts from end of list to select the element that is drawn last (in front) 
            // in case of overlaping elements
            for(int i = Count-1; i >= 0; i--)
            {
                if (Elements[i].Selected(mouse))
                {
                    return Elements[i];
                }
            }
            return null;
        }

    }
}
