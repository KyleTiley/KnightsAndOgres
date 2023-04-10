using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // REFERENCES FOR GLOBAL ACCESS
    public static GameController Instance { get; private set; }
    public TurnController turnController;
    // public List<BoardController> boardControllers = new List<BoardController>();    // is this needed???

    // IMAGES FOR TILES
    public Sprite grassSprite;
    public Sprite knightSprite;
    public Sprite ogreSprite;

    // VARIABLES FOR COLOURS
    public Color32 highlightColour;
    public Color32 defaultBoardColour = new Color32(0, 0, 0, 0);
    
    //FUNCTIONS
    // Called before any other script to create singleton
    private void Awake() {
        // Sets default grass sprite for each tile
        grassSprite = Resources.Load<Sprite>("PixelArt/GrassTile");

        // Sets the tile sprites for each player as the game starts
        knightSprite = Resources.Load<Sprite>("Art/Knight_Test");
        ogreSprite = Resources.Load<Sprite>("Art/Orc_Test");

        // Creates a singleton
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
}
