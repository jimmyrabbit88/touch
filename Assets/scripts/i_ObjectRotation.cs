using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface i_ObjectRotation
{
    void Rotate(GameObject gameObject, Touch a, Touch b, bool invert = false);
    void GyroRotate(Vector3 acc);
}
