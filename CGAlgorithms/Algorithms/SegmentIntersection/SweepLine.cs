using System;
using System.Collections.Generic;
using System.Linq;

namespace CGAlgorithms.Algorithms.SegmentIntersection
{
<<<<<<< HEAD
    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace CGAlgorithms.Algorithms.SegmentIntersection
=======
    class SweepLine : Algorithm
>>>>>>> 961590000530149bc8338799a6eeff73b718b79a
    {
        class SweepLine : Algorithm
        {
<<<<<<< HEAD
            public override void Run(List<CGUtilities.Point> points, List<CGUtilities.Line> lines, List<CGUtilities.Polygon> polygons,
                ref List<CGUtilities.Point> outPoints, ref List<CGUtilities.Line> outLines, ref List<CGUtilities.Polygon> outPolygons)
            {
                // Combine points and lines for sorting
                List<EventPoint> eventPoints = new List<EventPoint>();

                foreach (var point in points)
                {
                    eventPoints.Add(new EventPoint(point, EventType.Point));
                }

                foreach (var line in lines)
                {
                    eventPoints.Add(new EventPoint(line.Start, EventType.Start, line));
                    eventPoints.Add(new EventPoint(line.End, EventType.End, line));
                }

                // Sort event points based on x-coordinate
                eventPoints.Sort();

                // Active set to keep track of lines intersecting the sweep line
                SortedSet<CGUtilities.Line> activeSet = new SortedSet<CGUtilities.Line>(new LineComparer());

                foreach (var eventPoint in eventPoints)
                {
                    switch (eventPoint.Type)
                    {
                        case EventType.Point:
                            // Handle point event
                            // You can implement specific logic for point events if needed
                            break;

                        case EventType.Start:
                            // Handle start event
                            activeSet.Add(eventPoint.Line);
                            break;

                        case EventType.End:
                            // Handle end event
                            activeSet.Remove(eventPoint.Line);
                            break;
                    }

                    // Check for intersection among active lines
                    CheckIntersections(activeSet.ToList(), ref outPoints, ref outLines);
                }

            }

            private void CheckIntersections(List<CGUtilities.Line> activeSet, ref List<CGUtilities.Point> outPoints, ref List<CGUtilities.Line> outLines)
            {
                for (int i = 0; i < activeSet.Count - 1; i++)
                {
                    for (int j = i + 1; j < activeSet.Count; j++)
                    {
                        if (DoIntersect(activeSet[i].Start, activeSet[i].End, activeSet[j].Start, activeSet[j].End))
                        {
                            // Intersection found, add it to the result
                            CGUtilities.Point intersectionPoint = ComputeIntersectionPoint(activeSet[i].Start, activeSet[i].End, activeSet[j].Start, activeSet[j].End);
                            outPoints.Add(intersectionPoint);
                            outLines.Add(new CGUtilities.Line(activeSet[i].Start, intersectionPoint));
                            outLines.Add(new CGUtilities.Line(activeSet[j].Start, intersectionPoint));
                        }
                    }
                }
            }

            // Check if two line segments intersect
            private bool DoIntersect(CGUtilities.Point p1, CGUtilities.Point q1, CGUtilities.Point p2, CGUtilities.Point q2)
            {
                int o1 = Orientation(p1, q1, p2);
                int o2 = Orientation(p1, q1, q2);
                int o3 = Orientation(p2, q2, p1);
                int o4 = Orientation(p2, q2, q1);

                if (o1 != o2 && o3 != o4)
                    return true;

                // Special cases
                if (o1 == 0 && OnSegment(p1, p2, q1)) return true;
                if (o2 == 0 && OnSegment(p1, q2, q1)) return true;
                if (o3 == 0 && OnSegment(p2, p1, q2)) return true;
                if (o4 == 0 && OnSegment(p2, q1, q2)) return true;

                return false;
            }

            // Find the orientation of three points (colinear, clockwise, or counterclockwise)
            private int Orientation(CGUtilities.Point p, CGUtilities.Point q, CGUtilities.Point r)
            {
                double val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);

                if (val == 0) return 0;  // colinear
                return (val > 0) ? 1 : 2; // clockwise or counterclockwise
            }

            // Check if point q lies on line segment p-r
            private bool OnSegment(CGUtilities.Point p, CGUtilities.Point q, CGUtilities.Point r)
            {
                return q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                       q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y);
            }

            // Compute the intersection point of two line segments
            private CGUtilities.Point ComputeIntersectionPoint(CGUtilities.Point p1, CGUtilities.Point q1, CGUtilities.Point p2, CGUtilities.Point q2)
            {
                double A1 = q1.Y - p1.Y;
                double B1 = p1.X - q1.X;
                double C1 = A1 * p1.X + B1 * p1.Y;

                double A2 = q2.Y - p2.Y;
                double B2 = p2.X - q2.X;
                double C2 = A2 * p2.X + B2 * p2.Y;

                double determinant = A1 * B2 - A2 * B1;

                double x = (B2 * C1 - B1 * C2) / determinant;
                double y = (A1 * C2 - A2 * C1) / determinant;

                return new CGUtilities.Point(x, y);
            }

            public override string ToString()
            {
                return "Sweep Line";
            }

            // Event types for sorting
            private enum EventType
            {
                Point,
                Start,
                End
            }

            // Class to represent event points
            private class EventPoint : IComparable<EventPoint>
            {
                public CGUtilities.Point Point { get; }
                public EventType Type { get; }
                public CGUtilities.Line Line { get; }

                public EventPoint(CGUtilities.Point point, EventType type, CGUtilities.Line line = null)
                {
                    Point = point;
                    Type = type;
                    Line = line;
                }

                public int CompareTo(EventPoint other)
                {
                    return Point.X.CompareTo(other.Point.X);
                }
            }

