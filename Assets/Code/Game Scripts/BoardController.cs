using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BoardController : MonoBehaviour
{
    // REFERENCES
    public List<TileController> tileControllers = new List<TileController>();
    private MainBoardController mainBoardController;

    // VARIABLES
    public int tilesPlayed;
    private bool boardIsWon;

    // METHODS
    private void Awake() {
        mainBoardController = GetComponentInParent<MainBoardController>();
        foreach(Transform child in transform){
            tileControllers.Add(child.GetComponent<TileController>());
        }
        tilesPlayed = 0;
        boardIsWon = false;
    }

    public void CheckBoardWinner(bool _isPlayersTurn){
        CheckRows(_isPlayersTurn);
        CheckColumns(_isPlayersTurn);
        CheckDiagonals(_isPlayersTurn);
    }

    public void ChooseNextBoard(char _tileNumber){
        // Converts the tile char to an int for the board (minus 1 as lists start at 0)
        int nextBoard = (int)Char.GetNumericValue(_tileNumber) - 1;
        mainBoardController.SetPlayableBoard(nextBoard);
    }

    // Checks rows for a winner
    private void CheckRows(bool _player){
        // Top row
        if(tileControllers[1].thisSprite.sprite.name != "UISprite"){
            if(tileControllers[0].thisSprite.sprite.name == tileControllers[1].thisSprite.sprite.name
            && tileControllers[1].thisSprite.sprite.name == tileControllers[2].thisSprite.sprite.name){
                SetBoardWinner(_player);
            }
        }
        
        // Middle row
        if(tileControllers[4].thisSprite.sprite.name != "UISprite"){
            if(tileControllers[3].thisSprite.sprite.name == tileControllers[4].thisSprite.sprite.name
            && tileControllers[4].thisSprite.sprite.name == tileControllers[5].thisSprite.sprite.name){
                SetBoardWinner(_player);
            }
        }
        
        // Bottom row
        if(tileControllers[7].thisSprite.sprite.name != "UISprite"){
            if(tileControllers[6].thisSprite.sprite.name == tileControllers[7].thisSprite.sprite.name
            && tileControllers[7].thisSprite.sprite.name == tileControllers[8].thisSprite.sprite.name){
                SetBoardWinner(_player);
            }
        }
    }

    // Checks columns for a winner
    private void CheckColumns(bool _player){
        // Left column
        if(tileControllers[3].thisSprite.sprite.name != "UISprite"){
            if(tileControllers[0].thisSprite.sprite.name == tileControllers[3].thisSprite.sprite.name
            && tileControllers[3].thisSprite.sprite.name == tileControllers[6].thisSprite.sprite.name){
                SetBoardWinner(_player);
            }
        }

        // Middle column
        if(tileControllers[4].thisSprite.sprite.name != "UISprite"){
            if(tileControllers[1].thisSprite.sprite.name == tileControllers[4].thisSprite.sprite.name
            && tileControllers[4].thisSprite.sprite.name == tileControllers[7].thisSprite.sprite.name){
                SetBoardWinner(_player);
            }
        }

        // Right column
        if(tileControllers[5].thisSprite.sprite.name != "UISprite"){
            if(tileControllers[2].thisSprite.sprite.name == tileControllers[5].thisSprite.sprite.name
            && tileControllers[5].thisSprite.sprite.name == tileControllers[8].thisSprite.sprite.name){
                SetBoardWinner(_player);
            }
        }
    }

    // Checks diagonals for a winner
    public void CheckDiagonals(bool _player){
        if(tileControllers[4].thisSprite.sprite.name != "UISprite"){
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
            this.GetComponent<Image>().color = Color.green;
        }
        else{
            this.GetComponent<Image>().color = Color.red;
        }
        DisableAllTiles();
    }

    // Disables all tiles on board
    public void DisableAllTiles(){
        foreach(TileController _tile in tileControllers){
            _tile.canUseTile = false;
        }
    }

    //Enables playable tiles on board
    public void EnableEmptyTiles(){
        foreach(TileController _tile in tileControllers){
            if(_tile.thisSprite.sprite.name == "UISprite"){
                _tile.canUseTile = true;
            }
        }
    }
}
