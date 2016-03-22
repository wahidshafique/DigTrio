using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {
    public static bool accelActive = true;
    public bool manualLoad = false;
    [SerializeField]
    Toggle accToggle;

    void Update() {

        if (manualLoad)
            Application.LoadLevel(0);
    }

    public void StartGame() {
        Application.LoadLevel("Game");
    }

    public void MainMenu() {
        Application.LoadLevel("Menu");
    }

    public void Credits() {
        Application.LoadLevel("Credits");
    }
    public void Quit() {
        Application.Quit();
    }

    public void SetAccel() {
        if (accToggle.isOn) accelActive = true; else accelActive = false;
        print("accel active is : " + accelActive);
    }
}
