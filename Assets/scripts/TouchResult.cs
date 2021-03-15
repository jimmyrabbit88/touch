using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchResult
{
    public int Type {get; set;}
    public List<Touch> Touch {get; set;}

    public int FingerId {get; set;}

    public TouchResult(int fingerId, int type, List<Touch> touch)
    {
        this.FingerId = fingerId;
        this.Type = type;
        this.Touch = touch;
    }

    
}
