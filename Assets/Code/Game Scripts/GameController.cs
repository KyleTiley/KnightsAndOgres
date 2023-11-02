using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    // REFERENCES FOR GLOBAL ACCESS
    public static GameController GameControllerInstance { get; private set; }
    public TurnController turnController;
    public MainBoardController mainBoardController;
    [SerializeField] private GameObject mainBoard;
    public GrassGeneration grassGeneration;
    public SpriteMerger spriteMerger;

    // AI REFERENCES
    public AIController aIController;
    public EasyAIController easyAIController;
    public MiniMaxAI miniMaxAI;
    public NeuralNetworkAI neuralNetworkAI;

    // IMAGES FOR TILES
    public Sprite grassSprite1;
    public Sprite grassSprite2;
    public Sprite grassSprite3;
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
    
    // FUNCTIONS
    // Called before any other script to create singleton
    private void Awake() {
        // Sets UI to defaults
        winnerText.text = "Battle!";

        // Set singleton access
        difficultyController = DifficultyController.DifficultyControllerInstance;

        // Sets whether game is against AI or not
        if(difficultyController.gameType == "PVP"){
            turnController.gameIsAgainstAI = false;
        }
        else{
            turnController.gameIsAgainstAI = true;
        }
        
        // Sets default grass sprite for each tile
        for(int i = 0; i < 3; i++){
            grassGeneration.GenerateGrassTile(i);
            switch (i)
            {
                case 0:
                    grassSprite1 = grassGeneration.newGrassSprite;
                    break;
                case 1:
                    grassSprite2 = grassGeneration.newGrassSprite;
                    break;
                case 2:
                    grassSprite3 = grassGeneration.newGrassSprite;
                    break;
            }
        }
        defaultBoardColour = new Color32(0, 0, 0, 0);
        boardWinColour = new Color32(255, 255, 255, 255);

        // Sets the tile sprites for each player as the game starts
        var baseKnightSprite = Resources.Load<Sprite>("Art/Knight");
        var baseOgreSprite = Resources.Load<Sprite>("Art/Ogre");


        // Merges sprites with newly generated grass tile
        // Knight Sprite
        spriteMerger.spritesToMerge.Add(grassSprite1);
        spriteMerger.spritesToMerge.Add(baseKnightSprite);
        spriteMerger.MergeSprites();
        knightSprite = spriteMerger.finalSprite;
        knightSprite.name = "Knight";
        // Empties sprites to merge
        spriteMerger.spritesToMerge.Clear();
        // Ogre Sprite
        spriteMerger.spritesToMerge.Add(grassSprite1);
        spriteMerger.spritesToMerge.Add(baseOgreSprite);
        spriteMerger.MergeSprites();
        ogreSprite = spriteMerger.finalSprite;
        ogreSprite.name = "Ogre";

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

        // Used to get list for AI
        mainBoardController = mainBoard.GetComponent<MainBoardController>();
    }
}
