using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Numerics;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class JarvisMarch : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if (points.Count > 4)
            {
                var LM = new Point(0, 0);
                foreach (Point p in points)
                    if (p.X < LM.X)
                        LM = p;

                for (Point q = points[0], p = LM;
                    p != LM || q == points[0];
                    p = q)
                {
                    outPoints.Add(p);
                    foreach (Point pt in points)
                    {
                        Point p23 = new Point(q.X - pt.X, q.Y - pt.Y);
                        Point p12 = new Point(pt.X - p.X, pt.Y - p.Y);

                        if ((HelperMethods.CheckTurn(p12, p23) == Enums.TurnType.Colinear
                            && !HelperMethods.PointOnSegment(pt, p, q))
                            || HelperMethods.CheckTurn(p12, p23) == Enums.TurnType.Left)
                            q = pt;
                    }
                }
            }
            else outPoints = points;
        }

        public override string ToString()
        {
            return "Convex Hull - Jarvis March";
        }
    }
}