using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraveFlower : MonoBehaviour {

    public bool floating;
	// Update is called once per frame
	void Update () {
        if(floating)
            transform.position = transform.position+(Vector3.up * Mathf.Sin(Time.frameCount*0.1f) * 0.005f);
	}
}
