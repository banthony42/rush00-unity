using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

	public Sprite		weaponAmo;

    public GameObject	attachBody;
    public GameObject	bullet;

    public bool		shotWeapon;
    public int		weaponCharger;
	public string	label;
	public string	weaponName;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}


	public void Fire(GameObject target)
	{
		if (shotWeapon)
		{
			float		angle;
			float		delta;
			float		deltaX;
			float		deltaY;
			GameObject	newBullet;

			deltaX = (target.transform.position.x - transform.position.x);
			deltaY = (target.transform.position.y - transform.position.y);
			delta = Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
			Vector3 vectorDirector = new Vector3(deltaX / delta, deltaY / delta, 0.0f);
			Vector3 vecTmp = new Vector3(transform.position.x + (0.15f * vectorDirector.x), transform.position.y + (0.15f * vectorDirector.y), 0);
			newBullet = Instantiate(attachBody, vecTmp, transform.rotation);
			newBullet.GetComponent<Rigidbody>().AddForce(vectorDirector * newBullet.GetComponent<Bullet>().speed);
		}
	}
}
