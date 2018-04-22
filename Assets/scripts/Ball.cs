using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public GolfAgent golfAgent;
    public GameObject terrain;
    public Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (rigidBody.velocity.magnitude < .75f)
        {
            rigidBody.velocity *= .975f;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("trigger! " + other.gameObject.name);
        golfAgent.InHole = true;
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("exit trigger! " + other.gameObject.name);
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
