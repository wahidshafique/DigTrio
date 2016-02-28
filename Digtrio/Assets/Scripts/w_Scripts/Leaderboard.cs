using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Leaderboard : MonoBehaviour {
    bool highSet;
    int[] scoreArray;
    Text[] textArr;
    // Use this for initialization
    void Start() {
        highSet = true;
        //PlayerPrefs.DeleteAll();
        int finalCash = InventoryManager.cash;
        print(finalCash);
        textArr = GetComponentsInChildren<Text>();
        for (int i = 1; i < textArr.Length - 1; i++) {
            //check if new device even has the keys, otherwise init them
            if (!PlayerPrefs.HasKey("HighScore" + i)) PlayerPrefs.SetInt("HighScore" + i, 0);
            if (finalCash > PlayerPrefs.GetInt("HighScore" + i) && highSet) {//if final is greater than index
                if (PlayerPrefs.GetInt("HighScore" + i) > PlayerPrefs.GetInt("HighScore" + (i + 1))) {//now check if its greater than the next one
                    PlayerPrefs.SetInt("HighScore" + (i + 1), PlayerPrefs.GetInt("HighScore" + i));
                };
                PlayerPrefs.SetInt("HighScore" + i, finalCash);//set final to it 
                highSet = false;
            }
            textArr[i].text = i + ". " + PlayerPrefs.GetInt("HighScore" + i);
        }
    }

    void Update() {

    }
}
