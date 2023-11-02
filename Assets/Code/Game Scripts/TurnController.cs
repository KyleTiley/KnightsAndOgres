using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    // REFERENCES
    private GameController gameController;
    private AIController aIController;

    // VARIABLES
    // Denotes the active player, true: player 1 knight, false: player 2 ogre
    public bool isPlayersTurn;
    public bool isAIStarting = false;
    public bool gameIsAgainstAI;

    //Player highlight colours
    private Color32 knightHighlight;
    private Color32 OgreHighlight;

    // FUNCTIONS
    private void Awake(){
        // Player colour declarations
        knightHighlight = new Color32(149, 105, 200, 150);
        OgreHighlight = new Color32(234, 175, 77, 150);

        // Randomly decides who starts the game
        isPlayersTurn = Random.Range(0,2) == 1;

        // Allows access to other scripts using GameController Singleton
        gameController = GameController.GameControllerInstance;
        aIController = gameController.aIController;

        // Swaps turn functionality based on if playing against AI or not
        if(isPlayersTurn){
            gameController.highlightColour = knightHighlight;
        }
        else{
            gameController.highlightColour = OgreHighlight;
            if(gameIsAgainstAI){
                isAIStarting = true;
            }
        }
    }

    private void Start() {
        // Calls the AI to play the first move if it is their turn
        if(isAIStarting){
            aIController.PlayTurn(true);
        }
    }

    // Switches from one player to the other
    public void SwitchPlayers(){
        isPlayersTurn = !isPlayersTurn;
        // Calls the AI to play their move if it is their turn
        if(!isPlayersTurn){
            if(gameIsAgainstAI){
                if(!gameController.winnerText.text.Contains("Win")){
                    aIController.PlayTurn(false);
                }
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
