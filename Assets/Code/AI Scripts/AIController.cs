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
    private NeuralNetworkAI neuralNetworkAI;

    // VARIABLES FOR BOARD STATES AND EVALUATION
    protected List<TileController> allTiles = new List<TileController>();
    protected List<TileController> availableTiles = new List<TileController>();
    protected List<BoardController> availableBoards = new List<BoardController>();
    protected int depthSpecified;
    protected char[,] boardStateArray = new char[9,9];

    // VARIABLES FOR TILE UTILITY VALUES
    protected int centreTileValue = -4;
    protected int cornerTileValue = -3;
    protected int sideTileValue = -2;

    // FUNCTIONS
    private void Awake() {
        gameController = GameController.GameControllerInstance;
        difficultyController = DifficultyController.DifficultyControllerInstance;
        mainBoardController = gameController.mainBoardController;

        easyAIController = gameController.easyAIController;
        miniMaxAI = gameController.miniMaxAI;
        neuralNetworkAI = gameController.neuralNetworkAI;

        CollectAllTiles();
    }

    // Plays the turn based on the chosen AI difficulty
    public void PlayTurn(bool shouldHardCode){
        if(difficultyController.gameType != "RANDOM"){
            if(!shouldHardCode){
                if(difficultyController.gameType == "MINIMAX"){
                    // Plays the MiniMax AI
                    depthSpecified = 4;
                    miniMaxAI.MiniMax();
                }
                else{
                    // Plays the Neural Network AI
                    neuralNetworkAI.NeuralNetwork();
                }
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

    protected void CollectAllTiles(){
        allTiles.Clear();

        // Adds all tiles to list
        foreach(BoardController board in mainBoardController.boardControllers){
            foreach(TileController tile in board.tileControllers){
                allTiles.Add(tile);
            }
        }
    }

    // Used to get a list of all available tiles, for hardcoded move and randomplay
    protected void CollectAvailableTiles(){
        // Empties available tile list before filling again
        availableTiles.Clear();

        // Adds all tiles that can be played to list
        foreach(BoardController board in mainBoardController.boardControllers){
            if(board.thisBoardSprite.color == gameController.highlightColour){
                // save the board name and make sure to check all boards
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
                char tileValue = '.';
                if(tile.canUseTile){
                    tileValue = '0';
                }
                // If tile is a knight, which is player
                else if(tile.thisSprite.sprite == gameController.knightSprite){
                    tileValue = 'K';
                }
                // If tile is ogre, which is AI
                else if(tile.thisSprite.sprite == gameController.ogreSprite){
                    tileValue = 'G';
                }

                boardStateArray[arrayHorizontalIndex, arrayVerticalIndex] = tileValue;
                arrayVerticalIndex++;
            }
            arrayVerticalIndex = 0;
            arrayHorizontalIndex++;
        }
    }
}
