using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    public static bool dead;
    static Fire instance;
    public ParticleSystem p1;
    private void Start ()
    {
        instance = this;
    }
    public void DecreaseFire()
    {

        //Diminuisce il particle system

        float c = p1.main.startLifetime.constant;
        c-=.004f;

        if(c==0f)
        {
            dead = true;
            return;
        }
        ParticleSystem.MinMaxCurve curve = new ParticleSystem.MinMaxCurve(c);
        ParticleSystem.MainModule main = GetComponentInChildren<ParticleSystem>().main;
        main.startLifetime = curve;

    }

    public static void Die()
    { 
        ParticleSystem.MainModule main = instance.GetComponentInChildren<ParticleSystem> ().main;
        main.startLifetime = new ParticleSystem.MinMaxCurve (0);
        dead = true;
    }
}
