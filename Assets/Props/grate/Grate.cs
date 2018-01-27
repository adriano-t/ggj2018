using System.Collections;
using UnityEngine;

public class Grate : MonoBehaviour
{

    private void OnTriggerEnter (Collider other)
    { 
        if(other.gameObject.tag == "crate")
        {

            StartCoroutine (RoutineAlignCrate (other.gameObject.transform));
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
    }
}
