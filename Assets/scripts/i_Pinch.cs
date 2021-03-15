using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface i_Pinch 
{
    void Pinch(GameObject gameObject, Touch a, Touch b);
    void Stretch(GameObject gameObject);
}
