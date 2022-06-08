using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    public float step = 5f;
    public int waypointsPerFrame = 50;
    private float waypointDistanceThresholdX = 1f;
    private float waypointDistanceThresholdY = 10f;

    public List<Vector2> waypoints = new List<Vector2>();
    private int currentWaypointIndex = 0;

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
        // TODO: Transform this to be constant 
        for (int i = 0; i < waypointsPerFrame; i += 1)
        {
            Vector2 currentWaypoint = waypoints[currentWaypointIndex];

            float waypointDistanceX = currentWaypoint.x - transform.position.x;
            float waypointDistanceY = currentWaypoint.y - transform.position.y;

            // If the current waypoint is very far the function is not continuous (not viable)
            if (waypointDistanceX <= waypointDistanceThresholdX && waypointDistanceY <= waypointDistanceThresholdY)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, step);

                if (currentWaypointIndex < waypoints.Count - 1)
                {
                    currentWaypointIndex += 1;
                }
                else
                {
                    Destroy(gameObject, 5); // TODO: Change
                }
            }
            else
            {
                Debug.Log("Distance to next waypoint was: " + waypointDistanceX + ":" + waypointDistanceY); // TODO: Remove
                Despawn(5); // TODO: Change
            }
        }
    }

    void Despawn(int seconds)
    {
        Destroy(gameObject, seconds);
    }

    public void OnUnitCollide(GameObject unit)
    {
        unit.GetComponent<SpriteRenderer>().color = Color.red;

        float magnitude = 5f;

        Vector2 force = unit.transform.position - transform.position;
        force.Normalize();

        unit.GetComponent<Rigidbody2D>().AddForce(force * magnitude, ForceMode2D.Impulse);

        Physics2D.IgnoreCollision(unit.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Unit")
        {
            if (collisionGameObject.transform != transform.parent)
            {
                OnUnitCollide(collisionGameObject);
            }
        }
    }
}
