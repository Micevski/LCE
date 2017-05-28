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

        public Component ClickSelect(Point mouse)
        {
            // Starts from end of list to select the element that is drawn last (in front) 
            // in case of overlaping elements
            for(int i = Count-1; i >= 0; i--)
            {
                if (!Elements[i].Component)
                    continue;
                if (Elements[i].Selected(mouse))
                {
                    return (Component)Elements[i];
                }
            }
            return null;
        }

        // Returns the Output WireHandle on cursor or null
        public WireHandle ClickedOutHandle(Point p)
        {
            foreach (Element e in Elements)
            {
                WireHandle w = e.HookOutHandle(p);
                if(w != null)
                {
                    return w;
                }
            }
            return null;
        }

        // Retruns the Input WireHandle on cursor or null
        public WireHandle ClickedInHandle(Point p)
        {
            foreach (Element e in Elements)
            {
                WireHandle w = e.HookInHandle(p);
                if (w != null)
                {
                    return w;
                }
            }
            return null;
        }

    }
}
