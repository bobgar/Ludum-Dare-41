using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// I have no idea what I'm doing - Kleiner

public class TileReplacement : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject replacement;

	void Start()
    {

	}

    void Update()
    {

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //TileReplacementMenu.instance.ShowMenu(this);
        InstantiateTile();
    }

    public void InstantiateTile(TileSelectMenu.TileItem ti)
    {
        Debug.Log(name + " Game Object Clicked!");
        Tile t = GameObject.Instantiate<Tile>(ti.tile);
        t.transform.position = gameObject.transform.position;
        GameObject.Destroy(this.gameObject);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // When selected tile is clicked, course becomes yellow
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }
}
