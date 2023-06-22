using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// OLD SCRIPT NO LONGER IN USE

public class TerrainProceduralGeneration : MonoBehaviour
{
    // REFERENCES
    public Sprite newGrassSprite;
    Color32 defaultGrassColour = new Color32(141, 237, 167, 255);
    Color32 grassBladeColour = new Color32(66, 190, 88, 255);

    // VARIABLES
    int grassTextureWidth = 256;
    int grassTextureHeight = 256;
    
    int grassOnX = 3;
    int grassOnY = 4;

    int numberOfStartingPoints;
    int[,] startingPoints;

    int bladesOfGrass = 3;
    int grassHeight;
    
    public void GenerateGrassTile(){
        // Apparently there is no trash collection when using Sprite.Create, so this might be needed
        Resources.UnloadUnusedAssets();
        
        Texture2D thisGrassTexture = new Texture2D(grassTextureWidth, grassTextureHeight);

        // Sets height of grass sections
        grassHeight = grassTextureHeight / (grassOnY + 2);

        // Fill with default grass colour
        for(int h = 0; h < grassTextureHeight; h++){
            for(int w = 0; w < grassTextureWidth; w++){
                thisGrassTexture.SetPixel(w, h, defaultGrassColour);
            }
        }

        SetStartingPoints();

        // Generates blades of grass for each starting point
        for(int point = 0; point < numberOfStartingPoints; point++){
            thisGrassTexture.SetPixel(startingPoints[point, 0], startingPoints[point, 1], grassBladeColour);
            
            // Generates a blade at each starting point
            for(int blade = 0; blade < bladesOfGrass; blade++){
                // Calculate a linear converted offset for each blade of grass from a range of -1 to 1 (for some reason only works if left as float)
                float offset = (blade - 0) * (1 - -1) / (bladesOfGrass - 1 - 0) + -1;
                // Create grass with a certain height
                for(int height = 0; height < grassHeight; height++){
                
                    System.Random rnd = new System.Random();
                    int newRnd = rnd.Next(-1,2);

                    thisGrassTexture.SetPixel(startingPoints[point, 0] + (int)offset + newRnd, startingPoints[point, 1] + height, grassBladeColour);
                    thisGrassTexture.SetPixel(startingPoints[point, 0] + (int)offset + newRnd +1, startingPoints[point, 1] + height, grassBladeColour);
                    thisGrassTexture.SetPixel(startingPoints[point, 0] + (int)offset + newRnd -1, startingPoints[point, 1] + height, grassBladeColour);
                    thisGrassTexture.SetPixel(startingPoints[point, 0] + (int)offset + newRnd -2, startingPoints[point, 1] + height, grassBladeColour);
                    thisGrassTexture.SetPixel(startingPoints[point, 0] + (int)offset + newRnd +2, startingPoints[point, 1] + height, grassBladeColour);
                    
                    // Cummulative offset to spread out blades
                    offset += offset/(grassOnX + height);
                }
            }
        }
        
        // Add texture to grass with random dots
        for(int h = 0; h < grassTextureHeight; h++){
            for(int w = 0; w < grassTextureWidth; w++){
                int randomChance = Random.Range(0,10);
                if(randomChance == 0){
                    thisGrassTexture.SetPixel(w, h, grassBladeColour);
                }
            }
        }

        // Applies the created texture and overwrites the blank texture
        thisGrassTexture.Apply();
        newGrassSprite = Sprite.Create(thisGrassTexture, new Rect(0.0f, 0.0f, thisGrassTexture.width, thisGrassTexture.height), new Vector2(0.5f, 0.5f), 100.0f);

        // Should call sprite merger from here !!! if possible
    }

    // Sets starting points based on how many grass is wanted on the X and Y axis respectively
    private void SetStartingPoints(){
        numberOfStartingPoints = grassOnX * grassOnY;

        int xOffset = grassTextureWidth / (grassOnX + 1);
        int yOffset = grassTextureHeight / (grassOnY + 1);

        startingPoints = new int[numberOfStartingPoints,2];
        
        for(int xPoint = 0; xPoint < grassOnX; xPoint++){

            for(int yPoint = 0; yPoint < grassOnY; yPoint++){
                startingPoints[(grassOnX * xPoint) + yPoint, 0] = (xPoint * xOffset) + xOffset;
                startingPoints[(grassOnX * xPoint) + yPoint, 1] = (yPoint * yOffset) + yOffset/2;
            }
        }

        for(int i = 0; i < numberOfStartingPoints; i++){
            Debug.Log(startingPoints[i, 0] + " : " + startingPoints[i, 1]);
        }
    }
}
