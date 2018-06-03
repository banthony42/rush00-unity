using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public List<GameObject>	EnnemisList = new List<GameObject>();
	public GameObject		Player;

    private bool victory = false;
//	gestion du jeu ici

	void Start ()
	{
	}

    public void removeEnnemis(string name)
    {
        Debug.Log("Ennemi has been removed :" + name);
    }

	// Update is called once per frame
	void Update ()
	{



        if (Player == null || victory)
            Debug.Log("Win!");
	}

}
