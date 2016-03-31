using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Speech_Canvas : MonoBehaviour {

    [Tooltip("GameObject that contains the canvas with the speech bubble.")]
    public GameObject SpeechBubbleObject;

    // Text that will be displayed.
    private string SpeechText;
    [Tooltip("Text object to display the speech bubble's speech.")]
    public Text SpeechBubble;

    // Can the speech bubble be shown?
    private bool CanShowBubble = false;

    void Update()
    {
        ShowSpeechBubble();
    }

    /// <summary>
    /// Display or hide the speech bubble.
    /// </summary>
    private void ShowSpeechBubble()
    {
        if (CanShowBubble)
        {
            SpeechBubbleObject.SetActive(true);
            SpeechBubble.text = SpeechText;
        }
        else
        {
            SpeechBubbleObject.SetActive(false);
        }
    }

    /// <summary>
    /// Activate/deactivate the speech bubble.
    /// </summary>
    /// <param name="b">Will the bubble be shown? T/F</param>
    public void ShowBubble(bool b)
    {
        CanShowBubble = b;
        SpeechText = RandomSpeech();
    }

    /// <summary>
    /// Call the sell items function to sell the player's items.
    /// </summary>
    public void SellItemsButton()
    {
        Debug.Log("Sold Items");
        Inventory.Finder.SellItems();

        // player prefs?

        //Application.LoadLevel("Leaderboards");
    }

    /// <summary>
    /// Get a random phrase to be displayed.
    /// </summary>
    /// <returns>random speech</returns>
    private string RandomSpeech()
    {
        int rand = Random.Range(0, 7);
        string randomSpeech = "";

        switch (rand)
        {
            case 0:
                randomSpeech = "\n Give me your goods.";
                break;
            case 1:
                randomSpeech = "Time is of the essence, I'm too mole for this...";
                break;
            case 2:
                randomSpeech = "I think I'm forgetting something. Can you dig it?";
                break;
            case 3:
                randomSpeech = "If you don't bring me what you find, you won't get any points!";
                break;
            case 4:
                randomSpeech = "Get a point multiplier by chaining the same items in a row.";
                break;
            case 5:
                randomSpeech = "Hitting snakes will make you drop what you are carrying!";
                break;
            case 6:
                randomSpeech = "If you are a friend, you speak the password, and the doors will open.";
                break;
            case 7:
                randomSpeech = "I DRINK YOUR MILKSHAKE!";
                break;
        }
        return randomSpeech;
    }

}
