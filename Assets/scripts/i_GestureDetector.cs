using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface i_GestureDetector
{
    List<TouchResult> GetGesture(Touch[] touches);
}
