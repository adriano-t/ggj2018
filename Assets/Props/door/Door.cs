using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform player;
    Vector3 center;
    Vector3 startPos;
    public Vector3 endPos;
    bool open;
    private void Start ()
    { 
        center = GetComponent<Renderer> ().bounds.center;
        startPos = transform.position;
    }
    private void Update ()
    { 
        if(!open && Vector3.Distance(player.position, center) < 5.0f)
        { 
            Open ();
        }
        else if(open && Vector3.Distance (player.position, center) > 5.0f)
        { 
            Close ();
        }
    }

    public void Open()
    { 
        open = true;
        StartCoroutine (RoutineOpenClose (startPos + endPos));
    }

    public void Close()
    {
        open = false;
        StartCoroutine (RoutineOpenClose (startPos));
    }

    private IEnumerator RoutineOpenClose (Vector3 targetPos)
    {
        Vector3 pos = transform.position;
        float t = 0;
        while(t < 1)
        {
            transform.position = Vector3.Lerp (pos, targetPos, t);
            t += Time.deltaTime * 2;
            yield return new WaitForEndOfFrame ();
        }
        transform.position = targetPos;
    }
}
