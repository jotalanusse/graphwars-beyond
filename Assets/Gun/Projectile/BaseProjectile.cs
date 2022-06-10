using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public float speed = 80f;
    public int waypointsPerFrame = 1;
    private float waypointDistanceThresholdX = 1f;
    private float waypointDistanceThresholdY = 10f;

    public List<Vector2> waypoints = new List<Vector2>();
    private int currentWaypointIndex = 0;

    public virtual void Start()
    {
        gameObject.tag = "Projectile";
        DiasbleFriendlyCollissions();
    }
    public virtual void Update()
    {
        UpdatePosition();
    }

    void DiasbleFriendlyCollissions()
    {
        Physics2D.IgnoreCollision(transform.parent.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    void UpdatePosition()
    {
        // TODO: Transform this to be constant 
        for (int i = 0; i < waypointsPerFrame; i += 1)
        {
            Vector2 currentWaypoint = waypoints[currentWaypointIndex];

            // float waypointDistanceX = Math.Abs(currentWaypoint.x - transform.position.x);
            // float waypointDistanceY = Math.Abs(currentWaypoint.y - transform.position.y);

            // If the current waypoint is very far the function is probably not continuous (not viable)
            /*            if (waypointDistanceX <= waypointDistanceThresholdX && waypointDistanceY <= waypointDistanceThresholdY)
                        {

                        }
                        else
                        {
                            // Debug.Log("Distance to next waypoint was: " + waypointDistanceX + ":" + waypointDistanceY); // TODO: Remove
                            Despawn(5); // TODO: Change
                        }*/

            Rigidbody2D projectileRigidbody = transform.GetComponent<Rigidbody2D>();

            Vector2 direction = currentWaypoint - (Vector2)transform.position;
            projectileRigidbody.velocity = direction.normalized * speed;

            // If we are close enough vontinue to the next one
            float waypointDistance = Vector2.Distance(transform.position, currentWaypoint);
            if (waypointDistance <= 0.3)
            {
                if (currentWaypointIndex < waypoints.Count - 1)
                {
                    currentWaypointIndex += 1;
                }
                else
                {
                    Destroy(gameObject, 5); // TODO: Change
                }
            }
        }
    }

    void Despawn(int seconds)
    {
        Destroy(gameObject, seconds);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
