using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    // REFERENCES FOR GLOBAL ACCESS
    public static DifficultyController DifficultyControllerInstance { get; private set; }

    public bool gameType; //true: pvp; false: pve

    private void Awake() {
        //Creates difficulty controller singleton
        if(DifficultyControllerInstance == null){
            DifficultyControllerInstance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    // Sets game options to PVP
    public void PlayerVersusPlayer(){
        gameType = true;
    }

    // Sets game options to AI game
    public void PlayerVersusAI(){
        gameType = false;
    }
}