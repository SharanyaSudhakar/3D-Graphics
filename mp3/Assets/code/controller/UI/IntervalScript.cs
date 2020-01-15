using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IntervalScript : MonoBehaviour {

    public Slider Y = null;
    public TheWorldRoom world = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setIntervalTime()
    {
        world.setTimeInterval(Y.value);
    }
}
