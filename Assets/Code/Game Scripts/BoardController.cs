using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField]
    public bool boardWon { get; private set; }
    [SerializeField]
    public string winner { get; private set; }
    [SerializeField]
    public static int boardsWon { get; private set; }

    private void Awake() {
        boardWon = false;
        winner = "";
        boardsWon = 0;    
    }

    public void setWinner(string winnerSymbol){
        if(!boardWon){
            boardsWon++;
            boardWon = true;
            winner = winnerSymbol;

            // use the correct path below to find winner symbols
            this.transform.Find("winner symbol").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(winnerSymbol);
            this.transform.Find("winner symbol").GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void CheckBoardWinner(){

    }
}
