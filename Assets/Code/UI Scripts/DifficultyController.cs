using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    // REFERENCES FOR GLOBAL ACCESS
    public static DifficultyController DifficultyControllerInstance { get; private set; }

    // Denotes game mode
    public string gameType;

    private void Awake() {
        //Creates difficulty controller singleton
        if(DifficultyControllerInstance == null){
            DifficultyControllerInstance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    // Sets game options to support specific modes

    public void PlayerVersusPlayer(){
        gameType = "PVP";
    }

    public void EasyAI(){
        gameType = "EASY";
    }

    public void MediumAI(){
        gameType = "MEDIUM";
    }

    public void HardAI(){
        gameType = "HARD";
    }
}