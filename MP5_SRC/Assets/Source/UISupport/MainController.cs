using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{

    public Camera MainCamera = null;
    public SliderWithEcho resolution = null;
    public GameObject lookAtObj = null;
    public MyMesh myMesh1 = null;
    public MyCylMesh myCylinder = null;
    public GameObject axisFrame = null;
    public GameObject axisFrame2 = null;
    public Material yellowMat = null;
    public Dropdown dropdown = null;

    private MyMesh myMesh = null;
    private GameObject selectedObj = null;
    private GameObject selectedAxis = null;

    private Material oldMat = null;
    private Renderer r = null;

    private Vector3 camDir;
    private float distance;
    private float zVal;

    private float xSpeed = .1f;
    private float ySpeed = .1f;

    private Vector3 oldPos;
    private Vector3 newPos;
    private Vector3 vec;
    private Vector3 vec2;

    // Use this for initialization
    void Start()
    {
        Debug.Assert(MainCamera != null);
        Debug.Assert(lookAtObj != null);
        Debug.Assert(resolution != null);
        Debug.Assert(myMesh1 != null);
        Debug.Assert(myCylinder != null);
        Debug.Assert(axisFrame != null);
        Debug.Assert(axisFrame2 != null);
        Debug.Assert(yellowMat != null);
        Debug.Assert(dropdown != null);

        camDir = (lookAtObj.transform.localPosition - MainCamera.transform.localPosition).normalized;
        distance = (MainCamera.transform.localPosition - lookAtObj.transform.localPosition).magnitude;
        zVal = MainCamera.transform.localPosition.z;

        resolution.SetSliderLabel("Resolution");
        resolution.TheSlider.wholeNumbers = true;
        resolution.TheSlider.minValue = 2;
        resolution.TheSlider.maxValue = 20;
        resolution.SetSliderValue(2);
        resolution.SetSliderListener(ResolutionChanged);
        dropdown.onValueChanged.AddListener(ChangeMesh);
        oldPos = Input.mousePosition;
        newPos = Input.mousePosition;
        axisFrame.SetActive(false);
        axisFrame2.SetActive(false);
        myMesh = myMesh1;
        myMesh1.gameObject.SetActive(true);
        myCylinder.gameObject.SetActive(false);
        MainCamera.transform.localPosition = new Vector3(0, 1.76f, -2);
    }

    void ChangeMesh(int index)
    {
        // plane mesh
        if (index == 0)
        {
            myMesh1.gameObject.SetActive(true);
            if ((int)resolution.GetSliderValue() != myMesh1.N)
                myMesh1.ResetMesh((int)resolution.GetSliderValue());
            myCylinder.gameObject.SetActive(false);
            myMesh = myMesh1;
            selectedObj = null;
            selectedAxis = null;
            axisFrame.SetActive(false);
            MainCamera.transform.localPosition = new Vector3(0, 1.76f, -2);
        }
        // cylinder
        else
        {
            myMesh1.gameObject.SetActive(false);
            myCylinder.gameObject.SetActive(true);
            selectedObj = null;
            selectedAxis = null;
            axisFrame.SetActive(false);
            MainCamera.transform.localPosition = new Vector3(0, 1.76f, -5.5f);
        }
    }

    void ResolutionChanged(float val)
    {
        selectedObj = null;
        selectedAxis = null;
        axisFrame.SetActive(false);
        myMesh1.ResetMesh((int)val);
    }

    void Update()
    {
        oldPos = newPos;
        newPos = Input.mousePosition;
        vec = newPos - oldPos;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            foreach (Transform child in myMesh.gameObject.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform child in myMesh.gameObject.transform)
            {
                if (r != null)
                    r.material = oldMat;
                child.gameObject.SetActive(false);
                axisFrame.SetActive(false);
                selectedObj = null;
                selectedAxis = null;
            }
        }
        if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            camDir = (lookAtObj.transform.localPosition - MainCamera.transform.localPosition).normalized;
            distance = (MainCamera.transform.localPosition - lookAtObj.transform.localPosition).magnitude;
            // Track: ALT + RMB
            if (Input.GetMouseButton(1))
            {
                zVal = MainCamera.transform.localPosition.z;
                float h = xSpeed * Input.GetAxis("Mouse X");
                float v = ySpeed * Input.GetAxis("Mouse Y");
                MainCamera.transform.Translate(-h, -v, 0);
                MainCamera.transform.localPosition = new Vector3(
                    MainCamera.transform.localPosition.x,
                    MainCamera.transform.localPosition.y, zVal);
                Vector3 newLookAtPos = MainCamera.transform.localPosition + distance * camDir;
                lookAtObj.transform.localPosition = newLookAtPos;
            }

            // Tumble: ALT + LMB
            else if (Input.GetMouseButton(0))
            {
                float h = xSpeed * Input.GetAxis("Mouse X") * 2;
                float v = ySpeed * Input.GetAxis("Mouse Y") * 2;
                MainCamera.transform.Translate(h, v, 0);
                Vector3 myVec = (lookAtObj.transform.localPosition - MainCamera.transform.localPosition).normalized;
                float testDot = Vector3.Dot(myVec, Vector3.up);
                if (testDot > .98 || testDot < -.99)
                    MainCamera.transform.Translate(-h, -v, 0);
            }
            // Dolly: ALT + scroll wheel
            else
            {
                float z = xSpeed * Input.GetAxis("Mouse ScrollWheel") * 30;
                MainCamera.transform.localPosition += z * camDir;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!(EventSystem.current.IsPointerOverGameObject()))
            {
                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo);
                if (hit)
                {
                    if (hitInfo.transform.gameObject.tag == "Sphere")
                    {
                        selectedAxis = null;
                        if (r != null)
                            r.material = oldMat;
                        selectedObj = hitInfo.transform.gameObject;
                        r = selectedObj.GetComponent<Renderer>();
                        oldMat = r.material;
                        r.material = yellowMat;
                        Debug.Log("Hit " + selectedObj.name);
                        axisFrame.SetActive(true);
                        axisFrame.transform.localPosition = selectedObj.transform.localPosition;
                    }
                    else if (hitInfo.transform.gameObject.tag == "AxisLine")
                    {
                        if (r != null)
                            r.material = oldMat;
                        selectedAxis = hitInfo.transform.gameObject;
                        r = selectedAxis.GetComponent<Renderer>();
                        oldMat = r.material;
                        r.material = yellowMat;
                    }
                    else
                    {
                        selectedAxis = null;
                        if (r != null)
                            r.material = oldMat;
                        selectedObj = null;
                        axisFrame.SetActive(false);
                    }
                }
                else
                {
                    selectedAxis = null;
                    selectedObj = null;
                    if (r != null)
                        r.material = oldMat;
                    axisFrame.SetActive(false);
                }

            }
            else
            {
                selectedObj = null;
                selectedAxis = null;
                axisFrame.SetActive(false);
            }

        }
        if (Input.GetMouseButton(0) && selectedAxis != null && !(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            if (selectedObj != null)
                vec2 = selectedObj.transform.localPosition;
            if (selectedAxis.name == "X-Axis")
            {
                vec2.x += (vec.x / 100);
            }
            else if (selectedAxis.name == "Y-Axis")
            {
                vec2.y += (vec.y / 100);
            }
            else    // Z-Axis
            {
                vec2.z += (vec.x / 100);
            }
            if (selectedObj != null)
            {
                selectedObj.transform.localPosition = vec2;
                axisFrame.transform.localPosition = selectedObj.transform.localPosition;
            }
        }
        camDir = (lookAtObj.transform.localPosition - MainCamera.transform.localPosition).normalized;
        MainCamera.transform.forward = camDir;
    }
}
