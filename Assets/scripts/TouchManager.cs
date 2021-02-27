using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    iControlable selectedObject;
    iControlable camera;
    RaycastHit info;

    bool firstDrag = true;

    Touch endTouch;

    Touch[] beginTouch = new Touch[10];
    float[] startTime = new float[10];

    float startingDistance;
    float lastDistance;
    

    GameObject ourCameraPlane;
    iGestureDetector gd;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.GetComponent<iControlable>();
        gd = new locking_gesture_detector();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.touchCount > 0)
        {
            List<TouchRes> gestureArray = gd.GetGesture(Input.touches);
            foreach(var item in gestureArray)
            {
                switch (item.Type)
                {
                    case 1:
                        Touch(item.Touch[0]);
                        print("touch");
                        break;
                    case 2:
                        print("drag");
                        Drag(item.Touch[0]);
                        break;
                    case 3:
                        print("pinch");
                        Scale(item.Touch[0], item.Touch[1]);
                        break;
                    case 4:
                        print("rotate");
                        Rotate(item.Touch[0], item.Touch[1]);
                        break;
                    case 5:
                        print("two finger Drag");
                        TwoFingerDrag(item.Touch[0], item.Touch[1]);
                        break;
                    case 6:
                        print("dead");
                        TouchStopped(item.Touch[0]);
                        
                        break;
                }


            }
        }
        
    }

    private void Touch(Touch touch)
    {
        selectObject(touch);
    }

    private void Drag(Touch touch)
    {
        
        if (selectedObject != null)
        {
            if (firstDrag)
            {
                //print("fd");
                selectedObject.DragStart(info.distance);
                selectedObject.SetRayCastDetection(false);
                firstDrag = false;
            }
            selectedObject.Dragged(touch);
        }
        else
        {
            camera.Dragged(touch);
        }
    }

    private void Scale(Touch touchA, Touch touchB)
    {
        //print("Scale : "+ touchB.position);
        //float distance = Vector2.Distance(touchA.position, touchB.position);
        if(selectedObject != null)
        {
            selectedObject.Scale(touchA, touchB);
        }
        else
        {
            camera.Scale(touchA, touchB);
        }
    }

    private void Rotate(Touch a, Touch b)
    {
        if(selectedObject != null)
        {
            selectedObject.Rotate(a, b);
        }
        else
        {
            camera.Rotate(a, b);
        }
    }

    private void TwoFingerDrag(Touch a, Touch b)
    {
        if(selectedObject != null)
        {

        }
        else
        {
            camera.FPVRotation(a, b);
        }
    }

    private void TouchStopped(Touch a)
    {
        if (selectedObject != null)
        {
            selectedObject.DragEnded();
            firstDrag = true;
            selectedObject.SetRayCastDetection(true);
        }
    }


    private void selectObject(Touch touch)
    {
        Ray myRay = Camera.main.ScreenPointToRay(touch.position);
        
        if (Physics.Raycast(myRay, out info))
        {
            
            if(info.transform.GetComponent<iControlable>()!= null)
            {
                if(info.transform.GetComponent<iControlable>() == selectedObject)
                {
                    selectedObject.SetSelected(false);
                    selectedObject = null;
                    //selectedObject.SwitchRayCastIO();
                }
                else
                {
                    if (selectedObject != null)
                    {
                        selectedObject.SetSelected(false);
                        selectedObject = null;
                        //selectedObject.SwitchRayCastIO();
                    }
                    selectedObject = info.transform.GetComponent<iControlable>();
                 
                    selectedObject.SetSelected(true);
                }
                
            }
            
        }
    }
}
