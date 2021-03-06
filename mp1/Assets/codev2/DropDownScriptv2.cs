using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownScriptv2 : MonoBehaviour {
	public GameObject creationTarget;
	public List<GameObject> objList = new List<GameObject>();
	public List<Collider> colliderList = new List<Collider>();
	public Dropdown dropdown;
	public float min = 0.0f;
	public float max = 5.0f;
	public List<float> t = new List<float> ();
	List<string> options = new List<string> () { "Select", "Cube", "Sphere", "Cylinder" };

	// Use this for initialization
	void Start () {
		populateDropDown ();
	}

	public void DropDown_IndexChange(int index){
		if (index == 1) {
			CreatePrimitiveCube();
		}
		else if(index ==2) {
			CreatePrimitiveSphere();
		}
		else {
			CreatePrimitiveCylinder();
		}
	}

	void CreatePrimitiveCube(){
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		objList.Add (cube);
		cube.transform.position = creationTarget.transform.position;
		cube.transform.Translate(0,0.25f,0);
		t.Add (0.0f);
		cube.GetComponent<Renderer> ().material.color = Color.white;
		BoxCollider boxCollider = (BoxCollider)cube.GetComponent<Collider>();
		colliderList.Add (boxCollider);
	}

	void CreatePrimitiveSphere(){
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		objList.Add (sphere);
		sphere.transform.position = creationTarget.transform.position;
		sphere.transform.Translate(0,0.25f,0);
		t.Add (0.0f);
		sphere.GetComponent<Renderer> ().material.color = Color.white;
		SphereCollider boxCollider = (SphereCollider)sphere.GetComponent<Collider>();
		colliderList.Add (boxCollider);
	}

	void CreatePrimitiveCylinder(){
		GameObject cyl = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		objList.Add (cyl);
		cyl.transform.position = creationTarget.transform.position;
		cyl.transform.localScale = new Vector3(1, 2, 1);
		cyl.transform.Translate(0,0.25f,0);
		t.Add(0.0f);
		cyl.GetComponent<Renderer> ().material.color = Color.green;
		Collider boxCollider = (Collider)cyl.GetComponent<Collider>();
		colliderList.Add (boxCollider);
	}

	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		moveobj ();


		if (Input.GetMouseButtonDown (0)) {
			if (Physics.Raycast (ray, out hit)) {
				foreach (GameObject obj in objList) {
					if (hit.collider.gameObject == obj)
						Destroy (obj);
				}
			}
		}
	}

	void moveobj(){
		for(int i = 0; i<objList.Count;i++) {
			if (objList [i] != null)
			if (objList [i].name == "Cube")
				movecube (objList[i],i);
		}
	}
	void movecube(GameObject cube,int i){
		cube.transform.Rotate (0, 90 * Time.deltaTime, 0);
		cube.transform.position = new Vector3 (cube.transform.position.x, 
			0.5f + Mathf.Lerp (min, max,t[i]),
										cube.transform.position.z);
		Debug.Log (t[i]);
		t[i] += 0.2f * Time.deltaTime;
		if (t[i]> 1.0f) {
			float temp = min;
			min = max;
			max = temp;
			t[i] = 0.0f;
			cube.GetComponent<Renderer> ().material.color = 
				(cube.GetComponent<Renderer> ().material.color == Color.magenta)? Color.white:Color.magenta;
				}
			}


	void populateDropDown()
	{
		dropdown.AddOptions (options);
	}

		
}
