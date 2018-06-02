using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorMenuScript : MonoBehaviour {

    public float gradientSpeed = 3f;
    public Color startColor;
    public Color endColor;

    private float t = 0;
    private bool change = false;
    // Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update()
    {
        if (!change)
            t += Time.deltaTime / gradientSpeed;
        else
            t -= Time.deltaTime / gradientSpeed;
        if (t >= 1)
            change = true;
        if (t <= 0)
            change = false;

        GetComponent<UnityEngine.UI.Image>().color = Color.Lerp(startColor, endColor, t);

	}
}
