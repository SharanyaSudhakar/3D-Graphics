using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBtn : MonoBehaviour {

	public InitialScene scene = null;

	public void resetALL(){
		if (scene.shapes.Count == 0)
			return;
		foreach (GameObject obj in scene.shapes) {
			obj.transform.localPosition = obj.GetComponent<Shape> ().tpos;
			obj.transform.localScale = new Vector3 (1, 1, 1);
			obj.transform.localRotation = Quaternion.Euler (0, 0, 0);
		}
	}
}
