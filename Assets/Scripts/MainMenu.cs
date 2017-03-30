using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    // Script designed to handle navigating to starting a new game of Tetris by moving to the Level scene
    // It also handles moving to the Score scene to see the latest score
    // Use this for initialization
    public void StartGame()
    {
        // Loads the level scene
        Application.LoadLevel("Level");

    }

    public void Score()
    {
        // Loads the score scene
        Application.LoadLevel("Score");

    }
}
