using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    #region Variables

    public float speed = 1.0f;

    private int ydir = 0;
    private Vector3 lastPos;

    private bool facingLeft = true;
    private bool movingLeft = true;

    public bool canWander = true;

    public Transform target = null;

    #endregion Variables

    #region Monobehaviour

    void Update () {
        if (canWander)
        {
            Wander();
        }
        else
        {
            Seek();
        }
        FlipSprite();
	}

    #endregion Monobehaviour

    #region Public Functions

    public void SetSeek(Transform t)
    {
        target = t;
        SetWander(false);
    }

    public void SetWander(bool b)
    {
        canWander = b;
    }

    #endregion Public Functions

    #region Private Functions

    private void Wander()
    {
        MoveForward();
        MoveY(RandomBinomial());
        if (Random.Range(-1, 20) < 0)
        {
            speed *= -1;
        }
    }

    private void Seek()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, Mathf.Abs(speed) * Time.deltaTime);
    }

    private void FlipSprite()
    {
        if (transform.position.x - lastPos.x > 0)   // Moving Right
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
        else
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

    private void MoveForward()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void MoveY(int direction)
    {
        transform.Translate(Vector2.up * Mathf.Abs(speed) * 0.5f * direction * Time.deltaTime);
    }

    private int RandomBinomial()
    {
        return (Random.Range(0, 2) - Random.Range(0, 2));
    }

    #endregion Private Functions
}
