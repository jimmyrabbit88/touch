using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface i_Controlable
{
    void touched();
    void MoveTo(Vector3 position);
    void SetRayCastDetection(bool MakeItemDetectable);
    void SetSelected(bool setToSelected);
    void DragEnded();
    void Dragged(Touch touch);
    void DragStart(float distance);
    void FPVRotation(Touch a, Touch b);
    void Scale(Touch a, Touch b);
    void Rotate(Touch a, Touch b);
    void GyroRotate(Vector3 acc);
}
