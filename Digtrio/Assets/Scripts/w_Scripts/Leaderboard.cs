using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Leaderboard : MonoBehaviour {
	int[] scoreArray;
    Text[] textArr;
	// Use this for initialization
	void Start () {
        textArr = GetComponentsInChildren<Text>();
        print(textArr[0]);
		//print (PlayerPrefs.GetInt("Player 1"));
		for (int i = 1; i < textArr.Length; i++) {
            textArr[i].text = i + ". " + PlayerPrefs.GetInt("Player");
		}
	}
	
	void Update () {

	}
}
