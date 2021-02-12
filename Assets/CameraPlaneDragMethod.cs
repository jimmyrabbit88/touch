using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlaneDragMethod : DragMethod
{
    GameObject allowedMovablePlane;
    public Vector3 Drag(Touch touch)
    {
        Debug.Log("allowedMovablePlaneMethod");
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
        }
        if(allowedMovablePlane.activeSelf == false)
        {
            allowedMovablePlane.SetActive(true);
            allowedMovablePlane.transform.up = (Camera.main.transform.position - allowedMovablePlane.transform.position).normalized;
            
        }

    }

    private void ClearMovablePlaneArea()
    {
        Debug.Log("DDD");
        this.allowedMovablePlane.SetActive(false);
    }

    public void StartDrag(RaycastHit hitObject)
    {
        Debug.Log("ok");
    }

    public void EndDrag()
    {
        ClearMovablePlaneArea();
    }
}
