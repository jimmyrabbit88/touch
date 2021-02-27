using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface iGestureDetector
{
    List<TouchRes> GetGesture(Touch[] touches);
}
