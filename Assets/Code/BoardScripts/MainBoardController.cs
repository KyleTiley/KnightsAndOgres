using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBoardController : MonoBehaviour
{
    // REFERENCES
    private GameController gameController;
    public List<BoardController> boardControllers = new List<BoardController>();

    // VARIABLES
    private int nextPlayableBoard;

    // FUNCTIONS
    private void Awake() {
        gameController = GameController.GameControllerInstance;
        foreach(Transform child in transform){
            boardControllers.Add(child.GetComponent<BoardController>());
        }
    }

    // Sets the next playable board/s based on the game rules
    public void SetPlayableBoard(int _nextBoard){
        // Sets next playable board
        nextPlayableBoard = _nextBoard;

        // Checks if the next board is already won, then the player can play on any available board
        if(boardControllers[nextPlayableBoard].boardIsWon){
            foreach(BoardController board in boardControllers){
                // Sets all empty tiles in uncomplete boards to usable
                if(!board.boardIsWon){
                    board.EnableEmptyTiles();
                    board.HighlightBoard();
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
                if(board != boardControllers[nextPlayableBoard]){
                    board.DisableAllTiles();
                    if(!board.boardIsWon){
                        board.thisBoardSprite.color = gameController.defaultBoardColour;
                    }
                }
                // Enables the available tiles in the next board
                else{
                    board.EnableEmptyTiles();
                    board.HighlightBoard();
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
        if(boardControllers[1].thisBoardSprite.color != gameController.defaultBoardColour && boardControllers[1].thisBoardSprite.color != gameController.highlightColour){
            if(boardControllers[0].thisBoardSprite.sprite.name == boardControllers[1].thisBoardSprite.sprite.name
            && boardControllers[1].thisBoardSprite.sprite.name == boardControllers[2].thisBoardSprite.sprite.name){
                SetGameWinner(_player);
            }
        }
        
        // Middle row
        if(boardControllers[4].thisBoardSprite.color != gameController.defaultBoardColour && boardControllers[4].thisBoardSprite.color != gameController.highlightColour){
            if(boardControllers[3].thisBoardSprite.sprite.name == boardControllers[4].thisBoardSprite.sprite.name
            && boardControllers[4].thisBoardSprite.sprite.name == boardControllers[5].thisBoardSprite.sprite.name){
                SetGameWinner(_player);
            }
        }
        
        // Bottom row
        if(boardControllers[7].thisBoardSprite.color != gameController.defaultBoardColour && boardControllers[7].thisBoardSprite.color != gameController.highlightColour){
            if(boardControllers[6].thisBoardSprite.sprite.name == boardControllers[7].thisBoardSprite.sprite.name
            && boardControllers[7].thisBoardSprite.sprite.name == boardControllers[8].thisBoardSprite.sprite.name){
                SetGameWinner(_player);
            }
        }
    }

    // Checks the main columns for a winner
    private void CheckMainColumns(bool _player){
        // Left column
        if(boardControllers[3].thisBoardSprite.color != gameController.defaultBoardColour && boardControllers[3].thisBoardSprite.color != gameController.highlightColour){
            if(boardControllers[0].thisBoardSprite.sprite.name == boardControllers[3].thisBoardSprite.sprite.name
            && boardControllers[3].thisBoardSprite.sprite.name == boardControllers[6].thisBoardSprite.sprite.name){
                SetGameWinner(_player);
            }
        }
        
        // Middle column
        if(boardControllers[4].thisBoardSprite.color != gameController.defaultBoardColour && boardControllers[4].thisBoardSprite.color != gameController.highlightColour){
            if(boardControllers[1].thisBoardSprite.sprite.name == boardControllers[4].thisBoardSprite.sprite.name
            && boardControllers[4].thisBoardSprite.sprite.name == boardControllers[7].thisBoardSprite.sprite.name){
                SetGameWinner(_player);
            }
        }
        
        // Right column
        if(boardControllers[5].thisBoardSprite.color != gameController.defaultBoardColour && boardControllers[5].thisBoardSprite.color != gameController.highlightColour){
            if(boardControllers[2].thisBoardSprite.sprite.name == boardControllers[5].thisBoardSprite.sprite.name
            && boardControllers[5].thisBoardSprite.sprite.name == boardControllers[8].thisBoardSprite.sprite.name){
                SetGameWinner(_player);
            }
        }
    }

    // Checks the main diagonals for a winner
    private void CheckMainDiagonals(bool _player){
        // First diagonal
        if(boardControllers[4].thisBoardSprite.color != gameController.defaultBoardColour && boardControllers[4].thisBoardSprite.color != gameController.highlightColour){
            if(boardControllers[0].thisBoardSprite.sprite.name == boardControllers[4].thisBoardSprite.sprite.name
            && boardControllers[4].thisBoardSprite.sprite.name == boardControllers[8].thisBoardSprite.sprite.name){
                SetGameWinner(_player);
            }
        }
        
        // Second diagonal
        if(boardControllers[4].thisBoardSprite.color != gameController.defaultBoardColour && boardControllers[4].thisBoardSprite.color != gameController.highlightColour){
            if(boardControllers[2].thisBoardSprite.sprite.name == boardControllers[4].thisBoardSprite.sprite.name
            && boardControllers[4].thisBoardSprite.sprite.name == boardControllers[6].thisBoardSprite.sprite.name){
                SetGameWinner(_player);
            }
        }
    }

    // Sets the winner of the game
    private void SetGameWinner(bool _winningPlayer){
        foreach(BoardController board in boardControllers){
            // Disables all tiles in the game since it is over
            board.DisableAllTiles();

            // Changes unused boards to default
            if(board.thisBoardSprite.sprite.name == "UISprite"){
                board.thisBoardSprite.color = gameController.defaultBoardColour;
            }

            //Displays text based on the winning player
            if(_winningPlayer){
                gameController.winnerText.text = " Knights Win!";
            }
            else{
                gameController.winnerText.text = " Ogres Win!";
            }
            
        }
    }
}
