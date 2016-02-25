using UnityEngine;
using System.Collections;

public class SpeechRand : MonoBehaviour {
    int message = 0;

    public void Start() {
        message = Random.Range(1, 3);
    }

    //public void OnGUI() {
    //    if (message == 1)
    //        GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 500, 30), "Text1");
    //    if (message == 2)
    //        GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 500, 30), "Text2");
    //    if (message == 3)
    //        GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 500, 30), "Text3");
    //}
}

