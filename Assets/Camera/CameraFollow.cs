using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speed = 0.1f;
    public float mapSize = 12;

    void Start()
    {
        
    }

    void Update()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 mapCenter = new Vector3(0, 0, -10);
        float distanceFromCenter = Vector2.Distance(transform.position, mapCenter);

        if (distanceFromCenter > 12)
        {
            Vector3 smoothPosition = Vector3.Lerp(transform.position, mapCenter, speed * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }
    }
}
