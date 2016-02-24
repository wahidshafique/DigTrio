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
            render.sprite = Resources.Load<Sprite>("Sprites/gold_ore");        
            break;
        case Items.Category.IRON:
            render.sprite = Resources.Load<Sprite>("Sprites/iron_ore");
            break;
        case Items.Category.COPPER:
            render.sprite = Resources.Load<Sprite>("Sprites/copper_ore");
            break;
        }
    }

    public Items.Category GetCategory()
    {
        return pickup.Type;
    }
	
    // player picks up item...
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name == "Player")
        {
            Inventory.Finder.PushItem(pickup);
            Destroy(gameObject);
            
            // some sort of feedback effect?            
        }
    }

}
