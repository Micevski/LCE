using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LCE
{
    public class GeometryUtil
    {
        public static int PointSquareDstance(Point a, Point b)
        {
            return (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
        }

        public static Point GetOffset(Point topLeft, Point location)
        {
            return new Point(topLeft.X - location.X, topLeft.Y -location.Y);
        }

        public static Point ApplyOffset(Point point, Point offset)
        {
            return new Point(point.X + offset.X, point.Y + offset.Y);
        }
    }
}
