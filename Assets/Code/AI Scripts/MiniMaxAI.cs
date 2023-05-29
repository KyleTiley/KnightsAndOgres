using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MiniMaxAI : AIController
{
    // VARIABLES
    int[,] boardUtilityArray = new int[9,9];

    // FUNCTIONS
    public void MiniMax(){
        // Saves the board state and collects all available tiles that the AI can play?
        SaveBoardState();

        Array.Clear(boardUtilityArray, 0, boardUtilityArray.Length);
        // Loops through all available tiles in saved board state
        for(int i = 0; i < 9; i ++){
            for(int j = 0; j < 9; j++){
                if(boardStateArray[i,j] == '0'){
                    // Call to evaluate the empty tile
                    boardUtilityArray[i,j] = EvaluateTile(i, j);
                }
            }
        }

        // for(int depthExplored = 0; depthExplored < depthSpecified; depthExplored++){
        //     for(int i = 0; i < 9; i ++){
        //         for(int j = 0; j < 9; j++){
        //             if(boardUtilityArray[i,j] > 0){
        //                 // Call to evaluate the utility again
        //                 boardUtilityArray[i,j] = EvaluateTile(i, j);
        //             }
        //         }
        //     }
        // }

        // do something where you flip the utils based on who is playing

        // Check which tiles utility is highest
        int maxUtility = boardUtilityArray.Cast<int>().Max();
        int maxUtilityIndex = boardUtilityArray.Cast<int>().ToList().IndexOf(maxUtility);

        // Play the move for the AI
        // Debug.Log(maxUtilityIndex);
        allTiles[maxUtilityIndex].OnTileClick();

        // string debugOutput = " \n";
        // for(int i = 0; i < 9; i++){
        //     string debugLine = "";
        //     for(int j = 0; j< 9; j++){
        //         debugLine += boardUtilityArray[i,j];
        //     }
        //     debugOutput += debugLine;
        //     debugOutput += '\n';
        // }
        // Debug.Log(debugOutput);
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

        tileUtility = CheckForWin(i,j,'G');

        Debug.Log(tileUtility);
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

        return utils;
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

        return utils;
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
    private int ClampUtils(int _utils){
        if(_utils < 0){
            _utils = 0;
        }
        return _utils;
    }

    // in minimax call this for each available board? then compare like below but with boards
    // private int EvaluateState(int availableBoard){
    //     // Creates an array to store all cummulative utilities in
    //     int[] tileUtilities = new int[9];

    //     // Creates a temporary board state array as to not overwrite the original one
        
    //     for(int i = 0; i < 9; i++){
    //         int evaluatedUtility = 0;
    //         if(tempBoardStateArray[availableBoard,i] == 0){
    //             evaluatedUtility += EvaluateTile(i);

    //             // Does the same check but for the minimising player, now deducting the tile utility 
    //             for(int j = 0; j < 9; j++){
    //                 // Checks if the tile is empty AND if its not the previously played tile
    //                 if(tempBoardStateArray[i,j] == 0 && i !=j ){
    //                     evaluatedUtility -= EvaluateTile(j);
    //                 }
    //             }
    //         }
    //         tileUtilities[i] = evaluatedUtility;
    //         // Debug.Log(tileUtilities[i]);
    //     }

    //     // here you should check which tiles utility is highest
    //     int maxUtility = tileUtilities.Max();
    //     int maxUtilityIndex = tileUtilities.ToList().IndexOf(maxUtility);

    //     // Debug.Log(maxUtilityIndex + " " + maxUtility);
    //     return maxUtilityIndex;
    // }

    // Used to evaluate the utility value of individual tiles based on the outlined Utility Function
    // private int EvaluateTile(int tile){
    //     int calculatedUtility = 0;
        
    //     if(tile != 4){
    //         if(tile%2 != 0){
    //             calculatedUtility += sideTileValue;
    //         }
    //         else{
    //             calculatedUtility += cornerTileValue;
    //         }
    //     }
    //     else{
    //         calculatedUtility += centreTileValue;
    //         // foreach(int tileToCheck in tempBoardStateArray){
    //         //     if(tileToCheck != tile && tileToCheck != 0){
    //         //         calculatedUtility -= 1;
    //         //     }
    //         // }
    //     }

    //     // Returns the utility for this given tile
    //     return calculatedUtility;
    // }

    
}
