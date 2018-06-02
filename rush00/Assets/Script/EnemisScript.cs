using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemisScript : MonoBehaviour {

	public WeaponSpawnerScript	weaponSpawner;
	public GameObject			player;

	private WeaponScript		currentAmo;
	private GameObject			currentAmoSprite;
	private bool				hasTarget = false;

	void Start ()
	{
		currentAmo = weaponSpawner.weapons[weaponSpawner.Index];
		currentAmo.label = "EnnemisBullet";
		Vector3 vecTmp = new Vector3(transform.position.x - 0.15f, transform.position.y - 0.15f, 0);
		currentAmoSprite = Instantiate(currentAmo.attachBody, vecTmp, transform.rotation);
	}

	void Update ()
	{
		if (hasTarget)
			Fire();
	}

	void Fire()
	{
		currentAmo.weaponCharger = 1;
		currentAmo.Fire(player);
	}
}
