using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

    public AudioClip clipMenu;

	// Use this for initialization
	void Start () {
        AudioSource.PlayClipAtPoint(clipMenu, transform.position);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
