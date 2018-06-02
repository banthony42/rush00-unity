using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float	speed;
	public float	TimeUntilEveryFrame;
	public int		NumberOfFrameMAX;
	public Vector3	vectorDirector;
	public float	dammage;
	public string	label;

	void Start ()
	{
			StartCoroutine("UpdateBullet");
	}

	IEnumerator UpdateBullet()
	{
		int i = 0;
		while (i < NumberOfFrameMAX)
		{
			yield return new WaitForSeconds(TimeUntilEveryFrame);
			gameObject.transform.position += vectorDirector * speed;
			++i;
		}
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("FIRE !");
		if (other.gameObject.tag == "Character")
		{
			if (label == "PlayedBullet")
	//
	//		other.gameObject.
			Destroy(gameObject);
		}
		else if (other.gameObject.tag == "Player")
		{
			if (label == "EnnemisBullet")
			//		other.gameObject.
			Destroy(gameObject);
		}
	}
}
