using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldGameController : MonoBehaviour
{
    // CONTAINERS FOR BOARD
    [SerializeField]
    private List<GameObject> boardPanels = new List<GameObject>();
    [SerializeField]
    private List<Button> tileButtons = new List<Button>();
    [SerializeField]
    private List<Button> board1Buttons = new List<Button>();
    [SerializeField]
    private List<Button> board2Buttons = new List<Button>();
    [SerializeField]
    private List<Button> board3Buttons = new List<Button>();
    [SerializeField]
    private List<Button> board4Buttons = new List<Button>();
    [SerializeField]
    private List<Button> board5Buttons = new List<Button>();
    [SerializeField]
    private List<Button> board6Buttons = new List<Button>();
    [SerializeField]
    private List<Button> board7Buttons = new List<Button>();
    [SerializeField]
    private List<Button> board8Buttons = new List<Button>();
    [SerializeField]
    private List<Button> board9Buttons = new List<Button>();

    // VARIABLES FOR GAME
    private bool isPlayersTurn;   // true: player 1, false: player2
    private int tilesLeft;
    private bool isBoardOver = true;

    // VARIABLES FOR MINMAX
    // do this later
    // Might have to hard code shortcuts for starting moves to improve speed
    // A visualiser for AI moves would also be cool

    // METHODS FOR GAME

    private void Start() {
        StartGame();
        // Make this initiate from the menu rather, once that is set up
    }

    // Called to start the game and choose a random starting player
    public void StartGame(){
        isPlayersTurn = Random.Range(0,2) == 1;
        ResetBoard();
        ResetGame();
    }

    // Called to reset the board
    private void ResetBoard(){
        foreach(GameObject boardPanel in boardPanels){
            boardPanel.GetComponentInChildren<Button>().interactable = true;
            SetColour(boardPanel.GetComponentInChildren<Button>(), Color.white);
        }
        isBoardOver = false;
    }

    // Called to reset the game
    private void ResetGame(){
        foreach(Button tileButton in tileButtons){
            tileButton.interactable = true;
            SetColour(tileButton, Color.white);
        }
        tilesLeft = 9;
        isBoardOver = false;
    }

    // Called when the game is over on a single board
    private void WinBoard(){
        isBoardOver = true;
        DisableTileButtons(true);
    }

    // Called if the game is a draw
    private void DrawGame(){
        // would i even need this?
    }


    private void DisableTileButtons(bool disable){
        foreach(Button tileButton in tileButtons){
            if(disable || GetColour(tileButton) == Color.white){    //not sure if i need the getcolour part
                tileButton.interactable = !disable;
            }
        }
    }

    // Get the current colour of a button
    private Color GetColour(Button tileButton){
        Debug.Log(tileButton.GetComponent<Image>().color);
        return tileButton.GetComponent<Image>().color;
    }

    private void SetColour(Button tileButton, Color colour){
        tileButton.GetComponent<Image>().color = colour;
    }

    // Sets the tile to whichever player's token is placed, and then checks for a win
    private bool SetTileToken(Button tileButton){
        Color colour = GetColour(tileButton);
        if(colour != Color.white){
            Debug.Log("false");
            return false;
        }
        if(isPlayersTurn){
            SetColour(tileButton, Color.blue);
            Debug.Log("SET BLUE");
            Debug.Log(tileButton.GetComponent<Image>().color);
        }
        else if(!isPlayersTurn){
            SetColour(tileButton, Color.red);
            Debug.Log("SET RED");
            Debug.Log(tileButton.GetComponent<Image>().color);
        }
        tilesLeft--;
        colour = GetColour(tileButton);
        return CheckForWin(colour);
    }

    // Called when a tile button is clicked to check for all scenarios
    public void OnTileButtonClick(Button tileButton){
        // Resets game if it is over (used to do this)
        if(isBoardOver){
            // set the symbol here eventually
            return;
        }
        // not sure if i need this
        if(tilesLeft <= 0){
            return;
        }

        // Checks if a player wins a board
        if(SetTileToken(tileButton)){
            WinBoard();
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
