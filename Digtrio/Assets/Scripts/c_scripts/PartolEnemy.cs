using UnityEngine;
using System.Collections;

public class PartolEnemy : EnemyMovement{

    private Pickup[] enemyInventory;        // Not 100% sure if this is how it will be...

	void Start () 
    {
        SetupVariables();
        enemyInventory = new Pickup[2];
	}
	
	void Update ()
    {
        Patrol();
        FlipSprite();
	}

    void FixedUpdate()
    {
        ForwardRayCast();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // Bumped into the player, make player drop some of it's inventory.
            // Steal a couple of items then thow them out right away into the immediate vacinity.
            for(int i = 0; i < enemyInventory.Length; i++)
            {
                //enemyInventory[i] = Inventory.Finder.StealItem(); // Example of how it should be
                //enemyInventory[i].ThrowAway();                    //
            }
        }
    }
}
