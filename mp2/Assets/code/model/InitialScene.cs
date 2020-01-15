using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialScene : MonoBehaviour {

	public List<GameObject> shapes = new List<GameObject>();
	public GameObject currentSelected;


	// Use this for initialization
	/*
	 * Three shapes are created and their relationships set.
	*/
	void Start () {
		for( int i=1; i < CommonVars.names.Length ; i++){
			if(i==1)
				createShape (i);
			else{
				currentSelected = shapes [i - 2];
				createShape (i);
			}
		}
		currentSelected = null;
	}

	/*
	 * Create a new shape based on the dropdown selection and parent it.
	 */
	public void createShape(int i)
	{
		if (i == 0)
			return;
		GameObject myshape = Instantiate(Resources.Load(CommonVars.names[i])) as GameObject;
		shapes.Add (myshape);
		if (currentSelected != null) {
			myshape.transform.SetParent (currentSelected.transform);
			currentSelected.GetComponent<Shape> ().addMyChild (ref myshape);
		}
			
		myshape.transform.localPosition = getPos() + CommonVars.offset;
		Color newcolor = getColor ();

		myshape.GetComponent<Renderer> ().material.color = newcolor;
		myshape.GetComponent<Shape> ().setMembers (newcolor, i);
		myshape.GetComponent<Shape> ().setInitialPos (myshape);
		myshape.name = myshape.GetComponent<Shape> ().getName ();
	}

	Vector3 getPos()
	{
		if (shapes.Count == 1)
			return new Vector3 (0f, 0.5f, -4f);
		if (shapes.Count == 2)
			return new Vector3 (1, 0.5f, 0);
		if (shapes.Count == 3)
			return new Vector3 (1, 0, 0);
		if (currentSelected == null)
			return new Vector3 (0.9f, 0.9f, 0.9f);
		else
			return currentSelected.GetComponent<Collider> ().bounds.size;
	}

	//Color of the newly created obj.
	Color getColor ()
	{
		if (shapes.Count == 1)
			return Color.cyan;
		
		Color clr;
		if (currentSelected !=null){
			clr = currentSelected.GetComponent<Shape> ().myColor;
			if (clr == Color.cyan)
				return Color.green;
			else if (clr == Color.green)
				return Color.red;
			else 
				return Color.white;
		}
		else
			return Color.black;
	}

	public void makeSelection(GameObject obj, Vector3 pt)
	{
		if (obj.name == "CreationPlane" || obj == currentSelected)
			deselect ();
		else {
			deselect ();
			obj.GetComponent<Renderer> ().material.color = new Color (1f, 0.92f, 0.016f, 0.3f);
			currentSelected = obj;
		}
	}

	public void deselect()
	{
		if (currentSelected != null) {
			Color colorcode = currentSelected.GetComponent<Shape> ().myColor;
			currentSelected.GetComponent<Renderer> ().material.color = colorcode;
			currentSelected = null;
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
