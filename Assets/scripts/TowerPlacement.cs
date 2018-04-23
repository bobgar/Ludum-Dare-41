using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlacement : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject aimTarget;
    
    void Start () {
		
	}
	
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        TowerSelectMenu.instance.ShowMenu(this);
    }

    public void InstantiateTower(TowerSelectMenu.TowerItem ti)
    {
        Debug.Log(name + " Game Object Clicked!");
        //TowerLibrary.TowerItem ti = TowerLibrary.instance.items[0];
        Tower t = GameObject.Instantiate<Tower>(ti.tower);
        t.aimTarget = aimTarget;
        t.transform.position = gameObject.transform.position;
        GameObject.Destroy(this.gameObject);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Debug.Log(name + " Enter");
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Debug.Log(name + " Exit");
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
