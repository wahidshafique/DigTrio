using UnityEngine;
using System.Collections;

/*
 * Use these namespaces for direct access to needed functions
 * without needing a reference to the component
 */

 // type of items available
 // "Items.Category.(Type)"
namespace Items
{
    public enum Category
    {
        GOLD, COPPER, IRON, MAX_CATEGORIES
    }
}

// used for access to UIManager script
// "UI.Finder.(Function)"
namespace UI
{
    public struct Finder
    {
        // Get the UIManager component
        public static UIManager GetUserInterface()
        {
            return GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
        }

        // Display item picked up in the ui
        public static void DisplayNewInventoryItem(GameObject item)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().DisplayNewItem(item);
        }

        // Pop the item pickups displayed
        public static void ClearInventoryDisplay()
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().PopDisplayItems();
        }

        // Pop requested amount of displayed items
        public static void PopInventoryDisplayItems(int count)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().PopDisplayItems(count);
        }
    }
}

// used for access to inventory manager script on the GameManager
// "Inventory.Finder.(Function)"
namespace Inventory
{
    public struct Finder
    {
        public static InventoryManager GetInventory()
        {
            return GameObject.FindGameObjectWithTag("GameManager").GetComponent<InventoryManager>();
        }
        
        // Static for easy external access
        public static void SellItems()
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<InventoryManager>().Sell();
        }

        // Static for easy external access
        // --returns a Pickup object that is "stolen"--
        // **Check if NULL while using**
        public static Pickup StealItem()
        {            
            return GameObject.FindGameObjectWithTag("GameManager").GetComponent<InventoryManager>().Steal();         
        }

        // static for easy external access
        public static void PushItem(Pickup item)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<InventoryManager>().Push(item);
        }

        // instantiate item GameObject based off of Pickup object
        // **returns NULL if pickup is invalid**
        public static GameObject InstantiateItem(Pickup pickup, Vector3 position)
        {
            return GameObject.FindGameObjectWithTag("GameManager").GetComponent<InventoryManager>().CreateItem(pickup, position);
        }
    }    
}