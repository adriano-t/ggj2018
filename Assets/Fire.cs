using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    public bool dead;
    public ParticleSystem p1;

    private void Update()
    {

    }

    public void DecreaseFire()
    {

        //Diminuisce il particle system

        float c = p1.main.startLifetime.constant;
        c-=.004f;

        if(c<=0.23f)
        {
            dead = true;
            return;
        }
        ParticleSystem.MinMaxCurve curve = new ParticleSystem.MinMaxCurve(c);
        ParticleSystem.MainModule main = GetComponentInChildren<ParticleSystem>().main;
        main.startLifetime = curve;

    }
}
