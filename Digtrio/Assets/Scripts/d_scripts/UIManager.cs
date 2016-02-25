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

    [Tooltip("Maximum amount of picked up items that are displayed on the UI.")]    
    public int maxDisplayedItems = 5; // displayed inventory items on stack

    StackList<Pickup> pickupsRef; // reference to the stack of inventory items
    GameObject[] displayedInventoryItems; // the current display GameObjects
    GameObject pnlInventory;    
    
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
        displayedInventoryItems = new GameObject[maxDisplayedItems];
         
        pnlInventory = Instantiate(Resources.Load<GameObject>("Prefabs/UI/PanelInventory"));        
        pnlInventory.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
    }

    void Start()
    {
        pickupsRef = Inventory.Finder.GetInventory().GetStack();
    }

    // Update the gold text on the UI
    public void UpdateCash(int n)
    {
        txtGold.text = n.ToString();
    }
    
    #region Item Display Functions

    // display picked up items on UI
    // can be accessed by UI.Finder as well
    public void UpdateItemDisplay()
    {
        RectTransform rtInventory = pnlInventory.GetComponent<RectTransform>();

        int displayedItemCount = 0; // number of items displayed so far

        int startCountingFrom = 0; // used for showing only maxDisplayedItems
        
        // as long as there are more items than the screen can show
        // then start counting the appropriate element
        if (pickupsRef.Count > maxDisplayedItems)
            startCountingFrom = pickupsRef.Count - maxDisplayedItems;

        float itemSpacing = 0.0f; // how much space between the first item and the current

        // Clear the old display of items
        foreach (GameObject r in displayedInventoryItems)
        {
            if (r != null)
                Destroy(r);
        }

        // Update the Item Display
        for (int i = startCountingFrom; i < pickupsRef.Count; i++)
        {
            // instantiate new UI image gameobject and attach
            // the appropriate components
            GameObject displayItem = new GameObject();
            displayItem.name = "Pickup";
            displayItem.transform.SetParent(pnlInventory.transform, false);
            displayItem.AddComponent<RectTransform>();
            displayItem.AddComponent<CanvasRenderer>();
        
            Image image = displayItem.AddComponent<Image>();        
            
            // Choose the right image to display
            switch (pickupsRef.GetItem(i).Type)
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
            
            // position, size, and space the new item appropriately on the display
            RectTransform rtDisplayItem = displayItem.GetComponent<RectTransform>();
            rtDisplayItem.sizeDelta = new Vector3(displayItemSize, displayItemSize);
            rtDisplayItem.position = new Vector3(rtDisplayItem.position.x, 
                                                 rtDisplayItem.position.y+
                                                 (rtInventory.rect.height/32 - 
                                                 displayItemSize + itemSpacing));            
            

            // keep track of created item
            displayedInventoryItems[displayedItemCount] = displayItem;

            // next item will be that much further from first item
            itemSpacing += displayItemSpacing;

            // how many items have been created so far, for indexing in displayedInventoryItems
            displayedItemCount++;
        }
    }

    /*
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
    */
    
    // Destroy all the displayed items
    // -- function can be accessed by UI.Finder --
    public void PopDisplayItems()
    {
        foreach (GameObject r in displayedInventoryItems)
        {
            if (r != null)
                Destroy(r);          
        }
    }

    /*
    // **overload** for popping the requested amount of items
    // -- function can be accessed by UI.Finder --
    public void PopDisplayItems(int count)
    {
        if (count > displayedInventoryItems.Count)
            count = displayedInventoryItems.Count;
        
        Pickup[] copy = new Pickup[Inventory.Finder.GetInventory().StackCount()];
        Inventory.Finder.GetInventory().CopyStackTo(copy);
        
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

        int difference = maxDisplayedItems - displayedInventoryItems.Count;
        if (difference > copy.Length)
        {
            difference = copy.Length;
        }
        for (int i = 0; i < difference; i++)
        {
            DisplayNewItem(copy[copy.Length - 1 - i]);
        }

        displayedInventoryItems.Reverse();
    }*/

    #endregion    	
}
