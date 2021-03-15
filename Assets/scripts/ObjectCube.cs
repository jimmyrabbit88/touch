using System;
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

    //private void OnTriggerEnter(Collider other)
    //{
    //    print("collided");
    //    collided = true;
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    print("left collision");
    //    collided = false;
    //}

    // Update is called once per frame
    void Update()
    {
        if (!collided)
        {
            transform.position = Vector3.Lerp(transform.position, dragPosition, 0.05f);
        }
    }

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
        throw new NotImplementedException();
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
