﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderWithEcho : MonoBehaviour
{

    public Slider TheSlider = null;
    public Text TheEcho = null;
    public Text TheLabel = null;

    public delegate void SliderCallbackDelegate(float v);      // defined a new type
    private SliderCallbackDelegate mCallBack = null;            // 


    // Use this for initialization
    void Start()
    {
        TheSlider.onValueChanged.AddListener(SliderValueChange);
    }

    public void SetSliderListener(SliderCallbackDelegate listener)
    {
        mCallBack = listener;
    }

    void SliderValueChange(float v)
    {
        TheEcho.text = v.ToString("0.0000");
        // Debug.Log("SliderValueChange: " + v);
        if (mCallBack != null)
            mCallBack(v);
    }

    public float GetSliderValue() { return TheSlider.value; }

    public void SetSliderLabel(string l) { TheLabel.text = l; }

    public void SetSliderValue(float v) { TheSlider.value = v; SliderValueChange(v); }

    public void InitSliderRange(float min, float max, float v)
    {
        TheSlider.minValue = min;
        TheSlider.maxValue = max;
        SetSliderValue(v);
    }

}