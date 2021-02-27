using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locking_gesture_detector : iGestureDetector
{
    private Touch[] beginTouch = new Touch[10];
    private float[] startTime = new float[10];

    public List<TouchRes> touchGestureList = new List<TouchRes>();

    private int itor = 0;

    public List<TouchRes> GetGesture(Touch[] touches)
    {
        RemovePreviousTapGesture();
        DetectBeginTouches(touches);
        
        foreach (Touch touch in touches)
        {
            TouchRes foundGesture = touchGestureList.Find(i => i.FingerId == touch.fingerId);
            if(foundGesture == null || foundGesture.Type == 0)
            {
                TapCheck(touch);
                MovementCheck(touch);
            }
            else
            { 
                if(touch.phase == TouchPhase.Ended)
                {
                    if (foundGesture.Type == 3 || foundGesture.Type == 4) // if foundGesture is scale or rotate, if one touch is ended, do not want second touch to be removed or continue returing gesture
                    {
                        foreach(var i in foundGesture.Touch)
                        {                          
                            UpdateListGesture(i, 6); // change to dead gesture for each linked touch  
                        }
                    }
                    if(foundGesture.Type == 2)
                    {
                        UpdateListGesture(touch, 6);
                    }
                    else // not scale or rotate
                    {
                        touchGestureList.Remove(foundGesture);
                    }
                }
                else // not touch.ended. Therefore will update the locked gesture with the new touch
                {
                    UpdateListTouches(touch, touch);
                }
            }
        }
        return touchGestureList;
    }

    private void RemovePreviousTapGesture()
    {
        foreach(var item in touchGestureList)
        {
            if(item.Type == 1)
            {
                touchGestureList.Remove(item);
                break;
            }
        }
    }

 

    private void DetectBeginTouches(Touch[] touches)
    {
        foreach(var touch in touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                beginTouch[touch.fingerId] = touch;
                startTime[touch.fingerId] = Time.time;
                AddToLockedGestureList(touch, 0);
            }
        }
    }

   

    private void TapCheck(Touch touch)
    {
        float touchTime = Time.time - startTime[touch.fingerId];
        if (touch.phase == TouchPhase.Ended)
        {
            if (touchTime <= .2 && Vector2.Distance(beginTouch[touch.fingerId].position, touch.position) <= 20.0)
            {
                UpdateListGesture(touch, 1);
                UpdateListTouches(touch, touch);
                
            }
        }
    }

    private void DragCheck(Touch touch)
    {
        Touch originTouch = beginTouch[touch.fingerId];
        if(touch.phase == TouchPhase.Moved)
        {
            float originDistance = Vector2.Distance(originTouch.position, touch.position);
            if(originDistance >=100 || originDistance <= -100)
            {
                UpdateListGesture(touch, 2);
                UpdateListTouches(touch, touch);
            }   
        }

    }

    private void MovementCheck(Touch touch)
    {
        if(Input.touchCount >= 2)
        {
            
            foreach (var secondaryTouch in Input.touches)
            {
                if (secondaryTouch.fingerId != touch.fingerId)
                {
                    Touch thisOriginTouch = beginTouch[touch.fingerId];
                    Touch secondaryOriginTouch = beginTouch[secondaryTouch.fingerId];
                    
                    float originDistance = Vector2.Distance(thisOriginTouch.position, secondaryOriginTouch.position);
                    float currentDistance = Vector2.Distance(touch.position, secondaryTouch.position);

                    float firstFingerMoveDistance = Vector2.Distance(thisOriginTouch.position, touch.position);
                    float secondFingerMoveDistance = Vector2.Distance(secondaryOriginTouch.position, secondaryTouch.position);

                    float originAngle = GetAngle(thisOriginTouch.position, secondaryOriginTouch.position);
                    float currentAngle = GetAngle(touch.position, secondaryTouch.position);

                    bool hasDistanceBetweenFingersChangedMuch = hasDistanceChanged(currentDistance, originDistance);
                    bool haveFingersMovedSimilarly = HaveFingersMovedSimilarly(firstFingerMoveDistance, secondFingerMoveDistance);

                    if (hasDistanceChanged(currentDistance, originDistance))
                    {
                        UpdateListGesture(touch, 3);
                        UpdateListTouches(touch, secondaryTouch);
                    }
                    else if (hasAngleChanged(currentAngle, originAngle))
                    {
                        UpdateListGesture(touch, 4);
                        UpdateListTouches(touch, secondaryTouch);
                    }
                    else if (!hasDistanceBetweenFingersChangedMuch && haveFingersMovedSimilarly && HadSignificantMovement(firstFingerMoveDistance))
                    {
                        UpdateListGesture(touch, 5);
                        UpdateListTouches(touch, secondaryTouch);
                    }
                }
            }
        }
        else // touchCount is not 2 or more
        {
            DragCheck(touch);
        }
    }

    private bool HadSignificantMovement(float a)
    {
        if(a > 100 || a < -100)
        {
            return true;
        }
        return false;
    }

    private bool HaveFingersMovedSimilarly(float a, float b)
    {
        if(a - b <=50 && a-b >= -50)
        {
            return true;
        }
        return false;
    }

    private bool hasDistanceChanged(float current, float origin)
    {
        if(current - origin >= 100 || current - origin <= -100)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool hasAngleChanged(float current, float origin)
    {
        if(current - origin >= .1f || current - origin <= -.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float GetAngle(Vector2 a, Vector2 b)
    {
        Vector2 difference = a - b;
        return Mathf.Atan2(difference.y, difference.x);
    }

   

    private void AddToLockedGestureList(Touch touch, int gesture)
    {
        var found = touchGestureList.Find(i => i.FingerId == touch.fingerId);
        if (found != null)
        {
            touchGestureList.Remove(found);
        }

        var tl = new List<Touch>();
        tl.Add(touch);
        touchGestureList.Add(new TouchRes(touch.fingerId, gesture, tl));
    }

    private void UpdateListGesture(Touch touch, int gesture)
    {
        foreach (var i in touchGestureList)
        {
            if (i.FingerId == touch.fingerId)
            {
                i.Type = gesture;
                break;
            }
        }
    }

    public List<TouchRes> UpdateListTouches(Touch touch, Touch linkTouch)
    {
        foreach (var lockedGesture in touchGestureList)
        {
            foreach (var linkedTouch in lockedGesture.Touch)
            {
                if (linkedTouch.fingerId == linkTouch.fingerId)
                {
                    lockedGesture.Touch.Remove(linkedTouch);
                    break;
                }
            }
            lockedGesture.Touch.Add(linkTouch);
        }
        return touchGestureList;
    }

    //Debug Touch List helper method
    //
    //private void DTL(int a)
    //{
    //    Debug.Log(a + " count = " + touchGestureList.Count);
    //    foreach (var i in touchGestureList)
    //    {

    //        Debug.Log(a + ":: Fid" + i.FingerId);
    //        Debug.Log(a + ":: Gid" + i.Type);
    //        foreach(var j in i.Touch)
    //        {
    //            Debug.Log(a + ":: TL " + j.fingerId);

    //        }
    //    }
    //}

}



