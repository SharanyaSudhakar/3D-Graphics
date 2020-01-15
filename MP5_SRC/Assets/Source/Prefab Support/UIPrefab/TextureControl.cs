using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureControl : MonoBehaviour {
    public Toggle T, R, S;
    public SliderWithEcho X, Y, Z;
    public Text ObjectName;
    
    public TexturePlacement tp = null;
    private Vector3 mPreviousSliderValues = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        Debug.Assert(tp != null);
        ObjectName.text = "Selected: " + tp.gameObject.name;
        T.isOn = true;
        R.isOn = false;
        S.isOn = false;

        T.onValueChanged.AddListener(SetToTranslation);
        R.onValueChanged.AddListener(SetToRotation);
        S.onValueChanged.AddListener(SetToScaling);
        X.SetSliderListener(XValueChanged);
        Y.SetSliderListener(YValueChanged);
        Z.SetSliderListener(ZValueChanged);
        
        SetToTranslation(true);
    }

    void SetToTranslation(bool v)
    {
        Vector3 p = new Vector3(tp.translate.x, tp.translate.y, 0);
        mPreviousSliderValues = p;
        X.InitSliderRange(-4, 4, p.x);
        Y.InitSliderRange(-4, 4, p.y);
        Z.InitSliderRange(0, 0, p.z);
    }

    void SetToScaling(bool v)
    {
        Vector3 s = new Vector3(tp.scale.x, tp.scale.y, 0);
        mPreviousSliderValues = s;
        X.InitSliderRange(0.1f, 10, s.x);
        Y.InitSliderRange(0.1f, 10, s.y);
        Z.InitSliderRange(1f, 1, s.z);
    }

    void SetToRotation(bool v)
    {
        Vector3 r = Vector3.zero;
        mPreviousSliderValues = r;
        X.InitSliderRange(0, 0, r.x);
        Y.InitSliderRange(0, 0, r.y);
        Z.InitSliderRange(-180, 180, r.z);
        mPreviousSliderValues = r;
    }

    void XValueChanged(float v)
    {   // only for scaling or translation 
        if (T.isOn)
        {
            float dx = v - mPreviousSliderValues.x;
            mPreviousSliderValues.x = v;
            Vector2 V = new Vector2(dx, 0);
            tp.Translate(V);
        }
        else if (S.isOn)
        {
            float dx = v / mPreviousSliderValues.x;
            mPreviousSliderValues.x = v;
            Vector2 V = new Vector2(dx, 1);
            tp.Scale(V);
        }
    }

    void YValueChanged(float v)
    {   // only for scaling or translation
        if (T.isOn)
        {
            float dx = v - mPreviousSliderValues.y;
            mPreviousSliderValues.y = v;
            Vector2 V = new Vector2(0, dx);
            tp.Translate(V);
        }
        else if (S.isOn)
        {
            float dx = v / mPreviousSliderValues.y;
            mPreviousSliderValues.y = v;
            Vector2 V = new Vector2(1, dx);
            tp.Scale(V);
        }
    }

    void ZValueChanged(float v)
    {   // only for rotation
        if (R.isOn)
        {
            float dx = v - mPreviousSliderValues.z;
            mPreviousSliderValues.z = v;
            tp.Rotate(dx);
        }
    }
}

