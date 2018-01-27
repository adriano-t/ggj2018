using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    private float fireHp = 100;

    private void Update()
    {

    }

    public void DecreaseFire()
    {
        fireHp -= 0.5f;
        if(fireHp==0)
        {
            //Diminuisce il particle system
        }
    }

}
