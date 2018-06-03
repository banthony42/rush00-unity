using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGameUiScript : MonoBehaviour
{

    // Use this for initialization
    public Vector3 directionStart;
    public float timer;
    private float i = 0;
    private RectTransform myRect;
    private Vector3 direction;

    void Start()
    {
        myRect = GetComponent<RectTransform>();
        myRect.Rotate(directionStart * 5, Space.World);
        if (directionStart == Vector3.back)
            direction = Vector3.forward;
        else
            direction = Vector3.back;
    }

    void Update()
    {
        if (i > timer)
        {
            if (direction == Vector3.forward)
                direction = Vector3.back;
            else
                direction = Vector3.forward;
            i = 0;
        }
        myRect.Rotate(direction * Time.deltaTime * 10, Space.World);
        i += Time.deltaTime / timer;
    }
}
