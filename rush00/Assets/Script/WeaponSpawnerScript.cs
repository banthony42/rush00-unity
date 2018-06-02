using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnerScript : MonoBehaviour {

    public List<WeaponScript> weapons;

    private Vector2 dropPosition;
    private bool weaponDropped;
    public bool WeaponDropped
    {
        set
        {
            weaponDropped = value;
        }
    }

    private int index;
    public int Index
    {
        get
        {
            return index;
        }
    }

    private Sprite weaponSprite;
    private SpriteRenderer mySpriteRenderer;
	
    // Use this for initialization
	void Start () {
        dropPosition = transform.position;
        weaponDropped = true;
        index = Random.Range(0, weapons.Count);
        weaponSprite = weapons[index].GetComponent<SpriteRenderer>().sprite;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        mySpriteRenderer.sprite = weaponSprite;
	}
	
    public void drop(Vector2 position, Vector3 direction)
    {
        weaponDropped = true;
        gameObject.SetActive(true);
        transform.position = position;
    }

    // Update is called once per frame
    void Update () {
        if (gameObject.activeSelf != weaponDropped)
            gameObject.SetActive(weaponDropped);
	}
}
