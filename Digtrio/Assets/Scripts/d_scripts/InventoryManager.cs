using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
    Stack<Pickup> items;

	// Use this for initialization
	void Start () {
	    items = new Stack<Pickup>(100);
        items.Push(new Pickup(Items.Category.GOLD));
        InventoryManager.StealItem();
        InventoryManager.StealItem();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Sell()
    {
        // items sell with a delay, with visual feedback
    }

    public void Steal()
    {
        if (items.Count > 0)
            items.Pop();
        else
            Debug.Log("Can not steal an item that does not exist.");
    }

    // Static for easy external access
    public static void SellItems()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<InventoryManager>().Sell();
    }

    // Static for easy external access
    public static void StealItem()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<InventoryManager>().Steal();
    }
}
