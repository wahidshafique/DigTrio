using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/*
 * This component on the GameManager object can be accessed by the UI.Finder
 */

public class UIManager : MonoBehaviour {
   Text txtGold;

    #region Item Display Variables

    [SerializeField, Tooltip("Maximum amount of picked up items that are displayed on the UI.")] 
    int maxDisplayedItems; // displayed inventory items on stack

    GameObject pnlInventory;
    List<GameObject> displayedInventoryItems;
    
    [SerializeField, Tooltip("Size of the pickup items displayed on the UI.")] 
    float displayItemSize = 20.0f;
    [SerializeField, Tooltip("Spacing between each pickup item displayed on the UI.")] 
    float displayItemSpacing = 4.0f;

    #endregion

    void Awake()
    {
        // for now...       
        txtGold = Instantiate(Resources.Load<GameObject>("Prefabs/UI/GoldText")).GetComponent<Text>();
        txtGold.gameObject.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
        
        // Setup for item display
        displayedInventoryItems = new List<GameObject>();
         
        pnlInventory = Instantiate(Resources.Load<GameObject>("Prefabs/UI/PanelInventory"));        
        pnlInventory.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
    }

    // Update the gold text on the UI
    public void UpdateGold(int n)
    {
        txtGold.text = n.ToString();
    }
    
    #region Item Display Functions

    // display picked up items on UI, and store in list
    // can be accessed by UI.Finder as well
    public void DisplayNewItem(GameObject item)
    {
        RectTransform rtInventory = pnlInventory.GetComponent<RectTransform>();
        
        // instantiate new UI image gameobject and position
        GameObject displayItem = new GameObject();
        displayItem.name = "Pickup";
        displayItem.transform.SetParent(pnlInventory.transform, false);
        displayItem.AddComponent<RectTransform>();
        displayItem.AddComponent<CanvasRenderer>();
        
        Image image = displayItem.AddComponent<Image>();        
        image.sprite = item.GetComponent<SpriteRenderer>().sprite;
        
        RectTransform rtDisplayItem = displayItem.GetComponent<RectTransform>();
        rtDisplayItem.sizeDelta = new Vector3(displayItemSize, displayItemSize);
        rtDisplayItem.position = new Vector3(rtDisplayItem.position.x, rtDisplayItem.position.y+(rtInventory.rect.height/4));

        // move items down when new item is pushed
        if (displayedInventoryItems.Count > 0)
        {
            foreach (GameObject r in displayedInventoryItems)
            {
                GameObject current = r;
                current.transform.position -= new Vector3(0, displayItemSize/displayItemSpacing);
            }
        }                

        // pop any excess items
        if (displayedInventoryItems.Count >= maxDisplayedItems)
        {
            Destroy(displayedInventoryItems[0]);
            displayedInventoryItems.RemoveAt(0);
        }
        
        displayedInventoryItems.Add(displayItem);
    }

    // display picked up items on UI, and store in list
    // can be accessed by UI.Finder as well
    public void DisplayNewItem(Pickup item)
    {
        RectTransform rtInventory = pnlInventory.GetComponent<RectTransform>();
        
        // instantiate new UI image gameobject and position
        GameObject displayItem = new GameObject();
        displayItem.name = "Pickup";
        displayItem.transform.SetParent(pnlInventory.transform, false);
        displayItem.AddComponent<RectTransform>();
        displayItem.AddComponent<CanvasRenderer>();
        
        Image image = displayItem.AddComponent<Image>();
        switch(item.Type)
        {
        case Items.Category.GOLD:
            image.sprite = Resources.Load<Sprite>("Sprites/Gold_Ingot");        
            break;
        case Items.Category.IRON:
            image.sprite = Resources.Load<Sprite>("Sprites/Iron_Ingot");
            break;
        case Items.Category.COPPER:
            image.sprite = Resources.Load<Sprite>("Sprites/Copper_Ingot");
            break;
        }
        
        RectTransform rtDisplayItem = displayItem.GetComponent<RectTransform>();
        rtDisplayItem.sizeDelta = new Vector3(displayItemSize, displayItemSize);
        rtDisplayItem.position = new Vector3(rtDisplayItem.position.x, rtDisplayItem.position.y+(rtInventory.rect.height/4));

        // move items down when new item is pushed
        if (displayedInventoryItems.Count > 0)
        {
            foreach (GameObject r in displayedInventoryItems)
            {
                GameObject current = r;
                current.transform.position -= new Vector3(0, displayItemSize/displayItemSpacing);
            }
        }                

        // pop any excess items
        if (displayedInventoryItems.Count >= maxDisplayedItems)
        {
            Destroy(displayedInventoryItems[0]);
            displayedInventoryItems.RemoveAt(0);
        }
        
        displayedInventoryItems.Add(displayItem);
    }
    
    // generally called when items are going to be sold
    // -- function can be accessed by UI.Finder --
    public void PopDisplayItems()
    {
        foreach (GameObject r in displayedInventoryItems)
        {
            GameObject current = r;
            Destroy(current);            
        }

        displayedInventoryItems.RemoveRange(0, displayedInventoryItems.Count);
    }

    // **overload** for popping the requested amount of items
    // -- function can be accessed by UI.Finder --
    public void PopDisplayItems(int count)
    {
        if (count > displayedInventoryItems.Count)
            count = displayedInventoryItems.Count;
        
        //Pickup[] copy = Inventory.Finder.GetInventory().GetStackCopy();
        
        displayedInventoryItems.Reverse();

        for (int i = 0; i < count; i++)
        {
            // move items back up to compensate for when
            // a new item is pushed
            if (displayedInventoryItems.Count > 0)
            {
                foreach (GameObject r in displayedInventoryItems)
                {
                    GameObject current = r;
                    current.transform.position += new Vector3(0, displayItemSize/displayItemSpacing);
                }
            }    

            Destroy(displayedInventoryItems[i]);
            displayedInventoryItems.RemoveAt(i);
        }

        displayedInventoryItems.Reverse();
    }

    #endregion    	
}
