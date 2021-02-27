using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertedRotation : iObjectRotation
{
    public void Rotate(GameObject gameObject, Touch a, Touch b)
    {
        Vector2 oldDiff = (a.position - a.deltaPosition) - (b.position - b.deltaPosition);
        Vector2 difference = a.position - b.position;
        float oldAngle = Mathf.Atan2(oldDiff.y, oldDiff.x);
        float change = Mathf.Atan2(difference.y, difference.x);
        var rotate = (change - oldAngle) * 8;
        Debug.Log(rotate);
        if (rotate <= 20 && rotate >= -20)
        {
            var rotation = Quaternion.AngleAxis(rotate*-1, Camera.main.transform.forward);
            gameObject.transform.rotation = rotation * gameObject.transform.rotation;
        }
    }
}

