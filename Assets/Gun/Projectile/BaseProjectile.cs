using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public float speed = 20f;
    private float waypointDistanceThreshold = 0.3f;

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
        Rigidbody2D projectileRigidbody = transform.GetComponent<Rigidbody2D>();

        Vector2 currentWaypoint = waypoints[currentWaypointIndex];
        Vector2 direction = currentWaypoint - (Vector2)transform.position;

        projectileRigidbody.velocity = direction.normalized * speed;

        // If we are close enough continue to the next one
        float waypointDistance = Vector2.Distance(transform.position, currentWaypoint);
        if (waypointDistance <= waypointDistanceThreshold)
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

    void Despawn(int seconds)
    {
        Destroy(gameObject, seconds);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
