using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGameTextScript : MonoBehaviour {

    public GameManager gm;
    public string textEntry;

    private UnityEngine.UI.Text TitleText;
    private UnityEngine.UI.Text keepPlayingText;
	// Use this for initialization
	void Start () {
        if (textEntry == "title")
            TitleText = GetComponent<UnityEngine.UI.Text>();
        if (textEntry == "keepPlaying")
            keepPlayingText = GetComponent<UnityEngine.UI.Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gm.GameOver == true)
        {
            if (textEntry == "title")
                TitleText.text = "Game Over";
            if (textEntry == "keepPlaying")
                keepPlayingText.text = "Restart";
        }
        else
        {
            if (textEntry == "title")
                TitleText.text = "Win";
            if (textEntry == "keepPlaying")
                keepPlayingText.text = "Next lvl";            
        }
	}
}
