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

    // Game Scene
    public void LaunchGame(){
        SceneManager.LoadScene(1);
    }

    // Quits Game
    public void QuitGame(){
        Application.Quit();
    }
}
