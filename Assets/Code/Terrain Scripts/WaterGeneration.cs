using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterGeneration : MonoBehaviour
{
    // REFERENCES
    Color32 darkWater = new Color32(109, 171, 232, 255);
    Color32 lightWater = new Color32(165, 207, 245, 255);
    Color32 whiteWater = new Color32(250, 250, 250, 255);

    [SerializeField] RawImage waterTexture;

    // VARIABLES
    int waterWidth = 210;
    int waterHeight = 1080;
    float waveScale = 10.0f;
    int maxRnd = 10000;

    // FUNCTIONS
    private void Start() {
        GenerateWaterTexture();
    }

    public void GenerateWaterTexture(){
        Texture2D newWaterTexture = new Texture2D(waterWidth, waterHeight);

        for(int x = 0; x < waterWidth; x++){
            for(int y = 0; y < waterHeight; y++){
                Color32 pixelColor = GetPixelColour(x,y);
                newWaterTexture.SetPixel(x, y, pixelColor);
            }
        }

        newWaterTexture.Apply();
        waterTexture.texture = newWaterTexture;
    }

    private Color32 GetPixelColour(int _x, int _y){
        System.Random rnd = new System.Random();

        int rndX = rnd.Next(0,maxRnd);
        float xOffset = rndX / maxRnd;
        float x = (float)_x / waterWidth * waveScale;

        int rndY = rnd.Next(0,maxRnd);
        float yOffset = rndY / maxRnd;
        float y = (float)_y / waterHeight * waveScale;

        float perlinValue = Mathf.PerlinNoise(x + xOffset, y + yOffset);

        Color32 setColour;
        if(perlinValue < 0.2){
            setColour = whiteWater;
        }
        else if(perlinValue < 0.6){
            setColour = lightWater;
        }
        else{
            setColour = darkWater;
        }

        return setColour;
    }
}
