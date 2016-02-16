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
        public static void PopInventoryDisplay()
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>().PopDisplayItems();
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
        public static void StealItem()
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<InventoryManager>().Steal();
        }

        // static for easy external access
        public static void PushItem(Pickup item)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<InventoryManager>().Push(item);
        }
    }    
}