using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadControl : MonoBehaviour
{

    public GameObject linePt = null;
    public GameObject myQuad = null;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MovePoint(Vector3 newpos)
    {
        linePt.transform.localPosition = newpos;
    }

}
