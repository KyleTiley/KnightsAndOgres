using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBoardController : MonoBehaviour
{
    // REFERENCES
    public List<BoardController> boardControllers = new List<BoardController>();

    // VARIABLES
    public int boardsComplete;

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

    // Checks all possibilities for a won game
    public void CheckGameWinner(bool _isPlayersTurn){
        CheckMainRows(_isPlayersTurn);
        CheckMainColumns(_isPlayersTurn);
        CheckMainDiagonals(_isPlayersTurn);
    }

    // Checks the main rows for a winner
    private void CheckMainRows(bool _player){
        // Top row
        if(boardControllers[1].thisBoardSprite.color != Color.green && boardControllers[1].thisBoardSprite.color != Color.yellow){
            if(boardControllers[0].thisBoardSprite.color == boardControllers[1].thisBoardSprite.color
            && boardControllers[1].thisBoardSprite.color == boardControllers[2].thisBoardSprite.color){
                SetGameWinner(_player);
            }
        }
        
        // Middle row
        if(boardControllers[4].thisBoardSprite.color != Color.green && boardControllers[4].thisBoardSprite.color != Color.yellow){
            if(boardControllers[3].thisBoardSprite.color == boardControllers[4].thisBoardSprite.color
            && boardControllers[4].thisBoardSprite.color == boardControllers[5].thisBoardSprite.color){
                SetGameWinner(_player);
            }
        }
        
        // Bottom row
        if(boardControllers[7].thisBoardSprite.color != Color.green && boardControllers[7].thisBoardSprite.color != Color.yellow){
            if(boardControllers[6].thisBoardSprite.color == boardControllers[7].thisBoardSprite.color
            && boardControllers[7].thisBoardSprite.color == boardControllers[8].thisBoardSprite.color){
                SetGameWinner(_player);
            }
        }
    }

    // Checks the main columns for a winner
    private void CheckMainColumns(bool _player){
        // Left column
        if(boardControllers[3].thisBoardSprite.color != Color.green && boardControllers[3].thisBoardSprite.color != Color.yellow){
            if(boardControllers[0].thisBoardSprite.color == boardControllers[3].thisBoardSprite.color
            && boardControllers[3].thisBoardSprite.color == boardControllers[6].thisBoardSprite.color){
                SetGameWinner(_player);
            }
        }
        
        // Middle column
        if(boardControllers[4].thisBoardSprite.color != Color.green && boardControllers[4].thisBoardSprite.color != Color.yellow){
            if(boardControllers[1].thisBoardSprite.color == boardControllers[4].thisBoardSprite.color
            && boardControllers[4].thisBoardSprite.color == boardControllers[7].thisBoardSprite.color){
                SetGameWinner(_player);
            }
        }
        
        // Right column
        if(boardControllers[5].thisBoardSprite.color != Color.green && boardControllers[5].thisBoardSprite.color != Color.yellow){
            if(boardControllers[2].thisBoardSprite.color == boardControllers[5].thisBoardSprite.color
            && boardControllers[5].thisBoardSprite.color == boardControllers[8].thisBoardSprite.color){
                SetGameWinner(_player);
            }
        }
    }

    // Checks the main diagonals for a winner
    private void CheckMainDiagonals(bool _player){
        // First diagonal
        if(boardControllers[4].thisBoardSprite.color != Color.green && boardControllers[4].thisBoardSprite.color != Color.yellow){
            if(boardControllers[0].thisBoardSprite.color == boardControllers[4].thisBoardSprite.color
            && boardControllers[4].thisBoardSprite.color == boardControllers[8].thisBoardSprite.color){
                SetGameWinner(_player);
            }
        }
        
        // Second diagonal
        if(boardControllers[4].thisBoardSprite.color != Color.green && boardControllers[4].thisBoardSprite.color != Color.yellow){
            if(boardControllers[2].thisBoardSprite.color == boardControllers[4].thisBoardSprite.color
            && boardControllers[4].thisBoardSprite.color == boardControllers[6].thisBoardSprite.color){
                SetGameWinner(_player);
            }
        }
    }

    // Sets the winner of the game
    private void SetGameWinner(bool _winningPlayer){
        foreach(BoardController board in boardControllers){
            // Disables all tiles in the game since it is over
            board.DisableAllTiles();

            // Changes next boards back to default
            if(board.thisBoardSprite.color == Color.yellow){
                board.ChangeBoardImage(Color.green);
            }

            // Changes the image of the main board based on the winning player
            if(_winningPlayer){
                this.GetComponent<Image>().color = Color.blue;
            }
            else{
                this.GetComponent<Image>().color = Color.red;
            }
        }
    }
}