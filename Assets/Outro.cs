using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Outro : MonoBehaviour {

    public GameObject spotLight;
    public GameObject foglie;
    public GameObject whiteBkg;
    public Text finalText;
    public AudioSource source;

    private void OnEnable()
    {
        StartCoroutine(OutroRoutine());
    }

    IEnumerator OutroRoutine()
    {
        spotLight.SetActive(true);
        foglie.SetActive(true);
        yield return new WaitForSeconds(3f);

        Color c = new Color(0, 0, 0, 0);

        while(c.a<255)
        {
            c.a += 1;
            whiteBkg.GetComponent<RawImage>().color = c;
            yield return new WaitForSeconds(0.1f);
        }

        c = new Color(finalText.color.r, finalText.color.g, finalText.color.b, 0);

        while(c.a<255)
        {
            c.a += 1;
            finalText.color = c;
            yield return new WaitForSeconds(0.1f);
        }

        while (!Input.GetKey(KeyCode.Return) && !Input.GetMouseButton(0))
        {
            yield return new WaitForSeconds(0.01f);
        }

        if (source)
            source.Play();
    }
}
