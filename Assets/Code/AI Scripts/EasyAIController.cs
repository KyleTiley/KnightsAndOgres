using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyAIController : AIController
{
    // EASY AI THAT JUST RANDOMLY PLAYS A TURN

    public void PlayTurn(){

        // Only collects tiles, since easy AI does not use minimax and rather chooses a random tile
        CollectAvailableTiles();

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
        Debug.Log(debugOutput);

        // Randomly chooses the tile to play
        int tileToPlay;
        tileToPlay = Random.Range(0, availableTiles.Count);
        availableTiles[tileToPlay].OnTileClick();
    }
    
}
