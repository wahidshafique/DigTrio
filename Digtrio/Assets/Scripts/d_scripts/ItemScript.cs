using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {
    Pickup pickup;

    void Start()
    {
        pickup = new Pickup();
    }
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name == "Player")
        {
            InventoryManager.PushItem(pickup);
            Destroy(gameObject);            
        }
    }
}
