using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemisScript : MonoBehaviour {

	public WeaponSpawnerScript	weaponSpawner;
	public GameObject			player;
	public GameObject			currentAmo;
	public GameManager          gameManager;
    public GameObject			checkPoint;

	private WeaponScript		WeaponScript;
	public bool					hasTarget = false;
	public int					Status = 0;

	private float				speed = 0.1f;
	private float				absoluteDistanceToNextCheckpoint;
	private float				currentDist;
	private float				previousDist;
	private Vector3				vectorDirector;
    private AudioSource myAudioSource;

	public GameObject			currentRoom;

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
		calculateDistAndDirectorVector(checkPoint);
		transform.GetChild(0).GetChild(3).GetComponent<Animator>().SetInteger("Status", 1);
        myAudioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		DetectPlayer();
		if (hasTarget && player)
		{
			runToPlayer();
			Fire();
		}
		else
			MoveToNextCheckpoint();
	}

	private void runToPlayer()
	{
		if (currentRoom.GetComponent<Room>().roomNumber != player.transform.GetChild(0).GetComponent<PlayerScript>().currentRoom.GetComponent<Room>().roomNumber)
		{
			calculateDistAndDirectorVector(checkPoint);
			checkPoint = currentRoom.GetComponent<Room>().CorrectCheckpoint[player.transform.GetChild(0).GetComponent<PlayerScript>().currentRoom.GetComponent<Room>().roomNumber];
			MoveToNextCheckpoint();
		}
		else
		{
			calculateDistAndDirectorVector(player);
			MoveToPlayer();
		}
	}

	private void calculateDistAndDirectorVector(GameObject obj)
	{
		float		deltaX;
		float		deltaY;

		deltaX = (obj.transform.position.x - transform.position.x);
		deltaY = (obj.transform.position.y - transform.position.y);
		absoluteDistanceToNextCheckpoint = Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
		vectorDirector = new Vector3(deltaX / absoluteDistanceToNextCheckpoint, deltaY / absoluteDistanceToNextCheckpoint, 0.0f);
		currentDist = absoluteDistanceToNextCheckpoint;
	}

	private void MoveToPlayer()
	{
		float		deltaX;
		float		deltaY;

		previousDist = currentDist;
		transform.position += vectorDirector * speed;
		deltaX = (player.transform.position.x - transform.position.x);
		deltaY = (player.transform.position.y - transform.position.y);
		currentDist = (Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY)));
	}

	private void MoveToNextCheckpoint()
	{
		float		deltaX;
		float		deltaY;

		previousDist = currentDist;
		transform.position += vectorDirector * speed;
		deltaX = (checkPoint.transform.position.x - transform.position.x);
		deltaY = (checkPoint.transform.position.y - transform.position.y);
		currentDist = (Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY)));
		if (previousDist < currentDist)
		{
			checkPoint = checkPoint.GetComponent<Checkpoint>().NextCheckpoint;
			calculateDistAndDirectorVector(checkPoint);
		}
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
                        myAudioSource.PlayOneShot(currentAmo.GetComponent<WeaponScript>().WeaponSound);
                        currentAmo.GetComponent<WeaponScript>().Fire(player.transform.position);
                        currentAmo.GetComponent<WeaponScript>().weaponCharger = 1;
                    }
					yield return new WaitForSeconds(0.1f);
					tmp -= 1;
				}
			}
            yield return new WaitForSeconds(0.3f* currentAmo.GetComponent<WeaponScript>().fireRate);
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
