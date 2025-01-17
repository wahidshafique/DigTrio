﻿using UnityEngine;
using System.Collections;
using Inventory;

public class EnemyMovement : MonoBehaviour
{
    #region Variables

    // Speed variables
    [Tooltip("Speed at which the enemy will travel.")]
    public float speed = 1.0f;
    [Tooltip("Multiplies the enemies speed, this is the speed at which the enemy will travel vertically.")]
    public float vertSpeedMultipliyer = 1.0f;

    // Movement variables
    [Tooltip("Minimum amount of time the enemy will travel in a certain direction before it has the possibility to change directions.")]
    public float minWaitForFlip = 1.0f;
    private bool canFlip = true;
    private bool canChangeVert = true;
    private Coroutine flipCoroutine, vertCoroutine;     // Saves coroutines to be able to stop them.
    private int prevDir;
    protected bool canWander = true;
    //[Tooltip("Screen limit markers.  Enemy cannot pass these points.")]
    //public Transform xLeftLimit, xRightLimit, yBottomLimit, yTopLimit;
    private float xMin, xMax, yMin, yMax;

    // Sprite facing variables
    private Vector3 lastPos;
    private bool facingLeft = true;
    private bool movingLeft = true;
    protected bool atWall = false;

    // Enemy Detection
    [Tooltip("How close the player needs to be before it is detected by the enemy.  Also used for Raycast distance.")]
    public float detectRadius;
    [Tooltip("Transform of the target used in the seek function.  Can be player or collectable, etc.")]
    public Transform target = null;
    private bool targetDetected = false;
    private int layerMask = 1 << 8;

    // Stealing Items
    public float dropRadius = 1.5f;
    protected InventoryManager inventory;

    #endregion Variables

    #region Monobehaviour
    // NOTE: These monobehaviour calls are just for testing, this class will be extended
    // and the functions called need to be called in the child script.
    /*void Start()
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
            //Wander();
        }
        else
        {
            //Seek();
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
        //Debug.Log("Hit Player");
        if(other.CompareTag("Player"))
        {
            SetWander(false);
        }
    }*/

    #endregion Monobehaviour

    #region Public Functions

    /// <summary>
    /// Set the target to which you want the enemy to seek.
    /// </summary>
    /// <param name="t">Target's Transform</param>
    public void SetSeek(Transform t)
    {
        target = t;
        SetWander(false);
    }

    /// <summary>
    /// Set the enemy to Wander.
    /// </summary>
    /// <param name="b">True/False</param>
    public void SetWander(bool b)
    {
        canWander = b;
    }

    #endregion Public Functions

    #region Protected Functions

    protected void SetupVariables()     // Call this in the Start() function of child classes to setup boundries.
    {
        yMin = WorldBounds.Get.bottom;
        yMax = WorldBounds.Get.top;
        xMin = WorldBounds.Get.left;
        xMax = WorldBounds.Get.right;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        inventory = Inventory.Finder.GetInventory();
        //layerMask = ~layerMask;
    }

    protected void Wander()             // Enemy will wander.
    {
        MoveForward();
        SmoothMoveY(RandomBinomial());
        HorizontalFlip();
    }

    protected void Patrol()             // Enemy will partol only in the horizontal direction.
    {
        MoveForward();
        MoveY(RandomBinomial());
        HorizontalFlip();
    }

    protected void Seek()               // Enemy will seek (at 1.5x speed) a target set in the SetTarget() function.
    {
        if (transform.position.y <= yMax && transform.position.y >= yMin && transform.position.x >= xMin && transform.position.x <= xMax)
        {
            speed *= 2;
            transform.position = Vector2.MoveTowards(transform.position, target.position, Mathf.Abs(speed * 1.5f) * Time.deltaTime);
            speed *= 0.5f;
            Rotation();
        }
    }

