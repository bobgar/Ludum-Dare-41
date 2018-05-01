using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject aimTarget;
    public float shotPower;
    public float rateOfFire;
    public TowerSelectMenu.TowerItem towerItem;
    public int rateOfFireLevel = 0;
    public int powerLevel = 0;
    protected float fireTimer = 0;    

    public virtual void Setup(GameObject target)
    {
        aimTarget = target;
        rateOfFire = towerItem.rateOfFireLevels[0];
        shotPower = towerItem.powerLevels[0];
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpgradePower()
    {
        powerLevel++;
        shotPower = towerItem.powerLevels[powerLevel];
    }

    public void UpgradeRateOfFire()
    {
        rateOfFireLevel++;
        rateOfFire = towerItem.rateOfFireLevels[rateOfFireLevel];
    }


    public void OnPointerClick(PointerEventData pointerEventData)
    {
        TowerUpgrade.instance.ShowMenu(this);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Debug.Log(name + " Enter");
        //gameObject.GetComponentInChildren<Renderer>().material.color = Color.yellow;
        //gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Debug.Log(name + " Exit");
        //gameObject.GetComponentInChildren<Renderer>().material.color = Color.white;
        //gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
