using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    // Script designed for navigating back to the MainMenu scene from the Score scene
    // Use this for initialization
    public void ReturnToMainMenu()
    {
        // Returns to Main Menu
        Application.LoadLevel("MainMenu");

    }
}
