using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class ExtremeSegments : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if (points.Count > 3)
            {
                foreach (var i in points)
                    foreach (var j in points)
                    {
                        if (i.Equals(j)) continue;
                        bool f = false, t = false;
                        foreach (Point k in points)
                        {
                            if (k.Equals(i)) continue;
                            else if (k.Equals(j)) continue;
                            if (HelperMethods.CheckTurn(new Line(i, j), k) == Enums.TurnType.Right) t = true;
                            else if (HelperMethods.CheckTurn(new Line(i, j), k) == Enums.TurnType.Left) f = true;
                            if (t == f) break;
                        }
                        if (f != t && outPoints.Contains(i) == false) outPoints.Add(i);
                    }

                for (int i = 0; i < outPoints.Count; i++)
                    foreach (var j in outPoints)
                    {
                        bool tmp = false;
                        foreach (var k in outPoints)
                            if (j == k) continue;
                            else
                                if (outPoints[i] != j && outPoints[i] != k && HelperMethods.PointOnSegment(outPoints[i], j, k))
                            {
                                outPoints.RemoveAt(i);
                                i--;
                                tmp = true;
                                break;
                            }
                        if (tmp) break;
                    }
            }
            else outPoints = points;
        }

        public override string ToString()
        {
            return "Convex Hull - Extreme Segments";
        }
    }
}