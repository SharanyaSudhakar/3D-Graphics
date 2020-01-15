using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderZ : MonoBehaviour {

	public InitialScene scene = null;
	public Slider z;
	public Toggle tt = null, st = null, rt = null;

	void Start()
	{

	}

	public void onmousedown_custom()
	{
		Vector3 oldval = new Vector3(0,0,0);
		if (tt.isOn) {
			oldval = scene.currentSelected.transform.localPosition;
			scene.currentSelected.transform.localPosition = new Vector3 (oldval.x, oldval.y, z.value);
			return;
		}
		if(st.isOn){
			oldval = scene.currentSelected.transform.localScale;
			scene.currentSelected.transform.localScale = new Vector3 (oldval.x, oldval.y, z.value);
			return;
		}
		if (rt.isOn) {
			scene.currentSelected.transform.Rotate (Vector3.forward * z.value);
		}
	}
}
