using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMover : MonoBehaviour {

    public Transform firstBox;
    public Transform secondBox;
    public Transform thirdBox;

	// Use this for initialization
	void Start () {
        ElevateBoxes();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ElevateBoxes()
    {
        StartCoroutine(ElevateBox(firstBox, 1f));
        StartCoroutine(ElevateBox(secondBox, 2f));
        StartCoroutine(ElevateBox(thirdBox, 3f));
    }

    IEnumerator ElevateBox(Transform transform,float y)
    {
        while(transform.position.y!=y)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x, y, transform.position.z),Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
    }

}
