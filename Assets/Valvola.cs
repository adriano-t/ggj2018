using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valvola : MonoBehaviour {
    public CraveFlower[] crates;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GetComponent<Animator>().SetBool("turn", true);
            foreach (var crate in crates)
            {
                crate.floating = false;
                crate.GetComponent<Rigidbody> ().isKinematic = false;
            }
            Grate.Kill (); //don't do this in your lief (sono le 2:01 e manca poco alla consegna)

            //TODO PLAY SAUND
        }


    }

}
