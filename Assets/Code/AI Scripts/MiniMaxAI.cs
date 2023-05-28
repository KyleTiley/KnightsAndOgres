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

        string debugOutput = " \n";
        for(int i = 0; i < 9; i++){
            string debugLine = "";
            for(int j = 0; j< 9; j++){
                debugLine += boardUtilityArray[i,j];
            }
            debugOutput += debugLine;
            debugOutput += '\n';
        }
        Debug.Log(debugOutput);


        // An array for the finalised utility value assigned to each available tile
        // int [] finalisedUtility = new int [availableTiles.Count];

        // // Calls all functions needed to perform the MiniMax algorithm as many times as the depth specifies
        // for(int depth = 0; depth < specifiedDepth; depth++){

        // }

        // for( int i = 0; i < availableTiles.Count; i++){
        //     // Creates a temporary board state array as to not overwrite the original one
        //     // tempBoardStateArray = boardStateArray;
        //     finalisedUtility[i] = EvaluateState(i);
        // }

        // // Finds the available tile with the highest utility
        // int tileToPlayUtility = finalisedUtility.Max();
        // // Finds the index of said tile
        // int tileToPlay = finalisedUtility.ToList().IndexOf(tileToPlayUtility);
        // // Plays the tile for the AI
        // availableTiles[tileToPlay].OnTileClick();
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
        // Assigned these -1 to see if anything breaks
        int ignored1 = -1;
        int ignored2 = -1;

        utils += cornerTileValue;
        for(int i = 0; i < 9; i++){
            if(i == 0){
                ignored1 = 5;
                ignored2 = 7;
            }
            else if(i == 2){
                ignored1 = 3;
                ignored2 = 7;
            }
            else if(i == 6){
                ignored1 = 1;
                ignored2 = 5;
            }
            else if(i == 8){
                ignored1 = 1;
                ignored2 = 3;
            }
            
            if(boardStateArray[board, i] == 'K'){
                if(i != ignored1 || i != ignored2){
                    utils -= 1;
                }
            }
        }

        return utils;
    }

    private int CheckSideUtility(int board, int tile){
        int utils = 0;

        utils += sideTileValue;

        return utils;
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
