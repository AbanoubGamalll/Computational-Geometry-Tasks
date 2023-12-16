using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class GrahamScan : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if (points.Count > 3)
            {
                points = points.OrderBy(p => p.X).ToList();
                points = points.OrderBy(p => p.Y).ToList();

                outPoints.Add(points[0]);
                points.RemoveAt(0);

                var Point_Angles = new List<KeyValuePair<Point, double>>();

                foreach (var p in points)
                    Point_Angles.Add(new KeyValuePair<Point, double>(p, Math.Atan2(p.Y - outPoints[0].Y, p.X - outPoints[0].X) / Math.PI));

                Point_Angles = Point_Angles.OrderBy(x => x.Value).ToList();

                outPoints.Add(Point_Angles[0].Key);
                Point_Angles.RemoveAt(0);
                Line line;
                foreach (var angle in Point_Angles)
                {
                    line = new Line(outPoints[outPoints.Count - 2], outPoints[outPoints.Count - 1]);
                    while (HelperMethods.CheckTurn(line, angle.Key) == Enums.TurnType.Right
                        || HelperMethods.CheckTurn(line, angle.Key) == Enums.TurnType.Colinear)
                    {
                        outPoints.RemoveAt(outPoints.Count - 1);
                        if (outPoints.Count < 2) break;
                        else line = new Line(outPoints[outPoints.Count - 2], outPoints[outPoints.Count - 1]);
                    }
                    outPoints.Add(angle.Key);
                }
            }
            else outPoints = points;
        }

        public override string ToString()
        {
            return "Convex Hull - Graham Scan";
        }
    }
}