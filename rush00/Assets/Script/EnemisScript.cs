using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemisScript : MonoBehaviour {

	public WeaponSpawnerScript	weaponSpawner;
	public GameObject			player;
	public GameObject			currentAmo;

	private WeaponScript		WeaponScript;
	private bool				hasTarget = true;

	void Start()
	{
		WeaponScript = weaponSpawner.weapons[weaponSpawner.Index];
		currentAmo.GetComponent<WeaponScript>().label = "EnnemisBullet";
		currentAmo.GetComponent<SpriteRenderer>().sprite = WeaponScript.attachBody;
		currentAmo.GetComponent<WeaponScript>().bullet = WeaponScript.bullet;
		currentAmo.GetComponent<WeaponScript>().shotWeapon = WeaponScript.shotWeapon;
		currentAmo.GetComponent<WeaponScript>().weaponCharger = WeaponScript.weaponCharger;
		currentAmo.GetComponent<WeaponScript>().label = WeaponScript.label;
		currentAmo.GetComponent<WeaponScript>().weaponName = WeaponScript.weaponName;
	}

	void Update()
	{

		//IA de l'ennemis a faire
		if (hasTarget)
			Fire();
	}

	void Fire()
	{
		currentAmo.GetComponent<WeaponScript>().weaponCharger = 1;
		currentAmo.GetComponent<WeaponScript>().Fire(player.transform.position);
	}
}
