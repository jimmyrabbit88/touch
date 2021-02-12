using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectControlScript : MonoBehaviour, iControlable
{
    DragMethod dragMethod;
    private Vector3 dragPosition;
    // Start is called before the first frame update
    void Start()
    {
        dragMethod = new CameraPlaneDragMethod();
        dragPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, dragPosition, 0.05f);
    }

    public void touched()
    {
        transform.position += Vector3.up;
    }

    public void MoveTo(Vector3 position)
    {
        dragPosition = position;
    }

    public void SwitchRayCastIO()
    {
        print("before: " + gameObject.layer);
        if(gameObject.layer == 0)
        {
            print("in");
            gameObject.layer = 2;
        }
        else if(gameObject.layer == 2)
        {
            print("in too");
            gameObject.layer = 0;
        }
        print("after: " + gameObject.layer);
    }


    public void Dragged(Touch touch)
    {
        print("objpos: " + gameObject.transform.position);
        MoveTo(dragMethod.Drag(touch));
    }

    public void DragEnded()
    {
        print("CCC");
        dragMethod.EndDrag();
    }


    public void DragStart(RaycastHit hitObject)
    {
        dragMethod.StartDrag(hitObject);
    }
}
