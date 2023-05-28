using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MiniMaxAI : AIController
{
    // VARIABLES
    int comparableUtility;
    int maximisedUtility;

    // this is for minimax
    public void MiniMax(){
        CollectAvailableTiles();

        int [] finalisedUtility = new int [availableTiles.Count];
        for( int i = 0; i < availableTiles.Count; i++){
            finalisedUtility[i] = EvaluateState(i);
        }

        int tileToPlayUtility = finalisedUtility.Max();
        int tileToPlay = finalisedUtility.ToList().IndexOf(tileToPlayUtility);
        availableTiles[tileToPlay].OnTileClick();
    }

    // either delete this or the above one? do need to use depth
    private void MiniMaxAlgorithm(int depth){
        for(int i = 0; i < availableTiles.Count; i++){
            int tempDepth = 0;
            while(tempDepth < depth){

            }

            //
            if(comparableUtility > maximisedUtility){
                maximisedUtility = comparableUtility;
            }
        }
    }

    // in minimax call this for each available board? then compare like below but with boards
    private int EvaluateState(int availableBoard){
        // Creates an array to store all cummulative utilities in
        int[] tileUtilities = new int[9];

        // Creates a temporary board state array as to not overwrite the original one
        int[,] tempBoardStateArray = boardStateArray;
        
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

    // Used to evaluate the utility value of individual tiles
    private int EvaluateTile(int tile){
        int calculatedUtility = 0;
        
        if(tile != 4){
            if(tile%2 != 0){
                calculatedUtility += sideTileValue;
            }
            else{
                calculatedUtility += cornerTileValue;
            }
        }
        else{
            calculatedUtility += centreTileValue;
        }

        return calculatedUtility;
    }

    public void MinimaxDebugger(){
        SaveBoardState();
        // string debugOutput = " \n";
        // for(int i = 0; i < 9; i++){
        //     string debugLine = "";
        //     for(int j = 0; j< 9; j++){
        //         debugLine += boardStateArray[i,j];
        //     }
        //     debugOutput += debugLine;
        //     debugOutput += '\n';
        // }
        // Debug.Log(debugOutput);
        // EvaluateState(1);
    }
}
