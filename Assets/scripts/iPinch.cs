using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface iPinch 
{
    void Pinch(GameObject gameObject, Touch a, Touch b);
    void Stretch(GameObject gameObject);
}
