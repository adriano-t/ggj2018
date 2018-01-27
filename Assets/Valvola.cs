using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valvola : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GetComponent<Animator>().SetBool("turn", true);
        }
    }

}
