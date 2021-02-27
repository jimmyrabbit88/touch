using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface DragMethod
{
    void StartDrag(float distance);
    Vector3 Drag(Touch touch);
    void EndDrag();
}
