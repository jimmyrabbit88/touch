using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlaneDragMethod : DragMethod
{
    GameObject allowedMovablePlane;
    public Vector3 Drag(Touch touch)
    {
        SetMovablePlaneArea();
        Ray moveToRay = Camera.main.ScreenPointToRay(touch.position);

        RaycastHit movablePlane;
        if(Physics.Raycast(moveToRay, out movablePlane))
        {
            return movablePlane.point;
        }
        else
        {
            return new Vector3();
        }

    }

    private void SetMovablePlaneArea()
    {
        if (allowedMovablePlane == null)
        {
            allowedMovablePlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            allowedMovablePlane.transform.forward = (Camera.main.transform.position - allowedMovablePlane.transform.position).normalized;
            allowedMovablePlane.transform.Rotate(Camera.main.transform.right, 90);

            //allowedMovablePlane.transform.up = (Camera.main.transform.position - allowedMovablePlane.transform.position).normalized;
        }
        if (allowedMovablePlane.activeSelf == false)
        {
            allowedMovablePlane.SetActive(true);
            
            
        }

    }

    private void ClearMovablePlaneArea()
    {
        this.allowedMovablePlane.SetActive(false);
    }

    public void StartDrag(float distance )
    {
    }

    public void EndDrag()
    {
        ClearMovablePlaneArea();
    }
}
