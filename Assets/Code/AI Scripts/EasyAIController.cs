using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyAIController : MonoBehaviour
{
    // REFERENCES
    private GameController gameController;

    // VARIABLES


    // FUNCTIONS
    private void Awake() {
        gameController = GameController.GameControllerInstance;
    }


    
}
