﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudScript : MonoBehaviour {

    public string field;
    public PlayerScript player;

    private UnityEngine.UI.Text weaponNameText;
    private UnityEngine.UI.Text munitionText;
    private WeaponScript currentWeapon;

	// Use this for initialization
	void Start () {
        if (field == "weaponName")  
            weaponNameText = GetComponent<UnityEngine.UI.Text>();
        if (field == "munition")
            munitionText = GetComponent<UnityEngine.UI.Text>();
        currentWeapon = player.currentAmo.GetComponent<WeaponScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (field == "weaponName")
        {
            if (currentWeapon.weaponCharger > 0)
                weaponNameText.text = currentWeapon.weaponName;
            else
                weaponNameText.text = "No Weapon";
        }
        if (field == "munition")
        {
            if (currentWeapon.weaponCharger > 0)
                munitionText.text = currentWeapon.weaponCharger.ToString();
            else
                munitionText.text = "-";
        }		
	}
}
