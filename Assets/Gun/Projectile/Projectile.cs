using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public int maxStepsPerFrame = 5;
    public float waypointDistanceThresholdX = 1f;
    public float waypointDistanceThresholdY = 10f;

    public List<Vector2> waypoints = new List<Vector2>();
    private int currentWaypointIndex = 0;

    public Player owner;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Projectile";

        Physics2D.IgnoreCollision(transform.parent.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
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
            Debug.Log("Distance to next waypoint was: " + waypointDistanceX + ":" + waypointDistanceY);
            transform.GetComponent<SpriteRenderer>().color = Color.red;
            Destroy(gameObject, 5);
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
}
