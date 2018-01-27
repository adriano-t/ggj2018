using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public float fireHp = 100;

    private void Update()
    {

    }

    public void DecreaseFire()
    {
        fireHp -= 1f;
        if(fireHp==0)
        {
            //Diminuisce il particle system
            if(fireHp%10==0)
            {
                float c=GetComponentInChildren<ParticleSystem>().main.startLifetime.constant;
                c--;

                ParticleSystem.MinMaxCurve curve = new ParticleSystem.MinMaxCurve(c);
                ParticleSystem.MainModule main = GetComponentInChildren<ParticleSystem> ().main;
                main.startLifetime = curve;
                
            }
        }
    }

}
