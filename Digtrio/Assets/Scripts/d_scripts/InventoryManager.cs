using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * This component on the GameManager object can be accessed by the Inventory.Finder
 */

public class InventoryManager : MonoBehaviour {
    Stack<Pickup> pickups;
    int gold = 0; // for now...

    [SerializeField, Tooltip("Delay between each item that is sold, for visual feedback.")] 
    float popDelay = 0.0f;
    
    int multiplier = 0;                         // used to determine points (same in a row = 2x2, 3x3, etc.)
    Items.Category prevType;                    // the previous item type sold

	// Use this for initialization
	void Start () {
	    pickups = new Stack<Pickup>(100);    
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.F))
        {
            Sell();            
        }
	}

    // sell the items to the vendor
    // can be accessed by Inventory.Finder as well
    public void Sell()
    {
        StartCoroutine(SellDelay());

        // clear the ui display of pickups
        UI.Finder.PopInventoryDisplay();
        multiplier = 0;       
    }

    // delay each item sold to the vendor
    IEnumerator SellDelay()
    {
        yield return new WaitForSeconds(popDelay);

        if (pickups.Count > 0)
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

            gold += multiplier*multiplier;

            // update the UI on what has changed
            UI.Finder.GetUserInterface().UpdateGold(gold);

            StartCoroutine(SellDelay());
        }            
    }

    // For now, item is simply popped off the stack
    // can be accessed by Inventory.Finder as well
    public void Steal()
    {
        if (pickups.Count > 0)
            pickups.Pop();
        else
            Debug.Log("Can not steal an item that does not exist.");
    }

    // Push a new item onto the stack
    // can be accessed by Inventory.Finder as well
    public void Push(Pickup item)
    {
        pickups.Push(item);
        Debug.Log(pickups.Peek().Name + " , Size: " + pickups.Count);
    }    
}
