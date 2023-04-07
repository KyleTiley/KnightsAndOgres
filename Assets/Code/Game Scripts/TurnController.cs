using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    // VARIABLES
    // Denotes the active player, true: player 1, false: player 2
    public bool isPlayersTurn;

    // FUNCTIONS
    private void Start(){
        isPlayersTurn = Random.Range(0,2) == 1;
    }

    // Switches from one player to the other
    public void SwitchPlayers(){
        isPlayersTurn = !isPlayersTurn;
    }
}
