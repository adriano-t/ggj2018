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
		
        if(!transform.position.Equals(destinationPos))
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPos,Time.deltaTime);
        }
        else
        {
            destinationPos= new Vector3(transform.position.x, transform.position.y + offset*-1, transform.position.z);
        }
	}
}
