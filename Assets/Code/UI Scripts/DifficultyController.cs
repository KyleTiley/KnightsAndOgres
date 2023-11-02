using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    // REFERENCES FOR GLOBAL ACCESS
    public static DifficultyController DifficultyControllerInstance { get; private set; }

    // Denotes game mode
    public string gameType = "";

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

    public void RandomAI(){
        gameType = "RANDOM";
    }

    public void MiniMaxAI(){
        gameType = "MINIMAX";
    }

    public void NeuralAI(){
        gameType = "NEURAL";
    }
}