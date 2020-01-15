using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickScript : MonoBehaviour {
	public GameObject sphere;
	Vector3 targetPos;
	// Use this for initialization
	void Start () {
		sphere = GameObject.Find ("CreationTarget");
		targetPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//code used from coffeebreakcodes.com/move-object-to-mouse-click-position-unity3d/
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray,out hit)){
				targetPos = hit.point;
				targetPos.y = 0.25f;
				sphere.transform.position = targetPos;
			}
		}
		//end copy code
	}
}
