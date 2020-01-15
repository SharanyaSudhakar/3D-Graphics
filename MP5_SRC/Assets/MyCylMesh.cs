using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyCylMesh : MonoBehaviour
{

    public int N; //N = resolution 
    public int M;  // M = height
    public int[] t;
    public static bool meshChange;

    private float deg;
    private float degI;
    private int vertices;
    private int triangles; //y=height 

    private float cylHeight;
    private float regRadius;
    private Vector3[] cylRadius;
    private float cylRotation;

    private Vector3[] v;
    private Vector3[] n;
    private Mesh theMesh;


    // Use this for initialization
    void Start()
    {
        N = 3; M = 4;
        vertices = (N + 1) * (M + 1);
        triangles = N * M * 2;

        v = new Vector3[vertices];   // 2x2 mesh needs 3x3 vertices
        t = new int[triangles * 3];         // Number of triangles: 2x2 mesh and 2x triangles on each mesh-unit
        n = new Vector3[vertices];   // MUST be the same as number of vertices 
        cylRadius = new Vector3[M];

        regRadius = 1;
        cylHeight = 5;
        cylRotation = 180;
        meshChange = false;

        if (!meshChange)
            setRegularRadius();

        ReDrawMesh(N);

    }

    void makeMesh(float rot, float radius, float height)
    {
        theMesh = GetComponent<MeshFilter>().mesh;   // get the mesh component
        theMesh.Clear();    // delete whatever is there!!

        vertices = (N + 1) * (M + 1);
        triangles = N * M * 2;

        v = new Vector3[vertices];   // 2x2 mesh needs 3x3 vertices
        t = new int[triangles * 3];         // Number of triangles: 2x2 mesh and 2x triangles on each mesh-unit
        n = new Vector3[vertices];   // MUST be the same as number of vertices

        if (!meshChange)
            setRegularRadius();

        degI = rot / N;

        for (int i = 0; i <= M; i++)
        {
            for (int j = 0; j <= N; j++)
            {
                int index = j + (i * (N + 1));
                v[index] = new Vector3(cylRadius[i].x * Mathf.Cos(CalcDeg(deg)), cylRadius[i].y, cylRadius[i].z * Mathf.Sin(CalcDeg(deg)));
                n[index] = new Vector3(0, 1, 0);
                deg += degI;
            }
            deg = 0;
        }

        //for loop for t
        for (int i = 0, ti = 0; i < M; i++) //ti = tIndex
        {
            for (int j = 0; j < N; j++)
            {
                int t1, t2, t3, t4, t5, t6;
                t1 = i * (N + 1) + j;
                t2 = j + ((i + 1) * (N + 1));
                t3 = t2 + 1;

                t4 = t1;
                t5 = t3;
                t6 = t4 + 1;

                t[ti++] = t1;
                t[ti++] = t2;
                t[ti++] = t3;

                t[ti++] = t4;
                t[ti++] = t5;
                t[ti++] = t6;
            }
        }

        theMesh.vertices = v; //  new Vector3[3];
        theMesh.triangles = t; //  new int[3];
        theMesh.normals = n;

        InitControllers(v);
        InitNormals(v, n);
    }

    void setRegularRadius() //radius for regular M
    {
        if (cylRadius.Length != M + 1)
            cylRadius = new Vector3[M + 1];
        float h = cylHeight / 2;
        float rowH = cylHeight / M;
        for (int i = 0; i < cylRadius.Length; i++)
        {
            cylRadius[i] = new Vector3(regRadius, h, regRadius);
            h -= rowH;
        }
    }

    public void radiusChange(Vector3 newPos, int index)
    {
        deg = degI;
        for (int j = index + 1; j <= index + N; j++)
        {
            mControllers[j].transform.localPosition = new Vector3(newPos.x * Mathf.Cos(CalcDeg(deg)), newPos.y, newPos.x * Mathf.Sin(CalcDeg(deg)));
            deg += degI;
        }

        cylRadius[index / (N + 1)] = new Vector3(newPos.x, newPos.y, newPos.x);
    }

    // Update is called once per frame
    void Update()
    {
        Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector3[] v = theMesh.vertices;
        Vector3[] n = theMesh.normals;
        for (int i = 0; i < mControllers.Length; i++)
        {
            v[i] = mControllers[i].transform.localPosition;
        }

        ComputeNormals(v, n);

        theMesh.vertices = v;
        theMesh.normals = n;

    }

    //degree to radians
    float CalcDeg(float deg)
    {
        return deg * Mathf.PI / 180.0f;
    }

    public void ReDrawMesh()
    {
        foreach (Transform child in gameObject.transform)
        {
            DestroyObject(child.gameObject);
        }
        makeMesh(cylRotation, regRadius, cylHeight / 2);
    }

    public void ReDrawMesh(int newN)
    {
        N = newN;
        if (M < newN)
            M = newN;
        else
            M = (int)cylHeight;
        foreach (Transform child in gameObject.transform)
        {
            DestroyObject(child.gameObject);
        }
        meshChange = false;
        makeMesh(cylRotation, regRadius, cylHeight / 2);
    }

    public void incNMesh(int newN)
    {
        if (N != newN)
            N = newN;
        foreach (Transform child in gameObject.transform)
        {
            DestroyObject(child.gameObject);
        }
        makeMesh(cylRotation, regRadius, cylHeight / 2);
    }

    public void RotationChange(float newRot)
    {
        cylRotation = newRot;
        foreach (Transform child in gameObject.transform)
        {
            DestroyObject(child.gameObject);
        }
        makeMesh(cylRotation, regRadius, cylHeight / 2);
    }

    public void meshExtrude(int val)
    {
        cylHeight = val;
        M = (int)cylHeight *2;
        foreach (Transform child in gameObject.transform)
        {
            DestroyObject(child.gameObject);
        }
        meshChange = false;
        makeMesh(cylRotation, regRadius, cylHeight / 2);
    }
}
