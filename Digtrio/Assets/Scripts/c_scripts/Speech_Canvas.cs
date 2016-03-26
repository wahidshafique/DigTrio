using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Speech_Canvas : MonoBehaviour {

    public GameObject SpeechBubbleObject;

    private string SpeechText;
    public Text SpeechBubble;

    public bool IsShowBubble = false;

    void Update()
    {
        DisplayText();
    }

    public void DisplayText()
    {
        if (IsShowBubble)
        {
            SpeechBubbleObject.SetActive(true);
            SpeechBubble.text = SpeechText;
        }
        else
        {
            SpeechBubbleObject.SetActive(false);
        }
    }

    // Activate the bubble
    public void ShowBubble(bool b)
    {
        IsShowBubble = b;
        SpeechText = RandomSpeech();
    }

    public void SellItemsButton()
    {
        Inventory.Finder.SellItems();

        // player prefs?

        //Application.LoadLevel("Leaderboards");
    }

    // random speech
    string RandomSpeech()
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
