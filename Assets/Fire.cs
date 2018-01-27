using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    public bool dead;
    public ParticleSystem p1;
    public ParticleSystem p2;

    private void Update()
    {

    }

    public void DecreaseFire()
    {

        //Diminuisce il particle system

        float c = p1.main.startLifetime.constant;
        c-=.002f;

        if(c<=0.23)
        {
            dead = true;
            return;
        }
        ParticleSystem.MinMaxCurve curve = new ParticleSystem.MinMaxCurve(c);
        ParticleSystem.MainModule main = GetComponentInChildren<ParticleSystem>().main;
        main.startLifetime = curve;
        main = p2.main;
        main.startLifetime = curve;

    }
}
