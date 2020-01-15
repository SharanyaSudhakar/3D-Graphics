using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScrollPanel : MonoBehaviour {

	public InitialScene scene = null;
	public Text xinfo = null, yinfo = null, zinfo = null;
	public Slider xslider = null, yslider = null, zslider = null;
	public ToggleGroup resizeToggleGrp = null;
	public Toggle Ttoggle = null, Rtoggle = null, Stoggle = null;

	// Use this for initialization
	void Start () {
		xslider.value = 0;
		yslider.value = 0;
		zslider.value = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (resizeToggleGrp.AnyTogglesOn ())
			setupToggleEvent ();
	}

	public void updateSilderPos(ref GameObject obj)
	{
		if (Ttoggle.isOn) {
			
			xslider.value = obj.transform.localPosition.x;
			yslider.value = obj.transform.localPosition.y;
			zslider.value = obj.transform.localPosition.z;
		}
		if (Rtoggle.isOn) {
			
			xslider.value = obj.transform.localRotation.x;
			yslider.value = obj.transform.localRotation.y;
			zslider.value = obj.transform.localRotation.z;
		}
		if (Stoggle.isOn) {

			xslider.value = obj.transform.localScale.x;
			yslider.value = obj.transform.localScale.y;
			zslider.value = obj.transform.localScale.z;
		}
	}
		
	public void updateSliderText(ref GameObject obj)
	{
		if (Ttoggle.isOn) {
			xinfo.text = obj.transform.localPosition.x.ToString ();
			yinfo.text = obj.transform.localPosition.y.ToString ();
			zinfo.text = obj.transform.localPosition.z.ToString ();
		}
		if (Rtoggle.isOn) {
			xinfo.text = obj.transform.localRotation.x.ToString ();
			yinfo.text = obj.transform.localRotation.y.ToString ();
			zinfo.text = obj.transform.localRotation.z.ToString ();
		}
		if (Stoggle.isOn) {
			xinfo.text = obj.transform.localScale.x.ToString ();
			yinfo.text = obj.transform.localScale.y.ToString ();
			zinfo.text = obj.transform.localScale.z.ToString ();
		}
	}

	public void setupToggleEvent  (){
		if (Ttoggle.isOn || Rtoggle.isOn) {
			xslider.minValue = -10;
			xslider.maxValue = 10;
			yslider.minValue = -10;
			yslider.maxValue = 10;
			zslider.minValue = -10;
			zslider.maxValue = 10;
		}
		if (Stoggle.isOn) {
			xslider.minValue = 0;
			xslider.maxValue = 10;
			yslider.minValue = 0;
			yslider.maxValue = 10;
			zslider.minValue = 0;
			zslider.maxValue = 10;
		}
		if(scene.currentSelected!=null)
			updateSliderText (ref scene.currentSelected);
	}
}
