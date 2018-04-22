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
        //AddAgent();
        StartCoroutine(TestAddAgents());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator TestAddAgents()
    {
        while(true)
        {
            yield return new WaitForSeconds(3f);
            if (agents.Count < 10)
            {
                AddAgent();
            }
        }
    }

    private void AddAgent()
    {
        GolfAgent ga = GameObject.Instantiate<GolfAgent>(golfAgentPrefab);
        ga.character.transform.position = agentSpawnPoint.position;
        ga.SetupAgent(this);
        agents.Add(ga);
    }

    public Hole GetNextHole(Hole hole)
    {
        for(int i = 0; i < holes.Count; i++)
        {
            if(holes[i] == hole)
            {
                if(i+1 < holes.Count)
                {
                    return holes[i + 1];
                }
                else
                {
                    return null;
                }
            }
        }
        return null;
    }
}
