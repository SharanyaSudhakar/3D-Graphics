using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{

    public SliderWithEcho extrude, resolution, rotation, resolutionN;
    public MyCylMesh myMesh = null;

    public Button exit = null;
    // Use this for initialization
    void Start()
    {
        resolution.SetSliderLabel("Cylinder Resolution (N X M)");
        resolution.TheSlider.wholeNumbers = true;
        resolution.TheSlider.minValue = 4;
        resolution.TheSlider.maxValue = 20;
        resolution.SetSliderValue(4);
        resolution.SetSliderListener(ResolutionChanged);

        resolutionN.SetSliderLabel("Cylinder Resolution N");
        resolutionN.TheSlider.wholeNumbers = true;
        resolutionN.TheSlider.minValue = 4;
        resolutionN.TheSlider.maxValue = 20;
        resolutionN.SetSliderValue(4);
        resolutionN.SetSliderListener(ResolutionNChanged);

        extrude.SetSliderLabel("Cylinder Extrude");
        extrude.TheSlider.wholeNumbers = true;
        extrude.TheSlider.maxValue = 20;
        extrude.TheSlider.minValue = 1;
        extrude.SetSliderValue(5);
        extrude.SetSliderListener(ExtrudeCylinder);

        rotation.SetSliderLabel("Cylinder Rotation");
        rotation.TheSlider.wholeNumbers = true;
        rotation.TheSlider.minValue = 30;
        rotation.TheSlider.maxValue = 360;
        rotation.SetSliderValue(180);
        rotation.SetSliderListener(makeRotation);
        exit.onClick.AddListener(appExit);
    }

    void appExit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {

    }

    void makeRotation(float val)
    {
        myMesh.RotationChange(val);
    }

    void ExtrudeCylinder(float value)
    {
        myMesh.ReDrawMesh();
        myMesh.meshExtrude((int)value);
    }

    void ResolutionChanged(float value)
    {
        //axisFrame.SetActive(false);
        myMesh.ReDrawMesh((int)value - 1);
        resolutionN.SetSliderValue(value);
    }

    void ResolutionNChanged(float value)
    {
        myMesh.ReDrawMesh();
        myMesh.incNMesh((int)value-1);
    }
}
