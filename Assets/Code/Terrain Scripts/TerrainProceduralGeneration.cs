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

    int[,] startingPoints = new int[9, 2]
    { {64, 64}, {64, 128}, {64, 192},
    {128, 64}, {128, 128}, {128, 192},
    {192, 64}, {192, 128}, {192, 192} };

    public void GenerateGrassTile(){
        Texture2D thisGrassTexture = new Texture2D(grassTextureWidth, grassTextureHeight);

        // Fill with default grass colour
        for(int h = 0; h < grassTextureHeight; h++){
            for(int w = 0; w < grassTextureWidth; w++){
                thisGrassTexture.SetPixel(w, h, defaultGrassColour);
            }
        }

        // Generate blades of grass for the sprite
        int pixelsPerGrass = 0;
        while(pixelsPerGrass < 20){
            // Generates for each starting point
            for(int i = 0; i < 9; i++){
                int grassBladeHeight = Random.Range(10,50);
                for(int j = 0; j < grassBladeHeight; j++){
                    int grassBladeThickness = Random.Range(0,5);
                    thisGrassTexture.SetPixel(startingPoints[i,0], startingPoints[i,1] + j, grassBladeColour);
                }
            }
            pixelsPerGrass++;
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
