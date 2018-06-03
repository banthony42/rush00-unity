using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemisLister : MonoBehaviour {

	public GameObject	EnnemisEntity;

	void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerFire")
        {
			EnnemisEntity.GetComponent<EnemisScript>().hasTarget = true;
        }
    }
}
