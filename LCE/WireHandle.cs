using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE
{
    public class WireHandle
    {
        public static int Radius = 5;

        public Point Location { get; set; }
        public Element Source { get; set; }

        public WireHandle(Point p)
        {
            Location = p;
            Source = null;
        }

        public void Draw(Graphics g)
        {
            Brush br = new SolidBrush(Color.Blue);
            g.FillEllipse(br, Location.X - Radius, Location.Y - Radius, 2 * Radius, 2 * Radius);
            br.Dispose();
        }


        public bool Selected(Point p)
        {
            Debug.WriteLine(GeometryUtil.PointSquareDstance(Location, p));
            return (GeometryUtil.PointSquareDstance(Location, p)) <= Radius*Radius;
        }

        public void Move(Point delta)
        {
            Location = GeometryUtil.ApplyOffset(Location, delta);
        }
    }
}
