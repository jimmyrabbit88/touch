using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface DragMethod
{
    void StartDrag(RaycastHit hitObject);
    Vector3 Drag(Touch touch);

    void EndDrag();
}
