using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    // Script to handle the GameOver scene and display the score that was achieved in the game
    public Text gameover_score;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Calls the method from below
        UpdateUI();
	}

    public void UpdateUI()
    {
        // Shows the final score in the canvas in Unity
        gameover_score.text = Globalclass.globalHighscore.ToString();
    }
}
