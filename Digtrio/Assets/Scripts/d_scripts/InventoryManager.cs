﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * This component on the GameManager object can be accessed by the Inventory.Finder
 */

public class InventoryManager : MonoBehaviour {
    StackList<Pickup> pickups;
    int cash;
    [SerializeField, Tooltip("Delay between each item that is sold, for visual feedback.")] 
    float popDelay = 0.0f;
    
    int multiplier = 0;                         // used to determine points (same in a row = 2x2, 3x3, etc.)
    Items.Category prevType;                    // the previous item type sold    

	// Use this for initialization
	void Awake () {
	    pickups = new StackList<Pickup>(50);    
	}

    // sell the items to the vendor
    // can be accessed by Inventory.Finder as well
    

    // delay each item sold to the vendor
    public void SellItems()
    {
        if (pickups.Count > 0)
        {
            for (int i = 0; i <= pickups.Count; i++)
            {
                // check for potential point multiplier
                if (pickups.Peek().Type == prevType)
                {
                    multiplier++;
                }
                else
                {
                    multiplier = 1;
                }

                // pop the top of the stack
                prevType = pickups.Peek().Type;
                pickups.Pop();

                cash += multiplier * multiplier;
            }
            
            UIManager ui = UI.Finder.GetUserInterface();
            ui.UpdateCash(cash);
            ui.UpdateItemDisplay();
        }            
    }

    // item is popped and returned by the stack
    // --function can be accessed by Inventory.Finder as well--
    public Pickup Steal()
    {
        if (pickups.Count > 0)
        {            
            Items.Category pickupType = pickups.Peek().Type;

            pickups.Pop();
			if(pickups.Count > 0)
				cash -= pickups.Peek ().Worth;

            UI.Finder.UpdateItemDisplay();

            return new Pickup(pickupType);
        }
        else
            Debug.Log("Can not steal an item that does not exist.");

        return null;
    }
    
    public int StackCount()
    {
        return pickups.Count;
    }    
    
    public void CopyStackTo(Pickup[] copy)
    {
        pickups.CopyTo(copy);
    }

    public StackList<Pickup> GetStack()
    {
        return pickups;
    }

    // Push a new item onto the stack
    // --function can be accessed by Inventory.Finder as well--
    public void Push(Pickup item)
    {
        pickups.Push(item);
        UI.Finder.UpdateItemDisplay();
        Debug.Log(pickups.Peek().Name + " , Size: " + pickups.Count);
    }
    
    // Instantiate an item based off of a Pickup object
    // --function can be accessed by Inventory.Finder as well--
    // returns NULL if pickup is invalid
    public GameObject CreateItem(Pickup pickup, Vector3 position)
    {
        if (pickup != null)
        {
            GameObject item = Instantiate(Resources.Load("Prefabs/Game/Item"), position, Quaternion.identity) as GameObject;
            ItemScript script = item.GetComponent<ItemScript>();        
            script.Initialize(pickup);

            return item;
        }

        return null;
    }   
}
