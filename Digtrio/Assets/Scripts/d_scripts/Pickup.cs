using UnityEngine;
using System.Collections;
using Items; // namespace

public class Pickup {
    Category type;
    uint worth;
    
    // default constructor
    public Pickup()
    {
        type = SetRandomPickup();
        worth = (uint)type;
    }

    public Pickup(Category t)
    {
        type = t;
        worth = (uint)type;
    }    

    // used with default constructor
    Category SetRandomPickup()
    {
        int random = Random.Range(0, (int)Category.MAX_CATEGORIES);
        return (Category)random;
    }
    
    public Category GetItemCategory()
    {
        return type;
    }
    
    public string GetName()
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
        case Category.SILVER:
            name = "Silver";
            break;
        }

        return name;
    }

    public uint GetWorth()
    {
        return worth;
    }
}
