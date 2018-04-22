using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public GameObject aimTarget;
    public float shotPower = 200;
    public float rateOfFire;
    protected float fireTimer = 0;

    public virtual void Setup(GameObject target)
    {
        aimTarget = target;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
