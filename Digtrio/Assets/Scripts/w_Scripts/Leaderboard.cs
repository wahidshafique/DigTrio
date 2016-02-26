using UnityEngine;
using System.Collections;

public class Leaderboard : MonoBehaviour {
	int[] scoreArray;
	// Use this for initialization
	void Start () {
		print (PlayerPrefs.GetInt("Player Score"));
		for (int i = 0; i < 11; i++) {
			
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
