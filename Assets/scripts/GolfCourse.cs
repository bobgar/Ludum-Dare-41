using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolfCourse : MonoBehaviour {

    public enum STATE
    {
        BUILD,
        WAVE
    }

    public STATE curState = STATE.BUILD;
    public const float BUILD_TIME = 15;

    public static GolfCourse instance;
    
    public Text waveText;
    public Text par;
    public Text waveScore;
    public Text goalScore;
    public Text golfersLeftText;
    public Text moneyText;

    public List<Wave> waves;

    public GolfAgent golfAgentPrefab;
    public Transform agentSpawnPoint;

    public List<Hole> holes;

    public List<GolfAgent> agents;

    public int curMoney = 400;

    private int totalScore = 0;
    private int totalFinished = 0;

    private int curWave = 0;
    private int golfersLeft;

    private float _elapsedBuildTime;

    // Use this for initialization
    void Start () {
        //AddAgent();
        //StartCoroutine(TestAddAgents());

        //StartCoroutine(StartWave(0));
        ChangeState(STATE.BUILD);

        instance = this;

        SetMoney(400);
    }

    public void SetMoney(int money)
    {
        curMoney = money;
        moneyText.text = "Money: $" + curMoney;
    }
	
	// Update is called once per frame
	void Update () {
		switch(curState)
        {
            case STATE.BUILD:
                _elapsedBuildTime += Time.deltaTime;
                if (_elapsedBuildTime > BUILD_TIME)
                {
                    ChangeState(STATE.WAVE);
                }
                else
                {
                    waveText.text = "Wave " + (curWave + 1) + " in " + (int)(BUILD_TIME-_elapsedBuildTime);
                }
                break;
            case STATE.WAVE:
                break;
        }
	}

    private void ChangeState(STATE s)
    {
        curState = s;
        switch (curState)
        {
            case STATE.BUILD:
                _elapsedBuildTime = 0;
                break;
            case STATE.WAVE:
                StartCoroutine(StartWave(curWave));
                break;
        }
    }

    private IEnumerator StartWave(int waveIndex)
    {
        curWave = waveIndex;
        Wave w = waves[waveIndex];

        totalScore = 0;
        totalFinished = 0;

        waveText.text = "Wave: " + (waveIndex + 1);        
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

    public void RemoveAgent(GolfAgent a)
    {
        agents.Remove(a);
        GameObject.Destroy(a.gameObject);
        if(agents.Count == 0)
        {
            curWave++;
            SetMoney(curMoney + totalScore * 10);// - (hole.par * totalFinished) * 10)
            //GetMoneyFromWaveAndReset();
            ChangeState(STATE.BUILD);
        }
    }

    public Hole FinishHoleAndGetNext(Hole hole, int swingCount)
    {
        golfersLeft--;
        golfersLeftText.text = "Golfers Left: " + golfersLeft;
        totalScore += swingCount;
        totalFinished++;
        //long comparedToPar = totalScore - (hole.par * totalFinished);
        waveScore.text = "Wave Score : " + totalScore;
        
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
