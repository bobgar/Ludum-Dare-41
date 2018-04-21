using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfAgent : MonoBehaviour {

    //TODO do something with this!
    public GameObject character;
    public GameObject ball;
    
    private GolfCourse _course;
    private Hole _curHole;
    private int _curScore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetupAgent(GolfCourse course)
    {
        _course = course;
        _curHole = course.holes[0];
    }
}
