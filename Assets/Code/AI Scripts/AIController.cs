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

    protected int[,] boardStateArray = new int[9,9];

    // FUNCTIONS
    private void Awake() {
        gameController = GameController.GameControllerInstance;
        mainBoardController = gameController.mainBoardController;
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

    protected void SaveBoardState(){
        // dont forget to clear array?
        int arrayHorizontalIndex = 0; // determines board number
        int arrayVerticalIndex = 0; // determines tile number on board

        foreach(BoardController board in mainBoardController.boardControllers){
            foreach(TileController tile in board.tileControllers){
                int tileValue = 0;
                if(tile.thisSprite.sprite == gameController.knightSprite){
                    tileValue = 1;
                }
                else if(tile.thisSprite.sprite == gameController.ogreSprite){
                    tileValue = -1;
                }

                boardStateArray[arrayHorizontalIndex, arrayVerticalIndex] = tileValue;

                arrayVerticalIndex++;
            }
            arrayVerticalIndex = 0;
            arrayHorizontalIndex++;
        }
    }

}
