using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestingPlayer : MonoBehaviour {
    [SerializeField] Transform child;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    Pickup pickup;

        if (Input.GetKeyDown(KeyCode.F))
        {
            Inventory.Finder.SellItems();           
        }
        if (Input.GetKeyDown(KeyCode.I))
        {            
            pickup = Inventory.Finder.StealItem();
            Inventory.Finder.InstantiateItem(pickup, child.position);
        }
	}
}
