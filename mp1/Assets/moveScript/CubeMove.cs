using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    float startpos, endpos, t;
    bool iscolor;
    Renderer render;
    Material mat, defaultMat;
    // Start is called before the first frame update
    void Start()
    {
        t = 0.0f;
        startpos = 0.0f;
        endpos = 3.0f;
        iscolor = false;
        render = this.GetComponent<Renderer>();
        mat = new Material(Shader.Find("Learning/StdColor"));
        mat.SetColor("_AmbientColor", new Color(0f, 0.95f, 0.45f, 1f));
        mat.SetColor("_Color", new Color(1f, 0.15f, 0.2f, 1f));
        mat.SetFloat("_ColorPower", 7.1f);
        defaultMat = new Material(Shader.Find("Learning/StdDiffuse"));
        defaultMat.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 90 * Time.deltaTime, 0);
        this.transform.position = new Vector3(this.transform.position.x,
            0.5f + Mathf.Lerp(startpos, endpos, t),
            this.transform.position.z);
        t += 0.4f * Time.deltaTime;
        if (t > 1.0f)
        {
            float temp = startpos;
            startpos = endpos;
            endpos = temp;
            t = 0.0f;
            if(!iscolor)
            {
                render.material = mat;
                iscolor = true;
            }
            else
            {
               render.material = defaultMat;
               iscolor = false;
            }
        }
    }
}
