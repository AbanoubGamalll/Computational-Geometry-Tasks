using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.PolygonTriangulation
{
    public class SubtractingEars : Algorithm
    {
        public override void Run(List<CGUtilities.Point> points, List<CGUtilities.Line> lines, List<CGUtilities.Polygon> polygons, ref List<CGUtilities.Point> outPoints, ref List<CGUtilities.Line> outLines, ref List<CGUtilities.Polygon> outPolygons)
        {
            // Ensure that the input polygon list is not null
            if (polygons == null)
                throw new ArgumentNullException(nameof(polygons));

            // Process each input polygon
            foreach (var inputPolygon in polygons)
            {
                // Check if the polygon has at least 3 vertices (lines)
                if (inputPolygon.lines.Count < 3)
                {
                    // If not, add the original polygon to the output without modification
                    outPolygons.Add(inputPolygon);
                }
                else
                {
                    // Subtract ears from the polygon
                    List<CGUtilities.Line> subtractedPolygon = SubtractEars(inputPolygon.lines);

                    // Create a new polygon with the subtracted ears
                    CGUtilities.Polygon resultPolygon = new CGUtilities.Polygon(subtractedPolygon);

                    // Add the resulting polygon to the output
                    outPolygons.Add(resultPolygon);
                }
            }
        }

        private List<CGUtilities.Line> SubtractEars(List<CGUtilities.Line> polygonEdges)
        {
            List<CGUtilities.Line> resultPolygon = new List<CGUtilities.Line>(polygonEdges);

            int edgeCount = resultPolygon.Count;

            for (int i = 0; i < edgeCount; i++)
            {
                int prevIndex = (i - 1 + edgeCount) % edgeCount;
                int nextIndex = (i + 1) % edgeCount;

                // Check if the vertices at both ends of the edge are convex
                if (IsConvex(resultPolygon[prevIndex].End, resultPolygon[i].Start, resultPolygon[nextIndex].Start) &&
                    IsConvex(resultPolygon[prevIndex].Start, resultPolygon[i].End, resultPolygon[nextIndex].End))
                {
                    // Remove the convex edge (ear)
                    resultPolygon.RemoveAt(i);

                    // Adjust the loop variables after removal
                    i--;
                    edgeCount--;

                    // Restart the loop to check the new edge at the same index
                }
            }

            return resultPolygon;
        }

        private bool IsConvex(CGUtilities.Point a, CGUtilities.Point b, CGUtilities.Point c)
        {
            // Check if the orientation is counterclockwise (cross product > 0)
            double crossProduct = (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
            return crossProduct > 0;
        }

        public override string ToString()
        {
            return "Subtracting Ears";
        }
    }
}
