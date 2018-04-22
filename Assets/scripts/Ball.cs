using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public GolfAgent golfAgent;
    public GameObject terrain;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger! " + other.gameObject.name);
        golfAgent.InHole = true;
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("exit trigger! " + other.gameObject.name);
        golfAgent.InHole = false;
    }

    void OnCollisionEnter(Collision  other)
    {
        //Debug.Log("On Colission Enter:  " + other.gameObject.name + "  terrain = " + terrain.name);
        if(other.gameObject.tag == "OutOfBounds")
        {
            Debug.Log("resetting ball position!");
            golfAgent.ResetBallPosition();
        }
    }    
}
