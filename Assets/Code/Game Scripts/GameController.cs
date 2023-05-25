using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    // REFERENCES FOR GLOBAL ACCESS
    public static GameController GameControllerInstance { get; private set; }
    public TurnController turnController;
    public MainBoardController mainBoardController;
    [SerializeField] private GameObject mainBoard;

    // AI REFERENCES
    public EasyAIController easyAIController;
    public MiniMaxAI miniMaxAI;

    // IMAGES FOR TILES
    public Sprite grassSprite;
    public Sprite knightSprite;
    public Sprite ogreSprite;

    // IMAGES FOR BOARDS
    public Sprite castleSprite;
    public Sprite hutSprite;

    // VARIABLES FOR COLOURS
    public Color32 highlightColour;
    public Color32 defaultBoardColour;
    public Color32 boardWinColour;

    // VARIABLES FOR TEXT
    public TextMeshProUGUI winnerText;

    // SINGLETON ACCESS FOR AI FUNCTIONALITY
    private DifficultyController difficultyController;
    
    //FUNCTIONS
    // Called before any other script to create singleton
    private void Awake() {
        // Sets UI to defaults
        winnerText.text = "Battle!";

        // Set singleton access
        difficultyController = DifficultyController.DifficultyControllerInstance;

        // Sets whether game is against AI or not
        if(difficultyController.gameType){
            turnController.gameIsAgainstAI = false;
        }
        else{
            turnController.gameIsAgainstAI = true;
        }
        
        // Sets default grass sprite for each tile
        grassSprite = Resources.Load<Sprite>("Art/GrassTile");
        defaultBoardColour = new Color32(0, 0, 0, 0);
        boardWinColour = new Color32(255, 255, 255, 255);

        // Sets the tile sprites for each player as the game starts
        knightSprite = Resources.Load<Sprite>("Art/Knight");
        ogreSprite = Resources.Load<Sprite>("Art/Ogre");

        // Sets the board winning sprites for each player
        castleSprite = Resources.Load<Sprite>("Art/Castle");
        hutSprite = Resources.Load<Sprite>("Art/Hut");

        // Creates game controller singleton
        if(GameControllerInstance == null){
            GameControllerInstance = this;
        }
        else{
            Destroy(gameObject);
        }

        // this might be needed to get list for ai
        mainBoardController = mainBoard.GetComponent<MainBoardController>();
    }
}
