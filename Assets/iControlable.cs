using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface iControlable
{
    void touched();
    void MoveTo(Vector3 position);
    void SwitchRayCastIO();
    void DragEnded();
    void Dragged(Touch touch);
    void DragStart(RaycastHit hitObject);
}
