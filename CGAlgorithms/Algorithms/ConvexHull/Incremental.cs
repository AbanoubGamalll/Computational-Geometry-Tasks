using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class Incremental : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            points.Sort((point1, point2) =>
            {
                if (point1.X == point2.X) return point1.Y.CompareTo(point2.Y);
                else return point1.X.CompareTo(point2.X);
            });


            for (int i = 0; i < points.Count; i++)
                for (int j = i + 1; j < points.Count - 1; j++)
                    if (points[i].Equals(points[j]))
                        points.RemoveAt(j);

            int[] nxt = new int[points.Count];
            int[] prv = new int[points.Count];

            for (int i = 0, t; i < points.Count; i++)
            {
                if (i < 2) nxt[i] = prv[i] = i ^ 1;
                else
                {
                    if (points[i].Y > points[i - 1].Y)
                    {
                        prv[i] = prv[i - 1];
                        nxt[i] = i - 1;
                    }
                    else
                    {
                        prv[i] = i - 1;
                        nxt[i] = nxt[i - 1];
                    }

                    nxt[prv[i]] = prv[nxt[i]] = i;
                    while (HelperMethods.CrossProduct(HelperMethods.GetVector(new Line(points[nxt[nxt[i]]], points[nxt[i]])),
                        HelperMethods.GetVector(new Line(points[nxt[i]], points[i]))) <= 0)
                    {
                        t = nxt[nxt[i]];
                        prv[t] = i;
                        nxt[i] = t;
                    }
                    while (HelperMethods.CrossProduct(HelperMethods.GetVector(new Line(points[i], points[prv[i]])), HelperMethods.GetVector(new Line(points[prv[i]], points[prv[prv[i]]]))) <= 0)
                    {
                        t = prv[prv[i]];
                        prv[i] = t;
                        nxt[t] = i;
                    }
                }
            }

            outPoints.Add(points[0]);
            for (int i = nxt[0]; i != 0 && (outPoints.Count < points.Count); i = nxt[i])
                outPoints.Add(points[i]);
        }

        public override string ToString()
        {
            return "Convex Hull - Incremental";
        }
    }
}