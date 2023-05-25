using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMaxAI : AIController
{
    // private int depth = 5;

    // this is for minimax
    public void PlayTurn(){
        // CollectAvailableTiles();

        // // int tileToPlay;

        // for(int i = 0; i < boardStateArray.Length; i++){
        //     if(boardStateArray.Length == ){

        //     }
        // }

        // FOR testing only!!!
        SaveBoardState();
        
        string debugOutput = " \n";
        for(int i = 0; i < 9; i++){
            string debugLine = "";
            for(int j = 0; j< 9; j++){
                debugLine += boardStateArray[i,j];
            }
            debugOutput += debugLine;
            debugOutput += '\n';
        }
        // Debug.Log(debugOutput);
        MiniMaxAlgorithm();
        // availableTiles[tileToPlay].OnTileClick();
    }

    private void MiniMaxAlgorithm(){
        int utility = 0;
        
        for(int i = 0; i < 9; i++){
            for(int j = 0; j < 9; j++){
                utility += boardStateArray[i,j];
            }
        }

        Debug.Log(utility);
    }
}
