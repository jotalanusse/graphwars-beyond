using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speed = 0.5f;
    // public float mapSize = 12;

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
        Vector2 cameraPosition = transform.position;

        Camera camera = transform.GetComponent<Camera>();

        // TODO: Fix this whole shit
        if (Math.Abs(cameraPosition.y) > (20 - camera.orthographicSize) * 0.8)
        {
            Vector3 smoothPosition = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, mapCenter.y, speed * Time.fixedDeltaTime), transform.position.z);
            transform.position = smoothPosition;
        }

        if (Math.Abs(cameraPosition.x) > (20 - camera.orthographicSize) * camera.aspect)
        {
            Vector3 smoothPosition = new Vector3(Mathf.Lerp(transform.position.x, mapCenter.x, speed * Time.fixedDeltaTime), transform.position.y, transform.position.z);
            transform.position = smoothPosition;
        }
    }
}
