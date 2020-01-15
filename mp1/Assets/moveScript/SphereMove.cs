using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMove : MonoBehaviour
{
    float startpos, endpos, t;
    bool iscolor;
    Renderer render;
    Material mat, defaultMat;
    // Start is called before the first frame update
    void Start()
    {
        t = 0.0f;
        startpos = transform.position.x;
        endpos = (startpos + 3 >= 5) ? startpos - 3 : startpos + 3;
        iscolor = false;
        render = this.GetComponent<Renderer>();
        mat = new Material(Shader.Find("Learning/StdColor"));
        mat.SetColor("_AmbientColor", new Color(.5f, 0.2f, 1f, 1f));
        mat.SetColor("_Color", new Color(0.5f, 0f, 1f, 1f));
        mat.SetFloat("_ColorPower", 8.4f);
        defaultMat = new Material(Shader.Find("Learning/StdDiffuse"));
        defaultMat.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(Mathf.Lerp(startpos, endpos, t), this.transform.position.y,
            this.transform.position.z);
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
