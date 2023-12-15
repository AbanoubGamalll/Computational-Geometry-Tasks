using CGUtilities;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class DivideAndConquer : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if (points.Count < 4) outPoints = points;
            else
            {
                points.Sort((point1, point2) =>
              {
                  if (point1.X == point2.X) return point1.Y.CompareTo(point2.Y);
                  return point1.X.CompareTo(point2.X);
              });
                outPoints = recurseve(points);
            }
        }
        private List<Point> recurseve(List<Point> point)
        {
            if (point.Count < 2) return point;
            else
            {
                var Left_Points = new List<Point>();
                var Right_Points = new List<Point>();
                for (int i = 0; i < point.Count; i++)
                    if (i < point.Count / 2) Left_Points.Add(point[i]);
                    else Right_Points.Add(point[i]);
                return merge(recurseve(Left_Points), recurseve(Right_Points));
            }
        }
        public List<Point> merge(List<Point> Left_Points, List<Point> Right_Points)
        {

            int Max_Right_Point = 0;
            for (int i = 1; i < Right_Points.Count; i++)
                if (Right_Points[i].X < Right_Points[Max_Right_Point].X || (Right_Points[i].X == Right_Points[Max_Right_Point].X && Right_Points[i].Y < Right_Points[Max_Right_Point].Y))
                    Max_Right_Point = i;

            int Max_Left_Point = 0;
            for (int i = 1; i < Left_Points.Count; i++)
                if (Left_Points[i].X > Left_Points[Max_Left_Point].X || (Left_Points[i].X == Left_Points[Max_Left_Point].X && Left_Points[i].Y > Left_Points[Max_Left_Point].Y))
                    Max_Left_Point = i;

            int tmp_Max_Left_Point = Max_Left_Point;
            int tmp_Max_Right_Point = Max_Right_Point;
            int Next_Left_Point = (tmp_Max_Left_Point + 1) % Left_Points.Count;
            int Previes_Right_Point = (Right_Points.Count + tmp_Max_Right_Point - 1) % Right_Points.Count;

            bool check_Found = false;
            while (!check_Found)
            {
                check_Found = true;

                while (HelperMethods.CheckTurn(new Line(Right_Points[tmp_Max_Right_Point], Left_Points[tmp_Max_Left_Point]), Left_Points[Next_Left_Point]) == Enums.TurnType.Right)
                {
                    check_Found = false;
                    tmp_Max_Left_Point = Next_Left_Point;
                    Next_Left_Point = (tmp_Max_Left_Point + 1) % Left_Points.Count;
                }

                if (check_Found && (HelperMethods.CheckTurn(new Line(Right_Points[tmp_Max_Right_Point], Left_Points[tmp_Max_Left_Point]), Left_Points[Next_Left_Point]) == Enums.TurnType.Colinear)) tmp_Max_Left_Point = Next_Left_Point;
                Next_Left_Point = (tmp_Max_Left_Point + 1) % Left_Points.Count;

                while (HelperMethods.CheckTurn(new Line(Left_Points[tmp_Max_Left_Point], Right_Points[tmp_Max_Right_Point]), Right_Points[Previes_Right_Point]) == Enums.TurnType.Left)
                {
                    check_Found = false;
                    tmp_Max_Right_Point = Previes_Right_Point;
                    Previes_Right_Point = (Right_Points.Count + tmp_Max_Right_Point - 1) % Right_Points.Count;
                }

                if (check_Found && (HelperMethods.CheckTurn(new Line(Left_Points[tmp_Max_Left_Point], Right_Points[tmp_Max_Right_Point]), Right_Points[Previes_Right_Point]) == Enums.TurnType.Colinear))
                    tmp_Max_Right_Point = Previes_Right_Point;
                Previes_Right_Point = (Right_Points.Count + tmp_Max_Right_Point - 1) % Right_Points.Count;
            }


            int D_Left_Point = Max_Left_Point;
            int D_Right_Point = Max_Right_Point;
            int pre_Left_Point = (Left_Points.Count + D_Left_Point - 1) % Left_Points.Count;
            int Next_Right_Point = (Max_Right_Point + 1) % Right_Points.Count;

            check_Found = false;
            while (!check_Found)
            {
                check_Found = true;
                while (HelperMethods.CheckTurn(new Line(Right_Points[D_Right_Point], Left_Points[D_Left_Point]), Left_Points[pre_Left_Point]) == Enums.TurnType.Left)
                {
                    check_Found = false;
                    D_Left_Point = pre_Left_Point;
                    pre_Left_Point = (Left_Points.Count + D_Left_Point - 1) % Left_Points.Count;
                }
                if (check_Found && (HelperMethods.CheckTurn(new Line(Right_Points[D_Right_Point], Left_Points[D_Left_Point]), Left_Points[pre_Left_Point]) == Enums.TurnType.Colinear))
                    D_Left_Point = pre_Left_Point;

                pre_Left_Point = (Left_Points.Count + D_Left_Point - 1) % Left_Points.Count;

                while (HelperMethods.CheckTurn(new Line(Left_Points[D_Left_Point], Right_Points[D_Right_Point]), Right_Points[Next_Right_Point]) == Enums.TurnType.Right)
                {
                    check_Found = false;
                    D_Right_Point = Next_Right_Point;
                    Next_Right_Point = (D_Right_Point + 1) % Right_Points.Count;
                }

                if (check_Found && (HelperMethods.CheckTurn(new Line(Left_Points[D_Left_Point], Right_Points[D_Right_Point]), Right_Points[Next_Right_Point]) == Enums.TurnType.Colinear)) D_Right_Point = Next_Right_Point;
                Next_Right_Point = (D_Right_Point + 1) % Right_Points.Count;
            }

            var outPoints = new List<Point>();

            if (!outPoints.Contains(Left_Points[tmp_Max_Left_Point])) outPoints.Add(Left_Points[tmp_Max_Left_Point]);
            int tmp = tmp_Max_Left_Point;
            while (tmp != D_Left_Point)
            {
                tmp = (tmp + 1) % Left_Points.Count;
                if (!outPoints.Contains(Left_Points[tmp])) outPoints.Add(Left_Points[tmp]);
            }
            tmp = D_Right_Point;
            if (!outPoints.Contains(Right_Points[D_Right_Point])) outPoints.Add(Right_Points[D_Right_Point]);

            while (tmp != tmp_Max_Right_Point)
            {
                tmp = (tmp + 1) % Right_Points.Count;
                if (!outPoints.Contains(Right_Points[tmp])) outPoints.Add(Right_Points[tmp]);
            }

            return outPoints;
        }

        public override string ToString()
        {
            return "Convex Hull - Divide & Conquer";
        }

    }
}