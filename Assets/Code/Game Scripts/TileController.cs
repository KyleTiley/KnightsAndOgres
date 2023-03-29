using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour
{
    // VARIABLES FOR TILE FUNCTIONALITY
    [SerializeField]
    private Button thisButton;

    //VARIABLES FOR TILE LOCATION
    [SerializeField]
    private string locationOnMainBoard;
    [SerializeField]
    private string locationOnBoard;

    // VARIABLES FOR TILE SPRITE
    [SerializeField]
    private Image thisSprite;    // To access the image sprite of the tile

    private void Awake() {
        thisButton = this.GetComponent<Button>();

        AssignTileLocations();
        thisButton.onClick.AddListener(OnTileClick);    // Sets the onClick event for the tile

        thisSprite = thisButton.GetComponent<Image>();
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
        Debug.Log("Clicked " + locationOnBoard + " on " + locationOnMainBoard);
    }
}
