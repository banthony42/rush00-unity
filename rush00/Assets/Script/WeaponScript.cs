using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

	public Sprite		attachBody;
	public GameObject	bullet;

    public bool		shotWeapon;
    public int		weaponCharger;
    public float    fireRate;
	public string	label;
	public string	weaponName;

	public void Fire(Vector2 direction)
	{
		if (weaponCharger > 0)
		{
			float		delta;
			float		deltaX;
			float		deltaY;
			GameObject	newBullet;

			deltaX = (direction.x - transform.position.x);
			deltaY = (direction.y - transform.position.y);
			delta = Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
			Vector2 vectorDirector = new Vector2(deltaX / delta, deltaY / delta);
			Vector2 vecTmp = new Vector2(transform.position.x + (vectorDirector.x), transform.position.y + (vectorDirector.y));
			newBullet = Instantiate(bullet, vecTmp, transform.rotation);
            newBullet.transform.Rotate(0f, 0f, 90f, Space.Self);
			newBullet.GetComponent<Bullet>().vectorDirector = vectorDirector;
			newBullet.GetComponent<Bullet>().label = label;
		}
		if (shotWeapon)
			weaponCharger -= 1;
	}
}
