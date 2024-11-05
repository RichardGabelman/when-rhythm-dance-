using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    
    // Song 1 button on the main menu, triggers the Song 1 scene
    public void SongOneButton() {
        SceneManager.LoadScene(1);
    }

    // Quit button on the main menu, ends the application
    public void OnQuitButton() {
        Application.Quit();
    }

    // Back button on the results screen after finishing a song, returns to the main menu
    public void BackToMenuButton() {
        SceneManager.LoadScene(0);
    }

}
