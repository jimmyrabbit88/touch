using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereMovement : MonoBehaviour, iControlable
{
    DragMethod dragMethod;
    private Vector3 dragPosition;
    // Start is called before the first frame update
    void Start()
    {
        dragMethod = new CameraPlaneDragMethod();
        dragPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, dragPosition, 0.05f);
    }

    public void touched()
    {
        transform.position += Vector3.up;
    }

    public void MoveTo(Vector3 position)
    {
        dragPosition = position;
    }

    public void SwitchRayCastIO()
    {
        if (gameObject.layer == 0)
        {
            print("AAA");
            gameObject.layer = 2;
        }
        else if (gameObject.layer == 2)
        {
            gameObject.layer = 0;
        }
    }

    public void Dragged(Touch touch)
    {
        MoveTo(dragMethod.Drag(touch));
    }

    public void DragEnded()
    {
        dragMethod.EndDrag();
    }

    public void DragStart(RaycastHit hitObject)
    {
        dragMethod.StartDrag(hitObject);
    }
}
