using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderX : MonoBehaviour {

	public InitialScene scene = null;
	public Slider x;
	public Toggle tt = null, st = null, rt = null;

	void Start()
	{

	}

	public void onmousedown_custom()
	{
		Vector3 oldval = new Vector3(0,0,0);
		if (tt.isOn) {
			oldval = scene.currentSelected.transform.localPosition;
			scene.currentSelected.transform.localPosition = new Vector3 (x.value, oldval.y, oldval.z);
			return;
		}
		if(st.isOn){
			oldval = scene.currentSelected.transform.localScale;
			scene.currentSelected.transform.localScale = new Vector3(x.value,oldval.y,oldval.z);
			return;
		}
		if (rt.isOn) {
			scene.currentSelected.transform.Rotate (Vector3.right * x.value);
		}
	}
}
