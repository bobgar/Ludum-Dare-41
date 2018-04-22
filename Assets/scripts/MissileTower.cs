using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTower : Tower {
    public Animator anim;
    public GameObject top;
    public Missile missilePrefab;

    private float[] rateOfFireLevels = { 5, 4, 3, 2 };
    private float[] powerLevels = { 1, 2, 3, 4 };
    private float[] speedLevels = { 2f, 3f, 4f, 5f };
    ///private float[] explosionRadiusLevels = { 3, 5, 7, 9 };

    private int rateOfFireLevel = 0;
    private int powerLevel = 0;
    private int speedLevel = 0;

    // Use this for initialization
    void Start()
    {
        //top.transform.LookAt(aimTarget.transform);
        if (aimTarget != null)
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
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= rateOfFire)
        {
            fireTimer = 0;
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire()
    {
        anim.SetTrigger("fire");
        yield return new WaitForSeconds(.25f);
        Missile m = GameObject.Instantiate<Missile>(missilePrefab);
        m.radius = 3;// explosionRadiusLevels[explosionRadiusLevel];
        m.speed = speedLevels[speedLevel];
        m.power = powerLevels[powerLevel];
        Vector3 goalVec = (aimTarget.transform.position - top.transform.position).normalized;
        m.transform.position = top.transform.position + new Vector3(0, 1, 0) + goalVec.normalized * 1f;

        m.Setup();
        //m.GetComponent<Rigidbody>().AddForce(new Vector3(goalVec.x * shotPower, 200f, goalVec.z * shotPower));
    }
}
