using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLPXForm : MonoBehaviour {

    public SliderWithEcho X, Y, Z;
    public Transform mSelected;
    private Vector3 mPreviousSliderValues = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        SetToTranslation(true);
        X.SetSliderListener(XValueChanged);
        Y.SetSliderListener(YValueChanged);
        Z.SetSliderListener(ZValueChanged);

        ObjectSetUI();
        SetToTranslation(true);
    }

    void SetToTranslation(bool v)
    {
        Vector3 p = mSelected.position;
        mPreviousSliderValues = p;
        X.InitSliderRange(-15, 15, p.x);
        Y.InitSliderRange(-15, 15, p.y);
        Z.InitSliderRange(-15, 15, p.z);
    }

    void XValueChanged(float v)
    {
        Vector3 p = mSelected.position;
        p.x = v;
        mSelected.position = p;
    }

    void YValueChanged(float v)
    {
        Vector3 p = mSelected.position;
        p.y = v;
        mSelected.position = p;
    }

    void ZValueChanged(float v)
    {
        Vector3 p = mSelected.position;
        p.z = v;
        mSelected.position = p;
    }

    public void ObjectSetUI()
    {
        Vector3 p = mSelected.position;
        X.SetSliderValue(p.x);  // do not need to call back for this comes from the object
        Y.SetSliderValue(p.y);
        Z.SetSliderValue(p.z);
    }
}