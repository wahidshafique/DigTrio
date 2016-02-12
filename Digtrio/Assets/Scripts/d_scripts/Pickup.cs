using UnityEngine;
using System.Collections;
using Items; // namespace

public class Pickup {
    Category type;
    uint worth;
    
    public Pickup(Category t)
    {
        type = t;
        worth = (uint)type;
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
