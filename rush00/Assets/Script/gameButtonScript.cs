using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameButtonScript : MonoBehaviour {

	
    public void onClic(string button)
    {
        if (button == "keepPlaying")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        else if (button == "backToMenu")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
    }

	// Update is called once per frame
	void Update () {		
	}
}
