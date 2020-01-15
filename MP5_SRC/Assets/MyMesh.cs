using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour
{
    public int N = 2;
    private int[] t;
    private Vector3[] v;
    private Vector2[] uv;
    public Shader shader1 = null;

    // Use this for initialization
    void Start()
    {
        Debug.Assert(shader1 != null);
        Mesh theMesh = GetComponent<MeshFilter>().mesh;   // get the mesh component
        theMesh.Clear();    // delete whatever is there!!

        v = new Vector3[(N + 1) * (N + 1)];   // NxN mesh needs (N + 1)x(N + 1) vertices
        t = new int[N * N * 2 * 3];         // Number of triangles: NxN mesh and 2x triangles on each mesh-unit
        Vector3[] n = new Vector3[(N + 1) * (N + 1)];   // MUST be the same as number of vertices
        uv = new Vector2[(N + 1) * (N + 1)];

        int odd = 1;
        int even = 0;
        float zVal = -1;
        float yVal = 0;
        float xVal = -1;
        float x = 0;
        float y = 0;

        for (int i = 0; i <= (N + 1) * N; i += (N + 1))
        {
            for (int j = 0; j <= N; j++)
            {
                // v and n and uv
                // for cylinder
                if (gameObject.tag == "Cylinder")
                {
                    v[i + j] = new Vector3(xVal, zVal, yVal);
                    n[i + j] = new Vector3(0, 0, 1);
                }
                else
                {
                    // for plane
                    v[i + j] = new Vector3(xVal, yVal, zVal);
                    n[i + j] = new Vector3(0, 1, 0);
                    // need code here to make cylinder
                }

                uv[i + j] = new Vector2(x, y);
                xVal += (float)2 / N;
                x += (float)1 / N;

                // loop to set t
                if (j < N && i == 0)
                {
                    for (int start = j * (N + 1); start < j * (N + 1) + N; start++)
                    {
                        t[odd * 3] = start;
                        t[odd * 3 + 1] = start + N + 2;
                        t[odd * 3 + 2] = start + 1;
                        t[even * 3] = start;
                        t[even * 3 + 1] = start + N + 1;
                        t[even * 3 + 2] = start + N + 2;
                        odd += 2;
                        even += 2;
                    }
                }
            }
            xVal = -1;
            x = 0;
            zVal += (float)2 / N;
            y += (float)1 / N;
        }

        theMesh.vertices = v; //  new Vector3[3];
        theMesh.triangles = t; //  new int[3];
        theMesh.normals = n;
        theMesh.uv = uv;
        InitControllers(v);
        InitNormals(v, n);
        GetComponent<TexturePlacement>().SaveInitUV(uv);
        Renderer r = gameObject.GetComponent<Renderer>();
        r.material.shader = shader1;
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

    public void ResetMesh(int res)
    {
        N = res;
        foreach (Transform child in gameObject.transform)
        {
            DestroyObject(child.gameObject);
        }
        Start();
    }
}
