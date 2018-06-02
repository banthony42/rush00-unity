using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnerScript : MonoBehaviour {

    public List<WeaponScript> weapons;

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
        index = Random.Range(0, weapons.Count);
        weaponSprite = weapons[index].GetComponent<SpriteRenderer>().sprite;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        mySpriteRenderer.sprite = weaponSprite;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
