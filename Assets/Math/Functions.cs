using org.mariuszgromada.math.mxparser;
using org.mariuszgromada.math.mxparser.parsertokens;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions : MonoBehaviour
{
    void Start()
    {

    }
    public Expression ParseMathExpression(string expression)
    {
        RemoveUnwantedKeywords(); // TODO: Find a way not to call this everytime
        Expression projectilePathExpression = new Expression(expression);
        return projectilePathExpression;
    }

    private void RemoveUnwantedKeywords()
    {
        // Get every keyword available and remove it
        List<KeyWord> keyWords = mXparser.getKeyWords();
        foreach (KeyWord keyWord in keyWords)
        {
            mXparser.removeBuiltinTokens(keyWord.wordString);
        }

        // Then we add only the keywords we want
        mXparser.unremoveBuiltinTokens("sin", "cos", "tan", "sqrt", "log", "ln", "abs"); // Functions
        mXparser.unremoveBuiltinTokens("pi", "e"); // Constants
    }

    public Function GeneratePathFunction(Expression expression)
    {
        Function projectilePathFunction = new Function("f", expression.getExpressionString(), "x");
        return projectilePathFunction;
    }

    public List<Vector2> GeneratePath(Function projectilePathFunction, float range, float step, int maxSteps)
    {
        List<Vector2> waypoints = new List<Vector2>();

        // Go from negative amount of steps to the max amount of steps
        for (int i = 0; i < maxSteps; i += 1)
        {
            float x = step * i; // Get our x value for our current step
            float y = (float)projectilePathFunction.calculate(x); // Calcuate the y value for our x

            if (!float.IsNaN(y))
            {
                if (y < range && y > -range)
                {
                    Vector2 waypoint = new Vector2(x, y);
                    waypoints.Add(waypoint);
                }
            }
        }

        return waypoints;
    }

    public List<Vector2> AddWaypointsOffset(List<Vector2> waypoints, Vector2 offset)
    {
        for (int i = 0; i < waypoints.Count; i += 1)
        {
            waypoints[i] += offset;
        }

        return waypoints;
    }

    public List<Vector2> TrimWaypoints(List<Vector2> waypoints, Vector2 coordinates)
    {
        List<int> invalidWaypoints = new List<int>();

        for (int i = 0; i < waypoints.Count; i += 1)
        {
            if (waypoints[i].x < coordinates.x)
            {
                invalidWaypoints.Add(i);
            }
        }

        for (int i = 0; i < invalidWaypoints.Count; i += 1)
        {
            waypoints.RemoveAt(invalidWaypoints[i] - i);
        }

        return waypoints;
    }

    public List<Vector2> SortWaypoints(List<Vector2> waypoints)
    {
        GFG comparer = new GFG();
        waypoints.Sort(comparer);

        return waypoints;
    }
}

class GFG : IComparer<Vector2>
{
    public int Compare(Vector2 a, Vector2 b)
    {
        if (a.sqrMagnitude < b.sqrMagnitude)
        {
            return -1;
        }
        else if (a.sqrMagnitude > b.sqrMagnitude)
        {
            return 1;
        }
        else
        {
           return 0;
        }

    }
}
