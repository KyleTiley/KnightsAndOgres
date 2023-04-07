using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBoardController : MonoBehaviour
{
    // REFERENCES
    public List<BoardController> boardControllers = new List<BoardController>();

    // FUNCTIONS
    private void Awake() {
        foreach(Transform child in transform){
            boardControllers.Add(child.GetComponent<BoardController>());
        }
    }

    // Sets the next playable board/s based on the game rules
    public void SetPlayableBoard(int _nextBoard){
        // Checks if the next board is already won, then the player can play on any available board
        if(boardControllers[_nextBoard].boardIsWon){
            foreach(BoardController board in boardControllers){
                // Sets all empty tiles in uncomplete boards to usable
                if(!board.boardIsWon){
                    board.EnableEmptyTiles();
                    board.GetComponent<Image>().color = Color.yellow;
                }
                // Makes sure complete boards are fully disabled
                else{
                    board.DisableAllTiles();
                }
            }
        }
        // Sets the tiles in the next board to be the only playable tiles
        else{
            foreach(BoardController board in boardControllers){
                // Disables tiles in boards that are not the next board
                if(board != boardControllers[_nextBoard]){
                    board.DisableAllTiles();
                    if(!board.boardIsWon){
                        board.GetComponent<Image>().color = Color.green;
                    }
                }
                // Enables the available tiles in the next board
                else{
                    board.EnableEmptyTiles();
                    board.ChangeBoardImage(Color.yellow);
                }
            }
        }
    }

    private void CheckGameWinner(){
        // gotta do this
    }
}
