using System.Collections;
using UnityEngine;

public class Grate : MonoBehaviour
{
    public float maxFloatingPointY;
    bool grabbed;

    private void OnTriggerEnter (Collider other)
    { 
        if(other.gameObject.tag == "crate")
        {
            StartCoroutine (RoutineAlignCrate (other.gameObject.transform));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "crate" && grabbed)
        {
            StartCoroutine(ElevateCrate(other.gameObject.transform));
        }
    }

    IEnumerator RoutineAlignCrate (Transform crate)
    {
        crate.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
        Vector3 startPos = crate.position;
        Vector3 endPos = new Vector3 (transform.position.x, crate.position.y, transform.position.z);
        float t = 0;
        while(t < 1)
        {
            crate.position = Vector3.Lerp (startPos, endPos, t);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame ();

        }
        grabbed = true;
    }

    IEnumerator ElevateCrate(Transform crate)
    {
        Color currentColor = crate.GetComponent<Renderer>().material.color;
        float y;

        if(currentColor.Equals(Color.white))
        {
            y = 0.4f;
        }
        else
        {
            y = maxFloatingPointY;
        }
        crate.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Vector3 startPos = crate.position;
        Vector3 endPos = new Vector3(crate.position.x, y, crate.position.z);
        float t = 0;
        while (t < 1)
        {
            crate.position = Vector3.Lerp(startPos, endPos, t);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }
    }
}
