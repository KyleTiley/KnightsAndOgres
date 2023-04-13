using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    // REFERENCES
    private GameController gameController;

    // VARIABLES
    // Denotes the active player, true: player 1 knight, false: player 2 ogre
    public bool isPlayersTurn;

    //Player highlight colours
    private Color32 knightHighlight;
    private Color32 OgreHighlight;

    // FUNCTIONS
    private void Awake(){
        // Player colour declarations
        knightHighlight = new Color32(149, 105, 200, 150);
        OgreHighlight = new Color32(234, 175, 77, 150);

        isPlayersTurn = Random.Range(0,2) == 1;
        gameController = GameController.Instance;

        if(isPlayersTurn){
            gameController.highlightColour = knightHighlight;
        }
        else{
            gameController.highlightColour = OgreHighlight;
        }
    }

    // Switches from one player to the other
    public void SwitchPlayers(){
        isPlayersTurn = !isPlayersTurn;
    }

    // Changes highlight colour to show which player's turn it is
    public void SwitchColours(){
        if(!isPlayersTurn){
            gameController.highlightColour = knightHighlight;
        }
        else{
            gameController.highlightColour = OgreHighlight;
        }
    }
}
