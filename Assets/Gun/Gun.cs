using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using org.mariuszgromada.math.mxparser;

public class Gun : MonoBehaviour
{
    public GameObject mathGameObject;

    // Helpers
    private Functions mathFunctions;
    private Algorithms mathAlgorithms;

    // Projectile function
    private Function projectilePathFunction;
    public string expression;
    public string parsedExpression;
    public bool validExpression;

    private float step = 0.01f;
    private int maxSteps = 10000;
    private float range = 50f;

    // Line simplification
    private float epsilon = 0.008f;

    // Game objects
    public GameObject tracer;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        mathFunctions = mathGameObject.GetComponent<Functions>();
        mathAlgorithms = mathGameObject.GetComponent<Algorithms>();
    }

    public void Shoot()
    {
        List<Vector2> waypoints = mathFunctions.GeneratePath(projectilePathFunction, range, step, maxSteps);
        List<Vector2> sortedWaypoints = mathFunctions.SortWaypoints(new List<Vector2>(waypoints));

        // Get our offset parameters
        Vector2 unitPosition = transform.position;
        Vector2 lowestWaypoint = sortedWaypoints[0];
        Vector2 waypointsOffset = -lowestWaypoint + unitPosition;
        Vector2 unitCenter = waypointsOffset + lowestWaypoint;

        // TODO: if the function has a root it has to be the player's position
        List<Vector2> offsettedWaypoints = mathFunctions.AddWaypointsOffset(waypoints, waypointsOffset);
        List<Vector2> trimmedWaypoints = mathFunctions.TrimWaypoints(offsettedWaypoints, unitCenter);
        List<Vector2> optimizedWaypoints = mathAlgorithms.RamerDouglasPeucker(new List<Vector2>(trimmedWaypoints), epsilon);

        SpawnProjectile(optimizedWaypoints);
/*        foreach (Vector2 waypoint in optimizedWaypoints)
        {
            Instantiate(tracer, waypoint, Quaternion.identity);
        }*/

        Debug.Log("Waypoints [" + trimmedWaypoints.Count + "], optimized [" + optimizedWaypoints.Count + "]");
    }

    void SpawnProjectile(List<Vector2> waypoints)
    {
        GameObject projectileInstance = Instantiate(projectile, waypoints[0], Quaternion.identity); // Instanciate the projectile
        projectileInstance.GetComponent<BaseProjectile>().waypoints = waypoints;

        projectileInstance.transform.SetParent(transform);
    }

    public void SetExpression(string newExpression)
    {
        expression = newExpression;

        // Create our expression and function
        Expression projectilePathExpression = mathFunctions.ParseMathExpression(expression);
        projectilePathFunction = mathFunctions.GeneratePathFunction(projectilePathExpression);

        // Parse the expression and check it's syntax
        parsedExpression = projectilePathExpression.getCanonicalExpressionString();
        validExpression = projectilePathFunction.checkSyntax();
    }

    bool IsInsideCircle(float radius, float x, float y)
    {
        return Math.Pow(x, 2) + Math.Pow(y, 2) <= Math.Pow(radius, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
