using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyAIController : AIController
{
    // EASY AI THAT JUST RANDOMLY PLAYS A TURN

    public void RandomPlay(){

        // Only collects tiles, since easy AI does not use minimax and rather chooses a random tile
        CollectAvailableTiles();

        // Randomly chooses the tile to play
        int tileToPlay;
        tileToPlay = Random.Range(0, availableTiles.Count);
        availableTiles[tileToPlay].OnTileClick();
    }
}
