using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemisScript : MonoBehaviour {

	public WeaponSpawnerScript	weaponSpawner;
	public GameObject			player;
	public GameObject			currentAmo;

	private WeaponScript		WeaponScript;
	public bool				hasTarget = false;

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
		StartCoroutine(ResetState());
	}

/*
Les ennemis peuvent voir le joueur loin devant eux et un petit peu derrière eux.
Lorsqu’ils repèrent le joueur ils le poursuivent en lui tirant dessus. Leur poursuite
s’arrête après un certain temps ou à la mort du joueur ou de l’ennemi.
*/

	void Update()
	{
		DetectPlayer();
	}

	public void DetectPlayer()
	{
		float	ray = 0.0f;
		Vector3 origin = new Vector3(gameObject.transform.position.x + 1.0f, gameObject.transform.position.y + 1.0f, gameObject.transform.position.z);

		while (ray < 1.0f)
		{
			Vector3 direction1 = new Vector3(ray, 1.0f - ray, 0);
			Vector3 direction2 = new Vector3(-ray, 1.0f - ray, 0);
			RaycastHit2D hit = Physics2D.Raycast(origin, direction1, 2f);
			if (hit.collider && hit.collider.tag == "Player")
				hasTarget = true;
			hit = Physics2D.Raycast(origin, direction2, 2f);
			if (hit.collider && hit.collider.tag == "Player")
				hasTarget = true;
			ray += 0.01f;
		}
		while (ray > 0.0f)
		{
			Vector3 direction1 = new Vector3(1.0f - ray, -ray, 0);
			Vector3 direction2 = new Vector3(-1.0f + ray, -ray, 0);
			RaycastHit2D hit = Physics2D.Raycast(origin, direction1, 8.0f);
			if (hit.collider && hit.collider.tag == "Player")
				hasTarget = true;
			hit = Physics2D.Raycast(origin, direction2, 8.0f);
			if (hit.collider && hit.collider.tag == "Player")
				hasTarget = true;
			ray -= 0.01f;
		}
	}

	IEnumerator Fire()
	{
		while (true)
		{
			if (hasTarget == true)
			{
				Debug.Log("fire !");
				int tmp = Random.Range(1, 4);
				while (tmp > 0)
				{
					currentAmo.GetComponent<WeaponScript>().Fire(player.transform.position);
					currentAmo.GetComponent<WeaponScript>().weaponCharger = 1;
					yield return new WaitForSeconds(0.1f);
					tmp -= 1;
				}
			}
			yield return new WaitForSeconds(1);
			// 							    ^
			//						    attak speed
		}
	}

	IEnumerator ResetState()
	{
		while (true)
		{
			hasTarget = false;
			yield return new WaitForSeconds(4);
		}
	}

}
