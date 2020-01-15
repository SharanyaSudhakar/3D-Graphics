using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliceUp : MonoBehaviour {

	public InitialScene scene = null;
	public Material fadematerial = null, transparent = null;
	public Slider sliderup = null;

	public void mousedown_custom(){
		if (scene.currentSelected != null) 
			scene.currentSelected.GetComponent<Renderer> ().material = fadematerial;
	}

	public void mousedrag_custom(){
		if (scene.currentSelected != null) 
			scene.currentSelected.GetComponent<Renderer> ().material.SetFloat ("_Cutoff", sliderup.value);
	}

	public void mouseup_custom(){
		if (scene.currentSelected != null) {
			scene.currentSelected.GetComponent<Renderer> ().material = transparent;
			scene.currentSelected.GetComponent<Renderer> ().material.color = new Color (1f, 0.92f, 0.016f, 0.3f);
		}
		sliderup.value = 0;
	}
}
