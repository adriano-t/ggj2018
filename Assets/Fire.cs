using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    private float fireHp = 100;
    public bool test;

    private void Update()
    {

    }

    public void DecreaseHp()
    {
        fireHp -= 0.5f;
        if(fireHp==0)
        {
            //GetComponentInChildren<FireBaseScript>().End();
        }
    }

}
