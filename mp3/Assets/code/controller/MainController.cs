using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainController : MonoBehaviour
{

    public Camera camera1 = null;
    public TheWorldRoom Room = null;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mouseClickSelection();
    }

    public void mouseClickSelection()
    {
        int layermask = LayerMask.GetMask("walls");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
                if (Physics.Raycast(ray, out hit, 100f, layermask))
                {
                    Room.moveEndPoints(hit.transform.gameObject, hit.point);
                }
        }

    }
}
