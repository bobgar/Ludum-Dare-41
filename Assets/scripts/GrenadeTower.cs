using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeTower : Tower {
    public Animator anim;
    public GameObject top;
    public Grenade grenadePrefab;

    private float[] rateOfFireLevels = { 6, 5, 4, 3};
    private float[] powerLevels = { 3, 6, 10, 15 };
    private float[] explosionRadiusLevels = { 3, 5, 7, 9 };

    private int rateOfFireLevel = 0;
    private int powerLevel = 0;
    private int explosionRadiusLevel = 0;

    // Use this for initialization
    void Start () {
        //top.transform.LookAt(aimTarget.transform);
        if(aimTarget != null)
        {
            Setup(aimTarget);
        }
	}

    public override void Setup(GameObject target)
    {
        base.Setup(target);
        top.transform.LookAt(aimTarget.transform.position);
        rateOfFire = rateOfFireLevels[rateOfFireLevel];
    }

    // Update is called once per frame
    void Update () {
        fireTimer += Time.deltaTime;
        if(fireTimer >= rateOfFire)
        {
            fireTimer = 0;
            StartCoroutine( Fire());
        }
	}

    IEnumerator Fire()
    {
        anim.SetTrigger("fire");
        yield return new WaitForSeconds(.25f);
        Grenade g = GameObject.Instantiate<Grenade>(grenadePrefab);
        g.radius = explosionRadiusLevels[explosionRadiusLevel];
        g.power = powerLevels[powerLevel];
        Vector3 goalVec = (aimTarget.transform.position  - top.transform.position).normalized;
        g.transform.position = top.transform.position + new Vector3(0,1,0) +  goalVec.normalized * 1f;
        g.GetComponent<Rigidbody>().AddForce(new Vector3(goalVec.x * shotPower, 200f, goalVec.z * shotPower) );
    }
}
