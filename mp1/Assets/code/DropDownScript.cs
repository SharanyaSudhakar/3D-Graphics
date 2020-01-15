using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownScript : MonoBehaviour {
	public GameObject creationTarget;
	public List<GameObject> objList = new List<GameObject>();
	public List<Collider> colliderList = new List<Collider>();
    public List<Material> matlist = new List<Material>();
	public Dropdown dropdown;
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
		cube.GetComponent<Renderer> ().material.color = Color.white;
		BoxCollider boxCollider = (BoxCollider)cube.GetComponent<Collider>();
		colliderList.Add (boxCollider);
        cube.AddComponent<CubeMove>();
	}

	void CreatePrimitiveSphere(){
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		objList.Add (sphere);
		sphere.transform.position = creationTarget.transform.position;
		sphere.transform.Translate(0,0.25f,0);
		sphere.GetComponent<Renderer> ().material.color = Color.white;
		SphereCollider boxCollider = (SphereCollider)sphere.GetComponent<Collider>();
		colliderList.Add (boxCollider);
        sphere.AddComponent<SphereMove>();
	}

	void CreatePrimitiveCylinder(){
		GameObject cyl = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		objList.Add (cyl);
		cyl.transform.position = creationTarget.transform.position;
		cyl.transform.localScale = new Vector3(0.5f, 1, 0.5f);
		cyl.transform.Translate(0,.5f,0);
		cyl.GetComponent<Renderer> ().material.color = Color.white;
		Collider boxCollider = (Collider)cyl.GetComponent<Collider>();
		colliderList.Add (boxCollider);
        cyl.AddComponent<CylMove>();
	}

	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Input.GetMouseButtonDown (0)) {
			if (Physics.Raycast (ray, out hit)) {
				foreach (GameObject obj in objList) {
					if (hit.collider.gameObject == obj) {
						Destroy (obj);
                        break;
					}
				}
			}
		}
	}
	void populateDropDown()
	{
		dropdown.AddOptions (options);
	}
}
