using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {
    Pickup pickup;

    void Awake()
    {
        Initialize(null);
    }

    public void Initialize(Pickup p)
    {
        if (p == null)
            pickup = new Pickup();
        else
        {
            pickup = p;
        }

        SetImage();
    }

    void SetImage()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();

        switch(pickup.Type)
        {
        case Items.Category.GOLD:
            render.sprite = Resources.Load<Sprite>("Sprites/Gold_Ingot");        
            break;
        case Items.Category.IRON:
            render.sprite = Resources.Load<Sprite>("Sprites/Iron_Ingot");
            break;
        case Items.Category.COPPER:
            render.sprite = Resources.Load<Sprite>("Sprites/Copper_Ingot");
            break;
        }
    }

    public Pickup GetPickup()
    {
        return pickup;
    }
	
    // player picks up item...
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name == "Player")
        {
            Inventory.Finder.PushItem(pickup);
            UI.Finder.DisplayNewInventoryItem(gameObject);
            Destroy(gameObject);
            
            // some sort of feedback effect?            
        }
    }

}
