using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MiniMaxAI : AIController
{
    // VARIABLES
    int[,] boardUtilityArray = new int[9,9];
    char[,] savedBoardStateArray = new char[9,9];

    // FUNCTIONS
    public void MiniMax(){
        // Saves the board state and collects all available tiles that the AI can play?
        SaveBoardState();
        // Save a copy to use when determining the move
        savedBoardStateArray = boardStateArray;

        Array.Clear(boardUtilityArray, 0, boardUtilityArray.Length);
        // Loops through all available tiles in saved board state
        for(int board = 0; board < 9; board ++){
            for(int tile = 0; tile < 9; tile++){
                
                // This is where MiniMax begins
                if(savedBoardStateArray[board,tile] == '0'){
                    int thisTileUtility = 100;
                    
                    thisTileUtility += EvaluateTile(board, tile);

                    for(int depthExplored = 0; depthExplored < depthSpecified; depthExplored++){
                        thisTileUtility += EvaluateTile(board, tile);
                        // Empty Tiles
                        boardStateArray[board,tile] = '.';
                        // AI
                        if(depthExplored % 2 == 0){
                            boardStateArray[board,tile] = 'G';
                        }
                        // Player
                        else{
                            boardStateArray[board,tile] = 'K';
                        }
                        // Set next playable tiles to playable
                        for(int nextTile = 0; nextTile < 9; nextTile++){
                            if(boardStateArray[tile,nextTile] == '.'){
                                boardStateArray[tile,nextTile] = '0';
                            }
                        }

                        for(int i = 0; i < 9; i++){
                            thisTileUtility += EvaluateTile(tile, i);
                        }
                    }

                    // Add utitlity to array
                    boardUtilityArray[board, tile] = thisTileUtility;
                }
            }
        }

        

        // do something where you flip the utils based on who is playing

        // DEBUG TO SHOW ALL UTILITY FOR TILES
        string debugOutput = " \n";
        for(int i = 0; i < 9; i++){
            string debugLine = "";
            for(int j = 0; j< 9; j++){
                debugLine += boardUtilityArray[i,j];
            }
            debugOutput += debugLine;
            debugOutput += '\n';
        }
        // Debug.Log(debugOutput);

        // Check which tiles utility is highest
        int maxUtility = boardUtilityArray.Cast<int>().Max();
        // Finds all tiles with max utility and adds them to a new set of lists
        List<int> maxUtilityBoards = new List<int>();
        List<int> maxUtilityTiles = new List<int>();
        for(int i = 0; i < 9; i++){
            for(int j = 0; j< 9; j++){
                if(boardUtilityArray[i,j] == maxUtility){
                    maxUtilityBoards.Add(i);
                    maxUtilityTiles.Add(j);
                }
            }
        }
        // Randomly picks one (only if there is a tie in choices)
        int tileToPlay;
        int maxUtilityIndex = 0;
        tileToPlay = UnityEngine.Random.Range(0, maxUtilityTiles.Count);
        for(int rand = 0; rand < maxUtilityTiles.Count; rand++){
            if(rand == tileToPlay){
                maxUtilityIndex = maxUtilityBoards[rand] * 9 + maxUtilityTiles[rand];
            }
        }

        // Play the move for the AI
        allTiles[maxUtilityIndex].OnTileClick();
    }

    // Takes in the tile's coords (i = board; j = tile) and evaluates the total utility for playing there
    private int EvaluateTile(int i, int j){
        int tileUtility = 0;

        if(boardStateArray[i,j] != '0'){
            return tileUtility;
        }
        else if(j != 4){
            if(j%2 != 0){
                tileUtility = CheckSideUtility(i, j);
            }
            else{
                tileUtility = CheckCornerUtility(i, j);
            }
        }
        else{
            tileUtility = CheckCentreUtility(i, j);
        }

        // Checks if maximiser can win
        tileUtility += CheckForWin(i,j,'G');
        // Checks if maximiser can block minimiser
        tileUtility += CheckForWin(i,j,'K') / 2;

        // Debug.Log(tileUtility);
        return tileUtility;
    }

    private int CheckCentreUtility(int board, int tile){
        int utils = 0;

        utils += centreTileValue;
        for(int i = 0; i < 9; i++){
            if(boardStateArray[board, i] == 'K'){
                utils -= 1;
            }
        }
        return ClampUtils(utils);
    }

    private int CheckCornerUtility(int board, int tile){
        int utils = 0;

        utils += cornerTileValue;
        for(int i = 0; i < 9; i++){
            // Assigned these -1 to see if anything breaks
            int ignored1 = -1;
            int ignored2 = -1;
            if(tile == 0){
                ignored1 = 5;
                ignored2 = 7;
            }
            else if(tile == 2){
                ignored1 = 3;
                ignored2 = 7;
            }
            else if(tile == 6){
                ignored1 = 1;
                ignored2 = 5;
            }
            else if(tile == 8){
                ignored1 = 1;
                ignored2 = 3;
            }
            
            if(i == ignored1 || i == ignored2){
                // Do nothing
            }
            else{
                if(boardStateArray[board, i] == 'K'){
                    utils -= 1;
                }
                else if(boardStateArray[board, i] == 'G'){
                    utils += 1;
                }
            }
        }

        return ClampUtils(utils);
    }

    private int CheckSideUtility(int board, int tile){
        int utils = 0;

        utils += sideTileValue;
        for(int i = 0; i < 9; i++){
            // Assigned these -1 to see if anything breaks
            int ignored1 = -1;
            int ignored2 = -1;
            int ignored3 = -1;
            int ignored4 = -1;
            
            if(tile == 1){
                ignored1 = 3;
                ignored2 = 6;
                ignored3 = 5;
                ignored4 = 8;
            }
            else if(tile == 3){
                ignored1 = 1;
                ignored2 = 2;
                ignored3 = 7;
                ignored4 = 8;
            }
            else if(tile == 5){
                ignored1 = 0;
                ignored2 = 1;
                ignored3 = 6;
                ignored4 = 7;
            }
            else if(tile == 7){
                ignored1 = 0;
                ignored2 = 2;
                ignored3 = 3;
                ignored4 = 5;
            }

            if(i == ignored1 || i == ignored2 || i == ignored3 || i == ignored4){
                // Do nothing
            }
            else{
                if(boardStateArray[board, i] == 'K'){
                    utils -= 1;
                }
                else if(boardStateArray[board, i] == 'G'){
                    utils += 1;
                }
            }
        }

        return ClampUtils(utils);
    }

    private int CheckForWin(int board, int tile, char player){
        int utils = 0;
        int utilsForWin = 100;
        
        switch(tile){
            case 0:
                if(boardStateArray[board, 1] == player && boardStateArray[board, 2] == player){
                    utils += utilsForWin;
                }
                else if(boardStateArray[board, 3] == player && boardStateArray[board, 6] == player){
                    utils += utilsForWin;
                }
                else if(boardStateArray[board, 4] == player && boardStateArray[board, 8] == player){
                    utils += utilsForWin;
                }
                break;
            case 1:
                if(boardStateArray[board, 0] == player && boardStateArray[board, 2] == player){
                    utils += utilsForWin;
                }
                else if(boardStateArray[board, 4] == player && boardStateArray[board, 7] == player){
                    utils += utilsForWin;
                }
                break;
            case 2:
                if(boardStateArray[board, 0] == player && boardStateArray[board, 1] == player){
                    utils += utilsForWin;
                }
                else if(boardStateArray[board, 4] == player && boardStateArray[board, 6] == player){
                    utils += utilsForWin;
                }
                else if(boardStateArray[board, 5] == player && boardStateArray[board, 8] == player){
                    utils += utilsForWin;
                }
                break;
            case 3:
                if(boardStateArray[board, 0] == player && boardStateArray[board, 6] == player){
                    utils += utilsForWin;
                }
                else if(boardStateArray[board, 4] == player && boardStateArray[board, 5] == player){
                    utils += utilsForWin;
                }
                break;
            case 4:
                if(boardStateArray[board, 0] == player && boardStateArray[board, 8] == player){
                    utils += utilsForWin;
                }
                else if(boardStateArray[board, 2] == player && boardStateArray[board, 6] == player){
                    utils += utilsForWin;
                }
                else if(boardStateArray[board, 1] == player && boardStateArray[board, 7] == player){
                    utils += utilsForWin;
                }
                else if(boardStateArray[board, 3] == player && boardStateArray[board, 5] == player){
                    utils += utilsForWin;
                }
                break;
            case 5:
                if(boardStateArray[board, 2] == player && boardStateArray[board, 8] == player){
                    utils += utilsForWin;
                }
                else if(boardStateArray[board, 3] == player && boardStateArray[board, 4] == player){
                    utils += utilsForWin;
                }
                break;
            case 6:
                if(boardStateArray[board, 0] == player && boardStateArray[board, 3] == player){
                    utils += utilsForWin;
                }
                else if(boardStateArray[board, 2] == player && boardStateArray[board, 4] == player){
                    utils += utilsForWin;
                }
                else if(boardStateArray[board, 7] == player && boardStateArray[board, 8] == player){
                    utils += utilsForWin;
                }
                break;
            case 7:
                if(boardStateArray[board, 1] == player && boardStateArray[board, 4] == player){
                    utils += utilsForWin;
                }
                else if(boardStateArray[board, 6] == player && boardStateArray[board, 8] == player){
                    utils += utilsForWin;
                }
                break;
            case 8:
                if(boardStateArray[board, 0] == player && boardStateArray[board, 4] == player){
                    utils += utilsForWin;
                }
                else if(boardStateArray[board, 2] == player && boardStateArray[board, 5] == player){
                    utils += utilsForWin;
                }
                else if(boardStateArray[board, 6] == player && boardStateArray[board, 7] == player){
                    utils += utilsForWin;
                }
                break;
        }

        return utils;
    }

    // Dont use this anymore
    private int ClampUtils(int _utils){
        // if(_utils < 0){
        //     _utils = 0;
        // }
        return _utils;
    }
}
