using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemisScript : MonoBehaviour {

	public WeaponSpawnerScript	weaponSpawner;
	public GameObject			player;
	public GameObject			currentAmo;
    public GameManager          gameManager;

	private WeaponScript		WeaponScript;
	public bool					hasTarget = false;
	public int					Status = 0;

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

	void Update()
	{
		DetectPlayer();
		//a modif en dessous pour suivre le joueur
		if (hasTarget)
		{
			transform.GetChild(0).GetChild(3).GetComponent<Animator>().SetInteger("Status", 1);
			Fire();
		}
		else
			transform.GetChild(0).GetChild(3).GetComponent<Animator>().SetInteger("Status", 0);
	}

	private void OnDestroy()
	{
        gameManager.removeEnnemis(gameObject);
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
				int tmp = Random.Range(1, 4);
				while (tmp > 0)
				{
                    if (player)
                    {
                        currentAmo.GetComponent<WeaponScript>().Fire(player.transform.position);
                        currentAmo.GetComponent<WeaponScript>().weaponCharger = 1;
                    }
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
