using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour
{
    // VARIABLES FOR TILE FUNCTIONALITY
    [SerializeField]
    private Button thisButton;
    private TurnController turnController;
    private BoardController boardController;
    [SerializeField]
    private bool canUseTile;

    // VARIABLES FOR TILE LOCATION
    [SerializeField]
    private string locationOnMainBoard;
    [SerializeField]
    private string locationOnBoard;

    // VARIABLES FOR TILE SPRITE
    [SerializeField]
    private Image thisSprite;
    [SerializeField]
    private Sprite knightSprite;
    [SerializeField]
    private Sprite orcSprite;

    // METHODS
    private void Awake() {
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
    }

    // Called when this tile is clicked
    private void OnTileClick(){
        if(canUseTile){
            // Disables use of this tile
            canUseTile = false;
            boardController.tilesPlayed++;

            // Determines which symbol to place
            if(turnController.isPlayersTurn){
                thisSprite.sprite = knightSprite;
            }
            else{
                thisSprite.sprite = orcSprite;
            }
            
            if(boardController.tilesPlayed > 5){
                boardController.SetBoardWinner();
            }

            // Calls to switch players
            turnController.SwitchPlayers();
        }
    }
}
