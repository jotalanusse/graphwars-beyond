using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultProjectile : BaseProjectile, IProjectile
{
    public float force = 5f;

    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
    }

    // TODO: Move this to the parent function
    public override void OnCollisionEnter2D(Collision2D collision)
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

    public void OnUnitCollide(GameObject unit)
    {
        unit.GetComponent<SpriteRenderer>().color = Color.red;

        // Push the unit away
        Vector2 direction = unit.transform.position - transform.position;
        unit.GetComponent<Rigidbody2D>().AddForce(direction.normalized * force, ForceMode2D.Impulse);

        // We can only hit the same target once
        Physics2D.IgnoreCollision(unit.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

}
