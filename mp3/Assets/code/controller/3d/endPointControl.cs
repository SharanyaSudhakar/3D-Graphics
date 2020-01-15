using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endPointControl : MonoBehaviour
{
    /*
     Code copied from 
     https://forum.unity.com/threads/dragging-a-object-on-two-axis-regardless-of-camera-angle.69149/
     Author: ivkoni, Dec 2, 2010

        Usage: This code was copied to allow the pointer to be clicked and draged by the user.
        The Code calculates the vector from the pointer to mouse position to 3dplane.
        the distance with Ray.GetPoint is used to get the point in the plane 
        where the ray would hit the plane. Then move your object to this point.
     */
    Ray ray;
    RaycastHit hit;
    Transform objectToDrag;
    public Plane dragPlane;
    Vector3 dragplaneNormal;
    float dragplanedistance = 0;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        //when the object is clicked the normal and plane are set.
        //only created once the left mouse button is clicked.
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                objectToDrag = hit.transform;
                if (objectToDrag.name == "pointFlr" || objectToDrag.name == "bullseye")
                    dragplaneNormal = Vector3.up;
                if (objectToDrag.name == "pointBk")
                    dragplaneNormal = Vector3.forward;
                if (objectToDrag.name == "pointRgt" || objectToDrag.name == "pointLft")
                    dragplaneNormal = Vector3.right;
                dragPlane = new Plane(dragplaneNormal, objectToDrag.position);
            }
        }

        //if the mousebutton is active then move the object. 
        //input is continuous
        if (Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (objectToDrag.name == "pointFlr" || objectToDrag.name == "pointBk" ||
                objectToDrag.name == "pointRgt" || objectToDrag.name == "pointLft" || objectToDrag.name == "bullseye")
                if (objectToDrag.GetComponent<Renderer>().enabled == true)
                    if (dragPlane.Raycast(ray, out dragplanedistance))
                        objectToDrag.position = ray.GetPoint(dragplanedistance);
        }
    }

    /*
     end code copy.
     */
}
