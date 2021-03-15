using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    i_Controlable selectedObject;
    i_Controlable camera;
    RaycastHit info;

    bool firstDrag = true;
    
    //Turn these to true to turn on
    //Accelerometer Stering
    private bool ACCSteering = false;
    //Gyro First Person Viewing
    private bool GyroLook = false;

    private Gyroscope gyroscope;
    private Quaternion inv;

    GameObject ourCameraPlane;
    i_GestureDetector gd;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.GetComponent<i_Controlable>();
        gd = new GestureDetectorLocking();
        if (GyroLook)
        {
            InitialiseGyro();
        }
    }

    private void InitialiseGyro()
    {
        print("hasGyro");
        gyroscope = Input.gyro;
        gyroscope.enabled = true;
        inv = Quaternion.Inverse(gyroscope.attitude);
    
    }

    // Update is called once per frame
    void Update()
    {
        if(ACCSteering){
            camera.GyroRotate(Input.acceleration);
        }

        if (GyroLook == true)
        { 
            Camera.main.transform.rotation = gyroscope.attitude * inv;
        }
        

        if (Input.touchCount > 0)
        {
            List<TouchResult> gestureArray = gd.GetGesture(Input.touches);
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
            if(!ACCSteering){
                camera.Rotate(a, b);
            }
        }
    }

    private void TwoFingerDrag(Touch a, Touch b)
    {
        if(selectedObject != null)
        {
            selectedObject.FPVRotation(a, b);
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
            
            if(info.transform.GetComponent<i_Controlable>()!= null)
            {
                if(info.transform.GetComponent<i_Controlable>() == selectedObject)
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
                    selectedObject = info.transform.GetComponent<i_Controlable>();
                 
                    selectedObject.SetSelected(true);
                }
                
            }
            
        }
    }
}
