using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleControlT : MonoBehaviour {

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
			
		xslider.value = obj.transform.localPosition.x;
		yslider.value = obj.transform.localPosition.y;
		zslider.value = obj.transform.localPosition.z;

		xslider.minValue = -10;
		xslider.maxValue = 10;
		yslider.minValue = -10;
		yslider.maxValue = 10;
		zslider.minValue = -10;
		zslider.maxValue = 10;

		xinfo.text = obj.transform.localPosition.x.ToString ();
		yinfo.text = obj.transform.localPosition.y.ToString ();
		zinfo.text = obj.transform.localPosition.z.ToString ();
	}

	public void initialState()
	{
		xslider.value = 0;
		yslider.value = 0;
		zslider.value = 0;

		xinfo.text = "0";
		yinfo.text = "0";
		zinfo.text = "0";

	}

	// Update is called once per frame
	void Update () {
		changeinfo(scene.currentSelected);
	}
}
