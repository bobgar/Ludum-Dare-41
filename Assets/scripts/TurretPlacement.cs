using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretPlacement : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject aimTarget;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        Debug.Log(name + " Game Object Clicked!");
        TowerLibrary.TowerItem ti = TowerLibrary.instance.items[0];
        Tower t = GameObject.Instantiate<Tower>(ti.tower);
        t.aimTarget = aimTarget;
        t.transform.position = gameObject.transform.position;
        GameObject.Destroy(this.gameObject);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Debug.Log(name + " Enter");
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Debug.Log(name + " Exit");
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
