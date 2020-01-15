using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorldRoom : MonoBehaviour
{
    public GameObject P1, P2, P3, P4;
    public List<GameObject> travellingBalls = new List<GameObject>();
    float timeinterval, lfTime, speed;
    GameObject tball;
    public GameObject aimLine, bigLine;
    public TheBarrier plane;
    public TravellerControl TravellerCtrl;
    public XFormControl xformCtrl;

    private void Start()
    {
        aimLine = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        bigLine = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        startup();
    }

    void startup()
    {
        setTimeInterval(1f);
        set_LifeTime(10f);
        setSpeed(15f);
        InvokeRepeating("duplicateTBall", 0f, timeinterval);
    }

    public void setTimeInterval(float t)
    {
        CancelInvoke();
        timeinterval = t;
        InvokeRepeating("duplicateTBall", 0f, timeinterval);
    }

    public float getTimeInterval() { return timeinterval; }

    public void set_LifeTime(float t) { lfTime = t; }

    public float get_LifeTime() { return lfTime; }

    public void setSpeed(float dt) { speed = (16 - dt) * 60; }

    public float getSpeed() { return Mathf.Abs(speed / 60 - 16); }

    private void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

        drawLine(P1.transform.localPosition, P2.transform.localPosition, 0.2f, ref aimLine);
        drawLine(P3.transform.localPosition, P4.transform.localPosition, 3f, ref bigLine);
    }

    public void drawLine(Vector3 one, Vector3 two, float radius, ref GameObject line)
    {
        Vector3 v = two - one;
        float D = v.magnitude;
        v.Normalize();

        line.transform.localScale = new Vector3(radius, D , radius);
        line.transform.localRotation = Quaternion.FromToRotation(Vector3.up, v);
        line.transform.localPosition = one + D * v;
        line.GetComponent<Renderer>().material.color = Color.black;
    }

    void duplicateTBall()
    {
        tball = Instantiate(Resources.Load("3d/travellingBalls")) as GameObject;
        tball.transform.localPosition = P1.transform.localPosition;
        travellingBalls.Add(tball);
        tball.GetComponent<TravellingBallControl>().P1 = P1;
        tball.GetComponent<TravellingBallControl>().P2 = P2;
        tball.GetComponent<TravellingBallControl>().P3 = P3;
        tball.GetComponent<TravellingBallControl>().P4 = P4;
        tball.GetComponent<TravellingBallControl>().bigline = bigLine;
        tball.GetComponent<TravellingBallControl>().plane = plane;
        tball.GetComponent<TravellingBallControl>().setLifeTime(lfTime);
        tball.GetComponent<TravellingBallControl>().setSpeed(speed);
        tball.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

    }

    public void destroyBall(GameObject TBALL)
    {
        Destroy(TBALL);
    }
    public void moveEndPoints(GameObject obj, Vector3 pt)
    {
        if (obj.GetComponent<QuadControl>() != null)
            //  Debug.Log("");
            obj.GetComponent<QuadControl>().MovePoint(pt);
    }
}
