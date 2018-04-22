using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolfCourse : MonoBehaviour {

    public static GolfCourse instance;

    public Text waveText;
    public Text par;
    public Text waveScore;
    public Text goalScore;
    public Text golfersLeftText;

    public List<Wave> waves;

    public GolfAgent golfAgentPrefab;
    public Transform agentSpawnPoint;

    //ordered list of start and end location for each hole.
    //for now we just use parallel lists.
    public List<Hole> holes;

    public List<GolfAgent> agents;

    private long totalScore = 0;
    private long totalFinished = 0;

    private int curWave = 0;
    private int golfersLeft;

	// Use this for initialization
	void Start () {
        //AddAgent();
        //StartCoroutine(TestAddAgents());

        StartCoroutine(StartWave(0));

        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator StartWave(int waveIndex)
    {
        curWave = waveIndex;
        Wave w = waves[waveIndex];

        waveText.text = "Wave: " + (waveIndex + 1);
        par.text = "Par: " + holes[0].par;
        waveScore.text = "Wave Score: 0";
        goalScore.text = "Goal Score: " + w.goalScore;
        golfersLeft = w.golfers;
        golfersLeftText.text = "Golfers Left: " + golfersLeft;

        for (int i = 0; i < w.golfers; i++)
        {
            AddAgent();
            yield return new WaitForSeconds(w.timeBetweenGolfers);
        }
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

    public Hole FinishHoleAndGetNext(Hole hole, int swingCount)
    {
        golfersLeft--;
        golfersLeftText.text = "Golfers Left: " + golfersLeft;
        totalScore += swingCount;
        totalFinished++;
        long comparedToPar = totalScore - (hole.par * totalFinished);
        waveScore.text = "Wave Score : " + comparedToPar;
        
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
