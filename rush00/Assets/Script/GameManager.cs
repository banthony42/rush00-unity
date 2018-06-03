using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public List<GameObject>	EnnemisList = new List<GameObject>();
	public GameObject		Player;

    private bool gameOVer = false;

    public bool GameOver
    {
        get 
        {
            return gameOVer;
        }
    }

    private bool endGame = false;
//	gestion du jeu ici

	void Start ()
	{
	}

    public void removeEnnemis(GameObject item)
    {
        if (item)
        {
            EnnemisList.Remove(item);
            Debug.Log("Ennemi has been removed :" + item.name);
        }
    }

	// Update is called once per frame
	void Update ()
	{
        if (EnnemisList.Count == 0 && !endGame)
        {
            Debug.Log("Win!");
            endGame = true;
        }
        if (Player == null && !endGame)
        {
            Debug.Log("Loose!");
            endGame = true;
            gameOVer = true;
        }
	}

}
