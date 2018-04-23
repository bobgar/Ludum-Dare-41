using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgrade : MonoBehaviour {

    public static TowerUpgrade instance;

    public Button rateOfFireButton;
    public Button powerButton;

    private Tower _curTower;
    
    // Use this for initialization
    void Start () {

        instance = this;
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowMenu(Tower tower)
    {
        _curTower = tower;
        gameObject.SetActive(true);
        if(tower.rateOfFireLevel  < tower.towerItem.rateOfFireUpgradeCosts.Length)
        {
            rateOfFireButton.GetComponentInChildren<Text>().text = "Level Up Rate of Fire   $" + tower.towerItem.rateOfFireUpgradeCosts[tower.rateOfFireLevel];
            rateOfFireButton.interactable = tower.towerItem.rateOfFireUpgradeCosts[tower.rateOfFireLevel] <= GolfCourse.instance.curMoney;
        }
        else
        {
            rateOfFireButton.GetComponentInChildren<Text>().text = "RATE OF FIRE MAX";
            rateOfFireButton.interactable = false;
        }
        if (tower.powerLevel < tower.towerItem.rateOfFireUpgradeCosts.Length)
        {
            powerButton.GetComponentInChildren<Text>().text = "Level Up Power   $" + tower.towerItem.powerLevelUpgradeCosts[tower.powerLevel];
            powerButton.interactable = tower.towerItem.powerLevelUpgradeCosts[tower.powerLevel] <= GolfCourse.instance.curMoney;
        }
        else
        {
            powerButton.GetComponentInChildren<Text>().text = "POWER MAX";
            powerButton.interactable = false;
        }
    }

    public void UpgradeRateOfFire()
    {
        if (_curTower != null)
        {
            GolfCourse.instance.SetMoney(GolfCourse.instance.curMoney - _curTower.towerItem.rateOfFireUpgradeCosts[_curTower.rateOfFireLevel]);
            _curTower.UpgradeRateOfFire();
        }
        this.gameObject.SetActive(false);
    }

    public void UpgradePower()
    {
        if (_curTower != null)
        {
            GolfCourse.instance.SetMoney(GolfCourse.instance.curMoney - _curTower.towerItem.powerLevelUpgradeCosts[_curTower.powerLevel]);
            _curTower.UpgradePower();
        }
        this.gameObject.SetActive(false);
    }
}
