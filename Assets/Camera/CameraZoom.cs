using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraZoom : MonoBehaviour
{
    public GameObject UIGameObject;
    private State UIState;

    public float speed = 10.0f;
    public float minZoom = 10.0f;
    public float maxZoom = 20.0f;

    float zoom;

    private void Start()
    {
        UIState = UIGameObject.GetComponent<State>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIState.IsMouseOverUI())
        {
            zoom = Mathf.Clamp(newZoom(zoom), minZoom, maxZoom);
            GetComponent<Camera>().orthographicSize = zoom;
        }
    }

    float newZoom(float zoom)
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            return zoom - speed * Time.deltaTime * 10f;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            return zoom + speed * Time.deltaTime * 10f;
        }

        return zoom;
    }
}
