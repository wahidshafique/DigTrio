using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game;

public class GameManager : MonoBehaviour {

    GameState state;

    // make sure update only fires effect of state once
    bool bStateChange;

	// Use this for initialization
	void Start () {
	    ToMenu();
        bStateChange = false;
	}
	
	// Update is called once per frame
	void Update () {
	    CheckState();
	}

    void CheckState()
    {
        switch(state)
        {
        case GameState.SCORE:
            if (bStateChange)
            {
                bStateChange = false;
                
                // animation?                
            }
            break;
        case GameState.GAME:
            break;
        case GameState.GAMEOVER:
            // animation?
            break;
        case GameState.MENU:
            break;
        case GameState.PAUSED:
            break;
        }
    }

    #region State Changers
    public void ToPause()
    {
        state = GameState.PAUSED;
        bStateChange = true;
    }

    public void ToGame()
    {
        state = GameState.GAME;
        bStateChange = true;
    }

    public void ToMenu()
    {
        state = GameState.MENU;
        bStateChange = true;
    }

    public void ToScore()
    {
        state = GameState.SCORE;
        bStateChange = true;
    }

    public void ToGameover()
    {
        state = GameState.GAMEOVER;
        bStateChange = true;
    }

    #endregion
}
