using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    float panSpeed = 2f;
    float zoomSpeed = 15f;
    public Transform lookAtPosition = null;

    void Start()
    {

    }

    void Update()
    {
        transform.LookAt(lookAtPosition);
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.alt)
        {
            if (Input.GetMouseButton(0))
            {
                TumbleCamera(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            }
            if (e.isMouse && e.button != 0 || e.button != 1)
            {
                float d = Input.GetAxis("Mouse ScrollWheel");
                ZoomCamera(d);
            }
            if (Input.GetMouseButton(1))
            {
                TrackCamera(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            }
        }

    }

    void ZoomCamera(float d)
    {
        Vector3 pos = lookAtPosition.position-transform.position;
        pos.Normalize();

        transform.position += pos * d * zoomSpeed * Time.deltaTime;
    }

    void TumbleCamera(float x, float y)
    {
        Vector3 pos = transform.position;
        pos.x += x * panSpeed * Time.deltaTime;
        pos.y += y * panSpeed * Time.deltaTime;

        transform.position = pos;
        quartRot();
    }

    void TrackCamera(float x, float y)
    {
        Vector3 pos = lookAtPosition.position;
        pos.x += x * panSpeed * Time.deltaTime;
        pos.y += y * panSpeed * Time.deltaTime;
        lookAtPosition.position = pos;
    }

    void quartRot()
    {
        Vector3 V = lookAtPosition.localPosition - transform.localPosition;
        Vector3 W = Vector3.Cross(V, transform.up);
        Vector3 U = Vector3.Cross(W, V);
        transform.localRotation = Quaternion.LookRotation(V, U);
    }
}
