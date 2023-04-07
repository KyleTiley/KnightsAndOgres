using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour
{
    // VARIABLES FOR TILE FUNCTIONALITY
    [SerializeField]
    private Button thisButton;
    public TurnController turnController;
    private BoardController boardController;
    [SerializeField]
    public bool canUseTile;

    // VARIABLES FOR TILE LOCATION
    [SerializeField]
    private string locationOnMainBoard;
    [SerializeField]
    private string locationOnBoard;
    [SerializeField]
    private char tileNumber;

    // VARIABLES FOR TILE SPRITE
    [SerializeField]
    public Image thisSprite;
    [SerializeField]
    private Sprite knightSprite;
    [SerializeField]
    private Sprite orcSprite;

    // METHODS
    private void Start() {
        // Sets up the global references
        GameController gameController = GameController.Instance;
        turnController = gameController.turnController;
        boardController = GetComponentInParent<BoardController>();

        // Sets tile to usable
        canUseTile = true;

        // Gets components of this tile button
        thisButton = this.GetComponent<Button>();
        thisSprite = thisButton.GetComponent<Image>();
        AssignTileLocations();

        // Sets the onClick event for the tile
        thisButton.onClick.AddListener(OnTileClick);    


        // the below spite loading should be done once, not for every button, fix this (or is it fine?)
        knightSprite = Resources.Load<Sprite>("Art/Knight_Test");
        orcSprite = Resources.Load<Sprite>("Art/Orc_Test");
    }

    // Used to assign location properties to the tile
    private void AssignTileLocations(){
        // Gets the name of the board
        locationOnMainBoard = transform.parent.name;
        // Gets the name of the tile
        locationOnBoard = transform.name;
        //Gets the tile number in the board
        tileNumber = locationOnBoard[locationOnBoard.Length-1];
    }

    // Called when this tile is clicked
    private void OnTileClick(){
        // Checks if tile is not disabled or already used
        if(canUseTile){
            // Disables use of this tile
            canUseTile = false;
            // Increases the tiles played on the tile's board
            boardController.tilesPlayed++;

            // Determines which symbol to place
            if(turnController.isPlayersTurn){
                thisSprite.sprite = knightSprite;
            }
            else{
                thisSprite.sprite = orcSprite;
            }

            // Checks if at least 3 tiles have been played, as that is the minimum to win a board
            if(boardController.tilesPlayed >= 3){
                // Calls to check if the board is won
                boardController.CheckBoardWinner(turnController.isPlayersTurn);
            }
            
            // Calls to choose the next board
            boardController.ChooseNextBoard(tileNumber);

            // Calls to switch players
            turnController.SwitchPlayers();
        }
    }
}
