using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLibrary : MonoBehaviour {

    public static TowerLibrary instance;

    [System.Serializable]
    public struct TowerItem
    {
        public string name;
        public int cost;
        public Tower tower;        
    }

    public List<TowerItem> items;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
