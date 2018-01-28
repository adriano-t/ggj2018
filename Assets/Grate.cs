using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grate : MonoBehaviour
{
    static List<Grate> grates = new List<Grate>();
    bool grab;
    public static bool active;
    public static bool kill;

    private void Start ()
    {
        grates.Add (this);
    }

    private void OnTriggerEnter (Collider other)
    { 
        if(other.gameObject.tag == "crate" && !grab)
        {
            StartCoroutine (RoutineAlignCrate (other.gameObject.transform));
            grab = true;
        }
    }

    private void Update()
    {
        if (active)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public static void Kill()
    {
        foreach(var grate in grates)
            grate.transform.GetChild (0).gameObject.SetActive (false);
        active = false;
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

        t = 0;
        startPos = crate.position;
        endPos= new Vector3(transform.position.x, transform.position.y+2f, transform.position.z);
        while (t<1)
        {
            crate.position = Vector3.Lerp(startPos, endPos, t);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }


        crate.GetComponent<CraveFlower>().enabled = true;
        crate.GetComponent<CraveFlower>().floating = true;
    }
}
