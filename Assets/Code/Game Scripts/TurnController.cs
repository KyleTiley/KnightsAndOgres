using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    // VARIABLES
    public bool isPlayersTurn; // true: player 1, false: player 2
    // private bool isGameOver;
    // private int totalBoardsWon;

    private void Start() {
        isPlayersTurn = Random.Range(0,2) == 1;
    }

    public void SwitchPlayers(){
        isPlayersTurn = !isPlayersTurn;
    }
}
