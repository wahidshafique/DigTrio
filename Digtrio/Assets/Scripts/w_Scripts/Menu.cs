using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	public bool manualLoad = false;
	
	void Update () {
		if (manualLoad)
			Application.LoadLevel(0);
	}

	public void StartGame () {
		Application.LoadLevel("Game");
	}
	
	public void MainMenu () {
		Application.LoadLevel("Menu");
	}
	
	public void Credits () {
		Application.LoadLevel("Credits");
	}
	public void Quit () {
		Application.Quit();
	}
}
