using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSphere : MonoBehaviour, i_Controlable
{
    i_DragMethod dragMethod;
    i_Pinch pinching;
    i_ObjectRotation rotateFunc;
    private Vector3 dragPosition;
    // Start is called before the first frame update
    void Start()
    {
        dragMethod = new DragMethodDistanceFromCamera();
        dragPosition = transform.position;
        pinching = new PinchScale();
        rotateFunc = new RotateRelativeToCamera();
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


    public void SetRayCastDetection(bool MakeItemDetectable)
    {
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
        pinching.Pinch(gameObject, a, b);
    }

    public void Rotate(Touch a, Touch b)
    {

    }

    public void Stretch()
    {
        throw new System.NotImplementedException();
    }

    public void FPVRotation(Touch a, Touch b)
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }
}
