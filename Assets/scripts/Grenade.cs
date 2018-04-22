using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    private float timeToExplode = 2f;
    public float power = 10f;
    public float radius = 5f;
    private float upforce = .5f;

	// Use this for initialization
	void Start () {
        StartCoroutine(Explode());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(timeToExplode);
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, radius);
        //Debug.Log("collider count: "+ colliders.Length);
        for(int i = 0; i < colliders.Length; i++)
        {
            //Debug.Log("exploding " + colliders[i].gameObject.name);
            Rigidbody r = colliders[i].GetComponent<Rigidbody>();
            if (r != null)
            {
                //Debug.Log(r);
                r.AddExplosionForce(power, this.transform.position, radius, upforce, ForceMode.Impulse);
            }
        }

        GameObject.Destroy(this.gameObject);
    }
}
