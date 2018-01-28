using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public RawImage background;
    public Text text;
    AudioSource source;
    void Start ()
    {
        source = GetComponent<AudioSource>();
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
            background.color = new Color (0f, 0f, 0f, Mathf.Lerp (1f, 0f, t));  
            text.color = new Color (1f, 1f, 1f, Mathf.Lerp (1f, 0f, t)); 
            t += Time.deltaTime / 3;
            yield return new WaitForEndOfFrame ();
        } 
        background.color = new Color (0f, 0f, 0f, 0F);
        text.color = new Color (1f, 1f, 1f, 0);
        source.Stop();
    }
}
