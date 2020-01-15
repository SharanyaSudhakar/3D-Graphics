using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TheWorld : MonoBehaviour  {

    public SceneNode TheRoot;
    public Button resetBtn = null;

    private void Start()
    {
        resetBtn.onClick.AddListener(resetScene);
    }

    void resetScene()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }

    private void Update()
    {
        Matrix4x4 i = Matrix4x4.identity;
        TheRoot.CompositeXform(ref i);
    }
}
