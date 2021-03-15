using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRelativeToCamera : i_ObjectRotation
{
    public void Rotate(GameObject gameObject, Touch a, Touch b, bool invert = false)
    {
        Vector2 oldDiff = (a.position - a.deltaPosition) - (b.position - b.deltaPosition);
        Vector2 difference = a.position - b.position;
        float oldAngle = Mathf.Atan2(oldDiff.y, oldDiff.x);
        float change = Mathf.Atan2(difference.y, difference.x);
        var rotate = (change - oldAngle) * 8;
        
        if(invert == true)
        {
            rotate = rotate * -1;
        }
        
        if(rotate <= 20 && rotate >= -20)
        {
            var rotation = Quaternion.AngleAxis(rotate, Camera.main.transform.forward);
            gameObject.transform.rotation = rotation * gameObject.transform.rotation;
        }
    }

    public void XYRotate(GameObject gameObject, Touch a, Touch b)
    {
        var x = a.deltaPosition.x / 100;
        var y = a.deltaPosition.y / 100;
        gameObject.transform.eulerAngles += new Vector3(y, x, 0);
    }

    public void CameraRotate(GameObject gameObject, Touch a, Touch b)
    {

    }

    public void GyroRotate(Vector3 acc)
    {
        var rotation = Quaternion.AngleAxis(acc.x, Camera.main.transform.forward);
        Camera.main.transform.rotation = rotation * Camera.main.transform.rotation;
    }
}
