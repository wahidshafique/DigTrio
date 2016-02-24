using UnityEngine;
using System.Collections;

public class ThiefEnemy : EnemyMovement
{

    #region Variables

    // Variables for stealing items
    private Pickup[] enemyItems;
    private Coroutine resetWanderCoroutine;
    [Tooltip("Amount of time the enemy will flee the player.")]
    public float fleeTime = 1.5f;
    [Tooltip("Number of items the enemy will take from the player.")]
    public int maxStolenItems = 7;
    private bool hasStolen = false;

    // For giving player items while testing
    private bool test = false;

    #endregion Variables

    #region Monobehaviour

    void Start()
    {
        SetupVariables();
        enemyItems = new Pickup[maxStolenItems];
    }

    void Update()
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BumpIntoEnemy(other.gameObject.transform);
            SetWander(false);
            speed *= 2;
            if (resetWanderCoroutine != null)
            {
                StopCoroutine(resetWanderCoroutine);
            }
            resetWanderCoroutine = StartCoroutine(ResetWander());
        }

        if (other.CompareTag("Wall"))
        {
            atWall = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Wall"))
        {
            atWall = false;
        }
    }

    #endregion Monobehaviour

    #region Private Functions

    // Waits for a bit while the enemy flees, then starts the enemy to wander again.  When it starts wandering again it throws away all its items.
    private IEnumerator ResetWander()
    {
        yield return new WaitForSeconds(fleeTime);
        SetWander(true);
        speed *= 0.5f;
        ThrowAwayItems();
    }

    // Called when the enemy bumps into the player.  It steals items before running away with them.
    private void BumpIntoEnemy(Transform player)
    {
        if (!hasStolen)
        {
            int r = Random.Range(3, maxStolenItems);
            for (int i = 0; i < r; i++)
            {
                enemyItems[i] = Inventory.Finder.StealItem();
            }
            hasStolen = true;
        }
    }

    // Throws away all it's items then allows the enemy to be able to steal again.
    private void ThrowAwayItems()
    {
        for (int i = 0; i < enemyItems.Length; i++)
        {
            if (enemyItems[i] != null)
            {
                Inventory.Finder.InstantiateItem(enemyItems[i], DropRange(this.transform));
            }
        }
        hasStolen = false;
    }

    #endregion Private Functions

}
