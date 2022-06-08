using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Player owner;

    public int health = 100;
    bool isAlive = true;

    void Start()
    {
        gameObject.tag = "Unit";
    }

    void Update()
    {

    }
}
