using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyCylMesh : MonoBehaviour
{
    LineSegment[] mNormals;

    void InitNormals(Vector3[] v, Vector3[] n)
    {
        mNormals = new LineSegment[v.Length];
        for (int i = 0; i < v.Length; i++)
        {
            GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            mNormals[i] = o.AddComponent<LineSegment>();
            mNormals[i].SetWidth(0.05f);
            mNormals[i].transform.SetParent(this.transform);
        }
        UpdateNormals(v, n);
    }

    void UpdateNormals(Vector3[] v, Vector3[] n)
    {
        for (int i = 0; i < v.Length; i++)
        {
            mNormals[i].SetEndPoints(v[i], v[i] - 1.0f * n[i]);
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
        Vector3[] triNormal = new Vector3[triangles];
        for (int i = 0; i < triangles; i++)
        {
            int t1 = t[i * 3];
            int t2 = t[i * 3 + 1];
            int t3 = t[i * 3 + 2];
            triNormal[i] = FaceNormal(v, t1, t2, t3);
        }

        // top left 
        n[0] = (triNormal[0] + triNormal[1]).normalized;
        // top right
        n[N] = triNormal[N * 2 - 1].normalized;
        // bottom left 
        n[vertices-(N+1)] = triNormal[(N * 2) * (M - 1)].normalized;
        // bottom right 
        n[vertices - 1] = (triNormal[triangles - 1] + triNormal[triangles - 2]).normalized;

        // top and bottom 
        int topTri = 1;
        int btmTri = triangles - (2 * N);
        for (int tb = 1; tb < N; tb++)
        {
            n[tb] = (triNormal[topTri] + triNormal[topTri + 1] + triNormal[topTri + 2]).normalized;
            n[vertices - (N+1) + tb] = (triNormal[btmTri] + triNormal[btmTri + 1] + triNormal[btmTri + 2]).normalized;
            topTri += 2;
            btmTri += 2;
        }

        // right and left
        int startRight = (N * 2) - 1;
        int startLeft = 0;
        for (int a = N + 1; a < (N + 1) * M; a += (N + 1))
        {
            n[a] = (triNormal[startLeft] + triNormal[startLeft + (N * 2)] + triNormal[startLeft + (N * 2) + 1]).normalized;
            startLeft += (N * 2);
            n[a + N] = (triNormal[startRight] + triNormal[startRight - 1] + triNormal[startRight + (N * 2)]).normalized;
            startRight += (N * 2);
        }

        // inner
        int startI = 0;
        for (int d = 1; d <  M; d++)
        {
            for (int c = 1; c < N; c++)
            {
                n[c+(d*(N+1))] = (triNormal[startI] + triNormal[startI + 1] + triNormal[startI + 2]
                    + triNormal[startI + (N * 2) + 1] + triNormal[startI + (N * 2) + 2] + triNormal[startI + (N * 2) + 3]).normalized;
                startI += 2;
            }
            startI += 2;
        }
        UpdateNormals(v, n);
    }
}
