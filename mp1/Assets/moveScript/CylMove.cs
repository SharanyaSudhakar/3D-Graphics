using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylMove : MonoBehaviour
{
    float startpos, endpos, t;
    bool iscolor;
    Renderer render;
    Material mat, defaultMat;
    // Start is called before the first frame update
    void Start()
    {
        t = 0.0f;
        startpos = transform.position.z;
        endpos = (startpos - 3 <= -5) ? startpos + 3 : startpos - 3;
        iscolor = false;
        render = this.GetComponent<Renderer>();
        mat = new Material(Shader.Find("Learning/StdColor"));
        mat.SetColor("_AmbientColor", new Color(0.5f, 0f, 0.9f, 1f));
        mat.SetColor("_Color", new Color(0.1f, 1f, 0.9f, 1f));
        mat.SetFloat("_ColorPower", 10f);
        defaultMat = new Material(Shader.Find("Learning/StdDiffuse"));
        defaultMat.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x,
             this.transform.position.y, Mathf.Lerp(startpos, endpos, t));
        t += 0.4f * Time.deltaTime;
        if (t > 1.0f)
        {
            float temp = startpos;
            startpos = endpos;
            endpos = temp;
            t = 0.0f;
            if (!iscolor)
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
