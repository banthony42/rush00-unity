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
		currentAmo.GetComponent<WeaponScript>().weaponName = WeaponScript.weaponName;
		StartCoroutine(Fire());
	}

	void Update()
	{
		//IA de l'ennemis a faire
	}

	IEnumerator Fire()
	{
		while (true)
		{
			if (hasTarget)
			{
				int tmp = Random.Range(1, 4);
				while (tmp > 0)
				{
					currentAmo.GetComponent<WeaponScript>().Fire(player.transform.position);
					yield return new WaitForSeconds(0.1f);
					tmp -= 1;
				}
			}
			yield return new WaitForSeconds(1);
			// 								 ^
			//						    attak speed
		}
	}

}
