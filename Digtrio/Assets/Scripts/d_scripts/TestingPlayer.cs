using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestingPlayer : MonoBehaviour {
    
	// Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.S))
        {
            Inventory.Finder.SellItems();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Inventory.Finder.StealItem();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Game.Finder.ToScore();
        }
	}

    void Stun()
    {
        Debug.Log("Stunned.");
    }
}
