using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyCylMesh : MonoBehaviour
{

    GameObject[] mControllers;

    void InitControllers(Vector3[] v)
    {
        mControllers = new GameObject[v.Length];
        for (int i = 0; i < v.Length; i++)
        {
            mControllers[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            mControllers[i].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            mControllers[i].transform.localPosition = v[i];
            mControllers[i].transform.parent = this.transform;
            if (i % (N + 1) == 0)
            {
                mControllers[i].GetComponent<Renderer>().material.color = Color.white;
                mControllers[i].layer = 8;
                mControllers[i].name = i.ToString();//used to identiify which sphere is selected.
            }
            else
                mControllers[i].GetComponent<Renderer>().material.color = Color.black;
        }
    }
}
