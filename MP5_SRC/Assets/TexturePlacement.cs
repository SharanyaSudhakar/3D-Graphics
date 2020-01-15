using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexturePlacement : MonoBehaviour {

    public Vector2 translate = Vector2.zero;
    public Vector2 scale = Vector2.one;
    public float rotate = 0f;
    Vector2[] mInitUV = null; // initial values

    public void SaveInitUV(Vector2[] uv)
    {
        mInitUV = new Vector2[uv.Length];
        for (int i = 0; i < uv.Length; i++)
            mInitUV[i] = uv[i];
    }

    public void Translate(Vector2 t)
    {
        translate += t;
        Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector2[] uv = theMesh.uv;
        Matrix3x3 myMatrix = Matrix3x3Helpers.CreateTranslation(t);
        for (int i = 0; i < uv.Length; i++)
        {
            uv[i] = Matrix3x3.MultiplyVector2(myMatrix, uv[i]);
        }
        theMesh.uv = uv;
    }

    public void Rotate(float r)
    {
        rotate += r;
        Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector2[] uv = theMesh.uv;
        Matrix3x3 myMatrix = Matrix3x3Helpers.CreateRotation(r);
        for (int i = 0; i < uv.Length; i++)
        {
            uv[i] = Matrix3x3.MultiplyVector2(myMatrix, uv[i]);
        }
        theMesh.uv = uv;

    }

    public void Scale(Vector2 s)
    {
        scale.x *= s.x;
        scale.y *= s.y;
        Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector2[] uv = theMesh.uv;
        Matrix3x3 myMatrix = Matrix3x3Helpers.CreateScale(s);
        for (int i = 0; i < uv.Length; i++)
        {
            uv[i] = Matrix3x3.MultiplyVector2(myMatrix, uv[i]);
        }
        theMesh.uv = uv;
    }

    public void SetTRS(Vector2 t, float r, Vector2 s)
    {
        translate = t;
        rotate = r;
        scale = s;
    }
}
