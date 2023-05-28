using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // PARENT SCRIPT FOR ALL AI DIFFICULTIES

    // REFERENCES
    private GameController gameController;
    private MainBoardController mainBoardController;
    private DifficultyController difficultyController;

    private EasyAIController easyAIController;
    private MiniMaxAI miniMaxAI;

    // VARIABLES FOR BOARD STATES AND EVALUATION
    protected List<TileController> availableTiles = new List<TileController>();
    protected List<BoardController> availableBoards = new List<BoardController>();
    protected int specifiedDepth;
    protected int[,] boardStateArray = new int[9,9];

    // VARIABLES FOR TILE UTILITY VALUES
    protected int centreTileValue = 3;
    protected int cornerTileValue = 2;
    protected int sideTileValue = 1;

    // FUNCTIONS
    private void Awake() {
        gameController = GameController.GameControllerInstance;
        difficultyController = DifficultyController.DifficultyControllerInstance;
        mainBoardController = gameController.mainBoardController;

        easyAIController = gameController.easyAIController;
        miniMaxAI = gameController.miniMaxAI;
    }

    // Plays the turn based on the chosen AI difficulty
    public void PlayTurn(bool shouldHardCode){
        if(difficultyController.gameType != "EASY"){
            if(!shouldHardCode){
                if(difficultyController.gameType == "MEDIUM"){
                    specifiedDepth = 3;
                }
                else{
                    specifiedDepth = 5;
                }
                miniMaxAI.MiniMax();
            }
            else{
                HardcodedFirstMove();
            }
        }
        else{
            easyAIController.RandomPlay();
        }
    }

    // This is always the optimal first move when the AI starts the game, so it is harcoded to save time
    private void HardcodedFirstMove(){
        CollectAvailableTiles();
        // Tile 36 is the first available corner tile in the middle board (all corners tiles in middle board hold the same value)
        availableTiles[36].OnTileClick();
    }

    protected void CollectAvailableTiles(){
        // Empties available tile list before filling again
        availableTiles.Clear();

        // Adds all tiles that can be played to list
        foreach(BoardController board in mainBoardController.boardControllers){
            if(board.thisBoardSprite.color == gameController.highlightColour){
                availableBoards.Add(board);
                foreach(TileController tile in board.tileControllers){
                    if(tile.canUseTile){
                        availableTiles.Add(tile);
                    }
                }
            }
        }
    }

    // Saves the entire board state to an array
    protected void SaveBoardState(){
        int arrayHorizontalIndex = 0; // Represents board number
        int arrayVerticalIndex = 0; // Represents tile number on board

        foreach(BoardController board in mainBoardController.boardControllers){
            foreach(TileController tile in board.tileControllers){
                // Sets all tiles to empty to begin with
                int tileValue = 0;

                // If tile is a knight, set to -1, since they will always be the minimising player
                if(tile.thisSprite.sprite == gameController.knightSprite){
                    tileValue = -1;
                }
                // If tile is ogre, set to 1, since the AI is the maximising player
                else if(tile.thisSprite.sprite == gameController.ogreSprite){
                    tileValue = 1;
                }

                boardStateArray[arrayHorizontalIndex, arrayVerticalIndex] = tileValue;
                arrayVerticalIndex++;
            }
            arrayVerticalIndex = 0;
            arrayHorizontalIndex++;
        }

        string debugOutput = " \n";
        for(int i = 0; i < 9; i++){
            string debugLine = "";
            for(int j = 0; j< 9; j++){
                debugLine += boardStateArray[i,j];
            }
            debugOutput += debugLine;
            debugOutput += '\n';
        }
        Debug.Log(debugOutput);
    }

}
