﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCube : MonoBehaviour, i_Controlable
{
    i_DragMethod dragMethod;
    i_Pinch pinchAction;
    i_ObjectRotation rotateFunc;
    private Vector3 dragPosition;
    private bool collided = false;
    // Start is called before the first frame update
    void Start()
    {
        dragMethod = new DragMethodWorldPoint();
        pinchAction = new PinchScale();
        rotateFunc = new RotateRelativeToCamera();
        dragPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!collided)
        {
            transform.position = Vector3.Lerp(transform.position, dragPosition, 0.05f);
        }
        else
        {
            dragPosition = transform.position;
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("collision");
    //    collided = true;
    //}
    //void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("trigger");
    //    collided = true; ///Change shouldMove
    //}

    public void touched()
    {
        transform.position += Vector3.up;
    }

    public void MoveTo(Vector3 position)
    {
        
        dragPosition = position;
    }

    public void SetRayCastDetection(bool MakeItemDetectable)
    {
        Debug.Log("ya");
        if (MakeItemDetectable)
        {
            gameObject.layer = 0;
        }
        else
        {
            gameObject.layer = 2;
        }
    }


    public void Dragged(Touch touch)
    {
        MoveTo(dragMethod.Drag(gameObject, touch));
    }

    public void DragEnded()
    {
        dragMethod.EndDrag();
    }


    public void DragStart(float distance)
    {
        dragMethod.StartDrag(distance);
    }

    public void Scale(Touch a, Touch b)
    {
        pinchAction.Pinch(gameObject, a, b);
    }

    public void Stretch()
    {
        pinchAction.Stretch(gameObject);
    }

    public void Rotate(Touch a, Touch b)
    {
        rotateFunc.Rotate(gameObject, a, b);
    }

    public void FPVRotation(Touch a, Touch b)
    {
        rotateFunc.XYRotate(gameObject, a, b);
        var x = a.deltaPosition.x / 100;
        var y = a.deltaPosition.y / 100;
        Debug.Log(gameObject.transform.rotation.eulerAngles);
        gameObject.transform.eulerAngles += new Vector3(y, x, 0);
    }

    public void SetSelected(bool setToSelected)
    {
        if (setToSelected)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.grey;
        }
    }

    public void GyroRotate(Vector3 acc)
    {
        throw new NotImplementedException();
    }
}
