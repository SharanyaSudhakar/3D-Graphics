using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TravellerControl : MonoBehaviour {

    public SliderWithEcho X = null, Y = null, Z = null;
    public Text ObjectName = null;
    public TheWorldRoom world = null;

    // Use this for initialization
    void Start()
    {
        X.SetSliderListener(XValueChanged);
        Z.SetSliderListener(ZValueChanged);

        ObjectName.text = "Traveller Balls Control:";

        SetInitialValues();
    }

    void SetInitialValues()
    {
        Vector3 p = GetSelectedXformParameter();
        X.InitSliderRange(0.5f, 15, p.x);
        Y.InitSliderRange(0.5f, 4, p.y);
        Z.InitSliderRange(1, 15, p.z);
    }

    void XValueChanged(float v)
    {
        world.setSpeed(v);
    }

    void ZValueChanged(float v)
    {
        world.set_LifeTime(v);
    }

    public Vector3 GetSelectedXformParameter()
    {
        return new Vector3(world.getSpeed(), world.getTimeInterval(), world.get_LifeTime());
    }

    public void ObjectSetUI()
    {
        Vector3 p = GetSelectedXformParameter();
        X.SetSliderValue(p.x);  // do not need to call back for this comes from the object
        Y.SetSliderValue(p.y);
        Z.SetSliderValue(p.z);
    }

    public void closeScene()
    {
        this.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void openScene()
    {
        this.GetComponent<CanvasGroup>().alpha = 1;
    } 
}
