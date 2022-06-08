using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Player owner;

    public int health = 100;
    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Unit";
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.tag == "Projectile")
        {
           if (gameObject.transform.parent != transform)
           {
                transform.GetComponent<SpriteRenderer>().color = Color.red;
           }
        }
    }
}
