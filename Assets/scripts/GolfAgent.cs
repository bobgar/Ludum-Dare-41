using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfAgent : MonoBehaviour {

    public static int WALK_SPEED = 4;
    public static float WAIT_TIME = 1f;

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
    public Ball ball;

    public State _state;
    public State _lastState;
    public int swingCount = 0;

    public bool InHole { get; set; }

    private GolfCourse _course;
    private Hole _curHole;
    private int _curScore;
    private float _maxSwingStrength = 800f;    

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        switch (_state)
        {
            case State.WALKING_TO_HOLE:
                float speed = Time.deltaTime * WALK_SPEED;
                Vector3 goalVec = (_curHole.start.transform.position + _curHole.start.startLocation) - character.transform.position - new Vector3(0, 0,0.5f); 
                if (goalVec.magnitude > speed)
                {
                    character.transform.position += goalVec.normalized * speed;
                    character.transform.LookAt(_curHole.transform.position +_curHole.start.startLocation);
                }
                else
                {
                    //Arrived at hole!
                    character.transform.position += goalVec;
                    ChangeState(State.WAITING_FOR_NEXT_SWING);
                }
                break;
            case State.WALKING_TO_BALL:
                speed = Time.deltaTime * WALK_SPEED;
                goalVec = ball.transform.position - character.transform.position - new Vector3(0,0,0.5f);
                if (goalVec.magnitude > speed)
                {
                    character.transform.position += goalVec.normalized * speed;
                    character.transform.LookAt(ball.transform.position);
                }
                else
                {
                    //Arrived at ball!
                    character.transform.position += goalVec;
                    ChangeState(State.WAITING_FOR_NEXT_SWING);
                }
                break;
            case State.WAITING_FOR_BALL_TO_STOP:
                if (ball.GetComponent<Rigidbody>().velocity.magnitude < .1f)
                {
                    if (InHole)
                    {
                        //Hole complete!  Handle move to next hole?
                        _curHole = _course.GetNextHole(_curHole);
                        if (_curHole == null)
                        {
                            _course.agents.Remove(this);
                            GameObject.Destroy(this.gameObject);
                        }
                        else
                        {
                            ball.gameObject.SetActive(false);
                            ChangeState(State.WALKING_TO_HOLE);
                        }
                    }
                    else
                    {
                        ChangeState(State.WALKING_TO_BALL);
                    }
                }
                break;
        }
	}

    public void ResetBallPosition()
    {
        ball.transform.position = (_curHole.start.transform.position + _curHole.start.startLocation);
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void SetupAgent(GolfCourse course)
    {
        _course = course;
        _curHole = course.holes[0];
        ChangeState(State.WALKING_TO_HOLE);        
   }

    private void ChangeState(State s)
    {
        if (s == _state)
            return;
        _lastState = _state;
        _state = s;
        switch(s)
        {
            case State.WALKING_TO_HOLE:
                //You've picked up your ball
                ball.gameObject.SetActive(false);
                Debug.Log("WALKING TO HOLE");
                break;
            case State.WALKING_TO_BALL:
                Debug.Log("WALKING TO BALL");
                break;
            case State.WAITING_FOR_NEXT_SWING:
                Debug.Log("SWINGING");
                if (!ball.gameObject.activeSelf)
                {
                    ball.gameObject.SetActive(true);
                    ball.transform.position = (_curHole.start.transform.position + _curHole.start.startLocation);
                }
                StartCoroutine(SwingAfterWait());
                break;
            case State.WAITING_FOR_BALL_TO_STOP:
                Debug.Log("WAITING FOR BALL TO STOP");
                break;
        }
    }

    public IEnumerator SwingAfterWait()
    {
        yield return new WaitForSeconds(WAIT_TIME);
        Vector3 goalVec = CalculateGoalSwing();
        float swingStrength = Mathf.Min(_maxSwingStrength, goalVec.magnitude * 40.0f);
        Debug.Log("Swinging with strength: " + swingStrength);
        ball.GetComponent<Rigidbody>().AddForce(goalVec.normalized * swingStrength);
        swingCount++;
        ChangeState(State.WAITING_FOR_BALL_TO_STOP);
    }

    public Vector3 CalculateGoalSwing()
    {
        Vector3 goalVec = (_curHole.finish.transform.position + _curHole.finish.finishLocation) - ball.transform.position;
        return new Vector3(goalVec.x, 0, goalVec.z);
    }
    
}
