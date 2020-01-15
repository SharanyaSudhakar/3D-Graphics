using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBarrier : MonoBehaviour
{

    public GameObject sphereTarget = null;
    public GameObject quadObj = null;
    public GameObject normal = null;
    public Vector3 n;

    void Start()
    {
        // the plane and its normal
        n = -transform.forward;
        Vector3 center = quadObj.transform.localPosition;
        normal.transform.localScale = new Vector3(0.2f, 2f, 0.2f);
        normal.transform.localRotation = Quaternion.FromToRotation(Vector3.up, n);
        normal.transform.localPosition = center + (2f * n);
        transform.localRotation = Quaternion.Euler(0, 75, 0);
    }

    public float getTargetMagnitude() { return sphereTarget.transform.localScale.x * transform.localScale.x; }

    public bool getMySide(Vector3 pt) { return Vector3.Dot(pt, getNormal()) > 0; }

    public Vector3 getNormal() { return -transform.forward; }

    public Vector3 getCenter() { return quadObj.transform.position; }

    // Update is called once per frame
    void Update() { }
}