            // Comparer for sorting active set based on y-coordinate
            private class LineComparer : IComparer<CGUtilities.Line>
            {
                public int Compare(CGUtilities.Line x, CGUtilities.Line y)
                {
                    if (x == null || y == null)
                        throw new ArgumentNullException();

                    return x.Start.Y.CompareTo(y.Start.Y);
                }
            }
        }

=======
            // Combine points and lines for sorting
            List<EventPoint> eventPoints = new List<EventPoint>();

            foreach (var point in points)
            {
                eventPoints.Add(new EventPoint(point, EventType.Point));
            }

            foreach (var line in lines)
            {
                eventPoints.Add(new EventPoint(line.Start, EventType.Start, line));
                eventPoints.Add(new EventPoint(line.End, EventType.End, line));
            }

            // Sort event points based on x-coordinate
            eventPoints.Sort();

            // Active set to keep track of lines intersecting the sweep line
            SortedSet<CGUtilities.Line> activeSet = new SortedSet<CGUtilities.Line>(new LineComparer());

            foreach (var eventPoint in eventPoints)
            {
                switch (eventPoint.Type)
                {
                    case EventType.Point:
                        // Handle point event
                        // You can implement specific logic for point events if needed
                        break;

                    case EventType.Start:
                        // Handle start event
                        activeSet.Add(eventPoint.Line);
                        break;

                    case EventType.End:
                        // Handle end event
                        activeSet.Remove(eventPoint.Line);
                        break;
                }

                // Check for intersection among active lines
                CheckIntersections(activeSet.ToList(), ref outPoints, ref outLines);
            }
        
        }

        private void CheckIntersections(List<CGUtilities.Line> activeSet, ref List<CGUtilities.Point> outPoints, ref List<CGUtilities.Line> outLines)
        {
            for (int i = 0; i < activeSet.Count - 1; i++)
            {
                for (int j = i + 1; j < activeSet.Count; j++)
                {
                    if (DoIntersect(activeSet[i].Start, activeSet[i].End, activeSet[j].Start, activeSet[j].End))
                    {
                        // Intersection found, add it to the result
                        CGUtilities.Point intersectionPoint = ComputeIntersectionPoint(activeSet[i].Start, activeSet[i].End, activeSet[j].Start, activeSet[j].End);
                        outPoints.Add(intersectionPoint);
                        outLines.Add(new CGUtilities.Line(activeSet[i].Start, intersectionPoint));
                        outLines.Add(new CGUtilities.Line(activeSet[j].Start, intersectionPoint));
                    }
                }
            }
        }

        // Check if two line segments intersect
        private bool DoIntersect(CGUtilities.Point p1, CGUtilities.Point q1, CGUtilities.Point p2, CGUtilities.Point q2)
        {
            int o1 = Orientation(p1, q1, p2);
            int o2 = Orientation(p1, q1, q2);
            int o3 = Orientation(p2, q2, p1);
            int o4 = Orientation(p2, q2, q1);

            if (o1 != o2 && o3 != o4)
                return true;

            // Special cases
            if (o1 == 0 && OnSegment(p1, p2, q1)) return true;
            if (o2 == 0 && OnSegment(p1, q2, q1)) return true;
            if (o3 == 0 && OnSegment(p2, p1, q2)) return true;
            if (o4 == 0 && OnSegment(p2, q1, q2)) return true;

            return false;
        }

        // Find the orientation of three points (colinear, clockwise, or counterclockwise)
        private int Orientation(CGUtilities.Point p, CGUtilities.Point q, CGUtilities.Point r)
        {
            double val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);

            if (val == 0) return 0;  // colinear
            return (val > 0) ? 1 : 2; // clockwise or counterclockwise
        }

        // Check if point q lies on line segment p-r
        private bool OnSegment(CGUtilities.Point p, CGUtilities.Point q, CGUtilities.Point r)
        {
            return q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                   q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y);
        }

        // Compute the intersection point of two line segments
        private CGUtilities.Point ComputeIntersectionPoint(CGUtilities.Point p1, CGUtilities.Point q1, CGUtilities.Point p2, CGUtilities.Point q2)
        {
            double A1 = q1.Y - p1.Y;
            double B1 = p1.X - q1.X;
            double C1 = A1 * p1.X + B1 * p1.Y;

            double A2 = q2.Y - p2.Y;
            double B2 = p2.X - q2.X;
            double C2 = A2 * p2.X + B2 * p2.Y;

            double determinant = A1 * B2 - A2 * B1;

            double x = (B2 * C1 - B1 * C2) / determinant;
            double y = (A1 * C2 - A2 * C1) / determinant;

            return new CGUtilities.Point(x, y);
        }

        public override string ToString()
        {
            return "Sweep Line";
        }

        // Event types for sorting
        private enum EventType
        {
            Point,
            Start,
            End
        }

        // Class to represent event points
        private class EventPoint : IComparable<EventPoint>
        {
            public CGUtilities.Point Point { get; }
            public EventType Type { get; }
            public CGUtilities.Line Line { get; }

            public EventPoint(CGUtilities.Point point, EventType type, CGUtilities.Line line = null)
            {
                Point = point;
                Type = type;
                Line = line;
            }

            public int CompareTo(EventPoint other)
            {
                return Point.X.CompareTo(other.Point.X);
            }
        }

        // Comparer for sorting active set based on y-coordinate
        private class LineComparer : IComparer<CGUtilities.Line>
        {
            public int Compare(CGUtilities.Line x, CGUtilities.Line y)
            {
                if (x == null || y == null)
                    throw new ArgumentNullException();

                return x.Start.Y.CompareTo(y.Start.Y);
            }
        }
>>>>>>> 961590000530149bc8338799a6eeff73b718b79a
    }
}