using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * This component on the GameManager object can be accessed by the Inventory.Finder
 */
public class InventoryManager : MonoBehaviour {
    StackList<Pickup> pickups;                  // the inventory
    Dictionary<Items.Category, int> itemCount; // keeps track of how many of each item   
    
    int multiplier = 0;                         // used to determine points (same in a row = 2x2, 3x3, etc.)
    Items.Category prevType;                    // the previous item type sold    
    public static int cash;                                   // the amount of cash the player has made
    int iter;

	// Use this for initialization
	void Awake () {
	    pickups = new StackList<Pickup>(50);
        itemCount = new Dictionary<Items.Category, int>();              
	}

    void Start()
    {
        //initiaze the leaderboard values
        cash = 0;

        // initialize values of itemCount
        for (int i = 0; i < (int)Items.Category.MAX_CATEGORIES; i++)
        {
            itemCount.Add((Items.Category)i, 0);
        }  
    }

    // sell the items to the vendor
    // can be accessed by Inventory.Finder as well
    public void SellItems()
    {
        if (pickups.Count > 0)
        {
            for (int i = pickups.Count - 1; i >= 0; i--)
            {
                Pickup pickup = pickups.GetItem(i);

                // check for potential point multiplier
                if (pickup.Type == prevType)
                {
                    multiplier++;
                }
                else
                {
                    multiplier = 1;
                }

                // pop the top of the stack
                prevType = pickup.Type;

                // add to the cash
                cash += (multiplier * multiplier) * pickups.GetItem(i).Worth;                           
            }
            
            // clear the stack
            pickups.Clear();  

            // update the UI
            UIManager ui = UI.Finder.GetUserInterface();
            ui.UpdateCash(cash);
			print (cash);
            ui.UpdateItemDisplay();
        }
        else
        {
            Debug.Log("There is nothing to sell.");
        }            
    }

    public Dictionary<Items.Category, int> GetItemCount()
    {
        return itemCount;
    }

    void AddToItemCount(Pickup item)
    {
        if (itemCount.ContainsKey(item.Type))
            itemCount[item.Type]++;   
    }

    void SubtractToItemCount(Pickup item)
    {
        if (itemCount.ContainsKey(item.Type))
            itemCount[item.Type]--; 
    }

    // item is popped and returned by the stack
    // --function can be accessed by Inventory.Finder as well--
    public Pickup Steal()
    {
        if (pickups.Count > 0)
        {            
            // get item from top of list
            Pickup pickup = pickups.Peek();

            Items.Category pickupType = pickup.Type;
            
            // Subtract item from pickup count
            SubtractToItemCount(pickup);

            pickups.Pop();

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
        
        // add item to pickup itemCount
        AddToItemCount(item);

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
