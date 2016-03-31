using UnityEngine;
using System.Collections;

public class PlayerSell : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("NPC"))
        {
            other.GetComponent<Speech_Canvas>().ShowBubble(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            other.GetComponent<Speech_Canvas>().ShowBubble(false);
        }
    }

}
