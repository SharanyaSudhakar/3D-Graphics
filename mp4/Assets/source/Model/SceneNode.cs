using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneNode : MonoBehaviour
{

    protected Matrix4x4 mCombinedParentXform;

    public Vector3 Pivot = Vector3.zero;
    public List<NodePrimitive> PrimitiveList;
    public GameObject pivotP = null;
    public Vector3 defaultPivotPos = Vector3.zero;
    public bool isRoot = false;
    public bool isCamera = false;
    public Camera smallViewCamera = null;
    public Vector3 cameraPos = Vector3.zero;
    private Transform initP;

    // Use this for initialization
    protected void Start()
    {
        initP = transform;
        InitializeSceneNode();
        if (!isRoot && pivotP !=null)
            pivotP.SetActive(false);

        // Debug.Log("PrimitiveList:" + PrimitiveList.Count);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void InitializeSceneNode()
    {
        mCombinedParentXform = Matrix4x4.identity;
    }

    // This must be called _BEFORE_ each draw!! 
    public void CompositeXform(ref Matrix4x4 parentXform)
    {
        Matrix4x4 pivot = Matrix4x4.TRS(Pivot, Quaternion.identity, Vector3.one);  // Pivot translation
        Matrix4x4 invPivot = Matrix4x4.TRS(-Pivot, Quaternion.identity, Vector3.one);  // inv Pivot
        Matrix4x4 trs = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);

        mCombinedParentXform = parentXform * pivot * trs;

        // propagate to all children
        foreach (Transform child in transform)
        {
            SceneNode cn = child.GetComponent<SceneNode>();
            if (cn != null)
            {
                cn.CompositeXform(ref mCombinedParentXform);
            }
        }

        // disenminate to primitives
        foreach (NodePrimitive p in PrimitiveList)
        {
            p.LoadShaderMatrix(ref mCombinedParentXform);
        }

        // Compute AxisFrame 
        if (pivotP != null)
        {
            Vector3 x = mCombinedParentXform.GetColumn(0);
            Vector3 y = mCombinedParentXform.GetColumn(1);
            Vector3 z = mCombinedParentXform.GetColumn(2);
            Vector3 size = new Vector3(x.magnitude, y.magnitude, z.magnitude);
            Matrix4x4 xform = mCombinedParentXform * invPivot;
            pivotP.transform.localPosition = xform.MultiplyPoint(defaultPivotPos);
            pivotP.transform.localRotation = Quaternion.LookRotation(z / size.z, y / size.y);
        }

        if(isCamera)
        {
            if (smallViewCamera != null)
            {
                Vector3 x = mCombinedParentXform.GetColumn(0);
                Vector3 y = mCombinedParentXform.GetColumn(1);
                Vector3 z = mCombinedParentXform.GetColumn(2);
                Vector3 size = new Vector3(x.magnitude, y.magnitude, z.magnitude);
                Matrix4x4 xform = mCombinedParentXform * invPivot;
                smallViewCamera.transform.localPosition = xform.MultiplyPoint(cameraPos);
                smallViewCamera.transform.localRotation = Quaternion.LookRotation(y / size.y, -z / size.z);
            }
        }
    }

    public void OffPivot()
    {
        if (pivotP != null)
            pivotP.SetActive(false);
    }
    public void showPivot()
    {
        if (pivotP != null)
            pivotP.SetActive(true);
    }
}