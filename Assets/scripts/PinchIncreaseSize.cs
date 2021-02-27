using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchIncreaseSize : iPinch
{
    
    public void Pinch(GameObject gameObject, Touch a, Touch b)
    {
        float change = CalculateChange(a, b);
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * change, gameObject.transform.localScale.y * change, gameObject.transform.localScale.z * change);
    }
    public void Stretch(GameObject gameObject)
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 1.001f, gameObject.transform.localScale.y * 1.001f, gameObject.transform.localScale.z * 1.001f);
    }

    private float CalculateChange(Touch a, Touch b)
    {
        var originalD = Vector2.Distance((a.position+a.deltaPosition), (b.position+b.deltaPosition));
        var latestD = Vector2.Distance(a.position, b.position);
        var value = (((originalD - latestD)/10000) + 1f);
        return (value);
    }
}
