using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMethodParrallelToCamera : i_DragMethod
{
    GameObject allowedMovablePlane;
    public Vector3 Drag(GameObject gameObject, Touch touch)
    {
        SetMovablePlaneArea(gameObject);
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

    private void SetMovablePlaneArea(GameObject gameObject)
    {
        if (allowedMovablePlane == null)
        {
            allowedMovablePlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            allowedMovablePlane.transform.position = gameObject.transform.position;
            allowedMovablePlane.transform.rotation = Camera.main.transform.rotation *Quaternion.Euler(90, 180, 0);
            allowedMovablePlane.GetComponent<MeshRenderer>().enabled = false;
            allowedMovablePlane.transform.localScale = allowedMovablePlane.transform.localScale * 10;
        }
        if (allowedMovablePlane.activeSelf == false)
        {
            allowedMovablePlane.SetActive(true);
            allowedMovablePlane.transform.position = gameObject.transform.position;
            allowedMovablePlane.transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(90,180,0); 
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
