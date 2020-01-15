using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleControlR : MonoBehaviour {

	public Toggle t = null;
	public InitialScene scene = null;
	public Slider xslider = null, yslider = null, zslider = null;
	public Text xinfo = null, yinfo = null, zinfo = null;

	// Use this for initialization
	void Start () {
		if (t.isOn) 
			initialState ();

		t.onValueChanged.AddListener(delegate {changeinfo(scene.currentSelected);});
	}

	public void changeinfo(GameObject obj){
		if (!t.isOn) 
			return;

		if (obj == null) {
			initialState ();
			return;
		}

		xinfo.text = obj.transform.localRotation.eulerAngles.x.ToString ();
		yinfo.text = obj.transform.localRotation.eulerAngles.y.ToString ();
		zinfo.text = obj.transform.localRotation.eulerAngles.z.ToString ();
	}

	public void initialState()
	{
		xslider.value = 0;
		yslider.value = 0;
		zslider.value = 0;

		xslider.minValue = 0;
		xslider.maxValue = 360;
		yslider.minValue = 0;
		yslider.maxValue = 360;
		zslider.minValue = 0;
		zslider.maxValue = 360;

		xinfo.text = "0";
		yinfo.text = "0";
		zinfo.text = "0";
	}

	public void setValues()
	{
		xslider.value = scene.currentSelected.transform.localRotation.eulerAngles.x;
		yslider.value = scene.currentSelected.transform.localRotation.eulerAngles.y;
		zslider.value = scene.currentSelected.transform.localRotation.eulerAngles.z;
	}

	// Update is called once per frame
	void Update () {
		changeinfo(scene.currentSelected);
	}
}
