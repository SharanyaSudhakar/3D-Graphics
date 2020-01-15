using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XformLoader : MonoBehaviour
{
    public enum Direction { UP, RIGHT, FORWARD};
    public Direction dir = 0;

    // to support slow rotation about the y-axis
    public float kRotateDelta = 45; // per second
    private float IncSign = 1;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        IncrementXform();
    }

    Vector3 GetDir(Direction d)
    {
        switch(d)
        {
            case Direction.UP:
                return Vector3.up;
            case Direction.FORWARD:
                return Vector3.forward;
            case Direction.RIGHT:
                return Vector3.right;
        }
        return transform.up;
    }

    void IncrementXform()
    {
        // rotation 
        if ((transform.localRotation.eulerAngles.x < 0) || (transform.localRotation.eulerAngles.x > 180) || (transform.localRotation.eulerAngles.y < 0) || (transform.localRotation.eulerAngles.y > 180) || (transform.localRotation.eulerAngles.z < 0) || (transform.localRotation.eulerAngles.z > 180))
            IncSign *= -1;
        Quaternion q = Quaternion.AngleAxis(IncSign * kRotateDelta * Time.fixedDeltaTime, GetDir(dir));
        transform.localRotation = q * transform.localRotation;
    }
}
