using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class QuickHull : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
                Point min_x_point = points[0],
                    max_x_point = points[0],
                    min_y_point = points[0],
                    max_y_point = points[0];

                foreach (Point point in points)
                {
                    if (point.X < min_x_point.X) min_x_point = point;
                    if (point.X > max_x_point.X) max_x_point = point;
                    if (point.Y < min_y_point.Y) min_y_point = point;
                    if (point.Y > max_y_point.Y) max_y_point = point;
                }

                if (!outPoints.Contains(min_x_point)) outPoints.Add(min_x_point);
                if (!outPoints.Contains(max_x_point)) outPoints.Add(max_x_point);
                if (!outPoints.Contains(min_y_point)) outPoints.Add(min_y_point);
                if (!outPoints.Contains(max_y_point)) outPoints.Add(max_y_point);

                recurceve(points, ref outPoints, min_y_point, max_x_point);
                recurceve(points, ref outPoints, max_x_point, max_y_point);
                recurceve(points, ref outPoints, max_y_point, min_x_point);
                recurceve(points, ref outPoints, min_x_point, min_y_point);

        }
        private void recurceve(List<Point> points, ref List<Point> outPoints, Point point1, Point point2, int point_side = -1)
        {
            double max_dist = 0;
            Point my_point = null;
            foreach (var point in points)
            {
                double t = ((point.Y - point1.Y) * (point2.X - point1.X) - (point2.Y - point1.Y) * (point.X - point1.X));
                double tmp = Math.Abs(t);
                int myside = t > 0 ? 1 : -1;
                if (myside == point_side)
                    if (tmp > max_dist)
                    {
                        my_point = point;
                        max_dist = tmp;
                    }
            }

            if (my_point != null)
            {
                outPoints.Add(my_point);
                recurceve(points, ref outPoints, my_point, point2);
                recurceve(points, ref outPoints, point1, my_point);
            }
        }


        public override string ToString()
        {
            return "Convex Hull - Quick Hull";
        }
    }
}
