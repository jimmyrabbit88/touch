using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMethodWorldPoint : i_DragMethod
{
    public Vector3 Drag(GameObject gameObject, Touch touch)
    {
        Ray moveToRay = Camera.main.ScreenPointToRay(touch.position);

        RaycastHit movablePlane;
        if (Physics.Raycast(moveToRay, out movablePlane))
        {
            return movablePlane.point;
        }
        else
        {
            return new Vector3();
        }
    }

    public void EndDrag()
    {
        
    }

    public void StartDrag(float distance)
    {
        
    }


}
