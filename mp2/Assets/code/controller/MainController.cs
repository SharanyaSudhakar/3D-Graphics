using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainController : MonoBehaviour {

	public Camera camera1 = null;
	public InitialScene scene = null;
	public Text selectedTxt = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		mouseClickSelection ();

		if(scene.currentSelected != null)
			selectedTxt.text = "Selected- " + scene.currentSelected.name;
		else
			selectedTxt.text = "Selected- ";
	}

	public void mouseClickSelection()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		if (Input.GetMouseButtonDown (0)) {
			if (Physics.Raycast (ray, out hit))
				scene.makeSelection (hit.transform.gameObject, hit.point);
			else if (!EventSystem.current.IsPointerOverGameObject ())
				scene.deselect();
		}
				
	}
}
