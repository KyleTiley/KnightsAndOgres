using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour
{
    // REFERENCES
    private GameController gameController;
    private TurnController turnController;
    private BoardController boardController;
    private MainBoardController mainBoardController;

    // VARIABLES FOR TILE FUNCTIONALITY
    private Button thisButton;
    public bool canUseTile;

    // VARIABLES FOR TILE LOCATION (mainly for debugging purposes)
    private string locationOnMainBoard;
    private string locationOnBoard;
    private char tileNumber;

    // VARIABLES FOR TILE SPRITE
    public Image thisSprite;

    // FUNCTIONS
    private void Awake() {
        // Sets up the global references
        gameController = GameController.GameControllerInstance;
        turnController = gameController.turnController;
        boardController = GetComponentInParent<BoardController>();
        mainBoardController = transform.parent.GetComponentInParent<MainBoardController>();

        // Gets components of this tile button
        thisButton = this.GetComponent<Button>();
        thisSprite = thisButton.GetComponent<Image>();
        AssignTileLocations();

        // Sets the onClick event for the tile
        thisButton.onClick.AddListener(OnTileClick);

        // Sets tile to default at the start of game
        ResetTile();
    }

    // Resets the tile to default state
    public void ResetTile(){
        // Sets tile to usable
        canUseTile = true;

        // Sets the sprite of the tile to one of the 3 grass tile, randomly
        System.Random rnd = new System.Random();
        int rndGrass = rnd.Next(0,3);
        switch (rndGrass)
            {
                case 0:
                    thisSprite.sprite = gameController.grassSprite1;
                    break;
                case 1:
                    thisSprite.sprite = gameController.grassSprite2;
                    break;
                case 2:
                    thisSprite.sprite = gameController.grassSprite3;
                    break;
            }
    }

    // Used to assign location properties to the tile
    private void AssignTileLocations(){
        // Gets the name of the board
        locationOnMainBoard = transform.parent.name;
        // Gets the name of the tile
        locationOnBoard = transform.name;
        // Gets the tile number in the board
        tileNumber = locationOnBoard[locationOnBoard.Length-1];
    }

    // Called when this tile is clicked
    public void OnTileClick(){
        // Checks if tile is not disabled or already used
        if(canUseTile){
            // Disables use of this tile
            canUseTile = false;
            // Increases the tiles played on the tile's board
            boardController.tilesPlayed++;

            // Determines which symbol to place
            if(turnController.isPlayersTurn){
                thisSprite.sprite = gameController.knightSprite;
            }
            else{
                thisSprite.sprite = gameController.ogreSprite;
            }

            // Checks if at least 3 tiles have been played, as that is the minimum to win a board
            if(boardController.tilesPlayed >= 3){
                // Calls to check if the board is won
                boardController.CheckBoardWinner(turnController.isPlayersTurn);
            }

            // Checks for a tie in the board before doing anything else
            boardController.CheckForTie();

            // Calls to switch colours befoer choosing the next board
            turnController.SwitchColours();
            
            // Calls to choose the next board
            boardController.ChooseNextBoard(tileNumber);

            // Checks if the game is won
            mainBoardController.CheckGameWinner(turnController.isPlayersTurn);

            // Calls to switch players
            turnController.SwitchPlayers();
        }
    }
}
