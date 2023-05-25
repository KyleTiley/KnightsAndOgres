using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public static DifficultyController DifficultyControllerInstance { get; private set; }
    private GameController gameController;

    public bool gameType; //true: pvp; false: pve

    private void Awake() {
        gameController = GameController.GameControllerInstance;

        //Creates difficulty controller singleton
        if(DifficultyControllerInstance == null){
            DifficultyControllerInstance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    public void PlayerVersusPlayer(){
        gameType = true;
    }

    public void PlayerVersusAI(){
        gameType = false;
    }
}