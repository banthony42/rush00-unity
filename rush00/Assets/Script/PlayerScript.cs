using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public GameObject player;
    public GameObject leg;
    public float moveSpeed;
    public SpriteRenderer attachWeapon;
    public LayerMask weaponMask;
    public AudioClip pickUpWeaponSound;
    public AudioClip dropWeaponSound;
    public AudioClip emptyChargerSound;

	public GameObject			currentAmo;

    private WeaponSpawnerScript pickUpWSpawner;
    private Animator legAnimator;
    private float offset = 90f;
    private Vector2 direction;
	private bool moving = false;
    private bool hasWeapon = false;
    private bool allowFire = true;
    private AudioSource myAudioSource;
    public bool HasWeapon
    {
        get
        {
            return hasWeapon;
        }
    }

    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        legAnimator = leg.GetComponent<Animator>();
    }

	void FixedUpdate()
    {
        // Gestion suivi du curseur souris
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + offset);

        keyBoardHandler();

        if (moving)
            legAnimator.Play("legAnimation");
        else
            legAnimator.Play("idle");
        player.transform.Translate(direction.normalized * Time.deltaTime * moveSpeed);
    }

    void dropWeapon()
    {
		if (hasWeapon == false)
			return ;
        myAudioSource.PlayOneShot(dropWeaponSound);
        Vector2 dirDrop = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dirDrop.Normalize();
        pickUpWSpawner.drop(player.transform.position, dirDrop);
        attachWeapon.sprite = null;
		hasWeapon = false;
        allowFire = true;
    }

    void pickUp()
    {
        RaycastHit2D hit;
        Vector2 point = player.transform.position;
        hit = Physics2D.Raycast(point, Vector2.up, 0, weaponMask);
        if (hit && hit.collider && hit.collider.gameObject.tag == "weaponSpawner")
        {
            pickUpWSpawner = hit.collider.gameObject.GetComponent<WeaponSpawnerScript>();
            if (pickUpWSpawner && attachWeapon)
            {
                myAudioSource.PlayOneShot(pickUpWeaponSound);
                attachWeapon.sprite = pickUpWSpawner.weapons[pickUpWSpawner.Index].attachBody;
                pickUpWSpawner.WeaponDropped = false;
				hasWeapon = true;
				UpdateCurrentWeapon(pickUpWSpawner.weapons[pickUpWSpawner.Index]);
            }
        }
    }

	void UpdateCurrentWeapon(WeaponScript WeaponScript)
	{
		currentAmo.GetComponent<WeaponScript>().label = "PlayerBullet";
		currentAmo.GetComponent<SpriteRenderer>().sprite = WeaponScript.attachBody;
		currentAmo.GetComponent<WeaponScript>().bullet = WeaponScript.bullet;
		currentAmo.GetComponent<WeaponScript>().shotWeapon = WeaponScript.shotWeapon;
		currentAmo.GetComponent<WeaponScript>().weaponCharger = WeaponScript.weaponCharger;
		currentAmo.GetComponent<WeaponScript>().weaponName = WeaponScript.weaponName;
        currentAmo.GetComponent<WeaponScript>().fireRate = WeaponScript.fireRate;
        currentAmo.GetComponent<WeaponScript>().WeaponSound = WeaponScript.WeaponSound;
	}

    void keyBoardHandler()
    {
        // Mouvement w a s d
        direction = Vector2.zero;
        if (Input.GetKey("w"))
        {
            direction.y += 1.0f;
            moving = true;
        }
        else if (Input.GetKey("s"))
        {
            direction.y -= 1.0f;
            moving = true;
        }

        if (Input.GetKey("a"))
        {
            direction.x -= 1.0f;
            moving = true;
        }
        else if (Input.GetKey("d"))
        {
            direction.x += 1.0f;
            moving = true;
        }

        if (Input.GetKeyUp("w") || Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d"))
            moving = false;

        if (Input.GetKeyDown("e"))
            pickUp();

        if (Input.GetMouseButton(1))
            dropWeapon();

        if (Input.GetMouseButton(0) && hasWeapon && allowFire)
			StartCoroutine(Fire());
	}

	IEnumerator Fire()
	{
        allowFire = false;
		currentAmo.GetComponent<WeaponScript>().Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (currentAmo.GetComponent<WeaponScript>().weaponCharger > 0)
            myAudioSource.PlayOneShot(currentAmo.GetComponent<WeaponScript>().WeaponSound);
        else
            myAudioSource.PlayOneShot(emptyChargerSound);
		player.tag = "PlayerFire";
        yield return new WaitForSeconds(0.3f * currentAmo.GetComponent<WeaponScript>().fireRate);
		player.tag = "Player";
        allowFire = true;
	}

}
