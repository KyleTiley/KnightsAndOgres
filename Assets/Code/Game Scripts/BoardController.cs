using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BoardController : MonoBehaviour
{
    // REFERENCES
    private List<TileController> tileControllers = new List<TileController>();
    private GameController gameController;
    private MainBoardController mainBoardController;

    // VARIABLES
    public int tilesPlayed;
    public bool boardIsWon;

    // VARIABLES FOR BOARD SPRITE
    private GameObject thisBoard;
    public Image thisBoardSprite;

    // FUNCTIONS
    private void Awake() {
        gameController = GameController.Instance;
        mainBoardController = GetComponentInParent<MainBoardController>();
        foreach(Transform child in transform){
            tileControllers.Add(child.GetComponent<TileController>());
        }

        // Gets components of this board
        thisBoard = this.gameObject;
        thisBoardSprite = thisBoard.GetComponent<Image>();

        // Sets board variables to defaults
        tilesPlayed = 0;
        boardIsWon = false;

        // Hightlights the board as the colour of the starting player
        HighlightBoard();
    }

    // Checks all possibilities for a won board
    public void CheckBoardWinner(bool _isPlayersTurn){
        CheckRows(_isPlayersTurn);
        CheckColumns(_isPlayersTurn);
        CheckDiagonals(_isPlayersTurn);
    }

    // Chooses the next playable board/s based on the previous move
    public void ChooseNextBoard(char _tileNumber){
        // Converts the tile char to an int for the board (minus 1 as lists start at 0)
        int nextBoard = (int)Char.GetNumericValue(_tileNumber) - 1;
        mainBoardController.SetPlayableBoard(nextBoard);
    }

    // Checks rows for a winner
    private void CheckRows(bool _player){
        // Top row
        if(tileControllers[1].thisSprite.sprite != gameController.grassSprite){
            if(tileControllers[0].thisSprite.sprite.name == tileControllers[1].thisSprite.sprite.name
            && tileControllers[1].thisSprite.sprite.name == tileControllers[2].thisSprite.sprite.name){
                SetBoardWinner(_player);
            }
        }
        
        // Middle row
        if(tileControllers[4].thisSprite.sprite != gameController.grassSprite){
            if(tileControllers[3].thisSprite.sprite.name == tileControllers[4].thisSprite.sprite.name
            && tileControllers[4].thisSprite.sprite.name == tileControllers[5].thisSprite.sprite.name){
                SetBoardWinner(_player);
            }
        }
        
        // Bottom row
        if(tileControllers[7].thisSprite.sprite != gameController.grassSprite){
            if(tileControllers[6].thisSprite.sprite.name == tileControllers[7].thisSprite.sprite.name
            && tileControllers[7].thisSprite.sprite.name == tileControllers[8].thisSprite.sprite.name){
                SetBoardWinner(_player);
            }
        }
    }

    // Checks columns for a winner
    private void CheckColumns(bool _player){
        // Left column
        if(tileControllers[3].thisSprite.sprite != gameController.grassSprite){
            if(tileControllers[0].thisSprite.sprite.name == tileControllers[3].thisSprite.sprite.name
            && tileControllers[3].thisSprite.sprite.name == tileControllers[6].thisSprite.sprite.name){
                SetBoardWinner(_player);
            }
        }

        // Middle column
        if(tileControllers[4].thisSprite.sprite != gameController.grassSprite){
            if(tileControllers[1].thisSprite.sprite.name == tileControllers[4].thisSprite.sprite.name
            && tileControllers[4].thisSprite.sprite.name == tileControllers[7].thisSprite.sprite.name){
                SetBoardWinner(_player);
            }
        }

        // Right column
        if(tileControllers[5].thisSprite.sprite != gameController.grassSprite){
            if(tileControllers[2].thisSprite.sprite.name == tileControllers[5].thisSprite.sprite.name
            && tileControllers[5].thisSprite.sprite.name == tileControllers[8].thisSprite.sprite.name){
                SetBoardWinner(_player);
            }
        }
    }

    // Checks diagonals for a winner
    private void CheckDiagonals(bool _player){
        if(tileControllers[4].thisSprite.sprite != gameController.grassSprite){
            // First diagonal
            if(tileControllers[0].thisSprite.sprite.name == tileControllers[4].thisSprite.sprite.name
            && tileControllers[4].thisSprite.sprite.name == tileControllers[8].thisSprite.sprite.name){
                SetBoardWinner(_player);
            }

            // Second diagonal
            if(tileControllers[2].thisSprite.sprite.name == tileControllers[4].thisSprite.sprite.name
            && tileControllers[4].thisSprite.sprite.name == tileControllers[6].thisSprite.sprite.name){
                SetBoardWinner(_player);
            }
        }
    }

    // Sets the winner of this board
    private void SetBoardWinner(bool _winningPlayer){
        if(_winningPlayer){
            thisBoardSprite.sprite = gameController.castleSprite;
        }
        else{
            thisBoardSprite.sprite = gameController.hutSprite;
        }
        thisBoardSprite.color= gameController.boardWinColour;
        HideAllTiles();
        //DisableAllTiles();    not needed???
        boardIsWon = true;
    }

    // Disables all tiles on board
    public void DisableAllTiles(){
        foreach(TileController _tile in tileControllers){
            _tile.canUseTile = false;
        }
    }

    // Hides all tiles on board
    public void HideAllTiles(){
        foreach(Transform child in transform){
            child.gameObject.SetActive(false);
        }
    }

    // Enables playable tiles on board
    public void EnableEmptyTiles(){
        foreach(TileController _tile in tileControllers){
            if(_tile.thisSprite.sprite == gameController.grassSprite){
                _tile.canUseTile = true;
            }
        }
    }

    // Changes the image of the board based on game rule circumstances
    // public void ChangeBoardImage(Color32 _color){
    //     thisBoardSprite.color = _color;
    // }

    // Highlights the boards that the player can play on
    public void HighlightBoard(){
        thisBoardSprite.color = gameController.highlightColour;
    }
}
