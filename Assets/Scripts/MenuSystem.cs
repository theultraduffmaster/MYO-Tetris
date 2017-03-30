using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystem : MonoBehaviour {

    // Script to handle reloading the Level scene back in after getting to the GameOver scene
	// Use this for initialization
	public void PlayAgain()
    {
        Globalclass.globalHighscore += 0;
        // Loads the level scene to play again
        Application.LoadLevel("Level");

    }

    public void ReturnToMainMenu()
    {
        // Returns to Main Menu
        Application.LoadLevel("MainMenu");

    }
}
