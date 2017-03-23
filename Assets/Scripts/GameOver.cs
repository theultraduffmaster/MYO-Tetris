using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public Text gameover_score;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateUI();
	}

    public void UpdateUI()
    {
        gameover_score.text = Globalclass.globalHighscore.ToString();
    }
}
