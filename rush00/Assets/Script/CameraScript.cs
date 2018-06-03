using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public GameObject player;
    // Use this for initialization
    void Start()
    {
        transform.position = player.transform.position;
        transform.Translate(new Vector3(0F, 0F, -10));
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            transform.position = player.transform.position;
            transform.Translate(new Vector3(0F, 0F, -10));
        }
    }
}
