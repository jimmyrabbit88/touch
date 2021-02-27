using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distanceFromScreenDrag : DragMethod
{
    float fixedDistance;
    public Vector3 Drag(Touch touch)
    {
        Ray moveToRay = Camera.main.ScreenPointToRay(touch.position);

        Vector3 newPoint = moveToRay.GetPoint(fixedDistance);

        return newPoint;
        
    }

    public void EndDrag()
    {
        fixedDistance = 0;
    }

    public void StartDrag(float distance)
    {
        fixedDistance = distance;   
    }
}
