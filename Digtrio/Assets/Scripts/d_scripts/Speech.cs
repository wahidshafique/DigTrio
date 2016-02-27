using UnityEngine;
using System.Collections;

public class Speech : MonoBehaviour {
    string SpeechText;
    public string ButtonText = "";
    public int xOffset = 3;
    public int yOffset = 1;
    public Texture2D BubbleImage;    
    
    public int fontSize = 20;

    Vector2 BubblePosition; // set point to work off of bubble
    public Vector2 BubbleSize;
    public Vector2 BubbleOffset; // offset from set position
    
    public Vector2 TextArea;
    public Vector2 TextOffset;

    public Vector2 ButtonSize;
    public Vector2 ButtonOffset;

    // use ShowBubble(bool) to display the bubble
    public bool IsShowBubble = false;

    void Awake()
    {
        SpeechText = RandomSpeech();
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        screenPos.y = Screen.height - screenPos.y;

        Debug.Log(screenPos);

        BubblePosition = new Vector2(screenPos.x * xOffset, yOffset);
    }

    void OnGUI()
    {
        // Show the bubble
        if (IsShowBubble)
        {
            /* Speech Bubble Group Start */
            GUI.BeginGroup(new Rect(BubblePosition, new Vector2(BubbleImage.width, BubbleImage.height)), BubbleImage);
        
            // Speech Bubble button
            if (GUI.Button(new Rect(ButtonOffset, ButtonSize), ButtonText))
            {
                OnButton();
            }

            // displayed text
            Color backupContentColor = GUI.contentColor;
            Color oldColor = GUI.skin.settings.cursorColor;
            Color oldSelectionColor = GUI.skin.settings.selectionColor;
            float oldFlash = GUI.skin.settings.cursorFlashSpeed;

            GUI.contentColor = new Color(0, 0, 0);
        
            GUI.skin.textArea.normal.background = null;
            GUI.skin.textArea.active.background = null;
            GUI.skin.textArea.hover.background = null;
            GUI.skin.textArea.fontSize = fontSize;
            GUI.skin.settings.selectionColor = new Color(0, 0, 0, 0);
            GUI.skin.settings.cursorFlashSpeed = 0;        
            GUI.skin.settings.cursorColor = new Color(0,0,0,0);
        
            SpeechText = GUI.TextArea(new Rect(TextOffset, TextArea), SpeechText);            

            GUI.EndGroup();
            /* Speech Bubble Group End */

            GUI.contentColor = backupContentColor;
            GUI.skin.settings.cursorColor = oldColor;
            GUI.skin.settings.selectionColor = oldSelectionColor;
            GUI.skin.settings.cursorFlashSpeed = oldFlash;
        }
    }

    // Activate the bubble
    public void ShowBubble(bool b)
    {
        IsShowBubble = b;
        SpeechText = RandomSpeech();
    }

    void OnButton()
    {
        Inventory.Finder.SellItems();

        // player prefs?

        //Application.LoadLevel("Leaderboards");
    }

    // random speech
    string RandomSpeech()
    {
        int rand = Random.Range(0, 3);

        string randomSpeech = "";

        switch(rand)
        {
        case 0:
            randomSpeech = "\n Give me your goods.";
            break;
        case 1:
            randomSpeech = "Time is of the essence, father!";
            break;
        case 2:
            randomSpeech = "I think I'm forgetting  something.";
            break;
        }

        return randomSpeech;
    }
}
