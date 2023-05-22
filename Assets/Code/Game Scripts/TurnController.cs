using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    //??? make sure to allow switching between different ai difficulties
    // REFERENCES
    private GameController gameController;
    private EasyAIController easyAIController;
    private AIController aiController;

    // VARIABLES
    // Denotes the active player, true: player 1 knight, false: player 2 ogre
    public bool isPlayersTurn;
    private bool isAIStarting = false;

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

        aiController = gameController.aiController;

        if(isPlayersTurn){
            gameController.highlightColour = knightHighlight;
        }
        else{
            gameController.highlightColour = OgreHighlight;
            isAIStarting = true;
        }
    }

    // might want to use this for ai rather, might be a waste
    private void Start() {
        // Calls the AI to play the first move
        if(isAIStarting){
            aiController.MiniMax();
        }
    }

    // Switches from one player to the other
    public void SwitchPlayers(){
        isPlayersTurn = !isPlayersTurn;
        // Calls the AI to play their move
        if(!isPlayersTurn){
            //might have to change this to something better later
            if(gameController.winnerText.text == "Battle!"){
                aiController.MiniMax();
            }
        }
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
