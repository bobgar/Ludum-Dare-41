using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfCourse : MonoBehaviour {
    public GolfAgent golfAgentPrefab;
    public Transform agentSpawnPoint;

    //ordered list of start and end location for each hole.
    //for now we just use parallel lists.
    public List<Hole> holes;

    public List<GolfAgent> agents;

	// Use this for initialization
	void Start () {
        addAgent();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void addAgent()
    {
        GolfAgent ga = GameObject.Instantiate<GolfAgent>(golfAgentPrefab);
        ga.character.transform.position = agentSpawnPoint.position;
        ga.SetupAgent(this);
        agents.Add(ga);
    }
}
