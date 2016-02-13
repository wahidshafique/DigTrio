using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
    Stack<Pickup> pickups;

	// Use this for initialization
	void Start () {
	    pickups = new Stack<Pickup>(100);        
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
        if (pickups.Count > 0)
            pickups.Pop();
        else
            Debug.Log("Can not steal an item that does not exist.");
    }

    public void Push(Pickup item)
    {
        pickups.Push(item);
        Debug.Log(pickups.Peek().GetName() + " , Size: " + pickups.Count);
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

    // static for easy external access
    public static void PushItem(Pickup item)
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<InventoryManager>().Push(item);
    }
}
