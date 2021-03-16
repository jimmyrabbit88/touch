using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
//
// Deprecated use GestureDetectorLocking instead ('',)
//
//
//public class GestureDetectorSimple //: iGestureDetector
//{
//    private bool[] isActive = new bool[10];
//    private List<Touch> registeredTouches = new List<Touch>();
//    private int counter = 0;
//    private TouchResult touchRes;
//    private Touch[] beginTouch = new Touch[10];
//    private float[] startTime = new float[10];

//    //private float startingDistance;
//    private float lastDistance;
//    private float lastAngle;
//    public TouchResult GetGesture(Touch[] touches)
//    {
//        foreach (var touch in Input.touches)
//        {
//            int value = DetermineTouchGesture(touch);
//            touchRes = new TouchResult(0, value, registeredTouches);
//        }
//        return touchRes;
        
//    }

//    private int DetermineTouchGesture(Touch touch)
//    {
//        if(counter == 20)
//        {
//            counter = 0;
//        }
//        registeredTouches.Clear();
//        if (touch.phase == TouchPhase.Began)
//        {
//            if(Input.touchCount == 2)
//            {
//                lastAngle = GetRotation(Input.touches[0], Input.touches[1]);
//            }
//            beginTouch[touch.fingerId] = touch;
//            startTime[touch.fingerId] = Time.time;
//            return 0;
//        }
//        else
//        {
//            float touchTime = Time.time - startTime[touch.fingerId];
//            if (touchTime <= .2)
//            {
//                if (touch.phase == TouchPhase.Ended && Vector2.Distance(beginTouch[touch.fingerId].position, touch.position) <= 20.0)
//                {
//                    registeredTouches.Add(touch);
//                    return 1;//is a tap
//                }
//            }
//            else if (touch.phase == TouchPhase.Moved)
//            {
//                if (Input.touchCount == 2)
//                {
//                    //Pinch or stretch
//                    //Rotate
//                    //two finger drag
                    
//                    if (lastDistance == 0.0 || counter == 0)
//                    {
//                        counter++;
//                        lastDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
//                    }
//                    else
//                    {
//                        float newDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
//                        float newRotation = GetRotation(Input.touches[0], Input.touches[1]);
//                        //Debug.Log(newRotation);
//                        if(newRotation - lastAngle >= .3f || newRotation - lastAngle <= -.3f)
//                        {
//                            registeredTouches.Add(Input.touches[0]);
//                            registeredTouches.Add(Input.touches[1]);
//                            return 4; // rotation
//                        }
//                        else if(lastDistance - newDistance >= 10.0f || lastDistance - newDistance <= -10.0f)
//                        {
//                            registeredTouches.Add(Input.touches[0]);
//                            registeredTouches.Add(Input.touches[1]);
//                            return 3; // pinch

//                        }
//                        //    lines below were for blocking the drag after a finger has been let go
//                        //    isActive[Input.touches[0].fingerId] = true;
//                        //    isActive[Input.touches[1].fingerId] = true;
                     
//                    }
//                }
//                else
//                {
//                    if (isActive[touch.fingerId] == true)
//                    {
//                        return 0;
//                    }
//                    else
//                    {
//                        registeredTouches.Add(touch);
//                        return 2;// is a drag

//                    }
//                }
//            }
//            else if (touch.phase == TouchPhase.Ended)
//            {
//                isActive[touch.fingerId] = false;
//                lastDistance = 0.0f;
//            }
//        }
//        return 0;
//    }

//    private float GetRotation(Touch a, Touch b)
//    {
//        Vector2 diff = b.position - a.position;
//        float angle = Mathf.Atan2(diff.y, diff.x);
//        return angle;
//    }
//}
