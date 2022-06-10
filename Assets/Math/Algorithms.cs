using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Algorithms : MonoBehaviour
{
    public double PerpendicularDistance(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
    {
        double distanceX = lineEnd.x - lineStart.x;
        double distanceY = lineEnd.y - lineStart.y;

        // Normalize
        double mag = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
        if (mag > 0.0)
        {
            distanceX /= mag;
            distanceY /= mag;
        }
        double pvx = point.x - lineStart.x;
        double pvy = point.y - lineStart.y;

        // Get dot product (project pv onto normalized direction)
        double pvdot = distanceX * pvx + distanceY * pvy;

        // Scale line direction vector and subtract it from pv
        double ax = pvx - pvdot * distanceX;
        double ay = pvy - pvdot * distanceY;

        return Math.Sqrt(ax * ax + ay * ay);
    }

    public List<Vector2> RamerDouglasPeucker(List<Vector2> points, double epsilon)
    {
        if (points.Count < 2)
        {
            throw new ArgumentOutOfRangeException("Not enough points to simplify");
        }

        // Find the point with the maximum distance from line between the start and end
        double maxDistance = 0.0;
        int index = 0;
        for (int i = 1; i < points.Count - 1; i += 1)
        {
            double distance = PerpendicularDistance(points[i], points[0], points[points.Count - 1]);
            if (distance > maxDistance)
            {
                index = i;
                maxDistance = distance;
            }
        }

        List<Vector2> output = new List<Vector2>();

        // If max distance is greater than epsilon, recursively simplify
        if (maxDistance > epsilon)
        {
            List<Vector2> firstLine = points.Take(index + 1).ToList();
            List<Vector2> lastLine = points.Skip(index).ToList();

            List<Vector2> recResults1 = RamerDouglasPeucker(firstLine, epsilon);
            List<Vector2> recResults2 = RamerDouglasPeucker(lastLine, epsilon);

            // Build the result list
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

        return output;
    }
}