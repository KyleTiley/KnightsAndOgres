using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Add variables for AI
    // Might have to hard code shortcuts for starting moves to improve speed
    // A visualiser for AI moves would also be cool

    //VARIABLES FOR GAME

    [SerializeField]
    private List<Button> tileButtons = new List<Button>();

    private bool isPlayersTurn;   // true: player 1, false: player2
    private int tilesLeft;
    private bool isGameOver = true;

    // VARIABLES FOR MINMAX
    // do this later

    // METHODS FOR GAME

    private void Start() {
        StartGame();
    }

    // Called to start the game and choose a random starting player
    public void StartGame(){
        isPlayersTurn = Random.Range(0,2) == 1;
        Debug.Log(isPlayersTurn);
        ResetGame();
    }

    // Called to reset the game
    private void ResetGame(){
        //reset the game here
    }

    // Called when the game is over
    private void WinGame(){
        isGameOver = true;
        EnableTileButtons(false);
    }

    // Called if the game is a draw
    private void DrawGame(){
        // would i even need this?
    }

    private void EnableTileButtons(bool enabled, bool ignore = false){
        foreach(Button tileButton in tileButtons){
            if(!enabled || ignore || GetColour(tileButton) == Color.white){
                tileButton.interactable = enabled;
            }
        }
    }

    // Get the current colour of a button
    private Color GetColour(Button tileButton){
        return tileButton.GetComponent<Image>().color;
    }

    // Sets the tile to whichever player's token is placed, and then checks for a win
    private bool SetTileToken(Button tileButton){
        Color colour = GetColour(tileButton);
        if(colour != Color.white){
            return false;
        }
        colour = Color.black; // TEMP
        tilesLeft--;

        return CheckForWin(colour);
    }

    // Called when a tile button is clicked
    public void OnTileButtonClick(Button tileButton){
        // Resets game if it is over
        if(isGameOver){
            ResetGame();
            return;
        }
        // not sure if i need this
        if(tilesLeft <= 0){
            return;
        }

        // Checks if a player wins
        if(SetTileToken(tileButton)){
            WinGame();
        }

        tileButton.interactable = false;    // Can't interact with button again

        // Calls a draw if no tile buttons are left
        if(tilesLeft <= 0){
            DrawGame();
        }

        // Switches player turns
        isPlayersTurn = !isPlayersTurn;

        // this is where the ai turn should go when i add it? i think
    }

    // Checks if 3 of a player's token appears in a row
    private bool CheckForWin(Color token){
        if(tilesLeft > 6){
            return false;   // There can't be a win if only 2 tiles have been played
        }

        // Horizontal check
        if(CompareButtons(0, 1, 2, token) || CompareButtons(3, 4, 5, token) || CompareButtons(6, 7, 8, token)
            // Vertical check
            || CompareButtons(0, 3, 6, token) || CompareButtons(1, 4, 7, token) || CompareButtons(2, 5, 8, token)
            // Diagonal check
            || CompareButtons(0, 4, 8, token) || CompareButtons(2, 4, 6, token)){
            return true;    // There is a winner
        }
        return false;   // No winner yet
    }

    private bool CompareButtons(int check1, int check2, int check3, Color token){
        Color colour1 = GetColour(tileButtons[check1]);
        Color colour2 = GetColour(tileButtons[check2]);
        Color colour3 = GetColour(tileButtons[check3]);

        // Checks if all 3 token are the same
        bool isEqual = colour1 == token && colour2 == token && colour3 == token;

        // here should be the castle or camp creation when a player wins

        return isEqual;
    }

    

    

    // METHODS FOR MINMAX

    private bool IsAITurn(){
        return false;   // wont use ai yet
    }
}
