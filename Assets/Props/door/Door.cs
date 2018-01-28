using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform player;
    Vector3 startPos;
    public Vector3 endPos;
    bool open;
    private void Start ()
    { 
        startPos = GetComponent<Renderer> ().bounds.center;
    }
    private void Update ()
    { 
        if(!open && Vector3.Distance(player.position, startPos) < 5.0f)
        { 
            Open ();
        }
        else if(open && Vector3.Distance (player.position, startPos) > 5.0f)
        { 
            Close ();
        }
    }

    public void Open()
    { 
        open = true;
        StartCoroutine (RoutineOpenClose (endPos));
    }

    public void Close()
    {
        open = false;
        StartCoroutine (RoutineOpenClose (-endPos));
    }

    private IEnumerator RoutineOpenClose (Vector3 targetPos)
    {
        Vector3 pos = transform.localPosition;
        float t = 0;
        while(t < 1)
        {
            transform.localPosition = Vector3.Lerp (pos, pos + targetPos, t);
            t += Time.deltaTime * 2;
            yield return new WaitForEndOfFrame ();
        }
        transform.position = pos + targetPos;
    }
}
