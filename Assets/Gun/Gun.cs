using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using org.mariuszgromada.math.mxparser;

public class Gun : MonoBehaviour
{
    public GameObject mathGameObject;
    private Functions mathFunctions;

    // Function behaiviour
    public string expression;
    public string parsedExpression;
    public bool validExpression;
    private Function projectilePathFunction;

    public float step = 0.05f;
    public int maxSteps = 3000;
    public float range = 50f;

    // Game objects
    public GameObject tracer;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        mathFunctions = mathGameObject.GetComponent<Functions>();
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

        Instantiate(tracer, unitCenter, Quaternion.identity);

        List<Vector2> offsettedWaypoints = mathFunctions.AddWaypointsOffset(waypoints, waypointsOffset);
        List<Vector2> trimmedWaypoints = mathFunctions.TrimWaypoints(offsettedWaypoints, unitCenter);

        SpawnProjectile(trimmedWaypoints);
    }

    void SpawnProjectile(List<Vector2> waypoints)
    {
        GameObject projectileInstance = Instantiate(projectile, waypoints[0], Quaternion.identity); // Instanciate the projectile
        projectileInstance.GetComponent<Projectile>().waypoints = waypoints;

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
