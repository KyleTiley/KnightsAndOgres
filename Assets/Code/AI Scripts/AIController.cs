using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // PARENT SCRIPT FOR ALL AI DIFFICULTIES

    // REFERENCES
    private GameController gameController;
    private MainBoardController mainBoardController;

    // VARIABLES
    protected List<TileController> availableTiles = new List<TileController>();

    // FUNCTIONS
    private void Awake() {
        Debug.Log("IT DOES");
        gameController = GameController.GameControllerInstance;
        mainBoardController = gameController.mainBoardController;
    }

    protected void MiniMax(){
        CollectAvailableTiles();
    }

    // might have to make this return bool
    // The optimal move will probably always be the same
    // so will hard code that to cut down on processing time
    private void HardcodedFirstMove(){
        // do this if neces, probably will be
    }

    protected void CollectAvailableTiles(){
        // Empties available tile list before filling again
        availableTiles.Clear();

        // Adds all tiles that can be played to list
        foreach(BoardController board in mainBoardController.boardControllers){
            if(board.thisBoardSprite.color == gameController.highlightColour){
                foreach(TileController tile in board.tileControllers){
                    if(tile.canUseTile){
                        availableTiles.Add(tile);
                    }
                }
            }
        }
    }

}