    protected void Flee()               // Enemy will flee (at 2x speed) a target set in the SetTarget() function.
    {
        if (transform.position.y <= yMax && transform.position.y >= yMin && transform.position.x >= xMin && transform.position.x <= xMax)
        {
            speed *= 2;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-target.position.x * 10, target.position.y - 1000), Mathf.Abs(speed * 2) * Time.deltaTime);
            speed *= 0.5f;
            Rotation();
        }
    }

    protected void MoveForward()        // Enemy will move horizontaly.
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        Rotation();
    }

    protected void MoveY(int direction) // Enemy will wiggle in the vertical direction, possiblility of moving patrol up or down slightly.
    {
        if ((direction > 0 && transform.position.y < yMax) || (direction < 0 && transform.position.y > yMin))
        {
            transform.Translate(Vector2.up * Mathf.Abs(speed) * vertSpeedMultipliyer * direction * Time.deltaTime);
        }
        else if (direction > 0 && transform.position.y >= yMax)
        {
            MoveY(-1);
        }
        else if (direction < 0 && transform.position.y <= yMin)
        {
            MoveY(1);
        }
    }

    protected void SmoothMoveY(int direction)   // Enemy will move up or down in its wander.
    {
        if ((direction > 0 && transform.position.y < yMax) || (direction < 0 && transform.position.y > yMin))
        {
            if (canChangeVert)
            {
                transform.Translate(Vector2.up * Mathf.Abs(speed) * vertSpeedMultipliyer * direction * Time.deltaTime);
                prevDir = direction;
                vertCoroutine = StartCoroutine(CountToChangeVert());
            }
            else
            {
                transform.Translate(Vector2.up * Mathf.Abs(speed) * vertSpeedMultipliyer * prevDir * Time.deltaTime);
            }
        }
        else if (direction > 0 && transform.position.y >= yMax)
        {
            if (flipCoroutine != null)
            {
                StopCoroutine(vertCoroutine);
            }
            canChangeVert = true;
            MoveY(-1);
        }
        else if (direction < 0 && transform.position.y <= yMin)
        {
            if (flipCoroutine != null)
            {
                StopCoroutine(vertCoroutine);
            }
            canChangeVert = true;
            MoveY(1);
        }
    }

    protected void FlipSprite()         // Enemy sprite will face in the direction its moving.  Needs to be called on Update().
    {
        if (transform.position.x - lastPos.x > 0f && !atWall)   // Moving Right
        {
            if (movingLeft && facingLeft)
            {
                Vector2 scale = transform.localScale;
                scale.x *= -1.0f;
                transform.localScale = scale;
                facingLeft = false;
                movingLeft = false;
            }
        }                                           // Moving Left
        else if (transform.position.x - lastPos.x < 0f && !atWall)
        {
            if (!movingLeft && !facingLeft)
            {
                Vector2 scale = transform.localScale;
                scale.x *= -1.0f;
                transform.localScale = scale;
                facingLeft = true;
                movingLeft = true;
            }
        }
        lastPos = transform.position;
    }

    protected void ForwardRayCast()           // Raycasts a forward ray to detect is Player is in front of the enemy. Triples the enemy's speed if Player is detected. 
    {
        if (!targetDetected)
        {
            Debug.DrawRay(transform.position + Vector3.up * 0.25f, Vector2.left * (speed / Mathf.Abs(speed)) * detectRadius, Color.red);
            Debug.DrawRay(transform.position + Vector3.down * 0.25f, Vector2.left * (speed / Mathf.Abs(speed)) * detectRadius, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * 0.25f, Vector2.left * (speed / Mathf.Abs(speed)), detectRadius, layerMask);
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position + Vector3.down * 0.25f, Vector2.left * (speed / Mathf.Abs(speed)), detectRadius, layerMask);
            if ((hit.collider != null && hit.collider.gameObject.CompareTag("Player")) ||
                (hit2.collider != null && hit2.collider.gameObject.CompareTag("Player")))      // Detected player
            {
                if (flipCoroutine != null)
                {
                    StopCoroutine(flipCoroutine);
                }
                speed *= 3;
                StartCoroutine(ChargeTimer());
            }
        }
    }

    protected Vector3 DropRange(Transform t)   // Returns a vector around the passed in transform, based off of the dropRadius.
    {
        Vector3 range = t.localPosition;
        range.x += Random.Range(-dropRadius, dropRadius);
        range.y += Random.Range(-dropRadius, dropRadius);
        return range;
    }

    protected IEnumerator NoLongerAtWall()
    {
        yield return new WaitForSeconds(0.3f);
        atWall = false;
    }

    #endregion Protected Functions

    #region Private Functions

    private int RandomBinomial()        // Returns one of the following, -1, 0, or 1. 0 is more likely.
    {
        return (Random.Range(0, 2) - Random.Range(0, 2));
    }

    private void HorizontalFlip()       // Changes the horizontal direction randomly, or if a boundry is hit.
    {
        if (transform.position.x > xMin && transform.position.x <= xMax)
        {
            if (canFlip)
            {
                if (Random.Range(-2, 9) < 0)
                {
                    speed *= -1;
                }
                flipCoroutine = StartCoroutine(CountToFlip());
            }
        }
        else
        {
            speed *= -1;
            if (flipCoroutine != null)
            {
                StopCoroutine(flipCoroutine);
            }
            flipCoroutine = StartCoroutine(CountToFlip());
        }
    }

    private IEnumerator CountToFlip()       // Waits for a time before allowing a change in horizontal direction, called after a change in direction.
    {
        canFlip = false;
        yield return new WaitForSeconds(minWaitForFlip);
        canFlip = true;
    }

    private IEnumerator CountToChangeVert() // Waits for a time before allowing a change in vertical direction, called after a change in direction.
    {
        canChangeVert = false;
        yield return new WaitForSeconds(minWaitForFlip);
        canChangeVert = true;
    }

    private IEnumerator ChargeTimer()   // Delays the detection of the Player, reduces enemy speed back to normal. Called after Player is detected.
    {
        targetDetected = true;
        yield return new WaitForSeconds(1.25f);
        speed /= 3;
        flipCoroutine = StartCoroutine(CountToFlip());
        targetDetected = false;
    }

    public float rotationSpeed = 0.0f;
    private void Rotation()
    {
        /*t = Mathf.PingPong(Time.deltaTime * speed, 1.0f);
        transform.rotation = Quaternion.Lerp(a.rotation, b.rotation, t);*/
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, -25.0f + Mathf.PingPong(Time.time * rotationSpeed * speed, 50.0f));
    }

    #endregion Private Functions
}
