using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
    private float maxDist = 10f;
    public float power = 10f;
    public float radius = 2f;
    public float speed = 1f;
    public float upforce = .3f;
    public GameObject target;

    // Use this for initialization
    void Start () {
        
	}

    public void Setup()
    {
        FindTarget();
    }

    void FindTarget()
    {
        float minDist = maxDist;
        GameObject curBestTarget = null;
        foreach (GolfAgent a in GolfCourse.instance.agents)
        {
            float dist = Vector3.Distance(a.ball.transform.position, this.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                curBestTarget = a.ball.gameObject;
            }
        }
        if (curBestTarget != null)
        {
            target = curBestTarget;
        }
        else
        {
            Explode();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            FindTarget();
            return;
        }

        var s = Time.deltaTime * speed;
        this.transform.LookAt(target.transform);
        Vector3 goalVec = target.transform.position - this.transform.position;
        if(goalVec.magnitude > maxDist)
        {
            FindTarget();
        }
        else if (goalVec.magnitude > .5)
        {
            this.transform.position += goalVec.normalized * s;                        
        }
        else
        {
            Explode();
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, radius);
        //for (int i = 0; i < colliders.Length; i++)
        //{
        if (target)
        {
            Rigidbody r = target.GetComponent<Rigidbody>();
            if (r != null)
            {
                r.AddExplosionForce(power, this.transform.position, radius, upforce, ForceMode.Impulse);
            }
        }
        //}

        GameObject.Destroy(this.gameObject);
    }
}
