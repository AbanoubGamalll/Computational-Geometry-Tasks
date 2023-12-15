using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class ExtremePoints : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            bool tmp;
            foreach (Point i in points)
            {
                tmp = true;
                foreach (Point j in points)
                {
                    if (j.Equals(i)) continue;
                    foreach (Point k in points)
                    {
                        if (k.Equals(i)) continue;
                        if (k.Equals(j)) continue;
                        foreach (Point l in points)
                        {
                            if (l.Equals(i)) continue;
                            else if (l.Equals(j)) continue;
                            else if (l.Equals(k)) continue;
                            else 
                                if (HelperMethods.PointInTriangle(i, j, k, l) != Enums.PointInPolygon.Outside)
                                    tmp = false;
                            if (tmp == false) break;
                        }
                        if (tmp == false) break;
                    }
                    if (tmp == false) break;
                }
                if (tmp == true && outPoints.Contains(i) == false) outPoints.Add(i);
            }
        }

        public override string ToString()
        {
            return "Convex Hull - Extreme Points";
        }
    }
}
