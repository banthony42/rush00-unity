using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public GameObject player;
    public AudioClip levelMusic;
    // Use this for initialization
    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(levelMusic);
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
