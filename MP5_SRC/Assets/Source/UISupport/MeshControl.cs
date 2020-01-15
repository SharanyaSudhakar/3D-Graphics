using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MeshControl : MonoBehaviour
{

    public Camera mainCamera = null;
    public MyCylMesh myCylMesh = null;
    public Material newmat = null;
    public Transform axisFrame = null;
    public Material redMat = null;
    public Material greenMat = null;
    public Material blueMat = null;

    private Transform selectedObj;
    private Transform selectedAxis;

    private int layer;
    private int axislayer;

    private Vector3 oldMousePos;
    private Vector3 newMousePos;
    private Vector3 dis;
    private Vector3 newPos;

    // Use this for initialization
    void Start()
    {

        layer = 1 << 8;//white sphere layer
        axislayer = 1 << 9;//axisframe layer
        newMousePos = Input.mousePosition;
        oldMousePos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {

        oldMousePos = newMousePos;
        newMousePos = Input.mousePosition;
        dis = newMousePos - oldMousePos;

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            foreach (Transform child in myCylMesh.gameObject.transform)
            {
                child.gameObject.SetActive(true);
            }

            if (Input.GetMouseButtonDown(0) && !(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
            {
                RaycastHit hitInfo = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //if sphere is selected
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layer))
                {
                    selectedAxis = null;
                    selectedObj = hitInfo.transform;
                    axisFrame.gameObject.SetActive(true);
                    axisFrame.localPosition = selectedObj.localPosition;
                }
                //if axis is selected
                else if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, axislayer))
                {
                    resetAxisColor();
                    selectedAxis = hitInfo.transform;
                    selectedAxis.GetComponent<Renderer>().material = newmat;
                }
                else
                {
                    resetAxisColor();
                    selectedAxis = null;
                    selectedObj = null;
                    axisFrame.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            foreach (Transform child in myCylMesh.gameObject.transform)
            {
                child.gameObject.SetActive(false);
            }
            resetAxisColor();
            selectedAxis = null;
            selectedObj = null;
            axisFrame.gameObject.SetActive(false);
        }

        if (Input.GetMouseButton(0) && selectedAxis != null && !(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
        {
            if (selectedObj != null)
                newPos = selectedObj.localPosition;
            if (selectedAxis.name == "X-Axis")
            {
                newPos.x += dis.x / 100;
            }
            if (selectedAxis.name == "Y-Axis")
            {
                newPos.y += dis.y / 100;
            }
            else
            {
                newPos.x += dis.x / 100;
            }
            selectedObj.localPosition = new Vector3(newPos.x * Mathf.Cos(0), newPos.y, newPos.x * Mathf.Sin(0));
            MyCylMesh.meshChange = true;
            myCylMesh.radiusChange(newPos, int.Parse(selectedObj.name));
            axisFrame.localPosition = selectedObj.localPosition;
        }
    }

    void resetAxisColor()
    {
        foreach (Transform t in axisFrame.transform)
        {
            Renderer rt = t.GetComponent<Renderer>();
            switch (rt.name)
            {
                case "X-Axis":
                    rt.material = redMat;
                    break;
                case "Y-Axis":
                    rt.material = greenMat;
                    break;
                case "Z-Axis":
                    rt.material = blueMat;
                    break;
            }
        }
    }
}