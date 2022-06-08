using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    public float speed = 5f;
    private int maxStepsPerFrame = 5;
    private float waypointDistanceThresholdX = 1f;
    private float waypointDistanceThresholdY = 10f;

    public List<Vector2> waypoints = new List<Vector2>();
    private int currentWaypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Projectile";
        DiasbleFriendlyCollissions();
    }
    void Update()
    {
        UpdatePosition();
    }

    void DiasbleFriendlyCollissions()
    {
        Physics2D.IgnoreCollision(transform.parent.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    void UpdatePosition()
    {
        Vector2 currentWaypoint = waypoints[currentWaypointIndex];

        float step = speed * Time.deltaTime;

        float waypointDistanceX = currentWaypoint.x - transform.position.x;
        float waypointDistanceY = currentWaypoint.y - transform.position.y;

        // If the current waypoint is very far the function is not continuous (not viable)
        if (waypointDistanceX <= waypointDistanceThresholdX && waypointDistanceY <= waypointDistanceThresholdY)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, step);
        }
        else
        {
            Debug.Log("Distance to next waypoint was: " + waypointDistanceX + ":" + waypointDistanceY); // TODO: Remove
            Despawn(5);
        }

        // If we reached our waypoint we target the next one
        if ((Vector2)transform.position == currentWaypoint)
        {
            if (currentWaypointIndex < waypoints.Count - 1)
            {
                currentWaypointIndex += 1;
            }
            else
            {
                Destroy(gameObject, 5);
            }
        }
    }

    void Despawn(int seconds)
    {
        Destroy(gameObject, seconds);
    }
}
