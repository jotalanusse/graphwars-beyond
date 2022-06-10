using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Algorithms : MonoBehaviour
{
    /*    public List<Vector2> DouglasPeucker(List<Vector2> points, float epsilon)
        {
            // Find the point with the maximum distance
            float dmax = 0;
            int index = 0;
            int end = points.Count - 1;
            for (int i = 0; i < end; i += 1)
            {

                Vector2 startingPoint = points[0];
                Vector2 endPoint = points[end];
                Vector2 direction = endPoint - startingPoint;

                float d = PerpendicularDistance(points[i], new Ray2D(startingPoint, direction.normalized));

                if (d > dmax)
                {
                    index = i;
                    dmax = d;
                }
            }

            List<Vector2> resultPoints = new List<Vector2>();

            // If max distance is greater than epsilon, recursively simplify
            if (dmax > epsilon)
            {
                // Recursive call
                List<Vector2> recResults1 = DouglasPeucker(points.GetRange(0, index - 1), epsilon);
                List<Vector2> recResults2 = DouglasPeucker(points.GetRange(index, end), epsilon);

                // Build the result list
                resultPoints.AddRange(recResults1.GetRange(0, recResults1.Count - 2));
                resultPoints.AddRange(recResults2.GetRange(0, recResults2.Count - 1));
            }
            else
            {
                resultPoints.Add(points[0]);
                resultPoints.Add(points[end]);
                // resultPoints = { PointList[1], PointList[end] }
            }

            // Return the result
            return resultPoints;
        }

        public float PerpendicularDistance(Vector2 point, Ray2D line)
        {
            float distance = Vector3.Cross(line.direction, point - line.origin).magnitude;
            return distance;
        }*/

    public double PerpendicularDistance(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
    {
        double dx = lineEnd.x - lineStart.x;
        double dy = lineEnd.y - lineStart.y;

        // Normalize
        double mag = Math.Sqrt(dx * dx + dy * dy);
        if (mag > 0.0)
        {
            dx /= mag;
            dy /= mag;
        }
        double pvx = point.x - lineStart.x;
        double pvy = point.y - lineStart.y;

        // Get dot product (project pv onto normalized direction)
        double pvdot = dx * pvx + dy * pvy;

        // Scale line direction vector and subtract it from pv
        double ax = pvx - pvdot * dx;
        double ay = pvy - pvdot * dy;

        return Math.Sqrt(ax * ax + ay * ay);
    }

    public void RamerDouglasPeucker(List<Vector2> points, double epsilon, List<Vector2> output)
    {
        if (points.Count < 2)
        {
            throw new ArgumentOutOfRangeException("Not enough points to simplify");
        }

        // Find the point with the maximum distance from line between the start and end
        double dmax = 0.0;
        int index = 0;
        int end = points.Count - 1;
        for (int i = 1; i < end; i += 1)
        {
            double d = PerpendicularDistance(points[i], points[0], points[end]);
            if (d > dmax)
            {
                index = i;
                dmax = d;
            }
        }

        // If max distance is greater than epsilon, recursively simplify
        if (dmax > epsilon)
        {
            List<Vector2> recResults1 = new List<Vector2>();
            List<Vector2> recResults2 = new List<Vector2>();
            List<Vector2> firstLine = points.Take(index + 1).ToList();
            List<Vector2> lastLine = points.Skip(index).ToList();
            RamerDouglasPeucker(firstLine, epsilon, recResults1);
            RamerDouglasPeucker(lastLine, epsilon, recResults2);

            // build the result list
            output.AddRange(recResults1.Take(recResults1.Count - 1));
            output.AddRange(recResults2);
            if (output.Count < 2) throw new Exception("Problem assembling output");
        }
        else
        {
            // Just return start and end points
            output.Clear();
            output.Add(points[0]);
            output.Add(points[points.Count - 1]);
        }
    }
}