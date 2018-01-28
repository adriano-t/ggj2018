using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valvola : MonoBehaviour {
    public CraveFlower[] crates;
    public GameObject ball;
    public PlayerController pC;

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
            GetComponent<AudioPlayer>().PlayClip();
            Grate.Kill (); //don't do this in your lief (sono le 2:01 e manca poco alla consegna)
            pC.ResetWeapon();
            ball.SetActive(true);
        }


    }

}
