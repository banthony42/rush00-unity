using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public GameObject leg;
    public float moveSpeed;
    public Transform playerTransform;

    private Animator legAnimator;
    private float offset = 90f;
    private Vector2 direction;
    private bool moving = false;

    void Start()
    {
        legAnimator = leg.GetComponent<Animator>();
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
        
        if (Input.GetKeyDown("e"))
        {
            moving = false;
        }
        playerTransform.Translate(direction.normalized * Time.deltaTime * moveSpeed);
    }
}
