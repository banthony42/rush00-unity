using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public List<GameObject>	EnnemisList = new List<GameObject>();
	public GameObject		Player;
    public GameObject       EndGameUi;
    public AudioClip        winSound;
    public AudioClip        looseSound;
    public AudioClip        playerDeathSound;

    private AudioSource myAudioSource;
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
        myAudioSource = GetComponent<AudioSource>();
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
            myAudioSource.PlayOneShot(winSound);
            Debug.Log("Win!");
            endGame = true;
            EndGameUi.SetActive(true);
        }
        if (Player == null && !endGame)
        {
            myAudioSource.PlayOneShot(playerDeathSound);
            myAudioSource.PlayOneShot(looseSound);
            Debug.Log("Loose!");
            endGame = true;
            gameOVer = true;
            EndGameUi.SetActive(true);
        }
	}

}
