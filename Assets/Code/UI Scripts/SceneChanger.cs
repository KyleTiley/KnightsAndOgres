using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Main Menu
    public void MainMenu(){
        SceneManager.LoadScene(0);
    }

    // Launches into the game scene
    public void LaunchGame(){
        SceneManager.LoadScene(1);
    }

    // Quits game
    public void QuitGame(){
        Application.Quit();
    }
}
