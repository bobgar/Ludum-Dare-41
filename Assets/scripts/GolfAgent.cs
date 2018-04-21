using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfAgent : MonoBehaviour {

    public static int WALK_SPEED = 10;

    public enum State
    {
        IDLE,
        WALKING_TO_HOLE,
        WALKING_TO_BALL,        
        WAITING_FOR_NEXT_SWING,
        WAITING_FOR_BALL_TO_STOP
    }

    //TODO do something with this!
    public GameObject character;
    public GameObject ball;

    public State _state;
    public State _lastState;
    
    private GolfCourse _course;
    private Hole _curHole;
    private int _curScore;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        switch (_state)
        {
            case State.WALKING_TO_HOLE:
                float speed = Time.deltaTime * WALK_SPEED;
                Vector3 goalVec = (_curHole.start.transform.position + _curHole.start.startLocation) - character.transform.position;
                if (goalVec.magnitude > speed)
                {
                    character.transform.position += goalVec.normalized * speed;
                    character.transform.LookAt(_curHole.transform.position +_curHole.start.startLocation);
                }
                else
                {
                    //Arrived at hole!
                    character.transform.position += goalVec;
                    changeState(State.WAITING_FOR_NEXT_SWING);
                }
                break;
        }
	}

    public void SetupAgent(GolfCourse course)
    {
        _course = course;
        _curHole = course.holes[0];
        changeState(State.WALKING_TO_HOLE);        
   }

    private void changeState(State s)
    {
        if (s == _state)
            return;
        _lastState = _state;
        _state = s;
        switch(s)
        {
            case State.WALKING_TO_HOLE:
                //You've picked up your ball
                ball.SetActive(false);
                break;
            case State.WALKING_TO_BALL:
                break;
            case State.WAITING_FOR_NEXT_SWING:
                if (!ball.activeSelf)
                {
                    ball.SetActive(true);
                    ball.transform.position = (_curHole.start.transform.position + _curHole.start.startLocation);
                }
                break;
        }
    }
    
}
