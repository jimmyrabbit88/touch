using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    iControlable selectedObject;
    RaycastHit info;

    Touch endTouch;

    Touch[] beginTouch = new Touch[10];
    float[] startTime = new float[10];

    float startingDistance;

    GameObject ourCameraPlane;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            foreach (var touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        IsTap(touch);
                        IsDrag(touch);
                        break;

                    case TouchPhase.Moved:
                        if (IsDrag(touch))
                        {
                            print("AAA");
                            selectedObject.Dragged(touch);
                        }
                        break;
                    
                    case TouchPhase.Ended:
                        IsDrag(touch);
                        if (IsTap(touch)) {}
                        break;
                }
            }
        }
    }

    private void selectObject(Touch touch)
    {
        Ray myRay = Camera.main.ScreenPointToRay(touch.position);
        
        if (Physics.Raycast(myRay, out info))
        {
            selectedObject = info.transform.GetComponent<iControlable>();
            selectedObject.SwitchRayCastIO();
        }
    }

    private bool IsDrag(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            print("began");
            beginTouch[touch.fingerId] = touch;
            selectObject(touch);
            startTime[touch.fingerId] = Time.time;
            selectedObject.DragStart(info);
            return true;
        }

        if (touch.phase == TouchPhase.Moved)
        { 
            return true;
        }
        
        if (touch.phase == TouchPhase.Ended)
        {
            selectedObject.SwitchRayCastIO();
            selectedObject.DragEnded();
        } 
        return false;
    }

    private bool IsTap(Touch touch) {

        if (touch.phase == TouchPhase.Began)
        {
            beginTouch[touch.fingerId] = touch;
            startTime[touch.fingerId] = Time.time;
            return false;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            float touchTime = Time.time - startTime[touch.fingerId];
            if (Vector2.Distance(beginTouch[touch.fingerId].position, touch.position) <= 20.0 && touchTime <= .5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
