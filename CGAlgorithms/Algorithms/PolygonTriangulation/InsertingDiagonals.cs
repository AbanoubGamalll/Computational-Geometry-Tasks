using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;

namespace CGAlgorithms.Algorithms.PolygonTriangulation
{
    // Cheating 
    class InsertingDiagonals : Algorithm
    {
        public override void Run(List<CGUtilities.Point> points, List<CGUtilities.Line> lines, List<CGUtilities.Polygon> polygons, ref List<CGUtilities.Point> outPoints, ref List<CGUtilities.Line> ListOfLines, ref List<CGUtilities.Polygon> outPolygons)
        {
            // output el  line list
            ListOfLines = new List<Line>();
            // Create a Polygon mn el e input lines
            Polygon poly = new Polygon(lines);

            //asks tarteb el lines lw el poly m4 cck
            poly = ReverseAndSwapPoints(poly);
            //  insert diagonals ll poly
            Ins_Dia(poly, ref ListOfLines);
        }

        // asks tarteb el lines lw el poly m4 cck
        public Polygon ReverseAndSwapPoints(Polygon pol)
        {
            // ashof pos_Or_Neg of the poly
            double pos_Or_Neg = 0;
            foreach (var line in pol.lines)
            {
                pos_Or_Neg += (line.End.X - line.Start.X) * (line.End.Y + line.Start.Y);
            }
            pos_Or_Neg /= 2;

            // a3ks tarteb lines w abdl begin and nehayet_el_point points lw pos_Or_Neg is neg
            if (pos_Or_Neg > 0)
            {
                pol.lines.Reverse();
                foreach (var line in pol.lines)
                {
                    Point item = line.Start;
                    line.Start = line.End;
                    line.End = item;
                }
            }

            return pol;
        }


        //  insert diagonals ll poly
        private void Ins_Dia(Polygon polygon, ref List<Line> el_diagonals)
        {     // lw el poly feh  3 or a2el vertices, m4 ha3rf a3ml diagonals
            if (polygon.lines.Count <= 3)
                return;
            // adwer 3ala convex vertex feh el  poly
            int NumOfPoints = 0;
            while (!check_is_it_convex(polygon, NumOfPoints))
            {
                NumOfPoints = (NumOfPoints + 1) % polygon.lines.Count;
            }
            // a5ly indices elly fattet  w elly gaya mn el  vertices
            int elly_b3dy = (NumOfPoints + 1) % polygon.lines.Count;
            int elly_2bly = (NumOfPoints - 1 + polygon.lines.Count) % polygon.lines.Count;

            //a5ly el vertices ll convex vertex w el etnen  adjacent vertices
            Point firstPo = polygon.lines[elly_2bly].Start;
            Point sec = polygon.lines[NumOfPoints].Start;
            Point thir = polygon.lines[elly_b3dy].Start;
            // adwer 3ala  points gwa el triangle mkowna  mn el convex vertex and w kman tkon adjacent vertices
            List<Point> Pointts = polygon.lines
                .Select(line => line.Start)
                .Where(po => HelperMethods.PointInTriangle(po, firstPo, sec, thir) == Enums.PointInPolygon.Inside)
                .ToList();

            Line Ins_dia;

            // adwer 3ala el  point gwa the triangle ab3d mn el line connecting the first and third vertices
            if (Pointts.Count != 0)
            {
                int akbr_distance = Pointts
            .Select((point, index) => new { rqm_el_point = index, ab3d_point = between_distance(firstPo, thir, point) })
            .OrderByDescending(item => item.ab3d_point)
            .First()
            .rqm_el_point;

                Ins_dia = new Line(sec, Pointts[akbr_distance]);

            }
            else
            {
                // lw mafe4 points gwaa el triangle, a3ml a Ins_dia ben el first and third vertices
                Ins_dia = new Line(firstPo, thir);
            }

            el_diagonals.Add(Ins_dia);
            // afsel el poly into two b el  Ins_dia elly btt3ml and arg3 a3mel recursive  l kol poly so8yer
            Polygon poly1, poly2;
            List<Line> l1 = new List<Line>();
            List<Line> l2 = new List<Line>();
            int begin = 0, nehayet_el_point = 0;

            for (int i = 0; i < polygon.lines.Count; i++)
            {
                if (polygon.lines[i].Start.Equals(Ins_dia.Start))
                    begin = i;

                if (polygon.lines[i].Start.Equals(Ins_dia.End))
                    nehayet_el_point = i;
            }

            int first_to_insert = begin;
            while (first_to_insert != nehayet_el_point)
            {
                l1.Add(polygon.lines[first_to_insert]);
                first_to_insert = (first_to_insert + 1) % polygon.lines.Count;
            }
            l1.Add(new Line(Ins_dia.End, Ins_dia.Start));
            poly1 = new Polygon(l1);

            first_to_insert = nehayet_el_point;
            while (first_to_insert != begin)
            {
                l2.Add(polygon.lines[first_to_insert]);
                first_to_insert = (first_to_insert + 1) % polygon.lines.Count;
            }
            l2.Add(new Line(Ins_dia.Start, Ins_dia.End));
            poly2 = new Polygon(l2);
            //hene b recursive 
            Ins_Dia(poly1, ref el_diagonals);
            Ins_Dia(poly2, ref el_diagonals);
        }

        //lw el  vertex btkon convex
        public bool check_is_it_convex(Polygon p, int just_point)
        {
            int elly_2bly = (just_point - 1 + p.lines.Count) % p.lines.Count;
            int elly_b3dy = (just_point + 1) % p.lines.Count;

            Point one = p.lines[elly_2bly].Start;
            Point two = p.lines[just_point].Start;
            Point three = p.lines[elly_b3dy].Start;

            return HelperMethods.CheckTurn(new Line(one, two), three) == Enums.TurnType.Left;
        }


        //  a7seb  el  between_distance ben  a point and a line
        public double between_distance(Point p1, Point p2, Point p0)
        {
            double numerator1 = (p2.X - p1.X) * (p1.Y - p0.Y);
            double numerator2 = (p1.X - p0.X) * (p2.Y - p1.Y);
            double denominator = Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));

            double result = Math.Abs(numerator1 - numerator2) / denominator;

            return result;
        }



        public override string ToString()
        {
            return "Inserting Diagonals";
        }
    }
}