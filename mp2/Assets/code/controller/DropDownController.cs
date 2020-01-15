using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownController : MonoBehaviour {

	public Dropdown dropmenu = null;
	public InitialScene scene = null;
	// Use this for initialization
	void Start () {
		dropmenu.onValueChanged.AddListener(processSelection);
	}

	void processSelection(int i){
		dropmenu.value = 0;
		scene.createShape (i);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
