using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leva : MonoBehaviour
{
    public Transform[] pieces = new Transform[8];
    public AudioSource source;

    bool activated;

    public void Open ()
    {
        if(!activated)
            StartCoroutine (RoutineOpen ());
	}

    IEnumerator RoutineOpen()
    {
        float t = 0;
        Vector3 startRot = transform.localEulerAngles;
        Vector3 endRot = new Vector3 (startRot.x, startRot.y, startRot.z - 40);
        while (t < 1)
        {
            transform.localEulerAngles = Vector3.Lerp (startRot, endRot, t);
            t += Time.deltaTime * 2;
            yield return new WaitForEndOfFrame ();
        }

        if(source)
        source.Play ();

        t = 0;
        while (t < 1)
        {
            foreach (var piece in pieces)
            {
                float angle = piece.localEulerAngles.z;
                piece.localPosition += new Vector3 (Mathf.Cos (Mathf.Deg2Rad * (angle + 10)), Mathf.Sin (Mathf.Deg2Rad * (angle + 10)), 0)*0.1f;
            }
            t += Time.deltaTime * 0.01f;
            yield return new WaitForEndOfFrame ();
        }
    }
}
