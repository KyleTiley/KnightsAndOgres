using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBoardController : MonoBehaviour
{
    // REFERENCES
    public List<BoardController> boardControllers = new List<BoardController>();

    private void Awake() {
        foreach(Transform child in transform){
            boardControllers.Add(child.GetComponent<BoardController>());
        }
    }

    public void SetPlayableBoard(int _nextBoard){
        foreach(BoardController board in boardControllers){
            if(board != boardControllers[_nextBoard]){
                board.DisableAllTiles();
            }
            else{
                board.EnableEmptyTiles();
            }
        }
    }

    private void CheckGameWinner(){

    }
}
