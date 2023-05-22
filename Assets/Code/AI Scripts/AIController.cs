using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // make this the script that all ai uses to get access to different components in the game
    // master script even if dont make more than one other ai script

    // REFERENCES
    private GameController gameController;
    private MainBoardController mainBoardController;

    // VARIABLES
    private List<TileController> availableTiles = new List<TileController>();

    // FUNCTIONS
    private void Awake() {
        gameController = GameController.Instance;
        mainBoardController = gameController.mainBoardController;
    }

    public void MiniMax(){
        // CLEAR Tiles?
        availableTiles.Clear();

        // adds all tiles that can be played to list
        foreach(BoardController board in mainBoardController.boardControllers){
            if(board.thisBoardSprite.color == gameController.highlightColour){
                foreach(TileController tile in board.tileControllers){
                    if(tile.canUseTile){
                        availableTiles.Add(tile);
                    }
                }
            }
        }

        //randomly choose tile to play
        int tileToPlay;
        tileToPlay = Random.Range(0, availableTiles.Count);
        availableTiles[tileToPlay].OnTileClick();

        // is it fine that ai will always play as ogre?
        // need to be informed then when it is ai turn
        // need to call ontileclick based on the selected tile?
        // loop through tiles randomly to pick one
        // rather than making everything public, use a method to call and check each step
        // steps will be which board, then which tile, then randomly choosing a tile, might use while loop but double check no infinite loop
    }

    // might have to make this return bool
    // The optimal move will probably always be the same
    // so will hard code that to cut down on processing time
    private void HardcodedFirstMove(){
        // do this if neces, probably will be
    }
}
