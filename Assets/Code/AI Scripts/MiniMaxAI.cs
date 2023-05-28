using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MiniMaxAI : AIController
{
    // VARIABLES
    int[,] tempBoardStateArray;

    // FUNCTIONS
    public void MiniMax(){
        // Saves the board state and collects all available tiles that the AI can play
        SaveBoardState();
        CollectAvailableTiles();

        // An array for the finalised utility value assigned to each available tile
        int [] finalisedUtility = new int [availableTiles.Count];

        // Calls all functions needed to perform the MiniMax algorithm as many times as the depth specifies
        for(int depth = 0; depth < specifiedDepth; depth++){

        }

        for( int i = 0; i < availableTiles.Count; i++){
            // Creates a temporary board state array as to not overwrite the original one
            tempBoardStateArray = boardStateArray;
            finalisedUtility[i] = EvaluateState(i);
        }

        // Finds the available tile with the highest utility
        int tileToPlayUtility = finalisedUtility.Max();
        // Finds the index of said tile
        int tileToPlay = finalisedUtility.ToList().IndexOf(tileToPlayUtility);
        // Plays said tile for the AI
        availableTiles[tileToPlay].OnTileClick();
    }

    // in minimax call this for each available board? then compare like below but with boards
    private int EvaluateState(int availableBoard){
        // Creates an array to store all cummulative utilities in
        int[] tileUtilities = new int[9];

        // Creates a temporary board state array as to not overwrite the original one
        
        
        for(int i = 0; i < 9; i++){
            int evaluatedUtility = 0;
            if(tempBoardStateArray[availableBoard,i] == 0){
                evaluatedUtility += EvaluateTile(i);

                // Does the same check but for the minimising player, now deducting the tile utility 
                for(int j = 0; j < 9; j++){
                    // Checks if the tile is empty AND if its not the previously played tile
                    if(tempBoardStateArray[i,j] == 0 && i !=j ){
                        evaluatedUtility -= EvaluateTile(j);
                    }
                }
            }
            tileUtilities[i] = evaluatedUtility;
            // Debug.Log(tileUtilities[i]);
        }

        // here you should check which tiles utility is highest
        int maxUtility = tileUtilities.Max();
        int maxUtilityIndex = tileUtilities.ToList().IndexOf(maxUtility);

        Debug.Log(maxUtilityIndex + " " + maxUtility);
        return maxUtilityIndex;
    }

    // Used to evaluate the utility value of individual tiles based on the outlined Utility Function
    private int EvaluateTile(int tile){
        int calculatedUtility = 0;
        
        if(tile != 4){
            if(tile%2 != 0){
                calculatedUtility += sideTileValue;
                // if(tempBoardStateArray[4] != 0){

                // }
            }
            else{
                calculatedUtility += cornerTileValue;
            }
        }
        else{
            calculatedUtility += centreTileValue;
            foreach(int tileToCheck in tempBoardStateArray){
                if(tileToCheck != tile && tileToCheck != 0){
                    calculatedUtility -= 1;
                }
            }
        }

        // Returns the utility for this given tile
        return calculatedUtility;
    }
}
