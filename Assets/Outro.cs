﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Outro : MonoBehaviour {

    public GameObject spotLight;
    public GameObject foglie;
    public RawImage whiteBkg;
    public RawImage blackBkg;

    public Text finalText;
    AudioSource source;

    public void Run()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(OutroRoutine());
    }

    IEnumerator OutroRoutine()
    {
        spotLight.SetActive(true);
        //fadeout
        float t = 0;
        while (t < 1)
        {
            whiteBkg.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
            t += Time.deltaTime*.3f;
            yield return new WaitForEndOfFrame ();
        }


        foglie.SetActive(true);
        yield return new WaitForSeconds(2f);
        t = 0;
        while (t < 1)
        {
            whiteBkg.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
            t += Time.deltaTime*.3f;
            yield return new WaitForEndOfFrame ();
        }

        yield return new WaitForSeconds(5f);

        t = 0;
        while (t < 1)
        {
            blackBkg.color = new Color(0f, 0f, 0f, Mathf.Lerp(0f, 1f, t));
            finalText.color = new Color(1f, 1f, 1f, Mathf.Lerp( 0f, 1f, t));
            t += Time.deltaTime * .3f;
            yield return new WaitForEndOfFrame ();
        }

        if (source)
            source.Play();

        while (!Input.GetKey(KeyCode.Return) && !Input.GetMouseButton(0))
        {
            yield return new WaitForSeconds(0.01f);
        }

        blackBkg.gameObject.SetActive(false);
        finalText.gameObject.SetActive(false);
       
    }
}
