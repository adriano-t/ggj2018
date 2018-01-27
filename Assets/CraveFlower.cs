using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraveFlower : MonoBehaviour {

    Vector3 destinationPos;
    float offset = 0.3f;

    // Use this for initialization
    void Start () {
        destinationPos = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position+(Vector3.up * Mathf.Sin(Time.frameCount*0.1f) * 0.005f);
	}
}
