using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface i_DragMethod
{
    void StartDrag(float distance);
    Vector3 Drag(GameObject gameObject, Touch touch);
    void EndDrag();
}
