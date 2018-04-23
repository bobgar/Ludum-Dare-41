using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelectMenu : MonoBehaviour {

    [System.Serializable]
    public struct TowerItem
    {
        public string name;
        public int cost;
        public Tower tower;
    }

    public TowerItem missileItem;
    public TowerItem grenadeItem;

    public Button missileButton;
    public Button grenadeButton;

    public static TowerSelectMenu instance;

    private TowerPlacement _curPlacement;

	// Use this for initialization
	void Start () {
        instance = this;
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowMenu(TowerPlacement placement)
    {
        gameObject.SetActive(true);
        missileButton.GetComponentInChildren<Text>().text = missileItem.name +"   $" + missileItem.cost;
        grenadeButton.GetComponentInChildren<Text>().text = grenadeItem.name + "   $" + grenadeItem.cost;
        
        missileButton.interactable = GolfCourse.instance.curMoney >= missileItem.cost;
        grenadeButton.interactable = GolfCourse.instance.curMoney >= grenadeItem.cost;
        _curPlacement = placement;
    }

    public void BuyMissileTower()
    {
        if (_curPlacement != null)
        {
            GolfCourse.instance.SetMoney(GolfCourse.instance.curMoney - missileItem.cost);
            _curPlacement.InstantiateTower(missileItem);
        }
        this.gameObject.SetActive(false);
    }

    public void BuyGrenadeTower()
    {
        if (_curPlacement != null)
        {
            GolfCourse.instance.SetMoney(GolfCourse.instance.curMoney - grenadeItem.cost);
            _curPlacement.InstantiateTower(grenadeItem);
        }
        this.gameObject.SetActive(false);
    }
}