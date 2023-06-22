using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainProceduralGeneration : MonoBehaviour
{
    // REFERENCES
    public Sprite newGrassSprite;
    Color32 defaultGrassColour = new Color32(141, 237, 167, 255);
    Color32 grassBladeColour = new Color32(66, 190, 88, 255);

    // VARIABLES
    int grassTextureWidth = 256;
    int grassTextureHeight = 256;

    int numberOfStartingPoints = 9;
    int[,] startingPoints = new int[,]
    { {64, 32}, {64, 96}, {64, 160},
    {128, 32}, {128, 96}, {128, 160},
    {192, 32}, {192, 96}, {192, 160} };

    int bladesOfGrass = 3;
    int grassHeight = 50;
    
    public void GenerateGrassTile(){
        Texture2D thisGrassTexture = new Texture2D(grassTextureWidth, grassTextureHeight);

        // Fill with default grass colour
        for(int h = 0; h < grassTextureHeight; h++){
            for(int w = 0; w < grassTextureWidth; w++){
                thisGrassTexture.SetPixel(w, h, defaultGrassColour);
            }
        }

        // Generates blades of grass for each starting point
        for(int point = 0; point < numberOfStartingPoints; point++){
            thisGrassTexture.SetPixel(startingPoints[point, 0], startingPoints[point, 1], grassBladeColour);
            
            // Generates a blade at each starting point
            for(int blade = 0; blade < bladesOfGrass; blade++){
                // Calculate a an offset for each blade of grass
                float offset = (int)Mathf.Pow(-1,blade);
                // Create grass with a certain height
                for(int height = 0; height < grassHeight; height++){
                
                    System.Random rnd = new System.Random();
                    int newRnd = rnd.Next(-3,4);
                    thisGrassTexture.SetPixel(startingPoints[point, 0] + (int)offset + newRnd, startingPoints[point, 1] + height, grassBladeColour);

                    // Cummulative offset to spread out blades
                    offset += offset/10;
                }
            }
            // int grassBladeHeight = Random.Range(10,50);
            // 
            // for(int j = 0; j < grassBladeHeight; j++){
            //     int grassBladeThickness = Random.Range(0,5);
                    
            // }
        }
        
        // Add texture to grass with random dots
        // for(int h = 0; h < grassTextureHeight; h++){
        //     for(int w = 0; w < grassTextureWidth; w++){
        //         int randomChance = Random.Range(0,5);
        //         if(randomChance == 0){
        //             thisGrassTexture.SetPixel(w, h, grassBladeColour);
        //         }
        //     }
        // }

        // Applies the created texture and overwrites the blank texture
        thisGrassTexture.Apply();
        newGrassSprite = Sprite.Create(thisGrassTexture, new Rect(0.0f, 0.0f, thisGrassTexture.width, thisGrassTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
}
