using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour {

	public string type;
	public List<GameObject> children;
	public CommonVars.Family familyName;
	public Color myColor;
	public Vector3 tpos;

	public void addMyChild (ref GameObject obj){
		children.Add (obj);
	}

	public void setMembers(Color clr, int i){
		myColor = clr;
		setFamily ();
		setType (i);
	}

	public void setInitialPos(GameObject obj)
	{
		tpos = new Vector3();
		tpos = obj.transform.localPosition;
	}

	public void setFamily()	{
		if (myColor == Color.black)
			familyName = CommonVars.Family.NoParent;
		else if (myColor == Color.cyan)
			familyName = CommonVars.Family.Grandparent;
		else if (myColor == Color.green)
			familyName = CommonVars.Family.Parent;
		else if (myColor == Color.red)
			familyName = CommonVars.Family.Child;
		else
			familyName = CommonVars.Family.Leaf;
	}

	public string getName (){
		return familyName + ": " + type;
	}

	public void setType(int i)
	{
		switch (i) {
		case 1:
			type = "Cube";
			return;
		case 2:
			type = "Sphere";
			return;
		case 3:
			type = "Cylinder";
			return;
		}
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
