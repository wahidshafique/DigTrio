using UnityEngine;
using System.Collections;

public class WanderEnemy : EnemyMovement {

    private Pickup enemyInventory;
    private Coroutine coroutine;
    public float fleeTime = 1.5f;
    public int maxItemSteal = 3;

	void Start ()
    {
        SetupVariables();
	}

	void Update ()
    {
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
        ForwardRayCast();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetWander(false);
            speed *= 2;
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine = StartCoroutine(ResetWander());
        }
    }

    private IEnumerator ResetWander()
    {
        yield return new WaitForSeconds(fleeTime);
        SetWander(true);
        speed *= 0.5f;
    }

    private void BumpIntoEnemy()
    {
        for(int i = 0; i < maxItemSteal; i++)
        {
            // pop
            // Instantiate
        }
    }
}
