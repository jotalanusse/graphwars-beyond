using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public GameObject UIGameObject;
    private State UIState;

    public float mouseSensitivity = 0.02f;
    private Vector3 lastPosition;
     
    private void Start()
    {
        UIState = UIGameObject.GetComponent<State>();
    }

    void Update()
    {
        if (!UIState.IsMouseOverUI())
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 delta = Input.mousePosition - lastPosition;
                transform.Translate(-delta.x * mouseSensitivity, -delta.y * mouseSensitivity, 0);
                lastPosition = Input.mousePosition;
            }
        }
    }
}
