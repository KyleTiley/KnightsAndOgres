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
    // Dimensions of water texture
    int waterWidth = 210;
    int waterHeight = 1080;
    // 
    [SerializeField] float xOrigin = 0.0f;
    [SerializeField] float yOrigin = 0.0f;
    [SerializeField] float waveScale = 5.0f;
    int maxRnd = 10000;

    [SerializeField] bool toggle = false;

    // FUNCTIONS
    private void Start() {
        System.Random rnd = new System.Random();
        xOrigin = rnd.Next(0, maxRnd);
        yOrigin = rnd.Next(0, maxRnd);
        GenerateWaterTexture();
    }

    private void Update() {
        // Used for testing purposes
        if(toggle){
            GenerateWaterTexture();
            toggle = false;
        }
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
        float x = ((float)_x + xOrigin) / waterWidth * waveScale;
        float y = ((float)_y + yOrigin) / waterHeight * waveScale;

        float perlinValue = Mathf.PerlinNoise(x, y);

        Color32 setColour;
        if(perlinValue < 0.2){
            setColour = whiteWater;
        }
        else if(perlinValue < 0.5){
            setColour = lightWater;
        }
        else{
            setColour = darkWater;
        }

        return setColour;
    }
}
