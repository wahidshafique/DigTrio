using UnityEngine;
using System.Collections;

public class WanderEnemy : EnemyMovement
{

    #region Variables

    // Variables to store stolen items
    private Pickup enemyItem;
    private Coroutine resetWanderCoroutine;
    [Tooltip("Amount of time the enemy will flee the player.")]
    public float fleeTime = 1.5f;
    [Tooltip("Number of items the enemy will take from the player.")]
    public int maxStolenItems = 3;

    // For giving player items while testing
    private bool test = false;

    #endregion Variables

    void Start ()
    {
        SetupVariables();
	}

    #region Monobehaviour

    void Update ()
    {
        // For tests only, gives player items. Remember to remove this for final game.
        if(test)
        {
            for (int i = 0; i < 5; i++)
            {
                Inventory.Finder.PushItem(new Pickup());
            }
            test = false;
        }

        if (canWander)
        {
            if ((transform.position - target.position).magnitude < detectRadius)
            {
                Seek();
            }
            else
            {
                Wander();
            }
        }
        else
        {
            Flee();
        }
        FlipSprite();
	}

    void FixedUpdate()
    {
        if ((transform.position - target.position).magnitude > detectRadius)
        {
            ForwardRayCast();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // If the enemy bumps into the player, steal a bunch of items.
            BumpIntoEnemy(other.gameObject.transform);
            SetWander(false);
            speed *= 2; // increasing speed gives the effect of running away.
            if (resetWanderCoroutine != null)
            {
                StopCoroutine(resetWanderCoroutine);
            }
            resetWanderCoroutine = StartCoroutine(ResetWander());
        }
    }

    #endregion Monobehaviour

    #region Private Functions

    // Waits for a bit while the enemy flees, then starts the enemy to wander again.
    private IEnumerator ResetWander()
    {
        yield return new WaitForSeconds(fleeTime);
        SetWander(true);
        speed *= 0.5f;
    }

    // Called when the enemy bumps into the player.  It steals items then throws them away, gives the effect of dropping a bunch of items.
    private void BumpIntoEnemy(Transform player)
    {
        int r = Random.Range(2, maxStolenItems);
        for(int i = 0; i < r; i++)
        {
            enemyItem = inventory.Steal();//Inventory.Finder.StealItem();
            inventory.CreateItem(enemyItem, DropRange(player));//Inventory.Finder.InstantiateItem(enemyItem, DropRange(player));
        }
    }

    #endregion Private Functions

}
