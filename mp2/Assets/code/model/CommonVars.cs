using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonVars {
	public static bool selected1, selected2;
	public enum Family {NoParent, Grandparent, Parent, Child, Leaf};
	public enum axis {x,y,z};
	public static Vector3 offset = new Vector3(0.1f,0.1f,0.1f);
	public static string[] names = { "null", "MyCube", "MySphere", "MyCylinder" };
}
