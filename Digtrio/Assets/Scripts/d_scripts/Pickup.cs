using UnityEngine;
using System.Collections;
using Items; // namespace

public class Pickup {
    Category type;
    int worth;
    
    // default constructor
    public Pickup()
    {
        type = SetRandomPickup();
        worth = (int)type;
    }

    public Pickup(Category t)
    {
        type = t;
        worth = (int)type;
    }
    
    public Pickup(Pickup pickup)
    {
        if (pickup == null)
            Debug.Log("NULL");
        type = pickup.Type;
        worth = pickup.Worth;
    }   

    // used with default constructor
    Category SetRandomPickup()
    {
        return (Category)Random.Range(0, (int)Category.MAX_CATEGORIES);
    }

    public Category Type
    {
        get
        {
            return type;
        }
    }

    public string Name
    {
        get
        {
            string name = null;

            switch(type)
            {
            case Category.GOLD:
                name = "Gold";
                break;
            case Category.IRON:
                name = "Iron";
                break;
            case Category.COPPER:
                name = "Copper";
                break;
            }

            return name;
        }
    }    

    public int Worth
    {
        get
        {
            return worth;
        }
    }

}
