using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetworkAI : AIController
{
    // I COULD NOT FINISH THE NEURAL NETWORK IN TIME
    // SO I COMMENTED IT OUT AND USE A RANDOM PLAY FUNCTION INSTEAD
    // JUST SO THE TURN IS AT LEAST PLAYED WHEN THE NEURAL NETWORK AI IS CALLED

    public void NeuralNetwork(){

        // Only collects tiles, since easy AI does not use minimax and rather chooses a random tile
        CollectAvailableTiles();

        // Randomly chooses the tile to play
        int tileToPlay;
        tileToPlay = Random.Range(0, availableTiles.Count);
        availableTiles[tileToPlay].OnTileClick();
    }
}
