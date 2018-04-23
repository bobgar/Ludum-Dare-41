using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileSelectMenu : MonoBehaviour
{
    [System.Serializable]
	public class TileItem
	{
        public string name;
        public int cost;
        public GameObject tile;
	}

    public TileItem hillItem;

    public Button hillButton;

    public static TileSelectMenu instance;

    private TileReplacement _curPlacement;

    void Start()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    void Update()
    {

    }

    public void ShowMenu(TileReplacement replacement)
    {
        gameObject.SetActive(true);
        hillButton.GetComponentInChildren<Text>().text = hillItem.name + " $" + hillItem.cost;
    }

    public void BuyHillTile()
    {
        if (_curPlacement != null)
        {
            GolfCourse.instance.SetMoney(GolfCourse.instance.curMoney - hillItem.cost);
            _curPlacement.InstantiateTile(hillItem.tile);
        }
        this.gameObject.SetActive(false);
    }
}
