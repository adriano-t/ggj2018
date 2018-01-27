using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Transform player;
    Vector3 startPos;
    public Vector3 endPos;
    bool open;
    private void Start ()
    {
        startPos = transform.position;
    }
    private void Update ()
    {
        return;
        if(!open && Vector3.Distance(player.transform.position, transform.position) < 2.0f)
        {
            Open ();
        }
        else if(open && Vector3.Distance (player.transform.position, transform.position) > 2.0f)
        {
            Close ();
        }
    }

    public void Open()
    {
        StartCoroutine (RoutineOpenClose (startPos + endPos));
    }

    public void Close()
    {
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
    }
}
