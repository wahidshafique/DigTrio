using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Leaderboard : MonoBehaviour {
    //bool hasEnteredName = false; FUTURE NAMING FEATURE, this applies to rest of commented out code

    /*int UNICODE = 65; // the ascii value
    public Text hText1;
    public Text hText2;
    public Text hText3;
    */
    Text[] textArr;//to hold the initial batch of leaderB text
    //string leaderName = "444";
    //List<string> leaderNameList = new List<string>();
    List<int> leaderList = new List<int>();

    void Start() {
        /*
        hText1.transform.parent.gameObject.SetActive(false);//sanity check
        hText1.text = decode();
        */
        //PlayerPrefs.DeleteAll();
        int endCash = InventoryManager.cash;

        textArr = GetComponentsInChildren<Text>();

        for (int i = 0; i < textArr.Length - 2; i++) {//pull the keys values from prefs
            if (!PlayerPrefs.HasKey("Leader" + i)) PlayerPrefs.SetInt("Leader" + i, 0);//if the key does not exist, set it
            //if (!PlayerPrefs.HasKey("Name" + i)) PlayerPrefs.SetString("Name" + i, "AAA");
            leaderList.Add(PlayerPrefs.GetInt("Leader" + i));//add everyone to this temp List
            //leaderNameList.Add(PlayerPrefs.GetString("Name" + i));
        }

        for (int i = 0; i < textArr.Length - 2; i++) {//set HS
            if (endCash > leaderList[i]) {//if final time is less than index
                leaderList.Insert(i, endCash);//add it to the List
                //StartCoroutine(EnterName());
                //leaderNameList.Insert(i, leaderName);
                break;
            }
        }

        for (int i = 0; i < textArr.Length - 2; i++) {
            //PlayerPrefs.SetString("Name" + i, leaderNameList[i]);//'key'
            PlayerPrefs.SetInt("Leader" + i, leaderList[i]);//'val'
            textArr[i].text = (i + 1) /*PlayerPrefs.GetString("Name" + i)*/ + ". " + PlayerPrefs.GetInt("Leader" + i).ToString();
        }
    }
    /* ALL THIS IS FOR FURTHER ITERATIONS
    IEnumerator EnterName() {
        //if (device != null) {need to get this to work w/ device! TODO
        hText1.transform.parent.gameObject.SetActive(true);//so first show the object
        while (!hasEnteredName) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                UNICODE++;
                hText1.text = decode();
                print("Up");
            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                UNICODE--;
                hText1.text = decode();
                print("Down");
            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                hText1.transform.parent.gameObject.SetActive(false);
                leaderName = hText1.text + hText2.text + hText3.text;
                print(leaderName);
                hasEnteredName = true;
            }
            yield return null;
        }
    }

    string decode() {
        //force characters to stay within limit
        if (UNICODE < 65) UNICODE = 65;
        else if (UNICODE > 90) UNICODE = 90;

        char character = (char)UNICODE;

        return character.ToString();
    }
    // } else print("NO DEVICE!!!!!");
    */
}

