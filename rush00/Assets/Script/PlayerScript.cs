using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public GameObject player;
    public GameObject leg;
    public float moveSpeed;
    public SpriteRenderer attachWeapon;

    private WeaponSpawnerScript pickUpWSpawner;
    private Animator legAnimator;
    private float offset = 90f;
    private Vector2 direction;
    private bool moving = false;

    void Start()
    {
        
        legAnimator = leg.GetComponent<Animator>();
    }

	void Update()
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
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
        pickUpWSpawner.drop(player.transform.position, direction);
        attachWeapon.sprite = null;
    }

    void pickUp()
    {
        RaycastHit2D hit;
        Vector2 point = player.transform.position;
        hit = Physics2D.Raycast(point, Vector2.up, 0);
        if (hit && hit.collider && hit.collider.gameObject.tag == "weaponSpawner")
        {
            pickUpWSpawner = hit.collider.gameObject.GetComponent<WeaponSpawnerScript>();
            if (pickUpWSpawner && attachWeapon)
            {
                attachWeapon.sprite = pickUpWSpawner.weapons[pickUpWSpawner.Index].attachBody;
                pickUpWSpawner.WeaponDropped = false;
            }
        }        
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
    }
}
