using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCamera : MonoBehaviour, i_Controlable
{
    i_ObjectRotation rotationControl;

    public void Start()
    {
        rotationControl = new RotateRelativeToCamera();
    }
    public void DragEnded()
    {
        throw new System.NotImplementedException();
    }

    public void Dragged(Touch touch)
    {
        Vector3 vec = new Vector3(touch.deltaPosition.x, touch.deltaPosition.y);
        gameObject.transform.position += (transform.rotation * vec * -.001f);
    }

    public void DragStart(float distance)
    {
        throw new System.NotImplementedException();
    }

    public void MoveTo(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    public void Rotate(Touch a, Touch b)
    {
        rotationControl.Rotate(gameObject, a, b, true);
    }

    public void GyroRotate(Vector3 acc){
        rotationControl.GyroRotate(acc);
    }

    public void Scale(Touch a, Touch b)
    {
        float change = CalculateChange(a, b);
        gameObject.transform.position += (transform.rotation * Vector3.forward * change);
    }

    public void FPVRotation(Touch a, Touch b)
    {
        var x = a.deltaPosition.x/100;
        var y = a.deltaPosition.y/100;
        Debug.Log(gameObject.transform.rotation.eulerAngles);
        gameObject.transform.eulerAngles += new Vector3(y, x, 0);
    }
   



    public void touched()
    {
        throw new System.NotImplementedException();
    }


    private float CalculateChange(Touch a, Touch b)
    {
        var originalD = Vector2.Distance((a.position + a.deltaPosition), (b.position + b.deltaPosition));
        var latestD = Vector2.Distance(a.position, b.position);
        var value = (((originalD - latestD) / 1000));
        print(value);
        return (value);
    }

    public void SetSelected(bool setToSelected)
    {
        throw new System.NotImplementedException();
    }

    public void SetRayCastDetection(bool MakeItemDetectable)
    {
        throw new System.NotImplementedException();
    }
}
