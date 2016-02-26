using UnityEngine;
using System.Collections;

public class PartolEnemy : EnemyMovement
{

    #region Variables

    // Variables to store stolen items.
    private Pickup enemyItem;
    [Tooltip("Number of items the enemy will take from the player.")]
    public int maxStolenItems = 1;

    // For giving player items while testing
    private bool test = false;

    #endregion Variables

    #region Monobehaviour

    void Start () 
    {
        SetupVariables();
	}
	
	void Update ()
    {
        // For tests only, gives player items. Remember to remove this for final game.
        if (test)
        {
            for (int i = 0; i < 5; i++)
            {
                Inventory.Finder.PushItem(new Pickup());
            }
            test = false;
        }

        Patrol();
        FlipSprite();
        ForwardRayCast();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // Bumped into the player, make player drop some of it's inventory.
            // Steal an item then thow them out right away into the immediate vacinity.
            for(int i = 0; i < maxStolenItems; i++)
            {
                enemyItem = inventory.Steal();//Inventory.Finder.StealItem();
                inventory.CreateItem(enemyItem, DropRange(other.gameObject.transform)); //Inventory.Finder.InstantiateItem(enemyItem, DropRange(other.gameObject.transform));
            }
        }
    }

    #endregion Monobehaviour

}
