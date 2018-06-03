using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public List<GameObject>	EnnemisList = new List<GameObject>();
	public GameObject		Player;

     public bool victory = false;

//	gestion du jeu ici

	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
        if (EnnemisList.Count == 0)
            victory = true;
	}

}
