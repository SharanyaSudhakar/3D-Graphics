using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderY : MonoBehaviour {

	public InitialScene scene = null;
	public Slider y;
	public Toggle tt = null, st = null, rt = null;

	public void onmousedown_custom()
	{
		Vector3 oldval = new Vector3(0,0,0);
		if (tt.isOn) {
			oldval = scene.currentSelected.transform.localPosition;
			scene.currentSelected.transform.localPosition = new Vector3 (oldval.x, y.value, oldval.z);
			return;
		}
		if(st.isOn){
			oldval = scene.currentSelected.transform.localScale;
			scene.currentSelected.transform.localScale = new Vector3 (oldval.x, y.value, oldval.z);
			return;
		}
		if (rt.isOn) {
			scene.currentSelected.transform.Rotate (Vector3.up * y.value);
		}
	}
}
