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

    // Player vs Player mode
    private void PVP(){
        SceneManager.LoadScene(1);
    }

    // Easy AI mode
    public void EasyAI(){
        SceneManager.LoadScene(2);
    }

    // Medium AI mode
    public void MediumAI(){
        SceneManager.LoadScene(3);
    }

    // Hard AI mode
    public void HardAI(){
        SceneManager.LoadScene(4);
    }
}
