using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour {
    LineSegment[] mNormals;

    void InitNormals(Vector3[] v, Vector3[] n)
    {
        mNormals = new LineSegment[v.Length];
        for (int i = 0; i < v.Length; i++)
        {
            GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            mNormals[i] = o.AddComponent<LineSegment>();
            mNormals[i].SetWidth(0.02f);
            mNormals[i].transform.SetParent(this.transform);
        }
        UpdateNormals(v, n);
    }
     
    void UpdateNormals(Vector3[] v, Vector3[] n)
    {
        for (int i = 0; i < v.Length; i++)
        {
            mNormals[i].SetEndPoints(v[i], v[i] + 1.0f * n[i]);
        }
    }

    Vector3 FaceNormal(Vector3[] v, int i0, int i1, int i2)
    {
        Vector3 a = v[i1] - v[i0];
        Vector3 b = v[i2] - v[i0];
        return Vector3.Cross(a, b).normalized;
    }

    void ComputeNormals(Vector3[] v, Vector3[] n)
    {
        Vector3[] triNormal = new Vector3[N * N * 2];
        for (int i = 0; i < N * N * 2; i++)
        {
            int t1 = t[i * 3];
            int t2 = t[i * 3 + 1]; 
            int t3 = t[i * 3 + 2];
            triNormal[i] = FaceNormal(v, t1, t2, t3);
        }
        
        // top left corner
        n[0] = (triNormal[0] + triNormal[1]).normalized;
        // top right corner
        n[N] = triNormal[N * 2 - 1].normalized;
        // bottom left corner
        n[N * (N + 1)] = triNormal[(N * 2) * (N - 1)].normalized;
        // bottom right corner
        n[(N + 1) * ( N + 1) - 1] = (triNormal[N * N * 2 - 1] + triNormal[N * N * 2 - 2]).normalized;

        // top and bottom sides
        int startTop = 1;
        int startBottom = (N * 2) * (N - 1);
        for (int b = 1; b < N; b++)
        {
            n[b] = (triNormal[startTop] + triNormal[startTop + 1] + triNormal[startTop + 2]).normalized;
            n[b + (N + 1) * N] = (triNormal[startBottom] + triNormal[startBottom + 1] + triNormal[startBottom + 2]).normalized;
            startTop += 2;
            startBottom += 2;
        }

        // right and left sides
        int startRight = (N * 2) - 1;
        int startLeft = 0;
        for (int a = N + 1; a < (N + 1) * N; a += (N + 1))
        {
            n[a] = (triNormal[startLeft] + triNormal[startLeft + (N * 2)] + triNormal[startLeft + (N * 2) + 1]).normalized;
            startLeft += (N * 2);
            n[a + N] = (triNormal[startRight] + triNormal[startRight - 1] + triNormal[startRight + (N * 2)]).normalized;
            startRight += (N * 2);
        }

        // inner
        int startI = 0;
        for (int d = N + 1; d < (N + 1) * N; d += (N + 1))
        {
            for (int c = 1; c < N; c++)
            {
                n[c + d] = (triNormal[startI] + triNormal[startI + 1] + triNormal[startI + 2] 
                    + triNormal[startI + (N * 2) + 1] + triNormal[startI + (N * 2) + 2] + triNormal[startI + (N * 2) + 3]).normalized;
                startI += 2;
            }
            startI += 2;
        }
        UpdateNormals(v, n);
    }
}
