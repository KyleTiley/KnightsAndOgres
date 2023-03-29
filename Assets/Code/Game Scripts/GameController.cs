using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // VARIABLES FOR GAME
    private bool isPlayersTurn; // true: player 1, false: player 2
    private bool isGameOver;
    private int totalBoardsWon;

    public void StartGame(){
        isPlayersTurn = Random.Range(0,2) == 1; // Decides if Player 1 should start

        isGameOver = false;
        totalBoardsWon = 0;
    }
}
