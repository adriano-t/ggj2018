using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    RawImage background;
    Text text;
    AudioSource source;
    void Start ()
    {
        StartCoroutine (Routine ());
	}
    IEnumerator Routine()
    {
        if(source)
            source.Play ();
        //wait for button press
        while (!Input.GetKey(KeyCode.Return) && !Input.GetMouseButton(0))
        {
            yield return new WaitForSeconds (0.01f);
        }

        //fadeout
        float t = 0;
        while (t < 1)
        {
            background.color = new Color (0, 0, 0, Mathf.Lerp (1, 0, t));
            text.color = new Color (0, 0, 0, Mathf.Lerp (1, 0, t));
            t += Time.deltaTime;
        }
    }
}
